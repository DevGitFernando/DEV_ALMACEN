---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_PRCS_OCEN__IntegracionDeMascaras_000_Validar_Datos' and xType = 'P' ) 
   Drop Proc spp_PRCS_OCEN__IntegracionDeMascaras_000_Validar_Datos 
Go--#SQL 

Create Proc spp_PRCS_OCEN__IntegracionDeMascaras_000_Validar_Datos  
( 
	@IdEstado varchar(2) = '9', @IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '12' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEstado = right('00' +  @IdEstado, 2) 
	Set @IdCliente = right('000000' +  @IdCliente, 4) 
	Set @IdSubCliente = right('00000' +  @IdSubCliente, 4) 		
	
	Exec sp_FormatearTabla CFG_ClavesSSA_Mascaras__CargaMasiva 
			
----------------------------------- FORMATEAR VALORES 
	Update P Set IdClaveSSA_Sal = C.IdClaveSSA_Sal  
	From CFG_ClavesSSA_Mascaras__CargaMasiva P 	
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.ClaveSSA = C.ClaveSSA ) 

	Update C Set 
		IdEstado = right('00' +  @IdEstado, 2), 
		IdCliente = right('0000' +  @IdCliente, 4), IdSubCliente = right('0000' +  @IdSubCliente, 4), 
		IdClaveSSA_Sal = right('0000' +  IdClaveSSA_Sal, 4)  
	From CFG_ClavesSSA_Mascaras__CargaMasiva C (NoLock) 


	Update P Set ClaveSSA_Proceso = C.ClaveSSA, DescripcionClaveSSA = C.Descripcion  
	From CFG_ClavesSSA_Mascaras__CargaMasiva P 	
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) 
----------------------------------- FORMATEAR VALORES 				




----------------------------------- CLAVES NO EXISTENTES  
	Select 
		IdEstado, IdCliente, IdSubCliente, ClaveSSA, Mascara, Descripcion, Presentacion 
	Into #tmpClavesNoExistentes 
	From CFG_ClavesSSA_Mascaras__CargaMasiva P (NoLock)  
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
		IdEstado, IdCliente, IdSubCliente 
	Into #tmpClientesNoExistentes 
	From CFG_ClavesSSA_Mascaras__CargaMasiva P (NoLock)  
	Where (IdCliente + IdSubCliente) <> (@IdCliente + @IdSubCliente) 
----------------------------------- CLIENTES - SUBCLIENTES NO EXISTENTES  


----------------------------------- CLAVES REPETIDAS 
	Select ClaveSSA, Mascara, Descripcion, Presentacion  
	Into #tmpDuplicadas  
	From CFG_ClavesSSA_Mascaras__CargaMasiva 
	Where ClaveSSA in 
	(	Select ClaveSSA 
		From CFG_ClavesSSA_Mascaras__CargaMasiva P (NoLock)  
		Group By ClaveSSA 
		Having count(*) >= 2 
	) 
	Order by Mascara 
----------------------------------- CLAVES REPETIDAS 


----------------------------------- DESCRIPCIONES EXCEDEN LONGITUD 
	Select Mascara, Descripcion, DescripcionCorta, Presentacion  
	Into #tmpDescripcionesExcedentes   
	From CFG_ClavesSSA_Mascaras__CargaMasiva 
	Where len(Mascara) > 50 or len(Descripcion) > 5000 or len(DescripcionCorta) > 200 or len(Presentacion) > 100 

----------------------------------- DESCRIPCIONES EXCEDEN LONGITUD 




----------------------- SALIDA FINAL	
	Select top 0 identity(int, 2, 1) as Orden, cast('' as varchar(200)) as NombreTabla Into #tmpResultados 

	Insert Into #tmpResultados ( NombreTabla ) select 'Claves repetidas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Claves no registradas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Claves no asignada a cliente' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Descripciones excendentes con longitud excedente' 

	Select * From #tmpResultados 
	Select * From #tmpDuplicadas 
	Select * From #tmpClavesNoExistentes
	Select * From #tmpClientesNoExistentes 
	Select * From #tmpDescripcionesExcedentes 

----------------------- SALIDA FINAL	


---		spp_PRCS_OCEN__IntegracionDeMascaras_000_Validar_Datos


End 
Go--#SQL 



