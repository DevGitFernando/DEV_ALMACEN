
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPersonalTA' and xType = 'P')
    Drop Proc spp_Mtto_CatPersonalTA
Go--#SQL
  
Create Proc spp_Mtto_CatPersonalTA ( @IdPersonal varchar(6), @Nombre varchar(102), @iOpcion smallint )
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


	If @IdPersonal = '*'
		Select @IdPersonal = cast( (max(IdPersonal) + 1) as varchar) From CatPersonalTA (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdPersonal = IsNull(@IdPersonal, '1')
	Set @IdPersonal = right(replicate('0', 4) + @IdPersonal, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatPersonalTA (NoLock) Where IdPersonal = @IdPersonal ) 
			  Begin 
				 Insert Into CatPersonalTA ( IdPersonal, Nombre, Status, Actualizado ) 
				 Select @IdPersonal, @Nombre, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			    Update CatPersonalTA 
				Set Nombre = @Nombre , Status = @sStatus, Actualizado = @iActualizado
				Where IdPersonal = @IdPersonal  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdPersonal 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPersonalTA Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdPersonal = @IdPersonal 
		   Set @sMensaje = 'La información del Personal ' + @IdPersonal + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdPersonal as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
