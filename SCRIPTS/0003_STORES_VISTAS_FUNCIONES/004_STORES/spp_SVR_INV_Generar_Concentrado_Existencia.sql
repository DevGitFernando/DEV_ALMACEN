If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_SVR_INV_Generar_Existencia_Concentrado' and xType = 'P' ) 
   Drop Proc spp_SVR_INV_Generar_Existencia_Concentrado 
Go--#SQL 

Create Proc spp_SVR_INV_Generar_Existencia_Concentrado 
As 
Begin 

	Select 
		getdate() as FechaGeneracion, 1 as Activo, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
		IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, 
		IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
		IdLaboratorio, Laboratorio, TasaIva, ClaveLote, 0 as ExistenciaAux, 0 as ExistenciaEnTransito, cast(Existencia as int) as Existencia, 
		FechaCaducidad, MesesParaCaducar, FechaRegistro  
	Into #tmpExistencia 	
	From vw_ExistenciaPorCodigoEAN_Lotes 
	Where IdEmpresa <> '' 
	
	
	If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'SVR_INV_Generar_Existencia_Concentrado' and xType = 'U' )
	Begin
		Select 		
			FechaGeneracion, Activo, 
			IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
			IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, 
			IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
			IdLaboratorio, Laboratorio, TasaIva, ExistenciaAux, ExistenciaEnTransito, Existencia  
		Into SVR_INV_Generar_Existencia_Concentrado 
		From #tmpExistencia   
		Where 1 = 0 
	End 
	
	If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'SVR_INV_Generar_Existencia_Detallado' and xType = 'U' )
	Begin
		Select 		
			FechaGeneracion, Activo, 
			IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
			IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, 
			IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
			IdLaboratorio, Laboratorio, TasaIva, ClaveLote, ExistenciaAux, ExistenciaEnTransito, Existencia, 
			FechaCaducidad, MesesParaCaducar, FechaRegistro  
		Into SVR_INV_Generar_Existencia_Detallado 
		From #tmpExistencia   
		Where 1 = 0 
	End	
	
	
--	if (select count(*) From #tmpExistencia ) > 0 
--	Begin 
	Delete From SVR_INV_Generar_Existencia_Detallado 
	Update SVR_INV_Generar_Existencia_Detallado Set Activo = 0 Where Activo <> 0 
	Insert Into SVR_INV_Generar_Existencia_Detallado 
	( 
		FechaGeneracion, Activo, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
		IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, 
		IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
		IdLaboratorio, Laboratorio, TasaIva, ClaveLote, ExistenciaAux, ExistenciaEnTransito, Existencia, 
		FechaCaducidad, MesesParaCaducar, FechaRegistro 	
	) 	
	Select 
		FechaGeneracion, Activo, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
		IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, 
		IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
		IdLaboratorio, Laboratorio, TasaIva, ClaveLote, ExistenciaAux, ExistenciaEnTransito, Existencia, 
		FechaCaducidad, MesesParaCaducar, FechaRegistro 	
	From #tmpExistencia 
--	End  


------------------------------------- Generar Concentrado 		
	Delete From SVR_INV_Generar_Existencia_Concentrado 		
	Insert Into SVR_INV_Generar_Existencia_Concentrado  
	( 
		FechaGeneracion, Activo, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
		IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, 
		IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
		IdLaboratorio, Laboratorio, TasaIva, ExistenciaAux, ExistenciaEnTransito, Existencia  
	) 	
	Select 
		getdate() as FechaGeneracion, 1 as Activo, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
		IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, 
		IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
		IdLaboratorio, Laboratorio, TasaIva, sum(ExistenciaAux), sum(ExistenciaEnTransito), sum(Existencia) 
	From SVR_INV_Generar_Existencia_Detallado 		
	Group by  
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, 
		IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, 
		IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ContenidoPaquete, 
		IdLaboratorio, Laboratorio, TasaIva			
		
End 
Go--#SQL 	
	
	
	
	
--	sp_listacolumnas vw_ExistenciaPorCodigoEAN_Lotes 
	