---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo' and xType = 'P' )
    Drop Proc spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo
Go--#SQL 

--	Exec spp_FACT_CFD_GetRemision  @IdEmpresa = '002', @IdEstado = '09', @IdFarmaciaGenera = '0001', @FolioRemision = '0000000001', @Detallado = '1'  
--- Exec spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo  @IdEmpresa = '002', @IdEstado = '09', @IdFarmaciaGenera = '0001', @FolioRemision = '0000000001', @Detallado = '1' 
  
--		sp_listacolumnas__Stores spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo,  1   
  
Create Proc spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioContraRecibo varchar(10) = '1' 
) 
With Encryption		
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2)
	Set @IdFarmaciaGenera = right('0000000000' + @IdFarmaciaGenera, 4)
	Set @FolioContraRecibo = right('0000000000' + @FolioContraRecibo, 10)			
	
--	select * from FACT_Facturas 	
	
--	spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo  	
	
------------------------------------------- Obtener la informacion base del proceso 
	Select 
		F.IdEmpresa, F.IdEstado, F.IdFarmaciaGenera, 
		R.IdEstado As IdEstadoOrigen, R.IdFarmacia, cast('' as varchar(200)) as Farmacia, 
		R.IdPrograma, P.Programa, R.IdSubPrograma, P.SubPrograma, 
		cast('' as varchar(50)) as Proveedor_MA, 
		F.Serie, F.FolioFacturaElectronica as Folio, 
		cast(ltrim(rtrim(F.Serie)) + '-' + ltrim(rtrim(F.FolioFacturaElectronica)) as varchar(50)) as FolioFactura, 
		
		CR.FolioContrarecibo, convert(varchar(10), CR.FechaRegistro, 120) as FechaContrarecibo,  
		
		cast((F.SubTotalSinGrabar + F.SubTotalGrabado) as numeric(14,2)) as SubTotal, 
		cast(F.Iva as numeric(14,2)) as Iva, 
		cast(F.Total as numeric(14,2)) as Importe 
	Into #tmpDetalles 
	From FACT_Contrarecibos CR (NoLock) 
	Inner Join FACT_Contrarecibos_Detalles CRD (NoLock) -- FACT_Remisiones -- FACT_Facturas 
		On ( CR.IdEmpresa = CRD.IdEmpresa and CR.IdEstado = CRD.IdEstado and CR.IdFarmaciaGenera = CRD.IdFarmaciaGenera and CR.FolioContraRecibo = CRD.FolioContraRecibo ) 	
	Inner Join FACT_Facturas F (NoLock) 
		On ( F.IdEmpresa = CRD.IdEmpresa and F.IdEstado = CRD.IdEstado and F.IdFarmaciaGenera = CRD.IdFarmaciaGenera and F.FolioFactura = CRD.FolioFactura ) 
	Inner Join FACT_Remisiones R (NoLock) 
		On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstadoGenera and F.IdFarmaciaGenera = R.IdFarmaciaGenera and F.FolioRemision = R.FolioRemision )  	
	Inner Join vw_Programas_SubProgramas P (NoLock) On ( R.IdPrograma = P.IdPrograma and R.IdSubPrograma = P.IdSubPrograma )  

	Where CR.IdEmpresa = @IdEmpresa and CR.IdEstado = @IdEstado and CR.IdFarmaciaGenera = @IdFarmaciaGenera and CR.FolioContraRecibo = @FolioContraRecibo 
	-- Order By V.FolioVenta, FechaVenta  
------------------------------------------- Obtener la informacion base del proceso 	

---		select * from FACT_Facturas		

---		select * from FACT_Contrarecibos 

---		select * from FACT_Contrarecibos_Detalles 		

--		spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo  


------------------------------------------- Obtener la informacion adicional 
	Update D Set Proveedor_MA = F.Referencia_MA_Facturacion, Farmacia = F.Farmacia  
	From #tmpDetalles D (NoLock)  
	Inner Join vw_INT_MA__Farmacias F (NoLock) On ( D.IdEstadoOrigen = F.IdEstado and D.IdFarmacia = F.IdFarmacia ) 
	
	----Update D Set FechaFactura = cast((convert(varchar(10), F.FechaRegistro, 120) + ' 00:00:00' ) as datetime)  
	----From #tmpDetalles D (NoLock)  
	----Inner Join CFDI_Documentos F (NoLock) 
	----	On ( D.IdEmpresa = F.IdEmpresa and D.IdEstado = F.IdEstado and D.IdFarmaciaGenera = F.IdFarmacia 
	----		and D.Serie = F.Serie and D.Folio = F.Folio ) 		
------------------------------------------- Obtener la informacion adicional 




--	spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo  
	
	
------------------------------------ SALIDA FINAL 
	Select	
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, Farmacia, Proveedor_MA, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		-- FechaFactura, 
		-- FolioFactura, NumeroReceta, Elegibilidad, 
		-- FechaVenta, 
		
		FolioFactura, 
		FolioContrarecibo, FechaContrarecibo, 
		cast('ENTREGADA' as varchar(100)) as Estado_Factura,  
		
		cast(sum(D.SubTotal) as numeric(14, 2)) as SubTotal, 
		cast(sum(D.Iva) as numeric(14, 2)) as Iva, 
		cast(sum(D.Importe) as numeric(14, 2)) as Importe   
	From #tmpDetalles D (NoLock)	
	Group by 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, Farmacia, Proveedor_MA, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		FolioFactura, FolioContrarecibo, FechaContrarecibo  
	-- Order By FechaVenta, DescripcionComercial 
		


End 
Go--#SQL 

