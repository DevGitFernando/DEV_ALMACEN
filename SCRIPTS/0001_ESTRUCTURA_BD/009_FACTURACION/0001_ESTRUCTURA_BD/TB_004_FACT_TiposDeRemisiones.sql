
------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_TiposDeRemisiones_Operacion'  and xType = 'U' ) 
   Drop Table FACT_TiposDeRemisiones_Operacion  
Go--#SQL   


------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_TiposDeRemisiones'  and xType = 'U' ) 
   Drop Table FACT_TiposDeRemisiones  
Go--#SQL    

Create Table FACT_TiposDeRemisiones
( 
	TipoDeRemision smallint Not Null Default 0,   
	Descripcion varchar(200) Not Null Default '' Unique, 
	TipoDeRemision_Relacionada smallint Not Null Default 0,   
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table FACT_TiposDeRemisiones Add Constraint PK_FACT_TiposDeRemisiones Primary Key ( TipoDeRemision )   
Go--#SQL 

---		sp_generainserts 'FACT_TiposDeRemisiones' ,  1 

----If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 1 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status )  Values ( 1, 'INSUMOS', 'A' )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'INSUMOS', Status = 'A' Where TipoDeRemision = 1  
----If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 2 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status )  Values ( 2, 'ADMINISTRACIÓN', 'A' )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'ADMINISTRACIÓN', Status = 'A' Where TipoDeRemision = 2  
----If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 3 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status )  Values ( 3, 'INSUMOS INCREMENTO', 'A' )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'INSUMOS INCREMENTO', Status = 'A' Where TipoDeRemision = 3  
----If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 4 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status )  Values ( 4, 'INSUMOS VENTA DIRECTA', 'A' )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'INSUMOS VENTA DIRECTA', Status = 'A' Where TipoDeRemision = 4  
----If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 5 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status )  Values ( 5, 'INSUMOS INCREMENTO VENTA DIRECTA', 'A' )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'INSUMOS INCREMENTO VENTA DIRECTA', Status = 'A' Where TipoDeRemision = 5  
----If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 6 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status )  Values ( 6, 'ADMINISTRACIÓN VENTA DIRECTA', 'A' )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'ADMINISTRACIÓN VENTA DIRECTA', Status = 'A' Where TipoDeRemision = 6  
----If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 7 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status )  Values ( 7, 'ADMINISTRACIÓN INCREMENTO', 'A' )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'ADMINISTRACIÓN INCREMENTO', Status = 'A' Where TipoDeRemision = 7  
----If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 8 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status )  Values ( 8, 'ADMINISTRACIÓN INCREMENTO VENTA DIRECTA', 'A' )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'ADMINISTRACIÓN INCREMENTO VENTA DIRECTA', Status = 'A' Where TipoDeRemision = 8  


If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 1 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status, TipoDeRemision_Relacionada )  Values ( 1, 'INSUMOS', 'A', 3 )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'INSUMOS', Status = 'A', TipoDeRemision_Relacionada = 3 Where TipoDeRemision = 1  
If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 2 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status, TipoDeRemision_Relacionada )  Values ( 2, 'ADMINISTRACIÓN', 'A', 7 )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'ADMINISTRACIÓN', Status = 'A', TipoDeRemision_Relacionada = 7 Where TipoDeRemision = 2  
If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 3 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status, TipoDeRemision_Relacionada )  Values ( 3, 'INSUMOS INCREMENTO', 'A', 1 )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'INSUMOS INCREMENTO', Status = 'A', TipoDeRemision_Relacionada = 1 Where TipoDeRemision = 3  
If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 4 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status, TipoDeRemision_Relacionada )  Values ( 4, 'INSUMOS VENTA DIRECTA', 'A', 5 )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'INSUMOS VENTA DIRECTA', Status = 'A', TipoDeRemision_Relacionada = 5 Where TipoDeRemision = 4  
If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 5 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status, TipoDeRemision_Relacionada )  Values ( 5, 'INSUMOS INCREMENTO VENTA DIRECTA', 'A', 4 )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'INSUMOS INCREMENTO VENTA DIRECTA', Status = 'A', TipoDeRemision_Relacionada = 4 Where TipoDeRemision = 5  
If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 6 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status, TipoDeRemision_Relacionada )  Values ( 6, 'ADMINISTRACIÓN VENTA DIRECTA', 'A', 8 )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'ADMINISTRACIÓN VENTA DIRECTA', Status = 'A', TipoDeRemision_Relacionada = 8 Where TipoDeRemision = 6  
If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 7 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status, TipoDeRemision_Relacionada )  Values ( 7, 'ADMINISTRACIÓN INCREMENTO', 'A', 3 )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'ADMINISTRACIÓN INCREMENTO', Status = 'A', TipoDeRemision_Relacionada = 3 Where TipoDeRemision = 7  
If Not Exists ( Select * From FACT_TiposDeRemisiones Where TipoDeRemision = 8 )  Insert Into FACT_TiposDeRemisiones (  TipoDeRemision, Descripcion, Status, TipoDeRemision_Relacionada )  Values ( 8, 'ADMINISTRACIÓN INCREMENTO VENTA DIRECTA', 'A', 6 )  Else Update FACT_TiposDeRemisiones Set Descripcion = 'ADMINISTRACIÓN INCREMENTO VENTA DIRECTA', Status = 'A', TipoDeRemision_Relacionada = 6 Where TipoDeRemision = 8  

Go--#SQL 



------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_TiposDeRemisiones_Operacion'  and xType = 'U' ) 
   Drop Table FACT_TiposDeRemisiones_Operacion  
Go--#SQL    

Create Table FACT_TiposDeRemisiones_Operacion
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	TipoDeRemision smallint Not Null Default 0,   
	Descripcion varchar(200) Not Null Default '' 
)
Go--#SQL    

Alter Table FACT_TiposDeRemisiones_Operacion Add Constraint PK_FACT_TiposDeRemisiones_Operacion Primary Key ( IdEmpresa, IdEstado, TipoDeRemision )   
Go--#SQL 

Alter Table FACT_TiposDeRemisiones_Operacion Add Constraint FK_FACT_TiposDeRemisiones_Operacion___FACT_TiposDeRemisiones  
	Foreign Key ( TipoDeRemision ) References FACT_TiposDeRemisiones ( TipoDeRemision )    
Go--#SQL 


