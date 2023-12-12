-------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_Clientes' and xType = 'V' ) 
   Drop View vw_CFDI_Clientes 
Go--#SQL

Create View vw_CFDI_Clientes 
With Encryption 
As 
	Select 	
		IdCliente, Nombre, NombreComercial, RFC, 
		'' as TipoDeCliente, '' as TipoDeClienteDescripcion, 				
		'' as Telefono_Default, 
		'' Email_Default, 
		FechaRegistro, Status, (case when Status = 'A' Then 'ACTIVO' Else 'CANCELADO' End) as StatusDescripcion  
	From CFDI_Clientes P (noLock) 
Go--#SQL 	 	
	

-------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_Clientes_Direcciones' and xType = 'V' ) 
   Drop View vw_CFDI_Clientes_Direcciones  
Go--#SQL 

Create View vw_CFDI_Clientes_Direcciones 
With Encryption 
As 

	Select P.IdCliente, P.Nombre, P.NombreComercial, P.RFC, 
		TipoDeCliente, TipoDeClienteDescripcion, 
		P.Telefono_Default, P.Email_Default, 
		P.FechaRegistro, P.Status as StatusCliente, 
		D.IdDireccion, 
		IsNull(D.Pais, '') as Pais, 
		IsNull(D.IdEstado, '') as IdEstado, G.Estado, 
		IsNull(D.IdMunicipio, '') as IdMunicipio, G.Municipio, 
		IsNull(D.IdColonia, '') as IdColonia, G.Colonia, 
		IsNull(D.Calle, '') as Calle,
		IsNull(D.NumeroExterior, '') as NumeroExterior,		  
		IsNull(D.NumeroInterior, '') as NumeroInterior,		  		
		IsNull(G.CodigoPostal, '') as CodigoPostal, 
		IsNull(D.Referencia, '') as Referencia,   
		IsNull(D.Status, '') as StatusDomicilio,  
		(case when IsNull(D.Status, '') = '' Then '' Else 
			(case when D.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' End) End)  
		as StatusDomicilioDescripcion  
	From vw_CFDI_Clientes P (NoLock) 
	Inner Join CFDI_Clientes_Direcciones D (NoLock) On ( P.IdCliente = D.IdCliente ) 
	Inner Join vw_CFDI_Geograficos G (NoLock) On ( D.IdEstado = G.IdEstado and D.IdMunicipio = G.IdMunicipio and D.IdColonia = G.IdColonia ) 
	
		
Go--#SQL



-------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_Clientes_EMails' and xType = 'V' ) 
   Drop View vw_CFDI_Clientes_EMails  
Go--#SQL

Create View vw_CFDI_Clientes_EMails 
With Encryption 
As 

	Select P.IdCliente, P.Nombre, P.NombreComercial, P.RFC, P.Status as StatusCliente, 
		D.IdEmail, D.IdTipoEMail, M.Descripcion as TipoMail, D.Email, 
		IsNull(D.Status, '') as StatusEmail,  
		(case when D.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' End) as StatusEmailDescripcion  		 
	From vw_CFDI_Clientes P (NoLock) 
	Inner Join CFDI_Clientes_EMails D (NoLock) On ( P.IdCliente = D.IdCliente ) 
	Inner Join CFDI_TiposEmail M (NoLock) On ( D.IdTipoEMail = M.IdTipoEMail ) 
	
Go--#SQL



-------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_Clientes_Telefonos' and xType = 'V' ) 
   Drop View vw_CFDI_Clientes_Telefonos  
Go--#SQL

Create View vw_CFDI_Clientes_Telefonos 
With Encryption 
As 

	Select P.IdCliente, P.Nombre, P.NombreComercial, P.RFC,  P.Status as StatusCliente, 
		D.IdTelefono, D.IdTipoTelefono, M.Descripcion as TipoTelefono, D.Telefono, 
		IsNull(D.Status, '') as StatusEmail,  
		(case when D.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' End) as StatusTelefonoDescripcion  		 
	From vw_CFDI_Clientes P (NoLock) 
	Inner Join CFDI_Clientes_Telefonos D (NoLock) On ( P.IdCliente = D.IdCliente ) 
	Inner Join CFDI_TiposTelefonos M (NoLock) On ( D.IdTipoTelefono = M.IdTipoTelefono ) 
	
Go--#SQL





------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_Clientes_Informacion' and xType = 'V' ) 
	Drop View vw_CFDI_Clientes_Informacion   
Go--#SQL 

Create View vw_CFDI_Clientes_Informacion 
With Encryption 
As 

	Select 
		 C.IdCliente, C.Nombre, C.Nombre as NombreFiscal, 
		 (case when C.NombreComercial = '' then C.Nombre Else C.NombreComercial end) as NombreComercial, 
		 C.RFC, 
		 C.TipoDeCliente, C.TipoDeClienteDescripcion, 
		 IsNull(
		 (
			 Select top 1 Telefono 
			 From CFDI_Clientes_Telefonos T (NoLock) 
			 Where C.IdCliente = T.IdCliente and T.Status = 'A' 
			 Order by IdTelefono 
		 ), '') as Telefonos, '' as Fax, '' as Email,  
		 C.Pais, C.IdEstado, C.Estado, C.IdMunicipio, C.Municipio, C.Municipio as Localidad, C.IdColonia, C.Colonia, 
		 C.Calle, C.NumeroExterior as NoExterior, C.NumeroInterior as NoInterior, C.CodigoPostal, C.Referencia, 
		 C.StatusDomicilio as Status 		 
	From vw_CFDI_Clientes_Direcciones C (NoLock) 


Go--#SQL 


