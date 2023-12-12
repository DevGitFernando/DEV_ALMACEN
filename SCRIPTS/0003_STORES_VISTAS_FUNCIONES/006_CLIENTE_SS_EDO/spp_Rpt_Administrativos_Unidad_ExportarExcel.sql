
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_Unidad_ExportarExcel' and xType = 'P' ) 
Drop Proc spp_Rpt_Administrativos_Unidad_ExportarExcel 
Go--#SQL

Create Proc spp_Rpt_Administrativos_Unidad_ExportarExcel
(
	@IdEstado varchar(2)
)
With Encryption 
As 
Begin 
Set NoCount On
Declare 
	@EncPrincipal varchar(500), 
	@EncSecundario varchar(500) 
	
	If @IdEstado = '09'
		Begin 
			-------------------  DETALLADO DISPENSACION ( VALIDACION )  -------------------------------------------------------------------------
			Select 'Núm. Cliente' = A.IdCliente, 'Cliente' = A.NombreCliente, 'Id Sub-Cliente' = A.IdSubCliente, 'Sub-Cliente' = A.NombreSubCliente, 
			'Id Programa' = A.IdPrograma, A.Programa, 'Id Sub-Programa' = A.IdSubPrograma, 'Sub-Programa' = A.SubPrograma, 
			A.Folio, 'Núm. Receta' = A.NumReceta, Convert(varchar(10), A.FechaReceta, 120) As 'Fecha Receta', 'Id Beneficiario' = A.FolioReferencia, 
			A.Beneficiario, 'Codigo EAN' = A.CodigoEAN, 'Clave Cliente' = IsNull(C.Clave_Cliente, 'PF000'), 'Clave SSA' = A.ClaveSSA, 'Descripción' = A.DescripcionCorta, 
			--'Precio Unitario' = A.PrecioUnitario, A.Cantidad, 'Importe' = A.ImporteEAN		
			'Precio Unitario' = A.PrecioUnitario, A.Cantidad, 'Importe' = A.PrecioUnitario * A.Cantidad		
			From RptAdmonDispensacion_Unidad A (NoLock)		
			Left Join CTE_CFG_CB_CAPREPA C (NoLock) On (A.IdEstado = C.IdEstado And A.ClaveSSA = C.ClaveSSA)
			Group By A.IdCliente, A.NombreCliente, A.IdSubCliente, A.NombreSubCliente, A.IdPrograma, A.Programa, A.IdSubPrograma, A.SubPrograma, 
			A.Folio, A.NumReceta, A.FechaReceta, A.FolioReferencia, A.Beneficiario, A.CodigoEAN, C.Clave_Cliente, A.ClaveSSA, A.DescripcionCorta,
			A.PrecioUnitario, A.Cantidad, A.ImporteEAN 
			Order By A.IdCliente, A.IdSubCliente, A.IdPrograma, A.IdSubPrograma, A.Folio
			
			-------------------  INCIDENCIAS EPIDEMIOLOGICAS  -----------------------------------------------------------------------------------	
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Grupo Clave' = GrupoClaves, 'Descripción' = DescripcionGrupo, 'Id Diagnostico' = IdDiagnostico, 
			Diagnostico, 'Núm. Incidencia' = Incidencias
			From Rpt_Admon_ConcentradoDiagnosticos (nolock)
			Group By IdFarmacia, Farmacia, IdGrupo, GrupoClaves, DescripcionGrupo, IdDiagnostico, Diagnostico, Incidencias
			Order By IdFarmacia, IdGrupo, IdDiagnostico		
			
			-------------------  DETALLADO MEDICOS  ----------------------------------------------------------------------------------------------	
			Select 'Id Farmacia' = A.IdFarmacia, A.Farmacia, 'Id Médico' = A.IdMedico, A.Medico, A.Folio, 'Núm. Receta' = A.NumReceta, 
			Convert(varchar(10), A.FechaReceta, 120) As 'Fecha Receta', 'Id Beneficiario' = A.FolioReferencia, A.Beneficiario, 
			'Clave Cliente' = IsNull(C.Clave_Cliente, 'PF000'), 'Clave SSA' = A.ClaveSSA, 'Descripcion' = A.DescripcionSal,	
			--A.Cantidad, 'Precio' = A.PrecioLicitacion, 'Importe' = A.ImporteEAN 
			A.Cantidad, 'Precio' = A.PrecioLicitacion, 'Importe' = A.PrecioLicitacion * A.Cantidad		
			From Rpt_Admon_MedicosDetallado_Unidad A(nolock)
			Left Join CTE_CFG_CB_CAPREPA C (NoLock) On (A.ClaveSSA = C.ClaveSSA)
			Group By A.IdFarmacia, A.Farmacia, A.IdMedico, A.Medico, A.Folio, A.NumReceta, A.FechaReceta, A.FolioReferencia, A.Beneficiario, 
			C.Clave_Cliente, A.ClaveSSA, A.DescripcionSal, A.Cantidad, A.PrecioLicitacion, A.ImporteEAN
			Order By A.IdFarmacia, A.IdMedico
			
			-------------------  COSTO POR MEDICO  -------------------------------------------------------------------------------------------------	
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Id Médico' = IdMedico, Medico, Count(ClaveSSA) as ClavesSurtidas, Sum(ImporteEAN) as Importe	
			From Rpt_Admon_CostoPorMedico_Unidad (nolock)
			Group By IdFarmacia, Farmacia, IdMedico, Medico
			Order By IdFarmacia, IdMedico
			
			-------------------  DETALLADO PACIENTES  ----------------------------------------------------------------------------------------------
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Id Beneficiario' = FolioReferencia, Beneficiario, Folio,  'Núm. Receta' = NumReceta, 
			Convert(varchar(10), FechaReceta, 120) As 'Fecha Receta', 'Descripción' = DescripcionCorta,	Cantidad, 
			--'Precio' = PrecioLicitacion, 'Importe' = ImporteEAN 
			'Precio' = PrecioLicitacion, 'Importe' = Cantidad * PrecioLicitacion
			From Rpt_Admon_PacientesDet_Unidad (nolock)
			Group By IdFarmacia, Farmacia, FolioReferencia, Beneficiario, Folio, NumReceta, FechaReceta, DescripcionCorta,
			Cantidad, PrecioLicitacion, ImporteEAN
			Order By IdFarmacia, Beneficiario	
			
			-------------------  COSTO POR PACIENTE  ----------------------------------------------------------------------------
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Id Beneficiario' = FolioReferencia, Beneficiario, Folio, 'Núm. Receta' = NumReceta, 
			Convert(varchar(10), FechaReceta, 120) As 'Fecha Receta', Count(ClaveSSA) as ClavesSurtidas, Sum(ImporteEAN) as Importe	
			From Rpt_Admon_CostoPorPaciente_Unidad (nolock)
			Group By IdFarmacia, Farmacia, FolioReferencia, Beneficiario, Folio, NumReceta, FechaReceta
			Order By IdFarmacia, Beneficiario
	
			
		End
	else
		Begin 
			
			-------------------  DETALLADO DISPENSACION ( VALIDACION )  -------------------------------------------------------------------------
			Select 'Núm. Cliente' = IdCliente, 'Cliente' = NombreCliente, 'Id Sub-Cliente' = IdSubCliente, 'Sub-Cliente' =NombreSubCliente, 
			'Id Programa' = IdPrograma, Programa, 'Id Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma, 
			Folio, 'Núm. Receta' =NumReceta, Convert(varchar(10), FechaReceta, 120) As 'Fecha Receta', 'Id Beneficiario' = FolioReferencia, 
			Beneficiario, 'Codigo EAN' = CodigoEAN, 'Clave SSA' = ClaveSSA, 'Descripción' = DescripcionCorta, 'Precio Unitario' = PrecioUnitario, 
			Cantidad, 'Importe' = ImporteEAN		
			From RptAdmonDispensacion_Unidad (NoLock)		
			Group By IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			Folio, NumReceta, FechaReceta, FolioReferencia, Beneficiario, CodigoEAN, ClaveSSA, DescripcionCorta,
			PrecioUnitario, Cantidad, ImporteEAN 
			Order By IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Folio	
			
			-------------------  INCIDENCIAS EPIDEMIOLOGICAS  -----------------------------------------------------------------------------------	
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Grupo Clave' = GrupoClaves, 'Descripción' = DescripcionGrupo, 'Id Diagnostico' = IdDiagnostico, 
			Diagnostico, 'Núm. Incidencia' = Incidencias
			From Rpt_Admon_ConcentradoDiagnosticos (nolock)
			Group By IdFarmacia, Farmacia, IdGrupo, GrupoClaves, DescripcionGrupo, IdDiagnostico, Diagnostico, Incidencias
			Order By IdFarmacia, IdGrupo, IdDiagnostico	
			
			-------------------  DETALLADO MEDICOS  ----------------------------------------------------------------------------------------------	
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Id Médico' = IdMedico, Medico, Folio, 'Núm. Receta' = NumReceta, 
			Convert(varchar(10), FechaReceta, 120) As 'Fecha Receta', 'Id Beneficiario' = FolioReferencia, Beneficiario, 'Clave SSA' = ClaveSSA, 
			'Descripcion' = DescripcionSal,	Cantidad, 'Precio' = PrecioLicitacion, 'Importe' = ImporteEAN 
			From Rpt_Admon_MedicosDetallado_Unidad (nolock)
			Group By IdFarmacia, Farmacia, IdMedico, Medico, Folio, NumReceta, FechaReceta, FolioReferencia, Beneficiario, ClaveSSA, DescripcionSal,
			Cantidad, PrecioLicitacion, ImporteEAN
			Order By IdFarmacia, IdMedico	
			
			-------------------  COSTO POR MEDICO  -------------------------------------------------------------------------------------------------	
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Id Médico' = IdMedico, Medico, Count(ClaveSSA) as ClavesSurtidas, Sum(ImporteEAN) as Importe	
			From Rpt_Admon_CostoPorMedico_Unidad (nolock)
			Group By IdFarmacia, Farmacia, IdMedico, Medico
			Order By IdFarmacia, IdMedico	
			
			-------------------  DETALLADO PACIENTES  ----------------------------------------------------------------------------------------------
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Id Beneficiario' = FolioReferencia, Beneficiario, Folio,  'Núm. Receta' = NumReceta, 
			Convert(varchar(10), FechaReceta, 120) As 'Fecha Receta', 'Descripción' = DescripcionCorta,	Cantidad, 
			'Precio' = PrecioLicitacion, 'Importe' = ImporteEAN 
			From Rpt_Admon_PacientesDet_Unidad (nolock)
			Group By IdFarmacia, Farmacia, FolioReferencia, Beneficiario, Folio, NumReceta, FechaReceta, DescripcionCorta,
			Cantidad, PrecioLicitacion, ImporteEAN
			Order By IdFarmacia, Beneficiario	
			
			-------------------  COSTO POR PACIENTE  ----------------------------------------------------------------------------
			Select 'Id Farmacia' = IdFarmacia, Farmacia, 'Id Beneficiario' = FolioReferencia, Beneficiario, Folio, 'Núm. Receta' = NumReceta, 
			Convert(varchar(10), FechaReceta, 120) As 'Fecha Receta', Count(ClaveSSA) as ClavesSurtidas, Sum(ImporteEAN) as Importe	
			From Rpt_Admon_CostoPorPaciente_Unidad (nolock)
			Group By IdFarmacia, Farmacia, FolioReferencia, Beneficiario, Folio, NumReceta, FechaReceta
			Order By IdFarmacia, Beneficiario
	End
End 
Go--#SQL