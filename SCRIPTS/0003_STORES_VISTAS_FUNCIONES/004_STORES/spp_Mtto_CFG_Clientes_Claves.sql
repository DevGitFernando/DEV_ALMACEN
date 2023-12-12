------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_Clientes_Claves' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_Clientes_Claves 
Go--#SQL

Create Proc spp_Mtto_CFG_Clientes_Claves ( @IdCliente varchar(4), @IdClaveSSA_Sal varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_Clientes_Claves (NoLock) Where IdCliente = @IdCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal ) 
	   Insert Into CFG_Clientes_Claves ( IdCliente, IdClaveSSA_Sal, Status, Actualizado ) 
	   Select @IdCliente, @IdClaveSSA_Sal, @Status, 0 as Actualizado 
	Else 
	   Update CFG_Clientes_Claves Set Status = @Status, Actualizado = 0 
	   Where IdCliente = @IdCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal 


	-- Actualizar la información de los SubClientes 
	Update CFG_Clientes_SubClientes_Claves 
		Set Status = @Status, Actualizado = 0 
	Where IdCliente = @IdCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal 

End 
Go--#SQL


------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_Clientes_SubClientes_Claves' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_Clientes_SubClientes_Claves 
Go--#SQL

Create Proc spp_Mtto_CFG_Clientes_SubClientes_Claves ( @IdCliente varchar(4), @IdSubCliente varchar(4), 
	@IdClaveSSA_Sal varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_Clientes_SubClientes_Claves (NoLock) 
					Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal ) 
	   Insert Into CFG_Clientes_SubClientes_Claves ( IdCliente, IdSubCliente, IdClaveSSA_Sal, Status, Actualizado ) 
	   Select @IdCliente, @IdSubCliente, @IdClaveSSA_Sal, @Status, 0 as Actualizado 
	Else 
	   Update CFG_Clientes_SubClientes_Claves Set Status = @Status, Actualizado = 0 
	   Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal 

End 
Go--#SQL
