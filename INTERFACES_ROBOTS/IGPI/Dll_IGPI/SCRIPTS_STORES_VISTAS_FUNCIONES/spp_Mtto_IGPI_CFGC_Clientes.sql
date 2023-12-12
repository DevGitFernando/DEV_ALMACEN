If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_IGPI_CFGC_Clientes' and xType = 'P')
    Drop Proc spp_Mtto_IGPI_CFGC_Clientes
Go--#SQL  
  
Create Proc spp_Mtto_IGPI_CFGC_Clientes ( @IdCliente varchar(4), @IdEstado varchar(2), @IdFarmacia varchar(4), @iOpcion smallint )
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


	If @IdCliente = '*' 
	   Select @IdCliente = cast( (max(IdCliente) + 1) as varchar)  From IGPI_CFGC_Clientes (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdCliente = IsNull(@IdCliente, '1')
	Set @IdCliente = right(replicate('0', 4) + @IdCliente, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From IGPI_CFGC_Clientes (NoLock) Where IdCliente = @IdCliente ) 
			  Begin 
				 Insert Into IGPI_CFGC_Clientes ( IdCliente, IdEstado, IdFarmacia, Status, Actualizado ) 
				 Select @IdCliente, @IdEstado, @IdFarmacia, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update IGPI_CFGC_Clientes Set IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, Status = @sStatus, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCliente 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update IGPI_CFGC_Clientes Set Status = @sStatus, Actualizado = @iActualizado Where IdCliente = @IdCliente 
		   Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdCliente as Clave, @sMensaje as Mensaje 
End
Go--#SQL  

