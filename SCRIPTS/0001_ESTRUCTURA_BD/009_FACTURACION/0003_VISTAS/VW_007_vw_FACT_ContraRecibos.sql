

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Contrarecibos' and xType = 'V' ) 
	Drop View vw_FACT_Contrarecibos 
Go--#SQL 
 	
Create View vw_FACT_Contrarecibos
With Encryption 
As 
	Select E.IdEmpresa, Ex.Nombre as Empresa, E.IdEstado, Edo.Nombre as Estado, E.IdFarmacia, F.NombreFarmacia as Farmacia,
	E.FolioContrarecibo as Folio, E.Contrarecibo, E.FechaRegistro, E.FechaDocumento, E.Observaciones,
	E.IdPersonalFactura, P.NombreCompleto as PersonalFactura, E.Status
	From FACT_Contrarecibos E (Nolock)
	Inner Join CatEmpresas Ex (NoLock) On ( E.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados Edo (NoLock) On ( E.IdEstado = Edo.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )	
	Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonalFactura = P.IdPersonal )	
Go--#SQL	
 
------------------------------------------------------------------------------   


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Contrarecibos_Detalles' and xType = 'V' ) 
	Drop View vw_FACT_Contrarecibos_Detalles 
Go--#SQL 
 	
Create View vw_FACT_Contrarecibos_Detalles 
With Encryption 
As 
	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia,
	E.Folio, E.Contrarecibo, E.FechaRegistro, E.FechaDocumento, E.Observaciones,
	E.IdPersonalFactura, E.PersonalFactura, 
	D.FolioFactura, V.TipoDeFactura, V.TipoDeFacturaDesc, V.FechaRegistro as FechaRegistroFactura, 
	V.FolioFacturaElectronica as NumFactura, V.SubTotalSinGrabar, V.SubTotalGrabado, V.Iva, V.Total as Importe, 
	E.Status
	From vw_FACT_Contrarecibos E (Nolock)
	Inner Join FACT_Contrarecibos_Detalles D (Nolock)
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioContrarecibo )
	Inner Join vw_FACT_Facturas_Remisiones V (Nolock)
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioFactura = V.FolioFactura )
		
Go--#SQL	
 
------------------------------------------------------------------------------   






	