
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_RemisionesDistDet' and xType = 'P')
    Drop Proc spp_Mtto_RemisionesDistDet
Go--#SQL
  
Create Proc spp_Mtto_RemisionesDistDet 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioRemision varchar(32), @IdClaveSSA varchar(4), 
	@Cant_Recibida numeric(14, 4), @CantidadRecibida numeric(14, 4), @iOpcion smallint, @Precio numeric(14, 4) = 0 
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

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From RemisionesDistDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioRemision = @FolioRemision 
								 And IdClaveSSA = @IdClaveSSA ) 
			  Begin 
				Insert Into RemisionesDistDet ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision, IdClaveSSA, Precio, 
					Cant_Recibida, CantidadRecibida, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioRemision, @IdClaveSSA, @Precio, 
						@Cant_Recibida, @CantidadRecibida, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update RemisionesDistDet Set Cant_Recibida = @Cant_Recibida, Cant_Devuelta = (@Cant_Recibida - @CantidadRecibida), 
				CantidadRecibida = @CantidadRecibida, Status = @sStatus, Actualizado = @iActualizado
				Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioRemision = @FolioRemision 
					 And IdClaveSSA = @IdClaveSSA 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioRemision 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update RemisionesDistDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioRemision = @FolioRemision 
				  And IdClaveSSA = @IdClaveSSA 

		   Set @sMensaje = 'La información del Folio ' + @FolioRemision + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioRemision as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
