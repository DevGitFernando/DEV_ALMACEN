



If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatIncidencias' and xType = 'P')
    Drop Proc spp_Mtto_CatIncidencias
Go--#SQL
  
 ------		 Exec spp_Mtto_CatIncidencias '*', 'PERMISO SIN GOCE DE SUELDO', 1
  
Create Proc spp_Mtto_CatIncidencias ( @IdIncidencia varchar(2), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdIncidencia = '*' 
	   Select @IdIncidencia = cast( (max(IdIncidencia) + 1) as varchar)  From CatIncidencias (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdIncidencia = IsNull(@IdIncidencia, '1')
	Set @IdIncidencia = right(replicate('0', 2) + @IdIncidencia, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatIncidencias (NoLock) Where IdIncidencia = @IdIncidencia ) 
			  Begin 
				 Insert Into CatIncidencias ( IdIncidencia, Descripcion, Status, Actualizado ) 
				 Select @IdIncidencia, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatIncidencias Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdIncidencia = @IdIncidencia  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdIncidencia 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatIncidencias Set Status = @sStatus, Actualizado = @iActualizado Where IdIncidencia = @IdIncidencia 
		   Set @sMensaje = 'La información del Puesto ' + @IdIncidencia + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdIncidencia as Clave, @sMensaje as Mensaje 
End
Go--#SQL
