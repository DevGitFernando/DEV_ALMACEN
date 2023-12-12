If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__FACT_GetInformacion_FacturarVenta' and xType = 'P' ) 
   Drop Proc spp_INT_MA__FACT_GetInformacion_FacturarVenta
Go--#SQL 

/* 
Begin tran 

	Exec spp_INT_MA__FACT_GetInformacion_FacturarVenta @IdEmpresa = '002', @IdEstado = '09', @IdFarmacia = '0011', @FolioVenta = 8730  


rollback tran 

*/ 

Create Proc spp_INT_MA__FACT_GetInformacion_FacturarVenta 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0011', 
	@FolioVenta varchar(8) = '8730' 
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On

Declare 
	@FolioRemision int,
	@SubTotalSinGrabar numeric(14,4),
	@Iva numeric(14,4),
	@SubTotalGrabado numeric(14,4),
	@sMensaje varchar(1000), 
	@TipoDeFacturacion int 

Declare 
	@Anexar__Lotes_y_Caducidades bit, 
	@EsInsumos bit, 	
	@EsMedicamento bit, 
	@sUnidadDeMedida varchar(100) 

Declare 
	@sSql varchar(max), 
	@sFiltro varchar(200) 
	
	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @FolioRemision =  0 
	
	
	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2)
	Set @IdFarmacia = right('0000000000' + @IdFarmacia, 4)
	Set @FolioVenta = right('0000000000' + @FolioVenta, 8)			
	Select @sUnidadDeMedida = Descripcion From CFDI_UnidadesDeMedida (NoLock) Where IdUnidad = '001'
	Select @TipoDeFacturacion = dbo.fg_Parametro_TipoFacturacion ( ) 
		
--		spp_INT_MA__FACT_GetInformacion_FacturarVenta 

-------------- Asegurar que todas las ventas esten procesadas 
	Exec spp_INT_MA__PRCS__CalcularPorcentajes  


--	INT_MA_Ventas_Importes

