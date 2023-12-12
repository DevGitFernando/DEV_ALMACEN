-------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_Svr_UnidadesRegistradas' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_Svr_UnidadesRegistradas 
Go--#SQL     

Create Proc spp_Mtto_CFG_Svr_UnidadesRegistradas 
( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @Status int = 1, @EsRegional int = 0 ) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sStatus varchar(1) 

	Set @sStatus = 'A' 
	if @Status = 0 
	   Set @sStatus = 'C' 

	If Not Exists ( Select * From CFG_Svr_UnidadesRegistradas (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ) 
	   Insert Into CFG_Svr_UnidadesRegistradas ( IdEmpresa, IdEstado, IdFarmacia, Status, EsRegional, Actualizado ) 
	   Select @IdEmpresa, @IdEstado, @IdFarmacia, @sStatus, @EsRegional, 0 as Actualizado 
	Else 
	   Update CFG_Svr_UnidadesRegistradas Set Status = @sStatus, EsRegional = @EsRegional, Actualizado = 0 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

End 
Go--#SQL  
