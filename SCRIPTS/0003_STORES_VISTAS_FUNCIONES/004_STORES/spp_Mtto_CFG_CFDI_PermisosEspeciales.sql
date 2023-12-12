-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'spp_Mtto_CFG_CFDI_PermisosEspeciales' and xType = 'P' )  
	Drop Proc spp_Mtto_CFG_CFDI_PermisosEspeciales 
Go--#SQL 

Create Proc spp_Mtto_CFG_CFDI_PermisosEspeciales 
(
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	--@FechaVigencia Varchar(10) = '', 
	@Tipo smallint = 1, 
	@Status varchar(1) = '', @MD5 varchar(500) = ''  
) 
As 
Begin 
Set NoCount Off 

	If Not Exists ( Select * From CFG_CFDI_PermisosEspeciales (NoLock)  Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ) 
		Begin 
			Insert Into CFG_CFDI_PermisosEspeciales ( IdEstado, IdFarmacia, Tipo, Status, MD5 ) 
			Select @IdEstado, @IdFarmacia, @Tipo, @Status, @MD5 
		End 
	Else 
		Begin 
			Update C Set Status = @Status, Tipo = @Tipo, MD5 = @MD5 
			From CFG_CFDI_PermisosEspeciales C (NoLock) 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
		End 

End  
Go--#SQL 

/* 

	exec sp_listacolumnas__stores spp_Mtto_CFG_CFDI_PermisosEspeciales	,	 1 

*/ 


