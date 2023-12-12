---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura' and xType = 'P' )
    Drop Proc spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura
Go--#SQL 

--	Exec spp_FACT_CFD_GetRemision  @IdEmpresa = '002', @IdEstado = '09', @IdFarmaciaGenera = '0001', @FolioRemision = '0000000001', @Detallado = '1'  
--- Exec spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura  @IdEmpresa = '002', @IdEstado = '09', @IdFarmaciaGenera = '0001', @FolioRemision = '0000000001', @Detallado = '1' 
  
--		sp_listacolumnas__Stores spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura,  1   
  
Create Proc spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioFactura varchar(10) = '165' 
) 
With Encryption		
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2)
	Set @IdFarmaciaGenera = right('0000000000' + @IdFarmaciaGenera, 4)
	Set @FolioFactura = right('0000000000' + @FolioFactura, 10)			
	
--	select * from FACT_Facturas 	
	
--	spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura  	
	
------------------------------------------- Obtener la informacion base del proceso 
	Select 
		F.IdEmpresa, F.IdEstado, F.IdFarmaciaGenera, 
		D.IdFarmacia, cast('' as varchar(200)) as Farmacia, 
		cast('' as varchar(50)) as Proveedor_MA, 
		F.Serie, F.FolioFacturaElectronica as Folio, 
		cast(ltrim(rtrim(F.Serie)) + ltrim(rtrim(F.FolioFacturaElectronica)) as varchar(50)) as FolioFactura, 
		getdate() as FechaFactura, 
		cast(VI.NumReceta as varchar(50)) as NumeroReceta, 
		1 as EsRecetaManual, 
		cast('' as varchar(50)) as Elegibilidad, 

		cast('' as varchar(10)) as CIE10_01, 
		cast('' as varchar(10)) as CIE10_02, 
		cast('' as varchar(10)) as CIE10_03, 
		cast('' as varchar(10)) as CIE10_04, 
								
		cast((convert(varchar(10), V.FechaRegistro, 120) + ' 00:00:00' ) as datetime) as FechaVenta, 
		D.FolioVenta, D.IdProducto, D.CodigoEAN, 
		P.ClaveSSA, P.Descripcion as DescripcionComercial, 		
		
		cast((D.PrecioUnitario) as numeric(14,2)) as PrecioUnitario, 
		cast((D.PrecioUnitario) as numeric(14,2)) as PrecioUnitario_Aseguradora, 		
		cast((D.PrecioUnitario) as numeric(14,2)) as PrecioUnitario_Descuento, 		
				
		cast(IM.Porcentaje_02 as numeric(14, 2)) as Porcenjate, 				
		cast(IM.Porcentaje_01 as numeric(14, 2)) as PorcenjateDescuento, 	
		cast((IM.Porcentaje_02 / 100.00)as numeric(14, 2)) as Porcenjate_Aplicar, 
		cast((IM.Porcentaje_01 / 100.00)as numeric(14, 2)) as Porcenjate_Aplicar_Descuento, 		
		
		cast((D.CantidadVendida) as int) as Cantidad, 
		cast((D.TasaIva) as numeric(14,2)) as TasaIva, 	
		cast(0 as numeric(14,2)) as SubTotal, 
		cast(0 as numeric(14,2)) as Iva, 
		cast(0 as numeric(14,2)) as Importe, 
		cast(0 as numeric(14,2)) as ImporteDescuento  		   	
	Into #tmpDetalles 
	From FACT_Facturas F (NoLock) -- FACT_Remisiones -- FACT_Facturas 
	Inner Join FACT_Remisiones R (NoLock) 
		On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstadoGenera and F.IdFarmaciaGenera = R.IdFarmaciaGenera and F.FolioRemision = R.FolioRemision )  	
	Inner Join VentasDet D (NoLock) 
		On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmacia 
			and R.FolioRemision = D.FolioRemision and F.Serie = D.FACT_Serie and F.FolioFacturaElectronica = D.FACT_Folio ) 
	Inner Join VentasEnc V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioVenta = V.FolioVenta ) 
	Inner Join VentasInformacionAdicional VI (NoLock) 
		On ( D.IdEmpresa = VI.IdEmpresa and D.IdEstado = VI.IdEstado and D.IdFarmacia = VI.IdFarmacia and D.FolioVenta = VI.FolioVenta ) 		
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )				
	Inner Join INT_MA_Ventas_Importes IM (NoLock) 
		On ( D.IdEmpresa = IM.IdEmpresa and D.IdEstado = IM.IdEstado and D.IdFarmacia = IM.IdFarmacia and D.FolioVenta = IM.FolioVenta ) 		
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmaciaGenera = @IdFarmaciaGenera and F.FolioFactura = @FolioFactura 
		and D.CantidadVendida > 0 
	Order By V.FolioVenta, FechaVenta  


	---------- Eliminar la información que determina las recetas con surtido parcial 
	Update D Set NumeroReceta =  ltrim(rtrim(left(NumeroReceta, len(NumeroReceta) - (len(NumeroReceta)-CHARINDEX('-', NumeroReceta)+1)))) 
	From #tmpDetalles D 
	Where NumeroReceta like '%-%' 

