If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Facturas_Remisiones' and xType = 'V' ) 
	Drop View vw_FACT_Facturas_Remisiones 
Go--#SQL 

Create View vw_FACT_Facturas_Remisiones 
With Encryption 
As 

	Select
		F.IdEmpresa, F.IdEstado, F.IdFarmaciaGenera as IdFarmacia, F.FolioFactura, 
		F.TipoDeFactura, 
		( 
			Upper(
			case when F.TipoDeFactura = 0 then 'Sin especificar' 
				else 
					case when F.TipoDeFactura = 1 then 'Insumos' else 'Administracion' end 
			end)
		) as TipoDeFacturaDesc, 
		F.FechaRegistro,  
		F.FolioRemision, R.FechaRemision as FechaRemision, 
		F.Serie, F.FolioFacturaElectronica as FacturaElectronica,  
		(F.Serie + ' - ' + F.FolioFacturaElectronica) as FolioFacturaElectronica, 
		F.IdPersonalFactura, F.SubTotalSinGrabar, F.SubTotalGrabado, F.Iva, F.Total, 
		F.Observaciones, R.Observaciones as ObservacionesRemision, F.Status,
		R.IdEstado As IdEstadoReferencia, A.Estado as EstadoReferencia, R.IdFarmacia As IdFarmaciaReferencia, A.Farmacia As FarmaciaReferencia
	From FACT_Facturas F (NoLock) 
	Inner Join FACT_Remisiones R (NoLock) 
		On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstadoGenera and F.IdFarmaciaGenera = R.IdFarmaciaGenera and F.FolioRemision = R.FolioRemision )
	Inner Join vw_Farmacias A (NoLock) On ( R.IdEstado = A.IdEstado And R.IdFarmacia = A.IdFarmacia )


Go--#SQL 

	select * from vw_FACT_Facturas_Remisiones 


--	select * from FACT_Facturas (nolock) 


--	select * from FACT_Remisiones (nolock) 
	

