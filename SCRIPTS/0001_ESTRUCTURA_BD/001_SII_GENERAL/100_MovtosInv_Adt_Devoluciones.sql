


If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Adt_Devoluciones' and xType = 'U' )
Begin
	Create Table MovtosInv_Adt_Devoluciones 
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		FolioMovtoInv varchar(30) Not Null,
		IdTipoMovto_Inv varchar(6) Not Null,
		IdMotivo varchar(3) Not Null, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table MovtosInv_Adt_Devoluciones Add Constraint PK_MovtosInv_Adt_Devoluciones
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, IdMotivo )	

	Alter Table MovtosInv_Adt_Devoluciones Add Constraint FK_MovtosInv_Adt_Devoluciones_MovtosInv_Enc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) References MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 

	Alter Table MovtosInv_Adt_Devoluciones Add Constraint FK_MovtosInv_Adt_Devoluciones_MovtosInv_Motivos_Dev
	Foreign Key ( IdTipoMovto_Inv, IdMotivo ) References MovtosInv_Motivos_Dev ( IdTipoMovto_Inv, IdMotivo ) 
End
Go--#SQL 

	
 