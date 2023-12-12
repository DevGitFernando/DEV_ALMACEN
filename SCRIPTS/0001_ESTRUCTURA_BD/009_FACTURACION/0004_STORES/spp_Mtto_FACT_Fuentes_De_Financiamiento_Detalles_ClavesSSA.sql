If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA' and xType = 'P')
    Drop Proc spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA
Go--#SQL

/* 
	Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA    
		@IdFuenteFinanciamiento = "0002", @IdFinanciamiento = "0001", @ClaveSSA = "060.004.0109",  @CantidadPresupuestadaPiezas = "1", @CantidadPresupuestada = "0", @iOpcion = "1"    
*/ 
  
Create Proc spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
( 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '0001', 
	@ClaveSSA varchar(50) = '101', 
	@CantidadPresupuestadaPiezas int = 0, @CantidadPresupuestada int = 0, 
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

	If Not Exists (	Select * From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA (NoLock) 
					Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento 
						And ClaveSSA = @ClaveSSA ) 
	  Begin 
		 Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
			( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, CantidadPresupuestadaPiezas, CantidadPresupuestada, Status, Actualizado ) 
		 Select @IdFuenteFinanciamiento, @IdFinanciamiento, @ClaveSSA, @CantidadPresupuestadaPiezas, @CantidadPresupuestada, @sStatus, @iActualizado 
	  End 
	Else
	  Begin			
		Update FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
			Set CantidadPresupuestadaPiezas = @CantidadPresupuestadaPiezas, CantidadPresupuestada = @CantidadPresupuestada, Status = @sStatus, Actualizado = @iActualizado 
		Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento And ClaveSSA = @ClaveSSA 
	  End 
	  
	  
----------------------	Agregar la clave a la Administración en caso de no existir 
	If Not Exists 
		(	Select * From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA (NoLock) 
			Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento And ClaveSSA = @ClaveSSA  ) 
	Begin 
		Insert Into FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA 
		( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, Status, Actualizado ) 
		Select 
			@IdFuenteFinanciamiento, @IdFinanciamiento, @ClaveSSA, 
			0 as Costo, 1 as Agrupacion, 0 as CostoUnitario, 0 as TasaIva, 0 as Iva, 0 as ImporteNeto, @sStatus, @iActualizado 
	End 
----------------------	Agregar la clave a la Administración en caso de no existir 

	
	
	-- Regresar la Clave Generada
    Select @ClaveSSA as Clave, @sMensaje as Mensaje 


End
Go--#SQL
