------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_INT_ND_Productos_ValidarDatosDeEntrada' and xType = 'P') 
    Drop Proc spp_Proceso_INT_ND_Productos_ValidarDatosDeEntrada
Go--#SQL 
  
--  Exec spp_Proceso_INT_ND_Productos_ValidarDatosDeEntrada '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Proceso_INT_ND_Productos_ValidarDatosDeEntrada 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0003' 
) 
With Encryption 
As 
Begin 
Set NoCount On  
Set DateFormat YMD  

Declare 
	@FolioPedido varchar(8), @sMensaje varchar(1000),			
	@sStatus varchar(1), 
	@iLenEAN smallint, 
	@sCadena varchar(100) 
		
	Set @iLenEAN = 20  
	Set @sCadena = replicate('0', @iLenEAN) 
	
------------------------------------------- DATOS DE CATALOGO 	
	Select 
		P.IdProducto, P.CodigoEAN, P.IdClaveSSA_Sal, P.ClaveSSA,
		right(@sCadena + P.CodigoEAN, @iLenEAN) as CodigoEAN_Formato, 
		right(@sCadena + replace(P.ClaveSSA_Base, '.', ''), @iLenEAN)  as ClaveSSA_Formato
	Into #vw_Productos_CodigoEAN  
	From vw_Productos_CodigoEAN P 		
------------------------------------------- DATOS DE CATALOGO 		
	
	
------------------------------------------- VALIDAR CODIGOS EAN 	
	Update C Set CodigoEAN_Existe = 1, IdProducto = P.IdProducto, CodigoEAN = P.CodigoEAN, ClaveSSA = P.ClaveSSA  
	From INT_ND_Productos_CargaMasiva C (NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) 
		On ( right(@sCadena + C.CodigoEAN_ND, @iLenEAN) = P.CodigoEAN_Formato ) 	
------------------------------------------- VALIDAR CODIGOS EAN 	



------------------------------------------- INSERTAR PRODUCTOS DE CATALOGO 		
	----Insert Into INT_ND_Productos_CargaMasiva 
	----( 
	----	ClaveSSA_ND, Codigo, Descripcion, Proveedor, CodigoEAN_ND, ContenidoPaquete, 
	----	ClaveSSA, IdProducto, CodigoEAN, CodigoEAN_Existe, EsClaveSSA_Valida 
	----) 
	----Select 
	----	'' as ClaveSSA_ND, '' as Codigo, '' as Descripcion, '' as Proveedor, '' as CodigoEAN_ND, 0 as ContenidoPaquete, 
	----	ClaveSSA, IdProducto, CodigoEAN, 1 as CodigoEAN_Existe, 0 as EsClaveSSA_Valida 
	----From #vw_Productos_CodigoEAN P (NoLock) 
	----Where Not Exists ( Select * From INT_ND_Productos_CargaMasiva C (NoLock) Where P.CodigoEAN = C.CodigoEAN ) 

	--------Update C Set CodigoEAN_Existe = 1, IdProducto = P.IdProducto, CodigoEAN = P.CodigoEAN, ClaveSSA = P.ClaveSSA  
	--------From INT_ND_Productos_CargaMasiva C (NoLock) 
	--------Inner Join #vw_Productos_CodigoEAN P (NoLock) 
	--------	On ( right(@sCadena + C.CodigoEAN_ND, @iLenEAN) = P.CodigoEAN_Formato ) 	
------------------------------------------- INSERTAR PRODUCTOS DE CATALOGO 		



