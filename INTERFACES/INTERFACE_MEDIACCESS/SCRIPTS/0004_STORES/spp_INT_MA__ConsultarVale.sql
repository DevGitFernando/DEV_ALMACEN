-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__ConsultarVale' and xType = 'P' ) 
   Drop Proc spp_INT_MA__ConsultarVale
Go--#SQL   

Create Proc spp_INT_MA__ConsultarVale
( 
	@IdSocioComercial varchar(8) = '00000001', @IdSucursal varchar(8) = '00000039', @Folio_Vale varchar(8) = '00000532',
	@IdCliente Varchar(4) = '0003', @IdSubCliente Varchar(4) = '0001',
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0011'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD
Declare 
	@iResultado int, 
	@sFolioReceta varchar(20), 		
	@sMensaje varchar(200), 
	@IdPersonal varchar(4) = '0001',
	
	@IdBeneficiario varchar(8), 
	@Beneficiario_Nombre varchar(200),
	@Beneficiario_ApPaterno varchar(200),
	@Beneficiario_ApMaterno varchar(200),
	@Beneficiario_FolioReferencia varchar(200),
	@Beneficiario_FechaNacimiento Varchar(10),
	@Beneficiario_FechaFinVigencia Varchar(10),
	@Beneficiario_Sexo Varchar(1),
	
	@IdMedico varchar(6),
	@Medico_Nombre varchar(200),
	@Medico_ApPaterno varchar(200),
	@Medico_ApMaterno varchar(200),
	@Medico_NumCedula varchar(30)
	

	Select 
		@Beneficiario_Nombre = Beneficiario_Nombre, @Beneficiario_ApPaterno = Beneficiario_ApPaterno,
		@Beneficiario_ApMaterno = Beneficiario_ApMaterno, @Beneficiario_FolioReferencia = Beneficiario_FolioReferencia,
		@Beneficiario_FechaNacimiento = Convert(Varchar(10), Beneficiario_FechaNacimiento, 120),
		@Beneficiario_FechaFinVigencia = Convert(Varchar(10), Beneficiario_FechaFinVigencia, 120), @Beneficiario_Sexo = Beneficiario_Sexo,
		@Medico_Nombre = Medico_Nombre, @Medico_ApPaterno = Medico_ApPaterno, @Medico_ApMaterno = Medico_ApMaterno, @Medico_NumCedula = Medico_NumCedula
	From INT_IME__RegistroDeVales_001_Encabezado (NoLock)
	Where IdSocioComercial = @IdSocioComercial And IdSucursal = @IdSucursal And Folio_Vale = @Folio_Vale

		If Not Exists ( 
						Select * From CatBeneficiarios (NoLock) 
						Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
							 And Nombre = @Beneficiario_Nombre And ApPaterno = @Beneficiario_ApPaterno And ApMaterno = @Beneficiario_ApMaterno
							 And FolioReferencia = @Beneficiario_FolioReferencia 
					   ) 
			Begin 

				Exec spp_Mtto_CatBeneficiarios 
					@IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, 
					@IdBeneficiario = '*', @Nombre = @Beneficiario_Nombre, @ApPaterno = @Beneficiario_ApPaterno,
					@ApMaterno = @Beneficiario_ApMaterno, @Sexo = @Beneficiario_Sexo, @FechaNacimiento = @Beneficiario_FechaNacimiento,
					@FolioReferencia = @Beneficiario_FolioReferencia, 
					@FechaInicioVigencia = '2016-01-01', @FechaFinVigencia = @Beneficiario_FechaFinVigencia, 
					@iOpcion = '1', @Domicilio = '', @FolioReferenciaAuxiliar = '', @IdPersonal = @IdPersonal, 
					@MostrarResultado = 0, @Resultado = @IdBeneficiario	output 			
			End 	
		Else
			Begin
				Select @IdBeneficiario = IdBeneficiario  
				From CatBeneficiarios B (NoLock)
				Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
							 And Nombre = @Beneficiario_Nombre And ApPaterno = @Beneficiario_ApPaterno And ApMaterno = @Beneficiario_ApMaterno
							 And FolioReferencia = @Beneficiario_FolioReferencia 
			End
			
			
		If Not Exists (
					Select *
					From CatMedicos M (NoLock)
					Where
						M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia And
						M.Nombre = @Medico_Nombre And M.ApPaterno = @Medico_ApPaterno And M.ApMaterno =  @Medico_ApMaterno And M.NumCedula = @Medico_NumCedula
				  )
		Begin			
			Exec spp_Mtto_CatMedicos 
			@IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdMedico = '*', 
			@Nombre = @Medico_Nombre, @ApPaterno = @Medico_ApPaterno, @ApMaterno = @Medico_ApMaterno, @NumCedula = @Medico_NumCedula, 
			@IdEspecialidad = '0000', @IdPersonal = @IdPersonal, @iOpcion = 1, 
			@MostrarResultado = 0, @Resultado = @IdMedico output
		End
	Else
		Begin
			Select @IdMedico = M.IdMedico
			From CatMedicos M (NoLock) 
			Where
				M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia And
				M.Nombre = @Medico_Nombre And M.ApPaterno = @Medico_ApPaterno And M.ApMaterno =  @Medico_ApMaterno And M.NumCedula = @Medico_NumCedula
		End
	
	Select @IdBeneficiario As IdBeneficiario, @IdMedico As IdMedico, CONVERT(VarChar(10),FechaReceta, 120) As FechaReceta, NumReceta, EsValeManual
	From INT_IME__RegistroDeVales_001_Encabezado E (NoLock)
	Where IdSocioComercial = @IdSocioComercial And IdSucursal = @IdSucursal And Folio_Vale = @Folio_Vale
	
End 
Go--#SQL 