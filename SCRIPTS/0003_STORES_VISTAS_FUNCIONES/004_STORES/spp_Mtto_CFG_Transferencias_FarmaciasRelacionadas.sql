-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'spp_Mtto_CFG_Transferencias_FarmaciasRelacionadas' and xType = 'P' )  
	Drop Proc spp_Mtto_CFG_Transferencias_FarmaciasRelacionadas 
Go--#SQL 

Create Proc spp_Mtto_CFG_Transferencias_FarmaciasRelacionadas 
(
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @IdFarmaciaRelacionada varchar(4) = '', 
	@Status varchar(1) = '', @MD5 varchar(500) = ''  
) 
As 
Begin 
Set NoCount Off 

	If Not Exists ( Select * From CFG_Transferencias_FarmaciasRelacionadas (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdFarmaciaRelacionada = @IdFarmaciaRelacionada ) 
		Begin 
			Insert Into CFG_Transferencias_FarmaciasRelacionadas ( IdEstado, IdFarmacia, IdFarmaciaRelacionada, Status, MD5 ) 
			Select @IdEstado, @IdFarmacia, @IdFarmaciaRelacionada, @Status, @MD5 
		End 
	Else 
		Begin 
			Update C Set Status = @Status, MD5 = @MD5 
			From CFG_Transferencias_FarmaciasRelacionadas C (NoLock) 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdFarmaciaRelacionada = @IdFarmaciaRelacionada
		End 

End  
Go--#SQL 

/* 
	Create Table CFG_Transferencias_FarmaciasRelacionadas  
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdFarmaciaRelacionada varchar(4) Not Null, 
		Status varchar(1) Not Null Default '', 
		MD5 varchar(500) Not Null Default ''  
	)
*/ 