----------------------------------------------- VALIDAR EXISTAN CLAVES 	
	Update C Set EsClaveSSA_Valida = 
		(case when right(@sCadena + replace(C.ClaveSSA_ND, '.', ''), @iLenEAN) = P.ClaveSSA_Formato Then 1 Else 0 End)
	From INT_ND_Productos_CargaMasiva C (NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( C.CodigoEAN = P.CodigoEAN ) 
		-- On ( right(@sCadena + replace(C.ClaveSSA_ND, '.', ''), @iLenEAN) = P.ClaveSSA_Formato ) 		
----------------------------------------------- VALIDAR EXISTAN CLAVES 	

	
----------------------------------- Cargar relacion de Claves 	
---------------- QUITAR 2K150606.1603 
	------Delete From INT_ND_CFG_ClavesSSA Where IdEstado = @IdEstado and IdClaveSSA = '0000'
	
	------Insert Into INT_ND_CFG_ClavesSSA ( IdEstado, IdClaveSSA, ClaveSSA, ClaveSSA_ND, Status ) 
	------Select Distinct @IdEstado, P.IdClaveSSA_Sal, P.ClaveSSA, C.ClaveSSA_ND, 'A' as Status  
	------From INT_ND_Productos_CargaMasiva C (NoLock) 
	------Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( C.CodigoEAN = P.CodigoEAN ) 	
	------Where Not Exists 
	------( 
	------	Select * 
	------	From INT_ND_CFG_ClavesSSA L (NoLock) 
	------	Where L.IdEstado = @IdEstado and P.ClaveSSA = L.ClaveSSA And C.ClaveSSA_ND = L.ClaveSSA_ND 
	------) 
----------------------------------- Cargar relacion de Claves 		
	
	
------------------------------- Validar las EAN, Claves y Descripciones previamente cargadas 		
	Select ClaveSSA_ND, ClaveSSA_ND as ClaveSSA_ND_Nueva, CodigoEAN_ND, Descripcion, Descripcion as Descripcion_Nueva, 
		0 as Diferencia_Descripcion, 0 as Diferencia_ClaveSSA 
	Into #tmpCodigosEAN_Existentes  
	From INT_ND_Productos 
		
	
	Update C Set Descripcion_Nueva = '', ClaveSSA_ND_Nueva = ''  
	From #tmpCodigosEAN_Existentes C 

	Update H Set Descripcion_Nueva = C.Descripcion, 
		Diferencia_Descripcion = (case when C.Descripcion <> H.Descripcion then 1 else 0 end)
	From #tmpCodigosEAN_Existentes H 
	Inner Join INT_ND_Productos_CargaMasiva C On ( H.CodigoEAN_ND = C.CodigoEAN_ND ) 	
	
	
	Update H Set ClaveSSA_ND_Nueva = C.ClaveSSA_ND, 
		Diferencia_ClaveSSA = (case when C.ClaveSSA_ND <> H.ClaveSSA_ND then 1 else 0 end)
	From #tmpCodigosEAN_Existentes H 
	Inner Join INT_ND_Productos_CargaMasiva C On ( H.CodigoEAN_ND = C.CodigoEAN_ND ) 		
	
--		spp_Proceso_INT_ND_Productos_ValidarDatosDeEntrada			
	
	

------------------------------- Validar las CodigosEAN nuevos  
	Select ClaveSSA_ND, CodigoEAN_ND, Descripcion 
	Into #tmpCodigosEAN_Nuevos 
	From INT_ND_Productos_CargaMasiva C (NoLock) 
	Where Not Exists ( Select * From INT_ND_Productos H (NoLock) Where H.CodigoEAN_ND = C.CodigoEAN_ND ) 



------------------------------- Validar las CodigosEAN eliminados  
	Select ClaveSSA_ND, CodigoEAN_ND, Descripcion  
	Into #tmpClaves_Eliminadas 
	From INT_ND_Productos C (NoLock) 
	Where Not Exists ( Select * From INT_ND_Productos_CargaMasiva H (NoLock) Where H.CodigoEAN_ND = C.CodigoEAN_ND )	
	
	
--		spp_Proceso_INT_ND_Productos_ValidarDatosDeEntrada				
	
	
	
------------------------------------------- RESULTADOS 
	Select 
		'Clave SSA Nadro' = ClaveSSA_ND, 'Código EAN Nadro' = CodigoEAN_ND, 
		'Descripción Nadro' = Descripcion, 'Descripción Nueva' = Descripcion_Nueva    
	From #tmpCodigosEAN_Existentes (NoLock) 
	Where Diferencia_Descripcion = 1  

	Select 
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Clave SSA Nadro Nueva' = ClaveSSA_ND_Nueva, 		
		'Código EAN Nadro' = CodigoEAN_ND, 
		'Descripción Nadro' = Descripcion  
	From #tmpCodigosEAN_Existentes (NoLock) 
	Where Diferencia_ClaveSSA = 1  
	
	
	Select 	
		'Clave SSA Nadro' = ClaveSSA_ND, 'Código EAN Nadro' = CodigoEAN_ND, 
		'Descripción Nadro' = Descripcion  
	From #tmpCodigosEAN_Nuevos 
	

	Select 	
		'Clave SSA Nadro' = ClaveSSA_ND, 'Código EAN Nadro' = CodigoEAN_ND, 
		'Descripción Nadro' = Descripcion  
	From #tmpClaves_Eliminadas  
------------------------------------------- RESULTADOS 

	
	
--		spp_Proceso_INT_ND_Productos_ValidarDatosDeEntrada		
	
	
End  
Go--#SQL 