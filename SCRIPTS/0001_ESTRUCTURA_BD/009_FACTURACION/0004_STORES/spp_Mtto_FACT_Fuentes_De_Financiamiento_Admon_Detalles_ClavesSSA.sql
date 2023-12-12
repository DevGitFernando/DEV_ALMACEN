
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA' and xType = 'P')
    Drop Proc spp_Mtto_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA
Go--#SQL
  
Create Proc spp_Mtto_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA 
( 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '0001', 
	@ClaveSSA varchar(50) = '101', 
	@Costo numeric(14,4) = 0, @Agrupacion int = 1, 
	@CostoUnitario numeric(14,4) = 0, @TasaIva numeric(14,4) = 0, @Iva Numeric(14,4) = 0, @ImporteNeto numeric(14,4) = 0, 
	@iOpcion smallint = 1 
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

	If @iOpcion = 2
	  Begin
		Set @sStatus = 'C'
		Set @sMensaje = 'Las Claves SSA del Financiamiento ' + @IdFinanciamiento + ' han sido canceladas satisfactoriamente.' 
	  End

	If Not Exists (	Select * From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA (NoLock) 
					Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento 
						And ClaveSSA = @ClaveSSA ) 
	  Begin 
		 Insert Into FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA 
		 ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, Status, Actualizado ) 
		 Select @IdFuenteFinanciamiento, @IdFinanciamiento, @ClaveSSA, @Costo, @Agrupacion, @CostoUnitario, @TasaIva, @Iva, @ImporteNeto, @sStatus, @iActualizado 
	  End 
	Else
	  Begin			
		Update FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA 
			Set 
				Costo = @Costo, Agrupacion = @Agrupacion, 
				CostoUnitario = @CostoUnitario, TasaIva = @TasaIva, Iva = @Iva, ImporteNeto = @ImporteNeto, 
				Status = @sStatus, Actualizado = @iActualizado 
		Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento And ClaveSSA = @ClaveSSA 
	  End

	-- Regresar la Clave Generada
    Select @ClaveSSA as Clave, @sMensaje as Mensaje 
End
Go--#SQL
