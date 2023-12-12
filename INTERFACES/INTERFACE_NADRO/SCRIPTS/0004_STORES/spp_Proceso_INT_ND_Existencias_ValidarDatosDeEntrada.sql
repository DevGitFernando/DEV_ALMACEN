------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_INT_ND_Existencias_ValidarDatosDeEntrada' and xType = 'P') 
    Drop Proc spp_Proceso_INT_ND_Existencias_ValidarDatosDeEntrada
Go--#SQL 
  
--  Exec spp_Proceso_INT_ND_Existencias_ValidarDatosDeEntrada '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Proceso_INT_ND_Existencias_ValidarDatosDeEntrada 
(   
    @IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003' 
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
		P.IdProducto, P.CodigoEAN, P.ClaveSSA, P.ClaveSSA_Base, 
		right(@sCadena + P.CodigoEAN, @iLenEAN) as CodigoEAN_Formato, 
		right(@sCadena + replace(P.ClaveSSA_Base, '.', ''), @iLenEAN)  as ClaveSSA_Formato
	Into #vw_Productos_CodigoEAN  
	From vw_Productos_CodigoEAN P 		
------------------------------------------- DATOS DE CATALOGO 		
	
	
------------------------------------------- VALIDAR CODIGOS EAN 	
	Update C Set CodigoEAN_Existe = 1, IdProducto = P.IdProducto, CodigoEAN = P.CodigoEAN, ClaveSSA = P.ClaveSSA   
	From INT_ND_Existencias_CargaMasiva C (NoLock)  
	Inner Join #vw_Productos_CodigoEAN P (NoLock) 
		On ( right(@sCadena + C.CodigoEAN_ND, @iLenEAN) = P.CodigoEAN_Formato ) 	
------------------------------------------- VALIDAR CODIGOS EAN 	


----------------------------------------------- VALIDAR EXISTAN CLAVES EN CONFIGURACION  	
----	Update C Set EsClaveSSA_Valida = 
----		(case when right(@sCadena + replace(C.ClaveSSA_ND, '.', ''), @iLenEAN) = P.ClaveSSA_Formato Then 1 Else 0 End)
----	From INT_ND_Existencias_CargaMasiva C (NoLock) 
----	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( C.CodigoEAN = P.CodigoEAN ) 
----		-- On ( right(@sCadena + replace(C.ClaveSSA_ND, '.', ''), @iLenEAN) = P.ClaveSSA_Formato ) 		
----	where EsClaveSSA_Valida = 0 and CodigoEAN_Existe = 1 
----------------------------------------------- VALIDAR EXISTAN CLAVES EN CONFIGURACION  	


------------------------------------------- VALIDAR EXISTAN CLAVES 	
	Update C Set EsClaveSSA_Valida = 
		(case when right(@sCadena + replace(C.ClaveSSA_ND, '.', ''), @iLenEAN) = P.ClaveSSA_Formato Then 1 Else 0 End)
	From INT_ND_Existencias_CargaMasiva C (NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( C.CodigoEAN = P.CodigoEAN ) 
		-- On ( right(@sCadena + replace(C.ClaveSSA_ND, '.', ''), @iLenEAN) = P.ClaveSSA_Formato ) 		
	where EsClaveSSA_Valida = 0 
------------------------------------------- VALIDAR EXISTAN CLAVES 	

	
	
------------------------------------------- RESULTADOS 
	Select 
		Keyx as Renglon, 
		'Clave SSA Nadro' = ClaveSSA_ND, 'Código EAN Nadro' = CodigoEAN_ND 
	From INT_ND_Existencias_CargaMasiva (NoLock) Where CodigoEAN_Existe = 0  
	
	Select 
		Keyx as Renglon, 
		'Clave SSA Nadro' = ClaveSSA_ND, 'Código EAN Nadro' = CodigoEAN_ND, 
		'Clave SSA' = ClaveSSA, 'Código EAN' = CodigoEAN   
	From INT_ND_Existencias_CargaMasiva (NoLock) 
	Where EsClaveSSA_Valida = 0 And CodigoEAN_Existe = 1 
		  -- and 1 = 0 
------------------------------------------- RESULTADOS 

	
	----Select * 
	----From INT_ND_Existencias_CargaMasiva C 
	----Inner Join #vw_Productos_CodigoEAN P On ( C.CodigoEAN = P.CodigoEAN ) 
	
--		spp_Proceso_INT_ND_Existencias_ValidarDatosDeEntrada		
	
	
End  
Go--#SQL 