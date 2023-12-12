If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_Secretaria_Parte_002' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_Secretaria_Parte_002 
Go--#SQL

Create Proc spp_Rpt_Administrativos_Secretaria_Parte_002 
(   
	@TipoDispensacion tinyint = 0, @FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-05-31',
	@TipoInsumoMedicamento tinyint = 0  
)  
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4) 	


----------------------------------  
--	Update B Set Empresa = I.Nombre 
--	From RptAdmonDispensacion_Secretaria B (NoLock) 
--	Inner Join CatEmpresas I (NoLock) On ( B.IdEmpresa = I.IdEmpresa ) 

-------------------------------- Temporales 
	Select Top 1 @IdEstado = IdEstado, @IdFarmacia = IdFarmacia From  RptAdmonDispensacion_Secretaria (NoLock) 

	Select * 
	into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 

	Select * 
	into #vw_Relacion_ClavesSSA_Claves 
	From vw_Relacion_ClavesSSA_Claves 

	Select * 
	Into #vw_Clientes_SubClientes 
	From vw_Clientes_SubClientes 

	Select * 
	Into #vw_Programas_SubProgramas 
	From vw_Programas_SubProgramas 

	Select * 
	into #vw_Medicos 
	From vw_Medicos 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

	Select * 
	into #vw_Beneficiarios
	From vw_Beneficiarios
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	Select * 
	Into #vw_CIE10_Diagnosticos
	From vw_CIE10_Diagnosticos 
-------------------------------- Temporales 


	Update B Set IdClaveSSA_Sal = I.IdClaveSSA_Sal, ClaveSSA = I.ClaveSSA, DescripcionSal = I.DescripcionSal, 
		 DescProducto = I.Descripcion, DescripcionCorta = I.DescripcionCorta, 
		 IdPresentacion_ClaveSSA = I.IdPresentacion_ClaveSSA, Presentacion_ClaveSSA = I.Presentacion_ClaveSSA
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join #vw_Productos_CodigoEAN I (NoLock) On ( B.IdProducto = I.IdProducto and B.CodigoEAN = I.CodigoEAN ) 


----------------------------------  Marcar solo para Precios de Lista 
----	Update B Set UsaPrecioLicitacion = 0 
----	From RptAdmonDispensacion_Secretaria B (NoLock) 
	


------- Reemplazo de Claves 
	Update B Set IdClaveSSA_Sal = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionSal = C.Descripcion -- , IdGrupoPrecios = 3 
--	select count(*) 	 
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join #vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA_Sal = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
------- Reemplazo de Claves 


------- Identificar todas la claves que son tipo Seguro Popular 	
	Update B Set EsSeguroPopular = I.EsSeguroPopular, TituloSeguroPopular = 'SEGURO POPULAR'  
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join CatClavesSSA_SeguroPopular I (NoLock) On ( B.IdClaveSSA_Sal = I.IdClaveSSA )

	if @TipoInsumoMedicamento <> 0 
	   Begin
	      if @TipoInsumoMedicamento = 1 
	         Delete from RptAdmonDispensacion_Secretaria Where EsSeguroPopular = 2 
	         
	      if @TipoInsumoMedicamento = 2 
	         Delete from RptAdmonDispensacion_Secretaria Where EsSeguroPopular = 1 	         
	   End  

--- Identificar el Tipo de Insumo 
	Update B Set TipoDeInsumo = 'MEDICAMENTO'	
	From RptAdmonDispensacion_Secretaria B (NoLock) 	
	Where TipoInsumo = 1 

	Update B Set TipoDeInsumo = 'MATERIAL DE CURACIÓN Y OTROS'	
	From RptAdmonDispensacion_Secretaria B (NoLock) 	
	Where TipoInsumo = 2 

----------------
	Update B Set NombreCliente = I.NombreCliente, NombreSubCliente = I.NombreSubCliente 
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join #vw_Clientes_SubClientes I (NoLock) On ( B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente ) 

	Update B Set Programa = I.Programa, SubPrograma = I.SubPrograma 
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join #vw_Programas_SubProgramas I (NoLock) On ( B.IdPrograma = I.IdPrograma and B.IdSubPrograma = I.IdSubPrograma ) 


-------------------------------- Reportes Especiales 	
--- Agregar informacion Adicional 
	Update B Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = V.FechaReceta, 
		IdMedico = V.IdMedico, -- Medico = I.NombreCompleto, 
		IdServicio = V.IdServicio, IdArea = V.IdArea, 
		-- IdDiagnostico = right('00000000' + V.IdDiagnostico, 6)      
		IdDiagnostico = V.IdDiagnostico 
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join VentasInformacionAdicional V (NoLock) 
		on ( -- B.IdEmpresa = V.IdEmpresa and 
			B.IdEstado = V.IdEstado and B.IdFarmacia = V.IdFarmacia and B.Folio = V.FolioVenta )
	--Inner Join vw_Medicos I (NoLock) On ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia and V.IdMedico = I.IdMedico ) 

--- Datos del Medico 
	Update B Set Medico = I.NombreCompleto  
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join #vw_Medicos I (NoLock) On ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia and B.IdMedico = I.IdMedico ) 
	

