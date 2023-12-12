
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Inventario_A_Una_Fecha' and  xType = 'P' ) 
   Drop Proc spp_Inventario_A_Una_Fecha 
Go--#SQL 	
--		select top 1 * from MovtosInv_Enc 
--		select top 1 * from MovtosInv_Det_CodigosEAN 
	
---		Exec spp_Inventario_A_Una_Fecha 21, 182, '2011-12-15'	

	
Create Proc spp_Inventario_A_Una_Fecha 
( 
--	@IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0011', 
	@IdEstado int = 21, @IdFarmacia int = 182, 
	@FechaRevision varchar(10) = '2011-11-15' 
	-- @Año int = 2009 
	, @iEjecutar int = 1 
) 	
As 
Begin 
Set DateFormat YMD 
     

	Select DM.IdEmpresa, space(100) as Empresa, 
		DM.IdEstado, space(100) as Estado, 
		DM.IdFarmacia, space(100) as Farmacia, 
		DM.IdProducto, DM.CodigoEAN, 
		P.Descripcion, P.ClaveSSA, P.DescripcionClave, 
		max(DM.Keyx) as Keyx, 
		getdate() as FechaUltMovto, 
		0 as Existencia 
	into #tmpInventario 
	from MovtosInv_Det_CodigosEAN DM (NoLock) 
	Inner Join MovtosInv_Enc EM (NoLock) 
		On ( 
		       DM.IdEmpresa = EM.IdEmpresa and DM.IdEstado = EM.IdEstado 
		       and DM.IdFarmacia = EM.IdFarmacia and DM.FolioMovtoInv = EM.FolioMovtoInv 
     	   ) 	
    Inner Join vw_Productos_CodigoEAN P (NoLock) On ( DM.IdProducto = P.IdProducto and DM.CodigoEAN = P.CodigoEAN )  	   
	Where EM.IdEstado = @IdEstado and EM.IdFarmacia = @IdFarmacia and 
		  convert(varchar(10), EM.FechaRegistro, 120) <= @FechaRevision 
		  -- and DM.CodigoEAN = '7501563010107' 
		  -- IdProducto = '00000008' 
		  -- and 
		  -- datepart(yy, FechaSistema) = @Año  -- and datepart(mm, FechaSistema) = 12 
	Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, -- EM.FolioMovtoInv, 
			 DM.IdProducto, DM.CodigoEAN, P.Descripcion, P.ClaveSSA, P.DescripcionClave 
			 
--- Obtener datos de Existencias 			 
	Update T Set Existencia = E.Existencia, FechaUltMovto = E.FechaSistema   
	From #tmpInventario T 
	Inner Join MovtosInv_Det_CodigosEAN E On ( E.Keyx = T.Keyx )	


	Update T Set Empresa = F.Nombre 
	From #tmpInventario T 
	Inner Join CatEmpresas F (NoLock) On ( T.IdEmpresa = F.IdEmpresa ) 
	

	Update T Set Estado = F.Estado, Farmacia = F.Farmacia 
	From #tmpInventario T 
	Inner Join vw_Farmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia ) 
	

	
--- Salida Final 	
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		  IdProducto, CodigoEAN, Descripcion, ClaveSSA, DescripcionClave, 
		  -- Keyx, 
		  FechaUltMovto, Existencia as Existencia_A_La_Fecha 
	From  #tmpInventario 
	Order by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN 

					 

End 
Go--#SQL 		

