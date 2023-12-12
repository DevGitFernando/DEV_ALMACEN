
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFDI_UnidadesDeMedida' and xType = 'P')
    Drop Proc spp_Mtto_CFDI_UnidadesDeMedida
Go--#SQL
  
Create Proc spp_Mtto_CFDI_UnidadesDeMedida 
( 
	@IdUnidad varchar(4) = '', @Descripcion varchar(100) = '', @iOpcion smallint = 1 
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


	If @IdUnidad = '*' 
	   Select @IdUnidad = cast( (max(IdUnidad) + 1) as varchar)  From CFDI_UnidadesDeMedida (NoLock) 

	-- Asegurar que IdUnidad sea valido y formatear la cadena 
	Set @IdUnidad = IsNull(@IdUnidad, '1')
	Set @IdUnidad = right(replicate('0', 4) + @IdUnidad, 4)


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CFDI_UnidadesDeMedida (NoLock) Where IdUnidad = @IdUnidad ) 
			  Begin 
				 Insert Into CFDI_UnidadesDeMedida ( IdUnidad, Descripcion, Status, Actualizado ) 
				 Select @IdUnidad, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFDI_UnidadesDeMedida Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdUnidad = @IdUnidad  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdUnidad 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFDI_UnidadesDeMedida Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdUnidad = @IdUnidad 
		   Set @sMensaje = 'La información de Unidad de Medida ' + @IdUnidad + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdUnidad as Clave, @sMensaje as Mensaje 
End
Go--#SQL 

