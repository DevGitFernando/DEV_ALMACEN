
-------------------------------------------------------------------------------------------------------------------------------------------
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Inv_ConteosCiclicos_Claves' and xType = 'U' )
----	Drop Table  Inv_ConteosCiclicos_Claves
----Go--#xxxSQL

-------------------------------------------------------------------------------------------------------------------------------------------
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Inv_ConteosCiclicos_Resumen' and xType = 'U' )
----	Drop Table  Inv_ConteosCiclicos_Resumen
----Go--#xxxSQL

-------------------------------------------------------------------------------------------------------------------------------------------
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Inv_ConteosCiclicosDet' and xType = 'U' )
----	Drop Table  Inv_ConteosCiclicosDet
----Go--#xxxSQL	


---------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Inv_ConteosCiclicosEnc' and xType = 'U' )
Begin  
	Create Table Inv_ConteosCiclicosEnc
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		FolioConteo varchar(30) Not Null,
		IdPersonal varchar(4) Not Null,
		FechaRegistro datetime Not Null Default getdate(),
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table Inv_ConteosCiclicosEnc Add Constraint PK_Inv_ConteosCiclicosEnc
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo )	 

End 	
Go--#SQL


--------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Inv_ConteosCiclicosDet' and xType = 'U' )
Begin  
	Create Table Inv_ConteosCiclicosDet
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		FolioConteo varchar(30) Not Null,
		ClaveSSA varchar(30) Not Null,
		Cantidad int Not Null Default 0,
		Total_Piezas int Not Null Default 0,
		Participacion numeric(14, 4) Not Null Default 0,
		Categoria varchar(3) Not Null Default '',
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table Inv_ConteosCiclicosDet Add Constraint PK_Inv_ConteosCiclicosDet
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo, ClaveSSA )	

	Alter Table Inv_ConteosCiclicosDet Add Constraint FK_Inv_ConteosCiclicosDet_Inv_ConteosCiclicosEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo ) References Inv_ConteosCiclicosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo ) 

End 
Go--#SQL 

	
--------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Inv_ConteosCiclicos_Resumen' and xType = 'U' )
Begin  
	Create Table Inv_ConteosCiclicos_Resumen
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		FolioConteo varchar(30) Not Null,
		Categoria varchar(3) Not Null,
		Claves int Not Null Default 0,
		Frecuencia int Not Null Default 0,
		Total_Claves int Not Null Default 0,
		Porc_Conteo numeric(14, 4) Not Null Default 0,
		Conteos int Not Null Default 0,
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table Inv_ConteosCiclicos_Resumen Add Constraint PK_Inv_ConteosCiclicos_Resumen
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo, Categoria )	

	Alter Table Inv_ConteosCiclicos_Resumen Add Constraint FK_Inv_ConteosCiclicos_Resumen_Inv_ConteosCiclicosEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo ) References Inv_ConteosCiclicosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo ) 

End 
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Inv_ConteosCiclicos_Claves' and xType = 'U' )
Begin  
	Create Table Inv_ConteosCiclicos_Claves
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		FolioConteo varchar(30) Not Null,
		ClaveSSA varchar(30) Not Null,
		FechaRegistro datetime Not Null Default getdate(),
		Categoria varchar(3) Not Null Default '',
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table Inv_ConteosCiclicos_Claves Add Constraint PK_Inv_ConteosCiclicos_Claves
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo, ClaveSSA )	

	Alter Table Inv_ConteosCiclicos_Claves Add Constraint FK_Inv_ConteosCiclicos_Claves_Inv_ConteosCiclicosDet
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo, ClaveSSA ) 
		References Inv_ConteosCiclicosDet ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo, ClaveSSA ) 
	
End 	
Go--#SQL 
	
---------------------------------------------------------------------------------------------------------------------------------------
	

		