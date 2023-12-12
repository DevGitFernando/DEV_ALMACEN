If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProveedores' and xType = 'P')
    Drop Proc spp_Mtto_CatProveedores
Go--#SQL 	

  
Create Proc spp_Mtto_CatProveedores 
( @IdProveedor varchar(6), @Nombre varchar(102), @RFC varchar(17), @AliasNombre varchar(100), @IdEstado varchar(4), @IdMunicipio varchar(6), 
  @IdColonia varchar(6), @Domicilio varchar(102), @CodigoPostal varchar(12), @Telefonos varchar(32), 
  @TieneLimiteDeCredito smallint, @LimiteDeCredito numeric(14, 4), @CreditoSuspendido smallint, 
  @SaldoActual numeric(14, 4), @CtaMay varchar(6), @SubCta varchar(6), @SSbCta varchar(6), @SSSCta varchar(6), 
  @IdPersonalRegistra varchar(6), @iOpcion smallint )
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


	If @IdProveedor = '*' 
	   Select @IdProveedor = cast( (max(IdProveedor) + 1) as varchar)  From CatProveedores (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdProveedor = IsNull(@IdProveedor, '1')
	Set @IdProveedor = right(replicate('0', 4) + @IdProveedor, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatProveedores (NoLock) Where IdProveedor = @IdProveedor ) 
			  Begin 
				 Insert Into CatProveedores 
					( IdProveedor, Nombre, RFC, AliasNombre, IdEstado, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, 
					  TieneLimiteDeCredito, LimiteDeCredito, CreditoSuspendido, SaldoActual, CtaMay, SubCta, 
					  SSbCta, SSSCta, Status, Actualizado ) 

				 Select @IdProveedor, @Nombre, @RFC, @AliasNombre, @IdEstado, @IdMunicipio, @IdColonia, @Domicilio, 
						@CodigoPostal, @Telefonos, @TieneLimiteDeCredito, @LimiteDeCredito, @CreditoSuspendido, 
						@SaldoActual, @CtaMay, @SubCta, @SSbCta, @SSSCta, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatProveedores Set 
					 IdProveedor = @IdProveedor, Nombre = @Nombre, RFC = @RFC, AliasNombre = @AliasNombre, IdEstado = @IdEstado, 
					 IdMunicipio = @IdMunicipio, IdColonia = @IdColonia, Domicilio = @Domicilio, 
					 CodigoPostal = @CodigoPostal, Telefonos = @Telefonos, TieneLimiteDeCredito = @TieneLimiteDeCredito, 
					 LimiteDeCredito = @LimiteDeCredito, CreditoSuspendido = @CreditoSuspendido, 
					 SaldoActual = @SaldoActual, CtaMay = @CtaMay, SubCta = @SubCta, SSbCta = @SSbCta, 
					 SSSCta = @SSSCta, Status = @sStatus, Actualizado = @iActualizado
				 Where IdProveedor = @IdProveedor  
              End 
              
           Exec spp_Mtto_CatProveedores_Certificacion  @IdProveedor, '', '', 0  
              
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdProveedor 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatProveedores Set Status = @sStatus, Actualizado = @iActualizado Where IdProveedor = @IdProveedor 
		   Set @sMensaje = 'La información del Proveedor ' + @IdProveedor + ' ha sido cancelada satisfactoriamente.' 
	   End 


	---- Actualizar el Historico 
	Insert Into CatProveedores_Historico ( IdProveedor, Nombre, RFC, IdPersonalRegistra, IdEstado, IdMunicipio, IdColonia, Domicilio, CodigoPostal, 
		Telefonos, TieneLimiteDeCredito, LimiteDeCredito, CreditoSuspendido, SaldoActual, CtaMay, SubCta, SSbCta, SSSCta, Status, Actualizado ) 
	Select 
		IdProveedor, Nombre, RFC, @IdPersonalRegistra, IdEstado, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, TieneLimiteDeCredito, 
		LimiteDeCredito, CreditoSuspendido, SaldoActual, CtaMay, SubCta, SSbCta, SSSCta, Status, Actualizado 
	from CatProveedores	
	Where IdProveedor = @IdProveedor  

	-- Regresar la Clave Generada
    Select @IdProveedor as Clave, @sMensaje as Mensaje 
End
Go--#SQL
	