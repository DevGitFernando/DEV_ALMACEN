If Exists ( Select Name From Sysobjects Where Name = 'Padron_Puebla' and xType = 'U' )
      Drop Table Padron_Puebla
Go--#SQL

Create Table Padron_Puebla  
(
     Folio varchar(20) Not Null Default '',
     Consecutivo varchar(2) Not Null Default '',
          
     Nombre varchar(50) Not Null Default '',
     ApPaterno varchar(50) Not Null Default '',
     ApMaterno varchar(50) Not Null Default '',
     Sexo varchar(1) Not Null Default 'A',

     FechaNacimiento datetime Not Null Default '',
     FechaInicioVigencia datetime Not Null,
     FechaFinVigencia datetime Not Null, 
     EsVigente tinyint Not Null Default 0  
)
Go--#SQL
