-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_007' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_007 
Go--#SQL 

--		Exec spp_Rpt_Administrativos	
 
Create Proc spp_Rpt_Administrativos_007 
With Encryption 
As 
Begin 
Set NoCount Off 
Set DateFormat YMD 

--------------------------------------------------------- TABLA BASE DEL REPORTE DE VALIDACION 
------------------------------- Reemplazo de Nombre de Beneficiario y Folio Referencia 	
	Update B Set 
		FolioReferencia = (case when R.ReemplazarFolioReferencia = 1 Then R.FolioReferencia Else B.FolioReferencia End),  
		Beneficiario = (case when R.ReemplazarBeneficiario = 1 Then R.Beneficiario Else B.Beneficiario End) 		
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join CFG_EX_Validacion_Titulos_Beneficiarios R (NoLock) 
		On (
				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma and R.Status = 'A' 
		   )
------------------------------- Reemplazo de Nombre de Beneficiario y Folio Referencia 	


------------------------------- Reemplazo de Titulos Encabezado 
	Update B Set IdCliente = '0001', 
		NombreCliente = R.Cliente, NombreSubCliente = R.SubCliente, Programa = R.Programa, SubPrograma = R.SubPrograma
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join CFG_EX_Validacion_Titulos R (NoLock) 
		On (
				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma  and R.Status = 'A' 
		   )
------------------------------- Reemplazo de Titulos Encabezado  

	
	
	
--------------------------------------------------------- TABLA BASE DEL REPORTE DE VALIDACION - VALES 
------------------------------- Reemplazo de Nombre de Beneficiario y Folio Referencia 	
	Update B Set 
		FolioReferencia = (case when R.ReemplazarFolioReferencia = 1 Then R.FolioReferencia Else B.FolioReferencia End),  
		Beneficiario = (case when R.ReemplazarBeneficiario = 1 Then R.Beneficiario Else B.Beneficiario End) 		
	From RptAdmonDispensacion_Documentos B (NoLock) 
	Inner Join CFG_EX_Validacion_Titulos_Beneficiarios R (NoLock) 
		On (
				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma and R.Status = 'A' 
		   )
------------------------------- Reemplazo de Nombre de Beneficiario y Folio Referencia 	


------------------------------- Reemplazo de Titulos Encabezado  
	Update B Set IdCliente = '0001', 
		NombreCliente = R.Cliente, NombreSubCliente = R.SubCliente, Programa = R.Programa, SubPrograma = R.SubPrograma
	From RptAdmonDispensacion_Documentos B (NoLock) 
	Inner Join CFG_EX_Validacion_Titulos R (NoLock) 
		On (
				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma and R.Status = 'A' 
		   )
------------------------------- Reemplazo de Titulos Encabezado  


End 
Go--#SQL 

	
	