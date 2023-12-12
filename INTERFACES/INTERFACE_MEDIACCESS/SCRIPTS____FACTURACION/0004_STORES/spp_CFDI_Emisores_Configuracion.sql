----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_CFDI_Emisores_Configuracion' and xType = 'P' ) 
   Drop Proc spp_CFDI_Emisores_Configuracion
Go--#SQL    

Create Proc spp_CFDI_Emisores_Configuracion  ( @IdEmpresa varchar(3) = '002' ) 
With Encryption 
As 
Begin 
Set NoCount On 
--		sp_listacolumnas CFDI_Emisores_Cerfificados  


---------------- Informacion del Emisor 
	Select 
		E.IdEmpresa, E.NombreFiscal as Nombre, E.NombreFiscal, E.NombreComercial, E.RFC, 
		space(100) as ClaveRegistroPatronal, E.EsPersonaFisica, E.PublicoGeneral_AplicaIva, 
		E.Telefonos, E.Fax, E.Email, E.DomExpedicion_DomFiscal, E.Status, 
		P.IdPAC, P.Usuario, P.Password, P.Password as KeyLicencia, P.EnProduccion, 
		C.UrlProduccion as DireccionUrl
	into #CFDI_Emisores 	
	From CFDI_Emisores E (NoLock) 
	Inner Join CFDI_Emisores_PAC P (NoLock) On ( E.IdEmpresa = P.IdEmpresa ) 
	Inner Join CFDI_PACs C (NoLock) On ( P.IdPAC = C.IdPAC ) 
	Where E.IdEmpresa = @IdEmpresa   

	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFDI_NM_Emisores' and xType = 'u' ) 
	Begin 
		Update T Set ClaveRegistroPatronal = E.ClaveRegistroPatronal
		From #CFDI_Emisores T 
		Inner Join CFDI_NM_Emisores E (NoLock) On ( T.IdEmpresa = E.IdEmpresa ) 
	End 



-------------------------------- Configuración 
	Select IdEmpresa, Nombre, NombreFiscal, NombreComercial, RFC, EsPersonaFisica, PublicoGeneral_AplicaIva, ClaveRegistroPatronal, 
		Telefonos, Fax, Email, DomExpedicion_DomFiscal, Status, 
		IdPAC, Usuario, Password, KeyLicencia, EnProduccion, 
		DireccionUrl	
	From #CFDI_Emisores 	

	Select 
		IdEmpresa, NombreFiscal, NombreComercial, RFC, Telefonos, Fax, Email, DomExpedicion_DomFiscal, 
		Pais, IdEstado, Estado, IdMunicipio, Municipio, IdColonia, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, 
		EPais, EIdEstado, EEstado, EIdMunicipio, EMunicipio, EIdColonia, EColonia, ECalle, ENoExterior, ENoInterior, ECodigoPostal, EReferencia, Status	
	From vw_CFDI_Emisores (NoLock) Where IdEmpresa = @IdEmpresa   


	Select 
		IdEmpresa, NumeroDeCertificado, NombreCertificado, Certificado, ValidoDesde, ValidoHasta, FechaInicio, FechaFinal, 
		Serie, Serial, NombreLlavePrivada, LlavePrivada, PasswordPublico, NombreCertificadoPfx, CertificadoPfx, 
		AvisoVencimiento, TiempoAviso, Status, Actualizado	
	From CFDI_Emisores_Certificados (NoLock) Where IdEmpresa = @IdEmpresa   


	Select Top 0 
		S.Serie, 'Clave docto' = S.IdTipoDocumento, 'Tipo documento' = NombreDocumento, 
		'Desde' = S.FolioInicial, 'Hasta' = S.FolioFinal, 'Ultimo folio utilizado' = S.FolioUtilizado, S.Status
	From CFDI_Emisores_SeriesFolios S (NoLock) 
	Inner Join CFDI_TiposDeDocumentos D (NoLock) On ( S.IdTipoDocumento = D.IdTipoDocumento ) 
	Where IdEmpresa = @IdEmpresa


	Select Logo 
	From CFDI_Emisores_Logos (NoLock) 
	Where IdEmpresa = @IdEmpresa 


	Select cast((case when cast(IsNull(E.IdEmpresa, 0) as int) = 0 then 0 else 1 end) as bit) as Activo, R.IdRegimen, R.Descripcion 
	From CFDI_RegimenFiscal R (NoLock) 
	Left Join CFDI_Emisores_Regimenes E (NoLock) On ( E.IdEmpresa = @IdEmpresa and R.IdRegimen = E.IdRegimen )  


	----Select IdEmpresa, Servidor, Puerto, TiempoDeEspera, Usuario, Password, EnableSSL, EmailRespuesta, NombreParaMostrar, CC, Asunto, MensajePredeterminado 
	----From CFDI_Emisores_Mail (NoLock) 
	----Where IdEmpresa = @IdEmpresa 


	----Select IdEmpresa, TipoDeFormato, NombreFormato 
	----From CFDI_Emisores_FormatosCFDI (NoLock) 
	----Where IdEmpresa = @IdEmpresa 
	----Order By TipoDeFormato 



End 
Go--#SQL 

--		spp_CFDI_Emisores_Configuracion  

--		select * from CFDI_Emisores_SeriesFolios 

