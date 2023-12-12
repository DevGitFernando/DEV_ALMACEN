------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (noLock) Where Name = 'vw_Farmacias' and xType = 'V' ) 
   Drop View vw_Farmacias 
Go--#SQL 

Create View vw_Farmacias 
With Encryption 
As 

/*
Muestra toda la información referente a las farmacias registradas 
*/ 

	Select  F.IdEstado, E.Nombre as Estado, E.ClaveRenapo, E.Status as EdoStatus, 
		F.IdFarmacia, F.NombreFarmacia as Farmacia, 
		
		F.CLUES, F.NombrePropio_UMedica, 

		F.EsUnidosis, 
		F.IdTipoUnidad, T.Descripcion as TipoDeUnidad, T.Abreviatura as Siglas, 
		F.IdNivelDeAtencion, 
		N.Descripcion as NivelDeAtencion, 
		F.eMail, 
		F.EsDeConsignacion, 
		F.ManejaVtaPubGral, F.ManejaControlados, 
		F.IdRegion, R.Descripcion as Region, 
		F.IdSubRegion, SR.Descripcion as SubRegion, 
		F.IdJurisdiccion, J.Descripcion as Jurisdiccion, 
		cast(F.EsAlmacen as int) as EsAlmacen, F.IdAlmacen, cast(F.EsFrontera as int) as EsFrontera, 
		F.IdMunicipio, M.Descripcion as Municipio, 
		F.IdColonia, C.Descripcion as Colonia, 
		F.Domicilio, F.CodigoPostal, F.Telefonos, 
		F.Transferencia_RecepcionHabilitada, 
		(Case When F.Transferencia_RecepcionHabilitada = 1 Then 'HABILITADA' Else 'DESHABILITADA' End) as Transferencia_RecepcionHabilitada__Descripcion,  
		F.Status, F.Status as FarmaciaStatus, 
		(Case When F.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' End) as FarmaciaStatusAux 
	From CatFarmacias F (NoLock) 
	Inner Join CatEstados E (NoLock) On ( F.IdEstado = E.IdEstado ) 
	Inner Join CatJurisdicciones J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 
	Inner Join CatTiposUnidades T On ( F.IdTipoUnidad = T.IdTipoUnidad ) 
	Inner Join CatUnidadesMedicas_NivelesDeAtencion N On ( F.IdNivelDeAtencion = N.IdNivelDeAtencion ) 
	Inner Join CatRegiones R (NoLock) On ( F.IdRegion = R.IdRegion ) 
	Inner Join CatSubRegiones SR (NoLock) On ( F.IdRegion = SR.IdRegion and F.IdSubRegion = SR.IdSubRegion )
	Inner Join CatMunicipios M (NoLock) On ( F.IdEstado = M.IdEstado and F.IdMunicipio = M.IdMunicipio ) 
	Inner Join CatColonias C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdMunicipio = C.IdMunicipio and F.IdColonia = C.IdColonia ) 

Go--#SQL
