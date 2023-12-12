---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_PRCS_OCEN__IntegracionDePrecios_000_Validar_Datos' and xType = 'P' ) 
   Drop Proc spp_PRCS_OCEN__IntegracionDePrecios_000_Validar_Datos 
Go--#SQL 

Create Proc spp_PRCS_OCEN__IntegracionDePrecios_000_Validar_Datos  
( 
	@IdEstado varchar(2) = '28', @IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '11' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEstado = right('00' +  @IdEstado, 2) 
	Set @IdCliente = right('000000' +  @IdCliente, 4) 
	Set @IdSubCliente = right('00000' +  @IdSubCliente, 4) 		
	
	Exec sp_FormatearTabla CFG_ClavesSSA_Precios__CargaMasiva 
			
----------------------------------- FORMATEAR VALORES 
	Update P Set IdClaveSSA_Sal = C.IdClaveSSA_Sal  
	From CFG_ClavesSSA_Precios__CargaMasiva P 	
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.ClaveSSA = C.ClaveSSA ) 

	Update P Set ContenidoPaquete_Licitado = C.ContenidoPaquete 
	From CFG_ClavesSSA_Precios__CargaMasiva P 	
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.ClaveSSA = C.ClaveSSA ) 
	Where ContenidoPaquete_Licitado = 0  -- or ContenidoPaquete_Licitado is null 
	
	Update P Set IdPresentacion_Licitado = C.IdPresentacion 
	From CFG_ClavesSSA_Precios__CargaMasiva P 	
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.ClaveSSA = C.ClaveSSA ) 
	Where ltrim(rtrim(IdPresentacion_Licitado)) = '' or ltrim(rtrim(IdPresentacion_Licitado)) = '0'

	Update P Set SAT_ClaveDeProducto_Servicio = '01010101'
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock) 
	Where SAT_ClaveDeProducto_Servicio = '' 

	Update P Set SAT_UnidadDeMedida = 'H87'
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock) 
	Where SAT_UnidadDeMedida = '' 


	Update C Set 
		IdEstado = right('00' +  @IdEstado, 2), 
		IdCliente = right('0000' +  @IdCliente, 4), IdSubCliente = right('0000' +  @IdSubCliente, 4), 
		IdClaveSSA_Sal = right('0000' +  IdClaveSSA_Sal, 4), 
		SAT_ClaveDeProducto_Servicio = ltrim(rtrim( replace( replace( replace(SAT_ClaveDeProducto_Servicio, char(13), ''), char(10), ''), char(160), '') )), 
		IdPresentacion_Licitado = right('00000' + ltrim(ltrim(IdPresentacion_Licitado)), 3), 
		Actualizado = 0    
	From CFG_ClavesSSA_Precios__CargaMasiva C (NoLock) 


	Update P Set ClaveSSA_Proceso = C.ClaveSSA, DescripcionClaveSSA = C.Descripcion  
	From CFG_ClavesSSA_Precios__CargaMasiva P 	
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) 
----------------------------------- FORMATEAR VALORES 				


----------------------------------- CLAVES NO EXISTENTES  
	Select 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpClavesNoExistentes 
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where --IdClaveSSA_Sal <> '0000' and 
		Not Exists 
		( 
			Select * 
			From CatClavesSSA_Sales C (NoLock) 
			-- Where P.IdClaveSSA_Sal = C.IdClaveSSA_Sal 
			Where P.ClaveSSA = C.ClaveSSA 
		) 
----------------------------------- CLAVES NO EXISTENTES  


----------------------------------- CLIENTES - SUBCLIENTES NO EXISTENTES  
	Select 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpClientesNoExistentes 
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where (IdCliente + IdSubCliente) <> (@IdCliente + @IdSubCliente) 
----------------------------------- CLIENTES - SUBCLIENTES NO EXISTENTES  


----------------------------------- CLAVES REPETIDAS 
	Select ClaveSSA, DescripcionClaveSSA, Precio 
	Into #tmpDuplicadas  
	From CFG_ClavesSSA_Precios__CargaMasiva 
	Where ClaveSSA in 
	(	Select ClaveSSA 
		From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
		Group By ClaveSSA 
		Having count(*) >= 2 
	) 
	Order by ClaveSSA 
