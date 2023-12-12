------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatParentescos' and xType = 'U' ) 
Begin 
	Create Table CatParentescos 
	(
		Folio Varchar(2) not null, 
		Descripcion varchar(100) Not Null Default '',
		Status varchar(2) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0	
	)

	Alter Table CatParentescos Add Constraint PK_CatParentescos Primary Key ( Folio ) 
End 
Go--#SQL


----insert into CatParentescos ( Folio, Descripcion, Status, Actualizado ) select '01', 'TITULAR', 'A', 0 
----insert into CatParentescos ( Folio, Descripcion, Status, Actualizado ) select '02', 'ESPOSO(A)', 'A', 0 
----insert into CatParentescos ( Folio, Descripcion, Status, Actualizado ) select '03', 'PAPÁ', 'A', 0 
----insert into CatParentescos ( Folio, Descripcion, Status, Actualizado ) select '04', 'MAMÁ', 'A', 0 
----insert into CatParentescos ( Folio, Descripcion, Status, Actualizado ) select '05', 'HERMANO(A)', 'A', 0 
----insert into CatParentescos ( Folio, Descripcion, Status, Actualizado ) select '06', 'HIJO(A)', 'A', 0 
----insert into CatParentescos ( Folio, Descripcion, Status, Actualizado ) select '07', 'OTROS', 'A', 0 

If Not Exists ( Select * From CatParentescos Where Folio = '01' )  Insert Into CatParentescos (  Folio, Descripcion, Status, Actualizado )  Values ( '01', 'TITULAR', 'A', 0 )    Else Update CatParentescos Set Descripcion = 'TITULAR', Status = 'A', Actualizado = 0 Where Folio = '01'  
If Not Exists ( Select * From CatParentescos Where Folio = '02' )  Insert Into CatParentescos (  Folio, Descripcion, Status, Actualizado )  Values ( '02', 'ESPOSO(A)', 'A', 0 )    Else Update CatParentescos Set Descripcion = 'ESPOSO(A)', Status = 'A', Actualizado = 0 Where Folio = '02'  
If Not Exists ( Select * From CatParentescos Where Folio = '03' )  Insert Into CatParentescos (  Folio, Descripcion, Status, Actualizado )  Values ( '03', 'PAPÁ', 'A', 0 )    Else Update CatParentescos Set Descripcion = 'PAPÁ', Status = 'A', Actualizado = 0 Where Folio = '03'  
If Not Exists ( Select * From CatParentescos Where Folio = '04' )  Insert Into CatParentescos (  Folio, Descripcion, Status, Actualizado )  Values ( '04', 'MAMÁ', 'A', 0 )    Else Update CatParentescos Set Descripcion = 'MAMÁ', Status = 'A', Actualizado = 0 Where Folio = '04'  
If Not Exists ( Select * From CatParentescos Where Folio = '05' )  Insert Into CatParentescos (  Folio, Descripcion, Status, Actualizado )  Values ( '05', 'HERMANO(A)', 'A', 0 )    Else Update CatParentescos Set Descripcion = 'HERMANO(A)', Status = 'A', Actualizado = 0 Where Folio = '05'  
If Not Exists ( Select * From CatParentescos Where Folio = '06' )  Insert Into CatParentescos (  Folio, Descripcion, Status, Actualizado )  Values ( '06', 'HIJO(A)', 'A', 0 )    Else Update CatParentescos Set Descripcion = 'HIJO(A)', Status = 'A', Actualizado = 0 Where Folio = '06'  
If Not Exists ( Select * From CatParentescos Where Folio = '07' )  Insert Into CatParentescos (  Folio, Descripcion, Status, Actualizado )  Values ( '07', 'OTROS', 'A', 0 )    Else Update CatParentescos Set Descripcion = 'OTROS', Status = 'A', Actualizado = 0 Where Folio = '07'  

Go--#SQL 


