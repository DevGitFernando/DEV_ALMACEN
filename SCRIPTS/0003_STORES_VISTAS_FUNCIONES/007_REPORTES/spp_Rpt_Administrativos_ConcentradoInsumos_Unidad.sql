If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_ConcentradoInsumos_Unidad' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_ConcentradoInsumos_Unidad 
Go--#SQL

Create Proc spp_Rpt_Administrativos_ConcentradoInsumos_Unidad 
With Encryption 
As 
Begin 
Set NoCount On
Declare 
	@EncPrincipal varchar(500), 
	@EncSecundario varchar(500) 
	
	Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario 
	from dbo.fg_Unidad_EncabezadoReportesClientesSSA() 

--- Borrar las tablas Intermedias de Datos 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_Unidad 

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_Desglozado_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_Desglozado_Unidad
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_Programa_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_Programa_Unidad 	   
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_ProgramaTotalizado_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_ProgramaTotalizado_Unidad	  
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_SinPrecio_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_SinPrecio_Unidad 	    
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_SinPrecioDetallado_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_SinPrecioDetallado_Unidad

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_Validacion_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_Validacion_Unidad 
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoDiagnosticos' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoDiagnosticos

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_MedicosDetallado_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_MedicosDetallado_Unidad

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_CostoPorMedico_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_CostoPorMedico_Unidad

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_PacientesDet_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_PacientesDet_Unidad

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_CostoPorPaciente_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_CostoPorPaciente_Unidad

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ServiciosAreas_Unidad' and xType = 'U' )
	   Drop Table Rpt_Admon_ServiciosAreas_Unidad

  
--- Borrar las tablas Intermedias de Datos 

------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------- 
--- Detallado de Dispensacion General 
	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
		IdFarmacia, Farmacia, ClaveSSA, IdClaveSSA_Sal, DescripcionSal, Sum( Cantidad ) As Cantidad, 
		PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Into Rpt_Admon_ConcentradoInsumos_Unidad
	From RptAdmonDispensacion_Unidad (Nolock )
	Group By IdFarmacia, Farmacia, ClaveSSA, IdClaveSSA_Sal, DescripcionSal, 
		PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Order By DescripcionSal

--- Concentrado Programa Desglozado 
	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
		IdFarmacia, Farmacia, EsSeguroPopular, TituloSeguroPopular, TipoInsumo, TipoDeInsumo, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		Sum(Cantidad) As Cantidad, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Into Rpt_Admon_ConcentradoInsumos_Desglozado_Unidad
	From RptAdmonDispensacion_Unidad (Nolock )
	Group By IdFarmacia, Farmacia, EsSeguroPopular, TituloSeguroPopular, TipoInsumo, TipoDeInsumo, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Order By EsSeguroPopular, TipoInsumo, IdClaveSSA_Sal

--- Concentrado por Programa 
	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
		IdFarmacia, Farmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, TipoInsumo, TipoDeInsumo,
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum( Cantidad ) As Cantidad, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Into Rpt_Admon_ConcentradoInsumos_Programa_Unidad
	From RptAdmonDispensacion_Unidad (Nolock )
	Group By IdFarmacia, Farmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, TipoInsumo, TipoDeInsumo,
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Order By IdFarmacia, IdCliente, IdSubCliente, IdPrograma, TipoInsumo, IdClaveSSA_Sal

--- Concentrado por Programa Totalizado 
	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
		IdFarmacia, Farmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, TipoInsumo, TipoDeInsumo,
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum( Cantidad ) As Cantidad, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Into Rpt_Admon_ConcentradoInsumos_ProgramaTotalizado_Unidad
	From RptAdmonDispensacion_Unidad (Nolock )
	Group By IdFarmacia, Farmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, TipoInsumo, TipoDeInsumo,
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Order By IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, TipoInsumo, IdClaveSSA_Sal

--- Concentrado de Productos sin Precio 
	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
		IdFarmacia, Farmacia, IdGrupoPrecios, DescripcionGrupoPrecios, TipoInsumo, TipoDeInsumo, IdClaveSSA_Sal, ClaveSSA, DescripcionSal,
		IdProducto, CodigoEAN, DescripcionCorta, Sum( Cantidad ) As Cantidad, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Into Rpt_Admon_ConcentradoInsumos_SinPrecio_Unidad 
	From RptAdmonDispensacion_Unidad (Nolock )
	Where IdGrupoPrecios = 1
	Group By IdFarmacia, Farmacia, IdGrupoPrecios, DescripcionGrupoPrecios, TipoInsumo, TipoDeInsumo, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		IdProducto, CodigoEAN, DescripcionCorta, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado	
	Order By IdFarmacia, IdGrupoPrecios, TipoInsumo, IdClaveSSA_Sal


--- Detallado de Productos sin Precio 
	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
		IdFarmacia, Farmacia, IdGrupoPrecios, DescripcionGrupoPrecios, TipoInsumo, TipoDeInsumo, IdClaveSSA_Sal, IdProducto, ClaveSSA, 
		DescripcionSal, CodigoEAN, DescripcionCorta, Sum( Cantidad ) As Cantidad, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado
	Into Rpt_Admon_ConcentradoInsumos_SinPrecioDetallado_Unidad
	From RptAdmonDispensacion_Unidad (Nolock )
	Where IdGrupoPrecios = 1
	Group By IdFarmacia, Farmacia, IdGrupoPrecios, DescripcionGrupoPrecios, TipoInsumo, TipoDeInsumo, IdClaveSSA_Sal, IdProducto, ClaveSSA, DescripcionSal, 
		CodigoEAN, DescripcionCorta, PrecioLicitacion, FechaInicial, FechaFinal, IdEstado, Estado	
	Order By IdFarmacia, IdGrupoPrecios, TipoInsumo, IdClaveSSA_Sal, IdProducto

