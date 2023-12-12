If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_IMach_CFGC_Clientes_Terminales' and xType = 'P')
    Drop Proc spp_Mtto_IMach_CFGC_Clientes_Terminales
Go--#SQL  
  
Create Proc spp_Mtto_IMach_CFGC_Clientes_Terminales ( 
	@IdCliente varchar(4), @IdTerminal varchar(4), @Asignada smallint, @Activa smallint, @PuertoDispensacion smallint, @iOpcion smallint )
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

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From IMach_CFGC_Clientes_Terminales (NoLock) Where IdCliente = @IdCliente And IdTerminal = @IdTerminal ) 
			  Begin 
				 Insert Into IMach_CFGC_Clientes_Terminales ( IdCliente, IdTerminal, Asignada, Activa, PuertoDispensacion, Status, Actualizado ) 
				 Select @IdCliente, @IdTerminal, @Asignada, @Activa, @PuertoDispensacion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update IMach_CFGC_Clientes_Terminales Set IdTerminal = @IdTerminal, Asignada = @Asignada, 
					Activa = @Activa, PuertoDispensacion = @PuertoDispensacion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente And IdTerminal = @IdTerminal 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCliente 
	   End 
    Else 
       Begin 
		Set @sStatus = 'C' 
		Update IMach_CFGC_Clientes_Terminales Set Status = @sStatus, Actualizado = @iActualizado 
		Where IdCliente = @IdCliente And IdTerminal = @IdTerminal
		Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdCliente as Clave, @sMensaje as Mensaje 
End
Go--#SQL  

