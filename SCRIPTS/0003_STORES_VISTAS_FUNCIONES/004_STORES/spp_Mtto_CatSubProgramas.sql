If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatSubProgramas' and xType = 'P')
    Drop Proc spp_Mtto_CatSubProgramas
Go--#SQL
  
Create Proc spp_Mtto_CatSubProgramas 
( 
	@IdPrograma varchar(4) = '', @IdSubPrograma varchar(4) = '', @Descripcion varchar(250) = '', @iOpcion smallint = 0, 
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


	If @IdSubPrograma = '*' 
	  Begin
	    Select @IdSubPrograma = cast( (max(IdSubPrograma) + 1) as varchar) From CatSubProgramas (NoLock)
		Where IdPrograma = @IdPrograma --And IdSubPrograma = @IdSubPrograma
	  End

	-- Asegurar que IdSubPrograma sea valido y formatear la cadena 
	Set @IdSubPrograma = IsNull(@IdSubPrograma, '1')
	Set @IdSubPrograma = right(replicate('0', 4) + @IdSubPrograma, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatSubProgramas (NoLock) Where IdPrograma = @IdPrograma And IdSubPrograma = @IdSubPrograma ) 
			  Begin 
				 Insert Into CatSubProgramas ( IdPrograma, IdSubPrograma, Descripcion, Status, Actualizado ) 
				 Select @IdPrograma, @IdSubPrograma, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatSubProgramas Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdPrograma = @IdPrograma And IdSubPrograma = @IdSubPrograma
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el SubPrograma ' + @IdSubPrograma 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatSubProgramas Set Status = @sStatus, Actualizado = @iActualizado Where IdPrograma = @IdPrograma And IdSubPrograma = @IdSubPrograma
		   Set @sMensaje = 'La información del SubPrograma ' + @IdSubPrograma + ' ha sido cancelada satisfactoriamente.' 
	   End 

	Set @Resultado = @IdSubPrograma  
	If @MostrarResultado  = 1 
	Begin 
		-- Regresar la Clave Generada
		Select @IdSubPrograma as SubPrograma, @sMensaje as Mensaje 
    End 

End
Go--#SQL