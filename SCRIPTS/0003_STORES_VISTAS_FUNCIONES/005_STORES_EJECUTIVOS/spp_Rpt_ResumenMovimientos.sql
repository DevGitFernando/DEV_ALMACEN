If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_ResumenMovimientos_Farmacia' and xType = 'P' )  
   Drop Proc spp_Rpt_ResumenMovimientos_Farmacia  
Go--#SQL 

--		Exec spp_Rpt_ResumenMovimientos_Farmacia '001', '21', '0132', '2012-01-01', '2012-01-01'

Create Proc spp_Rpt_ResumenMovimientos_Farmacia  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0132', 
	@FechaInicial varchar(10) = '2011-01-01', @FechaFinal varchar(10) = '2013-01-01'  
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@sEstado varchar(100),  
	@sFarmacia varchar(100) 

------------------------------------------  
	Select @sEstado = Estado, @sFarmacia = Farmacia From vw_Farmacias E (NoLock) Where E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia 

------------------------------------------
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioMovtoInv, E.FechaRegistro, 
		 E.IdTipoMovto_Inv as IdMovto, space(100) as DescMovto, 
		 year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes  
	Into #tmpFolios 	 
	From MovtosInv_Enc E (NoLock) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia 
		  and convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
		  and E.MovtoAplicado = 'S'  
		  
------------------------------------------
	Select E.IdEmpresa, E.IdEstado, @sEstado as Estado, E.IdFarmacia, @sFarmacia as Farmacia, 
		 E.IdMovto, space(100) as DescMovto, space(2) as Efecto, 
		 year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 
		 count(distinct E.FolioMovtoInv) as Folios, 
		 -- cast(sum(D.Cantidad) as int) as Piezas, 
		 -- (case when L.ClaveLote like '%*%' then 1 else 0 end) as EsConsignacion, 
		 cast(sum(L.Cantidad) as int) as Piezas 
	Into #tmp_Resumen 	 
	From #tmpFolios E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )	
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )			
------	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia 
------		  and convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
------		  and E.MovtoAplicado = 'S' 
	Group by 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdMovto, 
		year(E.FechaRegistro), month(E.FechaRegistro) 
		-- (case when L.ClaveLote like '%*%' then 1 else 0 end) 
	 
	Update R Set DescMovto = T.Descripcion, Efecto = T.Efecto_Movto 
	From #tmp_Resumen R  
	Inner Join Movtos_Inv_Tipos T (NoLock) On ( R.IdMovto = T.IdTipoMovto_Inv )  

--		spp_Rpt_ResumenMovimientos_Farmacia  	

	If Exists ( Select Name From sysobjects (NoLock) Where name = 'Rpt_Resumen_Movtos_Unidades' and xType = 'U' )
	   Drop Table Rpt_Resumen_Movtos_Unidades 
	   
	Select * 
	Into Rpt_Resumen_Movtos_Unidades 
	From #tmp_Resumen 
	Order By IdEmpresa, IdEstado, IdFarmacia, Efecto, DescMovto, Año, Mes  


End 
Go--#SQL 

--		select top 1 * from MovtosInv_Enc 


