---------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name from SysObjects (NoLock) Where Name = 'vw_Impresion_CambiosCarta' And xType = 'V')
	Drop View vw_Impresion_CambiosCarta
Go--#SQL

Create View vw_Impresion_CambiosCarta
With Encryption 
As

    Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, L.IdSubFarmacia, E.IdPersonal, E.FolioCarta, E.Tipo, E.FolioTransferenciaVenta ,E.TipoMovtoInv, 
        L.IdProducto, E.NombreFarmacia, E.FolioCambio, 
	    Convert(Varchar(10), Getdate(), 120) As FechaSistema, 
	    E.NombreEmpresa, E.NombreEstado, 
	    (Case When E.TipoMovtoInv = 'SCP' Then E.FolioCambio Else '' End) As FolioCambioSalida, 
	    (Case When E.TipoMovtoInv = 'ECP' Then E.FolioCambio else '' End) As FolioCambioEntrada, 
	    (Case When E.TipoMovtoInv = 'SCP' Then 'Salida por Cambio de Proveedor' Else '' End) As MovtoSalida, 
	    (Case When E.TipoMovtoInv = 'ECP' Then 'Entrada por Cambio de Provedor' Else '' End) As MovtoEntrada,
	    E.FechaRegistro, E.NombrePersonal, E.Observaciones, 
	    E.SubTotal, E.Iva, E.Total, 
	    D.DescripcionProducto, 
	    D.IdClaveSSA_Sal, D.ClaveSSA, D.DescripcionSal, D.ContenidoPaquete, D.Presentacion, D.CodigoEAN, 
	    L.Cantidad, D.Costo, D.TasaIva, 
	    --D.Importe, 
		(L.Cantidad * D.Costo) as Importe, 
		L.SubFarmacia, L.ClaveLote, L.Status, L.Existencia, L.EsConsignacion, 
	    Convert(Varchar(7), L.FechaReg, 120) As FechaReg, 
	    Convert(Varchar(7), L.FechaCad, 120) As FechaCad  
    From vw_CambiosCarta_Enc E (NoLock) 
    Inner Join vw_CambiosCarta_Det_CodigosEAN D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioCambio = D.FolioCambio ) 
    Inner Join vw_CambiosCarta_Det_CodigosEAN_Lotes L (NoLock) On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.FolioCambio = L.FolioCambio  )

Go--#SQL 
