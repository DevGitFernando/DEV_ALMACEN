-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0010_ObtenerRecetasParaSurtido_Detalles' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0010_ObtenerRecetasParaSurtido_Detalles
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0010_ObtenerRecetasParaSurtido_Detalles 
( 
	@IdEmpresa varchar(3) = '1', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '15', 
	@IdCliente varchar(4) = '2', 
	@IdSubCliente varchar(4) = '13', 
	@FolioInterface varchar(20) = '6435', 
	@IdPersonal varchar(4) = '0001' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 


Declare 
	@FechaActual varchar(20),  	
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@sIdBeneficiario varchar(10), 
	@sNombreBeneficiario varchar(500), 
	@sIdMedico varchar(10), 
	@sNombreMedico varchar(500), 
	@NombreB varchar(100), @ApPatB varchar(100), @ApMatB varchar(100), @Sexo varchar(10), 
	@FechaNacimientoB varchar(20),  	
	@NumReferencia varchar(100), 
	@FechaVigencia_Inicia varchar(20), @FechaVigencia_Termina varchar(20),  
	@NombreM varchar(100), @ApPatM varchar(100), @ApMatM varchar(100), 
	@sNumeroCedula varchar(20), @sIdEspecialidad varchar(10)   
	

	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3 ) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2 ) 
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4 ) 
	Set @IdCliente = right('000000000000' + @IdCliente, 4 ) 
	Set @IdSubCliente = right('000000000000' + @IdSubCliente, 4 ) 
	Set @FolioInterface = right('000000000000' + @FolioInterface, 12 ) 



	Set @FechaActual = convert(varchar(10), getdate(), 120)  
	Set @sIdBeneficiario = '' 
	Set @sNombreBeneficiario = '' 
	Set @sIdMedico = ''   
	Set @sNombreMedico = '' 
	Set @NombreB = '' 
	Set @ApPatB = '' 
	Set @ApMatB = '' 
	Set @Sexo = '' 
	Set @NumReferencia = '' 
	Set @FechaVigencia_Inicia = '' 
	Set @FechaVigencia_Termina = '' 
	Set @NombreM = '' 
	Set @ApPatM = '' 
	Set @ApMatM = '' 
	Set @sNumeroCedula = '' 
	Set @sIdEspecialidad = '' 




