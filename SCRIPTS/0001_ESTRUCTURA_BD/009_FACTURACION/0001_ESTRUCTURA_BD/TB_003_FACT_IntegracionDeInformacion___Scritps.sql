

Begin Tran 

----		rollback tran   

----		commit tran   


	Insert Into FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze 
	( 
		IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Partida, AfectaEstadistica, TasaIva, Costo, Iva, ImporteNeto, Status
	) 
	select 
		IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Partida, AfectaEstadistica, TasaIva, Costo, Iva, ImporteNeto, Status
	from FACT_FuentesFinanciamiento__Admon_Seccionado___LOAD L (NoLock) 
	Where 
	Not Exists 
		( 
			Select * 
			From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze D (NoLock) 
			Where D.IdFuenteFinanciamiento = L.IdFuenteFinanciamiento and D.IdFinanciamiento = L.IdFinanciamiento and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia 
				and D.ClaveSSA = L.ClaveSSA and D.Referencia_01 = L.Referencia_01 and D.Partida = L.Partida 
		) 
	and Exists 
	(
		Select * 
		From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia DF (NoLock) 
		Where DF.IdFuenteFinanciamiento = L.IdFuenteFinanciamiento and DF.IdFinanciamiento = L.IdFinanciamiento and DF.IdEstado = L.IdEstado and DF.IdFarmacia = L.IdFarmacia 
			and DF.ClaveSSA = L.ClaveSSA and DF.Referencia_01 = L.Referencia_01 
	 
	)



