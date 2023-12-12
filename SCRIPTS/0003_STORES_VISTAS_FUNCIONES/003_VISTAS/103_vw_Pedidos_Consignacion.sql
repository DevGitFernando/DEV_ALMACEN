------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PedidosEnc_Consignacion' and xType = 'V' ) 
   Drop View vw_PedidosEnc_Consignacion 
Go--#SQL  

Create View vw_PedidosEnc_Consignacion
With Encryption 
As 
	Select P.IdEmpresa, E.Nombre As Empresa, P.IdEstado, F.Estado, P.IdFarmacia, F.Farmacia, P.Folio,
		P.IdPersonal, R.NombreCompleto As Personal, P.FechaRegistro, P.FechaPedido, P.FechaEntrega,
		P.IdProveedor, V.Nombre As Proveedor, SF.IdSubFarmacia, P.ReferenciaPedido, P.Observaciones, P.Status
	From PedidosEnc_Consignacion P (NoLock) 
	Inner Join CatEmpresas E (NoLock) On ( P.IdEmpresa = E.IdEmpresa ) 
	Inner Join vw_Farmacias F (NoLock) On ( P.IdEstado = F.IdEstado And P.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Personal R (Nolock) On ( P.IdEstado = R.IdEstado And P.IdFarmacia = R.IdFarmacia And P.IdPersonal = R.IdPersonal ) 
	Inner Join CatProveedores V (Nolock) On ( P.IdProveedor = V.IdProveedor )
	Inner Join CFG_CNSGN_Proveedores_SubFarmacias SF (NoLock) On ( P.IdEstado = SF.IdEstado and P.IdProveedor = SF.IdProveedor ) 

Go--#SQL 	 	
	

------------------------------------------------------------------------------------------------------------------ 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PedidosDet_Consignacion' and xType = 'V' ) 
   Drop View vw_PedidosDet_Consignacion 
Go--#SQL

Create View vw_PedidosDet_Consignacion
With Encryption 
As 
	Select P.IdEmpresa, E.Nombre As Empresa, P.IdEstado, F.Estado, P.IdFarmacia, F.Farmacia, P.Folio,
		P.IdClaveSSA, C.ClaveSSA As ClaveSSA_Sist, P.ClaveSSA, P.DescripcionClaveSSA, P.Costo, P.Cantidad, P.Iva, P.Status
	From PedidosDet_Consignacion P (NoLock)
	Inner Join CatEmpresas E (NoLock) On (P.IdEmpresa = E.IdEmpresa)
	Inner Join vw_Farmacias F (NoLock) On (P.IdEstado = F.IdEstado And P.IdFarmacia = F.IdFarmacia)
	Inner Join vw_ClavesSSA_Sales C (NoLock) On (P.IdClaveSSA = C.IdClaveSSA_Sal)

Go--#SQL   


------------------------------------------------------------------------------------------------------------------ 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Pedidos_Consignacion' and xType = 'V' ) 
   Drop View vw_Impresion_Pedidos_Consignacion 
Go--#SQL

Create View vw_Impresion_Pedidos_Consignacion
With Encryption 
As 
	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio,
		E.IdPersonal, E.Personal, Convert(Varchar(10), E.FechaRegistro, 120) As FechaRegistro,
		Convert(Varchar(10), E.FechaPedido, 120) As FechaFormalizacion, Convert(Varchar(10), E.FechaEntrega, 120)  As FechaEntrega,
		E.IdProveedor, E.Proveedor, E.ReferenciaPedido, E.Observaciones,
		D.IdClaveSSA, D.ClaveSSA_Sist, D.ClaveSSA, D.DescripcionClaveSSA, D.Costo, D.Cantidad, D.Iva,
		(D.Costo * D.Cantidad) As Importe, ((D.Costo * D.Cantidad) + D.Iva) As Total ,
		E.Status
	From vw_PedidosEnc_Consignacion E (NoLock)
	Inner Join vw_PedidosDet_Consignacion D (NoLock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio)

Go--#SQL  

