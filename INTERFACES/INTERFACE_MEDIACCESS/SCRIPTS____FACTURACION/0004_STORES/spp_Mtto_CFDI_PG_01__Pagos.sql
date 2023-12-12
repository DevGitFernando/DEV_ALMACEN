-------------------------------------------------------------------------------------------------------------------------------
----If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFDI_PG_01__Pagos' and xType = 'P' )
----   Drop Proc spp_Mtto_FACT_CFDI_PG_01__Pagos 
----Go--#SQL 

------	select * from FACT_CFD_Documentos_Generados

----Create Proc spp_Mtto_FACT_CFDI_PG_01__Pagos 
----( 
----	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
----	@UsoDeCFDI varchar(6) = '', 
----	@IdTipoDocumento varchar(3) = '001', @IdentificadorSerie int = 0, 
----	@Serie varchar(10) = '', @Folio int = 0, 
----	@RFC varchar(15) = '', @NombreReceptor varchar(250) = '', 
----	@Status varchar(2) = 'A', @IdPersonalCancela varchar(6) = '', 
----	@Observaciones_01 varchar(1000) = '',  @Observaciones_02 varchar(1000) = '',  
----	@Observaciones_03 varchar(1000) = '', 
----	@XMLFormaPago varchar(500) = '', 
----	@XMLMetodoPago varchar(500) = '', 
----	@IdPersonalEmite varchar(6) = '' 	
----) 
----As 
----Begin 
----Set NoCount On 

----	If @Status = 'A' 
----		Begin 
----			If Not Exists 
----				( 
----					Select * From FACT_CFDI_ComplementoDePagos (NoLock) 
----					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
----						  and Serie = @Serie and Folio = @Folio 
----				) 
----			Begin 
----				Insert Into FACT_CFDI_ComplementoDePagos ( IdEmpresa, IdEstado, IdFarmacia, FechaRegistro, 
----						IdTipoDocumento, IdentificadorSerie, Serie, Folio, 
----						RFC, NombreReceptor, Status, IdPersonalEmite, IdPersonalCancela, FechaCancelacion,  
----						Observaciones_01, Observaciones_02, Observaciones_03,  
----					    XMLFormaPago, XMLMetodoPago, UsoDeCFDI 
----						) 
----				Select @IdEmpresa, @IdEstado, @IdFarmacia, getdate() as FechaRegistro, @IdTipoDocumento, @IdentificadorSerie, 
----					   @Serie, @Folio,  
----					   @RFC, @NombreReceptor, @Status, @IdPersonalEmite, '' as IdPersonalCancela, getdate() as FechaCancelacion, 
----					   @Observaciones_01, @Observaciones_02, @Observaciones_03,  
----					   @XMLFormaPago, @XMLMetodoPago, @UsoDeCFDI    
----			End 
----		End 
----	Else 
----		Begin 
----			Update F Set Status = 'C', IdPersonalCancela = @IdPersonalCancela, FechaCancelacion = getdate() 
----			From FACT_CFDI_ComplementoDePagos F (NoLock) 
----			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
----				  and Serie = @Serie and Folio = @Folio			  
----		End 


-------------------- SALIDA FINAL  
----	Select Keyx as FolioDocumento 
----	From FACT_CFDI_ComplementoDePagos (NoLock) 
----	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
----		and Serie = @Serie and Folio = @Folio 

----End 
----Go--#xSQL 


-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_PG_02__Pagos_Detalles' and xType = 'P' )
   Drop Proc spp_Mtto_CFDI_PG_02__Pagos_Detalles  
Go--#SQL 

Create Proc spp_Mtto_CFDI_PG_02__Pagos_Detalles 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = '', @Folio int = 0, 
	
	@FechaDePago varchar(10) = '', @HoraDePago varchar(8) = '', 
	@FormaDePago varchar(4) = '', 
	@Moneda varchar(6) = '', @TipoDeCambio numeric(14,4) = 0, @MontoDePago numeric(14,4) = 0, 
	@NumeroDeOperacion varchar(50) = '', 

	@RfcEmisorCtaOrd varchar(20) = '', --- Origen de pago 
	@NomBancoOrdExt varchar(200) = '', --- Origen de pago 
	@CtaOrdenante varchar(50) = '',	 --- Origen de pago 
	@RfcEmisorCtaBen varchar(20) = '', --- Repector de pago 
	@CtaBeneficiario varchar(50) = '', --- Repector de pago 

	@TipoCadPago varchar(4) = '',			--- Informacion tipo CFDI 
	@CertificadoPago varchar(500) = '',   --- Informacion tipo CFDI 
	@CadenaPago varchar(500) = '',		--- Informacion tipo CFDI 
	@SelloPago varchar(500) = '' 			--- Informacion tipo CFDI 
) 
As 
Begin 
Set NoCount On  

