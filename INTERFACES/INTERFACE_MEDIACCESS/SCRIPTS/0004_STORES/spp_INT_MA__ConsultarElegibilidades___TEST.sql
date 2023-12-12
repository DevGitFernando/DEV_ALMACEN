-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__ConsultarElegibilidades___TEST' and xType = 'P' ) 
   Drop Proc spp_INT_MA__ConsultarElegibilidades___TEST
Go--#SQL   

Create Proc spp_INT_MA__ConsultarElegibilidades___TEST
( 
	@Elegibilidad varchar(50) = 'E007078625', 
	@IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0015'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@iResultado int, 
	@sFolio varchar(20), 
	@sFolioReceta varchar(20), 		
	@sMensaje varchar(200), 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4),    
	@FechaInicioVigencia varchar(10), 
	@FechaFinVigencia varchar(10), 
	@IdBeneficiario varchar(8), 
	@IdMedico varchar(6), 
	@IdPrograma varchar(4), 
	@IdSubPrograma varchar(4),
	@IdPersonal varchar(4) = '0001',
	@IdClinica Varchar(10)
	
Declare 	
	@Empresa_y_RazonSocial varchar(500) = '', 

	@NombreEmpleado varchar(200) = '', 
	@ReferenciaEmpleado varchar(20) = '', 		

	@NombreMedico varchar(200) = '', 
	@ReferenciaMedico varchar(20) = '', 
	
	@Copago smallint = 0, 
	@CopagoEn smallint = 0 	  			
		
	
	Set @sMensaje = 'Elegibilidad guardada previamente' 
	Set @iResultado = 1  
	Set @sFolio = '' 
	Set @IdCliente = '0002'
	Set @IdSubCliente = '0001'
	Set @FechaInicioVigencia = convert(varchar(10), getdate(), 120)  
	Set @FechaFinVigencia = convert(varchar(10), dateadd(yy, 3, getdate()), 120)  		
	Set @IdBeneficiario = '' 
	Set @IdMedico = '' 
	Set @IdPrograma = '' 
	Set @IdSubPrograma = '' 
	
	
	If Exists ( Select top 1 * From INT_MA__Elegibilidades (NoLock) Where Elegibilidad = @Elegibilidad ) 
	Begin 
		--		spp_INT_MA__ConsultarElegibilidades___TEST
		
		Select Top 1 @NombreEmpleado = NombreEmpleado From INT_MA__Elegibilidades (NoLock) Where Elegibilidad = @Elegibilidad
		Select Top 1 @ReferenciaEmpleado = ReferenciaEmpleado From INT_MA__Elegibilidades (NoLock) Where Elegibilidad = @Elegibilidad
		
		
		If Not Exists ( 
						Select * From CatBeneficiarios (NoLock) 
						Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
							and FolioReferencia = @ReferenciaEmpleado 
					   ) 
			Begin 
				Exec spp_Mtto_CatBeneficiarios 
					@IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, 
					@IdBeneficiario = '*', @Nombre = @NombreEmpleado, @ApPaterno = '', @ApMaterno = '', @Sexo = 'M', 
					@FechaNacimiento = '2016-01-01', @FolioReferencia = @ReferenciaEmpleado, 
					@FechaInicioVigencia = @FechaInicioVigencia, 
					@FechaFinVigencia = @FechaFinVigencia, 
					@iOpcion = '1', @Domicilio = '', @FolioReferenciaAuxiliar = '', @IdPersonal = @IdPersonal, 
					@MostrarResultado = 0, @Resultado = @IdBeneficiario	output 
				
			End 	
		Else
			Begin
				Select @IdBeneficiario = IdBeneficiario  
				From CatBeneficiarios B (NoLock) 
				Inner Join INT_MA__Elegibilidades E (NoLock) 
					On ( --B.IdEstado = E.IdEstado and B.IdFarmacia = E.IdFarmacia and
					B.IdCliente = @IdCliente and B.IdSubCliente = @IdSubCliente 
						and B.FolioReferencia = ReferenciaEmpleado ) 
				Where B.IdEstado = @IdEstado and B.IdFarmacia = @IdFarmacia And E.Elegibilidad = @Elegibilidad
			End
			
			
		If Not Exists (
						Select *
						From CatMedicos M (NoLock) 
						--Inner Join INT_MA__Elegibilidades E (NoLock) 
						--	On ( M.IdEstado = E.IdEstado and M.IdFarmacia = E.IdFarmacia and M.NumCedula = E.ReferenciaMedico  )
						Inner Join INT_MA__RecetasElectronicas_001_Encabezado R (NoLock) On ( R.NombreMedico = M.Nombre)
						Where M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia And R.Elegibilidad = @Elegibilidad
					  )
			Begin
				Select top 1 @NombreMedico = UPPER(NombreMedico) From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) Where Elegibilidad = @Elegibilidad
				
				Exec spp_Mtto_CatMedicos 
				@IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdMedico = '*', 
				@Nombre = @NombreMedico, @ApPaterno = '', @ApMaterno = '', @NumCedula = @ReferenciaMedico, 
				@IdEspecialidad = '0000', @IdPersonal = @IdPersonal, @iOpcion = 1, 
				@MostrarResultado = 0, @Resultado = @IdMedico output
			End
		Else
			Begin
				Select @IdMedico = right('00000000' + cast(min(M.IdMedico) as varchar), 6)  
				From CatMedicos M (NoLock) 
				--Inner Join INT_MA__Elegibilidades E (NoLock) 
				--	On ( M.IdEstado = E.IdEstado and M.IdFarmacia = E.IdFarmacia and M.NumCedula = E.ReferenciaMedico  )
				Inner Join INT_MA__RecetasElectronicas_001_Encabezado R (NoLock) On ( R.NombreMedico = M.Nombre)
				Where M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia And R.Elegibilidad = @Elegibilidad
			End
		
		
		Select top 1 @IdClinica = IdFarmacia
		From INT_MA__RecetasElectronicas_001_Encabezado R
		Where R.Elegibilidad = @Elegibilidad
		
		If (@IdClinica is Null)
		Begin
				select @IdClinica = Referencia_MA From INT_MA__CFG_FarmaciasClinicas Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		End
		
		
		Select @IdPrograma = M.IdPrograma 
		From CatProgramas M (NoLock) 
		Inner Join INT_MA__Elegibilidades E (NoLock) On ( M.Descripcion = E.Empresa_y_RazonSocial and E.Elegibilidad = @Elegibilidad )		
		
		
		Select @IdSubPrograma = M.IdSubPrograma 
		From CatSubProgramas M (NoLock) 
		Inner Join INT_MA__Elegibilidades E (NoLock) On ( M.IdPrograma = @IdPrograma and M.Descripcion = E.Empresa_y_RazonSocial and E.Elegibilidad = @Elegibilidad   )				
			
		Select Top 1 @sFolioReceta = Folio_MA 
		From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) 
		Where Elegibilidad = @Elegibilidad 
		
		
		select * 
		From CatProgramas M (NoLock) 
		Inner Join INT_MA__Elegibilidades E (NoLock) On ( M.Descripcion = E.Empresa_y_RazonSocial  and E.Elegibilidad = @Elegibilidad  )				
		
		Select * 
		From CatSubProgramas M (NoLock) 
		Inner Join INT_MA__Elegibilidades E (NoLock) On ( M.IdPrograma = @IdPrograma and M.Descripcion = E.Empresa_y_RazonSocial  and E.Elegibilidad = @Elegibilidad  )						
		
		
		Select 
			Folio, FechaRegistro, IdEmpresa, IdEstado, IdFarmacia, IdPersonal, 
			Elegibilidad, Empresa_y_RazonSocial, Elegibilidad_DisponibleSurtido, 
			Elegibilidad_Surtidos, Elegibilidad_Surtidos_Aplicados, 
			@sFolioReceta as FolioReceta, 
			@IdCliente as IdCliente, @IdSubCliente as IdSubCliente,    
			@IdPrograma as IdPrograma, @IdSubPrograma as IdSubPrograma, 
			@IdBeneficiario as IdBeneficiario, NombreEmpleado, ReferenciaEmpleado, 
			@IdMedico as IdMedico, NombreMedico, ReferenciaMedico, 
			Copago, CopagoEn, @IdClinica As IdClinica
		From INT_MA__Elegibilidades (NoLock) 
		Where Elegibilidad = @Elegibilidad	

					
	End 	

End 
Go--#SQL 

