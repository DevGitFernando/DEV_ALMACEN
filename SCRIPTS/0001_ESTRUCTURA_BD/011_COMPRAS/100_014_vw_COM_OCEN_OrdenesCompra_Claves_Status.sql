If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_OCEN_OrdenesCompra_Claves_Status' and xType = 'V' ) 
   Drop View vw_COM_OCEN_OrdenesCompra_Claves_Status 
Go--#SQL

Create View vw_COM_OCEN_OrdenesCompra_Claves_Status
With Encryption 
As 
	Select S.IdEmpresa, E.Empresa, S.IdEstado, E.Estado, S.IdFarmacia, E.Farmacia, S.FolioOrden, E.IdProveedor, E.Proveedor,
		S.IdStatus, C.Nombre, PermiteDescarga, ModificaCompras 
    From COM_OCEN_OrdenesCompra_Claves_Status S
    Inner Join  vw_OrdenesCompras_Claves_Enc  E (NoLock)
       On (S.IdEmpresa = E.IdEmpresa And S.IdEstado = E.IdEstado And S.IdFarmacia = E.IdFarmacia And S.FolioOrden = E.Folio)
    Inner Join Cat_StatusDeOrdenesDeCompras C (NoLock) On (S.IdStatus = C.IdStatus)

Go--#SQL 	 	
	