
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INT_AMPM__Farmacias' and xType = 'V' ) 
   Drop View vw_INT_AMPM__Farmacias
Go--#SQL

Create View vw_INT_AMPM__Farmacias
With Encryption 
As 

	Select 
		MA.IdEmpresa, 
		F.IdEstado, F.Estado, F.ClaveRenapo, F.EdoStatus, 
		MA.Referencia_AMPM, MA.Referencia_AMPM_Facturacion, 
		F.IdFarmacia, F.Farmacia, 	
		F.EsUnidosis, F.IdTipoUnidad, F.TipoDeUnidad, F.Siglas, 
		F.eMail, F.EsDeConsignacion, F.ManejaVtaPubGral, F.ManejaControlados, F.IdRegion, F.Region, F.IdSubRegion, F.SubRegion, 
		F.IdJurisdiccion, F.Jurisdiccion, F.EsAlmacen, F.IdAlmacen, F.EsFrontera, F.IdMunicipio, F.Municipio, F.IdColonia, F.Colonia, 
		F.Domicilio, F.CodigoPostal, F.Telefonos, F.Status, F.FarmaciaStatus, F.FarmaciaStatusAux
	From vw_Farmacias F (NoLock) 
	Inner Join INT_AMPM__CFG_FarmaciasClinicas MA (NoLock) 
		On ( F.IdEstado = MA.IdEstado and F.IdFarmacia = MA.IdFarmacia ) 

	
Go--#SQL 

	
	select * 
	from vw_INT_AMPM__Farmacias 


