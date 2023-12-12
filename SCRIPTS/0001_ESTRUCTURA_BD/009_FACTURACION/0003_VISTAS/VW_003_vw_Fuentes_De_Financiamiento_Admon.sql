--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_FuentesDeFinanciamiento_Admon' and xType = 'V' ) 
	Drop View vw_FACT_FuentesDeFinanciamiento_Admon
Go--#SQL

Create View vw_FACT_FuentesDeFinanciamiento_Admon 
With Encryption 
As 
	Select	M.IdFuenteFinanciamiento, M.IdEstado, E.Nombre as Estado, 
			M.IdCliente, IsNull(C.NombreCliente, '') as Cliente, 
			M.IdSubCliente, IsNull(C.NombreSubCliente, '') as SubCliente, 
			M.FechaInicial, M.FechaFinal, 
			M.Monto, M.Piezas, 
			M.Status
	From FACT_Fuentes_De_Financiamiento_Admon M (NoLock) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 	
	Left Join vw_Clientes_SubClientes C (NoLock) On ( M.IdCliente = C.IdCliente and M.IdSubCliente = C.IdSubCliente )

Go--#SQL
 
--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_FuentesDeFinanciamiento_Admon_Detalle' and xType = 'V' ) 
	Drop View vw_FACT_FuentesDeFinanciamiento_Admon_Detalle
Go--#SQL

Create View vw_FACT_FuentesDeFinanciamiento_Admon_Detalle 
With Encryption 
As 
	Select	M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, 
			M.FechaInicial, M.FechaFinal, M.Monto as MontoFuente, M.Piezas as PiezasFuente, 
			D.IdFinanciamiento, D.Descripcion as Financiamiento, D.Monto as MontoDetalle, D.Piezas as PiezasDetalle, 
			D.ValidarPolizaBeneficiario, D.LongitudMinimaPoliza, D.LongitudMaximaPoliza, 
			M.Status, D.Status as StatusDetalle
	From vw_FACT_FuentesDeFinanciamiento_Admon M (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles D (NoLock) On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento )

Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_FuentesDeFinanciamiento_Admon_Claves' and xType = 'V' ) 
	Drop View vw_FACT_FuentesDeFinanciamiento_Admon_Claves
Go--#SQL

Create View vw_FACT_FuentesDeFinanciamiento_Admon_Claves 
With Encryption 
As 
	Select
			M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, 
			M.FechaInicial, M.FechaFinal, M.MontoFuente, M.PiezasFuente,
			M.IdFinanciamiento, M.Financiamiento, M.MontoDetalle, M.PiezasDetalle, 
			S.IdClaveSSA_Sal as IdClaveSSA, S.ClaveSSA_Base, D.ClaveSSA, S.DescripcionSal as DescripcionClaveSSA, S.Presentacion, 
			D.Costo, D.Agrupacion, D.CostoUnitario, D.TasaIva, D.Iva, D.ImporteNeto, 
			M.Status, D.Status as StatusClave
	From vw_FACT_FuentesDeFinanciamiento_Detalle M (NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA D (NoLock) On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento And M.IdFinanciamiento = D.IdFinanciamiento )
	Inner Join vw_ClavesSSA_Sales S(NoLock) On ( D.ClaveSSA = S.ClaveSSA )

Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_FuentesDeFinanciamiento_Admon' and xType = 'V' ) 
	Drop View vw_Impresion_FuentesDeFinanciamiento_Admon
Go--#SQL

Create View vw_Impresion_FuentesDeFinanciamiento_Admon 
With Encryption 
As 
	Select	M.IdFuenteFinanciamiento, M.IdEstado, M.Estado, 
			M.IdCliente, M.Cliente, M.IdSubCliente, M.SubCliente, 
			M.FechaInicial, M.FechaFinal, M.MontoFuente, M.PiezasFuente,
			M.IdFinanciamiento, M.Financiamiento, M.MontoDetalle, M.PiezasDetalle, 
			IsNull(S.IdClaveSSA_Sal, '') as IdClaveSSA, IsNull(S.ClaveSSA_Base, '' ) as ClaveSSA_Base, IsNull(D.ClaveSSA, '' ) as ClaveSSA, 
			IsNull(S.DescripcionSal, '' ) as DescripcionClaveSSA,  
			M.Status, D.Status as StatusClave
	From vw_FACT_FuentesDeFinanciamiento_Admon_Detalle M (NoLock) 
	Left Join FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA D (NoLock) On ( M.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento And M.IdFinanciamiento = D.IdFinanciamiento )
	Left Join vw_ClavesSSA_Sales S(NoLock) On ( D.ClaveSSA = S.ClaveSSA )

Go--#SQL 

