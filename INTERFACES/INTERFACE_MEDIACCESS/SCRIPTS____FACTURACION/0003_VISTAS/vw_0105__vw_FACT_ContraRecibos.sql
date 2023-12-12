
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Contrarecibos' and xType = 'V' ) 
	Drop View vw_FACT_Contrarecibos 
Go--#SQL 
 	
Create View vw_FACT_Contrarecibos
With Encryption 
As 
	Select 
		E.IdEmpresa, Ex.Nombre as Empresa, E.IdEstado, Edo.Nombre as Estado, E.IdFarmaciaGenera, F.NombreFarmacia as Farmacia,
		E.FolioContrarecibo as Folio, E.Contrarecibo, E.FechaRegistro, E.FechaDocumento, E.Observaciones,
		E.IdPersonalCaptura, P.NombreCompleto as PersonalCaptura, E.Status
	From FACT_Contrarecibos E (Nolock)
	Inner Join CatEmpresas Ex (NoLock) On ( E.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados Edo (NoLock) On ( E.IdEstado = Edo.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmaciaGenera = F.IdFarmacia )	
	Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmaciaGenera = P.IdFarmacia and E.IdPersonalCaptura = P.IdPersonal )	
Go--#SQL	
 
 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Contrarecibos_Detalles' and xType = 'V' ) 
	Drop View vw_FACT_Contrarecibos_Detalles 
Go--#SQL 
 	
Create View vw_FACT_Contrarecibos_Detalles 
With Encryption 
As 
	Select 
		E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmaciaGenera, E.Farmacia,
		E.Folio, E.Contrarecibo, E.FechaRegistro, E.FechaDocumento, E.Observaciones,
		E.IdPersonalCaptura, E.PersonalCaptura, 
		D.FolioFactura, V.TipoDeFactura, V.TipoDeFacturaDesc, V.FechaRegistro as FechaRegistroFactura, 
		V.FolioFacturaElectronica as NumFactura, V.SubTotalSinGrabar, V.SubTotalGrabado, V.Iva, V.Total as Importe, 
		E.Status
	From vw_FACT_Contrarecibos E (Nolock)
	Inner Join FACT_Contrarecibos_Detalles D (Nolock)
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmaciaGenera = D.IdFarmaciaGenera and E.Folio = D.FolioContrarecibo )
	Inner Join vw_FACT_Facturas_Remisiones V (Nolock)
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmaciaGenera = V.IdFarmacia and D.FolioFactura = V.FolioFactura )
		
Go--#SQL	
 