--- Detallado de Validacion 
	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
		IdFarmacia, Farmacia, IdGrupoPrecios, DescripcionGrupoPrecios, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa,
		IdSubPrograma, SubPrograma, Folio, NumReceta, FechaReceta, FolioReferencia, Beneficiario, ClaveSSA, DescripcionCorta, Cantidad,
		PrecioLicitacion, ImporteEAN, FechaInicial, FechaFinal, IdEstado, Estado
	Into Rpt_Admon_Validacion_Unidad 
	From RptAdmonDispensacion_Unidad (Nolock)
	Group By IdFarmacia, Farmacia, IdGrupoPrecios, DescripcionGrupoPrecios, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa,
		IdSubPrograma, SubPrograma, Folio, NumReceta, FechaReceta, FolioReferencia, Beneficiario, ClaveSSA, DescripcionCorta, Cantidad,
		PrecioLicitacion, ImporteEAN, FechaInicial, FechaFinal, IdEstado, Estado
	Order By IdFarmacia, IdGrupoPrecios, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma
------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------- 

--- Concentrado de Diagnosticos 
	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
		IdFarmacia, Farmacia, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, Diagnostico, FechaInicial, FechaFinal, count(*) as Incidencias  
	Into Rpt_Admon_ConcentradoDiagnosticos 	
	From RptAdmonDispensacion_Unidad (NoLock) 
	Group By IdFarmacia, Farmacia, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, Diagnostico, FechaInicial, FechaFinal


------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------

--------- Concentrado Detallado De Medicos  ---------------------------------------------------------------------------------

	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
	IdFarmacia, Farmacia, IdMedico, Medico, 
	Folio, NumReceta, FechaReceta, FolioReferencia, Beneficiario, ClaveSSA, DescripcionSal,	
	Cantidad, PrecioLicitacion, ImporteEAN, FechaInicial, FechaFinal
	Into Rpt_Admon_MedicosDetallado_Unidad
	From RptAdmonDispensacion_Unidad (Nolock)
	Group By IdFarmacia, Farmacia, IdMedico, Medico, Folio, NumReceta, FechaReceta, 
	FolioReferencia, Beneficiario, ClaveSSA, DescripcionSal, Cantidad, PrecioLicitacion, ImporteEAN, 
	FechaInicial, FechaFinal 

---------- Costo Por Medico  -------------------------------------------------------------------------------------------------- 

	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
	IdFarmacia, Farmacia, IdMedico, Medico,  
	ClaveSSA , Sum( ImporteEAN ) As ImporteEAN, FechaInicial, FechaFinal
	Into Rpt_Admon_CostoPorMedico_Unidad
	From RptAdmonDispensacion_Unidad (Nolock)
	Group By IdFarmacia, Farmacia, IdMedico, Medico, ClaveSSA, FechaInicial, FechaFinal
	Order By IdFarmacia, IdMedico

-------------- Reporte Detallado Por Paciente ----------------------------------------------------------------------------------

	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
	IdFarmacia, Farmacia, FolioReferencia, Beneficiario,
	Folio, NumReceta, FechaReceta, DescripcionCorta,	
	Cantidad, PrecioLicitacion, ImporteEAN, FechaInicial, FechaFinal
	Into Rpt_Admon_PacientesDet_Unidad
	From RptAdmonDispensacion_Unidad (Nolock)
	Group By IdFarmacia, Farmacia, FolioReferencia, Beneficiario,
	Folio, NumReceta, FechaReceta, DescripcionCorta,	
	Cantidad, PrecioLicitacion, ImporteEAN, FechaInicial, FechaFinal
	Order By IdFarmacia, Beneficiario
	
-------------- Reporte de Costo Por Paciente -------------------------------------------------------------------------------------

	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
	IdFarmacia, Farmacia, FolioReferencia, Beneficiario,
	Folio, NumReceta, FechaReceta, ClaveSSA, 
	Sum( ImporteEAN ) As ImporteEAN, FechaInicial, FechaFinal
	Into Rpt_Admon_CostoPorPaciente_Unidad
	From RptAdmonDispensacion_Unidad (Nolock)
	Group By IdFarmacia, Farmacia, FolioReferencia, Beneficiario,
	Folio, NumReceta, FechaReceta, ClaveSSA, FechaInicial, FechaFinal
	Order By IdFarmacia, Beneficiario


------------ Reporte de Servicios y Areas  ---------------------------------------------------------

	Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
	IdFarmacia, Farmacia, IdServicio, Servicio, IdArea, Area, 
	ClaveSSA, DescripcionSal,	
	Sum ( Cantidad ) As Cantidad,  Max ( PrecioLicitacion ) As PrecioLicitacion, 
	Sum ( ImporteEAN ) As ImporteEAN, FechaInicial, FechaFinal
	Into Rpt_Admon_ServiciosAreas_Unidad
	From RptAdmonDispensacion_Unidad (Nolock)
	Group By IdFarmacia, Farmacia, IdServicio, Servicio, IdArea, Area, 
	ClaveSSA, DescripcionSal, PrecioLicitacion, FechaInicial, FechaFinal
	Order By IdFarmacia, IdServicio, IdArea, DescripcionSal	

End 
Go--#SQL