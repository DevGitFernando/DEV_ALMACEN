-------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'FACT_CFD_MetodosPago' and So.xType = 'U' ) 
   Drop Table FACT_CFD_MetodosPago 
Go--#SQL 

Create Table FACT_CFD_MetodosPago 
( 
    IdMetodoPago varchar(6) Not Null, 
    Descripcion varchar(100) Not Null Default '',    
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table FACT_CFD_MetodosPago Add Constraint PK_FACT_CFD_MetodosPago Primary Key ( IdMetodoPago )     
Go--#SQL 

-------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'FACT_CFD_FormasDePago' and So.xType = 'U' ) 
   Drop Table FACT_CFD_FormasDePago 
Go--#SQL 

Create Table FACT_CFD_FormasDePago 
( 
    IdCondicionPago varchar(6) Not Null, 
    Descripcion varchar(100) Not Null Default '',    
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table FACT_CFD_FormasDePago Add Constraint PK_FACT_CFD_FormasDePago Primary Key ( IdCondicionPago )     
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'FACT_CFD_UnidadesDeMedida' and So.xType = 'U' ) 
   Drop Table FACT_CFD_UnidadesDeMedida 
Go--#SQL 

Create Table FACT_CFD_UnidadesDeMedida 
( 
    IdUnidad varchar(3) Not Null, 
    Descripcion varchar(100) Not Null Default '' Unique,    
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table FACT_CFD_UnidadesDeMedida Add Constraint PK_FACT_CFD_UnidadesDeMedida Primary Key ( IdUnidad )     
Go--#SQL 


If Not Exists ( Select * From FACT_CFD_MetodosPago Where IdMetodoPago = '01' )  Insert Into FACT_CFD_MetodosPago (  IdMetodoPago, Descripcion, Status, Actualizado )  Values ( '01', 'Efectivo', 'A', 0 )    Else Update FACT_CFD_MetodosPago Set Descripcion = 'Efectivo', Status = 'A', Actualizado = 0 Where IdMetodoPago = '01'  
If Not Exists ( Select * From FACT_CFD_MetodosPago Where IdMetodoPago = '02' )  Insert Into FACT_CFD_MetodosPago (  IdMetodoPago, Descripcion, Status, Actualizado )  Values ( '02', 'Tarjeta de crédito', 'A', 0 )    Else Update FACT_CFD_MetodosPago Set Descripcion = 'Tarjeta de crédito', Status = 'A', Actualizado = 0 Where IdMetodoPago = '02'  
If Not Exists ( Select * From FACT_CFD_MetodosPago Where IdMetodoPago = '03' )  Insert Into FACT_CFD_MetodosPago (  IdMetodoPago, Descripcion, Status, Actualizado )  Values ( '03', 'Vales', 'A', 0 )    Else Update FACT_CFD_MetodosPago Set Descripcion = 'Vales', Status = 'A', Actualizado = 0 Where IdMetodoPago = '03'  
Go--#SQL 

If Not Exists ( Select * From FACT_CFD_FormasDePago Where IdFormaDePago = '01' )  Insert Into FACT_CFD_FormasDePago (  IdFormaDePago, Descripcion, Status, Actualizado )  Values ( '01', 'En una sola exhibición', 'A', 0 )    Else Update FACT_CFD_FormasDePago Set Descripcion = 'En una sola exhibición', Status = 'A', Actualizado = 0 Where IdFormaDePago = '01'  
If Not Exists ( Select * From FACT_CFD_FormasDePago Where IdFormaDePago = '02' )  Insert Into FACT_CFD_FormasDePago (  IdFormaDePago, Descripcion, Status, Actualizado )  Values ( '02', 'En parcialidades', 'A', 0 )    Else Update FACT_CFD_FormasDePago Set Descripcion = 'En parcialidades', Status = 'A', Actualizado = 0 Where IdFormaDePago = '02'  
Go--#SQL 

If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '000' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '000', 'NO APLICA', 'A', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'NO APLICA', Status = 'A', Actualizado = 0 Where IdUnidad = '000'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '001' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '001', 'PIEZA', 'A', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'PIEZA', Status = 'A', Actualizado = 0 Where IdUnidad = '001'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '002' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '002', 'CAJA', 'A', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'CAJA', Status = 'A', Actualizado = 0 Where IdUnidad = '002'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '003' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '003', 'METRO LINEAL', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'METRO LINEAL', Status = 'C', Actualizado = 0 Where IdUnidad = '003'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '004' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '004', 'METRO CUADRADO', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'METRO CUADRADO', Status = 'C', Actualizado = 0 Where IdUnidad = '004'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '005' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '005', 'METRO CUBICO', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'METRO CUBICO', Status = 'C', Actualizado = 0 Where IdUnidad = '005'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '006' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '006', 'KILO', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'KILO', Status = 'C', Actualizado = 0 Where IdUnidad = '006'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '007' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '007', 'CABEZA', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'CABEZA', Status = 'C', Actualizado = 0 Where IdUnidad = '007'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '008' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '008', 'LITRO', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'LITRO', Status = 'C', Actualizado = 0 Where IdUnidad = '008'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '009' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '009', 'PAR', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'PAR', Status = 'C', Actualizado = 0 Where IdUnidad = '009'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '010' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '010', 'KILOWAT', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'KILOWAT', Status = 'C', Actualizado = 0 Where IdUnidad = '010'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '011' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '011', 'MILLAR', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'MILLAR', Status = 'C', Actualizado = 0 Where IdUnidad = '011'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '012' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '012', 'JUEGO', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'JUEGO', Status = 'C', Actualizado = 0 Where IdUnidad = '012'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '013' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '013', 'KILOWAT/HORA', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'KILOWAT/HORA', Status = 'C', Actualizado = 0 Where IdUnidad = '013'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '014' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '014', 'TONELADA', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'TONELADA', Status = 'C', Actualizado = 0 Where IdUnidad = '014'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '015' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '015', 'BARRIL', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'BARRIL', Status = 'C', Actualizado = 0 Where IdUnidad = '015'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '016' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '016', 'GRAMO NETO', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'GRAMO NETO', Status = 'C', Actualizado = 0 Where IdUnidad = '016'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '017' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '017', 'DECENAS', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'DECENAS', Status = 'C', Actualizado = 0 Where IdUnidad = '017'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '018' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '018', 'CIENTOS', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'CIENTOS', Status = 'C', Actualizado = 0 Where IdUnidad = '018'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '019' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '019', 'DOCENAS', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'DOCENAS', Status = 'C', Actualizado = 0 Where IdUnidad = '019'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '020' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '020', 'GRAMO', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'GRAMO', Status = 'C', Actualizado = 0 Where IdUnidad = '020'  
If Not Exists ( Select * From FACT_CFD_UnidadesDeMedida Where IdUnidad = '021' )  Insert Into FACT_CFD_UnidadesDeMedida (  IdUnidad, Descripcion, Status, Actualizado )  Values ( '021', 'BOTELLA', 'C', 0 )    Else Update FACT_CFD_UnidadesDeMedida Set Descripcion = 'BOTELLA', Status = 'C', Actualizado = 0 Where IdUnidad = '021'  
Go--#SQL 

/* 
Insert Into FACT_CFD_FormasDePago values ( '01', 'En una sola exhibición', 'A', 0 ) 
Insert Into FACT_CFD_FormasDePago values ( '02', 'En parcialidades', 'A', 0 ) 
Insert Into FACT_CFD_MetodosPago values ( '01', 'Efectivo', 'A', 0 ) 
Insert Into FACT_CFD_MetodosPago values ( '02', 'Tarjeta de crédito', 'A', 0 ) 
Insert Into FACT_CFD_MetodosPago values ( '03', 'Vales', 'A', 0 ) 
Go--#xSQL 
*/ 

-- select * from FACT_CFD_FormasDePago 
-- select * from FACT_CFD_MetodosPago 

--	sp_generainserts 'FACT_CFD_UnidadesDeMedida' ,   1
	  