--		sp_listacolumnas FACT_CFDI_ComplementoDePagos_Detalles 

	If Not Exists ( Select * From CFDI_ComplementoDePagos_Detalles (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio ) 
	Begin 
		Insert Into CFDI_ComplementoDePagos_Detalles 
		( 
			IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, FechaDePago, FormaDePago, Moneda, TipoCambio, Monto, NumeroDeOperacion, 
			RfcEmisorCtaOrd, NomBancoOrdExt, CtaOrdenante, RfcEmisorCtaBen, CtaBeneficiario, TipoCadPago, CertificadoPago, CadenaPago, SelloPago
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @FechaDePago, @FormaDePago, @Moneda, @TipoDeCambio, @MontoDePago, @NumeroDeOperacion, 
			@RfcEmisorCtaOrd, @NomBancoOrdExt, @CtaOrdenante, @RfcEmisorCtaBen, @CtaBeneficiario, @TipoCadPago, @CertificadoPago, @CadenaPago, @SelloPago 


		--Insert Into FACT_CFD_Documentos_Generados_Detalles ( 
		--	   IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Partida, 
		--	   Identificador, Clave, DescripcionConcepto, UnidadDeMedida, Cantidad, PrecioUnitario, TasaIva, SubTotal, Iva, Total, TipoImpuesto, 
		--	   SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida ) 
		--Select @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, 1 as Partida, 
		--	   'PAGO', '84111506', 'Pago' as DescripcionConcepto, 'ACT' as UnidadDeMedida, 1 as Cantidad, 
		--	   0 as PrecioUnitario, 0 as TasaIva, 0 as SubTotal, 0 as Iva, 0 as Total, 'IVA' as TipoImpuesto, 
		--	   '84111506' as SAT_ClaveProducto_Servicio, 'ACT' as SAT_UnidadDeMedida

		Insert Into CFDI_Documentos_Conceptos ( 
			   GUID, IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Partida, 
			   IdProducto, CodigoEAN, UnidadDeMedida, Cantidad, 
			   PrecioUnitario, DescuentoPorc, PrecioUnitarioFinal, 
			   TasaIva, SubTotal, Iva, Importe, TipoImpuesto, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida ) 
		Select '' As GUID, @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio,1 as Partida, 
			   ''  As IdProducto, '' As CodigoEAN, 'ACT' as UnidadDeMedida, 1 As Cantidad, 
			   0 As PrecioUnitario, 0 As DescuentoPorc, 0 As PrecioUnitarioFinal,
			   0 AS TasaIva, 0 As SubTotal, 0 As Iva, 0 As Total, 'IVA' As TipoImpuesto, '84111506' as SAT_ClaveProducto_Servicio, 'ACT' as SAT_UnidadDeMedida  

	End 
	
End 
Go--#SQL 



-----------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_PG_03__Pagos_DoctosRelacionados' and xType = 'P' )
   Drop Proc spp_Mtto_CFDI_PG_03__Pagos_DoctosRelacionados  
Go--#SQL 

Create Proc spp_Mtto_CFDI_PG_03__Pagos_DoctosRelacionados 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001',   
	@Serie varchar(10) = '', @Folio int = 0, 
	
	@Serie_Relacionada varchar(10) = '', @Folio_Relacionado int = 0, @UUID_Relacionado varchar(50) = '', 
	@Moneda varchar(10) = '', @TipoCambio numeric(14,4) = 1, @MetodoDePago varchar(10) = '', 
	@NumParcialidad int = 1, @Importe_SaldoAnterior numeric(14, 4) = 0, @Importe_Pagado numeric(14, 4) = 0, @Importe_SaldoInsoluto numeric(14, 4) = 0  

) 
As 
Begin 
Set NoCount On  


--		sp_listacolumnas FACT_CFDI_ComplementoDePagos_DoctosRelacionados 

	If Not Exists ( Select * From CFDI_ComplementoDePagos_DoctosRelacionados (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						  and Serie = @Serie and Folio = @Folio and Serie_Relacionada = @Serie_Relacionada and Folio_Relacionado = @Folio_Relacionado ) 
	Begin 
		Insert Into CFDI_ComplementoDePagos_DoctosRelacionados 
		( 
			IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Serie_Relacionada, Folio_Relacionado, UUID_Relacionado, Moneda, TipoCambio, 
			MetodoDePago, NumParcialidad, Importe_SaldoAnterior, Importe_Pagado, Importe_SaldoInsoluto
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @Serie_Relacionada, @Folio_Relacionado, @UUID_Relacionado, 
			@Moneda, @TipoCambio, @MetodoDePago, @NumParcialidad, @Importe_SaldoAnterior, @Importe_Pagado, @Importe_SaldoInsoluto
	End 


End 
Go--#SQL 

