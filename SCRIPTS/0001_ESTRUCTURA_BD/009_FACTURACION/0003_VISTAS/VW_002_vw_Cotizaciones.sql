

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_LCTCN_Cotizaciones' and xType = 'V' ) 
	Drop View vw_LCTCN_Cotizaciones 
Go--#SQL
 	

Create View vw_LCTCN_Cotizaciones 
With Encryption 
As 
	Select C.FolioCotizacion As Folio, C.IdEmpresa, E.Nombre as Empresa, C.NombreCliente, C.Licitacion,
	C.SubTotalSinGrabar_Min, C.SubTotalSinGrabar_Max, C.SubTotalGrabado_Min, C.SubTotalGrabado_Max, 
	C.Iva_Min, C.Iva_Max, C.Total_Min, C.Total_Max, C.Tipo, 
	Case When C.Tipo = 1 Then 'Maximos y Minimos' Else ' Cantidades Fijas' End As DescripcionTipo,
	C.Observaciones, C.FechaRegistro, C.Status	       
	From LCTCN_Cotizaciones C (NoLock) 
	Inner Join CatEmpresas E (NoLock) On ( C.IdEmpresa = E.IdEmpresa ) 
	
Go--#SQL	
 
-------------  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_LCTCN_Cotizaciones_Claves' and xType = 'V' ) 
	Drop View vw_LCTCN_Cotizaciones_Claves 
Go--#SQL
 	

Create View vw_LCTCN_Cotizaciones_Claves 
With Encryption 
As 
	Select E.Folio, E.IdEmpresa, E.Empresa, E.NombreCliente, E.Licitacion, E.Tipo, E.DescripcionTipo, E.FechaRegistro,
	S.ClaveSSA, D.Partida, D.IdClaveSSA, S.DescripcionClave, S.TipoDeClave, S.TipoDeClaveDescripcion, S.Presentacion, 
	S.ContenidoPaquete, D.TipoManejo, Case When D.TipoManejo = 1 Then 'CAJA' Else 'PIEZA' End As DescTipoManejo, 
	D.CostoCompra, D.PrecioPaquete, D.PrecioPieza, D.Porcentaje, D.CantidadMinima, D.CantidadMaxima, 
	Case When T.PorcIva = 0 Then (D.CantidadMinima * D.PrecioPaquete ) Else 0 End As SubTotalSinGrabarMin,
	Case When T.PorcIva = 0 Then (D.CantidadMaxima * D.PrecioPaquete ) Else 0 End As SubTotalSinGrabarMax,
	T.PorcIva As TasaIva,
	Case When T.PorcIva = 16 Then (D.CantidadMinima * D.PrecioPaquete ) Else 0 End As SubTotalGrabadoMin,
	Case When T.PorcIva = 16 Then (D.CantidadMaxima * D.PrecioPaquete ) Else 0 End As SubTotalGrabadoMax,
	Case When T.PorcIva = 16 Then (D.CantidadMinima * (D.PrecioPaquete * ( T.PorcIva/100.00 )) ) Else 0 End As IvaMin,
	Case When T.PorcIva = 16 Then (D.CantidadMaxima * (D.PrecioPaquete * ( T.PorcIva/100.00 )) ) Else 0 End As IvaMax,
	D.EsCause, D.Admon, E.Observaciones	   		
	From vw_LCTCN_Cotizaciones E (NoLock) 
	Inner Join LCTCN_Cotizaciones_Claves D (NoLock) On ( E.Folio = D.FolioCotizacion )
	Inner Join vw_ClavesSSA_Sales S (Nolock) On (D.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Inner Join CatTiposDeProducto T (Nolock) On ( S.TipoDeClave = T.IdTipoProducto ) 
	
Go--#SQL
 

-------------  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_LCTCN_Cotizaciones_Claves_Impresion' and xType = 'V' ) 
	Drop View vw_LCTCN_Cotizaciones_Claves_Impresion 
Go--#SQL
 	

Create View vw_LCTCN_Cotizaciones_Claves_Impresion 
With Encryption 
As 
	Select Folio, IdEmpresa, Empresa, NombreCliente, Licitacion, Tipo, DescripcionTipo, FechaRegistro,
	ClaveSSA, Partida, IdClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, Presentacion, 
	ContenidoPaquete, TipoManejo, DescTipoManejo, CostoCompra, PrecioPaquete, PrecioPieza, Porcentaje, 
	CantidadMinima, CantidadMaxima, 
	SubTotalSinGrabarMin, SubTotalSinGrabarMax, TasaIva, SubTotalGrabadoMin, SubTotalGrabadoMax, IvaMin, IvaMax,
	( SubTotalSinGrabarMin + SubTotalGrabadoMin + IvaMin ) As TotalMin, 
	( SubTotalSinGrabarMax + SubTotalGrabadoMax + IvaMax ) As TotalMax,
	EsCause, Admon, Observaciones	   		
	From vw_LCTCN_Cotizaciones_Claves (NoLock) 
	
	
Go--#SQL