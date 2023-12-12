------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_UsosDeCFDI' and xType = 'U' ) 
	Drop Table CFDI_UsosDeCFDI    
Go--#SQL  

Create Table CFDI_UsosDeCFDI 
(	
	Clave varchar(6) Not Null Default '', 
	Descripcion varchar(200) Not Null Default '' Unique, 
	EsPersonaFisica bit Not Null Default 'false', 
	EsPersonaMoral bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_UsosDeCFDI Add Constraint PK_CFDI_UsosDeCFDI Primary	Key ( Clave ) 
Go--#SQL 


If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D01' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D01', 'Honorarios médicos, dentales y gastos hospitalarios.', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Honorarios médicos, dentales y gastos hospitalarios.', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D01'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D02' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D02', 'Gastos médicos por incapacidad o discapacidad', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Gastos médicos por incapacidad o discapacidad', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D02'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D03' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D03', 'Gastos funerales.', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Gastos funerales.', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D03'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D04' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D04', 'Donativos.', 0, 0, 'A' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Donativos.', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where Clave = 'D04'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D05' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D05', 'Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación).', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación).', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D05'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D06' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D06', 'Aportaciones voluntarias al SAR.', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Aportaciones voluntarias al SAR.', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D06'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D07' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D07', 'Primas por seguros de gastos médicos.', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Primas por seguros de gastos médicos.', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D07'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D08' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D08', 'Gastos de transportación escolar obligatoria.', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Gastos de transportación escolar obligatoria.', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D08'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D09' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D09', 'Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones.', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones.', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D09'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'D10' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'D10', 'Pagos por servicios educativos (colegiaturas)', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Pagos por servicios educativos (colegiaturas)', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'D10'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'G01' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'G01', 'Adquisición de mercancias', 0, 0, 'A' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Adquisición de mercancias', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where Clave = 'G01'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'G02' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'G02', 'Devoluciones, descuentos o bonificaciones', 0, 0, 'A' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Devoluciones, descuentos o bonificaciones', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where Clave = 'G02'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'G03' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'G03', 'Gastos en general', 0, 0, 'A' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Gastos en general', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where Clave = 'G03'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'I01' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'I01', 'Construcciones', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Construcciones', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'I01'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'I02' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'I02', 'Mobilario y equipo de oficina por inversiones', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Mobilario y equipo de oficina por inversiones', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'I02'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'I03' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'I03', 'Equipo de transporte', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Equipo de transporte', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'I03'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'I04' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'I04', 'Equipo de computo y accesorios', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Equipo de computo y accesorios', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'I04'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'I05' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'I05', 'Dados, troqueles, moldes, matrices y herramental', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Dados, troqueles, moldes, matrices y herramental', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'I05'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'I06' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'I06', 'Comunicaciones telefónicas', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Comunicaciones telefónicas', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'I06'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'I07' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'I07', 'Comunicaciones satelitales', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Comunicaciones satelitales', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'I07'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'I08' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'I08', 'Otra maquinaria y equipo', 0, 0, 'C' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Otra maquinaria y equipo', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'C' Where Clave = 'I08'  
If Not Exists ( Select * From CFDI_UsosDeCFDI Where Clave = 'P01' )  Insert Into CFDI_UsosDeCFDI (  Clave, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( 'P01', 'Por definir', 0, 0, 'A' ) 
 Else Update CFDI_UsosDeCFDI Set Descripcion = 'Por definir', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where Clave = 'P01'  
Go--#SQL
 