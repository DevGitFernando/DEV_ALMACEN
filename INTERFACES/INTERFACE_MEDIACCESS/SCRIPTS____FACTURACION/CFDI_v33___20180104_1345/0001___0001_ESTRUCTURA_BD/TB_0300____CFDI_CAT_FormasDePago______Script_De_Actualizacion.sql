
-------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'CFDI_FormasDePago' and So.xType = 'U' ) 
   Drop Table CFDI_FormasDePago 
Go--#SQL 

Create Table CFDI_FormasDePago 
( 
    IdFormaDePago varchar(6) Not Null, 
    Descripcion varchar(100) Not Null Default '', 
	Version varchar(10) Not Null Default '', 
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0, 
	RequiereReferencia bit Not Null Default 'false'  
) 
Go--#SQL 

Alter Table CFDI_FormasDePago Add Constraint PK_CFDI_FormasDePago Primary Key ( IdFormaDePago )     
Go--#SQL 



If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '00' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '00', 'No identificado', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'No identificado', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '00'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '01' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '01', 'Efectivo', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Efectivo', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '01'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '02' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '02', 'Cheque nóminativo', '', 'A', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Cheque nóminativo', Version = '', Status = 'A', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '02'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '03' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '03', 'Transferencia electrónica de fondos', '', 'A', 0, 1 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Transferencia electrónica de fondos', Version = '', Status = 'A', Actualizado = 0, RequiereReferencia = 1 Where IdFormaDePago = '03'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '04' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '04', 'Tarjeta de crédito', '', 'C', 0, 1 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Tarjeta de crédito', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 1 Where IdFormaDePago = '04'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '05' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '05', 'Monedero electrónico', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Monedero electrónico', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '05'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '06' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '06', 'Dinero electrónico', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Dinero electrónico', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '06'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '07' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '07', 'Tarjetas digitales', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Tarjetas digitales', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '07'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '08' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '08', 'Vales de despensa', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Vales de despensa', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '08'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '09' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '09', 'Bienes', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Bienes', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '09'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '10' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '10', 'Servicio', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Servicio', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '10'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '11' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '11', 'Por cuenta de tercero', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Por cuenta de tercero', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '11'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '12' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '12', 'Dación en pago', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Dación en pago', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '12'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '13' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '13', 'Pago por subrogación', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Pago por subrogación', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '13'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '14' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '14', 'Pago por consignación', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Pago por consignación', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '14'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '15' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '15', 'Condonación', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Condonación', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '15'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '16' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '16', 'Cancelación', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Cancelación', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '16'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '17' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '17', 'Compensación', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Compensación', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '17'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '23' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '23', 'Novación', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Novación', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '23'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '24' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '24', 'Confusión', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Confusión', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '24'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '25' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '25', 'Remisión de deuda', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Remisión de deuda', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '25'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '26' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '26', 'Prescripción o caducidad', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Prescripción o caducidad', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '26'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '27' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '27', 'A satisfacción del acreedor', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'A satisfacción del acreedor', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '27'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '28' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '28', 'Tarjeta de débito', '', 'C', 0, 1 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Tarjeta de débito', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 1 Where IdFormaDePago = '28'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '29' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '29', 'Tarjeta de servicios', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Tarjeta de servicios', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '29'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '30' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '30', 'Aplicación de anticipos', '', 'A', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Aplicación de anticipos', Version = '', Status = 'A', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '30'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '98' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '98', 'NA', '', 'C', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'NA', Version = '', Status = 'C', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '98'  
If Not Exists ( Select * From CFDI_FormasDePago Where IdFormaDePago = '99' )  Insert Into CFDI_FormasDePago (  IdFormaDePago, Descripcion, Version, Status, Actualizado, RequiereReferencia )  Values ( '99', 'Por definir', '', 'A', 0, 0 ) 
 Else Update CFDI_FormasDePago Set Descripcion = 'Por definir', Version = '', Status = 'A', Actualizado = 0, RequiereReferencia = 0 Where IdFormaDePago = '99'  

Go--#SQL 

