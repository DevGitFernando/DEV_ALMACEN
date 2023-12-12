If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatSubProgramas_Farmacias' and xType = 'P')
    Drop Proc spp_Mtto_CatSubProgramas_Farmacias
Go--#SQL

Create Proc spp_Mtto_CatSubProgramas_Farmacias ( @IdEstado varchar(2), @IdFarmacia varchar(4), @IdPrograma varchar(3),@IdSubPrograma varchar(2), @iOpcion smallint )
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

	If @iOpcion = 1 
       Begin
			IF Not Exists ( Select * From CatSubProgramas_Farmacias (NoLock) 
							Where IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia AND IdPrograma = @IdPrograma AND IdSubPrograma = @IdSubPrograma )  
				Begin
					INSERT INTO CatSubProgramas_Farmacias (IdEstado,IdFarmacia,IdPrograma,IdSubPrograma,Status,Actualizado)
					SELECT @IdEstado, @IdFarmacia, @IdPrograma, @IdSubPrograma, @sStatus, @iActualizado
				End
			ELSE
				Begin
					UPDATE CatSubProgramas_Farmacias SET Status = @sStatus, Actualizado = @iActualizado 
					WHERE IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia AND IdPrograma = @IdPrograma AND IdSubPrograma = @IdSubPrograma
				End
			Set @sMensaje = 'La información se guardo satisfactoriamente. ' 
	   End 
	Else 
		Begin 
			Set @sStatus = 'C' 
			UPDATE CatSubProgramas_Farmacias SET Status = @sStatus, Actualizado = @iActualizado 
			WHERE IdEstado = @IdEstado AND IdFarmacia = @IdFarmacia AND IdPrograma = @IdPrograma AND IdSubPrograma = @IdSubPrograma
		   Set @sMensaje = 'La información ha sido cancelada satisfactoriamente.' 
		End 

	
    Select @sMensaje as Mensaje 
End
Go--#SQL