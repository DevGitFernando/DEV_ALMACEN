If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_Emision_Envio' and xType = 'P' )
    Drop Proc spp_Mtto_Vales_Emision_Envio
Go--#SQL
  
Create Proc spp_Mtto_Vales_Emision_Envio
(	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVale varchar(32) )
With Encryption 
As
Begin
Set NoCount On


	   If Not Exists ( Select * From Vales_Emision_Envio (NoLock) 
					   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale ) 
		  Begin 
			 Insert Into Vales_Emision_Envio 
				 (  IdEmpresa, IdEstado, IdFarmacia, FolioVale, FechaRegistro
				 ) 
			 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVale, GetDate()

          End
          
End
Go--#SQL	
