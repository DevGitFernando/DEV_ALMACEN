----------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_CFD_Conceptos' and xType = 'P')
    Drop Proc spp_Mtto_FACT_CFD_Conceptos 
Go--#SQL
 
--		 @IdConcepto = '{0}', @Descripcion = '{1}', @SAT_ProductoServicio = '{2}', @SAT_UnidadDeMedida = '{3}', @iOpcion = '{4}' 
  
Create Proc spp_Mtto_FACT_CFD_Conceptos 
( 
	@IdConcepto varchar(20) = '', @Descripcion varchar(500) = '', 
	@SAT_ProductoServicio varchar(20) = '', @SAT_UnidadDeMedida varchar(10) = '', 
	@iOpcion smallint = 0
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

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From FACT_CFD_Conceptos (NoLock) Where IdConcepto = @IdConcepto ) 
			  Begin 
				 Insert Into FACT_CFD_Conceptos ( IdConcepto, Descripcion, Status, Actualizado, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida  ) 
				 Select @IdConcepto, @Descripcion, @sStatus, @iActualizado, @SAT_ProductoServicio, @SAT_UnidadDeMedida
              End 
		   Else 
			  Begin 
			     Update FACT_CFD_Conceptos Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado, 
					SAT_ClaveProducto_Servicio = @SAT_ProductoServicio, SAT_UnidadDeMedida = @SAT_UnidadDeMedida 
				 Where IdConcepto = @IdConcepto  
              End  
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdConcepto 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update FACT_CFD_Conceptos Set Status = @sStatus, Actualizado = @iActualizado Where IdConcepto = @IdConcepto 
		   Set @sMensaje = 'La información del Concepto ' + @IdConcepto + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdConcepto as Clave, @sMensaje as Mensaje 

End
Go--#SQL
