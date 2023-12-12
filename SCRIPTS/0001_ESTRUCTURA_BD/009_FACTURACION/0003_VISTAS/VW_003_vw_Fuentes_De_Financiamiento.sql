--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_FuentesDeFinanciamiento' and xType = 'V' ) 
	Drop View vw_FACT_FuentesDeFinanciamiento
Go--#SQL 

Create View vw_FACT_FuentesDeFinanciamiento 
With Encryption 
As 
	Select	M.IdFuenteFinanciamiento, M.IdEstado, E.Nombre as Estado, 
			M.IdCliente, IsNull(C.NombreCliente, '') as Cliente, 
			M.IdSubCliente, IsNull(C.NombreSubCliente, '') as SubCliente, 
			M.NumeroDeContrato, M.NumeroDeLicitacion, 

			M.TipoDeUnidades, 
			( case When M.TipoDeUnidades = 1 then 'ORDINARIAS' else 'DOSIS UNITARIA' end) as TipoDeUnidades_Descripcion, 	

			M.EsParaExcedente, 
			( case When M.EsParaExcedente = 1 then 'EXCEDENTE' else 'ORDINARIO' end) as EsParaExcedente_Descripcion, 	

			M.EsDiferencial, 
			( case When M.EsDiferencial = 1 then 'SI' else 'NO' end) as EsDiferencial_Descripcion, 	
			M.Alias, 
			M.TipoClasificacionSSA, 
			M.FechaInicial, M.FechaFinal, 
			M.Monto, M.Piezas, 
			M.Status 
	From FACT_Fuentes_De_Financiamiento M (NoLock) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 	
	Left Join vw_Clientes_SubClientes C (NoLock) On ( M.IdCliente = C.IdCliente and M.IdSubCliente = C.IdSubCliente )

Go--#SQL
 
--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_FuentesDeFinanciamiento_Detalle' and xType = 'V' ) 
	Drop View vw_FACT_FuentesDeFinanciamiento_Detalle
Go--#SQL

Create View vw_FACT_FuentesDeFinanciamiento_Detalle 
With Encryption 
As 
	Select	M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, M.NumeroDeContrato, M.NumeroDeLicitacion, 
			M.FechaInicial, M.FechaFinal, M.Monto as MontoFuente, M.Piezas as PiezasFuente, 
			D.IdFinanciamiento, D.Descripcion as Financiamiento, 

			M.TipoDeUnidades, M.TipoDeUnidades_Descripcion, 	

			M.EsParaExcedente, M.EsParaExcedente_Descripcion, M.EsDiferencial, 
			D.Alias, 
			D.Prioridad, 
			D.Monto as MontoDetalle, D.Piezas as PiezasDetalle, 
			D.ValidarPolizaBeneficiario, D.LongitudMinimaPoliza, D.LongitudMaximaPoliza, 
			M.Status, D.Status as StatusDetalle
	From vw_FACT_FuentesDeFinanciamiento M (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles D (NoLock) On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento )

Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_FuentesDeFinanciamiento_Claves' and xType = 'V' ) 
	Drop View vw_FACT_FuentesDeFinanciamiento_Claves
Go--#SQL

Create View vw_FACT_FuentesDeFinanciamiento_Claves 
With Encryption 
As 
	Select	M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, M.NumeroDeContrato, M.NumeroDeLicitacion, 
			M.FechaInicial, M.FechaFinal, M.MontoFuente, M.PiezasFuente,
			M.IdFinanciamiento, M.Financiamiento, 
			M.Prioridad, 
			M.MontoDetalle, M.PiezasDetalle, 
			S.IdClaveSSA_Sal as IdClaveSSA, S.ClaveSSA_Base, D.ClaveSSA, S.DescripcionSal as DescripcionClaveSSA, 
			S.TipoDeClave, S.TipoDeClaveDescripcion, 
			D.PorcParticipacion, 
			S.ContenidoPaquete, 
			D.CantidadPresupuestadaPiezas, 
			D.CantidadPresupuestada, D.CantidadAsignada, D.CantidadRestante,  
			M.Status, D.Status as StatusClave 
	From vw_FACT_FuentesDeFinanciamiento_Detalle M (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento And M.IdFinanciamiento = D.IdFinanciamiento )
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( D.ClaveSSA = S.ClaveSSA )

Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_FuentesDeFinanciamiento' and xType = 'V' ) 
	Drop View vw_Impresion_FuentesDeFinanciamiento
Go--#SQL

Create View vw_Impresion_FuentesDeFinanciamiento 
With Encryption 
As 
	Select	M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, M.NumeroDeContrato, M.NumeroDeLicitacion, 
			M.FechaInicial, M.FechaFinal, M.MontoFuente, M.PiezasFuente,
			M.IdFinanciamiento, M.Financiamiento, M.MontoDetalle, M.PiezasDetalle, 
			IsNull(S.IdClaveSSA_Sal, '') as IdClaveSSA, IsNull(S.ClaveSSA_Base, '' ) as ClaveSSA_Base, IsNull(D.ClaveSSA, '' ) as ClaveSSA, 
			IsNull(S.DescripcionSal, '' ) as DescripcionClaveSSA,  
			M.Status, D.Status as StatusClave
	From vw_FACT_FuentesDeFinanciamiento_Detalle M (NoLock) 
	Left Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA D (NoLock) On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento And M.IdFinanciamiento = D.IdFinanciamiento )
	Left Join vw_ClavesSSA_Sales S(NoLock) On ( D.ClaveSSA = S.ClaveSSA )

