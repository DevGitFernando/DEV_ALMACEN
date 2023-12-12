If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TransferenciasEnc' and xType = 'V' ) 
	Drop View vw_TransferenciasEnc 
Go--#SQL 	

Create View vw_TransferenciasEnc 
With Encryption 
As 
	Select -- IsNull(Tx.FolioTransferenciaRef, '*') as FolioReferencia,
	    T.IdEmpresa, Ce.Nombre as Empresa, 
		T.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		T.IdFarmacia, F.NombreFarmacia as Farmacia,
		VF.IdJurisdiccion, VF.Jurisdiccion, VF.EsAlmacen,
		VF.Municipio, VF.Colonia, VF.Domicilio, VF.CodigoPostal,
		T.IdAlmacen, T.EsTransferenciaAlmacen,  
		T.FolioTransferencia as Folio, T.FolioTransferenciaRef,  
		T.FolioMovtoInv, T.TipoTransferencia, T.DestinoEsAlmacen, 
		T.FechaTransferencia, T.FechaRegistro as FechaReg, 
		T.IdPersonal as IdPersonal, 
		vP.NombreCompleto as NombrePersonal, 
		T.Observaciones, 
		T.SubTotal, T.Iva, T.Total, T.Status, 
		T.IdEstadoRecibe, Ex.Nombre as EstadoRecibe, Ex.ClaveRenapo as ClaveRenapoRecibe, 
		T.IdFarmaciaRecibe, Fx.NombreFarmacia as FarmaciaRecibe,
		wVF.IdJurisdiccion As IdJurisdiccionRecibe, wVF.Jurisdiccion As JurisdiccionRecibe, wVF.EsAlmacen As EsAlmacenRecibe,
		wVF.Municipio As MunicipioRecibe, wVF.Colonia As ColoniaRecibe, wVF.Domicilio As DomicilioRecibe, wVF.CodigoPostal As CodigoPostalRecibe,   
		T.IdAlmacenRecibe, T.Status as StatusTransferencia, 
		-- T.Keyx
		T.TransferenciaAplicada, T.IdPersonalRegistra, PR.NombreCompleto as NombrePersonalRegistra, T.FechaAplicada   
	From TransferenciasEnc T (NoLock) 
	Inner Join CatEmpresas Ce (NoLock) On ( T.IdEmpresa = Ce.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia ) 
	Inner Join CatEstados Ex (NoLock) On ( T.IdEstadoRecibe = Ex.IdEstado ) 
	Inner Join CatFarmacias Fx (NoLock) On ( T.IdEstadoRecibe = Fx.IdEstado and T.IdFarmaciaRecibe = Fx.IdFarmacia ) 
	Inner Join vw_Personal vP (NoLock) On ( T.IdEstado = vP.IdEstado and T.IdFarmacia = vP.IdFarmacia and T.IdPersonal = vP.IdPersonal )
	Inner Join vw_Farmacias VF (Nolock) On ( T.IdEstado = VF.IdEstado and T.IdFarmacia = VF.IdFarmacia )
	Inner Join vw_Farmacias wVF (Nolock) On	( T.IdEstadoRecibe = wVF.IdEstado and T.IdFarmaciaRecibe = wVF.IdFarmacia )
	Inner Join vw_Personal PR (NoLock) On ( T.IdEstado = PR.IdEstado and T.IdFarmacia = PR.IdFarmacia and T.IdPersonalRegistra = PR.IdPersonal )
Go--#SQL


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TransferenciasDet_CodigosEAN' and xType = 'V' ) 
	Drop View vw_TransferenciasDet_CodigosEAN 
Go--#SQL	

Create View vw_TransferenciasDet_CodigosEAN 
With Encryption 
As 
	Select T.IdEmpresa, T.Empresa, T.IdEstado, T.Estado, T.ClaveRenapo, T.IdFarmacia, T.Farmacia, T.Folio,
		T.FechaTransferencia, T.IdFarmaciaRecibe, T.FarmaciaRecibe, T.TransferenciaAplicada, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		1 as UnidadDeSalida, D.TasaIva, D.Cant_Enviada as Cantidad, 
		D.CostoUnitario as Costo, D.SubTotal as Importe, D.ImpteIva as Iva, D.Importe as Total -- , D.Keyx as KeyxDetalle  
	From vw_TransferenciasEnc T (NoLock) 
	Inner Join TransferenciasDet D (NoLock) On ( T.IdEmpresa = D.IdEmpresa and T.IdEstado = D.IdEstado and T.IdFarmacia = D.IdFarmacia and T.Folio = D.FolioTransferencia ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TransferenciaDet_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_TransferenciaDet_CodigosEAN_Lotes 
Go--#SQL	

Create View vw_TransferenciaDet_CodigosEAN_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		D.IdSubFarmaciaEnvia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmaciaEnvia) as SubFarmaciaEnvia, 
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, cast(D.CantidadEnviada as int) as Cantidad -- , D.Keyx as KeyxDetalleLote 
	From vw_TransferenciasDet_CodigosEAN E (NoLock) 
	Inner Join TransferenciasDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioTransferencia and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmaciaEnvia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
Go--#SQL	
