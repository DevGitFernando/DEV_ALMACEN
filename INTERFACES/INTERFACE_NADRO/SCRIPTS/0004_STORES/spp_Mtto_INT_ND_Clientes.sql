------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Clientes' and xType = 'P')
    Drop Proc spp_Mtto_INT_ND_Clientes
Go--#SQL 
  
Create Proc spp_Mtto_INT_ND_Clientes 
( 
	@IdCliente varchar(4) = '*', @CodigoCliente varchar(20) = '12345678', @Nombre varchar(200) = 'TEST',
	@IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0000', 
	@EsDeSurtimiento tinyint = 0, @iOpcion smallint = 1
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


	If @IdCliente = '*'
	Begin 
		Select @IdCliente = cast( (max(IdCliente) + 1) as varchar)  From INT_ND_Clientes (NoLock) 
	End
	-- Asegurar que IdCliente sea valido y formatear la cadena 
	Set @IdCliente = IsNull(@IdCliente, '1')
	Set @IdCliente = right(replicate('0', 4) + @IdCliente, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From INT_ND_Clientes (NoLock) Where IdCliente = @IdCliente ) 
			  Begin 
				 Insert Into INT_ND_Clientes ( IdCliente, CodigoCliente, Nombre, IdEstado, IdFarmacia, EsDeSurtimiento, Status, Actualizado )
				 Select @IdCliente, @CodigoCliente, @Nombre, @IdEstado, @IdFarmacia, @EsDeSurtimiento, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update INT_ND_Clientes Set 
					 Nombre = @Nombre, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia,
					 EsDeSurtimiento = @EsDeSurtimiento, Status = @sStatus, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCliente 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update INT_ND_Clientes Set Status = @sStatus, Actualizado = @iActualizado Where IdCliente = @IdCliente 
	        
	       Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdCliente as Clave, @sMensaje as Mensaje 
End
Go--#SQL