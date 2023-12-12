If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_CFD_Series_y_Folios' and xType = 'V' ) 
	Drop View vw_FACT_CFD_Series_y_Folios 
Go--#SQL 
 	
Create View vw_FACT_CFD_Series_y_Folios  
With Encryption 
As 

	Select 
		S.IdEmpresa, 
		cast(S.AñoAprobacion as varchar) as AñoAprobacion, cast(S.NumAprobacion as varchar) as NumAprobacion, 
		S.IdTipoDocumento, D.NombreDocumento as TipoDeDocumento, 
		S.IdentificadorSerie, 
		cast(S.Serie as varchar) as Serie, S.NombreDocumento, cast(S.FolioInicial as varchar) as FolioInicial, cast(S.FolioFinal as varchar) as FolioFinal, 
		cast(S.FolioUtilizado as varchar) as FolioUtilizado, S.Status -- , Actualizado 
	From FACT_CFD_SeriesFolios S (NoLock) 
	Inner Join FACT_CFD_TiposDeDocumentos D (NoLock) On ( S.IdTipoDocumento = D.IdTipoDocumento ) 

Go--#SQL	
	
---	Select * from FACT_CFD_SeriesFolios 	
	
-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_CFD_Sucursales_Series' and xType = 'V' ) 
	Drop View vw_FACT_CFD_Sucursales_Series 
Go--#SQL 
 	
Create View vw_FACT_CFD_Sucursales_Series 
With Encryption 
As 

	Select 
		F.IdEmpresa, IsNull(S.IdEstado, '') as IdEstado, IsNull(S.IdFarmacia, '') as IdFarmacia,  
		(case when dbo.fg_SerieDisponible(F.IdentificadorSerie) = 1 Then 'SI' Else 'NO' End ) as Bloqueado, 
		(case when IsNull(S.Status, '') = '' Then 'NO' Else (case when IsNull(S.Status, '') = 'A' Then 'SI' Else 'NO' End) End) as Asignado,   
		F.AñoAprobacion, F.NumAprobacion, F.IdTipoDocumento, F.TipoDeDocumento, 		
		F.Serie, F.NombreDocumento, F.FolioInicial, F.FolioFinal, 
		F.FolioUtilizado, F.IdentificadorSerie, IsNull(S.Status, F.Status) as Status -- , Actualizado 
	From vw_FACT_CFD_Series_y_Folios F (NoLock) 
	Left Join FACT_CFD_Sucursales_Series S (NoLock) 
		On ( F.IdentificadorSerie = S.IdentificadorSerie ) 

/* 
	Select 
		F.IdEmpresa, IsNull(S.IdEstado, '') as IdEstado, IsNull(S.IdFarmacia, '') as IdFarmacia,  
		(case when dbo.fg_SerieDisponible(F.IdentificadorSerie) = 1 Then 'SI' Else 'NO' End ) as Bloqueado, 
		(case when IsNull(S.Status, '') = '' Then 'NO' Else (case when IsNull(S.Status, '') = 'A' Then 'SI' Else 'NO' End) End) as Asignado,   
		cast(F.AñoAprobacion as varchar) as AñoAprobacion, cast(F.NumAprobacion as varchar) as NumAprobacion, 
		
		cast(F.Serie as varchar) as Serie, NombreDocumento, cast(F.FolioInicial as varchar) as FolioInicial, cast(F.FolioFinal as varchar) as FolioFinal, 
		cast(F.FolioUtilizado as varchar) as FolioUtilizado, F.IdentificadorSerie, IsNull(S.Status, F.Status) as Status -- , Actualizado 
	From vw_FACT_CFD_Series_y_Folios F (NoLock) 
	Left Join FACT_CFD_Sucursales_Series S (NoLock) 
		On ( F.IdentificadorSerie = S.IdentificadorSerie ) 
*/ 

Go--#SQL 

--		select * from  vw_FACT_CFD_Sucursales_Series 

--	sp_listacolumnas vw_FACT_CFD_Sucursales_Series 
	