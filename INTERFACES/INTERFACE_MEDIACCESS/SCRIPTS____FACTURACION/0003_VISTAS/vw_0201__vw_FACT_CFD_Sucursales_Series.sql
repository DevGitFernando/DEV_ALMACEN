-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_CFD_Sucursales_Series' and xType = 'V' ) 
	Drop View vw_FACT_CFD_Sucursales_Series 
Go--#SQL 
 	
Create View vw_FACT_CFD_Sucursales_Series 
With Encryption 
As 

	Select 
		F.IdEmpresa, F.IdEstado, F.IdFarmacia,  
		0 as Bloqueado, 
		(Case When F.Status = 'A' Then 'SI' Else 'NO' End) as Asignado,   
		0 as AñoAprobacion, 0 as NumAprobacion, 
		F.IdTipoDocumento, T.NombreDocumento as TipoDeDocumento, T.NombreDocumento as NombreDocumento, 		
		T.TipoDeComprobante, F.Serie, F.FolioInicial, F.FolioFinal, 
		F.FolioUtilizado, F.IdentificadorSerie, F.Status -- , Actualizado 
	From CFDI_Emisores_SeriesFolios F (NoLock) 
	Inner Join CFDI_TiposDeDocumentos T (NoLock) On ( F.IdTipoDocumento = T.IdTipoDocumento ) 



Go--#SQL 

--		select * from  vw_FACT_CFD_Sucursales_Series 

--		select * from  CFDI_TiposDeDocumentos 


--	sp_listacolumnas vw_FACT_CFD_Sucursales_Series 

-------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_CFD_Sucursales_Series__Informacion' and xType = 'V' ) 
	Drop View vw_FACT_CFD_Sucursales_Series__Informacion 
Go--#SQL 
 	
Create View vw_FACT_CFD_Sucursales_Series__Informacion 
With Encryption 
As 

	Select 
		F.IdEmpresa, E.Nombre as Empresa, 
		F.IdEstado, S.Estado, F.IdFarmacia, S.Farmacia, 
		F.IdTipoDocumento, T.NombreDocumento as TipoDeDocumento,  
		T.TipoDeComprobante, F.Serie, 
		(Case When F.Status = 'A' Then 'SI' Else 'NO' End) as Asignado,   
		F.FolioUtilizado, F.Status -- , Actualizado 
	From CFDI_Emisores_SeriesFolios F (NoLock) 
	Inner Join CatEmpresas E (NoLock) On ( F.IdEmpresa = E.IdEmpresa ) 
	Inner Join vw_Farmacias S (NoLock) On ( F.IdEstado = S.IdEstado and F.IdFarmacia = S.IdFarmacia ) 
	Inner Join CFDI_TiposDeDocumentos T (NoLock) On ( F.IdTipoDocumento = T.IdTipoDocumento ) 



Go--#SQL 
	