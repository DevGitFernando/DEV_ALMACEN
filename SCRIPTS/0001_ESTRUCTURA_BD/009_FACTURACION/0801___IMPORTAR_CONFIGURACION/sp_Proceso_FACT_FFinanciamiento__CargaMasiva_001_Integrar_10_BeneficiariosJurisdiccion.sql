
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_10_BeneficiariosJurisdiccion' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_10_BeneficiariosJurisdiccion 
Go--#SQL 

Create Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_10_BeneficiariosJurisdiccion
With Encryption 
As 
Begin 
Set NoCount On 
 Set Ansi_Warnings Off  --- Especial, peligroso 


	Select distinct IdFuenteFinanciamiento
	Into #Financiamientos
	From FACT_Fuentes_De_Financiamiento__CargaMasiva

	--Cancelar Las Anteriores
	Update T Set Status = 'C'
	From FACT_Fuentes_De_Financiamiento_Beneficiarios T
	Inner Join #Financiamientos F (NoLock) On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento)

	--Actulizar las existentes
	Update T
	Set 
		NombreBeneficiario = F.NombreBeneficiario, Status = F.Status, Actualizado = F.Actualizado
	From FACT_Fuentes_De_Financiamiento_Beneficiarios T
	Inner Join FACT_Fuentes_De_Financiamiento__CargaMasiva F (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdBeneficiario = F.IdBeneficiario)

	--Insertar las faltantes
	Insert Into FACT_Fuentes_De_Financiamiento_Beneficiarios
		(IdFuenteFinanciamiento, IdBeneficiario, NombreBeneficiario, Status, Actualizado)
	Select
		F.IdFuenteFinanciamiento, F.IdBeneficiario, F.NombreBeneficiario, F.Status, F.Actualizado
	From FACT_Fuentes_De_Financiamiento__CargaMasiva F
	Left Join FACT_Fuentes_De_Financiamiento_Beneficiarios T (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdBeneficiario = F.IdBeneficiario)
	Where T.IdFuenteFinanciamiento Is Null


End 
Go--#SQL