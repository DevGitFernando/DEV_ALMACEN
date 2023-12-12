If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatCentrosDeSalud' and xType = 'P')
    Drop Proc spp_Mtto_CatCentrosDeSalud
Go--#SQL 
  
Create Proc spp_Mtto_CatCentrosDeSalud ( @IdEstado varchar(2), @IdCentro varchar(4), @IdMunicipio varchar(4), @IdJurisdiccion varchar(3), @Descripcion varchar(102), @iOpcion smallint )
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
	Set @iActualizado = 1 


	If @IdCentro = '*' 
		Select @IdCentro = cast( (max(IdCentro) + 1) as varchar) From CatCentrosDeSalud (NoLock) Where IdEstado = @IdEstado

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdCentro = IsNull(@IdCentro, '1')
	Set @IdCentro = right(replicate('0', 4) + @IdCentro, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatCentrosDeSalud (NoLock) Where IdEstado = @IdEstado And IdCentro = @IdCentro ) 
			  Begin 
				 Insert Into CatCentrosDeSalud ( IdEstado, IdCentro, IdMunicipio, IdJurisdiccion, Descripcion, Status, Actualizado ) 
				 Select @IdEstado, @IdCentro, @IdMunicipio, @IdJurisdiccion, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatCentrosDeSalud Set IdMunicipio = @IdMunicipio, IdJurisdiccion = @IdJurisdiccion, Descripcion = @Descripcion, 
				 Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdCentro = @IdCentro  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCentro 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatCentrosDeSalud Set Status = @sStatus, Actualizado = @iActualizado Where IdEstado = @IdEstado And IdCentro = @IdCentro 
		   Set @sMensaje = 'La información del Centro de Salud ' + @IdCentro + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdCentro as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
