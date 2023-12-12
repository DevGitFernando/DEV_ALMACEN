------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_INT_ND_CuadrosBasicos_ValidarDatosDeEntrada' and xType = 'P') 
    Drop Proc spp_Proceso_INT_ND_CuadrosBasicos_ValidarDatosDeEntrada
Go--#SQL 
  
--  Exec spp_Proceso_INT_ND_CuadrosBasicos_ValidarDatosDeEntrada '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Proceso_INT_ND_CuadrosBasicos_ValidarDatosDeEntrada 
(   
    @IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', @Prioridades_Excluir int = 0 
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
		P.ClaveSSA, P.ClaveSSA_Base, 
		right(@sCadena + replace(P.ClaveSSA_Base, '.', ''), @iLenEAN)  as ClaveSSA_Formato
	Into #vw_ClavesSSA_Sales  
	From vw_ClavesSSA_Sales P 		
------------------------------------------- DATOS DE CATALOGO 		



------------------------------------- Limpiar las prioridades que no se utilizan 
	if @Prioridades_Excluir > 0 
	Begin 
		Delete From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva 
		Where Prioridad >= @Prioridades_Excluir 
	End 


------------------------------------------- VALIDAR EXISTAN CLAVES 	
	Update C Set 
		Contrato = ltrim(rtrim(Contrato)), 
		NombrePrograma = ltrim(rtrim(NombrePrograma)), 
		-- IdAnexo = replace(ltrim(rtrim(IdAnexo)), ' ', ''), 		
		NombreAnexo = ltrim(rtrim(NombreAnexo)), 
		Descripcion_Mascara = ltrim(rtrim(Descripcion_Mascara)) 
	From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva C (NoLock) 


	Update C Set 
		ClaveSSA = P.ClaveSSA, 
		EsClaveSSA_Valida = 1 
		-- (case when right(@sCadena + replace(C.ClaveSSA_ND, '.', ''), @iLenEAN) = P.ClaveSSA_Formato Then 1 Else 0 End)
	From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva C (NoLock) 
	Inner Join #vw_ClavesSSA_Sales P (NoLock) 
		On ( right(@sCadena + replace(C.ClaveSSA_ND, '.', ''), @iLenEAN) = P.ClaveSSA_Formato )   		
------------------------------------------- VALIDAR EXISTAN CLAVES 	



--		select top 10 * from INT_ND_CFG_CB_CuadrosBasicos 
	
------------------------------- Validar las Claves y Descripciones previamente cargadas 	
	Select IdAnexo, Prioridad, ClaveSSA_ND, Descripcion_Mascara, PrecioVenta, PrecioServicio, 
		Descripcion_Mascara as Descripcion_Mascara_Nueva, PrecioVenta as PrecioVenta_Nuevo, PrecioServicio as PrecioServicio_Nuevo, 
		0 as Diferencia_Descripcion, 
		0 as Diferencia_PrecioVenta, 
		0 as Diferencia_PrecioServicio  
	Into #tmpClaves_Existentes  
	From INT_ND_CFG_CB_CuadrosBasicos 
	
	Update C Set Descripcion_Mascara_Nueva = '', PrecioVenta_Nuevo = 0,  PrecioServicio_Nuevo = 0 
	From #tmpClaves_Existentes C 

	Update H Set Descripcion_Mascara_Nueva = C.Descripcion_Mascara, 
		PrecioVenta_Nuevo = C.PrecioVenta, 
		PrecioServicio_Nuevo = C.PrecioServicio, 
		Diferencia_Descripcion = (case when C.Descripcion_Mascara <> H.Descripcion_Mascara then 1 else 0 end), 
		Diferencia_PrecioVenta = (case when C.PrecioVenta <> H.PrecioVenta then 1 else 0 end), 		
		Diferencia_PrecioServicio = (case when C.PrecioServicio <> H.PrecioServicio then 1 else 0 end) 		
	From #tmpClaves_Existentes H 
	Inner Join INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva C 
		On ( H.IdAnexo = C.IdAnexo and H.ClaveSSA_ND = C.ClaveSSA_ND ) 



----------------------------------- Validar las Claves nuevas 
	Select IdAnexo, Prioridad, ClaveSSA_ND, Descripcion_Mascara  
	Into #tmpClaves_Nuevas_Anexo 
	From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva C (NoLock) 
	Where Not Exists ( Select * From INT_ND_CFG_CB_CuadrosBasicos H (NoLock) Where H.IdAnexo = C.IdAnexo and H.ClaveSSA_ND = C.ClaveSSA_ND ) 

	Select ClaveSSA_ND, Descripcion_Mascara  
	Into #tmpClaves_Nuevas_General 
	From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva C (NoLock) 
	Where Not Exists ( Select * From INT_ND_CFG_CB_CuadrosBasicos H (NoLock) Where H.ClaveSSA_ND = C.ClaveSSA_ND ) 


----------------------------------- Validar las Claves eliminadas  
	Select IdAnexo, Prioridad, ClaveSSA_ND, Descripcion_Mascara 
	Into #tmpClaves_Eliminadas_Anexo 
	From INT_ND_CFG_CB_CuadrosBasicos C (NoLock) 
	Where Not Exists ( Select * From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva H (NoLock) Where H.IdAnexo = C.IdAnexo and H.ClaveSSA_ND = C.ClaveSSA_ND )
	
	Select ClaveSSA_ND, Descripcion_Mascara 
	Into #tmpClaves_Eliminadas_General 
	From INT_ND_CFG_CB_CuadrosBasicos C (NoLock) 
	Where Not Exists ( Select * From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva H (NoLock) Where H.ClaveSSA_ND = C.ClaveSSA_ND )
		
		
--		spp_Proceso_INT_ND_CuadrosBasicos_ValidarDatosDeEntrada			


------------------------------------------- RESULTADOS 
	Select 
		'Id Anexo' = IdAnexo, Prioridad,  
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Descripción Nadro' = Descripcion_Mascara, 'Descripción Nueva' = Descripcion_Mascara_Nueva     
	From #tmpClaves_Existentes (NoLock) 
	Where Diferencia_Descripcion = 1  
	Order by Descripcion_Mascara 
	
	Select 
		'Id Anexo' = IdAnexo, Prioridad,  
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Descripción Nadro' = Descripcion_Mascara, 
		'Precio de venta' = PrecioVenta, 'Precio de venta nuevo' = PrecioVenta_Nuevo 
	From #tmpClaves_Existentes (NoLock) 
	Where Diferencia_PrecioVenta = 1  
	Order by Descripcion_Mascara 	
	
	
	Select 
		'Id Anexo' = IdAnexo, Prioridad,  
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Descripción Nadro' = Descripcion_Mascara, 
		'Precio de servicio' = PrecioServicio, 'Precio de servicio nuevo' = PrecioServicio_Nuevo 		
	From #tmpClaves_Existentes (NoLock) 
	Where Diferencia_PrecioServicio = 1  
	Order by Descripcion_Mascara 	
	
	
	Select 
		'Id Anexo' = IdAnexo, Prioridad,  
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Descripción Nadro' = Descripcion_Mascara   
	From #tmpClaves_Nuevas_Anexo (NoLock) 
	Order by Descripcion_Mascara	
	
	
	Select 
		'Id Anexo' = IdAnexo, Prioridad,  
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Descripción Nadro' = Descripcion_Mascara   
	From #tmpClaves_Eliminadas_Anexo (NoLock) 
	Order by Descripcion_Mascara		
	

	Select 
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Descripción Nadro' = Descripcion_Mascara   
	From #tmpClaves_Nuevas_General (NoLock) 
	Order by Descripcion_Mascara	
	
	
	Select 
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Descripción Nadro' = Descripcion_Mascara   
	From #tmpClaves_Eliminadas_General (NoLock) 
	Order by Descripcion_Mascara	
		
		
	Select 
		'Id Anexo' =  IdAnexo, 
		'Prioridad' = Prioridad, 
		'Clave SSA Nadro' = ClaveSSA_ND, 
		'Descripción Nadro' = Descripcion_Mascara 
	From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva C (NoLock)
	Where PrecioVenta = 0 or PrecioServicio = 0  		
------------------------------------------- RESULTADOS 	
	


------------------------------------------- RESULTADOS 	
	----Select Keyx as Renglon, 
	----	'Clave SSA Nadro' = ClaveSSA_ND, 'Código EAN Nadro' = CodigoEAN_ND, 
	----	'Clave SSA' = ClaveSSA, 'Código EAN' = CodigoEAN   
	----From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva (NoLock) Where EsClaveSSA_Valida = 0 And CodigoEAN_Existe = 1 
------------------------------------------- RESULTADOS 	
	
	
End  
Go--#SQL 