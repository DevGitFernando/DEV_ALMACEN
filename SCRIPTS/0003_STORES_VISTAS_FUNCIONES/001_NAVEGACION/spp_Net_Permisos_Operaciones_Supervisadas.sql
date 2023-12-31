If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Permisos_Operaciones_Supervisadas' and xType = 'P' ) 
   Drop Proc spp_Net_Permisos_Operaciones_Supervisadas 
Go--#SQL  
   
Create Proc spp_Net_Permisos_Operaciones_Supervisadas 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4), @IdOperacion varchar(4), @iOpcion tinyint  
) 
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
	-- IdEstado, IdFarmacia, IdPersonal, IdOperacion

	If @iOpcion = 2 
	   Set @sStatus = 'C' 
	
	If Not Exists ( Select * From Net_Permisos_Operaciones_Supervisadas (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal and IdOperacion = @IdOperacion )
	   Insert Into Net_Permisos_Operaciones_Supervisadas ( IdEstado, IdFarmacia, IdPersonal, IdOperacion, Status, Actualizado ) 
	   Select @IdEstado, @IdFarmacia, @IdPersonal, @IdOperacion, @sStatus, @iActualizado 
	Else 
	   Update Net_Permisos_Operaciones_Supervisadas Set FechaUpdate = getdate(), Status = @sStatus, Actualizado = @iActualizado
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal and IdOperacion = @IdOperacion 	
End 
Go--#SQL 