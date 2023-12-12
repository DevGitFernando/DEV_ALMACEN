
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_CFGC_ALMN__GruposDeUbicaciones_Det' and xType = 'P')
    Drop Proc spp_CFGC_ALMN__GruposDeUbicaciones_Det
Go--#SQL
  
Create Proc spp_CFGC_ALMN__GruposDeUbicaciones_Det ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdGrupo varchar(3), 
										@sIdPasillo int, @sIdEstante int, @sIdEntrepaño int,  @iOpcion smallint = 1 )
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
	
	
	
	Update CFGC_ALMN__GruposDeUbicaciones_Det
	Set Status = 'C', Actualizado = @iActualizado
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo And IdPasillo = @sIdPasillo And IdEstante = @sIdEstante And IdEntrepaño = @sIdEntrepaño


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CFGC_ALMN__GruposDeUbicaciones_Det (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo And IdPasillo = @sIdPasillo And IdEstante = @sIdEstante And IdEntrepaño = @sIdEntrepaño ) 
			  Begin 
				 Insert Into CFGC_ALMN__GruposDeUbicaciones_Det ( IdEmpresa, IdEstado, IdFarmacia, IdGrupo, IdPasillo, IdEstante, IdEntrepaño, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdGrupo, @sIdPasillo, @sIdEstante, @sIdEntrepaño, @sStatus, @iActualizado 
              End
		   Else
		   Update CFGC_ALMN__GruposDeUbicaciones_Det
		   Set Status = @sStatus, Actualizado = @iActualizado
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo And IdPasillo = @sIdPasillo And IdEstante = @sIdEstante And IdEntrepaño = @sIdEntrepaño
		   Set @sMensaje = 'La información se guardo satisfactoriamente.'
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFGC_ALMN__GruposDeUbicaciones_Det Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo And IdPasillo = @sIdPasillo And IdEstante = @sIdEstante And IdEntrepaño = @sIdEntrepaño
		   Set @sMensaje = 'La información ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdGrupo as Clave, @sMensaje as Mensaje 
End
Go--#SQL
