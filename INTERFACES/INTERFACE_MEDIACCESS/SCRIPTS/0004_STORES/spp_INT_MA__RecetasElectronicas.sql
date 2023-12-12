-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__RecetasElectronicas_001_Encabezado' and xType = 'P' ) 
   Drop Proc spp_INT_MA__RecetasElectronicas_001_Encabezado
Go--#SQL 

Create Proc spp_INT_MA__RecetasElectronicas_001_Encabezado 
( 
	@Folio_MA varchar(30) = '1', @IdFarmacia varchar(15) = '', 
	@NombrePaciente varchar(200) = '', @NombreMedico varchar(200) = '', @Especialidad varchar(200) = '',  
	@Copago smallint = 0, @PlanBeneficiario varchar(500) = '', @FechaEmision varchar(10) = '', 
	@Elegibilidad varchar(50) = '', 
	@CIE_01 varchar(10) = '', @CIE_02 varchar(10) = '', @CIE_03 varchar(10) = '', @CIE_04 varchar(10) = '', 
	@EsRecetaManual int = 0, @Consecutivo Varchar(2) = '01'
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200),
	@IdEstadoRegistra Varchar(2),
	@IdFarmaciaRegistra Varchar(4)  
	

	Select @sFolio = cast(max(cast(Folio as int)) + 1 as varchar) From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) 
	Set @sFolio = IsNull(@sFolio, '1') 
	Set @sFolio = right('000000000000000' + @sFolio, 12) 
	Set @sMensaje = 'Información guardada satisfactoriamente' 
	
	Set @CIE_01 = replace(@CIE_01, '.', '') 
	Set @CIE_01 = replace(@CIE_01, ' ', '') 	
	
	Set @CIE_02 = replace(@CIE_02, '.', '') 
	Set @CIE_02 = replace(@CIE_02, ' ', '') 	
	
	Set @CIE_03 = replace(@CIE_03, '.', '') 
	Set @CIE_03 = replace(@CIE_03, ' ', '') 	
	
	Set @CIE_04 = replace(@CIE_04, '.', '') 
	Set @CIE_04 = replace(@CIE_04, ' ', '')
	
	
	Select @Consecutivo = cast(max(cast(Consecutivo as int)) + 1 as varchar)
	From INT_MA__RecetasElectronicas_001_Encabezado (NoLock)
	Where Folio_MA = @Folio_MA
	
	Set @Consecutivo = ISNULL(@Consecutivo, 1)
	Set @Consecutivo = RIGHT('00' + @Consecutivo, 2)
	
	
	Insert Into INT_MA__RecetasElectronicas_001_Encabezado 
	( 
		Folio, Folio_MA, IdFarmacia, NombrePaciente, NombreMedico, Especialidad, Copago, PlanBeneficiario, FechaEmision, Elegibilidad, 
		CIE_01, CIE_02, CIE_03, CIE_04, EsRecetaManual, Consecutivo ) 
	Select 
		@sFolio, @Folio_MA, @IdFarmacia, @NombrePaciente, @NombreMedico, @Especialidad, @Copago, @PlanBeneficiario, @FechaEmision, @Elegibilidad, 
		@CIE_01, @CIE_02, @CIE_03, @CIE_04, @EsRecetaManual, @Consecutivo
	
	
	---- Registrar los Diagnosticos no encontrados 
	Exec spp_INT_MA__RecetasElectronicas_004_Diagnosticos @CIE_01, @CIE_02, @CIE_03, @CIE_04
	
	Select @IdEstadoRegistra = R.IdEstado
	From INT_MA__CFG_FarmaciasClinicas R (NoLock) 
	Where  R.Referencia_MA = @IdFarmacia
	
	Select @IdFarmaciaRegistra = R.IdFarmacia
	From INT_MA__CFG_FarmaciasClinicas R (NoLock) 
	Where  R.Referencia_MA = @IdFarmacia
		
		
	If Not Exists ( 
		Select * From CatMedicos (NoLock) 
		Where IdEstado = @IdEstadoRegistra and IdFarmacia = @IdFarmaciaRegistra and Nombre = @NombreMedico
		) 
	Begin 			
		Exec spp_Mtto_CatMedicos 
			@IdEstado = @IdEstadoRegistra, @IdFarmacia = @IdFarmaciaRegistra, @IdMedico = '*', 
			@Nombre = @NombreMedico, @ApPaterno = '', @ApMaterno = '', @NumCedula = '.', 
			@IdEspecialidad = '0000', @IdPersonal = '0001', @iOpcion = 1, 
			@MostrarResultado = 0
	End



------------------------- SALIDA 
	Select @sFolio as Folio, @Consecutivo As Consecutivo, @sMensaje as Mensaje 	


End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__RecetasElectronicas_002_Productos' and xType = 'P' ) 
   Drop Proc spp_INT_MA__RecetasElectronicas_002_Productos
