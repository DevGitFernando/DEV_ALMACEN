------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_9_ExcepcionPrecios_Servicio' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_9_ExcepcionPrecios_Servicio 
Go--#SQL 

Create Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_9_ExcepcionPrecios_Servicio
With Encryption 
As 
Begin 
Set NoCount On 
 Set Ansi_Warnings Off  --- Especial, peligroso 


	Select distinct IdFuenteFinanciamiento, IdFinanciamiento, Tipo
	Into #Financiamientos
	From FACT_Fuentes_De_Financiamiento__CargaMasiva

	--Cancelar Las Anteriores
	Update T Set Status = 'C'
	From FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones T
	Inner Join #Financiamientos F (NoLock) On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And T.Tipo = F.Tipo)

	--Actulizar las existentes
	Update T
	Set 
		Referencia_02 = F.Referencia_02, Referencia_03 = F.Referencia_03, Referencia_04 = F.Referencia_04,
		Tipo = F.Tipo, PrecioBase = F.PrecioBase, 
		Incremento = F.Incremento, Status = F.Status, PorcentajeIncremento = F.PorcentajeIncremento, PrecioFinal = F.PrecioFinal
	From FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones T
	Inner Join FACT_Fuentes_De_Financiamiento__CargaMasiva F (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento  and T.Referencia_01 = F.Referencia_01  
			And T.ClaveSSA = F.ClaveSSA And T.Tipo = F.Tipo)

	--Insertar las faltantes
	Insert Into FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones
		(IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, Referencia_02, Referencia_03, Referencia_04, 
			 ClaveSSA, Tipo, PrecioBase, Incremento, PorcentajeIncremento, PrecioFinal, Status)
	Select
		F.IdFuenteFinanciamiento, F.IdFinanciamiento, F.Referencia_01, F.Referencia_02, F.Referencia_03, F.Referencia_04, 
		F.ClaveSSA, F.Tipo, F.PrecioBase,F.Incremento, F.PorcentajeIncremento, F.PrecioFinal, F.Status
	From FACT_Fuentes_De_Financiamiento__CargaMasiva F
	Left Join FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones T (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento and T.Referencia_01 = F.Referencia_01  
			And T.ClaveSSA = F.ClaveSSA And T.Tipo = F.Tipo )
	Where T.IdFuenteFinanciamiento Is Null

End 
Go--#SQL