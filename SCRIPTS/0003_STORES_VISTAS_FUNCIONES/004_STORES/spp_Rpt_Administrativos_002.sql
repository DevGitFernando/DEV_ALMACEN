-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
If Exists ( select * from sysobjects (NoLock) Where Name = 'RptAdmonDispensacion' and xType = 'U' ) 
	Drop table RptAdmonDispensacion 
Go--#SQL 

If Exists ( select * from sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_Detallado' and xType = 'U' ) 
	Drop table RptAdmonDispensacion_Detallado 
Go--#SQL 

-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
--	spp_Rpt_Administrativos_001 

--	 Exec spp_Rpt_Administrativos_002 @TipoDispensacion = 0, @FechaInicial = '2017-05-01', @FechaFinal = '2017-05-01', @TipoInsumoMedicamento = 0, @MostrarPrecios = 1, @IncluirDetalleInformacion = 1        

If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_Administrativos_002' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_002 
Go--#SQL 
 
Create Proc spp_Rpt_Administrativos_002 
( 
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-06-30', 
	@TipoInsumoMedicamento tinyint = 0, @MostrarPrecios tinyint = 0, @IncluirDetalleInformacion tinyint = 1,
	@MostrarDevoluciones tinyint = 0    
)  
With Encryption 
As 
Begin 
Set NoCount On    
Set DateFormat YMD 

Declare 
	@bPrint bit, 
	@iPrints int 

Declare 
	@sIdEstado varchar(2), 
	@sIdFarmacia varchar(4), 
	@dPorcentaje numeric(14,4), 
	@sParametro_01 varchar(100), 
	@sParametro_02 varchar(100), 	 
	@ProgramaSubPrograma_ForzaCajas_Habilitado bit  

--- Obtener el Procentaje para los Productos No Licicitados/Conveniados 
	Select Top 1 @sIdEstado = IdEstado, @sIdFarmacia = IdFarmacia From RptAdmonDispensacion (NoLock) 
	Select @dPorcentaje = dbo.fg_ObtenerPorcentajePrecios(@sIdEstado) 
	Set @iPrints = 0 
	Set @bPrint = 1 

-------------	Obtener Parámetros pra validacion de Dispensacion 
	Set @ProgramaSubPrograma_ForzaCajas_Habilitado = 0  
	Select @sParametro_01 = Valor 
	From Net_CFGC_Parametros (NoLock) 
	Where IdEstado = @sIdEstado and IdFarmacia = @sIdFarmacia and NombreParametro = 'ForzarCapturaEnMultiplosHabilitarValidaciones' 

	Select @sParametro_02 = Valor 
	From Net_CFGC_Parametros (NoLock) 
	Where IdEstado = @sIdEstado and IdFarmacia = @sIdFarmacia and NombreParametro = 'ForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma' 

	--Select @sParametro_01 as Parametro_01, @sParametro_02 as Parametro_02
	If @sParametro_01 = 'TRUE' and @sParametro_02 = 'TRUE' 
	Begin 
		Set @ProgramaSubPrograma_ForzaCajas_Habilitado = 1 
	End 
	--Set @ProgramaSubPrograma_ForzaCajas_Habilitado = 1 
-------------	Obtener Parámetros pra validacion de Dispensacion 


----------------------- FARMACIAS Y CLAVES 
----	Select * 
----	Into #vw_ClavesSSA_Sales 
----	From vw_ClavesSSA_Sales 
----------------------- FARMACIAS Y CLAVES 

--		drop table RptAdmonDispensacion 


----------- Total de renglones a procesar 
	-- Select count(*) as Registros From RptAdmonDispensacion B (NoLock)  


----------------------------------  Validar el Factor de configurado en los Precios 
	--Select top 5 * 
	--From RptAdmonDispensacion Where ClaveSSA = '060.439.0039' 

	--Select top 5 * 
	--From RptAdmonDispensacion_Detallado  Where ClaveSSA = '060.439.0039' 

----------------------------------  
	--Set @iPrints = @iPrints + 1 
	--if @bPrint = 1 Print 'Proceso ' + cast(@iPrints as varchar(10))  

	Update B Set Empresa = I.Nombre 
	From RptAdmonDispensacion B (NoLock) 
	Inner Join CatEmpresas I (NoLock) On ( B.IdEmpresa = I.IdEmpresa ) 

	Update B Set IdClaveSSA_Sal = I.IdClaveSSA_Sal, ClaveSSA = I.ClaveSSA, DescripcionSal = I.DescripcionSal, 
		 DescProducto = I.DescripcionClave, DescripcionCorta = I.DescripcionCortaClave, 
		 ContenidoPaquete = I.ContenidoPaquete,  
		 ContenidoPaquete_ClaveSSA = I.ContenidoPaquete_ClaveSSA,  
		 ContenidoPaquete_ClaveSSA_Licitado = I.ContenidoPaquete_ClaveSSA,  
		 EsControlado = I.EsControlado, 
		 IdGrupoTerapeutico = I.IdGrupoTerapeutico, GrupoTerapeutico = I.GrupoTerapeutico   
	From RptAdmonDispensacion B (NoLock) 
	Inner Join vw_Productos_CodigoEAN__PRCS_VLDCN I (NoLock) On ( B.IdProducto = I.IdProducto and B.CodigoEAN = I.CodigoEAN ) 


	--- select * from RptAdmonDispensacion 

--	Select top 1 * From vw_Productos_CodigoEAN__PRCS_VLDCN 


----------------------------------  Marcar solo para Precios de Lista 
----	Update B Set UsaPrecioLicitacion = 0 
----	From RptAdmonDispensacion B (NoLock) 
	

------------------------- 2K111121.1310 Jesus Diaz	
------------- Se modifica función para que tome el precio por ClaveSSA ( Todas las Claves Relacionadas )  
---------------------------- Reemplazo de Claves  
	Update B Set IdClaveSSA_Sal = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionSal = C.Descripcion, DescripcionCorta = C.DescripcionCorta,  
		ContenidoPaquete_ClaveSSA = C.ContenidoPaquete, 
		Multiplo = C.Multiplo  
	From RptAdmonDispensacion B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) 
		On ( B.IdEstado = C.IdEstado and B.IdCliente = C.IdCliente and B.IdSubCliente = C.IdSubCliente 
			and B.IdClaveSSA_Sal = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
	Where EsConsignacion = 0 and AfectaVenta = 1 


	Update B Set IdClaveSSA_Sal = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionSal = C.Descripcion,  DescripcionCorta = C.DescripcionCorta,  
		ContenidoPaquete_ClaveSSA = C.ContenidoPaquete, 
		Multiplo = C.Multiplo  
	From RptAdmonDispensacion B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) 
		On ( B.IdEstado = C.IdEstado and B.IdCliente = C.IdCliente and B.IdSubCliente = C.IdSubCliente 
			and B.IdClaveSSA_Sal = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
	Where EsConsignacion = 1 and AfectaConsigna = 1 


	Update D Set Cantidad_A_Cobro = (Cantidad / Multiplo), Cantidad = (Cantidad / Multiplo) 
	From RptAdmonDispensacion D (NoLock) 
---------------------------- Reemplazo de Claves  


------- Identificar todas la claves que son tipo Seguro Popular 	
	Update B Set EsSeguroPopular = I.EsSeguroPopular, TituloSeguroPopular = 'SEGURO POPULAR'  
	From RptAdmonDispensacion B (NoLock) 
	Inner Join CatClavesSSA_SeguroPopular I (NoLock) On ( B.ClaveSSA = I.ClaveSSA ) 

	Update B Set TituloSeguroPopular = 'NO SEGURO POPULAR'  
	From RptAdmonDispensacion B (NoLock) 
	Where EsSeguroPopular  = 2 
	
	
	if @TipoInsumoMedicamento <> 0 
	   Begin
	      if @TipoInsumoMedicamento = 1 
	         Delete from RptAdmonDispensacion Where EsSeguroPopular = 2 
	         
	      if @TipoInsumoMedicamento = 2 
	         Delete from RptAdmonDispensacion Where EsSeguroPopular = 1 	         
	   End 
	
------- Identificar todas la claves que son tipo Seguro Popular 		
	
--- Identificar el Tipo de Insumo 
	Update B Set TipoDeInsumo = 'MEDICAMENTO'	
	From RptAdmonDispensacion B (NoLock) 	
	Where TipoInsumo = 1 

	Update B Set TipoDeInsumo = 'MATERIAL DE CURACIÓN Y OTROS'	
	From RptAdmonDispensacion B (NoLock) 	
	Where TipoInsumo = 2 


-------------------------------- Informacion de Clientes y Programas  	
	Update B Set NombreCliente = I.NombreCliente, NombreSubCliente = I.NombreSubCliente 
	From RptAdmonDispensacion B (NoLock) 
	Inner Join vw_Clientes_SubClientes I (NoLock) On ( B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente ) 

	Update B Set Programa = I.Programa, SubPrograma = I.SubPrograma 
	From RptAdmonDispensacion B (NoLock) 
	Inner Join vw_Programas_SubProgramas I (NoLock) On ( B.IdPrograma = I.IdPrograma and B.IdSubPrograma = I.IdSubPrograma ) 

	-- Set @iPrints = @iPrints + 1 
	-- if @bPrint = 1 Print 'Proceso ' + cast(@iPrints as varchar(10))  
 		
-------------------------------- Reportes Especiales 	
	If @IncluirDetalleInformacion = 1 
	Begin 
		--- Agregar informacion Adicional 
		Update B Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = V.FechaReceta, 
			IdUMedica = V.IdUMedica,  
			IdMedico = V.IdMedico, -- Medico = I.NombreCompleto, 
			IdServicio = V.IdServicio, IdArea = V.IdArea, 
			-- IdDiagnostico = right('00000000' + V.IdDiagnostico, 6)      
			IdDiagnostico = V.IdDiagnostico 
		From RptAdmonDispensacion B (NoLock) 
		Inner Join VentasInformacionAdicional V (NoLock)  
			on ( B.IdEmpresa = V.IdEmpresa and B.IdEstado = V.IdEstado and B.IdFarmacia = V.IdFarmacia and B.Folio = V.FolioVenta )
		--Inner Join vw_Medicos I (NoLock) On ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia and V.IdMedico = I.IdMedico ) 

		--- Datos del Medico 
		Update B Set Medico = I.NombreCompleto  
		From RptAdmonDispensacion B (NoLock) 
		Inner Join vw_Medicos I (NoLock) On ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia and B.IdMedico = I.IdMedico ) 
		
		
		Update B Set EsRecetaForanea = 1, CLUES_UMedica = M.CLUES, Nombre_UMedica = M.NombreUnidadMedica 
		From RptAdmonDispensacion B (NoLock) 
		Inner Join CatUnidadesMedicas M (NoLock) On ( B.IdUMedica = M.IdUMedica ) 
		Where B.IdUMedica <> '000000' 
	End 
	

