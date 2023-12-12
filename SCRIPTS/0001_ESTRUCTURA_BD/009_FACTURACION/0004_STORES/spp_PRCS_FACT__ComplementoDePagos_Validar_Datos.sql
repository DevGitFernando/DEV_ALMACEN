---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_PRCS_FACT__ComplementoDePagos_Validar_Datos' and xType = 'P' ) 
   Drop Proc spp_PRCS_FACT__ComplementoDePagos_Validar_Datos 
Go--#SQL 

Create Proc spp_PRCS_FACT__ComplementoDePagos_Validar_Datos   
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(4) = '11', @IdFarmacia varchar(4) = '1', @RFC varchar(20) = 'ISP961122JV5', 
	@Detallado smallint = 0    
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEmpresa = right('00' +  @IdEmpresa, 3) 
	Set @IdEstado = right('000000' +  @IdEstado, 2) 
	Set @IdFarmacia = right('00000' +  @IdFarmacia, 4) 		
	
	Exec spp_FormatearTabla FACT_CFDI_ComplementoDePagos__CargaMasiva 
			

----------------------------------- FORMATEAR VALORES 
	Update C Set 
		IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, 
		RFC = @RFC, 
		Serie = UPPER(Serie), 
		Importe_Pagado = round(Importe_Pagado, 2, 0) 
	From FACT_CFDI_ComplementoDePagos__CargaMasiva C (NoLock) 	


	
	Update C Set ExisteFactura = 1, UUID = F.UUID, ValorNominal = F.Importe, 
		NumParcialidad = IsNull(dbo.fg_FACT_ComplementoPagos_Parcialidad ( F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.Serie, F.Folio ), 0), 
		Importe_Abonos = IsNull(dbo.fg_FACT_ComplementoPagos_Acumulado ( F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.Serie, F.Folio ), 0)  
	From FACT_CFDI_ComplementoDePagos__CargaMasiva C (NoLock) 	
	Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock)   -- FACT_CFD_Documentos_Generados 
		On ( C.IdEmpresa = F.IdEmpresa and C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia and C.Serie = F.Serie and C.Folio = F.Folio )  


	Update C Set Importe_SaldoAnterior = ValorNominal - Importe_Abonos, Importe_SaldoInsoluto = (ValorNominal - Importe_Abonos) - Importe_Pagado 
	From FACT_CFDI_ComplementoDePagos__CargaMasiva C (NoLock) 	

	Update C Set EsFacturaDeRFC = 1 
	From FACT_CFDI_ComplementoDePagos__CargaMasiva C (NoLock) 	
	Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) 
		On ( C.IdEmpresa = F.IdEmpresa and C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia and C.Serie = F.Serie and C.Folio = F.Folio and C.RFC = F.RFC )  


	Update C Set RFC = F.RFC, RFC_Factura = F.NombreReceptor,  ExisteFactura = 1  
	From FACT_CFDI_ComplementoDePagos__CargaMasiva C (NoLock) 	
	Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) 
		On ( C.IdEmpresa = F.IdEmpresa and C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia and C.Serie = F.Serie and C.Folio = F.Folio )  
	Where EsFacturaDeRFC = 0 


	--Update C Set 
	--	Total = Importe_Pagado, SubTotal = (case when TasaIVA > 0 then round(Importe_Pagado / ( 1 / (TasaIVA / 100.00) ), 2, 1) else Importe_Pagado end)  
	--From FACT_CFDI_ComplementoDePagos__CargaMasiva C (NoLock) 		
----------------------------------- FORMATEAR VALORES 				



----------------------------------- FACTURAS NO EXISTENTES  
	Select 
		Serie, Folio
	Into #tmp_FacturaNoExiste 
	From FACT_CFDI_ComplementoDePagos__CargaMasiva P (NoLock)  
	Where ExisteFactura  = 0 
----------------------------------- FACTURAS NO EXISTENTES  



----------------------------------- FACTURAS OTRO RFC   
	Select 
		Serie, Folio, RFC, RFC_Factura as NombreCliente  
	Into #tmp_FacturaNoRFC 
	From FACT_CFDI_ComplementoDePagos__CargaMasiva P (NoLock)  
	Where EsFacturaDeRFC  = 0 And ExisteFactura = 1 
----------------------------------- FACTURAS OTRO RFC   



----------------------------------- FACTURAS DUPLICADAS 
	Select 
		Serie, Folio, UUID 
	Into #tmp_FacturaDuplicadas 
	From FACT_CFDI_ComplementoDePagos__CargaMasiva P (NoLock)  
	Where EsFacturaDeRFC  = 1  And ExisteFactura = 1 and @Detallado = 0 
	Group by Serie, Folio, UUID   
	Having count(*) >= 2 
----------------------------------- FACTURAS DUPLICADAS 


----------------------------------- FACTURAS IMPORTE CERO    
	Select 
		Serie, Folio, UUID, Importe_Pagado, ExisteFactura, EsFacturaDeRFC, NumParcialidad, ValorNominal, Importe_Abonos, Importe_SaldoAnterior, Importe_SaldoInsoluto
	Into #tmp_Factura_PagoCERO 
	From FACT_CFDI_ComplementoDePagos__CargaMasiva P (NoLock)  
	Where Importe_Pagado  = 0 And ExisteFactura = 1 
----------------------------------- FACTURAS IMPORTE CERO    


----------------------------------- FACTURAS IMPORTE CERO    
	Select 
		Distinct 
		Serie, Folio, UUID, Importe_Pagado, ExisteFactura, EsFacturaDeRFC, NumParcialidad, ValorNominal, Importe_Abonos, Importe_SaldoAnterior, Importe_SaldoInsoluto
	Into #tmp_Factura_PagoConExcedente  
	From FACT_CFDI_ComplementoDePagos__CargaMasiva P (NoLock)  
	Where Importe_SaldoInsoluto < 0 
----------------------------------- FACTURAS IMPORTE CERO    




----------------------- SALIDA FINAL	
	Select top 0 identity(int, 2, 1) as Orden, cast('' as varchar(200)) as NombreTabla Into #tmpResultados 

	Insert Into #tmpResultados ( NombreTabla ) select 'Facturas no encontradas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Facturas de otro RFC' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Facturas duplicadas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Facturas con pago CERO' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Facturas con pago excedente' 


	Select * From #tmpResultados
	Select * From #tmp_FacturaNoExiste
	Select * From #tmp_FacturaNoRFC 
	Select * From #tmp_FacturaDuplicadas 
	Select * From #tmp_Factura_PagoCERO 
	Select * From #tmp_Factura_PagoConExcedente 

----------------------- SALIDA FINAL	


---		spp_PRCS_FACT__ComplementoDePagos_Validar_Datos  


End 
Go--#SQL 



