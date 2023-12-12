If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatColonias' and xType = 'P')
    Drop Proc spp_Mtto_CatColonias
Go--#SQL
   
Create Proc spp_Mtto_CatColonias ( @IdEstado varchar(6), @IdMunicipio varchar(4), 
	@IdColonia varchar(4), @Descripcion varchar(52), @iOpcion smallint )
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


	If @IdColonia = '*' 
	   Select @IdColonia = cast( (max(IdColonia) + 1) as varchar)  From CatColonias (NoLock) 
			   Where IdEstado =  @IdEstado and IdMunicipio = @IdMunicipio  

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdColonia = IsNull(@IdColonia, '1')
	Set @IdColonia = right(replicate('0', 4) + @IdColonia, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatColonias (NoLock) 
							Where IdEstado = @IdEstado  and IdMunicipio = @IdMunicipio and IdColonia = @IdColonia ) 
			  Begin 
				 Insert Into CatColonias ( IdEstado, IdMunicipio, IdColonia, Descripcion, Status, Actualizado ) 
				 Select @IdEstado, @IdMunicipio, @IdColonia, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatColonias Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado  and IdMunicipio = @IdMunicipio and IdColonia = @IdColonia 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdColonia 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatColonias Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdEstado = @IdEstado and IdMunicipio = @IdMunicipio and IdColonia = @IdColonia 
		   Set @sMensaje = 'La información de la Colonia ' + @IdColonia + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdColonia as Colonia, @sMensaje as Mensaje 
End
Go--#SQL	
 