------------------------------------------------ Datos de Beneficiario 
	--Select 
	--	IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, FolioReferencia, 
	--	Nombre, ApPaterno, ApMaterno, ltrim(rtrim( (ApPaterno + ' ' + ApMaterno + ' ' + Nombre))) as NombreCompleto  
	--Into #tmpBeneficiarios 	
	--From CatBeneficiarios B (NoLock) 
	--Where Exists ( Select IdEstado, IdFarmacia From RptAdmonDispensacion T (NoLock) 
	--	Where T.IdEstado = B.IdEstado and T.IdFarmacia = B.IdFarmacia )
	
	--Update B Set Beneficiario = I.NombreCompleto, FolioReferencia = I.FolioReferencia  
	--From RptAdmonDispensacion B (NoLock) 
	---- Inner Join vw_Beneficiarios I (NoLock) 
	--Inner Join #tmpBeneficiarios I (NoLock) 	
	--	on ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia 
	--		 and B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente and B.IdBeneficiario = I.IdBeneficiario ) 	

	-- Set @iPrints = @iPrints + 1 
	-- if @bPrint = 1 Print 'Proceso P ' + cast(@iPrints as varchar(10))  

	-------- AJUSTE 
	Update B Set Beneficiario = I.NombreCompleto, FolioReferencia = I.FolioReferencia, CURP = I.CURP 
	From RptAdmonDispensacion B (NoLock) 
	-- Inner Join vw_Beneficiarios I (NoLock) 
	Inner Join 
	( 
		Select 
			IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, FolioReferencia, CURP, 
			Nombre, ApPaterno, ApMaterno, ltrim(rtrim( (ApPaterno + ' ' + ApMaterno + ' ' + Nombre))) as NombreCompleto  
		From CatBeneficiarios B (NoLock) 
		Where Exists ( Select IdEstado, IdFarmacia From RptAdmonDispensacion T (NoLock) 
			Where T.IdEstado = B.IdEstado and T.IdFarmacia = B.IdFarmacia )
	) I On ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia 
			 and B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente and B.IdBeneficiario = I.IdBeneficiario ) 	


	-------------- 20200117.1010  Proceso especial para Puebla 
	If Exists ( Select top 1 * from RptAdmonDispensacion (NoLock) Where IdEstado = '21' ) 
	Begin 
		----- Poblacion abierta  
		Update R Set CURP = (case when CURP <> '' Then CURP else 'N/A' end)  
		From RptAdmonDispensacion R 
		Where 1 = 1 -- CURP <> '' --- and len(FolioReferencia)  10 
			And 
			( 
				IdPrograma = '0002' and IdSubPrograma = '1312' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1403' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1409' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1411' 
			) 


		----- Poblacion abierta  
		Update R Set CURP = 'N/A'  
		From RptAdmonDispensacion R  
		Where 1 = 1 -- CURP <> '' --- and len(FolioReferencia)  10 
			And 
			( 
				IdPrograma = '0002' and IdSubPrograma = '1404' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1406' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1412' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1505' 
			) 

		----- Seguro Popular 
		Update R Set CURP = FolioReferencia 
		From RptAdmonDispensacion R  
		Where 1 = 1 -- FolioReferencia <> '' and len(FolioReferencia) >= 10 
			And 
			( 
				IdPrograma = '0002' and IdSubPrograma = '0001' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1381' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1408' 
				or 
				IdPrograma = '0002' and IdSubPrograma = '1410' 				
			) 


		----- Seguro Popular - Colectivos 
		Update R Set CURP = 'Colectivo' 
		From RptAdmonDispensacion R  
		Where 1 = 1 -- CURP <> '' --- and len(FolioReferencia)  10 
			And 
			( 
				IdPrograma = '0002' and IdSubPrograma = '0015' 
			) 
	End 
