Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_SESEQ__ProgramasAtencion' and xType = 'U' ) 
   Drop Table INT_SESEQ__ProgramasAtencion 
Go--#SQL   

Create Table INT_SESEQ__ProgramasAtencion 
(
	ClavePrograma varchar(10) Not Null, 
	Nombre varchar(100) Not Null Default '' UNIQUE, 
	Status varchar(1) Not Null Default 'A'  
)
Go--#SQL   

Alter Table INT_SESEQ__ProgramasAtencion 
	Add Constraint PK_INT_SESEQ__ProgramasAtencion Primary Key ( ClavePrograma ) 
Go--#SQL   


	insert into INT_SESEQ__ProgramasAtencion 
	select '0', 'PROGRAMA PREDETERMINADO', 'A' 


Go--#SQL 

