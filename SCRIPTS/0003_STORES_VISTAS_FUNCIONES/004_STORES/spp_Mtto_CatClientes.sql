If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatClientes' and xType = 'P')
    Drop Proc spp_Mtto_CatClientes
Go--#SQL
  
Create Proc spp_Mtto_CatClientes 
( @IdCliente varchar(6), @Nombre varchar(102), @RFC varchar(17), @iTipoCliente varchar(2), 
  @IdEstado varchar(4), @IdMunicipio varchar(6), 
  @IdColonia varchar(6), @Domicilio varchar(102), @CodigoPostal varchar(12), @Telefonos varchar(32), 
  @TieneLimiteDeCredito smallint, @LimiteDeCredito numeric(14, 4), @CreditoSuspendido smallint, 
  @SaldoActual numeric(14, 4), @CtaMay varchar(6), @SubCta varchar(6), @SSbCta varchar(6), @SSSCta varchar(6), 
  @iOpcion smallint )
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
	   Select @IdCliente = cast( (max(IdCliente) + 1) as varchar)  From CatClientes (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdCliente = IsNull(@IdCliente, '1')
	Set @IdCliente = right(replicate('0', 4) + @IdCliente, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatClientes (NoLock) Where IdCliente = @IdCliente ) 
			  Begin 
				 Insert Into CatClientes 
					( IdCliente, Nombre, RFC, IdTipoCliente, IdEstado, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, 
					  TieneLimiteDeCredito, LimiteDeCredito, CreditoSuspendido, SaldoActual, CtaMay, SubCta, 
					  SSbCta, SSSCta, Status, Actualizado ) 

				 Select @IdCliente, @Nombre, @RFC, @iTipoCliente, @IdEstado, @IdMunicipio, @IdColonia, @Domicilio, 
						@CodigoPostal, @Telefonos, @TieneLimiteDeCredito, @LimiteDeCredito, @CreditoSuspendido, 
						@SaldoActual, @CtaMay, @SubCta, @SSbCta, @SSSCta, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatClientes Set 
					 IdCliente = @IdCliente, Nombre = @Nombre, RFC = @RFC, IdTipoCliente = @iTipoCliente, 
					 IdEstado = @IdEstado, IdMunicipio = @IdMunicipio, IdColonia = @IdColonia, Domicilio = @Domicilio, 
					 CodigoPostal = @CodigoPostal, Telefonos = @Telefonos, TieneLimiteDeCredito = @TieneLimiteDeCredito, 
					 LimiteDeCredito = @LimiteDeCredito, CreditoSuspendido = @CreditoSuspendido, 
					 SaldoActual = @SaldoActual, CtaMay = @CtaMay, SubCta = @SubCta, SSbCta = @SSbCta, 
					 SSSCta = @SSSCta, Status = @sStatus, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCliente 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatClientes Set Status = @sStatus, Actualizado = @iActualizado Where IdCliente = @IdCliente 
	       
	       -- Cancelar el Cliente de las Tablas de Configuracion 
	       Update CFG_EstadosClientes Set Status = 'C', Actualizado = @iActualizado Where IdCliente = @IdCliente 
	       Update CFG_EstadosFarmaciasClientes Set Status = 'C', Actualizado = @iActualizado Where IdCliente = @IdCliente 	       
	       
		   Set @sMensaje = 'La información del Cliente ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdCliente as Clave, @sMensaje as Mensaje 
End
Go--#SQL