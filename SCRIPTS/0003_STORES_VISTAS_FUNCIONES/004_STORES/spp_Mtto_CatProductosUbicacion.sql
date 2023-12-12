If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProductosUbicacion' and xType = 'P')
    Drop Proc spp_Mtto_CatProductosUbicacion
Go--#SQL
  
Create Proc spp_Mtto_CatProductosUbicacion ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4),
	@CodigoEAN varchar(30), @IdEstante int, @Entrepano int )
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
	

		   If Not Exists ( Select * From CatProductosUbicacion (NoLock) 
						Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
						And CodigoEAN = @CodigoEAN And IdEstante = @IdEstante And Entrepano = @Entrepano ) 
			  Begin 
				 Insert Into CatProductosUbicacion ( IdEmpresa, IdEstado, IdFarmacia, CodigoEAN, IdEstante, 
													Entrepano, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @CodigoEAN, @IdEstante, @Entrepano, @sStatus, @iActualizado

				Set @sMensaje = 'La información se guardo satisfactoriamente ' 
              End 
		   

	-- Regresar la Clave Generada
    Select @sMensaje as Mensaje 
End
Go--#SQL