------------------------------------------------- OBTENER LA INFORMACION   
	Select -- Distinct 
		G.IdEmpresa, G.IdEstado, G.IdFarmacia, 
		G.Folio as Secuenciador, G.FolioReceta, G.FechaReceta, 
		
		(G.NombreBeneficiario + ' ' + G.ApPaternoBeneficiario + ' ' + G.ApMaternoBeneficiario) as NombreCompletoBeneficiario, 	
		(G.NombreMedico + ' ' + G.ApPaternoMedico + ' ' + G.ApMaternoMedico) as NombreCompletoMedico,  
		
		cast('' as varchar(20)) as IdBeneficiario, 
		G.NombreBeneficiario, G.ApPaternoBeneficiario, G.ApMaternoBeneficiario, G.FolioAfiliacionSPSS as NumeroReferencia, 
		(case when ltrim(rtrim(G.Sexo)) in ( '1', 'M' ) then 'M' else 'F' end) as Sexo,  FechaNacimientoBeneficiario, 
		G.FechaIniciaVigencia, G.FechaTerminaVigencia, 
		
		cast('' as varchar(20))  as IdMedico, 
		G.ClaveDeMedico, G.CedulaDeMedico, G.NombreMedico, G.ApPaternoMedico, G.ApMaternoMedico,  
		
		D.CIE10 as Diagnostico, 
		cast('0001' as varchar(20)) as CIE_10, 
		cast('001' as varchar(20)) as IdServicio, cast('001' as varchar(20)) as IdArea, cast('01' as varchar(20)) as TipoDispensacion, 
				
		identity(int, 1, 1) as Keyx  
	Into #tmp_01_General 
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock) 
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
	Left Join INT_SESEQ__RecetasElectronicas_0003_Diagnosticos D (NoLock) 
		On ( X.IdEmpresa = D.IdEmpresa and X.IdEstado = D.IdEstado and X.IdFarmacia = D.IdFarmacia and X.Folio = D.Folio ) 		
	Where G.EsSurtido = 0 and G.Procesado = 0 and G.Folio = @FolioInterface 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.RecepcionDuplicada = 0  


	Select 
		EnCuadroBasicoFarmacia = cast('NO' as varchar(20)),  
		cast(I.ClaveSSA as varchar(50)) as ClaveSSA, I.CantidadRequerida, C.DescripcionClave, 	
		EnCuadroBasicoFarmacia_General = cast('NO' as varchar(20)),  
		I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.Folio  
	Into #tmp_02_Medicamentos 
	From INT_SESEQ__RecetasElectronicas_0004_Insumos I (NoLock) 	
	Inner Join #tmp_01_General E (NoLock) 
		On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.Secuenciador ) 	
	Left Join vw_ClavesSSA_Sales C (NoLock) On ( I.ClaveSSA = C.ClaveSSA ) 	
	Where I.RecepcionDuplicada = 0 

		
	---------- Aplicar mascaras 
	Update R Set ClaveSSA = M.ClaveSSA, DescripcionClave = M.Descripcion   
	From #tmp_02_Medicamentos R (NoLock) 
	Inner Join vw_ClaveSSA_Mascara M (NoLock) 
		On ( 
			M.IdEstado = @IdEstado and M.IdCliente = @IdCliente and M.IdSubCliente = @IdSubCliente 
			and replace(replace(R.ClaveSSA, '.', ''), '-', '') = replace(replace(M.Mascara, '.', ''), '-', '')  ---- Forzar la compabilidad de los datos 
			) 


	--			spp_INT_SESEQ__RecetasElectronicas_0010_ObtenerRecetasParaSurtido_Detalles  
		


	--------------------------------- En caso de ser necesario la emisión de un vale total 
	Insert Into #tmp_02_Medicamentos ( EnCuadroBasicoFarmacia, EnCuadroBasicoFarmacia_General, ClaveSSA, CantidadRequerida, DescripcionClave, IdEmpresa, IdEstado, IdFarmacia, Folio   ) 
	Select top 0 
		'NO' as EnCuadroBasicoFarmacia, 'NO' as EnCuadroBasicoFarmacia_General, 'SC-CARGAR' as ClaveSSA, 0 as CantidadRequerida, 'EMISIÓN DE VALES' as Descripcion, 	
		@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @FolioInterface  as Folio 



	--------------------------------- Validar que las Claves esten incluidas en el Cuadro Básico 
	Update I Set EnCuadroBasicoFarmacia = 'SI', EnCuadroBasicoFarmacia_General = 'SI' 
	From #tmp_02_Medicamentos I 
	Inner Join vw_CB_CuadroBasico_Farmacias C (NoLock) 
		On 
		( 
			I.IdEstado = C.IdEstado and I.IdFarmacia = C.IdFarmacia and C.IdCliente = @IdCliente and I.ClaveSSA = C.ClaveSSA 
		) 
		

	Update I Set EnCuadroBasicoFarmacia = 'SI', EnCuadroBasicoFarmacia_General = 'SI' 
	From #tmp_02_Medicamentos I 
	Inner Join vw_Claves_Precios_Asignados C (NoLock) 
		On 
		( 
			I.IdEstado = C.IdEstado and C.IdCliente = @IdCliente and I.ClaveSSA = C.ClaveSSA 
		) 


	-------------- Marcar todo dentro de cuadro, por las claves de consigna 
	Update I Set EnCuadroBasicoFarmacia = 'SI' -- + '   ' + EnCuadroBasicoFarmacia_General 
	From #tmp_02_Medicamentos I 
	where 1 = 0 
	--------------------------------- Validar que las Claves esten incluidas en el Cuadro Básico 



		
	Select 
		@sNombreMedico = ltrim(rtrim(ApPaternoMedico)) + '|' + ltrim(rtrim(ApMaternoMedico)) + '|' + ltrim(rtrim(NombreMedico)), 
		@sNombreBeneficiario = ltrim(rtrim(ApPaternoBeneficiario)) + '|' + ltrim(rtrim(ApMaternoBeneficiario)) + '|' + ltrim(rtrim(NombreBeneficiario)), 
		@NombreB = NombreBeneficiario, @ApPatB = ApPaternoBeneficiario, @ApMatB = ApMaternoBeneficiario, 
		@Sexo = Sexo, -- 
		-- @Sexo = (case when cast(Sexo as vrachar) in ( '1', 'M' ) Then 'M' Else 'F' End), 
		@FechaNacimientoB = convert(varchar(10), FechaNacimientoBeneficiario, 120),  -- (case when FechaNacimientoBeneficiario = '' then getdate() Else FechaNacimientoBeneficiario end), 
		@NumReferencia = NumeroReferencia, 
		@FechaVigencia_Inicia = convert(varchar(10), FechaIniciaVigencia, 120), 
		@FechaVigencia_Termina = convert(varchar(10), FechaTerminaVigencia, 120), 
		@NombreM = NombreMedico, @ApPatM = ApPaternoMedico, @ApMatM = ApMaternoMedico, @sIdEspecialidad = '0000', 
		@sNumeroCedula = (case when CedulaDeMedico = '' then ClaveDeMedico else CedulaDeMedico end)
	From #tmp_01_General 
	
	--	select * from #tmp_01_General  
		
