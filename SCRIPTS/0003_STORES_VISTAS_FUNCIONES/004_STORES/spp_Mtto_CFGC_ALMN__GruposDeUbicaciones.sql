
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_ALMN__GruposDeUbicaciones' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_ALMN__GruposDeUbicaciones
Go--#SQL
  
Create Proc spp_Mtto_CFGC_ALMN__GruposDeUbicaciones ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdGrupo varchar(3), 
										@Nombre varchar(500), @iOpcion smallint )
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


	If @IdGrupo = '*' 
	   Select @IdGrupo = cast( (max(IdGrupo) + 1) as varchar)  From CFGC_ALMN__GruposDeUbicaciones (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdGrupo = IsNull(@IdGrupo, '1')
	Set @IdGrupo = right(replicate('0', 3) + @IdGrupo, 3)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CFGC_ALMN__GruposDeUbicaciones (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo ) 
			  Begin 
				 Insert Into CFGC_ALMN__GruposDeUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdGrupo, NombreGrupo, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdGrupo, @Nombre, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFGC_ALMN__GruposDeUbicaciones Set NombreGrupo = @Nombre, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdGrupo 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFGC_ALMN__GruposDeUbicaciones Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdGrupo = @IdGrupo
		   Set @sMensaje = 'La información del grupo ' + @IdGrupo + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdGrupo as Clave, @sMensaje as Mensaje 
End
Go--#SQL
