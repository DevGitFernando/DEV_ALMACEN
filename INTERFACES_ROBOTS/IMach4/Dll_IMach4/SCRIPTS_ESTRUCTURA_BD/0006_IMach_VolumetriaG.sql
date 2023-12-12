If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_VolumetriaG' and xType = 'U' ) 
   Drop Table IMach_VolumetriaG
Go--#SQL   

Create Table IMach_VolumetriaG
(
	CodigoEAN varchar(30) Not Null, 

    Peso Numeric(14,4) Not Null Default 0, 
    Ancho Numeric(14,4) Not Null Default 0, 
    Alto Numeric(14,4) Not Null Default 0,     
    Largo Numeric(14,4) Not Null Default 0,     

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL   

Alter Table IMach_VolumetriaG Add Constraint PK_IMach_VolumetriaG Primary Key ( CodigoEAN ) 
Go--#SQL 

