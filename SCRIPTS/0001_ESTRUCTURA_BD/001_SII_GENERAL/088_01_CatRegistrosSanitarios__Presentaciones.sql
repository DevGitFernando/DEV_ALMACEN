-----------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatRegistrosSanitarios_Presentaciones' and xType = 'U' ) 
Begin 
	Create Table CatRegistrosSanitarios_Presentaciones  
	( 
		IdPresentacion varchar(3) Not Null, 
		Descripcion varchar(100) Not Null Default '' Unique, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)
	
	Alter Table CatRegistrosSanitarios_Presentaciones Add Constraint PK_CatRegistrosSanitarios_Presentaciones Primary Key ( IdPresentacion )

End 
Go--#SQL 

If Not Exists ( Select * From CatRegistrosSanitarios_Presentaciones Where IdPresentacion = '000' )  Insert Into CatRegistrosSanitarios_Presentaciones (  IdPresentacion, Descripcion, Status, Actualizado )  Values ( '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update CatRegistrosSanitarios_Presentaciones Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdPresentacion = '000'   
If Not Exists ( Select * From CatRegistrosSanitarios_Presentaciones Where IdPresentacion = '001' )  Insert Into CatRegistrosSanitarios_Presentaciones (  IdPresentacion, Descripcion, Status, Actualizado )  Values ( '001', 'FRASCO', 'A', 0 )    Else Update CatRegistrosSanitarios_Presentaciones Set Descripcion = 'FRASCO', Status = 'A', Actualizado = 0 Where IdPresentacion = '001'  
If Not Exists ( Select * From CatRegistrosSanitarios_Presentaciones Where IdPresentacion = '002' )  Insert Into CatRegistrosSanitarios_Presentaciones (  IdPresentacion, Descripcion, Status, Actualizado )  Values ( '002', 'SOBRE', 'A', 0 )    Else Update CatRegistrosSanitarios_Presentaciones Set Descripcion = 'SOBRE', Status = 'A', Actualizado = 0 Where IdPresentacion = '002'  
If Not Exists ( Select * From CatRegistrosSanitarios_Presentaciones Where IdPresentacion = '003' )  Insert Into CatRegistrosSanitarios_Presentaciones (  IdPresentacion, Descripcion, Status, Actualizado )  Values ( '003', 'BLISTER', 'A', 0 )    Else Update CatRegistrosSanitarios_Presentaciones Set Descripcion = 'BLISTER', Status = 'A', Actualizado = 0 Where IdPresentacion = '003'  
Go--#SQL 

---		drop table CatRegistrosSanitarios_Presentaciones  


---		sp_generainserts CatRegistrosSanitarios_Presentaciones ,  1  

