------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Regimenes' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Regimenes    
Go--#SQL 

------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_RegimenFiscal' and xType = 'U' ) 
	Drop Table CFDI_RegimenFiscal    
Go--#SQL  

Create Table CFDI_RegimenFiscal 
(	
	IdRegimen varchar(4) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	EsPersonaFisica bit Not Null Default 'false', 
	EsPersonaMoral bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_RegimenFiscal Add Constraint PK_CFDI_RegimenFiscal Primary	Key ( IdRegimen ) 
Go--#SQL 


If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '01' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '01', 'REGIMEN GENERAL DE LEY', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'REGIMEN GENERAL DE LEY', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '01'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '601' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '601', 'General de Ley Personas Morales', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'General de Ley Personas Morales', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '601'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '603' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '603', 'Personas Morales con Fines no Lucrativos', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Personas Morales con Fines no Lucrativos', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '603'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '605' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '605', 'Sueldos y Salarios e Ingresos Asimilados a Salarios', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Sueldos y Salarios e Ingresos Asimilados a Salarios', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '605'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '606' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '606', 'Arrendamiento', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Arrendamiento', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '606'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '607' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '607', 'Régimen de Enajenación o Adquisición de Bienes', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Régimen de Enajenación o Adquisición de Bienes', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '607'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '608' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '608', 'Demás ingresos', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Demás ingresos', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '608'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '609' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '609', 'Consolidación', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Consolidación', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '609'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '610' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '610', 'Residentes en el Extranjero sin Establecimiento Permanente en México', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Residentes en el Extranjero sin Establecimiento Permanente en México', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '610'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '611' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '611', 'Ingresos por Dividendos (socios y accionistas)', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Ingresos por Dividendos (socios y accionistas)', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '611'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '612' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '612', 'Personas Físicas con Actividades Empresariales y Profesionales', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Personas Físicas con Actividades Empresariales y Profesionales', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '612'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '614' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '614', 'Ingresos por intereses', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Ingresos por intereses', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '614'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '615' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '615', 'Régimen de los ingresos por obtención de premios', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Régimen de los ingresos por obtención de premios', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '615'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '616' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '616', 'Sin obligaciones fiscales', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Sin obligaciones fiscales', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '616'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '620' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '620', 'Sociedades Cooperativas de Producción que optan por diferir sus ingresos', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Sociedades Cooperativas de Producción que optan por diferir sus ingresos', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '620'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '621' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '621', 'Incorporación Fiscal', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Incorporación Fiscal', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '621'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '622' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '622', 'Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '622'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '623' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '623', 'Opcional para Grupos de Sociedades', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Opcional para Grupos de Sociedades', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '623'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '624' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '624', 'Coordinados', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Coordinados', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '624'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '628' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '628', 'Hidrocarburos', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Hidrocarburos', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '628'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '629' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '629', 'De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '629'  
If Not Exists ( Select * From CFDI_RegimenFiscal Where IdRegimen = '630' )  Insert Into CFDI_RegimenFiscal (  IdRegimen, Descripcion, EsPersonaFisica, EsPersonaMoral, Status )  Values ( '630', 'Enajenación de acciones en bolsa de valores', 0, 0, 'A' ) 
 Else Update CFDI_RegimenFiscal Set Descripcion = 'Enajenación de acciones en bolsa de valores', EsPersonaFisica = 0, EsPersonaMoral = 0, Status = 'A' Where IdRegimen = '630'  
Go--#SQL 


------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Regimenes' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Regimenes    
Go--#SQL 

Create Table CFDI_Emisores_Regimenes
( 
	IdEmpresa varchar(3) Not Null,  	
	IdRegimen varchar(4) Not Null 
) 
Go--#SQL  

Alter Table CFDI_Emisores_Regimenes Add Constraint PK_CFDI_Emisores_Regimenes Primary Key ( IdEmpresa )  
Go--#SQL 


Alter Table CFDI_Emisores_Regimenes Add Constraint FK_CFDI_Emisores_Regimenes___CFDI_RegimenFiscal 
	Foreign Key ( IdRegimen ) References CFDI_RegimenFiscal ( IdRegimen ) 
Go--#SQL 
 

Insert Into CFDI_Emisores_Regimenes select '001', '01' 
Go--#SQL 
  