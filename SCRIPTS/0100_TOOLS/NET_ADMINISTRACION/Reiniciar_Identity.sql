

DBCC CHECKIDENT ('Movtos_Inv_Tipos', RESEED, 1) 
DBCC CHECKIDENT ('MovtosInv_Enc', RESEED, 1)  
DBCC CHECKIDENT ('MovtosInv_Det_CodigosEAN', RESEED, 1) 
DBCC CHECKIDENT ('MovtosInv_Det_CodigosEAN_Lotes', RESEED, 1) 
DBCC CHECKIDENT ('MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones', RESEED, 1) 
Go--#SQL 


DBCC CHECKIDENT ('AjustesInv_Enc', RESEED, 1)
DBCC CHECKIDENT ('AjustesInv_Det', RESEED, 1)
DBCC CHECKIDENT ('AjustesInv_Det_Lotes', RESEED, 1)
DBCC CHECKIDENT ('AjustesInv_Det_Lotes_Ubicaciones', RESEED, 1)
Go--#SQL 


DBCC CHECKIDENT ('Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico', RESEED, 1)
DBCC CHECKIDENT ('Pedidos_Cedis_Det_Surtido_Distribucion', RESEED, 1)
DBCC CHECKIDENT ('Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso', RESEED, 1)
DBCC CHECKIDENT ('Pedidos_Cedis_Enc_Surtido_Atenciones', RESEED, 1)
Go--#SQL 

DBCC CHECKIDENT ('CambiosCartasCanje_Enc', RESEED, 1)
DBCC CHECKIDENT ('CambiosProv_Enc', RESEED, 1)
DBCC CHECKIDENT ('CatClavesSSA_Sales_Historico', RESEED, 1)
DBCC CHECKIDENT ('CFG_Terminales_Versiones', RESEED, 1)
DBCC CHECKIDENT ('CierreInventario_Tablas_Limpiar', RESEED, 1) 
GO--#SQL 


DBCC CHECKIDENT ('FACT_CFD_Documentos_Generados', RESEED, 1)
DBCC CHECKIDENT ('FACT_CFD_Conceptos', RESEED, 1) 
DBCC CHECKIDENT ('FACT_CFDI_XML', RESEED, 1) 
DBCC CHECKIDENT ('FACT_CFD_SeriesFolios', RESEED, 1)

DBCC CHECKIDENT ('FACT_Facturas', RESEED, 1)
DBCC CHECKIDENT ('FACT_Remisiones___GUID', RESEED, 1) 


GO--#SQL 