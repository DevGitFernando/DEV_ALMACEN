If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatUbicacionClavesAlmacen' and xType = 'P')
    Drop Proc spp_Mtto_CatUbicacionClavesAlmacen
Go--#SQL
  
Create Proc spp_Mtto_CatUbicacionClavesAlmacen ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4),
	@IdClaveSSA varchar(4), @IdPasillo int, @IdEstante int, @IdEntrepa�o int )
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
	

		   If Not Exists ( Select * From CatUbicacionClavesAlmacen (NoLock) 
						Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
						And IdClaveSSA = @IdClaveSSA And IdPasillo = @IdPasillo
						And IdEstante = @IdEstante And IdEntrepa�o = @IdEntrepa�o ) 
			  Begin 
				 Insert Into CatUbicacionClavesAlmacen ( IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, IdPasillo,
															IdEstante, IdEntrepa�o, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdClaveSSA, @IdPasillo, @IdEstante, @IdEntrepa�o, @sStatus, @iActualizado

				Set @sMensaje = 'La informaci�n se guardo satisfactoriamente ' 
              End 
		   

	-- Regresar la Clave Generada
    Select @sMensaje as Mensaje 
End
Go--#SQL