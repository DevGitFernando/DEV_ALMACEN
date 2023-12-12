---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Documentos_Generados_VP' and xType = 'P' )
   Drop Proc spp_Mtto_FACT_CFD_Documentos_Generados_VP 
Go--#SQL 

--	select * from FACT_CFD_Documentos_Generados

Create Proc spp_Mtto_FACT_CFD_Documentos_Generados_VP 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@IdTipoDocumento varchar(3) = '001', @IdentificadorSerie int = 0, 
	@Serie varchar(10) = '', @NombreDocumento varchar(50) = '', @Folio int = 0, 
	@Importe numeric(14,4) = 0, 
	@RFC varchar(15) = '', @NombreReceptor varchar(250) = '', 
	@IdCFDI varchar(100) = '', 
	@Status varchar(2) = 'A', @IdPersonalCancela varchar(6) = '', 
	@Observaciones_01 varchar(1000) = '',  @Observaciones_02 varchar(1000) = '',  
	@Observaciones_03 varchar(1000) = '', @Referencia varchar(500) = '', 
	@XMLFormaPago varchar(500) = '', 
	@XMLCondicionesPago varchar(500) = '', 
	@XMLMetodoPago varchar(500) = '', 
	@TipoDocumento smallint = 0, @TipoInsumo smallint = 0, 
	@IdRubroFinanciamiento varchar(4)  = '', @IdFuenteFinanciamiento varchar(4) = '', 
	@IdPersonalEmite varchar(6) = '', 
	@UsoDeCFDI varchar(6) = ''   	  	
) 
As 
Begin 
Set NoCount On 

	If @Status = 'A' 
		Begin 
			If Not Exists 
				( 
					Select * From FACT_CFD_Documentos_Generados_VP (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						  and Serie = @Serie and Folio = @Folio 
				) 
			Begin 
				Insert Into FACT_CFD_Documentos_Generados_VP ( IdEmpresa, IdEstado, IdFarmacia, FechaRegistro, 
						IdTipoDocumento, IdentificadorSerie, Serie, NombreDocumento, Folio, Importe, 
						RFC, NombreReceptor, IdCFDI, Status, IdPersonalEmite, IdPersonalCancela, FechaCancelacion,  
						Observaciones_01, Observaciones_02, Observaciones_03, Referencia, 
					    XMLFormaPago, XMLCondicionesPago, XMLMetodoPago, TipoDocumento, TipoInsumo, IdRubroFinanciamiento, IdFuenteFinanciamiento, 
						UsoDeCFDI 
						) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, getdate() as FechaRegistro, @IdTipoDocumento, @IdentificadorSerie, 
					   @Serie, @NombreDocumento, @Folio, @Importe, 
					   @RFC, @NombreReceptor, @IdCFDI, @Status, @IdPersonalEmite, '' as IdPersonalCancela, getdate() as FechaCancelacion, 
					   @Observaciones_01, @Observaciones_02, @Observaciones_03, @Referencia, 
					   @XMLFormaPago, @XMLCondicionesPago, @XMLMetodoPago, @TipoDocumento, @TipoInsumo, @IdRubroFinanciamiento, @IdFuenteFinanciamiento, 
					   @UsoDeCFDI  
			End 
		End 
	Else 
		Begin 
			Update F Set Status = 'C', IdPersonalCancela = @IdPersonalCancela, FechaCancelacion = getdate() 
			From FACT_CFD_Documentos_Generados_VP F (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				  and Serie = @Serie and Folio = @Folio			  
		End 


---------------- SALIDA FINAL  
	Select Keyx as FolioDocumento 
	From FACT_CFD_Documentos_Generados_VP (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and Serie = @Serie and Folio = @Folio 

End 
Go--#SQL 

--		sp_listacolumnas FACT_CFD_Documentos_Generados 



---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Documentos_Generados_Detalles_VP' and xType = 'P' )
   Drop Proc spp_Mtto_FACT_CFD_Documentos_Generados_Detalles_VP  
Go--#SQL 

Create Proc spp_Mtto_FACT_CFD_Documentos_Generados_Detalles_VP 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = '', @Folio int = 0, 
	@Identificador varchar(10) = '', @Clave varchar(20) = '', @DescripcionConcepto varchar(500) = '',   
	@UnidadDeMedida varchar(50) = '', @Cantidad numeric(14,4) = 0, 	
	@PrecioUnitario numeric(14,4) = 0, @TasaIva numeric(14,4) = 0, 
	@SubTotal numeric(14,4) = 0, @Iva numeric(14,4) = 0, @Total numeric(14,4) = 0, @TipoImpuesto varchar(20) = '',
	@Partida int = 1, 
	@SAT_ClaveProducto_Servicio varchar(20) = '', @SAT_UnidadDeMedida varchar(5) = '' 
) 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From FACT_CFD_Documentos_Generados_Detalles_VP (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						  and Serie = @Serie and Folio = @Folio and Identificador = @Identificador and Partida = @Partida  ) 
	Begin 
		Insert Into FACT_CFD_Documentos_Generados_Detalles_VP ( 
			   IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Partida, 
			   Identificador, Clave, DescripcionConcepto, UnidadDeMedida, Cantidad, PrecioUnitario, TasaIva, SubTotal, Iva, Total, TipoImpuesto, 
			   SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida ) 
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @Partida, 
			   @Identificador, @Clave, @DescripcionConcepto, @UnidadDeMedida, @Cantidad, @PrecioUnitario, @TasaIva, @SubTotal, @Iva, @Total, @TipoImpuesto, 
			   @SAT_ClaveProducto_Servicio, @SAT_UnidadDeMedida
	End 

	
End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago_VP' and xType = 'P' ) 
	Drop Proc spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago_VP    
Go--#SQL  

Create Proc spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago_VP 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = '', @Folio int = 0, 	
	@IdMetodoDePago varchar(2) = '', @Importe numeric(14,4) = 0, @Referencia varchar(10) = '' 
) 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Insert Into FACT_CFD_Documentos_Generados_MetodosDePago_VP 
	(
		IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdMetodoPago, Importe, Referencia  	 
	) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @IdMetodoDePago, @Importe, @Referencia 
		
End 
Go--#SQL 