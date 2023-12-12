----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_CFDI_Emisores_Sucursales_Configuracion' and xType = 'P' ) 
   Drop Proc spp_CFDI_Emisores_Sucursales_Configuracion
Go--#SQL    

Create Proc spp_CFDI_Emisores_Sucursales_Configuracion  
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0001'  
) 
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




-------------------------------- Configuración 
	Select 
		IdEmpresa, IdEstado, Estado, IdFarmacia, Farmacia, NombreFiscal, NombreComercial, RFC, 
		Telefonos, Fax, Email, DomExpedicion_DomFiscal, 
		Pais, IdEstado, Estado, IdMunicipio, Municipio, IdColonia, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, 
		EPais, EIdEstado, EEstado, EIdMunicipio, EMunicipio, EIdColonia, EColonia, ECalle, ENoExterior, ENoInterior, ECodigoPostal, EReferencia, Status	
	From vw_CFDI_Emisores_Sucursales (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 


	Select 
		S.Serie, 'Clave docto' = S.IdTipoDocumento, 'Tipo documento' = NombreDocumento, 
		'Desde' = S.FolioInicial, 'Hasta' = S.FolioFinal, 'Ultimo folio utilizado' = S.FolioUtilizado, S.Status
	From CFDI_Emisores_SeriesFolios S (NoLock) 
	Inner Join CFDI_TiposDeDocumentos D (NoLock) On ( S.IdTipoDocumento = D.IdTipoDocumento ) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	

	Select 
		IdEmpresa, IdEstado, IdFarmacia, Servidor, Puerto, TiempoDeEspera, Usuario, Password, EnableSSL, 
		EmailRespuesta, NombreParaMostrar, CC, Asunto, MensajePredeterminado 
	From CFDI_Emisores_Mail (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 


End 
Go--#SQL 

---		sp_listacolumnas CFDI_Emisores_Mail 

--		spp_CFDI_Emisores_Sucursales_Configuracion  

--		select * from CFDI_Emisores_SeriesFolios 

