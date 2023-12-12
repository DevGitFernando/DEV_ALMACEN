------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_ClavesSSA_Claves' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_ClavesSSA_Claves 
Go--#SQL

Create Proc spp_Mtto_CFG_ClavesSSA_Claves ( @IdClaveSSA_Sal varchar(4), @IdClaveSSA_Sal_Relacionada varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_ClavesSSA_Claves (NoLock) 
	   Where IdClaveSSA_Sal = @IdClaveSSA_Sal and IdClaveSSA_Sal_Relacionada = @IdClaveSSA_Sal_Relacionada ) 
	   Insert Into CFG_ClavesSSA_Claves ( IdClaveSSA_Sal, IdClaveSSA_Sal_Relacionada, Status, Actualizado ) 
	   Select @IdClaveSSA_Sal, @IdClaveSSA_Sal_Relacionada, @Status, 0 as Actualizado 
	Else 
	   Update CFG_ClavesSSA_Claves Set Status = @Status, Actualizado = 0 
	   Where IdClaveSSA_Sal = @IdClaveSSA_Sal and IdClaveSSA_Sal_Relacionada = @IdClaveSSA_Sal_Relacionada 


End 
Go--#SQL
