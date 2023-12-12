--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatFormasDePago' and xType = 'U' ) 
   Drop Table CatFormasDePago  
Go--#SQL   

Create Table CatFormasDePago   
( 
	IdFormasDePago varchar(2) Not Null Default '',  
	Descripcion varchar(100) Not Null Default '',
	PermiteDuplicidad bit Not Null Default 0,
	Status varchar(1) Not Null Default 'A'  	
) 
Go--#SQL   

Alter Table CatFormasDePago Add Constraint PK_CatFormasDePago Primary Key ( IdFormasDePago ) 
Go--#SQL

--SP_GeneraInserts CatFormasDePago, 1
Set DateFormat YMD
Go--#SQL
If Not Exists ( Select * From CatFormasDePago Where IdFormasDePago = '01' )  Insert Into CatFormasDePago (  IdFormasDePago, Descripcion, PermiteDuplicidad, Status )  Values ( '01', 'EFECTIVO', 0, 'A' )    Else Update CatFormasDePago Set Descripcion = 'EFECTIVO', PermiteDuplicidad = 0, Status = 'A' Where IdFormasDePago = '01'  
If Not Exists ( Select * From CatFormasDePago Where IdFormasDePago = '02' )  Insert Into CatFormasDePago (  IdFormasDePago, Descripcion, PermiteDuplicidad, Status )  Values ( '02', 'TARJETA', 1, 'A' )    Else Update CatFormasDePago Set Descripcion = 'TARJETA', PermiteDuplicidad = 1, Status = 'A' Where IdFormasDePago = '02'
Go--#SQL