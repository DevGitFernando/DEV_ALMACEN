If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_Claves_Excluir_Generacion_Pedidos' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_Claves_Excluir_Generacion_Pedidos 
Go--#SQL

Create Proc spp_Mtto_CFG_Claves_Excluir_Generacion_Pedidos 
( 
	@IdEstado varchar(2), @IdCliente varchar(4), @IdSubCliente varchar(4),
	@IdClaveSSA_Sal varchar(4), @ClaveSSA varchar(30), @ExcluirDePedido tinyint = 0   
)  
With Encryption 
As
Begin 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion 
	Opcion 2.- Cancelar ----Eliminar.
	*/ 

	Set @sMensaje = ''	
	Set @iActualizado = 0 
	
       
	If Not Exists ( Select * From CFG_Claves_Excluir_Generacion_Pedidos (NoLock) 
					Where IdEstado = @IdEstado and IdCliente = @IdCliente 
					and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal and ClaveSSA = @ClaveSSA ) 
		Begin 
			Insert Into CFG_Claves_Excluir_Generacion_Pedidos ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA, ExcluirDePedido, Actualizado ) 
			Select @IdEstado, @IdCliente, @IdSubCliente, @IdClaveSSA_Sal, @ClaveSSA, @ExcluirDePedido, @iActualizado 
		End 
	Else 
		Begin 
			Update CFG_Claves_Excluir_Generacion_Pedidos Set ExcluirDePedido = @ExcluirDePedido
			Where IdEstado = @IdEstado and IdCliente = @IdCliente 
				and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal and ClaveSSA = @ClaveSSA  
		End 
          
End 
Go--#SQL