Go--#SQL 

Create Proc spp_INT_MA__RecetasElectronicas_002_Productos 
( 
	@Folio_MA varchar(30) = '1', @Partida int = 0, @CodigoEAN varchar(30) = '', @CantidadSolicitada int = 0, @Consecutivo Varchar(2) = '01'
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(10), 
	@sMensaje varchar(200),   
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4) 

	Set @CodigoEAN = right('00000000000000000000000000' + @CodigoEAN, 8) 	
		
	Insert Into INT_MA__RecetasElectronicas_002_Productos ( Folio_MA, Partida, CodigoEAN, CantidadSolicitada, CantidadSurtida, Consecutivo ) 
	Select @Folio_MA, @Partida, @CodigoEAN, @CantidadSolicitada, 0 as CantidadSurtida, @Consecutivo
	--- From INT_MA__RecetasElectronicas_002_Productos 



	--------------- Reserva de Existencia 
	Select R.IdEmpresa, R.IdEstado, R.IdFarmacia, @CodigoEAN as IdProducto, 0 as CantidadReservada 
	Into #tmpProducto 
	From INT_MA__CFG_FarmaciasClinicas R (NoLock) 
	Inner Join INT_MA__RecetasElectronicas_001_Encabezado E (NoLock) On ( E.Folio_MA = @Folio_MA and E.IdFarmacia = R.Referencia_MA ) 

	Insert Into INT_MA__RecetasElectronicas_003_ReservaExistencia ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CantidadReservada )	
	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CantidadReservada 
	From #tmpProducto F 
	Where Not Exists 
	( 
		Select *
		From INT_MA__RecetasElectronicas_003_ReservaExistencia R 
		Where F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmacia and F.IdProducto = R.IdProducto 
	) 	
	
	Update F Set CantidadReservada = ( F.CantidadReservada + @CantidadSolicitada ) 
	From INT_MA__RecetasElectronicas_003_ReservaExistencia F (NoLock) 
	Inner Join #tmpProducto R (NoLock) 
		On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmacia and F.IdProducto = R.IdProducto ) 		
	-- Where  F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.IdProducto = @CodigoEAN   	
	

--	select * from INT_MA__RecetasElectronicas_003_ReservaExistencia 

End 
Go--#SQL 
	
	
	
-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__RecetasElectronicas_004_Diagnosticos' and xType = 'P' ) 
   Drop Proc spp_INT_MA__RecetasElectronicas_004_Diagnosticos
Go--#SQL 

Create Proc spp_INT_MA__RecetasElectronicas_004_Diagnosticos 
( 
	@CIE_01 varchar(10) = '', @CIE_02 varchar(10) = '', @CIE_03 varchar(10) = '', @CIE_04 varchar(10) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@IdDiagnostico varchar(10), 
	@CIE10_Procesar varchar(10) 
	

	Set @IdDiagnostico = '' 
	Set @CIE10_Procesar = '' 

	Select top 0  cast('' as varchar(10)) as CIE10 Into #tmp_Diagnosticos 
	Insert Into #tmp_Diagnosticos ( CIE10 ) Select @CIE_01  
	Insert Into #tmp_Diagnosticos ( CIE10 ) Select @CIE_02  
	Insert Into #tmp_Diagnosticos ( CIE10 ) Select @CIE_03  
	Insert Into #tmp_Diagnosticos ( CIE10 ) Select @CIE_04  		

	
	Declare #cursorDiagnosticos  
	Cursor For 
		Select CIE10 
		From #tmp_Diagnosticos T 
		where CIE10 <> '' 
	Open #cursorDiagnosticos 
	FETCH NEXT FROM #cursorDiagnosticos Into @CIE10_Procesar 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
					
			If Not Exists ( Select * From CatCIE10_Diagnosticos (NoLock) Where ClaveDiagnostico = @CIE10_Procesar ) 
			Begin 
				Select @IdDiagnostico = cast(max(cast(IdDiagnostico as int)) + 1  as varchar)
				From CatCIE10_Diagnosticos
				Set @IdDiagnostico = IsNull(@IdDiagnostico, '1') 
				Set @IdDiagnostico = right('0000000000' + @IdDiagnostico, 6) 
			
			
				Insert Into CatCIE10_Diagnosticos ( IdDiagnostico, ClaveDiagnostico, Descripcion, cPadre, nPadre, Status, Actualizado ) 
				Select @IdDiagnostico, @CIE10_Procesar, 'NO IDENTIFICADO', '', '1000', 'A', 0 
			End 
			FETCH NEXT FROM #cursorDiagnosticos Into @CIE10_Procesar   
		END	 
	Close #cursorDiagnosticos 
	Deallocate #cursorDiagnosticos 	
		

End 
Go--#SQL 


	