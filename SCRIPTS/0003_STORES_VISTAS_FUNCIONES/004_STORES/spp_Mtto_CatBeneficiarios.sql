----------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_CatBeneficiarios' and xType = 'P' )
    Drop Proc spp_Mtto_CatBeneficiarios
Go--#SQL 
  
Create Proc spp_Mtto_CatBeneficiarios 
( 
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '',
	@IdBeneficiario varchar(10) = '', @Nombre varchar(150) = '', @ApPaterno varchar(150) = '', @ApMaterno varchar(150) = '', @Sexo varchar(2) = '', 	
	@FechaNacimiento varchar(10) = '', @FolioReferencia varchar(20) = '', @FechaInicioVigencia varchar(10) = '', @FechaFinVigencia varchar(10) = '', 
	@iOpcion smallint = 0, @Domicilio varchar(200) = '', @FolioReferenciaAuxiliar varchar(20) = '', @IdPersonal varchar(4) = '', 
	@TipoDeBenenficiario smallint = 0, @IdJurisdiccion Varchar(3) = '', 
	@CURP varchar(18) = '', @IdEstadoResidencia varchar(2) = '', @IdTipoDerechoHabiencia varchar(3) = '', @IdTipoDeIdentificacion varchar(3) = '', 
	@MostrarResultado smallint = 1, @Resultado varchar(10) = '' output       
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

	If @IdJurisdiccion = ''
	Begin
		Select @IdJurisdiccion = IdJurisdiccion From CatFarmacias Where Idestado = @Idestado And IdFarmacia = @IdFarmacia
	End

	--- Buscar el Beneficirio en Base al Folio de Referencia, es un registro de información Importada de Servidor Central 
	If @IdBeneficiario = '**' 
	Begin 
		Select Top 1 @IdBeneficiario = cast( (max(IdBeneficiario)) as varchar) 
		From CatBeneficiarios (NoLock) 
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente	
		and FolioReferencia = @FolioReferencia 
			  
		--- El Folio de SP no esta dado de Alta en la Unidad Actual 
		Set @IdBeneficiario = IsNull(@IdBeneficiario, '*') 		
	End 

	--- Validación Estandar para Beneficiarios 
	If @IdBeneficiario = '*' 
	Begin
		Select @IdBeneficiario = cast( (max(IdBeneficiario) + 1) as varchar)  
		From CatBeneficiarios (NoLock) 
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente
	End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdBeneficiario = IsNull(@IdBeneficiario, '1')
	Set @IdBeneficiario = right(replicate('0', 8) + @IdBeneficiario, 8)


	If @iOpcion = 1 
		Begin 

			If Not Exists 
				(  
					Select * From CatBeneficiarios (NoLock) 
					Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario 
				) 
				Begin 
					Insert Into CatBeneficiarios ( 
						IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, 
						Nombre, ApPaterno, ApMaterno, Domicilio, Sexo, FechaNacimiento, FolioReferencia,
						FechaInicioVigencia, FechaFinVigencia, FechaRegistro, Status, Actualizado, FolioReferenciaAuxiliar, TipoDeBeneficiario, IdJurisdiccion, CURP, IdEstadoResidencia, IdTipoDerechoHabiencia, IdTipoDeIdentificacion ) 
					Select 
						@IdEstado ,@IdFarmacia, @IdCliente, @IdSubCliente, @IdBeneficiario, 
						@Nombre, @ApPaterno, @ApMaterno, @Domicilio, @Sexo, @FechaNacimiento, @FolioReferencia, 
						@FechaInicioVigencia, @FechaFinVigencia, GetDate(), @sStatus, @iActualizado, @FolioReferenciaAuxiliar, @TipoDeBenenficiario, @IdJurisdiccion, @CURP, @IdEstadoResidencia, @IdTipoDerechoHabiencia, @IdTipoDeIdentificacion  
				End 
			Else 
				Begin 
					Update CatBeneficiarios 
					Set Nombre = @Nombre, ApPaterno = @ApPaterno, ApMaterno = @ApMaterno, 
						Domicilio = @Domicilio, 
						Sexo = @Sexo, FechaNacimiento = @FechaNacimiento, 
						FolioReferencia = @FolioReferencia, 
						FolioReferenciaAuxiliar = @FolioReferenciaAuxiliar, 
						TipoDeBeneficiario = @TipoDeBenenficiario, IdJurisdiccion = @IdJurisdiccion,
						FechaInicioVigencia = @FechaInicioVigencia, FechaFinVigencia = @FechaFinVigencia, 
						CURP = @CURP, IdEstadoResidencia = @IdEstadoResidencia, IdTipoDerechoHabiencia = @IdTipoDerechoHabiencia, 
						IdTipoDeIdentificacion = @IdTipoDeIdentificacion,  
						Status = @sStatus, Actualizado = @iActualizado
					Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario
				End 
				Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdBeneficiario 
		End 
    Else 
		Begin 
			Set @sStatus = 'C' 
			Update CatBeneficiarios Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente 
			And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario

			Set @sMensaje = 'La información del Beneficiario ' + @IdBeneficiario + ' ha sido cancelada satisfactoriamente.' 
		End 


--------------------------- Registrar el cambio del Log  
	Insert Into CatBeneficiarios_Historico 
	( 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Nombre, ApPaterno, ApMaterno, Sexo, FechaNacimiento, 
		FolioReferencia, FechaInicioVigencia, FechaFinVigencia, FechaRegistro, Status, Actualizado, 
		Domicilio, FolioReferenciaAuxiliar, IdPersonal, IdJurisdiccion, CURP, IdEstadoResidencia, IdTipoDerechoHabiencia, IdTipoDeIdentificacion   
	) 
	Select 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Nombre, ApPaterno, ApMaterno, Sexo, FechaNacimiento, 
		FolioReferencia, FechaInicioVigencia, FechaFinVigencia, FechaRegistro, Status, Actualizado, 
		Domicilio, FolioReferenciaAuxiliar, @IdPersonal as IdPersonal, IdJurisdiccion, CURP, IdEstadoResidencia, IdTipoDerechoHabiencia, IdTipoDeIdentificacion       
	From CatBeneficiarios (nolock) 
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario 


---		sp_listacolumnas CatBeneficiarios 

	Set @Resultado = @IdBeneficiario 
	If @MostrarResultado = 1 
	Begin 
		-- Regresar la Clave Generada
		Select @IdBeneficiario as Clave, @sMensaje as Mensaje 
    End 
    
End
Go--#SQL  


