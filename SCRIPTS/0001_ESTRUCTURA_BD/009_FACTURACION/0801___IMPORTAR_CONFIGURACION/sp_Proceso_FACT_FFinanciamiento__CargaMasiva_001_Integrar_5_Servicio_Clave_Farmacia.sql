
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_5_Servicio_Clave_Farmacia' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_5_Servicio_Clave_Farmacia 
Go--#SQL 

Create Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_5_Servicio_Clave_Farmacia
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
	From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia T
	Inner Join #Financiamientos F (NoLock) On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento)
	
	--Carga en la tabla FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA
	Insert Into FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA
		(IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, CantidadPresupuestadaPiezas, CantidadPresupuestada, 
		 CantidadRestante, Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, Status, Actualizado,
		 SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, Referencia_04, CostoBase, Porcentaje_01, Porcentaje_02)
	Select
		 Distinct IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, CantidadPresupuestadaPiezas, CantidadPresupuestada, 
		 CantidadRestante, Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, Status, Actualizado,
		 SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, Referencia_04, CostoBase, Porcentaje_01, Porcentaje_02
	From FACT_Fuentes_De_Financiamiento__CargaMasiva F (NoLock)
	Where Not Exists
	(Select * From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA C (NoLock)
	Where F.IdFuenteFinanciamiento = C.IdFuenteFinanciamiento And F.IdFinanciamiento = C.IdFinanciamiento And F.ClaveSSA = C.ClaveSSA)

	--Actulizar las existentes
	Update T
	Set 
		CantidadPresupuestadaPiezas = F.CantidadPresupuestadaPiezas, 
		CantidadPresupuestada = F.CantidadPresupuestada, CantidadRestante = F.CantidadRestante, Status = F.Status, Actualizado = F.Actualizado,
		Referencia_01 = F.Referencia_01, Referencia_02 = F.Referencia_02, Referencia_03 = F.Referencia_03, Referencia_04 = F.Referencia_04, Referencia_05 = F.Referencia_05
	From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia T
	Inner Join FACT_Fuentes_De_Financiamiento__CargaMasiva F (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia And T.ClaveSSA = F.ClaveSSA)

	--Insertar las faltantes
	Insert Into FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia
		(IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, CantidadPresupuestadaPiezas,
			CantidadPresupuestada, CantidadRestante, Status, Actualizado, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05)
	Select
		F.IdFuenteFinanciamiento, F.IdFinanciamiento, F.IdEstado, F.IdFarmacia, F.ClaveSSA, F.CantidadPresupuestadaPiezas,
		F.CantidadPresupuestada, F.CantidadRestante, F.Status, F.Actualizado, F.Referencia_01, F.Referencia_02, F.Referencia_03, F.Referencia_04, F.Referencia_05
	From FACT_Fuentes_De_Financiamiento__CargaMasiva F
	Left Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia T (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia And T.ClaveSSA = F.ClaveSSA)
	Where T.IdFuenteFinanciamiento Is Null


	

End 
Go--#SQL