------------------------------------------- Obtener la informacion base del proceso 	


--	spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura  


------------------------------------------- Obtener la informacion adicional 
	Update D Set Proveedor_MA = F.Referencia_MA_Facturacion, Farmacia = F.Farmacia  
	From #tmpDetalles D (NoLock)  
	Inner Join vw_INT_MA__Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia ) 
	
	
	Update D Set Elegibilidad = F.Elegibilidad, 
		CIE10_01 = CIE_01, CIE10_02 = CIE_02, CIE10_03 = CIE_03, CIE10_04 = CIE_04 
	From #tmpDetalles D (NoLock)  
	Inner Join INT_MA__RecetasElectronicas_001_Encabezado F (NoLock) On ( D.NumeroReceta = F.Folio_MA ) 	


	Update D Set EsRecetaManual = F.EsRecetaManual  
	From #tmpDetalles D (NoLock)  
	Inner Join INT_MA__RecetasElectronicas_001_Encabezado F (NoLock) On ( D.Elegibilidad = F.Elegibilidad ) 	

	Update D Set NumeroReceta = 'G' + NumeroReceta 
	From #tmpDetalles D (NoLock)  
	Where EsRecetaManual = 1 and len(NumeroReceta) <= 8 
		and Not ( NumeroReceta like 'G%' or NumeroReceta like 'E%' or NumeroReceta like 'E%' ) 
		  
		
	Update D Set 
		--FechaFactura = cast((convert(varchar(10), F.FechaRegistro, 120) + ' 00:00:00' ) as datetime)  
		FechaFactura = F.FechaRegistro
	From #tmpDetalles D (NoLock)  
	Inner Join CFDI_Documentos F (NoLock) 
		On ( D.IdEmpresa = F.IdEmpresa and D.IdEstado = F.IdEstado and D.IdFarmaciaGenera = F.IdFarmacia 
			and D.Serie = F.Serie and D.Folio = F.Folio ) 		
------------------------------------------- Obtener la informacion adicional 


------------------------------------------- Calcular los precios unitarios 
	Update P Set 
		PrecioUnitario_Aseguradora = (PrecioUnitario * Porcenjate_Aplicar), 
		PrecioUnitario_Descuento = (PrecioUnitario * Porcenjate_Aplicar_Descuento)
	From #tmpDetalles P (NoLock) 
	
	Update P Set 
		ImporteDescuento = (PrecioUnitario_Descuento * Cantidad), 	
		SubTotal = (PrecioUnitario_Aseguradora * Cantidad),  
		Iva = ((PrecioUnitario_Aseguradora * Cantidad) * ( TasaIva / 100.00 )),  		
		Importe = ((PrecioUnitario_Aseguradora * Cantidad) * ( 1 + ( TasaIva / 100.00 )))
	From #tmpDetalles P (NoLock) 	
------------------------------------------- Calcular los precios unitarios 	


--	spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura  
	
	
------------------------------------ SALIDA FINAL 
	Select	
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, Farmacia, Proveedor_MA, FechaFactura, 
		FolioFactura, EsRecetaManual, NumeroReceta, Elegibilidad, 
		CIE10_01, CIE10_02, CIE10_03, CIE10_04, 
		FechaVenta, 
		IdProducto, CodigoEAN, DescripcionComercial, 
		
		cast(D.PrecioUnitario as numeric(14, 2)) as PrecioUnitario_Venta, 
		Porcenjate, 
		PorcenjateDescuento, 
		cast(D.PrecioUnitario_Aseguradora as numeric(14, 2)) as PrecioUnitario, 
		cast(D.PrecioUnitario_Descuento as numeric(14, 2)) as PrecioUnitario_Descuento, 	
			
		
		cast(sum(D.Cantidad) as int) as Cantidad, 
		cast(sum(D.TasaIva) as numeric(14, 2)) as TasaIva, 
		cast(sum(D.SubTotal) as numeric(14, 2)) as SubTotal, 
		cast(sum(D.Iva) as numeric(14, 2)) as Iva, 
		cast(sum(D.Importe) as numeric(14, 2)) as Importe,  
		cast(sum(D.ImporteDescuento) as numeric(14, 2)) as ImporteDescuento 		
	From #tmpDetalles D (NoLock)	
	Group by 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, Farmacia, Proveedor_MA, FechaFactura, 
		FolioFactura, EsRecetaManual, NumeroReceta, Elegibilidad, 
		CIE10_01, CIE10_02, CIE10_03, CIE10_04, 
		FechaVenta, 
		IdProducto, CodigoEAN, DescripcionComercial, D.PrecioUnitario, D.PrecioUnitario_Descuento, 
		D.Porcenjate, D.PorcenjateDescuento, 
		D.PrecioUnitario_Aseguradora, D.TasaIva 
	Order By 
		-- FechaVenta, DescripcionComercial 
		FechaVenta, NumeroReceta, DescripcionComercial 




End 
Go--#SQL 