--- Datos de Beneficiario 
	Update B Set Beneficiario = I.NombreCompleto, FolioReferencia = I.FolioReferencia  
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join #vw_Beneficiarios I (NoLock) 
		on ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia 
			 and B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente and B.IdBeneficiario = I.IdBeneficiario )
	
	Update B Set Servicio = I.Servicio, Area = I.Area_Servicio  
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join vw_Servicios_Areas I (NoLock) On ( B.IdServicio = I.IdServicio and B.IdArea = I.IdArea ) 

	
	Update B Set IdGrupo = I.IdGrupo, GrupoClaves = I.GrupoClaves, DescripcionGrupo = I.DescripcionGrupo, 
		  SubGrupo = I.SubGrupo, ClaveSubGrupo = I.ClaveSubGrupo, DescripcionSubGrupo = I.DescripcionSubGrupo, 
		  ClaveDiagnostico = I.ClaveDiagnostico, Diagnostico = I.Diagnostico  	
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join #vw_CIE10_Diagnosticos I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico )  
	

	Update B Set IdDiagnostico = '0001' 
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	-- Inner Join vw_CIE10_Diagnosticos I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico ) 	
	Where B.Diagnostico = '' 

	Update B Set IdGrupo = I.IdGrupo, GrupoClaves = I.GrupoClaves, DescripcionGrupo = I.DescripcionGrupo, 
		  SubGrupo = I.SubGrupo, ClaveSubGrupo = I.ClaveSubGrupo, DescripcionSubGrupo = I.DescripcionSubGrupo, 
		  ClaveDiagnostico = I.ClaveDiagnostico, Diagnostico = I.Diagnostico  	
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Inner Join #vw_CIE10_Diagnosticos I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico )  
	Where B.Diagnostico = '' 	
-------------------------------- Reportes Especiales 	


----
--- Asignar Precio 
	Update B Set PrecioLicitacion = IsNull(PC.Precio, 0) -- , IdGrupoPrecios = 1, DescripcionGrupoPrecios = '' 
	     , IdGrupoPrecios = (case when IsNull(PC.Precio, 0) <= 0 Then 1 Else 2 End), 
		 DescripcionGrupoPrecios = (case when IsNull(PC.Precio, 0)  <= 0 Then 'SIN PRECIO DE LICITACION' Else 'PRECIO LICITACION ASIGNADO' End) 
		 , DescProducto = (case when IsNull(PC.Precio, 0) <= 0 Then '.' + DescProducto Else DescProducto End)
		 , DescripcionCorta = (case when IsNull(PC.Precio, 0) <= 0 Then '.' + DescripcionCorta Else DescripcionCorta End)		 

	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Left Join vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
		On ( B.IdEstado = PC.IdEstado and B.IdCliente = PC.IdCliente and B.IdSubCliente = PC.IdSubCliente 
		     and B.IdClaveSSA_Sal = PC.IdClaveSSA ) 	     

-- between convert(varchar(10), @FechaInicial, 120) and convert(varchar(10), @FechaFinal, 120)

------  Marcar los Precios 		
	Update B Set -- FechaInicial = cast(@FechaInicial as datetime),  FechaFinal = cast(@FechaFinal as datetime), 
		 ImporteEAN = (Cantidad * PrecioLicitacion), 
	     IdGrupoPrecios	= (case when PrecioLicitacion <= 0 Then 1 Else 2 End), 
		 DescripcionGrupoPrecios = (case when PrecioLicitacion <= 0 Then 'SIN PRECIO DE LICITACION' Else 'PRECIO LICITACION ASIGNADO' End)
	From RptAdmonDispensacion_Secretaria B (NoLock) 		
	-- Where IdGrupoPrecios <> 1 


	Update B Set FechaInicial = cast(@FechaInicial as datetime),  FechaFinal = cast(@FechaFinal as datetime), 
		 ImporteEAN = (Cantidad * PrecioLicitacion), 
	     -- IdGrupoPrecios	= 0, 
		 DescripcionGrupoPrecios = 'REASIGNACION DE PRECIOS' 
	From RptAdmonDispensacion_Secretaria B (NoLock) 
	Where IdGrupoPrecios = 3 
	
	
	Update B Set FechaInicial = cast(@FechaInicial as datetime),  FechaFinal = cast(@FechaFinal as datetime), 
		 ImporteEAN = (Cantidad * PrecioLicitacion), 
	     IdGrupoPrecios	= (case when PrecioLicitacion <= 0 Then 1 Else 2 End), 
		 DescripcionGrupoPrecios = (case when PrecioLicitacion <= 0 Then 'SIN PRECIO DE LICITACION' Else 'PRECIO LICITACION ASIGNADO' End)
	From RptAdmonDispensacion_Secretaria B (NoLock) 		
	Where IdGrupoPrecios <> 3 

	
	if @TipoDispensacion = 1 
	   Update B Set ImporteEAN = 0, PrecioLicitacion = 0 
	   From RptAdmonDispensacion_Secretaria B (NoLock) 
	   
End 
Go--#SQL
