If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Empresas' and xType = 'V' ) 
   Drop View vw_Empresas 
Go--#SQL

Create View vw_Empresas 
With Encryption 
As 

	Select IdEmpresa, Nombre as Empresa, NombreCorto, EsDeConsignacion, RFC, EdoCiudad, Colonia, Domicilio, CodigoPostal, Status, Actualizado 
	From CatEmpresas (NoLock) 
	
Go--#SQL 
