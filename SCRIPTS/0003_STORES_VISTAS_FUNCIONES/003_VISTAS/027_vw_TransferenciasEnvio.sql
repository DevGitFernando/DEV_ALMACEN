----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TransferenciaEnvio_Enc' and xType = 'V' ) 
	Drop View vw_TransferenciaEnvio_Enc 
Go--#SQL

Create View vw_TransferenciaEnvio_Enc 
With Encryption 
As 

	Select Distinct 
	    IsNull(Tx.FolioTransferencia, '*') as FolioReferencia, 	
	    IsNull(Tx.FolioTransferencia, '*') as FolioReferenciaEntrada, 
	    IsNull(convert(varchar(20), Tx.FechaRegistro, 120), '' ) as FechaRegistroEntrada, 
	   
		
		T.IdEmpresa, Ce.Nombre as Empresa, 
		T.IdEstadoEnvia, E.Nombre as Estado, E.ClaveRenapo, 
		T.IdFarmaciaEnvia, F.NombreFarmacia as Farmacia, 
		T.IdAlmacenEnvia, T.EsTransferenciaAlmacen,  

		IsNull(TY.TransferenciaAplicada, 0) as TransferenciaAplicada, 
		T.FolioTransferencia as Folio, T.FolioMovtoInv, T.TipoTransferencia, T.DestinoEsAlmacen, 
		
		T.FechaTransferencia, T.FechaRegistro as FechaReg, 
		T.IdPersonal as IdPersonal, 
		T.SubTotal, T.Iva, T.Total, T.Status, 
		T.IdEstadoRecibe, Ex.Nombre as EstadoRecibe, Ex.ClaveRenapo as ClaveRenapoRecibe, 
		T.IdFarmaciaRecibe, Fx.NombreFarmacia as FarmaciaRecibe,  
		T.IdAlmacenRecibe, T.Status as StatusTransferencia 
		-- T.Keyx   
	From TransferenciasEnvioEnc T (NoLock) 
	Inner Join CatEmpresas Ce (NoLock) On ( T.IdEmpresa = Ce.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( T.IdEstadoEnvia = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstadoEnvia = F.IdEstado and T.IdFarmaciaEnvia = F.IdFarmacia ) 
	Inner Join CatEstados Ex (NoLock) On ( T.IdEstadoRecibe = Ex.IdEstado ) 
	Inner Join CatFarmacias Fx (NoLock) On ( T.IdEstadoRecibe = Fx.IdEstado and T.IdFarmaciaRecibe = Fx.IdFarmacia ) 
--	Left Join TransferenciasEnc Tx On ( T.IdEmpresa = Tx.IdEmpresa and T.IdEstadoEnvia = Tx.IdEstado and T.IdFarmaciaRecibe = Tx.IdFarmacia and 
--		T.FolioTransferencia = Tx.FolioTransferenciaRef ) 

---	Left Join TransferenciasEnc Tx On ( T.IdEmpresa = Tx.IdEmpresa and T.IdEstadoEnvia = Tx.IdEstado 
---		and T.IdFarmaciaRecibe = Tx.IdFarmacia and T.FolioTransferencia = Tx.FolioTransferenciaRef ) -- and T.TipoTransferencia = 'TS' ) 
	Left Join TransferenciasEnc Tx On ( T.IdEmpresa = Tx.IdEmpresa and T.IdEstadoEnvia = Tx.IdEstadoRecibe  
		and T.IdFarmaciaEnvia = Tx.IdFarmaciaRecibe and T.FolioTransferencia = Tx.FolioTransferenciaRef ) 

	Left Join TransferenciasEnc Ty (NoLock) On ( T.IdEmpresa = Ty.IdEmpresa and T.IdEstadoEnvia = Ty.IdEstado 
		and T.IdFarmaciaEnvia = Ty.IdFarmacia and T.FolioTransferencia = Ty.FolioTransferencia and Ty.TipoTransferencia = 'TS' )   

Go--#SQL


----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TransferenciaEnvioDet_CodigosEAN' and xType = 'V' ) 
	Drop View vw_TransferenciaEnvioDet_CodigosEAN 
Go--#SQL	

Create View vw_TransferenciaEnvioDet_CodigosEAN 
With Encryption 
As 
	Select T.IdEmpresa, T.Empresa, 
		T.IdEstadoEnvia, T.Estado, T.ClaveRenapo, T.IdFarmaciaEnvia, T.Farmacia, T.Folio, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		1 as UnidadDeSalida, D.TasaIva, D.Cant_Enviada as Cantidad, 
		D.CostoUnitario as Costo, D.SubTotal as Importe, D.ImpteIva as Iva, D.Importe as Total -- , D.Keyx as KeyxDetalle  
	From vw_TransferenciaEnvio_Enc T (NoLock) 
	Inner Join TransferenciasEnvioDet D (NoLock) On ( T.IdEmpresa = D.IdEmpresa and T.IdEstadoEnvia = D.IdEstadoEnvia and T.IdFarmaciaEnvia = D.IdFarmaciaEnvia and 
		T.Folio = D.FolioTransferencia ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL


----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_TransferenciaEnvioDet_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_TransferenciaEnvioDet_CodigosEAN_Lotes 
Go--#SQL	

Create View vw_TransferenciaEnvioDet_CodigosEAN_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, 
		E.IdEstadoEnvia, E.Estado, E.ClaveRenapo, E.IdFarmaciaEnvia, E.Farmacia, 
		D.IdSubFarmaciaEnvia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstadoEnvia, D.IdFarmaciaEnvia, D.IdSubFarmaciaEnvia) as SubFarmaciaEnvia, 	
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, cast(D.CantidadEnviada as int) as Cantidad, D.CostoUnitario -- , D.Keyx as KeyxDetalleLote 
	From vw_TransferenciaEnvioDet_CodigosEAN E (NoLock) 
	Inner Join TransferenciasEnvioDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstadoEnvia = D.IdEstadoEnvia and E.IdFarmaciaEnvia = D.IdFarmaciaEnvia and E.Folio = D.FolioTransferencia and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join TransferenciasEnvioDet_LotesRegistrar L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstadoEnvia = L.IdEstadoEnvia and D.IdFarmaciaEnvia = L.IdFarmaciaEnvia 
			 and D.IdSubFarmaciaEnvia = L.IdSubFarmaciaEnvia and E.Folio = L.FolioTransferencia and 
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
			 			 
	-- Where D.Status = 'A' 
Go--#SQL	
 	

/* 

	sp_helptext vw_TransferenciaEnvioDet_CodigosEAN_Lotes 

	select * from TransferenciasEnvioDet_Lotes

	select * from TransferenciasEnvioEnc
	Select * from vw_TransferenciaEnvio_Enc 
	select * from vw_TransferenciaEnvioDet_CodigosEAN 
	select * from vw_TransferenciaEnvioDet_CodigosEAN_Lotes 
	
*/ 
	