If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Beneficiarios_Domicilios' and xType = 'V' ) 
   Drop View vw_Beneficiarios_Domicilios 
Go--#SQL

Create View vw_Beneficiarios_Domicilios 
With Encryption 
As 
	Select B.IdEstado, B.IdFarmacia, B.IdCliente, B.IdSubCliente, B.IdBeneficiario,
		B.IdEstado_D, E.Nombre As Estado_D, B.IdMunicipio_D, M.Descripcion As Municipio_D, B.IdColonia_D, C.Descripcion As Colonia_D,
		B.CodigoPostal, B.Direccion, B.Referencia, B.Telefonos
	From CatBeneficiarios_Domicilios B (NoLock)
	Inner Join CatEstados E (NoLock) On (B.IdEstado_D = E.IdEstado)
	Inner Join CatMunicipios M (NoLock) ON (B.IdEstado_D = M.IdEstado And B.IdMunicipio_D = M.IdMunicipio)
	Inner Join CatColonias C (NoLock) ON (B.IdEstado_D = C.IdEstado And B.IdMunicipio_D = C.IdMunicipio And B.IdColonia_D = C.IdColonia)

Go--#SQL 	 	
	