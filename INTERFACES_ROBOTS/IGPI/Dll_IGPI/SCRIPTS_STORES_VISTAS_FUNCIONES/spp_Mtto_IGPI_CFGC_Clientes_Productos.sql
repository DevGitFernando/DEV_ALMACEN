If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_IGPI_CFGC_Clientes_Productos' and xType = 'P')
    Drop Proc spp_Mtto_IGPI_CFGC_Clientes_Productos
Go--#SQL  
  
Create Proc spp_Mtto_IGPI_CFGC_Clientes_Productos ( @IdCliente varchar(4), @IdProducto varchar(8), @CodigoEAN varchar(30), @Status smallint, @iOpcion smallint )
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
	Set @iActualizado = 0 


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From IGPI_CFGC_Clientes_Productos (NoLock) Where IdCliente = @IdCliente And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN ) 
			  Begin 
				 Insert Into IGPI_CFGC_Clientes_Productos ( IdCliente, IdProducto, CodigoEAN, Status, Actualizado ) 
				 Select @IdCliente, @IdProducto, @CodigoEAN, @Status, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update IGPI_CFGC_Clientes_Productos Set Status = @Status, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCliente 
	   End 
    Else 
       Begin 
		Set @sStatus = 'C' 
		Update IGPI_CFGC_Clientes_Productos Set Status = @sStatus, Actualizado = @iActualizado 
		Where IdCliente = @IdCliente And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN
		Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdCliente as Clave, @sMensaje as Mensaje 
End
Go--#SQL  

