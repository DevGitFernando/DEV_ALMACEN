If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatGruposTerapeuticos' and xType = 'P' )
   Drop Proc spp_Mtto_CatGruposTerapeuticos 
Go--#SQL

Create Proc spp_Mtto_CatGruposTerapeuticos ( @IdGrupo varchar(3), @Descripcion varchar(50), @iOpcion smallint = 1 ) 
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
	   Select @IdGrupo = cast( (max(IdGrupoTerapeutico) + 1) as varchar)  From CatGruposTerapeuticos (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdGrupo = IsNull(@IdGrupo, '1')
	Set @IdGrupo = right(replicate('0', 3) + @IdGrupo, 3)


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CatGruposTerapeuticos (NoLock) Where IdGrupoTerapeutico = @IdGrupo ) 
			  Begin 
				 Insert Into CatGruposTerapeuticos ( IdGrupoTerapeutico, Descripcion, Status, Actualizado ) 
				 Select @IdGrupo, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatGruposTerapeuticos Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdGrupoTerapeutico = @IdGrupo  
              End 

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdGrupo 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
		   -- Se cancela la Familia
	       Update CatGruposTerapeuticos Set Status = @sStatus, Actualizado = @iActualizado Where IdGrupoTerapeutico = @IdGrupo 
		   Set @sMensaje = 'La información del Grupo Terapeutico ' + @IdGrupo + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdGrupo as Clave, @sMensaje as Mensaje 

End 
Go--#SQL	

