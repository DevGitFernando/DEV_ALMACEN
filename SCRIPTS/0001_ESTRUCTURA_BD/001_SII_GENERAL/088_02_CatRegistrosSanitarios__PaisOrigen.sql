-----------------------------------------------------------------------------------------------------------------
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatRegistrosSanitarios_PaisFabricacion' and xType = 'U' ) 
Begin 
	Create Table CatRegistrosSanitarios_PaisFabricacion  
	( 
		IdPais varchar(3) Not Null, 
		NombrePais varchar(100) Not Null Default '' Unique, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)
	
	Alter Table CatRegistrosSanitarios_PaisFabricacion Add Constraint PK_CatRegistrosSanitarios_PaisFabricacion Primary Key ( IdPais )

End 
Go--#SQL 

If Not Exists ( Select * From CatRegistrosSanitarios_PaisFabricacion Where IdPais = '000' )  Insert Into CatRegistrosSanitarios_PaisFabricacion (  IdPais, NombrePais, Status, Actualizado )  Values ( '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update CatRegistrosSanitarios_PaisFabricacion Set NombrePais = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdPais = '000'  
Go--#SQL 

---		drop table CatRegistrosSanitarios_PaisFabricacion  
