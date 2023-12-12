----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFDI_Emisores_Logos' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFDI_Emisores_Logos
Go--#SQL    

Create Proc spp_Mtto_FACT_CFDI_Emisores_Logos  ( @IdEmpresa varchar(3) = '00000001', @Logo text  ) 
With Encryption 
As 
Begin 
Set NoCount On 

	Delete From FACT_CFDI_Emisores_Logos Where IdEmpresa = @IdEmpresa 
	
	Insert Into FACT_CFDI_Emisores_Logos ( IdEmpresa, Logo )
	Select @IdEmpresa, @Logo 

End 
Go--#SQL    