Go--#SQL 




--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_FACT_FuentesDeFinanciamiento_Claves__Farmacia' and xType = 'V' ) 
	Drop View vw_FACT_FuentesDeFinanciamiento_Claves__Farmacia
Go--#SQL

Create View vw_FACT_FuentesDeFinanciamiento_Claves__Farmacia 
With Encryption 
As 
	Select	M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, M.NumeroDeContrato, M.NumeroDeLicitacion, 
			M.FechaInicial, M.FechaFinal, M.MontoFuente, M.PiezasFuente,
			M.IdFinanciamiento, M.Financiamiento, 
			M.Prioridad, 
			M.MontoDetalle, M.PiezasDetalle, 
			D.IdFarmacia, f.NombreFarmacia as Farmacia, 
			M.IdClaveSSA, M.ClaveSSA_Base, M.ClaveSSA, M.DescripcionClaveSSA, 
			M.TipoDeClave, M.TipoDeClaveDescripcion, 
			M.ContenidoPaquete, 
			M.CantidadPresupuestadaPiezas, 
			M.CantidadPresupuestada, M.CantidadAsignada, M.CantidadRestante,  
			M.Status, M.Status as StatusClave 
	From vw_FACT_FuentesDeFinanciamiento_Claves M (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia D (NoLock) 
		On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento And M.IdFinanciamiento = D.IdFinanciamiento and M.ClaveSSA = D.ClaveSSA )
	Inner Join CatFarmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia ) 

Go--#SQL 



---  vw_Fuentes_De_Financiamiento__02_Beneficiarios

--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Fuentes_De_Financiamiento___Beneficiarios' and xType = 'V' ) 
	Drop View vw_Fuentes_De_Financiamiento___Beneficiarios
Go--#SQL

Create View vw_Fuentes_De_Financiamiento___Beneficiarios 
With Encryption 
As 
	

	Select	M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, M.NumeroDeContrato, M.NumeroDeLicitacion, 
			B.IdEstado as IdEstado__Beneficiario_Relacionado, B.IdFarmacia as IdFarmacia__Beneficiario_Relacionado, 
			D.IdBeneficiario, D.NombreBeneficiario, 
			BR.IdBeneficiario_Relacionado, 
			(B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as Beneficiario_Relacionado, 
			J.Descripcion as Jurisdiccion_Beneficiario_Relacionado, 
			M.Status, D.Status as StatusDetalle, BR.Status as StatusDetalle_Beneficiario
	From vw_FACT_FuentesDeFinanciamiento M (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Beneficiarios D (NoLock) 
		On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento )
	Inner Join FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados BR (NoLock) 
		On ( D.IdFuenteFinanciamiento = BR.IdFuenteFinanciamiento and M.IdCliente = BR.IdCliente and M.IdSubCliente = BR.IdSubCliente and D.IdBeneficiario = BR.IdBeneficiario ) 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( B.IdEstado = BR.IdEstado and B.IdFarmacia = BR.IdFarmacia and B.IdCliente = BR.IdCliente and B.IdSubCliente = BR.IdSubCliente and B.IdBeneficiario = BR.IdBeneficiario_Relacionado ) 
	Inner Join CatJurisdicciones J (NoLock) On ( B.IdEstado = J.IdEstado and B.IdJurisdiccion = J.IdJurisdiccion ) 

-- IdFuenteFinanciamiento, IdBeneficiario, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario_Relacionado, Status, Actualizado

--	FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados 



Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Fuentes_De_Financiamiento___Beneficiarios_ClavesSSA' and xType = 'V' ) 
	Drop View vw_Fuentes_De_Financiamiento___Beneficiarios_ClavesSSA
Go--#SQL

Create View vw_Fuentes_De_Financiamiento___Beneficiarios_ClavesSSA 
With Encryption 
As 
	

	Select		 
			M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, M.NumeroDeContrato, M.NumeroDeLicitacion, 
			M.FechaInicial, M.FechaFinal, M.MontoFuente, M.PiezasFuente,
			M.IdFinanciamiento, M.Financiamiento, 

			DB.IdBeneficiario, 
				( Select Top 1 D.NombreBeneficiario From FACT_Fuentes_De_Financiamiento_Beneficiarios D (NoLock) 
					Where DB.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and DB.IdBeneficiario = D.IdBeneficiario ) as NombreBeneficiario,  

			M.Prioridad, 
			M.MontoDetalle, M.PiezasDetalle, 
			M.IdClaveSSA, M.ClaveSSA_Base, M.ClaveSSA, M.DescripcionClaveSSA, 
			M.TipoDeClave, M.TipoDeClaveDescripcion, 
			DB.Referencia_01, DB.Referencia_02, DB.Referencia_03, DB.Referencia_04, 
			M.PorcParticipacion, 
			M.ContenidoPaquete, 
			M.CantidadPresupuestadaPiezas, 
			M.CantidadPresupuestada, M.CantidadAsignada, M.CantidadRestante,  
			M.Status, M.StatusClave 
	From vw_FACT_FuentesDeFinanciamiento_Claves M (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario DB (NoLock) 
		On ( M.IdFuenteFinanciamiento = DB.IdFuenteFinanciamiento and M.IdFinanciamiento = DB.IdFinanciamiento and M.ClaveSSA = DB.ClaveSSA ) 
	-- Inner Join FACT_Fuentes_De_Financiamiento_Beneficiarios D (NoLock) On ( DB.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento )

--	FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados 
	
--	sp_listacolumnas FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario 


Go--#SQL 


