If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatEstados' and xType = 'P')
    Drop Proc spp_Mtto_CatEstados
Go--#SQL
  
Create Proc spp_Mtto_CatEstados ( @IdEstado varchar(6), @Nombre varchar(52), @Clave varchar(2), @iOpcion smallint )
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


	If @IdEstado = '*' 
	   Select @IdEstado = cast( (max(IdEstado) + 1) as varchar)  From CatEstados (NoLock) 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdEstado = IsNull(@IdEstado, '1')
	Set @IdEstado = right(replicate('0', 2) + @IdEstado, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatEstados (NoLock) Where IdEstado = @IdEstado ) 
			  Begin 
				 Insert Into CatEstados ( IdEstado, Nombre, ClaveRENAPO, Status, Actualizado ) 
				 Select @IdEstado, @Nombre, @Clave, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatEstados Set Nombre = @Nombre, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdEstado 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatEstados Set Status = @sStatus, Actualizado = @iActualizado Where IdEstado = @IdEstado 
		   Set @sMensaje = 'La información de el Estado ' + @IdEstado + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdEstado as Estado, @sMensaje as Mensaje 
End
Go--#SQL