----------------------------------------- Obtener la informacion a procesar 
	Select
		identity(int, 1, 1) as Keyx, 
		E.TipoDeVenta, 
		D.FolioVenta, D.IdProducto, D.CodigoEAN, 
		P.ClaveSSA, P.Descripcion as DescripcionComercial, 		
		cast((D.PrecioUnitario) as numeric(14,2)) as PrecioUnitario, 
		cast((D.PrecioUnitario) as numeric(14,2)) as PrecioUnitario_Facturacion, 		
		
		cast(IsNull(IM.Porcentaje_01, 100) as numeric(14, 2)) as Porcenjate, 
		(IsNull(IM.Porcentaje_01, 100) / 100.00) as Porcenjate_Aplicar, 
		
		cast((D.CantidadVendida) as numeric(14,4)) as Cantidad, 
		cast((D.TasaIva) as numeric(14,2)) as TasaIva, 	
		cast(0 as numeric(14,2)) as SubTotal, 
		cast(0 as numeric(14,2)) as Iva, 
		cast(0 as numeric(14,2)) as Importe,  
		cast(@sUnidadDeMedida as varchar(100)) as UnidadDeMedida, 
		StatusDocto, 
		Facturado_Al_Beneficiario as Facturado, 
		cast('' as varchar(10)) as UsoDeCFDI, 
		FACT_Serie_Beneficiario as Serie, FACT_Folio_Beneficiario as Folio, 
		cast(IsNull(F.RFC, '') as varchar(20)) as RFC, 
		cast(IsNull(F.IdCliente, '') as varchar(20)) as IdCliente, 		
		cast(IsNull(F.NombreReceptor, '') as varchar(200)) as NombreCliente, 
		IsNull(F.Identificador, 0) as Identificador, 
		0 as Producto_Para_Facturacion, 
		cast('' as varchar(20)) as SAT_ClaveProductoServicio, 
		cast('' as varchar(20)) as SAT_UnidadDeMedida 
		     
	Into #tmpDetalles  	
	From VentasEnc E (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	inner Join INT_MA_Ventas_Importes IM (NoLock) 
		On ( E.IdEmpresa = IM.IdEmpresa and E.IdEstado = IM.IdEstado and E.IdFarmacia = IM.IdFarmacia and E.FolioVenta = IM.FolioVenta ) 	
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )		
	Left Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) 
		On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia and 
			D.FACT_Serie_Beneficiario = F.Serie and D.FACT_Folio_Beneficiario = F.Folio )	
	Where -- E.TipoDeVenta = 2 and D.Facturado = 0 and D.EnRemision = 0
		-- Facturado_Al_Beneficiario = 0 
		-- And 
		E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia 
		And E.FolioVenta = @FolioVenta 
	

	Update P Set UsoDeCFDI = D.UsoDeCFDI  
	From #tmpDetalles P 
	Inner Join CFDI_Documentos D (NoLock) 
		On ( P.Serie = D.Serie and P.Folio = D.Folio )

	Update P Set Facturado = 0, UsoDeCFDI = '', Serie = '', Folio = '', RFC = '', IdCliente = '', NombreCliente = '', Identificador = 0 
	From #tmpDetalles P 
	Where StatusDocto = 'C' 	
	
	Update P Set PrecioUnitario_Facturacion = (PrecioUnitario * Porcenjate_Aplicar) 
	From #tmpDetalles P (NoLock) 
	
	Update P Set 
		SubTotal = (PrecioUnitario_Facturacion * Cantidad),  
		Iva = ((PrecioUnitario_Facturacion * Cantidad) * ( TasaIva / 100.00 )),  		
		Importe = ((PrecioUnitario_Facturacion * Cantidad) * ( 1 + ( TasaIva / 100.00 ))) 
	From #tmpDetalles P (NoLock) 	
	


	Update D Set SAT_ClaveProductoServicio = P.SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida = P.SAT_UnidadDeMedida
	From #tmpDetalles D (NoLock) 	
	Inner Join CatProductos_CFDI P (NoLock) On ( D.IdProducto = P.IdProducto ) 


--	select * from  #tmpDetalles 

--	select * from CatProductos_CFDI 

----------------------------------------- Obtener la informacion a procesar 


----	spp_INT_MA__FACT_GetInformacion_FacturarVenta  



------------------------------------------------ Salida final 
	
	Select	
		CodigoEAN, IdProducto, DescripcionComercial, 
		
		cast(D.PrecioUnitario as numeric(14, 2)) as PrecioUnitario_Venta, 
		Porcenjate, 				
		cast(D.PrecioUnitario_Facturacion as numeric(14, 2)) as PrecioUnitario, 
				
		cast(sum(D.Cantidad) as numeric(14, 2)) as Cantidad, 
		cast(sum(D.TasaIva) as numeric(14, 2)) as TasaIva, 
		cast(sum(D.SubTotal) as numeric(14, 2)) as SubTotal, 
		cast(sum(D.Iva) as numeric(14, 2)) as Iva, 
		cast(sum(D.Importe) as numeric(14, 2)) as Importe,  	

		@sUnidadDeMedida as UnidadDeMedida, 		
		SAT_ClaveProductoServicio, SAT_UnidadDeMedida,  
		Facturado, UsoDeCFDI, Serie, Folio, IdCliente, RFC, NombreCliente, Identificador  
		  
		
	From #tmpDetalles D (NoLock)	
	Group by 
		IdProducto, CodigoEAN, DescripcionComercial, D.PrecioUnitario, D.Porcenjate, D.PrecioUnitario_Facturacion, D.TasaIva, 
		D.Facturado, UsoDeCFDI, Serie, Folio, IdCliente, RFC, NombreCliente, Identificador, 
		SAT_ClaveProductoServicio, SAT_UnidadDeMedida     
	Order By DescripcionComercial 

------------------------------------------------ Salida final 

	
End 
Go--#SQL 
	