------------------------------------------------- OBTENER LA INFORMACION   	




------------------------------------------------- OBTENER LA INFORMACION DE DIAGNOSTICO 
	Update G Set CIE_10 = D.ClaveDiagnostico 
	From #tmp_01_General G (NoLock) 
	Inner Join CatCIE10_Diagnosticos D (NoLock) On ( D.ClaveDiagnostico = G.Diagnostico )  
------------------------------------------------- OBTENER LA INFORMACION DE DIAGNOSTICO 





------------------------------------------------- OBTENER LA INFORMACION DE BENEFICIARIO 
---		spp_INT_SESEQ__RecetasElectronicas_0010_ObtenerRecetasParaSurtido_Detalles 	

	If Not Exists ( 
					Select top 1 * 
					From CatBeneficiarios (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente  and 
						(   ltrim(rtrim(ApPaterno)) + '|' + ltrim(rtrim(ApMaterno)) + '|' + ltrim(rtrim(Nombre))  ) = @sNombreBeneficiario 
					) 
		Begin 
			-- print 'nada' 
			Exec spp_Mtto_CatBeneficiarios 
				@IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @IdBeneficiario = '*',
				@Nombre = @NombreB, @ApPaterno = @ApPatB, @ApMaterno = @ApMatB, @Sexo = @Sexo, 					
				@FechaNacimiento = @FechaNacimientoB, @FolioReferencia = @NumReferencia, 				
				@FechaInicioVigencia = @FechaVigencia_Inicia, @FechaFinVigencia = @FechaVigencia_Termina, 				
				@iOpcion = 1, @Domicilio = '', @FolioReferenciaAuxiliar = '', @IdPersonal = @IdPersonal, 
				@MostrarResultado = 0 -- , @Resultado varchar(10) = '' output       			
		End 
	
	Select @sIdBeneficiario = IdBeneficiario 
	From CatBeneficiarios B (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and 
		upper((ltrim(rtrim(ApPaterno)) + '|' + ltrim(rtrim(ApMaterno)) + '|' + ltrim(rtrim(Nombre)))) = upper(@sNombreBeneficiario)  
		
	Update G Set IdBeneficiario = @sIdBeneficiario 
	From #tmp_01_General G (NoLock) 	

	Update B Set FechaFinVigencia = dateadd(month, 3, cast(FechaFinVigencia as datetime)) 
	From CatBeneficiarios B (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and 
		IdBeneficiario = @sIdBeneficiario 
------------------------------------------------- OBTENER LA INFORMACION DE BENEFICIARIO



------------------------------------------------- OBTENER LA INFORMACION DE BENEFICIARIO
	If Not Exists ( 
					Select * 
					From CatMedicos (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and 
						(ltrim(rtrim(ApPaterno)) + '|' + ltrim(rtrim(ApMaterno)) + '|' + ltrim(rtrim(Nombre))) = @sNombreMedico ) 
		Begin 
			Exec spp_Mtto_CatMedicos 
				@IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdMedico = '*', 
				@Nombre = @NombreM, @ApPaterno = @ApPatM, @ApMaterno = @ApMatM, @NumCedula = @sNumeroCedula,
				@IdEspecialidad = @sIdEspecialidad, @IdPersonal = @IdPersonal, @iOpcion = 1, @MostrarResultado = 0 
		End 
	
		
	Select @sIdMedico = IdMedico,  @sNumeroCedula = NumCedula 
	From CatMedicos B (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and 
		(ltrim(rtrim(ApPaterno)) + '|' + ltrim(rtrim(ApMaterno)) + '|' + ltrim(rtrim(Nombre))) = @sNombreMedico 
		
	Update G Set IdMedico = @sIdMedico, CedulaDeMedico = @sNumeroCedula   
	From #tmp_01_General G (NoLock) 	
------------------------------------------------- OBTENER LA INFORMACION DE BENEFICIARIO




---		select top 10 * from INT_SESEQ__RecetasElectronicas_0004_Insumos 

	
---		spp_INT_SESEQ__RecetasElectronicas_0010_ObtenerRecetasParaSurtido_Detalles 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	----Select 	@IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, '*', 
	----									   @NombreB, @ApPatB, @ApMatB, @Sexo, @FechaActual, @NumReferencia, 
										   ----@FechaVigencia_Inicia, @FechaVigencia_Termina, 1, '', @IdPersonal  

	Select * From #tmp_01_General 
	Select * From #tmp_02_Medicamentos Order by ClaveSSA 


	----Select * From #tmp_03_Vales_Medicamentos Order by ClaveSSA 	
	----Select * From #tmp_04_SurtidoOtraUnidad Order by ClaveSSA 
	----Select * From #tmp_05_SurtidoOtraUnidad_Medicamentos Order by ClaveSSA 
	
	
End 
Go--#SQL 
	