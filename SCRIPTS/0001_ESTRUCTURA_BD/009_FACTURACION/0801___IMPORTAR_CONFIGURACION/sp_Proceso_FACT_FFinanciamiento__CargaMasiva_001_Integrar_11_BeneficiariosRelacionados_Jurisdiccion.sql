
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_11_BeneficiariosRelacionados_Jurisdiccion' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_11_BeneficiariosRelacionados_Jurisdiccion 
Go--#SQL 

Create Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_11_BeneficiariosRelacionados_Jurisdiccion
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
	From FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados T
	Inner Join #Financiamientos F (NoLock) On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento)

	--Actulizar las existentes
	Update T
	Set Status = F.Status, Actualizado = F.Actualizado
	From FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados T
	Inner Join FACT_Fuentes_De_Financiamiento__CargaMasiva F (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento	And T.IdBeneficiario = F.IdBeneficiario
			And T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia 
			And T.IdCliente  = F.IdCliente And T.IdSubCliente = F.IdSubCliente
			And T.IdBeneficiario_Relacionado = F.IdBeneficiario_Relacionado)

	--Insertar las faltantes
	Insert Into FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados
		(IdFuenteFinanciamiento, IdBeneficiario, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario_Relacionado, Status, Actualizado)
	Select
		F.IdFuenteFinanciamiento, F.IdBeneficiario, F.IdEstado, F.IdFarmacia, F.IdCliente, F.IdSubCliente, F.IdBeneficiario_Relacionado, F.Status, F.Actualizado
	From FACT_Fuentes_De_Financiamiento__CargaMasiva F
	Left Join FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados T (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento	And T.IdBeneficiario = F.IdBeneficiario
			And T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia 
			And T.IdCliente  = F.IdCliente And T.IdSubCliente = F.IdSubCliente
			And T.IdBeneficiario_Relacionado = F.IdBeneficiario_Relacionado)
	Where T.IdFuenteFinanciamiento Is Null

End 
Go--#SQL