If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatSubClientes' and xType = 'P')
    Drop Proc spp_Mtto_CatSubClientes
Go--#SQL
  
Create Proc spp_Mtto_CatSubClientes 
( 
	@IdCliente varchar(4), @IdSubCliente varchar(4), @Nombre varchar(52), 
	@PorcUtilidad numeric(14,4), @CapturaBeneficiarios smallint, @ImportaBeneficiarios smallint, @iOpcion smallint )
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


	If @IdSubCliente = '*' 
	  Begin
	    Select @IdSubCliente = cast( (max(IdSubCliente) + 1) as varchar) From CatSubClientes (NoLock)
		Where IdCliente = @IdCliente
	  End

	-- Asegurar que IdSubCliente sea valido y formatear la cadena 
	Set @IdSubCliente = IsNull(@IdSubCliente, '1')
	Set @IdSubCliente = right(replicate('0', 4) + @IdSubCliente, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatSubClientes (NoLock) Where IdCliente = @IdCliente And IdSubCliente = @IdSubCliente ) 
			  Begin 
				 Insert Into CatSubClientes ( IdCliente, IdSubCliente, Nombre, 
					PermitirCapturaBeneficiarios, ImportaBeneficiarios, PorcUtilidad, Status, Actualizado ) 
				 Select @IdCliente, @IdSubCliente, @Nombre, 
					@CapturaBeneficiarios, @ImportaBeneficiarios, @PorcUtilidad, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatSubClientes Set Nombre = @Nombre, PermitirCapturaBeneficiarios = @CapturaBeneficiarios, 
					ImportaBeneficiarios = @ImportaBeneficiarios, PorcUtilidad = @PorcUtilidad, 
					Status = @sStatus, Actualizado = @iActualizado
				 Where IdCliente = @IdCliente And IdSubCliente = @IdSubCliente
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdSubCliente 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatSubClientes Set Status = @sStatus, Actualizado = @iActualizado Where IdCliente = @IdCliente And IdSubCliente = @IdSubCliente
		   Set @sMensaje = 'La información del SubCliente ' + @IdSubCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdSubCliente as Clave, @sMensaje as Mensaje 
End
Go--#SQL