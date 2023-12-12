--------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__Impresion_CortesParciales' And xType = 'P' )
	Drop Proc spp_INT_MA__Impresion_CortesParciales
Go--#SQL  

Create Procedure spp_INT_MA__Impresion_CortesParciales 
(	
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0011', 
	@FechaDeSistema varchar(10) = '2017-01-04', @IdPersonal varchar(4) = '' -- '0014'   --- , @IdCorte varchar(2) = '02' 
) 
With Encryption   
As
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @FechaSistema datetime, 
		@EsCorte int, 
		@TasaIvaBase int 
		 
Declare @GrupoVentas int, 
		@GrupoDevDelDia int, 
	    @GrupoDevDelDiaAnt int,  
	    @GrupoTiempoAire int  

	Set @GrupoVentas = 1 
	Set @GrupoDevDelDia = 2 
	Set @GrupoDevDelDiaAnt = 3 
	Set @GrupoTiempoAire = 4 
		
	Set @EsCorte = 1 
	Set @FechaSistema = cast(@FechaDeSistema as datetime) 
	Set @IdPersonal = IsNull(@IdPersonal, '') 
	Set @TasaIvaBase = 0 

	Set @IdEmpresa = RIGHT('0000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('0000000' + @IdEstado, 2) 
	Set @IdFarmacia = RIGHT('0000000' + @IdFarmacia, 4) 
	
	if @IdPersonal  <> '' and @IdPersonal <> '0'
		Set @IdPersonal = RIGHT('0000000' + @IdPersonal, 4) 


	--------------------------------------
	-- Obtener la información de Ventas --
	--------------------------------------

	Select top 0  
		cast('' as varchar(10)) as IdEmpresa, 
		cast('' as varchar(10)) as IdEstado, 
		cast('' as varchar(10)) as IdFarmacia, 
		cast('' as varchar(10)) as FolioVenta, 
		@FechaSistema as FechaSistema, 'Venta' + space(50) as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		0 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, 1 as Grupo, 'Venta' + space(50) as NombreGrupo, space(6) as IdPersonal,  
		cast(0 as numeric(14,2)) as Porcentaje_Copago, 
		cast(0 as numeric(14,4)) as SubTotal_SinGravar, 
		cast(0 as numeric(14,4)) as SubTotal_Gravado, 
		cast(0 as numeric(14,4)) as DescuentoCopago, 
		cast(0 as numeric(14,4)) as Importe_SinGravar, 
		cast(0 as numeric(14,4)) as Importe_Gravado, 
		cast(0 as numeric(14,4)) as IVA_Gral, 
		cast(0 as numeric(14,4)) as Importe_Neto,
		identity(int, 1, 1) as Keyx   
	Into #tmpCorteParcial 
 
 ----SubTotal_SinGravar, SubTotal_Gravado, DescuentoCopago, Importe_SinGravar, Importe_Gravado, IVA, Importe_Neto 
 ----0, 0, 0, 0, 0, 0, 0  

	------Ventas de Contado 
	----Insert Into #tmpCorteParcial ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FechaSistema, TipoMovto, SubTotal, Iva, Total, 
	----	TipoDeVenta, TasaIva, Grupo, NombreGrupo, IdPersonal, 
	----	Porcentaje_Copago, SubTotal_SinGravar, SubTotal_Gravado, DescuentoCopago, Importe_SinGravar, Importe_Gravado, IVA_Gral, Importe_Neto  )
	----Select '', '', '', '' as FolioVenta, @FechaSistema as FechaSistema, 'Venta' as TipoMovto, 
	----	cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
	----	0 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoVentas, 'Venta' as NombreGrupo, '', 
	----	0, 0, 0, 0, 0, 0, 0, 0     


