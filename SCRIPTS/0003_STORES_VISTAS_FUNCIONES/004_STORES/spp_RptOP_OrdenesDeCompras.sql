---------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_RptOP_OrdenesDeCompras' and xType = 'P' ) 
   Drop Proc spp_RptOP_OrdenesDeCompras 
Go--#SQL 

Create Proc spp_RptOP_OrdenesDeCompras
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4 )= '3182', @IdProveedor varchar(4) = '',
	@FechaInicial varchar(10) = '2018-01-01', @FechaFin varchar(10) = '2018-04-20', @iTipo int = 2 
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare @sSQl varchar(800),				
		@sWhereProveedor varchar(300)
		
	Set @sWhereProveedor = '' 
	Set @IdEmpresa = RIGHT('000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('000000' + @IdEstado, 2) 
	Set @IdFarmacia = RIGHT('000000' + @IdFarmacia, 4) 


	If ( @IdProveedor <> '' and @IdProveedor <> '0000' )
		Begin
			Set @sWhereProveedor = ' and IdProveedor = ' + Char(39) + @IdProveedor + Char(39) 
		End 


	If (@iTipo = 1) 
		Begin
			
			Select top 0 * 
			Into #tmp__vw_Impresion_Recepcion_Orden_Compra 
			From vw_Impresion_Recepcion_Orden_Compra 

			Select top 0 * 
			Into #tmp__vw_OrdenesDeComprasEnc 
			From vw_OrdenesDeComprasEnc 

			Set @sSql = 'Insert Into #tmp__vw_Impresion_Recepcion_Orden_Compra  ' + 
						'Select * ' + 
						'From vw_Impresion_Recepcion_Orden_Compra (Nolock) ' + 
						'Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + '  and IdEstado = ' + Char(39) + @IdEstado + Char(39) + 
						'  and IdFarmacia = ' + Char(39)+ @IdFarmacia + Char(39)+ '  and ' + 
						'Convert(varchar(10), FechaRegistro, 120) Between ' + 
						Char(39)+ @FechaInicial + Char(39) + '  and ' + Char(39) + @FechaFin + Char(39) + @sWhereProveedor + 
						' Order By FechaRegistro, Folio' 
			Exec(@sSql) 

			Set @sSql = 'Insert Into #tmp__vw_OrdenesDeComprasEnc  ' + 			
						'Select * ' +  
						'From vw_OrdenesDeComprasEnc (Nolock) ' + 
						'Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + '  and IdEstado = ' + Char(39) + @IdEstado + Char(39) + 
						'  and IdFarmacia = ' + Char(39)+ @IdFarmacia + Char(39)+ '  and ' + 
						'Convert(varchar(10), FechaRegistro, 120) Between ' + 
						Char(39)+ @FechaInicial + Char(39) + '  and ' + Char(39) + @FechaFin + Char(39) + @sWhereProveedor + 
						' Order By FechaRegistro ' 
			Exec(@sSql) 


			-------------------------- SALIDA FINAL 
			Select 
				--IdEmpresa, Empresa, 
				IdEstado, Estado, 
				IdFarmacia, Farmacia, Folio, 'Folio de Orden de Compra asociada' = FolioOrdenCompraReferencia, 'Fecha de Orden de Compra' = FechaGeneracionOC, 
				-- EstadoGenera, FarmaciaGenera, FolioMovtoInv, 
				IdPersonal, 'Nombre del personal' = NombrePersonal, FechaRegistro, -- FechaSistema, 
				--IdProveedor, 
				Proveedor, EsFacturaOriginal, 
				'Número de documento' = ReferenciaDocto, 'Fecha del documento' = FechaDocto, 'Vencimiento del documento' = FechaVenceDocto, 
				Observaciones, 
				SubTotal, Iva, Total, ImporteFactura, FechaPromesaEntrega, Status
			From #tmp__vw_OrdenesDeComprasEnc 

			Select 
				-- IdEmpresa, Empresa, 
				IdEstado, Estado,  
				IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
				Folio, 'Folio de Orden de Compra asociada' = FolioOrdenCompraReferencia, 'Fecha registro' = FechaRegistro, 
				IdPersonal, 'Nombre del personal' = NombrePersonal, Observaciones, 
				--IdProveedor, 
				Proveedor, 
				'Número de documento' = ReferenciaDocto, 'Fecha del documento' = FechaDocto, 'Vencimiento del documento' = FechaVenceDocto, 
				-- SubTotal, Iva, Total, ImporteFactura, Status, 
				IdClaveSSA_Sal, ClaveSSA, 'Descripción clave' = DescripcionSal, 
				IdProducto, 'Descripción comercial' = DescripcionProducto, CodigoEAN, ClaveLote, CostoUnitario, CantidadLote, 
				TasaIva, SubTotalLote, ImpteIvaLote, ImporteLote, FechaCaducidad, 'Fecha primer ingreso del lote' = FechaRegistro  
			From #tmp__vw_Impresion_Recepcion_Orden_Compra 
			-------------------------- SALIDA FINAL 

		End 
	Else 
		Begin 

			
			Select top 0 * 
			Into #tmp__vw_Impresion_Devolucion_Orden_Compra 
			From vw_Impresion_Devolucion_Orden_Compra 

			Select top 0 * 
			Into #tmp__vw_DevolucionesEnc_Orden_Compra 
			From vw_DevolucionesEnc_Orden_Compra 

			
			Set @sSql = 'Insert Into #tmp__vw_Impresion_Devolucion_Orden_Compra ' + 
						'Select * ' + 
						'From vw_Impresion_Devolucion_Orden_Compra (Nolock) ' + 
						'Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + '  and IdEstado = ' + Char(39) + @IdEstado + Char(39) + 
						'  and IdFarmacia = ' + Char(39)+ @IdFarmacia + Char(39)+ '  and ' + 
						'Convert(varchar(10), FechaRegistro, 120) Between ' + 
						Char(39)+ @FechaInicial + Char(39) + '  and ' + Char(39) + @FechaFin + Char(39) + @sWhereProveedor + 
						' Order By FechaRegistro, Folio' 
			Exec(@sSql) 
			
			Set @sSql = 'Insert Into #tmp__vw_DevolucionesEnc_Orden_Compra ' + 
						'Select * ' + 
						'From vw_DevolucionesEnc_Orden_Compra (Nolock) ' + 
						'Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + '  and IdEstado = ' + Char(39) + @IdEstado + Char(39) + 
						'  and IdFarmacia = ' + Char(39)+ @IdFarmacia + Char(39)+ '  and ' + 
						'Convert(varchar(10), FechaRegistro, 120) Between ' + 
						Char(39)+ @FechaInicial + Char(39) + '  and ' + Char(39) + @FechaFin + Char(39) + @sWhereProveedor + 
						' Order By FechaRegistro ' 
			Exec(@sSql) 


			-------------------------- SALIDA FINAL 
			Select 
						--IdEmpresa, Empresa, 
				IdEstado, Estado, 
				IdFarmacia, Farmacia, Folio, 
				TipoDeDevolucion, NombreTipoDeDevolucion, 
				'Folio de Orden de Compra' = FolioOrdenCompra, 
				'Folio de Orden de Compra asociada' = ReferenciaFolioOrdenCompra,  
				-- EstadoGenera, FarmaciaGenera, FolioMovtoInv, 
				IdPersonal, 'Nombre del personal' = NombrePersonal, FechaRegistro, -- FechaSistema, 
				--IdProveedor, 
				Proveedor, 
				'Número de documento' = ReferenciaDocto, 'Fecha del documento' = FechaDocto, 'Vencimiento del documento' = FechaVenceDocto, 
				Observaciones, 
				SubTotal, Iva, Total, Status	 
			From #tmp__vw_DevolucionesEnc_Orden_Compra 

			Select 
				-- IdEmpresa, Empresa, 
				IdEstado, Estado,  
				IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
				Folio, 
				'Folio de Orden de Compra' = FolioOrdenCompra, 
				'Folio de Orden de Compra asociada' = ReferenciaFolioOrdenCompra,  	
				'Fecha registro' = FechaSistema, 
				IdPersonal, 'Nombre del personal' = NombrePersonal, Observaciones, 
				--IdProveedor, 
				Proveedor, 
				'Número de documento' = ReferenciaDocto, 'Fecha del documento' = FechaDocto, 'Vencimiento del documento' = FechaVenceDocto, 
				-- SubTotal, Iva, Total, ImporteFactura, Status, 
				IdClaveSSA_Sal, ClaveSSA, 'Descripción clave' = DescripcionSal, 
				IdProducto, 'Descripción comercial' = DescripcionProducto, CodigoEAN, ClaveLote, 'CostoUnitario' = PrecioCosto_Unitario, CantidadLote, 
				TasaIva, SubTotalLote, ImpteIvaLote, ImporteLote, FechaCaducidad, 'Fecha primer ingreso del lote' = FechaRegistro  			 
			From #tmp__vw_Impresion_Devolucion_Orden_Compra 
			-------------------------- SALIDA FINAL 

		End 
	


End 
Go--#SQL