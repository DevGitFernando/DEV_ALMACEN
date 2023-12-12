

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFGC_Checador' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFGC_Checador 
Go--#SQL   

Create Proc spp_Mtto_CFGC_Checador 
( 
    @Servidor varchar(100) = 'lapfernando', @WebService varchar(100) = 'wsAdministrativos',
	@PaginaWeb varchar(100) = 'wsChecador', @iOpcion tinyint = 1
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
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     
    
	If @iOpcion = 1
	  Begin
 
		Delete From CFGC_Checador
 
		Insert Into CFGC_Checador ( Servidor, WebService, PaginaWeb, Status, Actualizado ) 
		Select @Servidor, @WebService, @PaginaWeb, @sStatus, @iActualizado
		  
		Set @sMensaje = 'Informaci�n guardada satisfactoriamente.' 
	  End
	Else
	  Begin
		Set @sStatus = 'C'
		Update CFGC_Checador Set Status = @sStatus
		Where Servidor = @Servidor 

		Set @sMensaje = 'Informaci�n cancelada satisfactoriamente' 
	  End
    
    -- Se devuelve el resultado.
    Select @sMensaje as Mensaje 
    
End 
Go--#SQL   


