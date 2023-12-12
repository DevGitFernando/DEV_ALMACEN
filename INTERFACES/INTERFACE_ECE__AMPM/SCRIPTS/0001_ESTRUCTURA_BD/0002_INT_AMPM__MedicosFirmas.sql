------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__MedicosFirmas' and xType = 'U' )  
   Drop Table INT_AMPM__MedicosFirmas 
Go--#SQL   

Create Table INT_AMPM__MedicosFirmas 
( 
	NombreFirma varchar(200) Not Null Default '', 
	FirmaDigital varchar(max) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate(), 
	FechaActualizacion datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_AMPM__MedicosFirmas Add Constraint PK_INT_AMPM__MedicosFirmas Primary Key ( NombreFirma ) 
Go--#SQL   


