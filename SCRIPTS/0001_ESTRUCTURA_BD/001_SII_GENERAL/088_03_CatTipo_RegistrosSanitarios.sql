----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatTipo_RegistrosSanitarios' and xType = 'U' ) 
   Drop Table CatTipo_RegistrosSanitarios 
Go--#SQL   

Create Table CatTipo_RegistrosSanitarios 
(
	IdTipos varchar(2) Not Null, 
	Descripcion varchar(500) Not Null Default '',
	TipoCaduca bit Not Null Default 1,
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL 


Alter Table CatTipo_RegistrosSanitarios Add Constraint PK_CatTipo_RegistrosSanitarios Primary Key ( IdTipos ) 
Go--#SQL  



	--Insert Into CatTipo_RegistrosSanitarios
 --   Select 0 as Status, 'Sin especificar' as Descripcion, 1, 'A', 0 
 --   Union
 --   Select 1 as Status, 'Registro Sanitario' as Descripcion, 1, 'A', 0 -- 'Vigente' as Descripcion 
 --   Union
 --   Select 2 as Status, 'Tramite' as Descripcion, 1, 'A', 0 -- 'Renovacion' as Descripcion 
 --   Union
 --   Select 3 as Status, 'Prorroga' as Descripcion, 1, 'A', 0 
 --   Union
 --   Select 4 as Status, 'Revocada' as Descripcion, 1, 'A', 0
 --   Union
 --   Select 5 as Status, 'Diario Oficial' as Descripcion, 0, 'A', 0
 --   Union
 --   Select 6 as Status, 'Oficio' as Descripcion, 0, 'A', 0
 --	  Union
	--Select 7 as Status, 'Modificacion Registro' as Descripcion, 0, 'A', 0