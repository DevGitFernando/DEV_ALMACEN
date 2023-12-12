--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFG_Empresas' and xType = 'V' ) 
   Drop View vw_CFG_Empresas
Go--#SQL 

Create View vw_CFG_Empresas 
With Encryption 
As 

	Select 
		E.IdEmpresa, E.Nombre, E.NombreCorto, E.EsDeConsignacion, E.RFC, E.EdoCiudad, E.Colonia, E.Domicilio, E.CodigoPostal, E.Status
	From CatEmpresas E (NoLock) 
	Inner Join CFG_Empresas CE (NoLock) On ( E.IdEmpresa = CE.IdEmpresa and CE.Status = 'A' ) 	

Go--#SQL 


--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_EmpresasEstados' and xType = 'V' ) 
   Drop View vw_EmpresasEstados 
Go--#SQL

Create View vw_EmpresasEstados 
With Encryption 
As 

	Select C.IdEmpresaEdo, S.IdEmpresa, S.Nombre as NombreEmpresa, S.NombreCorto as NombreCortoEmpresa, 
		S.EsDeConsignacion as EsEmpConsignacion, S.Status as StatusEmp, 
		IsNull(E.IdEstado, '') as IdEstado, IsNull(E.Nombre, '') as NombreEstado, 
		IsNull(E.ClaveRenapo, '') as ClaveRenapo, IsNull(E.Status, '') as StatusEdo,
		C.Status As StatusRelacion
	From CatEmpresasEstados C (NoLock) 
	Inner Join CFG_Empresas CE (NoLock) On ( C.IdEmpresa = CE.IdEmpresa and CE.Status = 'A' ) 
	Left Join CatEmpresas S (NoLock) On ( C.IdEmpresa = S.IdEmpresa ) 
	Left Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) 
	
Go--#SQL
 

--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_EmpresasFarmacias' and xType = 'V' ) 
   Drop View vw_EmpresasFarmacias 
Go--#SQL

Create View vw_EmpresasFarmacias 
With Encryption 
As 

	Select C.IdEmpresa, S.Nombre as NombreEmpresa, S.NombreCorto as NombreCortoEmpresa,
		F.IdEstado, F.Estado, F.ClaveRenapo, S.EsDeConsignacion as EsEmpConsignacion, S.Status as StatusEmp, 
		F.IdFarmacia, F.Farmacia, F.IdJurisdiccion, F.Jurisdiccion, 
		F.ManejaVtaPubGral, F.ManejaControlados, F.EsAlmacen, F.EsUnidosis, F.Status, F.IdTipoUnidad, F.TipoDeUnidad, 
		F.IdMunicipio, F.Municipio, 
		C.Status As StatusRelacion 
	From CFG_EmpresasFarmacias C (NoLock) 
	Inner Join CatEmpresas S (NoLock) On ( C.IdEmpresa = S.IdEmpresa ) 	
	Inner Join CFG_Empresas CE (NoLock) On ( C.IdEmpresa = CE.IdEmpresa and CE.Status = 'A' ) 	
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
Go--#SQL 	
 
