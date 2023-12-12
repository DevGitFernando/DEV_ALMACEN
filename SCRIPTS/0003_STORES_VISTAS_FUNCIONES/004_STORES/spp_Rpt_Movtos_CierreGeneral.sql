


If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_Movtos_CierreGeneral' and Type = 'P' ) 
Drop Proc spp_Rpt_Movtos_CierreGeneral
Go--#SQL 

---		Exec spp_Rpt_Movtos_CierreGeneral  '001', '21', '1113', '', '2014-07-01', '2014-07-17'    
 
   
Create Proc spp_Rpt_Movtos_CierreGeneral 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0018', 
	@FolioCierre varchar(20) = '2', @FechaInicio varchar(10) = '2014-07-01', @FechaFin varchar(10) = '2014-07-11'
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@FechaInicial datetime,
	@FechaFinal datetime,
	
	@FolioMinimo int,
	@FolioMaximo int,
	@sPersonal varchar(100)
	
	Set @FechaInicial = getdate()
	Set @FechaFinal = getdate()
	
	Set @FolioMinimo = 0
	Set @FolioMaximo = 0
	
	Set @sPersonal = 'CEDIS'
	
	----- se genera tabla temporal de productos--codigoean   -------------------
	Select * Into #tmp_vw_Productos_CodigoEAN From vw_Productos_CodigoEAN (Nolock)
	----------------------------------------------------------------------------
	
	If @FolioCierre <> ''
		Begin
			
			Select E.IdEmpresa, Em.Nombre as Empresa, E.IdEstado, Ed.Nombre as Estado, E.IdFarmacia, F.NombreFarmacia As Farmacia, E.FolioCierre, 
			E.FolioCierre as FolioMinimo, E.FolioCierre as FolioMaximo, 
			E.IdPersonal, (P.Nombre + ' ' + P.ApPaterno + ' ' + P.ApMaterno) as Personal,
			E.FechaRegistro, E.FechaInicial, E.FechaFinal, 
			D.IdTipoMovto_Inv, M.Efecto_Movto, M.Descripcion as DescMovto, D.Claves, D.Piezas, 
			D.Importe_Licitacion, D.Importe_Inventario
			From Ctl_CierresGeneral E (Nolock) 
			Inner Join Ctl_CierresGeneralDetalles D (Nolock)
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioCierre = D.FolioCierre )
			Inner Join Movtos_Inv_Tipos M (Nolock) On ( M.IdTipoMovto_Inv = D.IdTipoMovto_Inv )
			Inner Join CatEmpresas Em (Nolock) On ( Em.IdEmpresa = E.IdEmpresa )
			Inner Join CatEstados Ed (Nolock) On ( Ed.IdEstado = E.IdEstado )
			Inner Join CatFarmacias F (Nolock) On ( F.IdEstado = E.IdEstado AND F.IdFarmacia = E.IdFarmacia )
			Inner Join CatPersonal P (Nolock) On ( P.IdEstado = E.IdEstado AND P.IdFarmacia = E.IdFarmacia AND P.IdPersonal = E.IdPersonal )
			Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and E.FolioCierre = @FolioCierre
			Order By M.Efecto_Movto
			
		End
	Else
		Begin
		
			Select @FolioMinimo = Min(FolioCierre), @FolioMaximo = Max(FolioCierre), 
			@FechaInicial = Min(FechaInicial), @FechaFinal = Max(FechaFinal) From Ctl_CierresGeneral (Nolock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and CONVERT(varchar(10), FechaRegistro, 120) Between @FechaInicio and @FechaFin
			
			
		
			Select E.IdTipoMovto_Inv, P.IdClaveSSA_Sal, Sum(D.Cantidad) as Piezas, SUM(Importe) as ImporteInv
			Into #tmpMovtos_ClaveSSA
			From MovtosInv_Enc E (Nolock)
			Inner Join MovtosInv_Det_CodigosEAN D (Nolock)
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )
			Inner Join #tmp_vw_Productos_CodigoEAN P (Nolock) On ( P.IdProducto = D.IdProducto and P.CodigoEAN = D.CodigoEAN )			
			Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia 
			and E.FolioCierre in ( Select FolioCierre From Ctl_CierresGeneral (Nolock) 
								   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
								   and CONVERT(varchar(10), FechaRegistro, 120) Between @FechaInicio and @FechaFin
								  )
			Group By E.IdTipoMovto_Inv, P.IdClaveSSA_Sal
			
			-------------------  SE GENERA LA TABLA CON PRECIOS ----------------------------------------------------------------
			Select T.IdTipoMovto_Inv, T.IdClaveSSA_Sal, T.Piezas, T.ImporteInv, IsNull((AVG(C.PrecioUnitario)), 0 ) as Precio
			Into #tmpMovtos_ClaveSSA_Precios
			From #tmpMovtos_ClaveSSA T (Nolock)
			Left Join vw_Claves_Precios_Asignados C (Nolock) On ( C.IdClaveSSA = T.IdClaveSSA_Sal and C.IdEstado = @IdEstado )
			Group By T.IdTipoMovto_Inv, T.IdClaveSSA_Sal, T.Piezas, T.ImporteInv
			
			--------   SE GENERA LA TABLA DE CONCENTRADO   ------------------------------------------------------------------------------------
			Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, cast(@FolioCierre as Int) as FolioCierre, 
			@FolioMinimo as FolioMinimo, @FolioMaximo as FolioMaximo, GetDate() as FechaRegistro,
			@FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 
			T.IdTipoMovto_Inv,  
			Count(T.IdClaveSSA_Sal) as Claves, sum(T.Piezas) as Piezas, sum((T.Piezas * T.Precio)) as Importe_Licitacion,
			SUM(T.ImporteInv) AS Importe_Inventario
			Into #tmpMovtos_ClaveSSA_Concentrado
			From  #tmpMovtos_ClaveSSA_Precios T (Nolock)			
			Group By T.IdTipoMovto_Inv 
			
			
			------------  SE GENERA LA TABLA FINAL  --------------------------------------------------------------------------------------------
			Select E.IdEmpresa, Em.Nombre as Empresa, E.IdEstado, Ed.Nombre as Estado, E.IdFarmacia, F.NombreFarmacia As Farmacia, E.FolioCierre, 
			E.FolioMinimo, E.FolioMaximo, 
			'0000' As IdPersonal, @sPersonal As Personal, 
			E.FechaRegistro, E.FechaInicial, E.FechaFinal, 
			E.IdTipoMovto_Inv, M.Efecto_Movto, M.Descripcion as DescMovto, E.Claves, E.Piezas, 
			E.Importe_Licitacion, E.Importe_Inventario
			Into #tmpMovtos_ClaveSSA_Final
			From #tmpMovtos_ClaveSSA_Concentrado E (Nolock)			
			Inner Join Movtos_Inv_Tipos M (Nolock) On ( M.IdTipoMovto_Inv = E.IdTipoMovto_Inv )
			Inner Join CatEmpresas Em (Nolock) On ( Em.IdEmpresa = E.IdEmpresa )
			Inner Join CatEstados Ed (Nolock) On ( Ed.IdEstado = E.IdEstado )
			Inner Join CatFarmacias F (Nolock) On ( F.IdEstado = E.IdEstado AND F.IdFarmacia = E.IdFarmacia )
			
			---------   SE DEVUELVE LA TABLA FINAL  ---------------------------------------------------------------------------------------------
			Select * From #tmpMovtos_ClaveSSA_Final (Nolock)
			Order By Efecto_Movto
			
		End
	
End  
Go--#SQL 


	