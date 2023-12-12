


--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_Adt_Rechazos_Enc' and xType = 'V' ) 
	Drop View vw_COM_Adt_Rechazos_Enc
Go--#SQL

Create View vw_COM_Adt_Rechazos_Enc
With Encryption 
As 
	Select R.IdEmpresa, E.Empresa, R.IdEstado, F.Estado, R.IdFarmacia, F.Farmacia, R.FolioRechazo as Folio, 
	R.IdPersonal, vP.NombreCompleto as Personal,
	R.FechaRegistro, R.NombreRecibeRechazo as RecibeRechazo, R.Observaciones,
	R.TipoProceso, Case When R.TipoProceso = 1 Then 'RESURTIDO'
					when R.TipoProceso = 2 Then 'NOTA DE CREDITO' else '' End As TipoProcesoDesc, R.FechaResurtido,  
	R.FolioOrden, C.IdProveedor, P.Nombre as Proveedor, R.Status 
	From COM_Adt_Rechazos_Enc R (NoLock) 
	Inner Join COM_OCEN_OrdenesCompra_Claves_Enc C (Nolock)
		On ( C.IdEmpresa = R.IdEmpresa and C.EstadoEntrega = R.IdEstado and C.EntregarEn = R.IdFarmacia and C.FolioOrden = R.FolioOrden )
	Inner Join vw_Empresas E (Nolock) On ( E.IdEmpresa = R.IdEmpresa )
	Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia )
	Inner Join CatProveedores P (Nolock) On ( P.IdProveedor = C.IdProveedor )
	Inner Join vw_Personal vP (Nolock) On ( R.IdEstado = vP.IdEstado and R.IdFarmacia = vP.IdFarmacia and R.IdPersonal = vP.IdPersonal )		
	
Go--#SQL



--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_Adt_Rechazos_Det' and xType = 'V' ) 
	Drop View vw_COM_Adt_Rechazos_Det
Go--#SQL

Create View vw_COM_Adt_Rechazos_Det
With Encryption 
As 
	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio,
	E.IdPersonal, E.Personal, E.FechaRegistro, E.RecibeRechazo, E.Observaciones,
	E.TipoProceso, E.TipoProcesoDesc, E.FechaResurtido, 
	E.FolioOrden, E.IdProveedor, E.Proveedor,
	D.IdRechazo, R.Descripcion as Rechazo, D.Status  
	From vw_COM_Adt_Rechazos_Enc E (NoLock) 
	Inner Join COM_Adt_Rechazos_Det D (Nolock)
		On ( D.IdEmpresa = E.IdEmpresa and D.IdEstado = E.IdEstado and D.IdFarmacia = E.IdFarmacia and D.FolioRechazo = E.Folio )
	Inner Join COM_Cat_Rechazos R (Nolock) On ( R.IdRechazo = D.IdRechazo )
			
	
Go--#SQL


		