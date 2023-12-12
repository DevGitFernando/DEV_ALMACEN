
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Inventario_A_Una_Fecha' and  xType = 'P' ) 
   Drop Proc spp_Inventario_A_Una_Fecha 
Go--#SQL 	
--		select top 1 * from MovtosInv_Enc 
--		select top 1 * from MovtosInv_Det_CodigosEAN 
	
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

/*
	select DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, DM.IdProducto, DM.CodigoEAN,
			(
				Select top 1 FolioMovtoInv 
				From MovtosInv_Det_CodigosEAN F (NoLock) 
				Where F.IdEmpresa = DM.IdEmpresa and F.IdEstado = DM.IdEstado and F.IdFarmacia = DM.IdFarmacia 
					  and F.IdProducto = DM.IdProducto and F.CodigoEAN = DM.CodigoEAN -- and datepart(yy, F.FechaSistema) = D.Año 
				Order by Keyx desc 
			) as Folio, 
			0 as Existencia   	  
	-- into #tmpDatos 
	from MovtosInv_Det_CodigosEAN DM (NoLock) 
	Inner Join MovtosInv_Enc EM (NoLock) 
		On ( 
		       DM.IdEmpresa = EM.IdEmpresa and DM.IdEstado = EM.IdEstado 
		       and DM.IdFarmacia = EM.IdFarmacia and DM.FolioMovtoInv = EM.FolioMovtoInv 
     	   ) 	
	Where EM.IdEstado = @IdEstado and EM.IdFarmacia = @IdFarmacia and 
	      --DM.IdProducto = '00005874' and 
		  convert(varchar(10), EM.FechaRegistro, 120) <= @FechaRevision 
		  -- IdProducto = '00000008' 
		  -- and 
		  -- datepart(yy, FechaSistema) = @Año  -- and datepart(mm, FechaSistema) = 12 
	Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, DM.IdProducto, DM.CodigoEAN 
*/     


	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN, M.FechaSistema as FechaUltMovto, 
		M.Existencia, 
		M.Costo, 
		cast(0 as numeric(14,6)) as Importe  
	    -- 0 as Existencia, 0 as ExistenciaEAN, 0 as ExistenciaLotes 
	Into #tmpInventario    
	From 
	( 	
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN, -- TasaIva, 	
			(
				Select top 1 FolioMovtoInv 
				From MovtosInv_Det_CodigosEAN F (NoLock) 
				Where F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia 
					  and F.IdProducto = D.IdProducto and F.CodigoEAN = D.CodigoEAN -- and datepart(yy, F.FechaSistema) = D.Año 
				Order by Keyx desc 
			) as Folio  
		From 
		( 
			select DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, DM.IdProducto, DM.CodigoEAN  
			-- into #tmpDatos 
			from MovtosInv_Det_CodigosEAN DM (NoLock) 
			Inner Join MovtosInv_Enc EM (NoLock) 
				On ( 
				       DM.IdEmpresa = EM.IdEmpresa and DM.IdEstado = EM.IdEstado 
				       and DM.IdFarmacia = EM.IdFarmacia and DM.FolioMovtoInv = EM.FolioMovtoInv 
		     	   ) 	
			Where EM.IdEstado = @IdEstado and EM.IdFarmacia = @IdFarmacia and 
				  convert(varchar(10), EM.FechaRegistro, 120) <= @FechaRevision 
				  -- IdProducto = '00000008' 
				  -- and 
				  -- datepart(yy, FechaSistema) = @Año  -- and datepart(mm, FechaSistema) = 12 
			Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, DM.IdProducto, DM.CodigoEAN 
		) as D 	
	) as X 
	Inner Join MovtosInv_Det_CodigosEAN M (NoLock) 
		On ( X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia and X.Folio = M.FolioMovtoInv 
		     and X.IdProducto = M.IdProducto and X.CodigoEAN = M.CodigoEAN )  
	-- Where M.IdProducto =  '00005874' 	     
	order by X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN 
	
	
---------------------------- 
--	Select * 
    Update I Set Costo = F.UltimoCosto 
	From #tmpInventario I (NoLock) 
	Inner Join FarmaciaProductos F (NoLock) 
		On ( I.IdEmpresa = F.IdEmpresa and I.IdEstado = F.IdEstado and I.IdFarmacia = F.IdFarmacia 
			   and I.IdProducto = F.IdProducto ) 
	Where I.Costo = 0 	

	Update I Set Importe = ( Costo * Existencia )
	From #tmpInventario I (NoLock) 


-- X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN 

/* 	
	update X Set Existencia = M.Existencia 
	From #tmpInventario X 
	Inner Join FarmaciaProductos M (NoLock) 
	      On ( X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia 
		     and X.IdProducto = M.IdProducto ) -- and X.CodigoEAN = M.CodigoEAN	
	
	update X Set ExistenciaEAN = IsNull(	( Select sum(Existencia) 
			From FarmaciaProductos_CodigoEAN M (NoLock) 
				  Where X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia 
					 and X.IdProducto = M.IdProducto and X.CodigoEAN = M.CodigoEAN		 ), 0 )
	From #tmpInventario X 


	update X Set ExistenciaLotes = 	IsNull( ( Select sum(Existencia) 
			From FarmaciaProductos_CodigoEAN_Lotes M (NoLock) 
				  Where X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia 
					 and X.IdProducto = M.IdProducto and X.CodigoEAN = M.CodigoEAN	), 0 ) 	 
	From #tmpInventario X 
	
	
---  0 as Existencia, 0 as ExistenciaEAN, 0 as ExistenciaLotes 
*/ 


----- 	
    If @iEjecutar = 1 
	   Select * from #tmpInventario I 


	-- Where Costo = 0 
		-- Kardex <> Existencia 
		--	Existencia <> ExistenciaLotes 
	
End 
Go--#SQL 		

--    spp_Inventario_A_Una_Fecha 

-- Select top 1 * 				From MovtosInv_Det_CodigosEAN F (NoLock) 
				