----------------------------------- CLAVES REPETIDAS 


----------------------------------- PRECIOS EN CERO 
	Select 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpPreciosEnCero 
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where Precio <= 0 and Status = 'A' 
----------------------------------- PRECIOS EN CERO 



----------------------------------- PRECIOS EN CERO 
	Select 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpFactorEnCero 
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where Factor <= 0 
----------------------------------- PRECIOS EN CERO

----------------------------------- Presentacion No Existe
	Select IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpPresentacionNoExiste
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where IdClaveSSA_Sal <> '0000' and 
		Not Exists 
		( 
			Select * 
			From CatPresentaciones C (NoLock) 
			Where P.IdPresentacion_Licitado = C.IdPresentacion 
		) 
----------------------------------- Presentacion No Existe


----------------------------------- Contenido Paquete Licitado EN CERO 
	Select 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpContenidoPaquete_LicitadoEnCero 
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where ContenidoPaquete_Licitado <= 0 
----------------------------------- Contenido Paquete Licitado EN CERO


----------------------------------- Clave Dispensacion en Cajas Completas Valido
	Select 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpDispensacion_CajasCompletas_Valido
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where Dispensacion_CajasCompletas not in (0, 1) 
----------------------------------- Clave Dispensacion en Cajas Completas Valido


----------------------------------- Presentacion No Existe
	Select 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpFACT_CFD_UnidadesDeMedida
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where IdClaveSSA_Sal <> '0000' and 
		Not Exists 
		( 
			Select * 
			From FACT_CFD_UnidadesDeMedida C (NoLock) 
			Where P.SAT_UnidadDeMedida = C.IdUnidad 
		) 
----------------------------------- Presentacion No Existe


----------------------------------- Presentacion No Existe
	Select 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Factor, ContenidoPaquete_Licitado, 
		IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	Into #tmpFACT_CFDI_Productos_Servicios
	From CFG_ClavesSSA_Precios__CargaMasiva P (NoLock)  
	Where IdClaveSSA_Sal <> '0000' and 
		Not Exists 
		( 
			Select * 
			From FACT_CFDI_Productos_Servicios C (NoLock) 
			Where P.SAT_ClaveDeProducto_Servicio = C.Clave 
		) 
----------------------------------- Presentacion No Existe


----------------------- SALIDA FINAL	
	Select top 0 identity(int, 2, 1) as Orden, cast('' as varchar(200)) as NombreTabla Into #tmpResultados 

	Insert Into #tmpResultados ( NombreTabla ) select 'Claves repetidas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Claves no registradas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Claves no asignada a cliente' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Claves con precio CERO'
	Insert Into #tmpResultados ( NombreTabla ) select 'Factor En CERO' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Presentación no registradas'
	Insert Into #tmpResultados ( NombreTabla ) select 'Contenido Paquete Licitado En CERO'
	Insert Into #tmpResultados ( NombreTabla ) select 'Clave Dispensacion en Cajas Completas Valido'
	Insert Into #tmpResultados ( NombreTabla ) select 'Unidades De Medida no registradas'
	Insert Into #tmpResultados ( NombreTabla ) select 'Productos ó Servicios no registrado' 

	Select * From #tmpResultados 
	Select * From #tmpDuplicadas 
	Select * From #tmpClavesNoExistentes
	Select * From #tmpClientesNoExistentes
	Select * From #tmpPreciosEnCero
	Select * From #tmpFactorEnCero
	Select * From #tmpPresentacionNoExiste
	Select * From #tmpContenidoPaquete_LicitadoEnCero
	Select * From #tmpDispensacion_CajasCompletas_Valido
	Select * From #tmpFACT_CFD_UnidadesDeMedida
	Select * From #tmpFACT_CFDI_Productos_Servicios

----------------------- SALIDA FINAL	


---		spp_PRCS_OCEN__IntegracionDePrecios_000_Validar_Datos


End 
Go--#SQL 



