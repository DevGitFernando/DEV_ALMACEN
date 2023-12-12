
-----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'CatCondicionesDeAlmacenamiento' and xType = 'U' )  
Begin 	

	Create Table CatCondicionesDeAlmacenamiento  
	(
		IdCondicion varchar(4) Not Null, 
		Descripcion varchar(200) Not Null Default '' unique, 
		Status varchar(1) Not Null Default '' 
	) 

	Alter Table CatCondicionesDeAlmacenamiento Add Constraint PK_CatCondicionesDeAlmacenamiento Primary Key ( IdCondicion ) 
End 
Go--#SQL 


If Not Exists ( Select * From CatCondicionesDeAlmacenamiento Where IdCondicion = '0001' )  Insert Into CatCondicionesDeAlmacenamiento (  IdCondicion, Descripcion, Status )  Values ( '0001', 'Mant�ngase a no mas de 30�C', 'A' ) 
 Else Update CatCondicionesDeAlmacenamiento Set Descripcion = 'Mant�ngase a no mas de 30�C', Status = 'A' Where IdCondicion = '0001'
If Not Exists ( Select * From CatCondicionesDeAlmacenamiento Where IdCondicion = '0002' )  Insert Into CatCondicionesDeAlmacenamiento (  IdCondicion, Descripcion, Status )  Values ( '0002', 'Mant�ngase a no mas de 25�C', 'A' ) 
 Else Update CatCondicionesDeAlmacenamiento Set Descripcion = 'Mant�ngase a no mas de 25�C', Status = 'A' Where IdCondicion = '0002'
If Not Exists ( Select * From CatCondicionesDeAlmacenamiento Where IdCondicion = '0003' )  Insert Into CatCondicionesDeAlmacenamiento (  IdCondicion, Descripcion, Status )  Values ( '0003', 'Mant�ngase en refrigeraci�n de 2�C a 8�C', 'A' ) 
 Else Update CatCondicionesDeAlmacenamiento Set Descripcion = 'Mant�ngase en refrigeraci�n de 2�C a 8�C', Status = 'A' Where IdCondicion = '0003'
If Not Exists ( Select * From CatCondicionesDeAlmacenamiento Where IdCondicion = '0004' )  Insert Into CatCondicionesDeAlmacenamiento (  IdCondicion, Descripcion, Status )  Values ( '0004', 'Mant�ngase en congelaci�n de -10�C a -25�C', 'A' ) 
 Else Update CatCondicionesDeAlmacenamiento Set Descripcion = 'Mant�ngase en congelaci�n de -10�C a -25�C', Status = 'A' Where IdCondicion = '0004'

Go--#SQL 


