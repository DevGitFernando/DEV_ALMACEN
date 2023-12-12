
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatDepartamentos' and xType = 'P')
    Drop Proc spp_Mtto_CatDepartamentos
Go--#SQL
  
Create Proc spp_Mtto_CatDepartamentos ( @IdDepartamento varchar(3), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdDepartamento = '*' 
	   Select @IdDepartamento = cast( (max(IdDepartamento) + 1) as varchar)  From CatDepartamentos (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdDepartamento = IsNull(@IdDepartamento, '1')
	Set @IdDepartamento = right(replicate('0', 3) + @IdDepartamento, 3)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatDepartamentos (NoLock) Where IdDepartamento = @IdDepartamento ) 
			  Begin 
				 Insert Into CatDepartamentos ( IdDepartamento, Descripcion, Status, Actualizado ) 
				 Select @IdDepartamento, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatDepartamentos Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdDepartamento = @IdDepartamento  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdDepartamento 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatDepartamentos Set Status = @sStatus, Actualizado = @iActualizado Where IdDepartamento = @IdDepartamento 
		   Set @sMensaje = 'La información del Departamento ' + @IdDepartamento + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdDepartamento as Clave, @sMensaje as Mensaje 
End
Go--#SQL
