
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatAntiguedades' and xType = 'P')
    Drop Proc spp_Mtto_CatAntiguedades
Go--#SQL
  
Create Proc spp_Mtto_CatAntiguedades ( @IdAntiguedad varchar(3), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdAntiguedad = '*' 
	   Select @IdAntiguedad = cast( (max(IdAntiguedad) + 1) as varchar)  From CatAntiguedades (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdAntiguedad = IsNull(@IdAntiguedad, '1')
	Set @IdAntiguedad = right(replicate('0', 2) + @IdAntiguedad, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatAntiguedades (NoLock) Where IdAntiguedad = @IdAntiguedad ) 
			  Begin 
				 Insert Into CatAntiguedades ( IdAntiguedad, Descripcion, Status, Actualizado ) 
				 Select @IdAntiguedad, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatAntiguedades Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdAntiguedad = @IdAntiguedad  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdAntiguedad 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatAntiguedades Set Status = @sStatus, Actualizado = @iActualizado Where IdAntiguedad = @IdAntiguedad 
		   Set @sMensaje = 'La información de la Antigüedad ' + @IdAntiguedad + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdAntiguedad as Clave, @sMensaje as Mensaje 
End
Go--#SQL