------------------------------------------------ Datos de Beneficiario 



---------------- Agregar informacion de Derechohabiencia y Estado de Residencia  
	------- 20200126.0855 
	Update B Set EstadoResidencia = E.Nombre, ClaveRENAPO_EstadoResidencia = E.ClaveRENAPO  
	From RptAdmonDispensacion B (NoLock) 
	Inner Join CatEstados E (NoLock) On ( B.IdEstadoResidencia = E.IdEstado ) 

	Update B Set TipoDerechoHabiencia = E.Descripcion 
	From RptAdmonDispensacion B (NoLock) 
	Inner Join CatTiposDeDerechohabiencia E (NoLock) On ( B.IdTipoDerechoHabiencia = E.IdTipoDerechoHabiencia ) 
---------------- Agregar informacion de Derechohabiencia y Estado de Residencia  



---------------- Datos de Diagnostico y Areas 
	If @IncluirDetalleInformacion = 1 
	Begin 
		----Select * 
		----Into #vw_CIE10_Diagnosticos 
		----From vw_CIE10_Diagnosticos 	
		
		----Select * 
		----Into #vw_Servicios_Areas
		----From vw_Servicios_Areas 	
		
		Update B Set Servicio = I.Servicio, Area = I.Area_Servicio  
		From RptAdmonDispensacion B (NoLock) 
		Inner Join vw_Servicios_Areas__PRCS_VLDCN I (NoLock) On ( B.IdServicio = I.IdServicio and B.IdArea = I.IdArea ) 
		
		Update B Set IdGrupo = I.IdGrupo, GrupoClaves = I.GrupoClaves, DescripcionGrupo = I.DescripcionGrupo, 
			  SubGrupo = I.SubGrupo, ClaveSubGrupo = I.ClaveSubGrupo, DescripcionSubGrupo = I.DescripcionSubGrupo, 
			  ClaveDiagnostico = I.ClaveDiagnostico, Diagnostico = I.Diagnostico  	
		From RptAdmonDispensacion B (NoLock) 
		Inner Join vw_CIE10_Diagnosticos__PRCS_VLDCN I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico )  
		

		Update B Set IdDiagnostico = '0001' 
		From RptAdmonDispensacion B (NoLock) 
		-- Inner Join #vw_CIE10_Diagnosticos I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico ) 	
		Where B.Diagnostico = '' 

		Update B Set IdGrupo = I.IdGrupo, GrupoClaves = I.GrupoClaves, DescripcionGrupo = I.DescripcionGrupo, 
			  SubGrupo = I.SubGrupo, ClaveSubGrupo = I.ClaveSubGrupo, DescripcionSubGrupo = I.DescripcionSubGrupo, 
			  ClaveDiagnostico = I.ClaveDiagnostico, Diagnostico = I.Diagnostico  	
		From RptAdmonDispensacion B (NoLock) 
		Inner Join vw_CIE10_Diagnosticos__PRCS_VLDCN I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico )  
		Where B.Diagnostico = '' 	
	End 
