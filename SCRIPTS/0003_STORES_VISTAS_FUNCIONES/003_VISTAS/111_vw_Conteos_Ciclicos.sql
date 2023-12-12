


--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Inv_ConteosCiclicosEnc' and xType = 'V' ) 
	Drop View vw_Inv_ConteosCiclicosEnc
Go--#SQL

Create View vw_Inv_ConteosCiclicosEnc
With Encryption 
As 
	Select R.IdEmpresa, E.Empresa, R.IdEstado, F.Estado, R.IdFarmacia, F.Farmacia, R.FolioConteo as Folio, 
	R.IdPersonal, vP.NombreCompleto as Personal, R.FechaRegistro, R.Status 
	From Inv_ConteosCiclicosEnc R (NoLock) 
	Inner Join vw_Empresas E (Nolock) On ( E.IdEmpresa = R.IdEmpresa )
	Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia )
	Inner Join vw_Personal vP (Nolock) On ( R.IdEstado = vP.IdEstado and R.IdFarmacia = vP.IdFarmacia and R.IdPersonal = vP.IdPersonal )		
	
Go--#SQL


--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Inv_ConteosCiclicosDet' and xType = 'V' ) 
	Drop View vw_Inv_ConteosCiclicosDet
Go--#SQL

Create View vw_Inv_ConteosCiclicosDet
With Encryption 
As 
	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio,
	E.IdPersonal, E.Personal, E.FechaRegistro, 
	S.IdClaveSSA_Sal, D.ClaveSSA, S.DescripcionSal, S.DescripcionCortaClave, S.IdPresentacion, S.Presentacion, S.ContenidoPaquete,
	D.Cantidad, D.Total_Piezas, D.Participacion, D.Categoria, D.Status  
	From vw_Inv_ConteosCiclicosEnc E (NoLock) 
	Inner Join Inv_ConteosCiclicosDet D (Nolock)
		On ( D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.FolioConteo = E.Folio )
	Inner Join vw_ClavesSSA_Sales S On ( S.ClaveSSA = D.ClaveSSA )		
	
Go--#SQL


--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Inv_ConteosCiclicos_Claves' and xType = 'V' ) 
	Drop View vw_Inv_ConteosCiclicos_Claves
Go--#SQL

Create View vw_Inv_ConteosCiclicos_Claves
With Encryption 
As 
	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, E.IdPersonal, E.Personal, 	
	S.IdClaveSSA_Sal, D.ClaveSSA, S.DescripcionSal, S.DescripcionCortaClave, S.IdPresentacion, S.Presentacion, S.ContenidoPaquete,
	Convert(varchar(10), D.FechaRegistro, 120) as FechaRegistro, D.Categoria, D.Status  
	From vw_Inv_ConteosCiclicosEnc E (NoLock) 
	Inner Join Inv_ConteosCiclicos_Claves D (Nolock)
		On ( D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.FolioConteo = E.Folio )
	Inner Join vw_ClavesSSA_Sales S On ( S.ClaveSSA = D.ClaveSSA )		
	
Go--#SQL


		