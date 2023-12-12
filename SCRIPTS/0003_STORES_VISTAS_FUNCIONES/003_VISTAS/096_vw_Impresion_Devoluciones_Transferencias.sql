


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Devoluciones_Transferencias' and xType = 'V' ) 
	Drop View vw_Impresion_Devoluciones_Transferencias 
Go--#SQL	

Create View vw_Impresion_Devoluciones_Transferencias 
With Encryption 
As 
	Select -- L.*, vP.*  
		   TE.IdEmpresa, TE.Empresa, TE.IdEstado, TE.Estado, TE.ClaveRenapo, TE.IdFarmacia, TE.Farmacia, 
		   L.IdSubFarmacia AS IdSubFarmaciaEnvia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmaciaEnvia,
		   TE.IdJurisdiccion, TE.Jurisdiccion, 		   
		   E.FolioDevolucion as Folio, E.FolioTransferencia, E.FolioTransferenciaRef, E.TipoTransferencia, 
		   TE.TransferenciaAplicada, 
		   E.FechaRegistro as  FechaSistema, E.FechaRegistro as FechaReg,		    
		   E.IdPersonal, (P.Nombre + ' ' + ApPaterno + ' ' + ApMaterno) as NombrePersonal, 
		   E.Observaciones, E.IdMotivo, M.Descripcion as Motivo, 
		   TE.IdEstadoRecibe, TE.EstadoRecibe, TE.ClaveRenapoRecibe, TE.IdFarmaciaRecibe, TE.FarmaciaRecibe, 	
		   L.IdSubFarmacia as IdSubFarmaciaRecibe, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmaciaRecibe,
		   TE.IdJurisdiccionRecibe, TE.JurisdiccionRecibe, 		   		   	   
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.CodigoEAN, -- D.Cantidad, D.Costo, D.Importe,
		   vP.IdSegmento, vP.Segmento, 
		   L.ClaveLote, L.CantidadEnviada as CantidadLote, F.FechaCaducidad, F.FechaRegistro  
	From DevolucionTransferenciasEnc E (NoLock) 
	Inner join vw_TransferenciasEnc TE (NoLock) 
		On ( E.IdEmpresa = TE.IdEmpresa and E.IdEstado = TE.IdEstado and E.IdFarmacia = TE.IdFarmacia and TE.Folio = E.FolioTransferencia )
	Inner Join DevolucionTransferenciasDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioDevolucion = D.FolioDevolucion ) 
	Inner Join DevolucionTransferenciasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioDevolucion = L.FolioDevolucion 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN )
	Inner Join CatPersonal P (Nolock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonal = P.IdPersonal ) 
	Inner Join CatMotivos_Dev_Transferencia M (Nolock) On ( E.IdMotivo = M.IdMotivo )
Go--#SQL


