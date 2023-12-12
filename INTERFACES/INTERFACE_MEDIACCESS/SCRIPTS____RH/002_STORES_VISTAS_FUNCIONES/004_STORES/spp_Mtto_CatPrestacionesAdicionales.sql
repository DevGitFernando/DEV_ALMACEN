



If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPrestacionesAdicionales' and xType = 'P')
    Drop Proc spp_Mtto_CatPrestacionesAdicionales
Go--#SQL
  
 ------		 Exec spp_Mtto_CatPrestacionesAdicionales '*', 'test', 1
  
Create Proc spp_Mtto_CatPrestacionesAdicionales ( @IdPrestacion varchar(2), @Descripcion varchar(102), @iOpcion smallint )
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


	If @IdPrestacion = '*' 
	   Select @IdPrestacion = cast( (max(IdPrestacion) + 1) as varchar)  From CatPrestacionesAdicionales (NoLock) 

	-- Asegurar que IdPrestacion sea valido y formatear la cadena 
	Set @IdPrestacion = IsNull(@IdPrestacion, '1')
	Set @IdPrestacion = right(replicate('0', 2) + @IdPrestacion, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatPrestacionesAdicionales (NoLock) Where IdPrestacion = @IdPrestacion ) 
			  Begin 
				 Insert Into CatPrestacionesAdicionales ( IdPrestacion, Descripcion, Status, Actualizado ) 
				 Select @IdPrestacion, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatPrestacionesAdicionales Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdPrestacion = @IdPrestacion  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdPrestacion 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPrestacionesAdicionales Set Status = @sStatus, Actualizado = @iActualizado Where IdPrestacion = @IdPrestacion 
		   Set @sMensaje = 'La información de la Prestación ' + @IdPrestacion + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdPrestacion as Clave, @sMensaje as Mensaje 
End
Go--#SQL
