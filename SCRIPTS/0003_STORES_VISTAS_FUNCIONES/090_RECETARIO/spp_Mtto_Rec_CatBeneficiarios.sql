


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Rec_CatBeneficiarios' and xType = 'P')
    Drop Proc spp_Mtto_Rec_CatBeneficiarios
Go--#SQL
  
Create Proc spp_Mtto_Rec_CatBeneficiarios ( @IdEstado varchar(2), @IdFarmacia varchar(6),
	@IdBeneficiario varchar(10), @Nombre varchar(52), @ApPaterno varchar(52), @ApMaterno varchar(52), @Sexo varchar(2), 
	@FechaNacimiento varchar(10), @FolioReferencia varchar(20), @FechaInicioVigencia varchar(10), @FechaFinVigencia varchar(10), 
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

	--- Buscar el Beneficirio en Base al Folio de Referencia, es un registro de información Importada de Servidor Central 
--	If @IdBeneficiario = '**' 
--	   Begin 
--		Select Top 1 @IdBeneficiario = cast( (max(IdBeneficiario)) as varchar) 
--		From Rec_CatBeneficiarios (NoLock) 
--		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente	
--			  and FolioReferencia = @FolioReferencia 
--			  
--		--- El Folio de SP no esta dado de Alta en la Unidad Actual 
--		Set @IdBeneficiario = IsNull(@IdBeneficiario, '*') 		
--	   End 

    --- Validación Estandar para Beneficiarios 
	If @IdBeneficiario = '*' 
	 Begin
		Select @IdBeneficiario = cast( (max(IdBeneficiario) + 1) as varchar)  
		From Rec_CatBeneficiarios (NoLock) 
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	 End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdBeneficiario = IsNull(@IdBeneficiario, '1')
	Set @IdBeneficiario = right(replicate('0', 8) + @IdBeneficiario, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists (  Select * From Rec_CatBeneficiarios (NoLock) 
							Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								  And IdBeneficiario = @IdBeneficiario ) 
			  Begin 
				 Insert Into Rec_CatBeneficiarios ( IdEstado, IdFarmacia, IdBeneficiario, 
												Nombre, ApPaterno, ApMaterno, Sexo, FechaNacimiento, FolioReferencia,
												FechaInicioVigencia, FechaFinVigencia, FechaRegistro, Status, Actualizado ) 

				 Select @IdEstado ,@IdFarmacia, @IdBeneficiario, 
						@Nombre, @ApPaterno, @ApMaterno, @Sexo, @FechaNacimiento, @FolioReferencia, 
						@FechaInicioVigencia, @FechaFinVigencia, GetDate(), @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update Rec_CatBeneficiarios 
				 Set Nombre = @Nombre, ApPaterno = @ApPaterno, ApMaterno = @ApMaterno, Sexo = @Sexo, FechaNacimiento = @FechaNacimiento, 
					 FolioReferencia = @FolioReferencia, FechaInicioVigencia = @FechaInicioVigencia, FechaFinVigencia = @FechaFinVigencia, 
					 Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
					   And IdBeneficiario = @IdBeneficiario
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdBeneficiario 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update Rec_CatBeneficiarios Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
				 And IdBeneficiario = @IdBeneficiario

		   Set @sMensaje = 'La información del Beneficiario ' + @IdBeneficiario + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdBeneficiario as Clave, @sMensaje as Mensaje 
End
Go--#SQL
