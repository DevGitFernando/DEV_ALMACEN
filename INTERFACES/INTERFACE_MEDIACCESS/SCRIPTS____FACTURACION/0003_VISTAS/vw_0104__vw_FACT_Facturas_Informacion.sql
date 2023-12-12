--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Facturas_Informacion' and xType = 'V' ) 
	Drop View vw_FACT_Facturas_Informacion 
Go--#SQL 
 	
Create View vw_FACT_Facturas_Informacion 
With Encryption 
As 

	Select  
		E.Keyx as Identificador, 
		E.IdEmpresa, EM.Nombre as Empresa, 
		E.IdEstado, F.Estado, 
		E.IdFarmacia as IdFarmaciaGenera, F.Farmacia as FarmaciaGenera, 
		E.FechaRegistro as FechaFactura, -- E.IdTipoDocumento, T.NombreDocumento as TipoDeDocumento, 
		FG.EstaEnCobro,
		E.Serie, E.Folio, 
		cast((E.Serie + ' - ' + cast(E.Folio as varchar)) as varchar(20)) as NumeroDeFactura,  
		FG.FolioFactura, 
		E.RFC, E.NombreReceptor, 
		E.Status as StatusDocto,  
		-- (case when E.Status = 'A' Then 1 Else 0 End) as StatusDoctoAux, 
		(case when E.Status = 'A' Then 'Activo' Else 'Cancelado' End) as StatusDocumento,  	
		FR.IdEstado As IdEstado_Factura, FR.Estado As Estado_Factura,
		FR.IdFarmacia as IdFarmacia_Facturada, 
		FR.Farmacia as Farmacia_Facturada, FR.Referencia_MA_Facturacion, 
		(R.SubTotalSinGrabar + R.SubTotalGrabado) as SubTotal, R.Iva, R.Total as Importe 
		
		----E.Observaciones_01, E.Observaciones_02, E.Observaciones_03, E.Referencia,	
		----E.TipoDocumento, 
		----(
		----	case When E.TipoDocumento = 1 Then 'Venta' 
		----		 When E.TipoDocumento = 2 Then 'Administración' 
		----	else '' 
		----	End 
		----) as TipoDocumentoDescripcion, 
		----E.TipoInsumo, 
		----(
		----	case When E.TipoInsumo = 1 Then 'Material de curación' 
		----		 When E.TipoInsumo = 2 Then 'Medicamento' 
		----	else '' 
		----	End 
		----) as TipoInsumoDescripcion 		
	From CFDI_Documentos E (NoLock) 
	Inner Join CFDI_TiposDeDocumentos T (NoLock) On ( E.IdTipoDocumento = T.IdTipoDocumento and E.IdTipoDocumento = '001' ) 
	Inner Join CatEmpresas EM (NoLock) On ( E.IdEmpresa = EM.IdEmpresa ) 
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Inner Join FACT_Facturas FG (NoLock) 
		On ( E.IdEmpresa = FG.IdEmpresa and E.IdEstado = FG.IdEstado and E.IdFarmacia = FG.IdFarmaciaGenera 
			and E.Serie = FG.Serie and E.Folio = FG.FolioFacturaElectronica ) 
	Inner Join FACT_Remisiones R (NoLock) 
		On ( FG.IdEmpresa = R.IdEmpresa and FG.IdEstado = R.IdEstadoGenera and FG.IdFarmaciaGenera = R.IdFarmaciaGenera and FG.FolioRemision = R.FolioRemision )  	
	Inner Join vw_INT_MA__Farmacias FR (NoLock) On ( R.IdEstado = FR.IdEstado and R.IdFarmacia = FR.IdFarmacia ) 			

Go--#SQL 



--		select * from  vw_FACT_Facturas_Informacion  

--		update FACT_CFD_Documentos_Generados Set Importe = 154.00 * 1.15 where Folio = 7 

--		select * from FACT_Remisiones 

--	sp_listacolumnas vw_FACT_CFD_Sucursales_Series 
	