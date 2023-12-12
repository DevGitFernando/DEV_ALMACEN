If Exists ( Select * From sysobjects (NoLock) Where Name = 'CNT_Cheque' and xType = 'U'  )
   Drop Table CNT_Cheque
Go--#SQL

Create Table CNT_Cheque
(
	IdEmpresa Varchar(3) Not Null,
	IdEstado Varchar(2) Not Null,
	IdCheque varchar(6) Not Null,
	Descripcion varchar(500) Not Null,
	IdChequera varchar(6) Not Null,
	FolioCheque Int Not Null,
	IdBeneficiario varchar(6) Not Null,
	Cantidad numeric(14,4) Not Null,
	FechaRegistro datetime Not Null Default getdate(),
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL

Alter Table CNT_Cheque Add Constraint PK_CNT_Cheque
	Primary Key ( IdEmpresa,  IdEstado, IdCheque)
Go--#SQL

Alter Table CNT_Cheque Add Constraint FK_CNT_Cheque__CatEmpresas
	Foreign Key ( IdEmpresa ) References CatEmpresas
Go--#SQL

Alter Table CNT_Cheque Add Constraint FK_CNT_Cheque__CatEstados
	Foreign Key ( IdEstado ) References CatEstados
Go--#SQL

Alter Table CNT_Cheque Add Constraint FK_CNT_Cheque__CNT_CatChequeras
	Foreign Key ( IdEmpresa, IdEstado, IdChequera ) References CNT_CatChequeras
Go--#SQL

Alter Table CNT_Cheque Add Constraint FK_CNT_Cheque__CNT_CatChequesBeneficiarios
	Foreign Key ( IdBeneficiario ) References CNT_CatChequesBeneficiarios
Go--#SQL