If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionDeDoctosDetTrans' and xType = 'V' ) 
	Drop View vw_DevolucionDeDoctosDetTrans 
Go--#SQL

Create View vw_DevolucionDeDoctosDetTrans 
With Encryption 
As 

		Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, E.IdVehiculo,
			E.Vehiculo, E.IdPersonal, E.Chofer, E.Observaciones, Convert(Varchar(10), E.FechaRegistro, 120) As FechaRuta,
			D.FolioTransferenciaVenta, Convert(Varchar(10), T.FechaTransferencia, 120) As Fecha, 
       (T.IdFarmaciaRecibe + ' - ' + T.FarmaciaRecibe) As FarmaciaRecibe, FolioDevuelto, Sum(Cantidad) As Piezas, Sum(Importe) As Importe
		From vw_RutasDistribucionEnc E (NoLock)
		Inner Join RutasDistribucionDet D (NoLock)
			On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio)
		--Inner Join vw_TransferenciasDet_CodigosEAN C (NoLock)
		--	On (D.IdEmpresa = C.IdEmpresa And D.IdEstado = T.IdEstado And D.IdFarmacia = T.IdFarmacia And ('TS' + D.FolioTransferenciaVenta) = T.Folio)
		Inner Join vw_TransferenciasDet_CodigosEAN T (NoLock)
			On (D.IdEmpresa = T.IdEmpresa And D.IdEstado = T.IdEstado And D.IdFarmacia = T.IdFarmacia And ('TS' + D.FolioTransferenciaVenta) = T.Folio)
		Where Tipo = 'T'
		Group By E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, E.IdVehiculo,
			E.Vehiculo, E.IdPersonal, E.Chofer, E.Observaciones, Convert(Varchar(10), E.FechaRegistro, 120),
			D.FolioTransferenciaVenta, Convert(Varchar(10), T.FechaTransferencia, 120), 
       (T.IdFarmaciaRecibe + ' - ' + T.FarmaciaRecibe), FolioDevuelto


Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_DevolucionDeDoctosDetVentas' and xType = 'V' ) 
	Drop View vw_DevolucionDeDoctosDetVentas 
Go--#SQL

Create View vw_DevolucionDeDoctosDetVentas 
With Encryption 
As 

		Select
			E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, E.IdVehiculo,
			E.Vehiculo, E.IdPersonal, E.Chofer, E.Observaciones, Convert(Varchar(10), E.FechaRegistro, 120) As FechaRuta,
			--Convert(Varchar(10), EV.FechaRegistro, 120) As FechaPedido,
			D.FolioTransferenciaVenta, Convert(Varchar(10), EV.FechaRegistro, 120) As Fecha,
			Beneficiario, FolioDevuelto, Sum(CantidadVendida) As Piezas, Sum((PrecioUnitario * CantidadVendida)) As Importe
		From vw_RutasDistribucionEnc E (NoLock)
		Inner Join RutasDistribucionDet D (NoLock)
			On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio)
		Inner Join VentasEnc EV (NoLock)
			On (D.IdEmpresa = EV.IdEmpresa And D.IdEstado = EV.IdEstado And D.IdFarmacia = EV.IdFarmacia And D.FolioTransferenciaVenta = EV.FolioVenta)
		Inner Join VentasDet DV (NoLock)
			On (D.IdEmpresa = DV.IdEmpresa And D.IdEstado = DV.IdEstado And D.IdFarmacia = DV.IdFarmacia And D.FolioTransferenciaVenta = DV.FolioVenta)
		Inner Join vw_VentasDispensacion_InformacionAdicional A (NoLock)
			On (E.IdEmpresa = A.IdEmpresa And E.IdEstado = A.IdEstado And E.IdFarmacia = A.IdFarmacia And EV.FolioVenta = A.Folio
				And EV.IdCliente = A.IdCliente And EV.IdSubCliente = A.IdSubCliente)
		Where Tipo = 'V' --And E.Folio = '00000002'
		Group By E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, E.IdVehiculo,
			E.Vehiculo, E.IdPersonal, E.Chofer, E.Observaciones, Convert(Varchar(10), E.FechaRegistro, 120),
			D.FolioTransferenciaVenta, Convert(Varchar(10), EV.FechaRegistro, 120), Beneficiario, FolioDevuelto

Go--#SQL