
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Net_CFGC_Parametros' and Sc.Name = 'FechaControl' ) 
   Alter Table Net_CFGC_Parametros Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CtlCortesParciales' and Sc.Name = 'FechaControl' ) 
   Alter Table CtlCortesParciales Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CtlCortesDiarios' and Sc.Name = 'FechaControl' ) 
   Alter Table CtlCortesDiarios Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Ctl_CierresDePeriodos' and Sc.Name = 'FechaControl' ) 
   Alter Table Ctl_CierresDePeriodos Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Ctl_CierresPeriodosDetalles' and Sc.Name = 'FechaControl' ) 
   Alter Table Ctl_CierresPeriodosDetalles Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CatPasillos' and Sc.Name = 'FechaControl' ) 
   Alter Table CatPasillos Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CatPasillos_Estantes' and Sc.Name = 'FechaControl' ) 
   Alter Table CatPasillos_Estantes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CatPasillos_Estantes_Entrepaños' and Sc.Name = 'FechaControl' ) 
   Alter Table CatPasillos_Estantes_Entrepaños Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'FarmaciaProductos' and Sc.Name = 'FechaControl' ) 
   Alter Table FarmaciaProductos Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'FarmaciaProductos_CodigoEAN' and Sc.Name = 'FechaControl' ) 
   Alter Table FarmaciaProductos_CodigoEAN Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'FarmaciaProductos_CodigoEAN_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table FarmaciaProductos_CodigoEAN_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Movtos_Inv_Tipos_Farmacia' and Sc.Name = 'FechaControl' ) 
   Alter Table Movtos_Inv_Tipos_Farmacia Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'MovtosInv_Enc' and Sc.Name = 'FechaControl' ) 
   Alter Table MovtosInv_Enc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'MovtosInv_Det_CodigosEAN' and Sc.Name = 'FechaControl' ) 
   Alter Table MovtosInv_Det_CodigosEAN Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'MovtosInv_Det_CodigosEAN_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table MovtosInv_Det_CodigosEAN_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'EntradasEnc_Consignacion' and Sc.Name = 'FechaControl' ) 
   Alter Table EntradasEnc_Consignacion Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'EntradasDet_Consignacion' and Sc.Name = 'FechaControl' ) 
   Alter Table EntradasDet_Consignacion Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'EntradasDet_Consignacion_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table EntradasDet_Consignacion_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'EntradasDet_Consignacion_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table EntradasDet_Consignacion_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'AjustesInv_Enc' and Sc.Name = 'FechaControl' ) 
   Alter Table AjustesInv_Enc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'AjustesInv_Det' and Sc.Name = 'FechaControl' ) 
   Alter Table AjustesInv_Det Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'AjustesInv_Det_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table AjustesInv_Det_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'AjustesInv_Det_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table AjustesInv_Det_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CambiosProv_Enc' and Sc.Name = 'FechaControl' ) 
   Alter Table CambiosProv_Enc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CambiosProv_Det_CodigosEAN' and Sc.Name = 'FechaControl' ) 
   Alter Table CambiosProv_Det_CodigosEAN Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CambiosProv_Det_CodigosEAN_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table CambiosProv_Det_CodigosEAN_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CatMedicos' and Sc.Name = 'FechaControl' ) 
   Alter Table CatMedicos Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'CatBeneficiarios' and Sc.Name = 'FechaControl' ) 
   Alter Table CatBeneficiarios Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'VentasEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table VentasEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'VentasDet' and Sc.Name = 'FechaControl' ) 
   Alter Table VentasDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'VentasDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table VentasDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'VentasDet_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table VentasDet_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'VentasEstDispensacion' and Sc.Name = 'FechaControl' ) 
   Alter Table VentasEstDispensacion Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'VentasInformacionAdicional' and Sc.Name = 'FechaControl' ) 
   Alter Table VentasInformacionAdicional Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'VentasEstadisticaClavesDispensadas' and Sc.Name = 'FechaControl' ) 
   Alter Table VentasEstadisticaClavesDispensadas Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Adt_VentasEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table Adt_VentasEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Adt_VentasInformacionAdicional' and Sc.Name = 'FechaControl' ) 
   Alter Table Adt_VentasInformacionAdicional Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Adt_ComprasEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table Adt_ComprasEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Adt_FarmaciaProductos_CodigoEAN_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table Adt_FarmaciaProductos_CodigoEAN_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Vales_EmisionEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table Vales_EmisionEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Vales_EmisionDet' and Sc.Name = 'FechaControl' ) 
   Alter Table Vales_EmisionDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Vales_Emision_InformacionAdicional' and Sc.Name = 'FechaControl' ) 
   Alter Table Vales_Emision_InformacionAdicional Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Vales_Cancelacion' and Sc.Name = 'FechaControl' ) 
   Alter Table Vales_Cancelacion Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ValesEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table ValesEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ValesDet' and Sc.Name = 'FechaControl' ) 
   Alter Table ValesDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ValesDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table ValesDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'RemisionesEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table RemisionesEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'RemisionesDet' and Sc.Name = 'FechaControl' ) 
   Alter Table RemisionesDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'RemisionesDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table RemisionesDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ComprasEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table ComprasEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ComprasDet' and Sc.Name = 'FechaControl' ) 
   Alter Table ComprasDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ComprasDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table ComprasDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ComprasDet_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table ComprasDet_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'COM_OCEN_OrdenesCompra_Claves_Enc' and Sc.Name = 'FechaControl' ) 
   Alter Table COM_OCEN_OrdenesCompra_Claves_Enc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'OrdenesDeComprasEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table OrdenesDeComprasEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'OrdenesDeComprasDet' and Sc.Name = 'FechaControl' ) 
   Alter Table OrdenesDeComprasDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'OrdenesDeComprasDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table OrdenesDeComprasDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'OrdenesDeComprasDet_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table OrdenesDeComprasDet_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'TransferenciasEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table TransferenciasEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'TransferenciasDet' and Sc.Name = 'FechaControl' ) 
   Alter Table TransferenciasDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'TransferenciasDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table TransferenciasDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'TransferenciasDet_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table TransferenciasDet_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'TraspasosEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table TraspasosEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'TraspasosDet' and Sc.Name = 'FechaControl' ) 
   Alter Table TraspasosDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'TraspasosDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table TraspasosDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'TraspasosDet_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table TraspasosDet_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'DevolucionesEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table DevolucionesEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'DevolucionesDet' and Sc.Name = 'FechaControl' ) 
   Alter Table DevolucionesDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'DevolucionesDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table DevolucionesDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'DevolucionesDet_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table DevolucionesDet_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PreSalidasPedidosEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table PreSalidasPedidosEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PreSalidasPedidosDet' and Sc.Name = 'FechaControl' ) 
   Alter Table PreSalidasPedidosDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PreSalidasPedidosDet_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table PreSalidasPedidosDet_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosDet' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosDet_Lotes_Ubicaciones' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosDet_Lotes_Ubicaciones Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosEnvioEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosEnvioEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosEnvioDet' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosEnvioDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosEnvioDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosEnvioDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosEnvioDet_Lotes_Registrar' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosEnvioDet_Lotes_Registrar Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosDistEnc' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosDistEnc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosDistDet' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosDistDet Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'PedidosDistDet_Lotes' and Sc.Name = 'FechaControl' ) 
   Alter Table PedidosDistDet_Lotes Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Pedidos_Cedis_Enc' and Sc.Name = 'FechaControl' ) 
   Alter Table Pedidos_Cedis_Enc Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Pedidos_Cedis_Det' and Sc.Name = 'FechaControl' ) 
   Alter Table Pedidos_Cedis_Det Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Pedidos_Cedis_Det_Surtido' and Sc.Name = 'FechaControl' ) 
   Alter Table Pedidos_Cedis_Det_Surtido Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Pedidos_Cedis_Det_Pedido_Distribuidor' and Sc.Name = 'FechaControl' ) 
   Alter Table Pedidos_Cedis_Det_Pedido_Distribuidor Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ALMJ_Pedidos_RC' and Sc.Name = 'FechaControl' ) 
   Alter Table ALMJ_Pedidos_RC Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ALMJ_Pedidos_RC_Det' and Sc.Name = 'FechaControl' ) 
   Alter Table ALMJ_Pedidos_RC_Det Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ALMJ_Concentrado_PedidosRC' and Sc.Name = 'FechaControl' ) 
   Alter Table ALMJ_Concentrado_PedidosRC Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ALMJ_Concentrado_PedidosRC_Claves' and Sc.Name = 'FechaControl' ) 
   Alter Table ALMJ_Concentrado_PedidosRC_Claves Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'ALMJ_Concentrado_PedidosRC_Pedidos' and Sc.Name = 'FechaControl' ) 
   Alter Table ALMJ_Concentrado_PedidosRC_Pedidos Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Ventas_ALMJ_PedidosRC_Surtido' and Sc.Name = 'FechaControl' ) 
   Alter Table Ventas_ALMJ_PedidosRC_Surtido Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) 
                Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) 
                Where So.Name = 'Ventas_TiempoAire' and Sc.Name = 'FechaControl' ) 
   Alter Table Ventas_TiempoAire Add FechaControl DateTime Not Null Default getdate() 
Go--#SQL 
 

