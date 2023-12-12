


------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_COM_Perfiles_Personal_ClavesSSA' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_COM_Perfiles_Personal_ClavesSSA 
Go--#SQL

Create Proc spp_Mtto_CFG_COM_Perfiles_Personal_ClavesSSA ( @IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4), 
								@IdClaveSSA_Sal varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 
	
	Declare @ClaveSSA varchar(30)
	
	Set @ClaveSSA = ''
	
	Set @ClaveSSA = ( Select ClaveSSA_Base From vw_ClavesSSA_Sales Where IdClaveSSA_Sal = @IdClaveSSA_Sal )

	If Not Exists ( Select * From CFG_COM_Perfiles_Personal_ClavesSSA (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal and IdClaveSSA_Sal = @IdClaveSSA_Sal ) 
	   Insert Into CFG_COM_Perfiles_Personal_ClavesSSA ( IdEstado, IdFarmacia, IdPersonal, IdClaveSSA_Sal, ClaveSSA, Status, Actualizado ) 
	   Select @IdEstado, @IdFarmacia, @IdPersonal, @IdClaveSSA_Sal, @ClaveSSA, @Status, 0 as Actualizado 
	Else 
	   Update CFG_COM_Perfiles_Personal_ClavesSSA Set Status = @Status, Actualizado = 0 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal and IdClaveSSA_Sal = @IdClaveSSA_Sal 



End 
Go--#SQL