------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_Emisores' and xType = 'V' ) 
	Drop View vw_CFDI_Emisores   
Go--#SQL 

Create View vw_CFDI_Emisores 
With Encryption 
As 

	Select 
		 C.IdEmpresa, C.NombreFiscal, C.NombreComercial, C.RFC, 
		 C.EsPersonaFisica, C.PublicoGeneral_AplicaIva, 
		 C.Telefonos, C.Fax, C.Email, 
		 C.DomExpedicion_DomFiscal, 
		 C.Pais, 
		 IsNull(G.IdEstado, '') as IdEstado, IsNull(G.Estado, '') as Estado, 
		 IsNull(G.IdMunicipio, '') as IdMunicipio, IsNull(G.Municipio, '') as Municipio, 		 
		 IsNull(G.IdColonia, '') as IdColonia, IsNull(G.Colonia, '') as Colonia, 			 
		 C.Calle, NoExterior, NoInterior, G.CodigoPostal, Referencia, 
		 
		 C.EPais, 
		 IsNull(G2.IdEstado, '') as EIdEstado, IsNull(G2.Estado, '') as EEstado, 
		 IsNull(G2.IdMunicipio, '') as EIdMunicipio, IsNull(G2.Municipio, '') as EMunicipio, 		 
		 IsNull(G2.IdColonia, '') as EIdColonia, IsNull(G2.Colonia, '') as EColonia, 			 
		 C.ECalle, ENoExterior, ENoInterior, ECodigoPostal, EReferencia, 
		 		 
		 Status 		 
	From CFDI_Emisores C (NoLock) 
	Left Join vw_CFDI_Geograficos G (NoLock) 
		On ( C.Estado = G.IdEstado and C.Municipio = G.IdMunicipio and C.Colonia = G.IdColonia ) 
	Left Join vw_CFDI_Geograficos G2 (NoLock) 
		On ( C.EEstado = G2.IdEstado and C.EMunicipio = G2.IdMunicipio and C.EColonia = G2.IdColonia ) 


Go--#SQL 



------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_Emisores_Sucursales' and xType = 'V' ) 
	Drop View vw_CFDI_Emisores_Sucursales    
Go--#SQL 

Create View vw_CFDI_Emisores_Sucursales 
With Encryption 
As 

	Select 
		 C.IdEmpresa, C.NombreFiscal, C.NombreComercial, C.RFC, 
		 C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia,  
		 C.EsPersonaFisica, C.PublicoGeneral_AplicaIva, 
		 C.Telefonos, C.Fax, C.Email, 
		 C.DomExpedicion_DomFiscal, 
		 C.Pais, 
		 --- IsNull(G.IdEstado, '') as IdEstado, IsNull(G.Estado, '') as Estado, 
		 IsNull(G.IdMunicipio, '') as IdMunicipio, IsNull(G.Municipio, '') as Municipio, 		 
		 IsNull(G.IdColonia, '') as IdColonia, IsNull(G.Colonia, '') as Colonia, 			 
		 C.Calle, NoExterior, NoInterior, G.CodigoPostal, Referencia, 
		 
		 C.EPais, 
		 IsNull(G2.IdEstado, '') as EIdEstado, IsNull(G2.Estado, '') as EEstado, 
		 IsNull(G2.IdMunicipio, '') as EIdMunicipio, IsNull(G2.Municipio, '') as EMunicipio, 		 
		 IsNull(G2.IdColonia, '') as EIdColonia, IsNull(G2.Colonia, '') as EColonia, 			 
		 C.ECalle, ENoExterior, ENoInterior, ECodigoPostal, EReferencia, 
		 		 
		 C.Status 		 
	From CFDI_Emisores_Sucursales C (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia )  
	Left Join vw_CFDI_Geograficos G (NoLock) 
		On ( C.Estado = G.IdEstado and C.Municipio = G.IdMunicipio and C.Colonia = G.IdColonia ) 
	Left Join vw_CFDI_Geograficos G2 (NoLock) 
		On ( C.EEstado = G2.IdEstado and C.EMunicipio = G2.IdMunicipio and C.EColonia = G2.IdColonia ) 


Go--#SQL 
