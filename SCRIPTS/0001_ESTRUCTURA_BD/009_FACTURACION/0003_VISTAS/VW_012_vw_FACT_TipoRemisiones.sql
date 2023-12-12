-------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_TipoRemisiones' and xType = 'V' ) 
	Drop View vw_FACT_TipoRemisiones 
Go--#SQL 

Create View vw_FACT_TipoRemisiones 
With Encryption 
As 

	Select 
		F.IdEmpresa, E.Nombre as Empresa, F.IdEstado, CE.Nombre as Estado, 
		F.IdFarmaciaGenera as IdFarmacia,  F.FolioRemision, 
		F.TipoDeRemision, 
		(
			Upper( 
				case when F.TipoDeRemision = 1 then 'INSUMOS' 
					 when F.TipoDeRemision = 3 then 'INSUMOS INCREMENTO'
					 when F.TipoDeRemision = 4 then 'INSUMOS VENTA DIRECTA' 
					 when F.TipoDeRemision = 5 then 'INSUMOS INCREMENTO VENTA DIRECTA' 
					 when F.TipoDeRemision = 2 then 'ADMINISTRACIÓN' 
					 when F.TipoDeRemision = 6 then 'ADMINISTRACIÓN VENTA DIRECTA' 
					 when F.TipoDeRemision = 7 then 'ADMINISTRACIÓN' 
					 when F.TipoDeRemision = 8 then 'ADMINISTRACIÓN VENTA DIRECTA' 
				else 
					'NO ESPECIFICADO' end 
				) 
		) as TipoDeRemisionDesc, 
		RT.Descripcion as TipoDeRemisionDesc_X, 

		-- F.TipoInsumo, case when F.TipoInsumo = '01' Then 'MATERIAL DE CURACIÓN' Else 'MEDICAMENTO' End As TipoDeInsumo, 
		F.TipoInsumo, 
		(
			Upper(
			case when F.TipoInsumo = 0 then 'MEDICAMENTO Y MATERIAL DE CURACIÓN' 
				 when F.TipoInsumo = 1 then 'MATERIAL DE CURACIÓN' 
				 when F.TipoInsumo = 2 then 'MEDICAMENTO' 
				 else 'NO ESPECIFICADO' 
			end) 
		) as TipoDeInsumo, 	

		F.OrigenInsumo, 
		( 
			Case When F.OrigenInsumo Not In ( 0, 1 ) Then 'SIN ESPECIFICAR' 
				Else 
				Case when F.OrigenInsumo = 0 then 'VENTA' else 'CONSIGNACIÓN' end	
			end 
		) as OrigenInsumoDesc, 

		FF.NumeroDeContrato, FF.NumeroDeLicitacion, 

		F.EsFacturada, 
		F.EsRelacionDocumento, 
		F.EsRelacionFacturaPrevia, 

		F.FechaRemision as FechaRemision, 
		convert(varchar(10), F.FechaInicial, 120) as FechaInicial, convert(varchar(10), F.FechaFinal, 120) as FechaFinal, 
		F.FechaValidacion, F.EsFacturable, F.EsExcedente,
		F.IdFuenteFinanciamiento,  F.IdFinanciamiento, 
		( Select top 1 FD.Descripcion From FACT_Fuentes_De_Financiamiento_Detalles FD (NoLock) 
			Where F.IdFuenteFinanciamiento = FD.IdFuenteFinanciamiento And F.IdFinanciamiento = FD.IdFinanciamiento ) as Financiamiento, 
		FF.IdCliente, FF.Cliente, FF.IdSubCliente, FF.SubCliente, 
		F.IdPersonalRemision, F.SubTotalSinGrabar, F.SubTotalGrabado, F.Iva, F.Total, 
		F.Observaciones, F.Observaciones as ObservacionesRemision, F.Status 
	From FACT_Remisiones F (NoLock) 
	Inner Join vw_FACT_FuentesDeFinanciamiento FF (NoLock) On( F.IdFuenteFinanciamiento = FF.IdFuenteFinanciamiento ) 
	Inner Join FACT_TiposDeRemisiones RT (NoLock) On ( F.TipoDeRemision = RT.TipoDeRemision ) 
	Inner Join CatEmpresas E (Nolock) On ( F.IdEmpresa = E.IdEmpresa ) 
	Inner Join CatEstados CE (Nolock) On ( CE.IdEstado = F.IdEstado ) 
	Inner Join CatFarmacias CF (Nolock) On ( CF.IdEstado = F.IdEstado and CF.IdFarmacia = F.IdFarmaciaGenera )

Go--#SQL 
	
	