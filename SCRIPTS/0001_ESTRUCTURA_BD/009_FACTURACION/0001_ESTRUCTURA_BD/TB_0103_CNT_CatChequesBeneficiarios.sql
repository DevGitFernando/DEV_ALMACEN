If Exists ( Select * From sysobjects (NoLock) Where Name = 'CNT_CatChequesBeneficiarios' and xType = 'U'  )
   Drop Table CNT_CatChequesBeneficiarios
Go--#SQL

Create Table CNT_CatChequesBeneficiarios
(
	IdBeneficiario Varchar(6) Not Null,
	Descripcion Varchar(500) Not Null,
	RFC Varchar(13) Not Null,
	Telefono Varchar(13) Not Null,
	Estado Varchar(30) Not Null,
	Municipio Varchar(50) Not Null,
	Direccion Varchar(500) Not Null,
	CP Varchar(10) Not Null,
	EsMoral Bit Not Null Default 0, 
	Status Varchar(1) Not Null Default 'A',
	Actualizado Tinyint Not Null Default 0
)
Go--#SQL

Alter Table CNT_CatChequesBeneficiarios Add Constraint PK_CNT_CatChequesBeneficiarios
	Primary Key ( IdBeneficiario )
Go--#SQL