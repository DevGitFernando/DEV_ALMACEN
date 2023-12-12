
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatMunicipios' and xType = 'P')
    Drop Proc spp_Mtto_CatMunicipios
Go--#SQL
  
Create Proc spp_Mtto_CatMunicipios ( @IdEstado varchar(6), @IdMunicipio varchar(4), @Descripcion varchar(52), @iOpcion smallint )
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


	If @IdMunicipio = '*' 
	   Select @IdMunicipio = cast( (max(IdMunicipio) + 1) as varchar)  From CatMunicipios (NoLock) Where IdEstado =  @IdEstado 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdMunicipio = IsNull(@IdMunicipio, '1')
	Set @IdMunicipio = right(replicate('0', 4) + @IdMunicipio, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatMunicipios (NoLock) Where IdEstado = @IdEstado  and IdMunicipio = @IdMunicipio ) 
			  Begin 
				 Insert Into CatMunicipios ( IdEstado, IdMunicipio, Descripcion, Status, Actualizado ) 
				 Select @IdEstado, @IdMunicipio, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatMunicipios Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado  and IdMunicipio = @IdMunicipio 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdMunicipio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatMunicipios Set Status = @sStatus, Actualizado = @iActualizado Where IdEstado = @IdEstado and IdMunicipio = @IdMunicipio
		   Set @sMensaje = 'La información de el Municipio ' + @IdMunicipio + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdMunicipio as Municipio, @sMensaje as Mensaje 
End
Go--#SQL	
