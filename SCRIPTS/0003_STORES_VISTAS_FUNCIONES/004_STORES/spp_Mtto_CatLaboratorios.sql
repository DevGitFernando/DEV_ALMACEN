
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatLaboratorios' and xType = 'P')
    Drop Proc spp_Mtto_CatLaboratorios
Go--#SQL
  
Create Proc spp_Mtto_CatLaboratorios ( @IdLaboratorio varchar(4), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdLaboratorio = '*' 
	   Select @IdLaboratorio = cast( (max(IdLaboratorio) + 1) as varchar)  From CatLaboratorios (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdLaboratorio = IsNull(@IdLaboratorio, '1')
	Set @IdLaboratorio = right(replicate('0', 4) + @IdLaboratorio, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatLaboratorios (NoLock) Where IdLaboratorio = @IdLaboratorio ) 
			  Begin 
				 Insert Into CatLaboratorios ( IdLaboratorio, Descripcion, Status, Actualizado ) 
				 Select @IdLaboratorio, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatLaboratorios Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdLaboratorio = @IdLaboratorio  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdLaboratorio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatLaboratorios Set Status = @sStatus, Actualizado = @iActualizado Where IdLaboratorio = @IdLaboratorio 
		   Set @sMensaje = 'La información del laboratorio ' + @IdLaboratorio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdLaboratorio as Clave, @sMensaje as Mensaje 
End
Go--#SQL
