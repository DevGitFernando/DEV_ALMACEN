
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'REC_CatMedicos' and xType = 'U' ) 
   Drop Table REC_CatMedicos 
Go--#SQL  

Create Table REC_CatMedicos 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 	
	IdMedico varchar(6) Not Null, 
	Nombre varchar(50) Not Null, 
	ApPaterno varchar(50) Not Null, 
	ApMaterno varchar(50) Not Null, 
	NumCedula varchar(30) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table REC_CatMedicos Add Constraint PK_REC_CatMedicos Primary Key ( IdEstado, IdFarmacia, IdMedico )  
Go--#SQL  

Alter Table REC_CatMedicos Add Constraint FK_REC_CatMedicos_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  
