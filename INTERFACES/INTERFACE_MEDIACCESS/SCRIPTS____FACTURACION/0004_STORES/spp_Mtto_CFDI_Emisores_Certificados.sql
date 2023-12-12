If Exists ( Select Name From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Emisores_Certificados' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_Emisores_Certificados 
Go--#SQL 

Create Proc spp_Mtto_CFDI_Emisores_Certificados 
( 
	@IdEmpresa varchar(3) = '', 
	@NumeroDeCertificado varchar (100) = '', 
	@NombreCertificado varchar(100) = '', @Certificado varchar(max) = '', 
	@NombreLlavePrivada varchar(100) = '', @LlavePrivada varchar(max) = '', @PasswordPublico varchar(100) = '', 
	@NombreCertificadoPfx varchar(100) = '', @CertificadoPfx varchar(max) = '', 		
	@Status varchar(1) = 'A'
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@iTipo int,
	@Mensaje varchar(200) 
	
	Delete From CFDI_Emisores_Certificados Where IdEmpresa = @IdEmpresa 

	Insert Into CFDI_Emisores_Certificados ( IdEmpresa, NumeroDeCertificado, NombreCertificado, Certificado, NombreLlavePrivada, LlavePrivada, PasswordPublico, NombreCertificadoPfx, CertificadoPfx, Status ) 
	Select @IdEmpresa, @NumeroDeCertificado, @NombreCertificado, @Certificado, @NombreLlavePrivada, @LlavePrivada, @PasswordPublico, @NombreCertificadoPfx, @CertificadoPfx, @Status 	

		
End 
Go--#SQL 
   
--		sp_listacolumnas CFDI_Emisores 

--		sp_listacolumnas__Stores spp_Mtto_CFDI_Emisores_Certificados   , 1 
 
	