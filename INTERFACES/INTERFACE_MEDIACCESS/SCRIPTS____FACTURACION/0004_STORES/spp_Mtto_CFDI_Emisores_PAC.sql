----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Emisores_PAC' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_Emisores_PAC
Go--#SQL    

Create Proc spp_Mtto_CFDI_Emisores_PAC  
( 
	@IdEmpresa varchar(3) = '00000001', @IdPAC varchar(3) = '001', 
	@Usuario varchar(1000) = 'DEMO520704UL7', @Password varchar(1000) = 'AXkcrC%%%', 
	@EnProduccion smallint = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On 

	Delete From CFDI_Emisores_PAC Where IdEmpresa = @IdEmpresa and IdPAC = @IdPAC 
	
	Insert Into CFDI_Emisores_PAC (  IdEmpresa, IdPAC, Usuario, Password, EnProduccion )
	Select @IdEmpresa, @IdPAC, @Usuario, @Password, @EnProduccion 

End 
Go--#SQL    

