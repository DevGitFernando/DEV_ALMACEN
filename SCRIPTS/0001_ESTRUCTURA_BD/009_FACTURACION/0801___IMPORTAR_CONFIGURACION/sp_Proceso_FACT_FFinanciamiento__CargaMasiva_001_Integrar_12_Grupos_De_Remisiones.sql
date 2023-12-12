
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_12_Grupos_De_Remisiones' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_12_Grupos_De_Remisiones 
Go--#SQL 

Create Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_12_Grupos_De_Remisiones-- () 
With Encryption 
As 
Begin 
Set NoCount On 
 Set Ansi_Warnings Off  --- Especial, peligroso 


	Select distinct IdFuenteFinanciamiento, IdFinanciamiento
	Into #Financiamientos
	From FACT_Fuentes_De_Financiamiento__CargaMasiva


	--Cancelar Las Anteriores
	Update T Set Status = 'C'
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones T
	Inner Join #Financiamientos F (NoLock) On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento)

	--Actulizar las existentes
	Update T
	Set ClaveSSA = F.ClaveSSA, TipoRemision = F.TipoRemision, FechaVigencia = F.FechaVigencia, Status = F.Status
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones T
	Inner Join FACT_Fuentes_De_Financiamiento__CargaMasiva F (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And
			T.Referencia_01 = F.Referencia_01 And T.IdGrupo = F.IdGrupo)

	--Insertar las faltantes
	Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones
		(IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo, ClaveSSA, TipoRemision, FechaVigencia, Status)
	Select
		F.IdFuenteFinanciamiento, F.IdFinanciamiento, F.Referencia_01, F.IdGrupo, F.ClaveSSA, F.TipoRemision, F.FechaVigencia, F.Status
	From FACT_Fuentes_De_Financiamiento__CargaMasiva F
	Left Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones T
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And 
			T.Referencia_01 = F.Referencia_01 And T.IdGrupo = F.IdGrupo) 
	Where T.IdFuenteFinanciamiento Is Null
	

End 
Go--#SQL