
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


