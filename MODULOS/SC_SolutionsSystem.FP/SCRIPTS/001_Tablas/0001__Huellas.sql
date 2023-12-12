------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Dedos' and xType = 'U' ) 
   Drop Table FP_Dedos  
Go--#SQL 

Create Table FP_Dedos 
(
	Dedo smallint Not Null Default 0, 
	Descripcion varchar(100) Not Null Default '' 
) 
Go--#SQL 

Alter Table FP_Dedos Add Constraint PK_FP_Dedos Primary Key ( Dedo ) 
Go--#SQL 

Insert Into FP_Dedos Values (  1, 'Mano izquierda - Meñique' ) 
Insert Into FP_Dedos Values (  2, 'Mano izquierda - Anular' ) 
Insert Into FP_Dedos Values (  3, 'Mano izquierda - Medio' ) 
Insert Into FP_Dedos Values (  4, 'Mano izquierda - Indice' ) 
Insert Into FP_Dedos Values (  5, 'Mano izquierda - Pulgar' ) 

Insert Into FP_Dedos Values (  6, 'Mano derecha - Meñique' ) 
Insert Into FP_Dedos Values (  7, 'Mano derecha - Anular' ) 
Insert Into FP_Dedos Values (  8, 'Mano derecha - Medio' ) 
Insert Into FP_Dedos Values (  9, 'Mano derecha - Indice' ) 
Insert Into FP_Dedos Values ( 10, 'Mano derecha - Pulgar' ) 
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
	Dedo smallint Not Null Default 0, 
	Huella varchar(max) Not Null Default '', -- Unique , --- text, -- varbinary(max), 
	Status varchar(2) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table FP_Huellas Add Constraint PK_FP_Huellas Primary Key ( IdHuella )  -- ReferenciaHuella, Dedo ) 
Go--#SQL 



------------------------------------------------------------------------------------------------------------- 
--------------------------- Contendra las Huellas recientes para agilizar la lectura de huellas en el Checador 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Huellas__Recientes' and xType = 'U' ) 
   Drop Table FP_Huellas__Recientes  
Go--#SQL 
   
Create Table FP_Huellas__Recientes
( 
	IdHuella int Not Null Default 0, 
	FechaRegistro datetime Not Null Default getdate(), 
	ReferenciaHuella varchar(100) Not Null Default '', 
	NumDedo smallint Not Null Default 0, 
	Huella varchar(max) Not Null Default '', -- Unique , --- text, -- varbinary(max), 
	Status varchar(2) Not Null Default 'A', 
	
	FechaDescarga datetime Not Null Default getdate(), 
	FechaUltimaActualizacion datetime Not Null Default getdate() 
) 
Go--#SQL 


