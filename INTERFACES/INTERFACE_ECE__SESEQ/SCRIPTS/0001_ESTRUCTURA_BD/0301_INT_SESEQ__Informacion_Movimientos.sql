Set NoCount On 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_SESEQ__Informacion_Movimientos' and xType = 'U' ) 
   Drop Table INT_SESEQ__Informacion_Movimientos  
Go--#SQL   

Create Table INT_SESEQ__Informacion_Movimientos 
( 
	Tipo smallint Not Null Default 0, 
	Nombre varchar(200) Not Null Default '', 
	Descripcion varchar(200) Not Null Default '', 
	Efecto varchar(1) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_SESEQ__Informacion_Movimientos 
	Add Constraint PK_INT_SESEQ__Informacion_Movimientos Primary Key ( Tipo ) 
Go--#SQL   


--	sp_generainserts 'INT_SESEQ__Informacion_Movimientos' , 1 

If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 1 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 1, 'Entrada', 'Entradas de Consignacion', 'E' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Entrada', Descripcion = 'Entradas de Consignacion', Efecto = 'E' Where Tipo = 1  
If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 2 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 2, 'Transferencia de salida', 'Transferencia entre Unidades', 'S' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Transferencia de salida', Descripcion = 'Transferencia entre Unidades', Efecto = 'S' Where Tipo = 2  
If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 3 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 3, 'Salida Centro de Salud', 'Ventas directas de CEDIS', 'S' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Salida Centro de Salud', Descripcion = 'Ventas directas de CEDIS', Efecto = 'S' Where Tipo = 3  
If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 4 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 4, 'Salida Receta Electronica', 'Dispensación', 'S' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Salida Receta Electronica', Descripcion = 'Dispensación', Efecto = 'S' Where Tipo = 4  
If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 5 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 5, 'Salida Colectivo', 'Dispensación', 'S' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Salida Colectivo', Descripcion = 'Dispensación', Efecto = 'S' Where Tipo = 5  
If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 6 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 6, 'Transferencia de Entrada', 'Transferencia entre Unidades', 'E' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Transferencia de Entrada', Descripcion = 'Transferencia entre Unidades', Efecto = 'E' Where Tipo = 6  
If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 7 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 7, 'Devoluciones', 'Devoluciones de Ventas Directas y/o Dispensación', 'E' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Devoluciones', Descripcion = 'Devoluciones de Ventas Directas y/o Dispensación', Efecto = 'E' Where Tipo = 7  
If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 8 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 8, 'Ajustes positivos', 'Movimientos especiales de entrada', 'E' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Ajustes positivos', Descripcion = 'Movimientos especiales de entrada', Efecto = 'E' Where Tipo = 8  
If Not Exists ( Select * From INT_SESEQ__Informacion_Movimientos Where Tipo = 9 )  Insert Into INT_SESEQ__Informacion_Movimientos (  Tipo, Nombre, Descripcion, Efecto )  Values ( 9, 'Ajustes negativos', 'Movimientos especiales de salida', 'S' )  Else Update INT_SESEQ__Informacion_Movimientos Set Nombre = 'Ajustes negativos', Descripcion = 'Movimientos especiales de salida', Efecto = 'S' Where Tipo = 9  

Go--#SQL   

