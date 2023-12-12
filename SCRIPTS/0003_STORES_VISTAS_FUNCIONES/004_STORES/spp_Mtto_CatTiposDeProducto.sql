If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatTiposDeProducto' and xType = 'P')
    Drop Proc spp_Mtto_CatTiposDeProducto
Go--#SQL
  
Create Proc spp_Mtto_CatTiposDeProducto ( @IdTipoProducto varchar(4), @Descripcion varchar(102), @PorcIva int, @iOpcion smallint )
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


	If @IdTipoProducto = '*' 
	   Select @IdTipoProducto = cast( (max(IdTipoProducto) + 1) as varchar)  From CatTiposDeProducto (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdTipoProducto = IsNull(@IdTipoProducto, '1')
	Set @IdTipoProducto = right(replicate('0', 2) + @IdTipoProducto, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatTiposDeProducto (NoLock) Where IdTipoProducto = @IdTipoProducto ) 
			  Begin 
				 Insert Into CatTiposDeProducto ( IdTipoProducto, Descripcion, PorcIva, Status, Actualizado ) 
				 Select @IdTipoProducto, @Descripcion, @PorcIva, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatTiposDeProducto Set Descripcion = @Descripcion, PorcIva = @PorcIva, Status = @sStatus, Actualizado = @iActualizado
				 Where IdTipoProducto = @IdTipoProducto  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdTipoProducto 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatTiposDeProducto Set Status = @sStatus, Actualizado = @iActualizado Where IdTipoProducto = @IdTipoProducto 
		   Set @sMensaje = 'La información del Tipo de Producto ' + @IdTipoProducto + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdTipoProducto as Clave, @sMensaje as Mensaje 
End
Go--#SQL