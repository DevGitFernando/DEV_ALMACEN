----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Emisores_mail' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_Emisores_mail
Go--#SQL    

Create Proc spp_Mtto_CFDI_Emisores_mail 
( 
	@IdEmpresa varchar(4) = '00000001', 
	@IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0001', 
	@Servidor varchar(100) = '', @Puerto int = 0, @TiempoEspera int = 0, 
	@Usuario varchar(100) = '', @Password varchar(100) = '', @EnableSSL bit = 1, 
	@EmailRespuesta varchar(100) = '', @NombreParaMostrar varchar(100) = '', @CC varchar(100) = '', 
	@Asunto varchar(100) = '', @MensajePredeterminado varchar(500) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	Delete From CFDI_Emisores_Mail Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
	Insert Into CFDI_Emisores_Mail ( IdEmpresa, IdEstado, IdFarmacia, Servidor, Puerto, TiempoDeEspera, 
		Usuario, Password, EnableSSL, EmailRespuesta, NombreParaMostrar, CC, Asunto, MensajePredeterminado )
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @Servidor, @Puerto, @TiempoEspera, 
		@Usuario, @Password, @EnableSSL, @EmailRespuesta, @NombreParaMostrar, @CC, @Asunto, @MensajePredeterminado  

End 
Go--#SQL    

/* 
	sp_listacolumnas CFDI_Emisores_Mail

	sp_listacolumnas__stores spp_Mtto_CFDI_Emisores_mail , 1  


	select * 
	from CFDI_Emisores_Mail
*/ 

	