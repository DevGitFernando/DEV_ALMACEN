

	If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPersonal_Doctos' and xType = 'P')
    Drop Proc spp_Mtto_CatPersonal_Doctos
Go--#SQL
  
Create Proc spp_Mtto_CatPersonal_Doctos ( @IdPersonal varchar(8), @IdDocumento varchar(2), @Archivo text, @NombreArchivo varchar(200), @iOpcion smallint = 1 )
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
	
	If Not Exists ( Select * From CatPersonal_Doctos (NoLock) Where IdPersonal = @IdPersonal and IdDocumento = @IdDocumento ) 
		Begin 
			Insert Into CatPersonal_Doctos ( IdPersonal, IdDocumento, NombreArchivo, Archivo, Status, Actualizado ) 
			Select @IdPersonal, @IdDocumento, @NombreArchivo, @Archivo, @sStatus, @iActualizado 
		End 
	Else 
		Begin 
			Update CatPersonal_Doctos Set NombreArchivo = @NombreArchivo, Archivo = @Archivo, Status = @sStatus, Actualizado = @iActualizado
			Where IdPersonal = @IdPersonal and IdDocumento = @IdDocumento 
		End 	   

	Set @sMensaje = 'La Información se guardo Satisfactoriamente..'

	Select 	@sMensaje as Mensaje

End
Go--#SQL
