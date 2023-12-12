---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Documentos_Generados' and xType = 'P' )
   Drop Proc spp_Mtto_CFDI_Documentos_Generados 
Go--#SQL 

--	select * from CFDI_Documentos  

Create Proc spp_Mtto_CFDI_Documentos_Generados 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@IdTipoDocumento varchar(3) = '001', @IdentificadorSerie int = 0, 
	@Serie varchar(10) = '', @NombreDocumento varchar(50) = '', @Folio int = 0, 
	@Importe numeric(14,4) = 0, 
	@RFC varchar(15) = '', @NombreReceptor varchar(250) = '', 
	@IdCFDI varchar(100) = '', 
	@Status varchar(2) = 'A', @IdPersonalCancela varchar(6) = '', 
	@Observaciones_01 varchar(500) = '',  @Observaciones_02 varchar(500) = '',  
	@Observaciones_03 varchar(500) = '', @Referencia varchar(500) = '', 
	@XMLFormaPago varchar(500) = '', 
	@XMLCondicionesPago varchar(500) = '', 
	@XMLMetodoPago varchar(500) = '', 
	@TipoDocumento smallint = 0, @TipoInsumo smallint = 0, 
	@IdPersonalEmite varchar(6) = '', @GUID varchar(100) = '',
	@IdFarmaciaReferencia Varchar(4) = '', @IdEstadoReferencia varchar(2) = '', 
	@UsoDeCFDI varchar(6) = '' 
) 
As 
Begin 
Set NoCount On


	If (@IdFarmaciaReferencia = '')
	Begin
		Set @IdFarmaciaReferencia = @IdFarmacia
	End
		
	If (@IdEstadoReferencia = '')
	Begin
		Set @IdEstadoReferencia = @IdEstado
	End

	If @Status = 'A' 
		Begin 
			If Not Exists 
				( 
					Select * From CFDI_Documentos (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						  and Serie = @Serie and Folio = @Folio 
				) 
			Begin 
				Insert Into CFDI_Documentos ( GUID, IdEmpresa, IdEstado, IdFarmacia, FechaRegistro, 
						IdTipoDocumento, IdentificadorSerie, Serie, NombreDocumento, Folio, Importe, 
						RFC, NombreReceptor, IdCFDI, Status, IdPersonalEmite, IdPersonalCancela, FechaCancelacion,  
						Observaciones_01, Observaciones_02, Observaciones_03, Referencia, 
					    XMLFormaPago, XMLCondicionesPago, XMLMetodoPago, TipoDocumento, TipoInsumo, IdFarmaciaReferencia, IdEstadoReferencia, UsoDeCFDI 
						) 
				Select @GUID, @IdEmpresa, @IdEstado, @IdFarmacia, getdate() as FechaRegistro, @IdTipoDocumento, @IdentificadorSerie, 
					   @Serie, @NombreDocumento, @Folio, @Importe, 
					   @RFC, @NombreReceptor, @IdCFDI, @Status, @IdPersonalEmite, '' as IdPersonalCancela, getdate() as FechaCancelacion, 
					   @Observaciones_01, @Observaciones_02, @Observaciones_03, @Referencia, 
					   @XMLFormaPago, @XMLCondicionesPago, @XMLMetodoPago, @TipoDocumento, @TipoInsumo, @IdFarmaciaReferencia, @IdEstadoReferencia, @UsoDeCFDI 
			End 
		End 
	Else 
		Begin 
			Update F Set Status = 'C', IdPersonalCancela = @IdPersonalCancela, FechaCancelacion = getdate() 
			From CFDI_Documentos F (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				  and Serie = @Serie and Folio = @Folio			  
		End 


---------------- SALIDA FINAL  
	Select Keyx as FolioDocumento 
	From CFDI_Documentos (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and Serie = @Serie and Folio = @Folio 


End 
Go--#SQL 

--		sp_listacolumnas CFDI_Documentos 

--		sp_listacolumnas__stores spp_Mtto_CFDI_Documentos_Generados_Formatos, 1 

---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Documentos_Generados_Formatos' and xType = 'P' )
   Drop Proc spp_Mtto_CFDI_Documentos_Generados_Formatos  
Go--#SQL 

Create Proc spp_Mtto_CFDI_Documentos_Generados_Formatos 
( 
	@IdCFDI varchar(100) = '', @XML text = null, @PDF text = null 
) 
As 
Begin 
Set NoCount On 
	
	----Update D Set FormatoXML = @XML, FormatoPDF = @PDF, TieneXML = 1, TienePDF = 1  
	----From CFDI_Documentos D (NoLock) 
	----Where IdCFDI = @IdCFDI 
		
End 
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Documentos_Generados_Detalles' and xType = 'P' )
   Drop Proc spp_Mtto_CFDI_Documentos_Generados_Detalles  
Go--#SQL 

Create Proc spp_Mtto_CFDI_Documentos_Generados_Detalles 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = '', @Folio int = 0, 
	@Partida int = 0, 
	@IdProducto varchar(8) = '', @CodigoEAN varchar(30) = '', 
	@UnidadDeMedida varchar(50) = '', @Cantidad numeric(14,4) = 0, 	
	@PrecioUnitario numeric(14,4) = 0, @DescuentoPorc numeric(14,4) = 0, @PrecioUnitarioFinal numeric(14,4) = 0,
	@TasaIva numeric(14,4) = 0, 
	@SubTotal numeric(14,4) = 0, @Iva numeric(14,4) = 0, @Total numeric(14,4) = 0, @TipoImpuesto varchar(20) = '', 
	@GUID varchar(100) = '', 
	@SAT_ClaveProducto_Servicio varchar(20) = '', @SAT_UnidadDeMedida varchar(5) = '' 	  
) 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFDI_Documentos_Conceptos (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						  and Serie = @Serie and Folio = @Folio and 
						  Partida = @Partida and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN ) 
	Begin 
		Insert Into CFDI_Documentos_Conceptos ( 
			   GUID, IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Partida, 
			   IdProducto, CodigoEAN, UnidadDeMedida, Cantidad, 
			   PrecioUnitario, DescuentoPorc, PrecioUnitarioFinal, 
			   TasaIva, SubTotal, Iva, Importe, TipoImpuesto, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida ) 
		Select @GUID, @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @Partida, 
			   @IdProducto, @CodigoEAN, @UnidadDeMedida, @Cantidad, 
			   @PrecioUnitario, @DescuentoPorc, @PrecioUnitarioFinal,
			   @TasaIva, @SubTotal, @Iva, @Total, @TipoImpuesto, @SAT_ClaveProducto_Servicio, @SAT_UnidadDeMedida  
	End 
	
End 
Go--#SQL 

--	sp_listacolumnas CFDI_Documentos_Detalles  

--	sp_listacolumnas__stores spp_Mtto_CFDI_Documentos_Generados_Detalles , 1 


------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Documentos_MetodosDePago' and xType = 'P' ) 
	Drop Proc spp_Mtto_CFDI_Documentos_MetodosDePago    
Go--#SQL  

Create Proc spp_Mtto_CFDI_Documentos_MetodosDePago 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = '', @Folio int = 0, 	
	@IdMetodoDePago varchar(2) = '', @Importe numeric(14,4) = 0, @Referencia varchar(10) = '', 	
	@GUID varchar(100) = ''    									
) 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Insert Into CFDI_Documentos_MetodosDePago 
	(
		GUID, IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdMetodoDePago, Importe, Referencia  	 
	) 
	Select @GUID, @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @IdMetodoDePago, @Importe, @Referencia 
		
End 
Go--#SQL 
	