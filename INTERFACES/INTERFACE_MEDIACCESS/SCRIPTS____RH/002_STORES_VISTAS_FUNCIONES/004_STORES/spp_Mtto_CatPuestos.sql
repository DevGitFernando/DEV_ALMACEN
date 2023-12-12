
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPuestos' and xType = 'P')
    Drop Proc spp_Mtto_CatPuestos
Go--#SQL
  
Create Proc spp_Mtto_CatPuestos ( @IdPuesto varchar(3), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdPuesto = '*' 
	   Select @IdPuesto = cast( (max(IdPuesto) + 1) as varchar)  From CatPuestos (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdPuesto = IsNull(@IdPuesto, '1')
	Set @IdPuesto = right(replicate('0', 3) + @IdPuesto, 3)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatPuestos (NoLock) Where IdPuesto = @IdPuesto ) 
			  Begin 
				 Insert Into CatPuestos ( IdPuesto, Descripcion, Status, Actualizado ) 
				 Select @IdPuesto, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatPuestos Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdPuesto = @IdPuesto  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdPuesto 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPuestos Set Status = @sStatus, Actualizado = @iActualizado Where IdPuesto = @IdPuesto 
		   Set @sMensaje = 'La información del Puesto ' + @IdPuesto + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdPuesto as Clave, @sMensaje as Mensaje 
End
Go--#SQL
