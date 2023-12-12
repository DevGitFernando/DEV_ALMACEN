
set dateformat YMD 
Go--#SQL 

--	sp_Listacolumnas MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones

	If Exists ( Select * From sysobjects (nolock) Where Name = 'tmp__REV__Datos_x_Ubicaciones' and xType = 'U' ) Drop Table  tmp__REV__Datos_x_Ubicaciones  
Go--#SQL 


	select 
		E.IdEmpresa, 
		E.IdEstado, E.IdFarmacia, E.FolioMovtoInv, E.FechaRegistro, 

		E.TipoES, E.IdTipoMovto_Inv, cast('' as varchar(100)) as Movimiento, 
		E.MovtoAplicado, 

		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro_Aux,
	
		
		U.IdSubFarmacia, U.IdProducto, U.CodigoEAN, 
		U.ClaveLote, 

		convert(varchar(10), getdate(), 120) as Caducidad, 
		--0 as CaducidadAsignada, 
		0 as Caducado_al_Movimiento, 

		U.EsConsignacion, U.IdPasillo, U.IdEstante, U.IdEntrepaño, U.Cantidad, U.Existencia, U.Status 

	Into tmp__REV__Datos_x_Ubicaciones 
	from MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones U (noLock) 
		On ( E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and E.IdFarmacia = U.IdFarmacia and E.FolioMovtoInv = U.FolioMovtoInv ) 
	where IdPasillo = 40 

Go--#SQL 

	
	Update L Set Movimiento = M.Descripcion 
	from tmp__REV__Datos_x_Ubicaciones L  
	inner join Movtos_Inv_Tipos M (NoLock) On ( L.IdTipoMovto_Inv = M.IdTipoMovto_Inv ) 

Go--#SQL 


	Update L Set 
		--CaducidadAsignada = 1, 
		Caducidad = convert(varchar(10), E.FechaCaducidad, 120) 
	from tmp__REV__Datos_x_Ubicaciones L  
	inner join FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
		On ( L.IdEmpresa = E.IdEmpresa and L.IdEstado = E.IdEstado and L.IdFarmacia = E.IdFarmacia and L.IdSubFarmacia = E.IdSubFarmacia
			and L.IdProducto = E.IdProducto and L.CodigoEAN = E.CodigoEAN and L.ClaveLote = E.ClaveLote ) 
		
Go--#SQL 		 


	Update L Set Caducado_al_Movimiento = 1 
	from tmp__REV__Datos_x_Ubicaciones L  
	Where FechaRegistro_Aux > Caducidad 



Go--#SQL


	select * 
	from tmp__REV__Datos_x_Ubicaciones 
	Order by IdFarmacia, FechaRegistro 

Go--#SQL
