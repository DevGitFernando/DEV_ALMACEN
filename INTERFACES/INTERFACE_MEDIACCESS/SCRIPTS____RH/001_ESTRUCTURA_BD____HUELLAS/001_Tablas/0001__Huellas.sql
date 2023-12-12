------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Huellas' and xType = 'U' ) 
   Drop Table FP_Huellas 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Manos_Dedos' and xType = 'U' ) 
   Drop Table FP_Manos_Dedos 
Go--#SQL 

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Dedos' and xType = 'U' ) 
   Drop Table FP_Dedos  
Go--#SQL 



------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Manos' and xType = 'U' ) 
   Drop Table FP_Manos  
Go--#SQL 

Create Table FP_Manos 
(
	Mano smallint not null Default 0, 
	Descripcion varchar(100) Not Null Default '', 
	Actualizado tinyint Not Null Default 0	
)
Go--#SQL 
	
Alter Table FP_Manos Add Constraint PK_FP_Manos Primary Key ( Mano ) 
Go--#SQL 

Insert Into FP_Manos Values (  1, 'Mano izquierda' ) 
Insert Into FP_Manos Values (  2, 'Mano derecha' ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Dedos' and xType = 'U' ) 
   Drop Table FP_Dedos  
Go--#SQL 

Create Table FP_Dedos 
(
	Dedo smallint Not Null Default 0, 
	Descripcion varchar(100) Not Null Default '',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table FP_Dedos Add Constraint PK_FP_Dedos Primary Key ( Dedo ) 
Go--#SQL 

Insert Into FP_Dedos Values (  1, 'Meñique' ) 
Insert Into FP_Dedos Values (  2, 'Anular' ) 
Insert Into FP_Dedos Values (  3, 'Medio' ) 
Insert Into FP_Dedos Values (  4, 'Indice' ) 
Insert Into FP_Dedos Values (  5, 'Pulgar' ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Manos_Dedos' and xType = 'U' ) 
   Drop Table FP_Manos_Dedos 
Go--#SQL 

Create Table FP_Manos_Dedos 
(
	NumDedo smallint Not Null Default 0, 
	Mano smallint Not Null Default 0,	
	Dedo smallint Not Null Default 0,
	Actualizado tinyint Not Null Default 0
)
Go--#SQL 	

Alter Table FP_Manos_Dedos Add Constraint PK_FP_Manos_Dedos Primary Key ( NumDedo ) 
Go--#SQL 

Alter Table FP_Manos_Dedos Add Constraint FK_FP_Manos_Dedos__FP_Manos 
	Foreign Key ( Mano ) References FP_Manos ( Mano ) 
Go--#SQL 

Alter Table FP_Manos_Dedos Add Constraint FK_FP_Manos_Dedos__FP_Dedos 
	Foreign Key ( Dedo ) References FP_Dedos ( Dedo ) 
Go--#SQL 


Insert Into FP_Manos_Dedos Values (  1, 1, 1  ) 
Insert Into FP_Manos_Dedos Values (  2, 1, 2  ) 
Insert Into FP_Manos_Dedos Values (  3, 1, 3  ) 
Insert Into FP_Manos_Dedos Values (  4, 1, 4  ) 
Insert Into FP_Manos_Dedos Values (  5, 1, 5  ) 
Insert Into FP_Manos_Dedos Values (  6, 2, 1  ) 
Insert Into FP_Manos_Dedos Values (  7, 2, 2  ) 
Insert Into FP_Manos_Dedos Values (  8, 2, 3  ) 
Insert Into FP_Manos_Dedos Values (  9, 2, 4  ) 
Insert Into FP_Manos_Dedos Values ( 10, 2, 5  ) 
Go--#SQL 

------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Huellas' and xType = 'U' ) 
   Drop Table FP_Huellas 
Go--#SQL 
   
Create Table FP_Huellas 
( 
	IdHuella int Identity(1,1), 
	FechaRegistro datetime Not Null Default getdate(), 
	ReferenciaHuella varchar(100) Not Null Default '', 
	NumDedo smallint Not Null Default 0, 
	Huella varchar(max) Not Null Default '', -- Unique , --- text, -- varbinary(max), 
	Status varchar(2) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table FP_Huellas Add Constraint PK_FP_Huellas Primary Key ( ReferenciaHuella, NumDedo ) 
Go--#SQL 


Alter Table FP_Huellas Add Constraint FK_FP_Huellas___FP_Manos_Dedos 
	Foreign Key ( NumDedo ) References FP_Manos_Dedos ( NumDedo ) 
Go--#SQL 

