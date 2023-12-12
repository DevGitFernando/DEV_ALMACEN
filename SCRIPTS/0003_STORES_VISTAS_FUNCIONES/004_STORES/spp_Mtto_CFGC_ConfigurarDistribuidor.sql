
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFGC_ConfigurarDistribuidor' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFGC_ConfigurarDistribuidor 
Go--#SQL   

Create Proc spp_Mtto_CFGC_ConfigurarDistribuidor 
( 
    @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0004', @IdDistribuidor varchar(4) = '0001',
    @CodigoCliente varchar(10) = '0001', @Nombre varchar(50) = '0001', @Servidor varchar(100) = 'localhost', @WebService varchar(100) = 'myWebService',
	@PaginaWeb varchar(100) = 'wsFarmacia', @EsDistribuidor tinyint = 0, @iOpcion tinyint = 1
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
		If Not Exists ( Select CodigoCliente From CFGC_ConfigurarDistribuidor (NoLock) 
						Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor And CodigoCliente = @CodigoCliente )
		  Begin 	
			Insert Into CFGC_ConfigurarDistribuidor ( IdEstado, IdFarmacia, IdDistribuidor, CodigoCliente, Nombre, Servidor, WebService, PaginaWeb, EsDistribuidor, FechaRegistro, Status, Actualizado ) 
			Select @IdEstado, @IdFarmacia, @IdDistribuidor, @CodigoCliente, @Nombre, @Servidor, @WebService, @PaginaWeb, @EsDistribuidor, GetDate(), @sStatus, @iActualizado
		  End 
		Else
		  Begin
			Update CFGC_ConfigurarDistribuidor 
			Set Nombre = @Nombre, Servidor = @Servidor, WebService = @WebService, PaginaWeb = @PaginaWeb,
				EsDistribuidor = @EsDistribuidor, Status = @sStatus, Actualizado = @iActualizado	
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor						
		  End
		Set @sMensaje = 'Información guardada satisfactoriamente con el Codigo Cliente  ' + @CodigoCliente 
	  End
	Else
	  Begin
		Set @sStatus = 'C'
		Update CFGC_ConfigurarDistribuidor Set Status = @sStatus
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor And CodigoCliente = @CodigoCliente

		Set @sMensaje = 'Información del Codigo Cliente ' + + @CodigoCliente + ' ha sido cancelada satisfactoriamente' 
	  End
    
    -- Se devuelve el resultado.
    Select @CodigoCliente as Folio, @sMensaje as Mensaje 
    
End 
Go--#SQL   

