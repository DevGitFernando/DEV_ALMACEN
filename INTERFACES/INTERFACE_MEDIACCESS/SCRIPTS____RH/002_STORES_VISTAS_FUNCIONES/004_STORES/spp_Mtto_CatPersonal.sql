
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPersonal' and xType = 'P')
    Drop Proc spp_Mtto_CatPersonal
Go--#SQL
  
Create Proc spp_Mtto_CatPersonal 
( 
	@IdPersonal varchar(8) = '*', @CURP varchar(20) = 'CURP', 
	@Nombre varchar(50) = 'EDGAR', @ApPaterno varchar(50) = 'MANJARREZ', @ApMaterno varchar(50) = 'LOPEZ', 
	@FechaNacimiento varchar(10) = '1979-12-31', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0188', 
	@IdPuesto varchar(3) = '001', @IdDepartamento varchar(3) = '002', 
	@Sexo varchar(1) = 'M', @IdEscolaridad varchar(2) = '03', 
	@IdTipoContrato varchar(2) = '01', @FechaIngreso varchar(10) = '2009-07-01',
	@IdEstado_Domicilio varchar(2) = '21', @IdMunicipio_Domicilio varchar(4) = '0001', @IdColonia_Domicilio varchar(4) = '0001', 
	@Calle_Domicilio varchar(100) = 'JUAN JOSE RIOS', @Numero_Domicilio varchar(20) = '599', @CodigoPostal_Domicilio varchar(10) = '80235', 
	@EMail varchar(100) = 'CORREO@HOTMAIL.COM', @Password varchar(500) = '', @IdGrupoSanguineo varchar(2) = '01', @Alergias varchar(8000)= '',
	@NombreFotoPersonal varchar(200) = '', @FotoPersonal text = '', @iOpcion smallint = 1, @DiasDeAguinaldo int = 0
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint,
		@iHistorial tinyint



-- Password varchar(500) Collate Latin1_General_CI_AI Not Null Default '', 	
/*Opciones

	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set Dateformat YMD
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	Set @iHistorial = 0

	If @IdPersonal = '*'
		Begin 
			Select @IdPersonal = cast( (max(IdPersonal) + 1) as varchar)  From CatPersonal (NoLock)
			--Set @iHistorial = 1
		End

	-- Asegurar que IdPersonal sea valido y formatear la cadena 
	Set @IdPersonal = IsNull(@IdPersonal, '1')
	Set @IdPersonal = right(replicate('0', 8) + @IdPersonal, 8)
	
	If @iHistorial = 0
	Begin
		If @IdEstado <> isnull((Select top 1 IdEstado From CatPersonal_HistorialOperaciones (Nolock) Where IdPersonal = @IdPersonal order by keyx desc ), '' )
		Begin
			Set @iHistorial = 1
		End
	End


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatPersonal (NoLock) Where IdPersonal = @IdPersonal ) 
			  Begin 
				 Insert Into CatPersonal ( IdPersonal, CURP, Nombre, ApPaterno, ApMaterno, FechaNacimiento, IdEstado, IdFarmacia, 
						IdPuesto, IdDepartamento, Sexo, IdEscolaridad, IdTipoContrato, FechaIngreso, 
						IdEstado_Domicilio, IdMunicipio_Domicilio, IdColonia_Domicilio, Calle_Domicilio, Numero_Domicilio, CodigoPostal_Domicilio, 
						EMail, Password, IdGrupoSanguineo, Alergias, NombreFotoPersonal, FotoPersonal, Status, Actualizado, DiasDeAguinaldo  ) 
				 Select @IdPersonal, @CURP,	@Nombre, @ApPaterno, @ApMaterno, @FechaNacimiento, @IdEstado, @IdFarmacia, 
						@IdPuesto, @IdDepartamento, @Sexo, @IdEscolaridad,	@IdTipoContrato, @FechaIngreso, 
						@IdEstado_Domicilio, @IdMunicipio_Domicilio, @IdColonia_Domicilio, @Calle_Domicilio, @Numero_Domicilio, @CodigoPostal_Domicilio, 
						@EMail, @Password, @IdGrupoSanguineo, @Alergias, @NombreFotoPersonal, @FotoPersonal, @sStatus, @iActualizado, @DiasDeAguinaldo 
              End 
		   Else 
			  Begin 
			     Update CatPersonal Set CURP = @CURP, Nombre = @Nombre, ApPaterno = @ApPaterno, ApMaterno = @ApMaterno, 
										FechaNacimiento = @FechaNacimiento, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, 
										IdPuesto = @IdPuesto, IdDepartamento = @IdDepartamento, Sexo = @Sexo, IdEscolaridad = @IdEscolaridad, 
										IdTipoContrato = @IdTipoContrato, FechaIngreso = @FechaIngreso, 
										IdEstado_Domicilio = @IdEstado_Domicilio, IdMunicipio_Domicilio = @IdMunicipio_Domicilio, 
										IdColonia_Domicilio = @IdColonia_Domicilio, Calle_Domicilio = @Calle_Domicilio, Numero_Domicilio = @Numero_Domicilio, 
										CodigoPostal_Domicilio = @CodigoPostal_Domicilio, 
										EMail = @EMail, Password = @Password,  IdGrupoSanguineo = @IdGrupoSanguineo, Alergias = @Alergias,
										NombreFotoPersonal = @NombreFotoPersonal, FotoPersonal = @FotoPersonal,
										DiasDeAguinaldo = @DiasDeAguinaldo,
										Status = @sStatus, Actualizado = @iActualizado
				 Where IdPersonal = @IdPersonal  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdPersonal 
		   
		   If @iHistorial = 1
		   Begin				
				Insert Into CatPersonal_HistorialOperaciones ( IdPersonal, IdEstado, FechaRegistro ) 
				Select @IdPersonal, @IdEstado, GETDATE()				
		   End
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPersonal Set Status = @sStatus, Actualizado = @iActualizado Where IdPersonal = @IdPersonal 
		   Set @sMensaje = 'La información del Personal ' + @IdPersonal + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdPersonal as Clave, @sMensaje as Mensaje 
End
Go--#SQL
