If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AIE_ADT_Accesos' and xType = 'U' )
   Drop Table AIE_ADT_Accesos 
Go--#SQL   
 
----------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AIE_ADT_Accesos' and xType = 'U' )
   Drop Table AIE_ADT_Accesos 
Go--#SQL   

Create Table AIE_ADT_Accesos 
(
	IdAccesoExterno varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(),  
	Sentencia varchar(500) Not Null Default '', 

	MAC varchar(20) Not Null Default '', 
	NombreHost varchar(100) Not Null Default '', 
	Keyx int identity  	
)
Go--#SQL

Alter Table AIE_ADT_Accesos Add Constraint PK_AIE_ADT_Accesos Primary Key ( IdAccesoExterno, FechaRegistro ) 
Go--#SQL 

