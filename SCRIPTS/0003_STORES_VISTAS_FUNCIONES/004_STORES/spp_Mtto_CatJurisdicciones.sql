If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatJurisdicciones' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatJurisdicciones 
Go--#SQL

Create Proc spp_Mtto_CatJurisdicciones ( @IdEstado varchar(2), @IdJurisdiccion varchar(3), @Descripcion varchar(50), @iOpcion smallint = 1 )
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

	If @IdJurisdiccion = '*' 
	   Select @IdJurisdiccion = cast( (max(IdJurisdiccion) + 1) as varchar)  From CatJurisdicciones (NoLock) Where IdEstado = @IdEstado 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdJurisdiccion = IsNull(@IdJurisdiccion, '1')
	Set @IdJurisdiccion = right(replicate('0', 3) + @IdJurisdiccion, 3)	


	If @iOpcion = 1 
	   Begin 
	       If Not Exists ( Select * From CatJurisdicciones (NoLock) Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion ) 
	          Insert Into CatJurisdicciones ( IdEstado, IdJurisdiccion, Descripcion, Status, Actualizado )
	          Select @IdEstado, @IdJurisdiccion, @Descripcion, @sStatus, @iActualizado 
	       Else
	          Update CatJurisdicciones Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado 
	          Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion   
	          
	       Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdJurisdiccion 
	   End 
	Else 
	   Begin
           Set @sStatus = 'C' 
	       Update CatJurisdicciones Set Status = @sStatus, Actualizado = @iActualizado Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion
		   Set @sMensaje = 'La información de la Jurisdiccion ' + @IdJurisdiccion + ' ha sido cancelada satisfactoriamente.' 	    
	   End    

	-- Regresar la Clave Generada
    Select @IdJurisdiccion as Clave, @sMensaje as Mensaje 
    
End 
Go--#SQL
	