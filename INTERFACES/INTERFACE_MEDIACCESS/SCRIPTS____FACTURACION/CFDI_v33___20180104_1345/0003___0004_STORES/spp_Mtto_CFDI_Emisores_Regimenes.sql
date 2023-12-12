----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Emisores_Regimenes' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_Emisores_Regimenes
Go--#SQL    

Create Proc spp_Mtto_CFDI_Emisores_Regimenes  ( @IdEmpresa varchar(4) = '00000001', @IdRegimen varchar(4) = ''  ) 
With Encryption 
As 
Begin 
Set NoCount On 
	
	If Not Exists ( Select * From CFDI_Emisores_Regimenes (NoLock) Where IdEmpresa = @IdEmpresa and IdRegimen = @IdRegimen ) 
		Begin 
			Insert Into CFDI_Emisores_Regimenes ( IdEmpresa, IdRegimen ) 
			Select @IdEmpresa, @IdRegimen 					
		End 
	----Else 
		----Begin 
		----	Update R Set Status = '
		----	From CFDI_Emisores_Regimenes R (NoLock) 
		----	Where IdEmpresa = @IdEmpresa and IdRegimen = @IdRegimen 
		----End 	

End 
Go--#SQL    

