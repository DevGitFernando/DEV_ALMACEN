---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Facturas_Remisiones' and xType = 'V' ) 
	Drop View vw_FACT_Facturas_Remisiones 
Go--#SQL 

Create View vw_FACT_Facturas_Remisiones 
With Encryption 
As 

	Select 
		F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.FolioFactura, 
		F.TipoDeFactura, 
		(
			----Upper(
			----case when F.TipoDeFactura = 0 then 'Sin especificar' 
			----	else 
			----		case when F.TipoDeFactura = 1 then 'Insumos' else 'Administracion' end 
			----end) 
			Upper( 
				case when F.TipoDeFactura = 1 then 'INSUMOS' 
					 when F.TipoDeFactura = 3 then 'INSUMOS INCREMENTO'
					 when F.TipoDeFactura = 4 then 'INSUMOS VENTA DIRECTA' 
					 when F.TipoDeFactura = 5 then 'INSUMOS INCREMENTO VENTA DIRECTA' 
					 when F.TipoDeFactura = 2 then 'ADMINISTRACIÓN' 
					 when F.TipoDeFactura = 6 then 'ADMINISTRACIÓN VENTA DIRECTA' 
				else 
					'NO ESPECIFICADO' end 
				) 

		) as TipoDeFacturaDesc, 

		R.EsFacturable, R.EsFacturada, R.EsRelacionFacturaPrevia,  

		F.FechaRegistro,  
		F.FolioRemision, R.FechaRemision as FechaRemision, 
		R.NumeroDeContrato, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento,
		F.FolioFacturaElectronica, 
		F.IdPersonalFactura, F.SubTotalSinGrabar, F.SubTotalGrabado, F.Iva, F.Total, 
		F.Observaciones, R.Observaciones as ObservacionesRemision, F.Status 
	From FACT_Facturas F (NoLock) 
	Inner Join vw_FACT_Remisiones R (NoLock) 
		On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmacia and F.FolioRemision = R.FolioRemision )


Go--#SQL 

	select * from vw_FACT_Facturas_Remisiones 

--	select * from FACT_Facturas (nolock) 
--	select * from FACT_Remisiones (nolock) 
	

