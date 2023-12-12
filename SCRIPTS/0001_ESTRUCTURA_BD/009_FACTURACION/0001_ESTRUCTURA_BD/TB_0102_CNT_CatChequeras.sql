If Exists ( Select * From sysobjects (NoLock) Where Name = 'CNT_CatChequeras' and xType = 'U'  )
   Drop Table CNT_CatChequeras
Go--#SQL

Create Table CNT_CatChequeras
(
	IdEmpresa Varchar(3) Not Null,
	IdEstado Varchar(2) Not Null,
	IdChequera varchar(6) Not Null,
	Descripcion varchar(500) Not Null,
	IdBanco varchar(3) Not Null,
	FolioInicio Int Not Null,
	FolioFin Int Not Null,
	NumeroDeSerie Varchar(20) Not Null,
	UltimoFolio Varchar(6),
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL

Alter Table CNT_CatChequeras Add Constraint PK_CNT_CatChequeras
	Primary Key ( IdEmpresa, IdEstado, IdChequera )
Go--#SQL

Alter Table CNT_CatChequeras Add Constraint FK_CNT_CatChequeras__CatEmpresas
	Foreign Key ( IdEmpresa ) References CatEmpresas
Go--#SQL

Alter Table CNT_CatChequeras Add Constraint FK_CNT_CatChequeras__CatEstados
	Foreign Key ( IdEstado ) References CatEstados
Go--#SQL

Alter Table CNT_CatChequeras Add Constraint FK_CNT_CatChequeras__CNT_CatBancos
	Foreign Key ( IdBanco ) References CNT_CatBancos    
Go--#SQL