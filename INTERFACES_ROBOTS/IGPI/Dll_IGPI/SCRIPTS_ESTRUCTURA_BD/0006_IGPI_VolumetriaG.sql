If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IGPI_VolumetriaG' and xType = 'U' ) 
   Drop Table IGPI_VolumetriaG
Go--#SQL   

Create Table IGPI_VolumetriaG
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

Alter Table IGPI_VolumetriaG Add Constraint PK_IGPI_VolumetriaG Primary Key ( CodigoEAN ) 
Go--#SQL 

