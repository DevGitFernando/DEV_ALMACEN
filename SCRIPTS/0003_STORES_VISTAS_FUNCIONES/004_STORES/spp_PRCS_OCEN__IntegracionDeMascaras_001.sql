---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_PRCS_OCEN__IntegracionDeMascaras' and xType = 'P' ) 
   Drop Proc spp_PRCS_OCEN__IntegracionDeMascaras 
Go--#SQL 

Create Proc spp_PRCS_OCEN__IntegracionDeMascaras  
( 
	@IdEstado varchar(2) = '9', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0012' 
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
	

-------------------------------------------------- Actualizar informacion  
	Update C Set Status = 'C' From CFG_ClaveSSA_Mascara C (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente   

	
	Insert Into CFG_ClaveSSA_Mascara 
	( 
		 IdEstado, IdCliente, IdSubCliente, IdClaveSSA,  
		 Mascara, Descripcion, DescripcionCorta, 
		 Status, Actualizado 
		 , Presentacion 
	) 
	Select 
		 IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal as IdClaveSSA, 
		 Mascara, left(Descripcion, 5000), left(DescripcionCorta, 200), 
		 Status, 0 as Actualizado 
		 , left(Presentacion, 100) 
	From CFG_ClavesSSA_Mascaras__CargaMasiva T 
	Where Not Exists 
	( 
		Select * From CFG_ClaveSSA_Mascara C (Nolock) 
		Where T.IdEstado = C.IdEstado and T.IdCliente = C.IdCliente and T.IdSubCliente = C.IdSubCliente and T.IdClaveSSA_Sal = C.IdClaveSSA   
	) 	
	-- and len(T.Mascara) <= 50 and len(T.Descripcion) <= 5000 and len(T.DescripcionCorta) <= 200 and len(T.Presentacion) <= 100 


-------------------------------------------------- Reactivar los registros cargados 		
	Update C Set Status = T.Status, 
		Mascara = T.Mascara, Descripcion = left(T.Descripcion, 5000), DescripcionCorta = left(T.DescripcionCorta, 200), Presentacion = left(T.Presentacion, 100) 
	From CFG_ClaveSSA_Mascara C (NoLock) 
	Inner Join CFG_ClavesSSA_Mascaras__CargaMasiva T (NoLock) 
		On ( C.IdEstado = T.IdEstado and C.IdCliente = T.IdCliente and C.IdSubCliente = T.IdSubCliente and C.IdClaveSSA = T.IdClaveSSA_Sal  )	
	-- Where len(T.Mascara) <= 50 and len(T.Descripcion) <= 5000 and len(T.DescripcionCorta) <= 200 and len(T.Presentacion) <= 100 
	
-------------------------------------------------- Actualizar informacion  

---		spp_PRCS_OCEN__IntegracionDeMascaras  	

End 
Go--#SQL 

