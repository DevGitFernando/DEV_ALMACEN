
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatDistribuidores_Clientes' and xType = 'P')
    Drop Proc spp_Mtto_CatDistribuidores_Clientes
Go--#SQL
  
Create Proc spp_Mtto_CatDistribuidores_Clientes ( @IdEstado varchar(2), @IdDistribuidor varchar(4), @CodigoCliente varchar(20),
													@NombreCliente varchar(102), @IdFarmacia varchar(4), @iOpcion smallint )
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


--	If @IdDistribuidor = '*' 
--	   Select @IdDistribuidor = cast( (max(IdDistribuidor) + 1) as varchar)  From CatDistribuidores_Clientes (NoLock) 

	-- Asegurar que IdDistribuidor sea valido y formatear la cadena 
	Set @CodigoCliente = IsNull(@CodigoCliente, '1')
	Set @CodigoCliente = right(replicate('0', 7) + @CodigoCliente, 7)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatDistribuidores_Clientes (NoLock) Where IdEstado = @IdEstado and IdDistribuidor = @IdDistribuidor 
							and CodigoCliente = @CodigoCliente ) 
			  Begin 
				 Insert Into CatDistribuidores_Clientes ( IdEstado, IdDistribuidor, CodigoCliente, NombreCliente, IdFarmacia, Status, Actualizado ) 
				 Select @IdEstado, @IdDistribuidor, @CodigoCliente, @NombreCliente, @IdFarmacia, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatDistribuidores_Clientes Set NombreCliente = @NombreCliente, IdFarmacia = @IdFarmacia,
				 Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado and IdDistribuidor = @IdDistribuidor and CodigoCliente = @CodigoCliente 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @CodigoCliente 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatDistribuidores_Clientes Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado and IdDistribuidor = @IdDistribuidor and CodigoCliente = @CodigoCliente

		   Set @sMensaje = 'La información del Cliente ' + @CodigoCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @CodigoCliente as Clave, @sMensaje as Mensaje 
End
Go--#SQL
