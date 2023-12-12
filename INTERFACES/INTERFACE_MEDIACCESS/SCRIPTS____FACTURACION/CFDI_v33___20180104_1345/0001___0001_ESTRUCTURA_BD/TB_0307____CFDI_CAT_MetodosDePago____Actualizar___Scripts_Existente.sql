
-------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'CFDI_MetodosPago' and So.xType = 'U' ) 
   Drop Table CFDI_MetodosPago 
Go--#SQL 

Create Table CFDI_MetodosPago 
( 
    IdMetodoDePago varchar(6) Not Null, 
    Descripcion varchar(100) Not Null Default '', 
	Version varchar(10) Not Null Default '', 
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CFDI_MetodosPago Add Constraint PK_CFDI_MetodosPago Primary Key ( IdMetodoDePago )     
Go--#SQL 

----If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFDI_MetodosPago' and xType = 'U' ) 
----Begin 
----	If Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
----		Where So.Name = 'CFDI_MetodosPago' and Sc.Name = 'IdMetodoDePago' ) 
----	Begin
----		Alter Table CFDI_MetodosPago Alter Column IdMetodoDePago varchar(6) Not Null 
----	End 
----End 
----Go--#SQL 


If Not Exists ( Select * From CFDI_MetodosPago Where IdMetodoDePago = '01' )  Insert Into CFDI_MetodosPago (  IdMetodoDePago, Descripcion, Version, Status, Actualizado )  Values ( '01', 'En una sola exhibición', '3.2', 'A', 0 ) 
 Else Update CFDI_MetodosPago Set Descripcion = 'En una sola exhibición', Version = '3.2', Status = 'A', Actualizado = 0 Where IdMetodoDePago = '01'  
If Not Exists ( Select * From CFDI_MetodosPago Where IdMetodoDePago = '02' )  Insert Into CFDI_MetodosPago (  IdMetodoDePago, Descripcion, Version, Status, Actualizado )  Values ( '02', 'En parcialidades', '3.2', 'A', 0 ) 
 Else Update CFDI_MetodosPago Set Descripcion = 'En parcialidades', Version = '3.2', Status = 'A', Actualizado = 0 Where IdMetodoDePago = '02'  
If Not Exists ( Select * From CFDI_MetodosPago Where IdMetodoDePago = 'PPD' )  Insert Into CFDI_MetodosPago (  IdMetodoDePago, Descripcion, Version, Status, Actualizado )  Values ( 'PPD', 'Pago en parcialidades o diferido', '3.3', 'A', 0 ) 
 Else Update CFDI_MetodosPago Set Descripcion = 'Pago en parcialidades o diferido', Version = '3.3', Status = 'A', Actualizado = 0 Where IdMetodoDePago = 'PPD'  
If Not Exists ( Select * From CFDI_MetodosPago Where IdMetodoDePago = 'PUE' )  Insert Into CFDI_MetodosPago (  IdMetodoDePago, Descripcion, Version, Status, Actualizado )  Values ( 'PUE', 'Pago en una sola exhibición', '3.3', 'A', 0 ) 
 Else Update CFDI_MetodosPago Set Descripcion = 'Pago en una sola exhibición', Version = '3.3', Status = 'A', Actualizado = 0 Where IdMetodoDePago = 'PUE'  
Go--#SQL 

