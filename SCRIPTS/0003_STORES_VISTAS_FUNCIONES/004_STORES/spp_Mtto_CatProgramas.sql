If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProgramas' and xType = 'P')
    Drop Proc spp_Mtto_CatProgramas
Go--#SQL

Create Proc spp_Mtto_CatProgramas 
( 
	@IdPrograma varchar(4) = '', @Descripcion varchar(250) = '', @iOpcion smallint = 0, 
	@MostrarResultado smallint = 1, @Resultado varchar(6) = '' output      
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


	If @IdPrograma = '*' 
	   Select @IdPrograma = cast( (max(IdPrograma) + 1) as varchar)  From CatProgramas (NoLock) 

	-- Asegurar que IdPrograma sea valido y formatear la cadena 
	Set @IdPrograma = IsNull(@IdPrograma, '1')
	Set @IdPrograma = right(replicate('0', 4) + @IdPrograma, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatProgramas (NoLock) Where IdPrograma = @IdPrograma ) 
			  Begin 
				 Insert Into CatProgramas ( IdPrograma, Descripcion, Status, Actualizado ) 
				 Select @IdPrograma, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatProgramas Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdPrograma = @IdPrograma  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdPrograma 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatProgramas Set Status = @sStatus, Actualizado = @iActualizado Where IdPrograma = @IdPrograma 
		   Set @sMensaje = 'La información del Programa ' + @IdPrograma + ' ha sido cancelada satisfactoriamente.' 
	   End 

	Set @Resultado = @IdPrograma 
	If @MostrarResultado  = 1 
	Begin 
		-- Regresar la Clave Generada
		Select @IdPrograma as Programa, @sMensaje as Mensaje 
    End 
    
End
Go--#SQL	
