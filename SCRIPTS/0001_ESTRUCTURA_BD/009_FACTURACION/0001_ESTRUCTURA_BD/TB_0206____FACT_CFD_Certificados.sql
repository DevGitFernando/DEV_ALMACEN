------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Certificados' and xType = 'U' ) 
	Drop Table FACT_CFD_Certificados  
Go--#SQL 

Create Table FACT_CFD_Certificados 
( 
	IdEmpresa varchar(3) Not Null, 
	NumeroDeCertificado varchar (100) Not Null,  
	NombreCertificado varchar(100) Not Null, 
	Certificado varchar(max) Not Null Default '', 
	ValidoDesde varchar(100) Not Null Default '', 
	ValidoHasta varchar(100) Not Null Default '', 
	FechaInicio datetime Not Null Default getdate(), 
	FechaFinal datetime Not Null Default getdate(), 

	Serie varchar(100) Not Null Default '', 
	Serial varchar(100) Not Null Default '', 	
	
	NombreLlavePrivada varchar(100) Not Null Default '',
	LlavePrivada varchar(max) Not Null Default '',
	PasswordPublico varchar(100) Not Null Default '', 

	NombreCertificadoPfx varchar(100) Not Null, 		
	CertificadoPfx varchar(max) Not Null Default '', 	
	
	AvisoVencimiento bit Not Null Default 'true', 
	TiempoAviso int Not Null Default 15, 
	
	Status varchar(1) Not Null Default '', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL  

Alter Table FACT_CFD_Certificados Add Constraint PK_FACT_CFD_Certificados Primary Key ( IdEmpresa ) 
Go--#SQL 

Alter Table FACT_CFD_Certificados Add Constraint FK_FACT_CFD_Certificados___FACT_CFD_Empresas 
	Foreign Key ( IdEmpresa  ) References FACT_CFD_Empresas ( IdEmpresa ) 
Go--#SQL 