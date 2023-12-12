--------------------- Quitar referencias 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_DevolucionesDet_Lotes_Ubicaciones_DevolucionesDet_Lotes' and xType = 'F' )
Begin 
	Alter Table DevolucionesDet_Lotes_Ubicaciones Drop Constraint FK_DevolucionesDet_Lotes_Ubicaciones_DevolucionesDet_Lotes 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_DevolucionesDet_Lotes_DevolucionesDet' and xType = 'F' )
Begin 
	Alter Table DevolucionesDet_Lotes Drop Constraint FK_DevolucionesDet_Lotes_DevolucionesDet 		 			
End 
Go--#SQL 	

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_DevolucionesDet_DevolucionesEnc' and xType = 'F' )
Begin 
	Alter Table DevolucionesDet Drop Constraint FK_DevolucionesDet_DevolucionesEnc 		 			
End 
Go--#SQL 	
--------------------- Quitar referencias 


--------------------- Agregar referencias con actualizacion en cascada 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_DevolucionesDet_DevolucionesEnc' and xType = 'F' )
Begin 
	Alter Table DevolucionesDet Add Constraint FK_DevolucionesDet_DevolucionesEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion ) References DevolucionesEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion ) 
	On Update Cascade
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_DevolucionesDet_Lotes_DevolucionesDet' and xType = 'F' )
Begin 
	Alter Table DevolucionesDet_Lotes Add Constraint FK_DevolucionesDet_Lotes_DevolucionesDet 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN ) 
		References DevolucionesDet ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN )  
	On Update Cascade 
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_DevolucionesDet_Lotes_Ubicaciones_DevolucionesDet_Lotes' and xType = 'F' )
Begin 
	Alter Table DevolucionesDet_Lotes_Ubicaciones Add Constraint FK_DevolucionesDet_Lotes_Ubicaciones_DevolucionesDet_Lotes
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote, SKU ) 
		References DevolucionesDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote, SKU ) 
	On Update Cascade 
End 
Go--#SQL 	
--------------------- Agregar referencias con actualizacion en cascada 
