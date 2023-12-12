------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Facturas' and xType = 'V' ) 
	Drop View vw_FACT_Facturas 
Go--#SQL 
 	
Create View vw_FACT_Facturas
With Encryption 
As 
	Select 
		E.IdEmpresa, Ex.Nombre as Empresa, E.IdEstado, Edo.Nombre as Estado, E.IdFarmacia, F.NombreFarmacia as Farmacia,
		E.FolioFactura as Folio, E.TipoDeFactura,
		(
				Upper(
				case when E.TipoDeFactura = 0 then 'Sin especificar' 
					else 
						case when E.TipoDeFactura = 1 then 'INSUMOS' else 'ADMINISTRACIÓN' end 
				end)
			) as TipoDeFacturaDesc,
		R.TipoInsumo, case when R.TipoInsumo = '01' Then 'MATERIAL DE CURACIÓN' Else 'MEDICAMENTO' End As TipoDeInsumo,
		E.FechaRegistro, E.FolioFacturaElectronica as NumFactura, 
		E.IdPersonalFactura, P.NombreCompleto as PersonalFactura, 
		E.SubTotalSinGrabar, E.SubTotalGrabado, E.Iva, E.Total as Importe,
		E.Observaciones, E.EstaEnCobro, E.Status 
	From FACT_Facturas E (Nolock)
	Inner Join FACT_Remisiones R (NoLock) 
		On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmaciaGenera and E.FolioRemision = R.FolioRemision )
	Inner Join CatEmpresas Ex (NoLock) On ( E.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados Edo (NoLock) On ( E.IdEstado = Edo.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )	
	Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonalFactura = P.IdPersonal )	

Go--#SQL	


---		Select * From FACT_Facturas (Nolock)