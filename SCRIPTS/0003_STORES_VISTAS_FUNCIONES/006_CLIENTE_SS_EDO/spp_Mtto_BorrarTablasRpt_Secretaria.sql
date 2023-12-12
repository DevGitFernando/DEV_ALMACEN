If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Mtto_BorrarTablasRpt_Secretaria' and xType = 'P' ) 
   Drop Proc spp_Mtto_BorrarTablasRpt_Secretaria 
Go--#SQL 

Create Proc spp_Mtto_BorrarTablasRpt_Secretaria 
With Encryption 
As 
Begin 
Set NoCount On

	

	--- Borrar la tabla base de los Datos

	If exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_Secretaria' and xType = 'U' ) 
	Drop table RptAdmonDispensacion_Secretaria


	--- Borrar las tablas Intermedias de Datos 

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_Secretaria 

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_Desglozado_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_Desglozado_Secretaria 
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_Programa_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_Programa_Secretaria 	   
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_ProgramaTotalizado_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_ProgramaTotalizado_Secretaria	  
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_SinPrecio_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_SinPrecio_Secretaria 	    
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoInsumos_SinPrecioDetallado_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoInsumos_SinPrecioDetallado_Secretaria

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_Validacion_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_Validacion_Secretaria 
	   
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ConcentradoDiagnosticos' and xType = 'U' )
	   Drop Table Rpt_Admon_ConcentradoDiagnosticos

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_MedicosDetallado_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_MedicosDetallado_Secretaria

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_CostoPorMedico_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_CostoPorMedico_Secretaria

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_PacientesDet_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_PacientesDet_Secretaria

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_CostoPorPaciente_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_CostoPorPaciente_Secretaria

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Admon_ServiciosAreas_Secretaria' and xType = 'U' )
	   Drop Table Rpt_Admon_ServiciosAreas_Secretaria
 
 

------------------- 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida' and xType = 'U' )
	   Drop Table Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteReg_Impresion_Seguimiento_Transferencias_Entrada' and xType = 'U' )
	   Drop Table Rpt_CteReg_Impresion_Seguimiento_Transferencias_Entrada

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteUnidad_Impresion_Seguimiento_Transferencias_Entrada' and xType = 'U' )
	   Drop Table Rpt_CteUnidad_Impresion_Seguimiento_Transferencias_Entrada

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteUnidad_Impresion_Seguimiento_Transferencias_Salida' and xType = 'U' )
	   Drop Table Rpt_CteUnidad_Impresion_Seguimiento_Transferencias_Salida
 

End 
Go--#SQL