---------------- Datos de Diagnostico y Areas 		
-------------------------------- Reportes Especiales 	 		

--	  select top 10 * from RptAdmonDispensacion (NoLock) where IdGrupoPrecios = 1 
		
	-- Set @iPrints = @iPrints + 1 
	-- if @bPrint = 1 Print 'Proceso ' + cast(@iPrints as varchar(10))  

--------------------------------------- Asignar Precio 
	Update B Set 
		Cantidad = (B.Cantidad * IsNull(PC.Factor, 1)), Factor = IsNull(PC.Factor, 1),  
		PrecioLicitacion = cast(IsNull(PC.Precio, 0) as numeric(14,2)), 
		--PrecioLicitacionUnitario = cast(round(IsNull(PC.PrecioUnitario, 0), 2, 1) as numeric(14,2)), 
		PrecioLicitacionUnitario = cast(round(IsNull(PC.PrecioUnitario_Licitacion, 0), 2, 1) as numeric(14,2)), 
		ContenidoPaquete_ClaveSSA = IsNull(PC.ContenidoPaquete, B.ContenidoPaquete_ClaveSSA), 
		ContenidoPaquete_ClaveSSA_Licitado	= IsNull(PC.ContenidoPaquete_Licitado, B.ContenidoPaquete_ClaveSSA),
		IdGrupoPrecios = (case when IsNull(PC.Precio, 0) <= 0 Then 1 Else 2 End), 
		DescripcionGrupoPrecios = (case when IsNull(PC.Precio, 0)  <= 0 Then 'SIN PRECIO DE LICITACION' Else 'PRECIO LICITACION ASIGNADO' End),  
		DescProducto = (case when IsNull(PC.Precio, 0) <= 0 Then '.' + DescProducto Else DescProducto End), 
		DescripcionCorta = (case when IsNull(PC.Precio, 0) <= 0 Then '.' + DescripcionCorta Else DescripcionCorta End)		 
	From RptAdmonDispensacion B (NoLock) 
	Left Join vw_Claves_Precios_Asignados__PRCS_VLDCN PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
		On ( B.IdEstado = PC.IdEstado and B.IdCliente = PC.IdCliente and B.IdSubCliente = PC.IdSubCliente 
		     -- and B.IdClaveSSA_Sal = PC.IdClaveSSA 
		     and B.ClaveSSA = PC.ClaveSSA and PC.Status = 'A' 
		    ) 


	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFG_ClavesSSA_Precios_Programas' and xType = 'U' ) 
	Begin 
		--Set @iPrints = @iPrints + 1 
		--if @bPrint = 1 Print 'Proceso ' + cast(@iPrints as varchar(10))  
		
		Update B Set 
			Cantidad = (B.Cantidad * PC.Factor), Factor = PC.Factor, 
			PrecioLicitacion = cast(IsNull(PP.Precio, 0) as numeric(14,2)), 
			--PrecioLicitacionUnitario = cast(round(IsNull(PC.PrecioUnitario, 0), 2, 1) as numeric(14,2)), 
			PrecioLicitacionUnitario = cast(round(IsNull(PC.PrecioUnitario_Licitacion, 0), 2, 1) as numeric(14,2)) 
		From RptAdmonDispensacion B (NoLock) 
		Inner Join vw_Claves_Precios_Asignados__PRCS_VLDCN PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
			On ( B.IdEstado = PC.IdEstado and B.IdCliente = PC.IdCliente and B.IdSubCliente = PC.IdSubCliente 
				 -- and B.IdClaveSSA_Sal = PC.IdClaveSSA 
				 and B.ClaveSSA = PC.ClaveSSA and PC.Status = 'A' 
				)
		Inner Join CFG_ClavesSSA_Precios_Programas PP (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
			On ( PP.IdEstado = PC.IdEstado and PP.IdCliente = PC.IdCliente and PP.IdSubCliente = PC.IdSubCliente 
				 -- and B.IdClaveSSA_Sal = PC.IdClaveSSA 
				 and PP.IdClaveSSA_Sal = PC.IdClaveSSA and PC.Status = 'A' 
				 And B.IdPrograma = PP.IdPrograma And B.IdSubPrograma = PP.IdSubPrograma
				) 
	End 


-- between convert(varchar(10), @FechaInicial, 120) and convert(varchar(10), @FechaFinal, 120)

------  Marcar los Precios 		
	Update B Set FechaInicial = cast(@FechaInicial as datetime),  FechaFinal = cast(@FechaFinal as datetime), 
		 ImporteEAN = cast((Cantidad * PrecioLicitacionUnitario) as numeric(34, 4)), 
	     IdGrupoPrecios	= (case when PrecioLicitacion <= 0 Then 1 Else 2 End), 
		 DescripcionGrupoPrecios = (case when PrecioLicitacion <= 0 Then 'SIN PRECIO DE LICITACION' Else 'PRECIO LICITACION ASIGNADO' End)
	From RptAdmonDispensacion B (NoLock) 		
	-- Where IdGrupoPrecios <> 1 

	--Set @iPrints = @iPrints + 1 
	--if @bPrint = 1 Print 'Proceso ' + cast(@iPrints as varchar(10))  


-------------------------- Jesus Diaz 2K111122.1628 
-------------------- Habilitar solo cuando se manejen Precios de Venta para Dispensación 
-------------   Asignar Precios a Productos Faltantes 
--------	Update B Set PrecioLicitacion = (PrecioUnitario * ( 1 + (@dPorcentaje / 100.00)) ), 
--------		 ImporteEAN = (Cantidad * (PrecioUnitario * ( 1 + (@dPorcentaje / 100.00)) ))  
--------	From RptAdmonDispensacion B (NoLock) 
--------	Where IdGrupoPrecios = 1 		 
-------------   Asignar Precios a Productos Faltantes 


---------------------------------------------------------------------------------------------------------
------------------- Informacion adicional para Validación y Facturacion  ---- AQUI 
	Update B Set SubTotalLicitacion_0 = cast(round(ImporteEAN, 8) as numeric(34, 4))   --  ImporteEAN 
	From RptAdmonDispensacion B 
	Where TipoInsumo = 1 
	
	Update B Set SubTotalLicitacion = cast(round(ImporteEAN, 8) as numeric(34, 4))   --  ImporteEAN  
	From RptAdmonDispensacion B 
	Where TipoInsumo = 2 	
	
	Update B Set IvaLicitacion = 
		-- SubTotalLicitacion * ( (TasaIva / 100.00) )  
		cast(round(SubTotalLicitacion * ( (TasaIva / 100.00) ), 8) as numeric(34, 4))
	From RptAdmonDispensacion B 
	Where TipoInsumo = 2 	
	
	Update B Set TotalLicitacion = SubTotalLicitacion_0 +  ( SubTotalLicitacion + IvaLicitacion )
	From RptAdmonDispensacion B 	

	Update B Set 
		SubTotalLicitacion_0 = round(SubTotalLicitacion_0, 8), 
		SubTotalLicitacion = round(SubTotalLicitacion, 8), 
		IvaLicitacion = round(IvaLicitacion, 8),  					
		TotalLicitacion = round(TotalLicitacion, 8) 
	From RptAdmonDispensacion B 	




--- Reporte Ambos asegurar q solo Venta muestre precios 		
	Update B Set 
		PrecioUnitario = 0, PrecioLicitacion = 0, PrecioLicitacionUnitario = 0, ImporteEAN = 0,  
		SubTotalLicitacion_0 = 0 , SubTotalLicitacion = 0, IvaLicitacion = 0, TotalLicitacion = 0 
	From RptAdmonDispensacion B 		
	Where EsConsignacion = 1 
------------------- Informacion adicional para Validación y Facturacion 
--------------------------------------------------------------------------------------------------------- 

--- No asignar ningun precio a lo no Licitado 	
    Update B Set 
        ImporteEAN = 0, PrecioLicitacion = 0, PrecioLicitacionUnitario = 0, 
	    SubTotalLicitacion_0 = 0, SubTotalLicitacion = 0, 
	    IvaLicitacion = 0, TotalLicitacion = 0  
    From RptAdmonDispensacion B (NoLock) 
    Where IdGrupoPrecios = 1     
----- Informacion adicional para Validación y Facturacion 	

	--Set @iPrints = @iPrints + 1 
	--if @bPrint = 1 Print 'Proceso ' + cast(@iPrints as varchar(10))  

----------------------------------------- Se pasa este proceso al store spp_Rpt_Administrativos_008 
----------------------------------------- Reemplazo de Titulos	
----------	Update B Set IdCliente = '0001', 
----------		NombreCliente = R.Cliente, NombreSubCliente = R.SubCliente, Programa = R.Programa, SubPrograma = R.SubPrograma
----------	From RptAdmonDispensacion B (NoLock) 
----------	Inner Join CFG_EX_Validacion_Titulos R (NoLock) 
----------		On (
----------				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
----------				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma 
----------		   )
----------------------------------------- Reemplazo de Titulos
----------
----------------------------------------- Reemplazo de Nombre de Beneficiario	
----------	Update B Set 
----------		FolioReferencia = (case when R.ReemplazarFolioReferencia = 1 Then R.FolioReferencia Else B.FolioReferencia End),  
----------		Beneficiario = (case when R.ReemplazarBeneficiario = 1 Then R.Beneficiario Else B.Beneficiario End) 		
----------	From RptAdmonDispensacion B (NoLock) 
----------	Inner Join CFG_EX_Validacion_Titulos_Beneficiarios R (NoLock) 
----------		On (
----------				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
----------				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma 
----------		   )
----------------------------------------- Reemplazo de Nombre de Beneficiario	




----------------------------------------- Conversion a Cajas Completas 
	Update R Set 
		-- Agrupacion = ((Cantidad) / (ContenidoPaquete_ClaveSSA * 1.0)), 	
		Agrupacion = ceiling((Cantidad) / (ContenidoPaquete_ClaveSSA * 1.0)), 
		AgrupadoMenor = floor(Cantidad / ( ContenidoPaquete_ClaveSSA * 1.0)), 
		AgrupadoMayor = ceiling(Cantidad / ( ContenidoPaquete_ClaveSSA * 1.0)),  
		
		-- Agrupacion_Comercial = ((Cantidad) / (ContenidoPaquete * 1.0)), 
		Agrupacion_Comercial = ceiling((Cantidad) / (ContenidoPaquete * 1.0)), 
		AgrupadoMenor_Comercial = floor(Cantidad / ( ContenidoPaquete * 1.0)), 
		AgrupadoMayor_Comercial = ceiling(Cantidad / ( ContenidoPaquete * 1.0)) 		
	From RptAdmonDispensacion  R  (NoLock)  

	Update R Set 
		PiezasSueltas = Cantidad - (AgrupadoMenor * ContenidoPaquete_ClaveSSA), 
		PiezasSueltas_Comercial = Cantidad - (AgrupadoMenor_Comercial * ContenidoPaquete)  		 
	From RptAdmonDispensacion R (NoLock) 
----------------------------------------- Conversion a Cajas Completas 

	
	If @TipoDispensacion = 1 ----  Consignacion 
	   Begin 
	       Update B Set 
	            IdGrupoPrecios = 1, DescripcionGrupoPrecios = 'PRECIO LICITACION ASIGNADO',  
	            ImporteEAN = 0, PrecioLicitacion = 0, PrecioLicitacionUnitario = 0, 
				SubTotalLicitacion_0 = 0, SubTotalLicitacion = 0, 
				IvaLicitacion = 0, TotalLicitacion = 0  
	       From RptAdmonDispensacion B (NoLock) 
	   End 


------------------------------------------------------ QUITAR LAS DEVOLUCIONES  	
	If @MostrarDevoluciones = 0  
	Begin  
		Delete From RptAdmonDispensacion Where Cantidad <= 0  
	End 
------------------------------------------------------ QUITAR LAS DEVOLUCIONES  



------------------------------------------------------------ SEPARAR EN BASE A CUADRO DE LICITACION	
------	Select Top 1 @sIdEstado = IdEstado From RptAdmonDispensacion 

------	Select Distinct ClaveSSA 
------	Into #tmpClaveCB  
------	From vw_CB_CuadroBasico_Claves 
------	Where IdEstado = @sIdEstado and StatusClave = 'A' 
	
------	Update R Set IdGrupoPrecios  = 1 
------	From RptAdmonDispensacion R (NoLock)  

------	Update R Set IdGrupoPrecios  = 2 
------	From RptAdmonDispensacion R (NoLock)  	
------	Inner Join  #tmpClaveCB C On ( R.ClaveSSA = C.ClaveSSA ) 
------------------------------------------------------------ SEPARAR EN BASE A CUADRO DE LICITACION	



------------------------------- Quitar los precios 
	If @MostrarPrecios = 0 
	Begin 
		Update R Set 
			SubTotal = 0, Descuento = 0, Iva = 0, Total = 0, 
			PrecioUnitario = 0, PrecioLicitacion = 0, PrecioLicitacionUnitario = 0, ImporteEAN = 0, ImporteEAN_Licitado = 0, 
			SubTotalLicitacion_0 = 0, SubTotalLicitacion = 0, IvaLicitacion = 0, TotalLicitacion = 0, 
			Cantidad__Negado = 0, PrecioLicitacion__Negado = 0, ImporteEAN__Negado = 0, SubTotalLicitacion_0__Negado = 0, SubTotalLicitacion__Negado = 0, 
			IvaLicitacion__Negado = 0, TotalLicitacion__Negado = 0 		
		From RptAdmonDispensacion R 
	End 
------------------------------- Quitar los precios 


	------------------------------ Reemplazo por Mascara, TODAS LAS OPERACIONES QUE QUIEREN MANEJAR LOS DATOS SEGUN LICITACION    
	Update B   
		Set B.ClaveSSA = PC.Mascara, B.DescripcionCorta = PC.Descripcion + ( case when ltrim(rtrim(PC.Presentacion)) = '' then '' else '<<' + PC.Presentacion + '>>' end)
	From RptAdmonDispensacion B (NoLock)
	Inner Join CFG_clavessa_Mascara PC (NoLock) 
		On ( B.IdEstado = PC.IdEstado and B.IdCliente = PC.IdCliente and B.IdSubCliente = PC.IdSubCliente and B.IdClaveSSA_Sal = PC.IdClaveSSA and PC.Status = 'A' ) 
	------------------------------ Reemplazo por Mascara, TODAS LAS OPERACIONES QUE QUIEREN MANEJAR LOS DATOS SEGUN LICITACION    
	
	-------------------------------------------------------------------------------------------------------------------------


	------------------------------------ VALIDAR SI EL PROGRAMA-SUBPROGRAMA REQUIERE DISPENSACION EN CAJAS COMPLETAS 
	----------- Requerimiento de Operación Puebla --- Jesús Diaz 20171103.1240
	Update B Set ProgramaSubPrograma_ForzaCajas = P.Dispensacion_CajasCompletas, ProgramaSubPrograma_ForzaCajas_Habilitado = 1  
	From RptAdmonDispensacion B (NoLock) 
	Inner Join vw_Farmacias_Programas_SubPrograma_Clientes P (NoLock) 
		On ( B.IdEstado = P.IdEstado and B.IdFarmacia = P.IdFarmacia and B.IdCliente = P.IdCliente and B.IdSubCliente = P.IdSubCliente
			and B.IdPrograma = P.IdPrograma and B.IdSubPrograma = P.IdSubPrograma ) 
	Where @ProgramaSubPrograma_ForzaCajas_Habilitado = 1 
	------------------------------------ VALIDAR SI EL PROGRAMA-SUBPROGRAMA REQUIERE DISPENSACION EN CAJAS COMPLETAS 


	--Set @iPrints = @iPrints + 1 
	--if @bPrint = 1 Print 'Proceso ' + cast(@iPrints as varchar(10))  


---------------------------------------------------------- PROCESAR DATOS NADRO 
	if exists ( select * from sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_Detallado' and xType = 'U' ) 
	   Drop table RptAdmonDispensacion_Detallado  
	   
	Select * 
	Into RptAdmonDispensacion_Detallado 
	From RptAdmonDispensacion  
	
	------------------------------------------------------------------------------------------------------------------------------ 
	Select 
		IdPerfilAtencion, IdSubPerfilAtencion, PerfilDeAtencion, IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, CLUES_Oficial, NombrePropio_UMedica, 
		IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, CURP, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 

		EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, 
		NumReceta, FechaReceta, StatusVenta, 
		sum(SubTotal) as SubTotal, sum(Descuento) as Descuento, sum(Iva) as Iva, sum(Total) as Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, ContenidoPaquete_ClaveSSA_Licitado, 
		'' as IdProducto, '' as CodigoEAN, ContenidoPaquete, 
		EsConsignacion, EsControlado, 
		0 as Renglon, 
		DescProducto, DescripcionCorta, UnidadDeSalida, 
		max(TasaIva) as TasaIva, 
		
		max(Factor) as Factor, 
		(sum(Cantidad) / max(Factor)) as Cantidad_Piezas, 
		sum(Cantidad) as Cantidad, 
		sum(Agrupacion) as Agrupacion, 		
		sum(AgrupadoMenor) as AgrupadoMenor, sum(AgrupadoMayor) as AgrupadoMayor, sum(PiezasSueltas) as PiezasSueltas, 	
		sum(Agrupacion_Comercial) as Agrupacion_Comercial, 		
		sum(AgrupadoMenor_Comercial) as AgrupadoMenor_Comercial, 
		sum(AgrupadoMayor_Comercial) as AgrupadoMayor_Comercial, sum(PiezasSueltas_Comercial) as PiezasSueltas_Comercial, 				
					
		ProgramaSubPrograma_ForzaCajas, ProgramaSubPrograma_ForzaCajas_Habilitado, 
		PrecioUnitario, PrecioLicitacion, PrecioLicitacionUnitario, 
		sum(ImporteEAN) as ImporteEAN, sum(ImporteEAN_Licitado) as ImporteEAN_Licitado, 
		sum(SubTotalLicitacion_0) as SubTotalLicitacion_0, sum(SubTotalLicitacion) as SubTotalLicitacion, 
		sum(IvaLicitacion) as IvaLicitacion, sum(TotalLicitacion) as TotalLicitacion, 
		sum(Cantidad__Negado) as Cantidad__Negado, 
		PrecioLicitacion as PrecioLicitacion__Negado,      	
		sum(Cantidad__Negado * PrecioLicitacion) as ImporteEAN__Negado, 		
		sum(SubTotalLicitacion_0__Negado) as SubTotalLicitacion_0__Negado, 
		sum(SubTotalLicitacion__Negado) as SubTotalLicitacion__Negado, 
		sum(IvaLicitacion__Negado) as IvaLicitacion__Negado, sum(TotalLicitacion__Negado) as TotalLicitacion__Negado, 		
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area,
		Idlaboratorio, Laboratorio, Idpais, Pais,
		FechaInicial, FechaFinal 
	Into #RptAdmonDispensacion 
	From RptAdmonDispensacion 
	Group by 
		IdPerfilAtencion, IdSubPerfilAtencion, PerfilDeAtencion, IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, CLUES_Oficial, NombrePropio_UMedica, 
		IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, CURP, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
		EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, 
		NumReceta, FechaReceta, StatusVenta, 
		-- SubTotal, Descuento, Iva, Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, ContenidoPaquete_ClaveSSA_Licitado, -- IdProducto, CodigoEAN, 
		ContenidoPaquete, 
		EsConsignacion, EsControlado, 
		DescProducto, DescripcionCorta, UnidadDeSalida, 
		TasaIva, -- Cantidad, 

		ProgramaSubPrograma_ForzaCajas, ProgramaSubPrograma_ForzaCajas_Habilitado, 
		PrecioUnitario, PrecioLicitacion, PrecioLicitacionUnitario, 
		-- Cantidad__Negado, SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area,
		Idlaboratorio, Laboratorio, Idpais, Pais,
		FechaInicial, FechaFinal 	
			

			
	if exists ( select * from sysobjects (NoLock) Where Name = 'RptAdmonDispensacion' and xType = 'U' ) 
	   Drop table RptAdmonDispensacion 

	Select * 
	Into RptAdmonDispensacion 
	From #RptAdmonDispensacion 
	------------------------------------------------------------------------------------------------------------------------------ 
			
	
---------------------------------------------------------- PROCESAR DATOS NADRO 



------------------------------------------------------ AJUSTE POR CLAVES 
--------------------- 	
	----if exists ( select * from sysobjects (NoLock) Where Name = 'RptAdmonDispensacion_Detallado' and xType = 'U' ) 
	----   Drop table RptAdmonDispensacion_Detallado  
	   
	----Select * 
	----Into RptAdmonDispensacion_Detallado 
	----From RptAdmonDispensacion  
--------------------- 	

	Select 
		IdPerfilAtencion, IdSubPerfilAtencion, PerfilDeAtencion, IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, CLUES_Oficial, NombrePropio_UMedica, 
		IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, CURP, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
						
		EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, 
		NumReceta, FechaReceta, StatusVenta, 
		sum(SubTotal) as SubTotal, sum(Descuento) as Descuento, sum(Iva) as Iva, sum(Total) as Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, ContenidoPaquete_ClaveSSA_Licitado, 
		'' as IdProducto, '' as CodigoEAN, ContenidoPaquete, 
		EsConsignacion, EsControlado, 
		0 as Renglon, 
		DescProducto, DescripcionCorta, UnidadDeSalida, 
		max(TasaIva) as TasaIva, 

		max(Factor) as Factor, 
		(sum(Cantidad) / max(Factor)) as Cantidad_Piezas, 
		sum(Cantidad) as Cantidad, 
		sum(Agrupacion) as Agrupacion, 		
		sum(AgrupadoMenor) as AgrupadoMenor, sum(AgrupadoMayor) as AgrupadoMayor, sum(PiezasSueltas) as PiezasSueltas, 	
		sum(Agrupacion_Comercial) as Agrupacion_Comercial, 		
		sum(AgrupadoMenor_Comercial) as AgrupadoMenor_Comercial, 
		sum(AgrupadoMayor_Comercial) as AgrupadoMayor_Comercial, sum(PiezasSueltas_Comercial) as PiezasSueltas_Comercial, 				
					
		ProgramaSubPrograma_ForzaCajas, ProgramaSubPrograma_ForzaCajas_Habilitado, 
		PrecioUnitario, PrecioLicitacion, PrecioLicitacionUnitario, 
		sum(ImporteEAN) as ImporteEAN, sum(ImporteEAN_Licitado) as ImporteEAN_Licitado, 
		sum(SubTotalLicitacion_0) as SubTotalLicitacion_0, sum(SubTotalLicitacion) as SubTotalLicitacion, 
		sum(IvaLicitacion) as IvaLicitacion, sum(TotalLicitacion) as TotalLicitacion, 
		sum(Cantidad__Negado) as Cantidad__Negado, 
		PrecioLicitacion as PrecioLicitacion__Negado,      	
		sum(Cantidad__Negado * PrecioLicitacion) as ImporteEAN__Negado, 		
		sum(SubTotalLicitacion_0__Negado) as SubTotalLicitacion_0__Negado, 
		sum(SubTotalLicitacion__Negado) as SubTotalLicitacion__Negado, 
		sum(IvaLicitacion__Negado) as IvaLicitacion__Negado, sum(TotalLicitacion__Negado) as TotalLicitacion__Negado, 		
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area,
		Idlaboratorio, Laboratorio, Idpais, Pais,
		FechaInicial, FechaFinal 
	Into #RptAdmonDispensacion_Aux 
	From RptAdmonDispensacion 
	Group by 
		IdPerfilAtencion, IdSubPerfilAtencion, PerfilDeAtencion, IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, CLUES_Oficial, NombrePropio_UMedica, 
		IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, CURP, 
		IdEstadoResidencia, EstadoResidencia, ClaveRENAPO_EstadoResidencia, IdTipoDerechoHabiencia, TipoDerechoHabiencia, 
				
		EsRecetaForanea, IdUMedica, CLUES_UMedica, Nombre_UMedica, 
		NumReceta, FechaReceta, StatusVenta, 
		-- SubTotal, Descuento, Iva, Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, ContenidoPaquete_ClaveSSA_Licitado, -- IdProducto, CodigoEAN, 
		ContenidoPaquete, 
		EsConsignacion, EsControlado, 
		DescProducto, DescripcionCorta, UnidadDeSalida, 
		TasaIva, -- Cantidad, 

		ProgramaSubPrograma_ForzaCajas, ProgramaSubPrograma_ForzaCajas_Habilitado, 
		PrecioUnitario, PrecioLicitacion, PrecioLicitacionUnitario, 
		-- Cantidad__Negado, SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area,
		Idlaboratorio, Laboratorio, Idpais, Pais,
		FechaInicial, FechaFinal 	
			
			
	if exists ( select * from sysobjects (NoLock) Where Name = 'RptAdmonDispensacion' and xType = 'U' ) 
	   Drop table RptAdmonDispensacion 

	Select * 
	Into RptAdmonDispensacion 
	From #RptAdmonDispensacion_Aux 
------------------------------------------------------ AJUSTE POR CLAVES 
			
End 
Go--#SQL 


	
	