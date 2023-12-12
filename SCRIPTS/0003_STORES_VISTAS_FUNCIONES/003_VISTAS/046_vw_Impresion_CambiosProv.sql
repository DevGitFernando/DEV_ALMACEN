If Exists (Select Name from SysObjects (NoLock) Where Name = 'vw_Impresion_CambiosProv' And xType = 'V')
	Drop View vw_Impresion_CambiosProv
Go--#SQL

Create View vw_Impresion_CambiosProv
With Encryption 
As

    Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, L.IdSubFarmacia, E.IdPersonal, E.IdProveedor, 
        L.IdProducto, E.NombreFarmacia, E.FolioCambio, 
	    Convert(Varchar(10), Getdate(), 120) As FechaSistema, 
	    E.NombreEmpresa, E.NombreEstado, 
	    (Case When E.TipoMovto = 'SCP' Then E.FolioCambio Else '' End) As FolioCambioSalida, 
	    (Case When E.TipoMovto = 'ECP' Then E.FolioCambio else '' End) As FolioCambioEntrada, 
	    (Case When E.TipoMovto = 'SCP' Then 'Salida por Cambio de Proveedor' Else '' End) As MovtoSalida, 
	    (Case When E.TipoMovto = 'ECP' Then 'Entrada por Cambio de Provedor' Else '' End) As MovtoEntrada,
	    E.NombreProveedor, E.FechaRegistro, E.NombrePersonal, E.Observaciones, 
	    E.SubTotal, E.Iva, E.Total, 
	    D.DescripcionProducto, 
	    D.IdClaveSSA_Sal, D.ClaveSSA, D.DescripcionSal, D.ContenidoPaquete, D.Presentacion, D.CodigoEAN, 
	    D.Cantidad, D.Costo, D.TasaIva, 
	    D.Importe, L.SubFarmacia, L.ClaveLote, L.Status, L.Existencia, L.EsConsignacion, 
	    Convert(Varchar(7), L.FechaReg, 120) As FechaReg, 
	    Convert(Varchar(7), L.FechaCad, 120) As FechaCad  
    From vw_CambiosProv_Enc E (NoLock) 
    Inner Join vw_CambiosProv_Det_CodigosEAN D (NoLock) On (D.FolioCambio = E.FolioCambio) 
    Inner Join vw_CambiosProv_Det_CodigosEAN_Lotes L (NoLock) On (L.FolioCambio = E.FolioCambio)

Go--#SQL 
