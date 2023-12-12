


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rec_CatBeneficiarios' and xType = 'U' ) 
   Drop Table Rec_CatBeneficiarios 
Go--#SQL 

Create Table Rec_CatBeneficiarios 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdBeneficiario varchar(8) Not Null, --- Consecutivo interno 
	Nombre varchar(50) Not Null, 
	ApPaterno varchar(50) Not Null, 
	ApMaterno varchar(50) Not Null, 
	Sexo varchar(1) Not Null Default '', 
	FechaNacimiento datetime Not Null Default getdate(), 
	FolioReferencia varchar(20) Not Null Default '',    --- Folio : Num. Poliza seguro, Num. Empleado, Num. Placa Policia, etc, es el folio de referencia proporcionado por la institucion que respalda al Beneficiario. 
	
	FechaInicioVigencia datetime Not Null Default getdate(),
	FechaFinVigencia datetime Not Null Default getdate(),  --- Fecha de vigencia de la cobertura 
	
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table Rec_CatBeneficiarios Add Constraint PK_Rec_CatBeneficiarios Primary Key ( IdEstado, IdFarmacia, IdBeneficiario ) 
Go--#SQL  