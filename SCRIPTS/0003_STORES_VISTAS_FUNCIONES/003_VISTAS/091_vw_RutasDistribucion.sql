------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_RutasDistribucionEnc' and xType = 'V' ) 
	Drop View vw_RutasDistribucionEnc 
Go--#SQL

Create View vw_RutasDistribucionEnc 
With Encryption 
As 


		Select E.IdEmpresa, M.Nombre As Empresa, E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, E.Folio, E.IdVehiculo,
			V.Descripcion As Vehiculo, E.IdPersonal, P.Nombre As Chofer, E.Observaciones, E.FechaRegistro, E.Status,
			E.Firmado, Case When E.Firmado = 1 Then 'FIRMADO' Else 'SIN FIRMAR' End As FirmadoDesc,
			E.IdPersonalCaptura, vP.NombreCompleto as PersonalCaptura
		From RutasDistribucionEnc E (NoLock)
		Inner Join CatEmpresas M (NoLock) On (E.IdEmpresa = M.IdEmpresa)
		Inner Join vw_Farmacias F (NoLock) On (E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia)
		Inner Join CatVehiculos V (NoLock) On (E.IdEstado = V.IdEstado And E.IdFarmacia = V.IdFarmacia And E.IdVehiculo = V.IdVehiculo)
		Inner Join CatPersonalCEDIS P (NoLock)
			On(E.IdEmpresa = P.IdEmpresa And E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.IdPersonal = P.IdPersonal)
		Inner Join vw_Personal vP (NoLock)
			On( E.IdEstado = vP.IdEstado And E.IdFarmacia = vP.IdFarmacia And E.IdPersonalCaptura = vP.IdPersonal)

Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_RutasDistribucionDetTrans' and xType = 'V' ) 
	Drop View vw_RutasDistribucionDetTrans 
Go--#SQL

Create View vw_RutasDistribucionDetTrans 
With Encryption 
As 

		Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, IsNull(P.FolioPedido, '000000') As FolioPedido, E.IdVehiculo,
			E.Vehiculo, E.IdPersonal, E.Chofer, 
			E.IdPersonalCaptura, E.PersonalCaptura,
			E.Observaciones, E.FechaRegistro,
			Convert(Varchar(10), IsNull(PE.FechaRegistro, T.FechaTransferencia), 120) As FechaPedido,
			D.FolioTransferenciaVenta, Convert(Varchar(10), T.FechaTransferencia, 120) As Fecha,
		T.IdFarmaciaRecibe, (T.IdFarmaciaRecibe + ' - ' + T.FarmaciaRecibe) As FarmaciaRecibe, D.Bultos, Sum(Cantidad) As Piezas, E.Status, D.Status As StatusDet
		From vw_RutasDistribucionEnc E (NoLock)
		Inner Join RutasDistribucionDet D (NoLock)
			On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio)
		Inner Join vw_TransferenciasDet_CodigosEAN T (NoLock)
			On (D.IdEmpresa = T.IdEmpresa And D.IdEstado = T.IdEstado And D.IdFarmacia = T.IdFarmacia And ('TS' + D.FolioTransferenciaVenta) = T.Folio)
		Left Join Pedidos_Cedis_Enc_Surtido P (NoLock)
			On (D.IdEmpresa = P.IdEmpresa And D.IdEstado = P.IdEstado And D.IdFarmacia = P.IdFarmacia  And ('TS' + D.FolioTransferenciaVenta) = P.FolioTransferenciaReferencia)
		Left Join Pedidos_Cedis_Enc PE (NoLock)
			On (P.IdEmpresa = PE.IdEmpresa And P.IdEstado = PE.IdEstado And P.IdFarmaciaPedido = PE.IdFarmacia And P.FolioPedido = PE.FolioPedido)
		Where Tipo = 'T' 
		Group By E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, P.FolioPedido, E.IdVehiculo,
			E.Vehiculo, E.IdPersonal, E.Chofer, E.IdPersonalCaptura, E.PersonalCaptura, E.Observaciones, E.FechaRegistro, PE.FechaRegistro, D.FolioTransferenciaVenta,
			T.FechaTransferencia, T.IdFarmaciaRecibe, T.FarmaciaRecibe, D.Bultos, E.Status, D.Status

Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_RutasDistribucionDetVentas' and xType = 'V' ) 
	Drop View vw_RutasDistribucionDetVentas 
Go--#SQL

Create View vw_RutasDistribucionDetVentas 
With Encryption 
As 

		Select
			E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, IsNull(P.FolioPedido, '000000') As FolioPedido, E.IdVehiculo,
			E.Vehiculo, E.IdPersonal, E.Chofer, E.IdPersonalCaptura, E.PersonalCaptura,
			E.Observaciones, E.FechaRegistro,
			Convert(Varchar(10), IsNull(PE.FechaRegistro, EV.FechaRegistro), 120) As FechaPedido,
			D.FolioTransferenciaVenta, Convert(Varchar(10), EV.FechaRegistro, 120) As Fecha,
			A.IdBeneficiario, A.Beneficiario, A.Domicilio, D.Bultos, Sum(CantidadVendida) As Piezas, E.Status, D.Status As StatusDet
		From vw_RutasDistribucionEnc E (NoLock)
		Inner Join RutasDistribucionDet D (NoLock)
			On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio)
		Inner Join VentasEnc EV (NoLock)
			On (D.IdEmpresa = EV.IdEmpresa And D.IdEstado = EV.IdEstado And D.IdFarmacia = EV.IdFarmacia And D.FolioTransferenciaVenta = EV.FolioVenta)
		Inner Join VentasDet DV (NoLock)
			On (EV.IdEmpresa = DV.IdEmpresa And EV.IdEstado = DV.IdEstado And EV.IdFarmacia = DV.IdFarmacia And EV.FolioVenta = DV.FolioVenta)
		Inner Join vw_VentasDispensacion_InformacionAdicional A (NoLock)
			On (E.IdEmpresa = A.IdEmpresa And E.IdEstado = A.IdEstado And E.IdFarmacia = A.IdFarmacia And EV.FolioVenta = A.Folio
				And EV.IdCliente = A.IdCliente And EV.IdSubCliente = A.IdSubCliente)
		Left Join Pedidos_Cedis_Enc_Surtido P (NoLock)
			On (D.IdEmpresa = P.IdEmpresa And D.IdEstado = P.IdEstado And D.IdFarmacia = P.IdFarmacia  And ('SV' + D.FolioTransferenciaVenta) = P.FolioTransferenciaReferencia)
		Left Join Pedidos_Cedis_Enc PE (NoLock)
			On (P.IdEmpresa = PE.IdEmpresa And P.IdEstado = PE.IdEstado And P.IdFarmaciaPedido = PE.IdFarmacia And P.FolioPedido = PE.FolioPedido)
		Where Tipo = 'V' --And E.Folio = '00000002'
		Group By E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio, P.FolioPedido, E.IdVehiculo,
			E.Vehiculo, E.IdPersonal, E.Chofer, E.IdPersonalCaptura, E.PersonalCaptura, E.Observaciones, E.FechaRegistro, PE.FechaRegistro, 
			D.FolioTransferenciaVenta,
			EV.FechaRegistro, A.IdBeneficiario, A.Beneficiario, A.Domicilio, D.Bultos, E.Status, D.Status

Go--#SQL