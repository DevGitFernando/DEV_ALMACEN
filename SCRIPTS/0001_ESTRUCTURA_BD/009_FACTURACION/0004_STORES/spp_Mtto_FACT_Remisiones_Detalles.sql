
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Remisiones_Detalles' and xType = 'P')
    Drop Proc spp_Mtto_FACT_Remisiones_Detalles
Go--#SQL
  
Create Proc spp_Mtto_FACT_Remisiones_Detalles ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmaciaGenera varchar(4) = '0001', 
	@IdFarmacia varchar(4) = '0034', @IdSubFarmacia varchar(2) = '01', @FolioVenta varchar(8) = '00001234', @FolioRemision varchar(10) = '0000000001', 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '0001', 
	@IdPrograma varchar(4) = '0002', @IdSubPrograma varchar(4) = '0005',	
	@ClaveSSA varchar(20) = '101', @IdProducto varchar(8) = '00000352', @CodigoEAN varchar(30) = '7501165005709', @ClaveLote varchar(30) = 'Lote1', 
	@PrecioLicitado numeric(14,4) = '100.0000', @Cantidad numeric(14,4) = '10.0000', 
	@TasaIva numeric(14,4) = '16.0000', @SubTotalSinGrabar numeric(14,4) = '100.0000', 
	@SubTotalGrabado numeric(14,4) = '200.0000', @Iva numeric(14,4) = '16.0000', @Importe numeric(14,4) = '116.0000' , 
	@iOpcion smallint = 1)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''

	If @iOpcion = 1 
	  Begin 
		If Not Exists ( Select * From FACT_Remisiones_Detalles (NoLock) 
						Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera 
							And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
							And FolioVenta = @FolioVenta And FolioRemision = @FolioRemision 
							And IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento
							And IdPrograma = @IdPrograma And IdSubPrograma = @IdSubPrograma 
							And ClaveSSA = @ClaveSSA And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote) 
		  Begin 
			Insert Into FACT_Remisiones_Detalles ( IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, 
												FolioVenta, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
												IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, 
												Cantidad, TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
			Select	@IdEmpresa, @IdEstado, @IdFarmaciaGenera, @IdFarmacia, @IdSubFarmacia, 
					@FolioVenta, @FolioRemision, @IdFuenteFinanciamiento, @IdFinanciamiento, 
					@IdPrograma, @IdSubPrograma, @ClaveSSA, @IdProducto, @CodigoEAN, @ClaveLote, @PrecioLicitado, 
					@Cantidad, @TasaIva, @SubTotalSinGrabar, @SubTotalGrabado, @Iva, @Importe 
		  End 
		Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio de Factura ' + @FolioRemision 
	  End 

	-- Regresar la Clave Generada
    Select @FolioRemision as Folio, @sMensaje as Mensaje 
End
Go--#SQL
