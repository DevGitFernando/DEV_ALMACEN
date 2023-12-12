--------------------- Quitar referencias 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_TransferenciasDet_TransferenciasEnc' and xType = 'F' )
Begin 
	Alter Table TransferenciasDet Drop Constraint FK_TransferenciasDet_TransferenciasEnc 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_TransferenciasDet_Lotes_TransferenciasDet' and xType = 'F' )
Begin 
	Alter Table TransferenciasDet_Lotes Drop Constraint FK_TransferenciasDet_Lotes_TransferenciasDet 		 			
End 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_TransferenciasDet_Lotes_TransferenciasDet' and xType = 'F' )
Begin 
	Alter Table TransferenciasDet_Lotes_Ubicaciones Drop Constraint FK_TransferenciasDet_Lotes_TransferenciasDet 		 			
End 
Go--#SQL 
--------------------- Quitar referencias 



--------------------- Agregar referencias con actualizacion en cascada 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_TransferenciasDet_TransferenciasEnc' and xType = 'F' )
Begin 
	Alter Table TransferenciasDet Add Constraint FK_TransferenciasDet_TransferenciasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) 
		References TransferenciasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) 
	On Update Cascade
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FK_TransferenciasDet_Lotes_TransferenciasDet' and xType = 'F' )
Begin 
	Alter Table TransferenciasDet_Lotes Add Constraint FK_TransferenciasDet_Lotes_TransferenciasDet 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon ) 
		References TransferenciasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon ) 
	On Update Cascade 
End 
Go--#SQL 

If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'PK_TransferenciasDet_Lotes_Ubicaciones_TransferenciasDet_Lotes' and xType = 'F' )
Begin 
	Alter Table TransferenciasDet_Lotes_Ubicaciones Add Constraint PK_TransferenciasDet_Lotes_Ubicaciones_TransferenciasDet_Lotes  
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon, SKU ) 
		References TransferenciasDet_Lotes  
					( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon, SKU )  
	On Update Cascade 
End 
Go--#SQL 	
--------------------- Agregar referencias con actualizacion en cascada 


