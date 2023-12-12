If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_001' and xType = 'P' ) 
   Drop Proc spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_001 
Go--#SQL 

Create Proc spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_001  
( 
	@IdEstado varchar(2) = '09', @IdCliente varchar(4) = '0023', @IdSubCliente varchar(4) = '0001' 
) 
With Encryption 
As 
Begin 
--Set NoCount On 
Set DateFormat YMD 

Declare @Actualizado smallint 
	Set @Actualizado = 0 

	Set @IdEstado = right('00' +  @IdEstado, 2) 
	Set @IdCliente = right('000000' +  @IdCliente, 4) 
	

----------------- Preparar tablas  
	Select Distinct IdEstado, IdCliente, @IdSubCliente as IdSubCliente, IdNivel, IdClaveSSA_Sal, ClaveSSA, EmiteVale, Status, 0 as Actualizado 
	Into #tmpClaves_Cliente 
	From CFG_CB_CuadroBasico_Claves__CargaMasiva 
----------------- Preparar tablas  
	
--	select * From CFG_CB_CuadroBasico_Claves__CargaMasiva 
	

-------------------------------------------------- Actualizar informacion  
	Update C Set Status = 'C' From CFG_CB_CuadroBasico_Claves C (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente 
	Update C Set EmiteVales = 0 From CFG_CB_EmisionVales C (NoLock) 
		Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 



	Insert Into CFG_CB_CuadroBasico_Claves ( IdEstado, IdCliente, IdNivel, IdClaveSSA_Sal, Status, Actualizado ) 
	Select IdEstado, IdCliente, IdNivel, IdClaveSSA_Sal, Status, Actualizado 
	From #tmpClaves_Cliente T 
	Where Not Exists 
	( 
		Select * From CFG_CB_CuadroBasico_Claves C (Nolock) 
		Where T.IdEstado = C.IdEstado and T.IdCliente = C.IdCliente and T.IdNivel = C.IdNivel and C.IdClaveSSA_Sal = T.IdClaveSSA_Sal 
	) 
	
	Insert Into CFG_CB_EmisionVales ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA, EmiteVales, Actualizado ) 
	Select IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA, max(EmiteVale) as EmiteVale, 0 as Actualizado 
	From #tmpClaves_Cliente T 
	Where Not Exists 
	( 
		Select * From CFG_CB_EmisionVales C (Nolock) 
		Where T.IdEstado = C.IdEstado and T.IdCliente = C.IdCliente and T.IdSubCliente = C.IdSubCliente and C.IdClaveSSA_Sal = T.IdClaveSSA_Sal 
	) 
	and Exists 
	( 
		Select * From CFG_ClavesSSA_Precios P (Nolock) 
		Where T.IdEstado = P.IdEstado and T.IdCliente = P.IdCliente and T.IdSubCliente = P.IdSubCliente and P.IdClaveSSA_Sal = T.IdClaveSSA_Sal 
	) 
	Group by IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA 	
	
---		spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_001  	



-------------------------------------------------- Reactivar los registros cargados 	
	Update C Set Status = T.Status  
	From CFG_CB_CuadroBasico_Claves C (NoLock) 
	Inner Join #tmpClaves_Cliente T (NoLock) 
		On ( C.IdEstado = T.IdEstado and C.IdCliente = T.IdCliente and T.IdNivel = C.IdNivel and C.IdClaveSSA_Sal = T.IdClaveSSA_Sal  ) 
		
	Update C Set EmiteVales = T.EmiteVale   
	From CFG_CB_EmisionVales C (NoLock) 
	Inner Join #tmpClaves_Cliente T (NoLock) 
		On ( C.IdEstado = T.IdEstado and C.IdCliente = T.IdCliente and T.IdSubCliente = C.IdSubCliente and C.IdClaveSSA_Sal = T.IdClaveSSA_Sal  ) 	
	Where T.EmiteVale = 1 		
-------------------------------------------------- Actualizar informacion  

---		spp_PRCS_OCEN__IntegracionDeCuadrosBasicos_001  	

End 
Go--#SQL 

