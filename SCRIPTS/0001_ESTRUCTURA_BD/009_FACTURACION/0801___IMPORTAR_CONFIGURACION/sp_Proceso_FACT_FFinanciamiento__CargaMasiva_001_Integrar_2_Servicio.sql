
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_2_Servicio' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_2_Servicio 
Go--#SQL 

Create Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_2_Servicio-- () 
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
	From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA T
	Inner Join #Financiamientos F (NoLock) On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento)

	--Actulizar las existentes
	Update T
	Set 
		ClaveSSA = F.ClaveSSA, CantidadPresupuestadaPiezas = F.CantidadPresupuestadaPiezas, CantidadPresupuestada = F.CantidadPresupuestada, CantidadRestante = F.CantidadRestante,
		Costo = F.Costo, Agrupacion = F.Agrupacion, CostoUnitario = F.CostoUnitario, TasaIva = F.TasaIva, Iva = F.Iva,
		ImporteNeto = F.ImporteNeto, Status = F.Status, Actualizado = F.Actualizado, SAT_ClaveProducto_Servicio = F.SAT_ClaveProducto_Servicio,
		SAT_UnidadDeMedida = F.SAT_UnidadDeMedida, Referencia_04 = F.Referencia_04, CostoBase = F.CostoBase, Porcentaje_01 = F.Porcentaje_01, Porcentaje_02 = F.Porcentaje_02
	From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA T
	Inner Join FACT_Fuentes_De_Financiamiento__CargaMasiva F (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And T.ClaveSSA = F.ClaveSSA)

	--Insertar las faltantes
	Insert Into FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA
		(IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, CantidadPresupuestadaPiezas, CantidadPresupuestada, CantidadRestante,
		Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, Status, Actualizado, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida,
		Referencia_04, CostoBase, Porcentaje_01, Porcentaje_02)
	Select
		F.IdFuenteFinanciamiento, F.IdFinanciamiento, F.ClaveSSA, F.CantidadPresupuestadaPiezas, F.CantidadPresupuestada, F.CantidadRestante,
		F.Costo, F.Agrupacion, F.CostoUnitario, F.TasaIva, F.Iva, F.ImporteNeto, F.Status, F.Actualizado, F.SAT_ClaveProducto_Servicio, F.SAT_UnidadDeMedida,
		F.Referencia_04, F.CostoBase, F.Porcentaje_01, F.Porcentaje_02
	From FACT_Fuentes_De_Financiamiento__CargaMasiva F
	Left Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA T
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And T.ClaveSSA = F.ClaveSSA) 
	Where T.IdFuenteFinanciamiento Is Null

End 
Go--#SQL