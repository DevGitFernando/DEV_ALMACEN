------------------------------------------------------------------------------------------------------------------------ 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFDI_Clientes' and xType = 'P')
    Drop Proc spp_Mtto_CFDI_Clientes
Go--#SQL 
  
Create Proc spp_Mtto_CFDI_Clientes 
( 
	@IdCliente varchar(8) = '', 
	@Nombre varchar(200) = '', --- @ApPaterno varchar(50) = '', @ApMaterno varchar(50) = '', 
	@NombreComercial varchar(200) = '',
	@RFC varchar(15) = '', -- @FechaNacimiento varchar(10) = '2014-03-01', 
	@TipoDeCliente smallint = 0, 
	@iOpcion smallint 
)
As 
Begin
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado tinyint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdCliente = '*' 
	Begin 
	   Select @IdCliente = cast( (max(IdCliente) + 1) as varchar)  
	   From CFDI_Clientes (NoLock) 
	End 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdCliente = IsNull(@IdCliente, '1')
	Set @IdCliente = right(replicate('0', 8) + @IdCliente, 8)


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CFDI_Clientes (NoLock) Where IdCliente = @IdCliente ) 
			  Begin 
				 Insert Into CFDI_Clientes ( IdCliente, Nombre, NombreComercial, RFC, TipoDeCliente, FechaRegistro, Status, Actualizado ) 
				 Select @IdCliente, @Nombre, @NombreComercial, @RFC, @TipoDeCliente, getdate(), @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFDI_Clientes Set 
					Nombre = @Nombre, NombreComercial = @NombreComercial, 
					RFC = @RFC, TipoDeCliente = @TipoDeCliente, 
					Status = @sStatus, Actualizado = @iActualizado 
				 Where IdCliente = @IdCliente  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdCliente 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update CFDI_Clientes Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdCliente = @IdCliente
			Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdCliente as IdCliente, @sMensaje as Mensaje 
    
End
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------ 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFDI_Clientes_Direcciones' and xType = 'P')
    Drop Proc spp_Mtto_CFDI_Clientes_Direcciones 
Go--#SQL 

Create Proc spp_Mtto_CFDI_Clientes_Direcciones 
( 
	@IdCliente varchar(8) = '',  @IdDireccion varchar(2) = '*', 
	@Pais varchar(100) = '', 
	@IdEstado varchar(2) = '', @IdMunicipio varchar(4) = '', @IdColonia varchar(4) = '', 
	@Calle varchar(100) = '', @NumeroExterior varchar(20) = '', @NumeroInterior varchar(20) = '', 
	@CodigoPostal varchar(10) = '', @Referencia varchar(100) = '', 
	@iOpcion smallint 
)
As 
Begin
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado tinyint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdDireccion = '*' 
	Begin 
	   Select @IdDireccion = cast( (max(IdDireccion) + 1) as varchar)  
	   From CFDI_Clientes_Direcciones (NoLock) 
	   Where IdCliente = @IdCliente 
	End 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdDireccion = IsNull(@IdDireccion, '1')
	Set @IdDireccion = right(replicate('0', 2) + @IdDireccion, 2)			

	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CFDI_Clientes_Direcciones (NoLock) Where IdCliente = @IdCliente and IdDireccion = @IdDireccion ) 
			  Begin 
				 Insert Into CFDI_Clientes_Direcciones ( IdCliente, IdDireccion, Pais, IdEstado, IdMunicipio, IdColonia, Calle, 
					NumeroExterior, NumeroInterior, CodigoPostal, Referencia, Status, Actualizado ) 
				 Select @IdCliente, @IdDireccion, @Pais, @IdEstado, @IdMunicipio, @IdColonia, @Calle, 
					@NumeroExterior, @NumeroInterior, @CodigoPostal, @Referencia, @sStatus, @iActualizado 
              End  
		   Else 
			  Begin 
			     Update CFDI_Clientes_Direcciones Set 
					Pais = @Pais, IdEstado = @IdEstado, IdMunicipio = @IdMunicipio, IdColonia = @IdColonia, 
					Calle = @Calle, NumeroExterior = @NumeroExterior, NumeroInterior = @NumeroInterior, CodigoPostal = @CodigoPostal, 
					Referencia = @Referencia, 
					Status = @sStatus, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente and IdDireccion = @IdDireccion  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdCliente 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update CFDI_Clientes_Direcciones Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdCliente = @IdCliente and IdDireccion = @IdDireccion 
			Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

End
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------ 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFDI_Clientes_EMails' and xType = 'P')
    Drop Proc spp_Mtto_CFDI_Clientes_EMails 
Go--#SQL 

Create Proc spp_Mtto_CFDI_Clientes_EMails 
( 
	@IdCliente varchar(8) = '',  @IdEmail varchar(2) = '*',
	@IdTipoEMail varchar(4) = '', @Email varchar(100) = '',  
	@iOpcion smallint 
)
As 
Begin
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado tinyint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdEmail = '*' 
	Begin 
	   Select @IdEmail = cast( (max(IdEmail) + 1) as varchar)  
	   From CFDI_Clientes_EMails (NoLock) 
	   Where IdCliente = @IdCliente 
	End 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdEmail = IsNull(@IdEmail, '1')
	Set @IdEmail = right(replicate('0', 2) + @IdEmail, 2)			

	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CFDI_Clientes_EMails (NoLock) Where IdCliente = @IdCliente and IdEmail = @IdEmail ) 
			  Begin 
				 Insert Into CFDI_Clientes_EMails ( IdCliente, IdEmail, IdTipoEMail, Email, Status, Actualizado ) 
				 Select @IdCliente, @IdEmail, @IdTipoEMail, @Email, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFDI_Clientes_EMails Set 
					IdTipoEMail = @IdTipoEMail, Email = @Email, 
					Status = @sStatus, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente and IdEmail = @IdEmail  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdCliente 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update CFDI_Clientes_EMails Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdCliente = @IdCliente and IdEmail = @IdEmail 
			Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

End
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFDI_Clientes_Telefonos' and xType = 'P')
    Drop Proc spp_Mtto_CFDI_Clientes_Telefonos 
Go--#SQL 

Create Proc spp_Mtto_CFDI_Clientes_Telefonos 
( 
	@IdCliente varchar(8) = '',  @IdTelefono varchar(2) = '*',
	@IdTipoTelefono varchar(4) = '', @Telefono varchar(20) = '',  
	@iOpcion smallint 
)
As 
Begin
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado tinyint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdTelefono = '*' 
	Begin 
	   Select @IdTelefono = cast( (max(IdTelefono) + 1) as varchar)  
	   From CFDI_Clientes_Telefonos (NoLock) 
	   Where IdCliente = @IdCliente 
	End 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdTelefono = IsNull(@IdTelefono, '1')
	Set @IdTelefono = right(replicate('0', 2) + @IdTelefono, 2)			

	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CFDI_Clientes_Telefonos (NoLock) Where IdCliente = @IdCliente and IdTelefono = @IdTelefono ) 
			  Begin 
				 Insert Into CFDI_Clientes_Telefonos ( IdCliente, IdTelefono, IdTipoTelefono, Telefono, Status, Actualizado ) 
				 Select @IdCliente, @IdTelefono, @IdTipoTelefono, @Telefono, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFDI_Clientes_Telefonos Set 
					IdTipoTelefono = @IdTipoTelefono, Telefono = @Telefono, 
					Status = @sStatus, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente and IdTelefono = @IdTelefono  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdCliente 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update CFDI_Clientes_Telefonos Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdCliente = @IdCliente and IdTelefono = @IdTelefono 
			Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

End
Go--#SQL 