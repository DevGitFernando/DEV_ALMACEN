
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Remisiones' and xType = 'P')
    Drop Proc spp_Mtto_FACT_Remisiones
Go--#SQL
  
Create Proc spp_Mtto_FACT_Remisiones 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmaciaGenera varchar(4) = '0001', 
	@FolioRemision varchar(10) = '*' Output, @TipoDeRemision smallint = 1, @EsExcedente smallint = 0, @IdPersonalRemision varchar(4) = '0001', 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '0002', 
	@SubTotalSinGrabar numeric(14,4) = 100.0000, @SubTotalGrabado numeric(14,4) = 0.0000, 
	@Iva numeric(14,4) = 16.0000, @Total numeric(14,4) = 116.0000, 
	@Observaciones varchar(200) = 'SIN OBSERVACIONES', @iOpcion smallint = 1, @TipoInsumo varchar(2) = '02', @OrigenInsumo int = 0  
)
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

	/*EsExcedente
	1.- Excedio la Fuente de Financiamiento
	2.- Excedio el Financiamiento
	3.- Excedieron la Fuente de Financiamiento y el Financiamiento
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @FolioRemision = '*' 
	  Begin
		Select @FolioRemision = cast( (max(FolioRemision) + 1) as varchar) 
		From FACT_Remisiones (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
	  End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioRemision = IsNull(@FolioRemision, '1')
	Set @FolioRemision = right(replicate('0', 10) + @FolioRemision, 10)


	If @iOpcion = 1 
	  Begin 
		If Not Exists ( Select * From FACT_Remisiones (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision ) 
		  Begin 
			Insert Into FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeRemision, EsExcedente, IdPersonalRemision, IdPersonalValida, 
										IdFuenteFinanciamiento, IdFinanciamiento, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, 
										Observaciones, Status, Actualizado, TipoInsumo, OrigenInsumo ) 
			Select	@IdEmpresa, @IdEstado, @IdFarmaciaGenera, @FolioRemision, @TipoDeRemision, @EsExcedente, @IdPersonalRemision, @IdPersonalRemision, 
					@IdFuenteFinanciamiento, @IdFinanciamiento, @SubTotalSinGrabar, @SubTotalGrabado, @Iva, @Total, 
					@Observaciones, @sStatus, @iActualizado, @TipoInsumo, @OrigenInsumo 
		  End 
		Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio de Remisión ' + @FolioRemision 
	  End 
    Else 
	  Begin 
		Set @sStatus = 'C' 
		Update FACT_Remisiones Set Status = @sStatus, Actualizado = @iActualizado 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 
		Set @sMensaje = 'La información del Folio de Factura ' + @FolioRemision + ' ha sido cancelada satisfactoriamente.' 
	  End 

	-- Regresar la Clave Generada
    Select @FolioRemision as Folio, @sMensaje as Mensaje 


End
Go--#SQL
