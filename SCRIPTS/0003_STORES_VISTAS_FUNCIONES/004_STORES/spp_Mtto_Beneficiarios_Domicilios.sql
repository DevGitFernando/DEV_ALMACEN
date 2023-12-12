------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_Beneficiarios_Domicilios' and xType = 'P' ) 
   Drop Proc spp_Mtto_Beneficiarios_Domicilios
Go--#SQL 

Create Proc spp_Mtto_Beneficiarios_Domicilios
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdCliente varchar(4), @IdSubCliente varchar(4), @IdBeneficiario varchar(8), @IdEstado_D varchar(2) , 
	@IdMunicipio_D varchar(4), @IdColonia_D varchar(4), @CodigoPostal varchar(10), @Direccion varchar(200), @Referencia varchar(200),
	@Telefonos varchar(50), @iOpcion smallint = 1
) 
As 
Begin 
Set NoCount On 
 
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	Set @sMensaje = ''
	Set @iActualizado = 0
	

	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select *
						   From CatBeneficiarios_Domicilios (NoLock)
						   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
								IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And  IdBeneficiario = @IdBeneficiario) 
			  Begin 
				 Insert Into CatBeneficiarios_Domicilios ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, IdEstado_D, 
							IdMunicipio_D, IdColonia_D, CodigoPostal, Direccion, Referencia, Telefonos ) 
				 Select @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdBeneficiario, @IdEstado_D,
							@IdMunicipio_D, @IdColonia_D, @CodigoPostal, @Direccion, @Referencia, @Telefonos
              End 
		   Else 
			  Begin 
			     Update CatBeneficiarios_Domicilios Set IdEstado_D = @IdEstado_D, IdMunicipio_D = @IdMunicipio_D, IdColonia_D = @IdColonia_D,
					CodigoPostal = @CodigoPostal, Direccion = @Direccion, Referencia = @Referencia, Telefonos = @Telefonos--, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
					   IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And  IdBeneficiario = @IdBeneficiario
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdBeneficiario 
	   End 
    --Else 
    --   Begin 
    --       Set @sStatus = 'C' 
	   --    Update CatBeneficiarios_Domicilios Set Status = @sStatus, Actualizado = @iActualizado Where IdPersonaFirma = @IdPersonaFirma 
		  -- Set @sMensaje = 'La información de la persona ' + @IdPersonaFirma + ' ha sido cancelada satisfactoriamente.' 
	   --End 

	-- Regresar la Clave Generada
    Select @IdBeneficiario as Clave, @sMensaje as Mensaje 


End 
Go--#SQL 