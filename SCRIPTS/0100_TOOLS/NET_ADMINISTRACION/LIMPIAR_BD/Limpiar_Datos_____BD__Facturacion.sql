Set DateFormat YMD 
Go--#SQL 

/* 


	
	select 
		'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + name + char(39) + ' and xType = ' + char(39) + 'U' + char(39) + ' )    Drop Table ' + name,   
		'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + name + char(39) + ' and xType = ' + char(39) + 'U' + char(39) + ' )    Delete From ' + name  
	From sysobjects 
	where name like '%FACT_CFDI%nota%' and xType = 'U' 
	order by crdate desc 
	

*/ 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__01__Encabezado' and xType = 'U' )    Delete From FACT_CFDI_TM__01__Encabezado
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__02__DetallesClaves' and xType = 'U' )    Delete From FACT_CFDI_TM__02__DetallesClaves
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__03__FolioElectronico' and xType = 'U' )    Delete From FACT_CFDI_TM__03__FolioElectronico
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_ComplementoDePagos_DoctosRelacionados' and xType = 'U' )    Delete From FACT_CFDI_ComplementoDePagos_DoctosRelacionados
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_ComplementoDePagos_Detalles' and xType = 'U' )    Delete From FACT_CFDI_ComplementoDePagos_Detalles
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_ComplementoDePagos__CargaMasiva' and xType = 'U' )    Delete From FACT_CFDI_ComplementoDePagos__CargaMasiva
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_MetodosPago' and xType = 'U' )    Delete From FACT_CFDI_MetodosPago
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_FormasDePago' and xType = 'U' )    Delete From FACT_CFDI_FormasDePago
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_NotasDeCredito__CargaMasiva' and xType = 'U' )    Delete From FACT_CFDI_NotasDeCredito__CargaMasiva
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP' and xType = 'U' )    Delete From FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_NotasDeCredito_DoctosRelacionados' and xType = 'U' )    Delete From FACT_CFDI_NotasDeCredito_DoctosRelacionados
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_XML_VP' and xType = 'U' )    Delete From FACT_CFDI_XML_VP 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados_MetodosDePago_VP' and xType = 'U' )    Delete From FACT_CFD_Documentos_Generados_MetodosDePago_VP
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados_Detalles_VP' and xType = 'U' )    Delete From FACT_CFD_Documentos_Generados_Detalles_VP
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados_VP' and xType = 'U' )    Delete From FACT_CFD_Documentos_Generados_VP
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_XML' and xType = 'U' )    Delete From FACT_CFDI_XML 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados_MetodosDePago' and xType = 'U' )    Delete From FACT_CFD_Documentos_Generados_MetodosDePago 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados_Detalles' and xType = 'U' )    Delete From FACT_CFD_Documentos_Generados_Detalles
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados' and xType = 'U' )    Delete From FACT_CFD_Documentos_Generados


Go--#SQL 



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles____Recalculo_Concentrado' and xType = 'U')    Drop Table FACT_Remisiones_Detalles____Recalculo_Concentrado  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles____Recalculo_Resumen' and xType = 'U')    Drop Table FACT_Remisiones_Detalles____Recalculo_Resumen  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles____Recalculo_Importes' and xType = 'U')    Drop Table FACT_Remisiones_Detalles____Recalculo_Importes  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles____Recalculo___VENTAS' and xType = 'U')    Drop Table FACT_Remisiones_Detalles____Recalculo___VENTAS  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles____Recalculo_Concentrado' and xType = 'U')    Delete From FACT_Remisiones_Detalles____Recalculo_Concentrado  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles____Recalculo_Resumen' and xType = 'U')    Delete From FACT_Remisiones_Detalles____Recalculo_Resumen  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles____Recalculo_Importes' and xType = 'U')    Delete From FACT_Remisiones_Detalles____Recalculo_Importes  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles____Recalculo___VENTAS' and xType = 'U')    Delete From FACT_Remisiones_Detalles____Recalculo___VENTAS  




 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Farmacias_CLUES' and xType = 'U')    Delete From FACT_Farmacias_CLUES  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_RPT_Facturacion_Referencias' and xType = 'U')    Delete From FACT_RPT_Facturacion_Referencias  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_ClavesSSA_InformacionMarcaComercial_Excepcion' and xType = 'U')    Delete From FACT_ClavesSSA_InformacionMarcaComercial_Excepcion  



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas_x_Importes' and xType = 'U')    Delete From FACT_Remisiones__RelacionFacturas_x_Importes  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas' and xType = 'U')    Delete From FACT_Remisiones__RelacionFacturas  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Cancelaciones_Detalles' and xType = 'U')    Delete From FACT_Remisiones_Cancelaciones_Detalles  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Cancelaciones' and xType = 'U')    Delete From FACT_Remisiones_Cancelaciones  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones___GUID' and xType = 'U')    Delete From FACT_Remisiones___GUID  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_InformacionAdicional_Almacenes' and xType = 'U')    Delete From FACT_Remisiones_InformacionAdicional_Almacenes  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Documentos' and xType = 'U')    Delete From FACT_Remisiones_Documentos  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_InformacionAdicional' and xType = 'U')    Delete From FACT_Remisiones_InformacionAdicional  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Resumen' and xType = 'U')    Delete From FACT_Remisiones_Resumen  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Concentrado' and xType = 'U')    Delete From FACT_Remisiones_Concentrado  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles' and xType = 'U')    Delete From FACT_Remisiones_Detalles  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Facturas' and xType = 'U')    Delete From FACT_Facturas  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Remisiones' and xType = 'U')    Delete From FACT_Remisiones  

Go--#SQL 



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento__ClavesSSA_Precios__Excepciones  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario___20180425' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario___20180425  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario___20180425' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario___20180425  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario__Duplicado' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario__Duplicado  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario__Duplicado' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario__Duplicado  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Duplicado' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Duplicado  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Duplicado' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Duplicado  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Beneficiarios' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Beneficiarios  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles__Beneficiario__LOAD___201710041230' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles__Beneficiario__LOAD___201710041230  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles__Beneficiario__LOAD' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles__Beneficiario__LOAD  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_Documentos' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_Documentos  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_FuentesFinanciamiento__LOAD' and xType = 'U')    Delete From FACT_FuentesFinanciamiento__LOAD  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_FuentesFinanciamiento__LOAD_Incremento' and xType = 'U')    Delete From FACT_FuentesFinanciamiento__LOAD_Incremento  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon_Detalles  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Admon  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento_Detalles  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento' and xType = 'U')    Delete From FACT_Fuentes_De_Financiamiento  

Go--#SQL 




If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Incremento___VentasDet_Lotes' and xType = 'U' )    Delete From FACT_Incremento___VentasDet_Lotes  
 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstadisticaClavesDispensadas' and xType = 'U' )    Delete From VentasEstadisticaClavesDispensadas  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasInformacionAdicional' and xType = 'U' )    Delete From VentasInformacionAdicional  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes' and xType = 'U' )    Delete From VentasDet_Lotes  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet' and xType = 'U' )    Delete From VentasDet  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEnc' and xType = 'U' )    Delete From VentasEnc  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'U' )    Delete From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' )    Delete From FarmaciaProductos_CodigoEAN_Lotes  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN' and xType = 'U' )    Delete From FarmaciaProductos_CodigoEAN 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos' and xType = 'U' )    Delete From FarmaciaProductos 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatBeneficiarios' and xType = 'U' )    Delete From CatBeneficiarios 

Go--#SQL 


