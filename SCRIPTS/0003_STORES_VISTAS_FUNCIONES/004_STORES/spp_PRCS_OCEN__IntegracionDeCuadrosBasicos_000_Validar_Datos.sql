---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_000_Validar_Datos' and xType = 'P' ) 
   Drop Proc spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_000_Validar_Datos 
Go--#SQL 
--			spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_000_Validar_Datos
Create Proc spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_000_Validar_Datos  
( 
	@IdEstado varchar(2) = '28', @IdCliente varchar(4) = '0002' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

		
----------------------------------- FORMATEAR VALORES 
	Update P Set IdClaveSSA_Sal = C.IdClaveSSA_Sal 
	From CFG_CB_CuadroBasico_Claves__CargaMasiva P 	
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.ClaveSSA = C.ClaveSSA ) 	


	Update C Set 
		IdEstado = right('00' +  @IdEstado, 2), 
		IdCliente = right('0000' +  @IdCliente, 4), 
		IdClaveSSA_Sal = right('0000' +  IdClaveSSA_Sal, 4)  
		-- Status = 'A', 
		-- Actualizado = 0    
	From CFG_CB_CuadroBasico_Claves__CargaMasiva C (NoLock) 
	


	Update P Set ClaveSSA_Proceso = C.ClaveSSA, DescripcionClaveSSA = C.Descripcion  
	From CFG_CB_CuadroBasico_Claves__CargaMasiva P 	
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) 
	
	
----------------------------------- FORMATEAR VALORES 				


----------------------------------- CLAVES NO EXISTENTES  
	Select IdEstado, IdCliente, IdNivel, IdClaveSSA_Sal, ClaveSSA, DescripcionClaveSSA  
	Into #tmpClavesNoExistentes 
	From CFG_CB_CuadroBasico_Claves__CargaMasiva P (NoLock)  
	Where IdClaveSSA_Sal <> '0000' and 
		Not Exists 
		( 
			Select * 
			From CatClavesSSA_Sales C (NoLock) 
			Where P.IdClaveSSA_Sal = C.IdClaveSSA_Sal 
		) 
----------------------------------- CLAVES NO EXISTENTES  


----------------------------------- NIVELES NO EXISTENTES  
	Select IdEstado, IdCliente, IdNivel, IdClaveSSA_Sal, ClaveSSA, DescripcionClaveSSA  
	Into #tmpNivelesNoExistentes 
	From CFG_CB_CuadroBasico_Claves__CargaMasiva P (NoLock)  
	Where IdClaveSSA_Sal <> '0000' and 
		Not Exists 
		( 
			Select * 
			From CFG_CB_NivelesAtencion C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdCliente = C.IdCliente and P.IdNivel = C.IdNivel  
		) 
----------------------------------- NIVELES NO EXISTENTES   



----------------------- SALIDA FINAL	
	Select top 0 identity(int, 2, 1) as Orden, cast('' as varchar(200)) as NombreTabla Into #tmpResultados 

	Insert Into #tmpResultados ( NombreTabla ) select 'Claves no registradas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Niveles no existentes' 


	Select * From #tmpClavesNoExistentes 
	Select * From #tmpNivelesNoExistentes 

----------------------- SALIDA FINAL	


---		spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_000_Validar_Datos


End 
Go--#SQL 



