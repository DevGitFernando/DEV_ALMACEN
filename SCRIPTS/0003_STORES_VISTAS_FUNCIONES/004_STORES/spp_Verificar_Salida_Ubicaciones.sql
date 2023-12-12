
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Verificar_Salida_Ubicaciones' and xType = 'P' ) 
   Drop Proc spp_Verificar_Salida_Ubicaciones 
Go--#SQL     

Create Proc spp_Verificar_Salida_Ubicaciones 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0187', 
	@TablaBase varchar(100) = 'tmpLotes' -- 'tmpLotes_0000EA' ) 
)
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sSql varchar(1000) 

	Set @sSql = '' 

--- Tabla Base del Proceso 
	Select Top 0 IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, 
	     0 as C_Gral, 0 as C_EAN, cast(Existencia as int) as Cantidad,  
	     0 as ExistenciaGral, 0 as ExistenciaEAN, 0 as ExistenciaLotes, 0 as ExistenciaUbicacion, 
	     0 as Error    	     
	Into #tmpListaProductos 
	--Into tmpLotes_0000EA
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 

--- Insertar Datos 
	Set @sSql = 'Insert Into #tmpListaProductos ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Cantidad, C_Gral, C_EAN, ExistenciaGral, ExistenciaEAN, ExistenciaLotes, ExistenciaUbicacion, Error  ) ' + 
		' Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Cantidad, 0, 0, 0, 0, 0, 0, 0  ' +  
		' From ' +  @TablaBase 
	Exec(@sSql) 
	-- Print @sSql 

--- Quitar tabla intermedia 	
	Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + @TablaBase + char(39) + ' and xType = ' + char(39) + 'U' + char(39) + ' ) Drop Table ' +  @TablaBase 
	--Exec(@sSql) 
	-- Print @sSql 
	
	
---------------------------- Agregar Existencias Actuales 
/* 
    select F.* 
	From #tmpListaProductos V 
	Inner Join FarmaciaProductos F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.IdProducto = F.IdProducto ) 	

    select F.*
	From #tmpListaProductos V 
	Inner Join FarmaciaProductos_CodigoEAN F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado 
		    and V.IdFarmacia = F.IdFarmacia and V.IdProducto = F.IdProducto and V.CodigoEAN = F.CodigoEAN ) 		


    select F.*
	From #tmpListaProductos V 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado 
		     and V.IdFarmacia = F.IdFarmacia and V.IdSubFarmacia = F.IdSubFarmacia 
			 and V.IdProducto = F.IdProducto and V.CodigoEAN = F.CodigoEAN and V.ClaveLote = F.ClaveLote ) 				
*/ 	
	


	Update V Set ExistenciaGral = ( F.Existencia - F.ExistenciaEnTransito )  -- , ExistenciaEAN, ExistenciaLote
	From #tmpListaProductos V 
	Inner Join FarmaciaProductos F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.IdProducto = F.IdProducto ) 	
		
	Update V Set ExistenciaEAN = ( F.Existencia - F.ExistenciaEnTransito )  -- , ExistenciaEAN, ExistenciaLote
	From #tmpListaProductos V 
	Inner Join FarmaciaProductos_CodigoEAN F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado 
		    and V.IdFarmacia = F.IdFarmacia and V.IdProducto = F.IdProducto and V.CodigoEAN = F.CodigoEAN ) 		
	
	Update V Set ExistenciaLotes = ( F.Existencia - F.ExistenciaEnTransito )  -- , ExistenciaEAN, ExistenciaLote
	From #tmpListaProductos V 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado 
		     and V.IdFarmacia = F.IdFarmacia and V.IdSubFarmacia = F.IdSubFarmacia 
			 and V.IdProducto = F.IdProducto and V.CodigoEAN = F.CodigoEAN and V.ClaveLote = F.ClaveLote ) 

	Update V Set ExistenciaUbicacion = ( F.Existencia - F.ExistenciaEnTransito )  -- , ExistenciaEAN, ExistenciaLote
	From #tmpListaProductos V 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado 
		     and V.IdFarmacia = F.IdFarmacia and V.IdSubFarmacia = F.IdSubFarmacia 
			 and V.IdProducto = F.IdProducto and V.CodigoEAN = F.CodigoEAN and V.ClaveLote = F.ClaveLote 
			 And V.IdPasillo = F.IdPasillo And V.IdEstante = F.IdEstante And V.IdEntrepaño = F.IdEntrepaño )

---------------------------- Agregar Existencias Actuales 
	
---------------------------- Cantidades requeridas 	
	Update V Set C_Gral = IsNull((select sum(Cantidad) From #tmpListaProductos x Where X.IdProducto = V.IdProducto), 0) 
		--, C_EAN
	From #tmpListaProductos V 
	
	Update V Set C_EAN = IsNull((select sum(Cantidad) From #tmpListaProductos x Where X.IdProducto = V.IdProducto and x.CodigoEAN = V.CodigoEAN), 0) 
		--, C_EAN
	From #tmpListaProductos V 	
---------------------------- Cantidades requeridas 	
	

---------------------------- Verificar Cantidades 
	Update V Set Error = 1 From #tmpListaProductos V Where C_Gral > ExistenciaGral 
	Update V Set Error = 2 From #tmpListaProductos V Where C_EAN > ExistenciaEAN and Error = 0 	
	Update V Set Error = 3 From #tmpListaProductos V Where Cantidad > ExistenciaLotes and Error = 0 
	Update V Set Error = 4 From #tmpListaProductos V Where Cantidad > ExistenciaUbicacion and Error = 0 
---------------------------- Verificar Cantidades 	
	
	Select IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(T.IdEstado, T.IdFarmacia, T.IdSubFarmacia)  as SubFarmacia, 
	    T.CodigoEAN, P.Descripcion, T.ClaveLote, IdPasillo, IdEstante, IdEntrepaño, 
		ExistenciaGral, ExistenciaEAN, ExistenciaLotes, ExistenciaUbicacion, 
		Cantidad as CantidadRequerida, T.IdProducto 
	From #tmpListaProductos T 
	Inner Join vw_Productos_CodigoEAN P (NoLock) on ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN ) 
	Where Error <> 0  

---- spp_Verificar_Salida_Ubicaciones 

End 
Go--#SQL 

