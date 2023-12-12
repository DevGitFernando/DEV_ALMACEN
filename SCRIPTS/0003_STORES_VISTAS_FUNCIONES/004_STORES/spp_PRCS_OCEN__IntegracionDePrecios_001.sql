---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_PRCS_OCEN__IntegracionDePrecios' and xType = 'P' ) 
   Drop Proc spp_PRCS_OCEN__IntegracionDePrecios 
Go--#SQL 

Create Proc spp_PRCS_OCEN__IntegracionDePrecios  
( 
	@IdEstado varchar(2) = '28', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0011' 
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
	Set @IdSubCliente = right('00000' +  @IdSubCliente, 4) 		
	

----------------- Preparar tablas  
	Select Distinct IdCliente, IdClaveSSA_Sal, Status, Actualizado 
	Into #tmpClaves_Cliente 
	From CFG_ClavesSSA_Precios__CargaMasiva 

	Select Distinct IdCliente, IdSubCliente, IdClaveSSA_Sal, Status, Actualizado
	Into #tmpClaves_SubCliente 	
	From CFG_ClavesSSA_Precios__CargaMasiva 
----------------- Preparar tablas  


-------------------------------------------------- Actualizar informacion  
	Update C Set Status = 'C' From CFG_Clientes_SubClientes_Claves C (NoLock) Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente  
	Update C Set Status = 'C', Precio = 0 From CFG_ClavesSSA_Precios C (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente  


	Insert Into CFG_Clientes_Claves ( IdCliente, IdClaveSSA_Sal, Status, Actualizado ) 
	Select IdCliente, IdClaveSSA_Sal, Status, Actualizado  
	From #tmpClaves_Cliente T 
	Where Not Exists 
	( 
		Select * From CFG_Clientes_Claves C (Nolock) 
		Where T.IdCliente = C.IdCliente and T.IdClaveSSA_Sal = C.IdClaveSSA_Sal 
	) 

	Insert Into CFG_Clientes_SubClientes_Claves ( IdCliente, IdSubCliente, IdClaveSSA_Sal, Status, Actualizado ) 
	Select IdCliente, IdSubCliente, IdClaveSSA_Sal, Status, Actualizado  
	From #tmpClaves_SubCliente T 
	Where Not Exists 
	( 
		Select * From CFG_Clientes_SubClientes_Claves C (Nolock) 
		Where T.IdCliente = C.IdCliente and T.IdSubCliente = C.IdSubCliente and T.IdClaveSSA_Sal = C.IdClaveSSA_Sal 
	) 
	
---		spp_PRCS_OCEN__IntegracionDePrecios  	
	
	Insert Into CFG_ClavesSSA_Precios ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Status, Actualizado, Factor,
	ContenidoPaquete_Licitado, IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida ) 
	Select IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, 0 as Precio, Status, Actualizado, Factor,
	ContenidoPaquete_Licitado, IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida
	From CFG_ClavesSSA_Precios__CargaMasiva T 
	Where Not Exists 
	( 
		Select * From CFG_ClavesSSA_Precios C (Nolock) 
		Where T.IdEstado = C.IdEstado and T.IdCliente = C.IdCliente and T.IdSubCliente = C.IdSubCliente and T.IdClaveSSA_Sal = C.IdClaveSSA_Sal 
	) 	


-------------------------------------------------- Reactivar los registros cargados 	
	Update C Set Status = T.Status  
	From CFG_Clientes_Claves C (NoLock) 
	Inner Join #tmpClaves_Cliente T On ( C.IdCliente = T.IdCliente and C.IdClaveSSA_Sal = T.IdClaveSSA_Sal  )
	
	Update C Set Status = T.Status 
	From CFG_Clientes_SubClientes_Claves C (NoLock) 
	Inner Join #tmpClaves_SubCliente T On ( C.IdCliente = T.IdCliente and C.IdSubCliente = T.IdSubCliente and C.IdClaveSSA_Sal = T.IdClaveSSA_Sal  )	
	
	Update C Set Status = T.Status, Precio = T.Precio, Factor = T.Factor, ContenidoPaquete_Licitado = T.ContenidoPaquete_Licitado, IdPresentacion_Licitado = T.IdPresentacion_Licitado,
					  Dispensacion_CajasCompletas = T.Dispensacion_CajasCompletas,
					   SAT_ClaveDeProducto_Servicio = T.SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida= T.SAT_UnidadDeMedida
	From CFG_ClavesSSA_Precios C (NoLock) 
	Inner Join CFG_ClavesSSA_Precios__CargaMasiva T (NoLock) 
		On ( C.IdEstado = T.IdEstado and C.IdCliente = T.IdCliente and C.IdSubCliente = T.IdSubCliente and C.IdClaveSSA_Sal = T.IdClaveSSA_Sal  )	
	
-------------------------------------------------- Actualizar informacion  

---		spp_PRCS_OCEN__IntegracionDePrecios  	

End 
Go--#SQL 

