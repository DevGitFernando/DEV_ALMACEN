-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'spp_Mtto_CFG_Almacenes_PermisosEspeciales' and xType = 'P' )  
	Drop Proc spp_Mtto_CFG_Almacenes_PermisosEspeciales 
Go--#SQL 

Create Proc spp_Mtto_CFG_Almacenes_PermisosEspeciales 
(
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @FechaVigencia Varchar(10) = '',
	@Status varchar(1) = '', @MD5 varchar(500) = ''  
) 
As 
Begin 
Set NoCount Off 

	If Not Exists ( Select * From CFG_Almacenes_PermisosEspeciales (NoLock)  Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ) 
		Begin 
			Insert Into CFG_Almacenes_PermisosEspeciales ( IdEstado, IdFarmacia, FechaVigencia, Status, MD5 ) 
			Select @IdEstado, @IdFarmacia, @FechaVigencia, @Status, @MD5 
		End 
	Else 
		Begin 
			Update C Set FechaVigencia = @FechaVigencia, Status = @Status, MD5 = @MD5 
			From CFG_Almacenes_PermisosEspeciales C (NoLock) 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
		End 

End  
Go--#SQL 

/* 
	Create Table CFG_Almacenes_PermisosEspeciales  
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdFarmaciaRelacionada varchar(4) Not Null, 
		Status varchar(1) Not Null Default '', 
		MD5 varchar(500) Not Null Default ''  
	)
*/ 


