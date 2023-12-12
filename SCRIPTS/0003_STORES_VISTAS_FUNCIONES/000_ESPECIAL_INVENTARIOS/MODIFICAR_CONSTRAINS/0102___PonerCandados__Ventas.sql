--------------------- Quitar referencias 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasEstDispensacion_VentasEnc' and xType = 'F' )
Begin 
	Alter Table VentasEstDispensacion Drop Constraint FK_VentasEstDispensacion_VentasEnc 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasEstadisticaClavesDispensadas_VentasEnc' and xType = 'F' )
Begin 
	Alter Table VentasEstadisticaClavesDispensadas Drop Constraint FK_VentasEstadisticaClavesDispensadas_VentasEnc 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasDet_Lotes_VentasDet' and xType = 'F' )
Begin 
	Alter Table VentasDet_Lotes Drop Constraint FK_VentasDet_Lotes_VentasDet 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasDet_Lotes_Ubicaciones_VentasDet_Lotes' and xType = 'F' )
Begin 
	Alter Table VentasDet_Lotes_Ubicaciones Drop Constraint FK_VentasDet_Lotes_Ubicaciones_VentasDet_Lotes 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasDet_VentasEnc' and xType = 'F' )
Begin 
	Alter Table VentasDet Drop Constraint FK_VentasDet_VentasEnc 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasInformacionAdicional_VentasEnc' and xType = 'F' )
Begin 
	Alter Table VentasInformacionAdicional Drop Constraint FK_VentasInformacionAdicional_VentasEnc 		 			
End 
Go--#SQL 	
--------------------- Quitar referencias 


--------------------- Agregar referencias con actualizacion en cascada 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasEstDispensacion_VentasEnc' and xType = 'F' )
Begin 
	Alter Table VentasEstDispensacion Add Constraint FK_VentasEstDispensacion_VentasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	--  On Update Cascade
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasDet_VentasEnc' and xType = 'F' )
Begin 
	Alter Table VentasDet Add Constraint FK_VentasDet_VentasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	--  On Update Cascade 
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasDet_Lotes_VentasDet' and xType = 'F' )
Begin 
	Alter Table VentasDet_Lotes Add Constraint FK_VentasDet_Lotes_VentasDet 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon ) 
		References VentasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon ) 
	--  On Update Cascade 
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasDet_Lotes_Ubicaciones_VentasDet_Lotes' and xType = 'F' )
Begin 
	Alter Table VentasDet_Lotes_Ubicaciones Add Constraint FK_VentasDet_Lotes_Ubicaciones_VentasDet_Lotes 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon, SKU ) 
		References VentasDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon, SKU ) 
	--On Update Cascade 
End 
Go--#SQL 


If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasEstadisticaClavesDispensadas_VentasEnc' and xType = 'F' )
Begin 
	Alter Table VentasEstadisticaClavesDispensadas Add Constraint FK_VentasEstadisticaClavesDispensadas_VentasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	--  On Update Cascade 		
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_VentasInformacionAdicional_VentasEnc' and xType = 'F' )
Begin 
	Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional_VentasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	--  On Update Cascade 
End 
Go--#SQL 	
--------------------- Agregar referencias con actualizacion en cascada 
