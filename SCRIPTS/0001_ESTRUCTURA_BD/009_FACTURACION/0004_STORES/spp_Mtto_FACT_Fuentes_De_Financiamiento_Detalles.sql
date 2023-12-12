---------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles' and xType = 'P' )
    Drop Proc spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles 
Go--#SQL

/* 
	Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA    
		@IdFuenteFinanciamiento = "0002", @IdFinanciamiento = "0001", @ClaveSSA = "060.004.0109",  @CantidadPresupuestadaPiezas = "1", @CantidadPresupuestada = "0", @iOpcion = "1"    
*/ 
Create Proc spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles 
( 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '*', 
	@Descripcion varchar(100) = 'SEGURO POPULAR', @iMonto numeric(14,4) = 100.0000, @iPiezas int = 10, 
	@Prioridad int = 0, @Alias varchar(50) = '', @iOpcion smallint = 1 
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

	Set DateFormat YMD
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdFinanciamiento 
	Set @iActualizado = 0 


	If @IdFinanciamiento = '*' 
	  Begin
		Select @IdFinanciamiento = cast( (max(IdFinanciamiento) + 1) as varchar) 
		From FACT_Fuentes_De_Financiamiento_Detalles (NoLock) 
		Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento
	  End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdFinanciamiento = IsNull(@IdFinanciamiento, '1')
	Set @IdFinanciamiento = right(replicate('0', 4) + @IdFinanciamiento, 4)

	If @iOpcion = 2
	  Begin
		Set @sStatus = 'C'
		Set @sMensaje = 'La información del Financiamiento ' + @IdFinanciamiento + ' ha sido cancelada satisfactoriamente.' 
	  End
	

	If Not Exists ( Select * From FACT_Fuentes_De_Financiamiento_Detalles (NoLock) Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento ) 
	  Begin 
		 Insert Into FACT_Fuentes_De_Financiamiento_Detalles ( IdFuenteFinanciamiento, IdFinanciamiento, Descripcion, Prioridad, Monto, Piezas, Status, Actualizado, Alias ) 
		 Select @IdFuenteFinanciamiento, @IdFinanciamiento, @Descripcion, @Prioridad, @iMonto, @iPiezas, @sStatus, @iActualizado, @Alias 
	  End 
	Else 
	  Begin 
		 Update FACT_Fuentes_De_Financiamiento_Detalles Set Descripcion = @Descripcion, Prioridad = @Prioridad, Monto = @iMonto, Piezas = @iPiezas, 
				Status = @sStatus, Actualizado = @iActualizado, Alias = @Alias 
		 Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento  
	  End 


--- Aplicar cambios a la Administración 
	Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Admon_Detalles 
		@IdFuenteFinanciamiento, @IdFinanciamiento, 
		@Descripcion, @iMonto, @iPiezas, @iOpcion, 0 


	-- Regresar la Clave Generada 
    Select @IdFinanciamiento as Clave, @sMensaje as Mensaje 
    
End
Go--#SQL


---------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_Poliza' and xType = 'P')
    Drop Proc spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_Poliza
Go--#SQL
  
Create Proc spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_Poliza 
( 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '*', 
	@ValidarPolizaBeneficiario bit = 0, @LongitudMinimaPoliza int = 0, @LongitudMaximaPoliza  int = 0 	
)
With Encryption 
As
Begin
Set NoCount On

	 Update FACT_Fuentes_De_Financiamiento_Detalles 
		Set ValidarPolizaBeneficiario = @ValidarPolizaBeneficiario, 
			LongitudMinimaPoliza = @LongitudMinimaPoliza, 
			LongitudMaximaPoliza = @LongitudMaximaPoliza 
	 Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento  

End
Go--#SQL

