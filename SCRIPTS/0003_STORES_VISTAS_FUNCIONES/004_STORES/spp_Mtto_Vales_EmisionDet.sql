If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_EmisionDet' and xType = 'P')
    Drop Proc spp_Mtto_Vales_EmisionDet
Go--#SQL
  
Create Proc spp_Mtto_Vales_EmisionDet 
(
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVale varchar(32), 
	@IdClaveSSA_Sal varchar(4), @Cantidad numeric(14, 4), @IdPresentacion varchar(3), @iOpcion smallint 	 
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

		   If Not Exists ( Select * From Vales_EmisionDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale 
								 And IdClaveSSA_Sal = @IdClaveSSA_Sal ) 
			  Begin 
				Insert Into Vales_EmisionDet ( IdEmpresa, IdEstado, IdFarmacia, FolioVale, IdClaveSSA_Sal, Cantidad, IdPresentacion, Status, Actualizado ) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVale, @IdClaveSSA_Sal, @Cantidad, @IdPresentacion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update Vales_EmisionDet Set Cantidad = @Cantidad, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale 
					 And IdClaveSSA_Sal = @IdClaveSSA_Sal
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el folio ' + @FolioVale 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update Vales_EmisionDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale 
				  And IdClaveSSA_Sal = @IdClaveSSA_Sal 

		   Set @sMensaje = 'La información del Folio ' + @FolioVale + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioVale as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
