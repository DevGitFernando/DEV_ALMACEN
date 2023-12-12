If Exists ( Select Name From sysobjects (noLock) Where Name = 'vw_Clientes_SubClientes' and xType = 'V' ) 
   Drop View vw_Clientes_SubClientes
Go--#SQL

Create View vw_Clientes_SubClientes 
With Encryption 
AS 

	Select 
		C.IdCliente, 
		C.Nombre as NombreCliente, 
		IsNull(S.IdSubCliente, '') as IdSubCliente, IsNull(S.Nombre, '') as NombreSubCliente, 
		C.RFC, C.IdTipoCliente, T.Descripcion as TipoDeCliente, 
		C.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		C.IdMunicipio, M.Descripcion as Municipio,
		C.IdColonia, L.Descripcion as Colonia, 
		C.Domicilio, C.CodigoPostal, C.Telefonos, C.TieneLimiteDeCredito, cast(C.CreditoSuspendido as tinyint) as CreditoSuspendido, 
		S.PorcUtilidad, 
		cast(S.PermitirCapturaBeneficiarios as tinyint) as PermitirCapturaBeneficiarios, 
		cast(S.ImportaBeneficiarios as tinyint) as PermitirImportaBeneficiarios, 
		C.Status, S.Status as StatusSubCliente  
	From CatClientes C (NoLock) 
	Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) 
	Inner Join CatMunicipios M (NoLock) On ( C.IdEstado = M.IdEstado and C.IdMunicipio = M.IdMunicipio )
	Inner Join CatColonias L (NoLock) On ( C.IdEstado = L.IdEstado and C.IdMunicipio = L.IdMunicipio and C.IdColonia = L.IdColonia )
	Inner Join CatTiposDeClientes T (NoLock) On ( C.IdTipoCliente = T.IdTipoCliente )
	Left Join CatSubClientes S (NoLock) On ( C.IdCliente = S.IdCliente ) 		
Go--#SQL	 


If Exists ( Select Name From sysobjects (noLock) Where Name = 'vw_Clientes' and xType = 'V' ) 
   Drop View vw_Clientes 
Go--#SQL
    	
	
Create View vw_Clientes 
With Encryption 
As 
	Select 
		C.IdCliente, C.NombreCliente, C.RFC, C.IdTipoCliente, C.TipoDeCliente, C.IdEstado, C.Estado, C.ClaveRenapo, 
		C.IdMunicipio, C.Municipio, C.IdColonia, C.Colonia, C.Domicilio, C.CodigoPostal, C.Telefonos, 
		C.TieneLimiteDeCredito, C.CreditoSuspendido, C.Status
	From vw_Clientes_SubClientes C (NoLock) 
	Group by  
		C.IdCliente, C.NombreCliente, C.RFC, C.IdTipoCliente, C.TipoDeCliente, C.IdEstado, C.Estado, C.ClaveRenapo, 
		C.IdMunicipio, C.Municipio, C.IdColonia, C.Colonia, C.Domicilio, C.CodigoPostal, C.Telefonos, 
		C.TieneLimiteDeCredito, C.CreditoSuspendido, C.Status
Go--#SQL
