If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Huellas' and xType = 'U' ) 
Begin 
	Create Table Vales_Huellas
	(
		IdEstado Varchar(2) Not Null,
		IdFarmacia Varchar(4) Not Null,
		IdPersonaFirma varchar(8) Not Null Default '', 
		GUID varchar(100) Not Null Default '', 
		Nombre varchar(100) Not Null Default '',
		ApPaterno Varchar(100) Not Null Default '',
		ApMaterno Varchar(100) Not Null Default '',
		EsPersonalFarmacia bit Default 0,
		Parentesco Varchar(2),
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0  
	)

	Alter Table Vales_Huellas Add Constraint PK_Vales_Huellas 
		Primary Key ( IdEstado, IdFarmacia, IdPersonaFirma ) 

	Alter Table Vales_Huellas Add Constraint FK_Vales_Huellas_CatParentescos
		Foreign Key ( Parentesco ) References CatParentescos

End 
Go--#SQL


