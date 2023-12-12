If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMotivosSobrePrecio' and xType = 'U' ) 
   Drop Table CatMotivosSobrePrecio
Go--#SQL  

Create Table CatMotivosSobrePrecio
( 
	Folio Varchar(4) Not Null,
	Descripcion  varchar(100) Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL  

Alter Table CatMotivosSobrePrecio Add Constraint PK_CatMotivosSobrePrecio
	Primary Key ( Folio )
Go--#SQL