If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Transferencias' and xType = 'V' ) 
	Drop View vw_Impresion_Transferencias 
Go--#SQL	

Create View vw_Impresion_Transferencias 
With Encryption 
As 
	Select -- L.*, vP.*  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmaciaEnvia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmaciaEnvia) as SubFarmaciaEnvia,
		   E.IdJurisdiccion, E.Jurisdiccion,
		   E.Municipio, E.Colonia, E.Domicilio, E.CodigoPostal,		   
		   E.Folio, E.TipoTransferencia, 
		   E.TransferenciaAplicada, 
		   E.FechaTransferencia as  FechaSistema, E.FechaReg, 
		   -- E.Referencia, E.MovtoAplicado, 
		   E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.IdEstadoRecibe, E.EstadoRecibe, E.ClaveRenapoRecibe, E.IdFarmaciaRecibe, E.FarmaciaRecibe, 	
		   L.IdSubFarmaciaRecibe, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmaciaRecibe) as SubFarmaciaRecibe,
		   E.IdJurisdiccionRecibe, E.JurisdiccionRecibe,
		   E.MunicipioRecibe, E.ColoniaRecibe, E.DomicilioRecibe, E.CodigoPostalRecibe,		   		   	   
		   E.SubTotal, E.Iva, E.Total, E.Status, -- E.Keyx as KeyEncabezado, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, 
		   D.CodigoEAN, -- D.Cantidad, D.Costo, D.Importe,
		   vP.IdSegmento, vP.Segmento, vP.EsAntibiotico, vP.EsControlado,
		   Case When ( vP.EsAntibiotico = 0 and vP.EsControlado = 1 ) Then 1
				When ( vP.EsAntibiotico = 1 and vP.EsControlado = 0 ) Then 2 
				When ( vP.EsAntibiotico = 0 and vP.EsControlado = 0 ) Then 3 end 
		   as SegmentoTipoMed,
		   vP.ContenidoPaquete, 
		   L.ClaveLote, L.CantidadEnviada as CantidadLote, 
		   cast((L.CantidadEnviada / vP.ContenidoPaquete) as Numeric(14,2)) as CantidadCajaLote,
		   F.FechaCaducidad, F.FechaRegistro 
	From vw_TransferenciasEnc E (NoLock) 
	Inner Join TransferenciasDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioTransferencia ) 
	Inner Join TransferenciasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioTransferencia = L.FolioTransferencia 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmaciaEnvia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	--Inner Join vw_Productos vP On ( D.IdProducto = vP.IdProducto ) 
	Inner Join vw_Productos_CodigoEAN vP (NoLock) On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = vP.CodigoEAN )  
	
Go--#SQL 
	