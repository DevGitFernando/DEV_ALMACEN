--------------------- Quitar referencias 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones__MovtosInv_Det_CodigosEAN_Lotes' and xType = 'F' )
Begin 
	Alter Table MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Drop Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones__MovtosInv_Det_CodigosEAN_Lotes 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_MovtosInv_Det_CodigosEAN_Lotes_MovtosInv_Det_CodigosEAN' and xType = 'F' )
Begin 
	Alter Table MovtosInv_Det_CodigosEAN_Lotes Drop Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_MovtosInv_Det_CodigosEAN 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_MovtosInv_Det_CodigosEAN_MovtosInv_Enc' and xType = 'F' )
Begin 
	Alter Table MovtosInv_Det_CodigosEAN Drop Constraint FK_MovtosInv_Det_CodigosEAN_MovtosInv_Enc 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_MovtosInv_ADT___MovtosInv_Enc' and xType = 'F' )
Begin 
	Alter Table MovtosInv_ADT Drop Constraint FK_MovtosInv_ADT___MovtosInv_Enc 		 			
End 
Go--#SQL 	
--------------------- Quitar referencias 


--------------------- Agregar referencias con actualizacion en cascada 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_MovtosInv_ADT___MovtosInv_Enc' and xType = 'F' )
Begin 
	Alter Table MovtosInv_ADT Add Constraint FK_MovtosInv_ADT___MovtosInv_Enc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
		References MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
	On Update Cascade 	
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_MovtosInv_Det_CodigosEAN_MovtosInv_Enc' and xType = 'F' )
Begin 
		Alter Table MovtosInv_Det_CodigosEAN Add Constraint FK_MovtosInv_Det_CodigosEAN_MovtosInv_Enc 
			Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
			References MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv )
		On Update Cascade
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_MovtosInv_Det_CodigosEAN_Lotes_MovtosInv_Det_CodigosEAN' and xType = 'F' )
Begin 
		Alter Table MovtosInv_Det_CodigosEAN_Lotes Add Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_MovtosInv_Det_CodigosEAN
			Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN ) 
			References MovtosInv_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN )  
		On Update Cascade 
End 
Go--#SQL 


If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones__MovtosInv_Det_CodigosEAN_Lotes' and xType = 'F' )
Begin 
	Alter Table MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Add Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones__MovtosInv_Det_CodigosEAN_Lotes  
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, SKU ) 
		References MovtosInv_Det_CodigosEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, SKU )
	On Update Cascade 
End 
Go--#SQL 		
--------------------- Agregar referencias con actualizacion en cascada 
