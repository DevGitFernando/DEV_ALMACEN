-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__RegistrarElegibilidades' and xType = 'P' ) 
   Drop Proc spp_INT_MA__RegistrarElegibilidades
Go--#SQL 

/* 

	Exec spp_INT_MA__RegistrarElegibilidades  @IdEmpresa = '001', @IdEstado = '09', @IdFarmacia = '0011', @IdPersonal = '0001', 
		@Elegibilidad = 'E006493943',  @Empresa_y_RazonSocial = 'MEDIACCESS SEGUROS COLECTIVOS - MAC011123M80', 
		@NombreEmpleado = 'CYNTIA FILIZOLA MARTINEZ', @ReferenciaEmpleado = 'ME281307',  
		@NombreMedico = 'SANTOS ZOZAYA  CARLOS OMAR', @ReferenciaMedico = '57678', @Copago = '0', @CopagoEn = '2'
	
*/  

Create Proc spp_INT_MA__RegistrarElegibilidades 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@IdPersonal varchar(4) = '', 

	@Elegibilidad varchar(50) = '', 		
	@Empresa_y_RazonSocial varchar(500) = '', 
	-- @Elegibilidad_DisponibleSurtido bit Not Null Default 'true', 

	@NombreEmpleado varchar(200) = '', 
	@ReferenciaEmpleado varchar(20) = '', 		

	@NombreMedico varchar(200) = '', 
	@ReferenciaMedico varchar(20) = '', 
	
	@Copago smallint = 0, 
	@CopagoEn smallint = 0, 
	@Plan_Producto varchar(500) = '' 

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
	@IdSubPrograma varchar(4)   
		
	
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
	

	If @Plan_Producto = '' 
	   Set @Plan_Producto = @Empresa_y_RazonSocial 

	
	If Not Exists ( Select top 1 * From INT_MA__Elegibilidades (NoLock) Where Elegibilidad = @Elegibilidad ) 
	Begin 
		Select @sFolio = cast(max(cast(Folio as int)) + 1 as varchar) From INT_MA__Elegibilidades (NoLock) 
		Set @sFolio = IsNull(@sFolio, '1') 
		Set @sFolio = right('000000000000000' + @sFolio, 12) 
		
		Insert Into INT_MA__Elegibilidades 
		( 
			Folio, IdEmpresa, IdEstado, IdFarmacia, IdPersonal, Elegibilidad, Elegibilidad_Surtidos, Elegibilidad_Surtidos_Aplicados, 
			Empresa_y_RazonSocial, Plan_Producto, NombreEmpleado, ReferenciaEmpleado, 
			NombreMedico, ReferenciaMedico, Copago, CopagoEn ) 
		Select 
			@sFolio, @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @Elegibilidad, 5 as Elegibilidad_Surtidos, 0 as Elegibilidad_Surtidos_Aplicados, 
			@Empresa_y_RazonSocial, @Plan_Producto, @NombreEmpleado, @ReferenciaEmpleado, 
			@NombreMedico, @ReferenciaMedico, @Copago, @CopagoEn 

		Set @iResultado = 0 	
		Set @sMensaje = 'Información guardada satisfactoriamente' 
				
	End 	
			
			
		/* 

			Exec spp_INT_MA__RegistrarElegibilidades  @IdEmpresa = '001', @IdEstado = '09', @IdFarmacia = '0011', @IdPersonal = '0001', 
				@Elegibilidad = 'E006493943',  @Empresa_y_RazonSocial = 'MEDIACCESS SEGUROS COLECTIVOS - MAC011123M80', 
				@NombreEmpleado = 'CYNTIA FILIZOLA MARTINEZ', @ReferenciaEmpleado = 'ME281307',  
				@NombreMedico = 'SANTOS ZOZAYA  CARLOS OMAR', @ReferenciaMedico = '57678', @Copago = '0', @CopagoEn = '2'
			
		*/  	

----------------------------------------   REGISTRAR EL BENEFICIARIO  		
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
			From CatBeneficiarios (NoLock) 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
				and FolioReferencia = @ReferenciaEmpleado 
		End 
		

----------------------------------------   REGISTRAR EL MEDICO 
	If Not Exists ( 
		Select * From CatMedicos (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ltrim(rtrim(NumCedula)) = ltrim(rtrim(@ReferenciaMedico))  
		) 
	Begin 			
		Exec spp_Mtto_CatMedicos 
			@IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdMedico = '*', 
			@Nombre = @NombreMedico, @ApPaterno = '', @ApMaterno = '', @NumCedula = @ReferenciaMedico, 
			@IdEspecialidad = '0000', @IdPersonal = @IdPersonal, @iOpcion = 1, 
			@MostrarResultado = 0, @Resultado = @IdMedico output 	
	End 
	Else 
		Begin
			Select @IdMedico = IdMedico  
			From CatMedicos (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ltrim(rtrim(NumCedula)) = ltrim(rtrim(@ReferenciaMedico)) 
		End 


----------------------------------------   REGISTRAR EL PROGRAMA 		
	If Not Exists ( Select * From CatProgramas (NoLock) Where ltrim(rtrim(Descripcion)) = ltrim(rtrim(@Empresa_y_RazonSocial)) ) 
	Begin 			
		Exec spp_Mtto_CatProgramas  @IdPrograma = '*', @Descripcion = @Empresa_y_RazonSocial, @iOpcion = 1, 
			@MostrarResultado = 0, @Resultado = @IdPrograma output 	
		
	End 
	Else 
		Begin 
			Select @IdPrograma = IdPrograma 
			From CatProgramas (NoLock) 
			Where ltrim(rtrim(Descripcion)) = ltrim(rtrim(@Empresa_y_RazonSocial)) 
		End 



----------------------------------------   REGISTRAR EL SUBPROGRAMA 
	If Not Exists ( Select * From CatSubProgramas (NoLock) Where IdPrograma = @IdPrograma and ltrim(rtrim(Descripcion)) = ltrim(rtrim(@Plan_Producto)) ) 
	Begin 
		Exec spp_Mtto_CatSubProgramas  @IdPrograma = @IdPrograma, @IdSubPrograma = '*', @Descripcion = @Plan_Producto, @iOpcion = 1, 
			@MostrarResultado = 0, @Resultado = @IdSubPrograma output 
	End 
	Else 
		Begin 
			Select @IdPrograma = IdPrograma, @IdSubPrograma = IdSubPrograma 
			From CatSubProgramas (NoLock) 
			Where IdPrograma = @IdPrograma and ltrim(rtrim(Descripcion)) = ltrim(rtrim(@Plan_Producto))
		End 




----------------------------------------   OBTENER EL FOLIO DE RECETA 
	Select Top 1 @sFolioReceta = Folio_MA 
	From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) 
	Where Elegibilidad = @Elegibilidad 
		
			
	------------------------- SALIDA 
	Select 
		@iResultado as Resultado, @sFolio as Folio, @sFolioReceta as FolioReceta, 
		@IdCliente as IdCliente, @IdSubCliente as IdSubCliente,    
		@IdPrograma as IdPrograma, @IdSubPrograma as IdSubPrograma, 
		@IdBeneficiario as IdBeneficiario, @IdMedico as IdMedico, 
		@sMensaje as Mensaje 	

End 
Go--#SQL 