--		spp_INT_MA__Impresion_CortesParciales 


	Insert Into #tmpCorteParcial ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FechaSistema, TipoMovto, SubTotal, Iva, Total, 
		TipoDeVenta, TasaIva, Grupo, NombreGrupo, IdPersonal, 
		Porcentaje_Copago, SubTotal_SinGravar, SubTotal_Gravado, DescuentoCopago, Importe_SinGravar, Importe_Gravado, IVA_Gral, Importe_Neto )
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVenta, FechaSistema, (case when TipoDeVenta = 1 then 'Asociados' else 'Beneficiarios' end) as TipoMovto, 
		sum(D.PrecioUnitario * D.CantidadVendida) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		TipoDeVenta, TasaIva, @GrupoVentas, 
		(case when TipoDeVenta = 1 then 'Asociados' else 'Beneficiarios' end) as NombreGrupo, 
		IdPersonal, 0, 0, 0, 0, 0, 0, 0, 0  
	-- Into #tmpCorteParcial 
	From VentasEnc E (NoLock)  
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  and FechaSistema = @FechaSistema and E.Corte = @EsCorte  
	Group by E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVenta, E.FechaSistema, E.TipoDeVenta, D.TasaIva, E.IdPersonal   	  
	Order by E.TipoDeVenta 



	Update E Set 
		Porcentaje_Copago = D.Porcentaje_01, 
		SubTotal_SinGravar = D.SubTotal_SinGravar, 
		SubTotal_Gravado = D.SubTotal_Gravado, 
		DescuentoCopago = D.DescuentoCopago, 
		Importe_SinGravar = D.Importe_SinGravar, 
		Importe_Gravado = D.Importe_Gravado, 
		IVA_Gral = D.IVA, 
		Importe_Neto = D.Importe_Neto 
	From #tmpCorteParcial E 
	Inner Join INT_MA_Ventas_Importes D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 

		

	----------------------------
	-- Salida para el Reporte --
	----------------------------  
	If @IdPersonal <> '' 
	   Update #tmpCorteParcial Set IdPersonal = @IdPersonal Where IdPersonal = '' 
		
		
	Select R.IdEmpresa, C.Nombre as Empresa, C.RFC, 
		   (F.Estado + ', ' + F.Municipio ) as EdoCiudad, 
		   F.Colonia, F.Domicilio, F.CodigoPostal, 
		   R.IdEstado, F.Estado, R.IdFarmacia, F.Farmacia, R.IdPersonal, IsNull(v.NombreCompleto, '') as NombreCompleto, 
		   R.FechaSistema, R.TipoMovto, R.SubTotal, R.Iva, R.Total, 
		   
		   R.Porcentaje_Copago, 
		   R.SubTotal_SinGravar, R.SubTotal_Gravado, R.DescuentoCopago, R.Importe_SinGravar, R.Importe_Gravado, R.IVA_Gral, R.Importe_Neto, 
		   
		   R.Grupo, R.NombreGrupo, R.TipoDeVenta, R.TasaIva, R.Keyx, 
		   getdate() as FechaImpresion  
	Into #tmpCorteParcialFinal 	   
	From 
	( 
		Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, IdPersonal,  
			   FechaSistema, TipoMovto, 
			   sum(SubTotal) as SubTotal, sum(Iva) as Iva, sum(Total) as Total, 
			   Porcentaje_Copago, 
			   sum(SubTotal_SinGravar) as SubTotal_SinGravar, sum(SubTotal_Gravado) as SubTotal_Gravado, 
			   sum(DescuentoCopago) as DescuentoCopago, 
			   sum(Importe_SinGravar) as Importe_SinGravar, sum(Importe_Gravado) as Importe_Gravado, 
			   sum(IVA_Gral) as IVA_Gral, sum(Importe_Neto) as Importe_Neto, 
			   Grupo, NombreGrupo, TipoDeVenta, 0 as TasaIva, 0 as Keyx
		From #tmpCorteParcial (NoLock) 
		Group by IdPersonal, FechaSistema, TipoMovto, TipoDeVenta, Grupo, NombreGrupo, TipoDeVenta, Porcentaje_Copago 
	) R 
	Inner Join CatEmpresas C (NoLock) On ( R.IdEmpresa = C.IdEmpresa )
	Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 
	Left Join vw_Personal v (NoLock) On ( R.IdEstado = V.IdEstado and R.IdFarmacia = V.IdFarmacia and R.IdPersonal = V.IdPersonal ) 
	-- Order by Keyx 
	Order by Porcentaje_Copago, TipoDeVenta  



	-- Select @IdPersonal 
	If @IdPersonal <> '' 
	   Delete From #tmpCorteParcialFinal Where IdPersonal <> '' and IdPersonal <> @IdPersonal  


	----Select 
	----	   sum(SubTotal) as SubTotal, sum(Iva) as Iva, sum(Total) as Total, 
	----		   sum(SubTotal_SinGravar) as SubTotal_SinGravar, sum(SubTotal_Gravado) as SubTotal_Gravado, 
	----		   sum(DescuentoCopago) as DescuentoCopago, 
	----		   sum(Importe_SinGravar) as Importe_SinGravar, sum(Importe_Gravado) as Importe_Gravado, 
	----		   sum(IVA_Gral) as IVA_Gral, sum(Importe_Neto) as Importe_Neto 
	----from #tmpCorteParcialFinal 

	Select IdEmpresa, Empresa, RFC, EdoCiudad, Colonia, Domicilio, CodigoPostal, 
		   IdEstado, Estado, IdFarmacia, Farmacia, IdPersonal, NombreCompleto, FechaSistema, TipoMovto, 
		   SubTotal, Iva, Total, 
		   
		   Porcentaje_Copago, 
		   SubTotal_SinGravar, SubTotal_Gravado, DescuentoCopago, Importe_SinGravar, Importe_Gravado, IVA_Gral, Importe_Neto, 		   
		   
		   Grupo, NombreGrupo, TipoDeVenta, TasaIva, Keyx, FechaImpresion 
	From #tmpCorteParcialFinal 
	Order by Porcentaje_Copago, TipoDeVenta  
		

--		spp_INT_MA__Impresion_CortesParciales 

End
Go--#SQL 


