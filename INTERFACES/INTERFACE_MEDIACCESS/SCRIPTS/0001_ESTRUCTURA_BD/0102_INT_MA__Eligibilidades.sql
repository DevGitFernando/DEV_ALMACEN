Set NoCount On 
Go--#SQL   

--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__Elegibilidades' and xType = 'U' ) 
   Drop Table INT_MA__Elegibilidades  
Go--#SQL   

Create Table INT_MA__Elegibilidades 
( 
	Folio varchar(12) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate(), 	
		
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	IdPersonal varchar(4) Not Null Default '', 

	Eligibilidad varchar(50) Not Null Default '' Unique, 		
	Empresa_y_RazonSocial varchar(500) Not Null Default '', 
	Plan_Producto varchar(500) Not Null Default '', 
	Elegibilidad_DisponibleSurtido bit Not Null Default 'true', 

	Elegibilidad_Surtidos Int Not Null Default 0, 
	Elegibilidad_Surtidos_Aplicados Int Not Null Default 0, 

	NombreEmpleado varchar(200) Not Null Default '', 
	ReferenciaEmpleado varchar(20) Not Null Default '', 		

	NombreMedico varchar(200) Not Null Default '', 
	ReferenciaMedico varchar(20) Not Null Default '', 
	
	Copago smallint Not Null Default 0, 
	CopagoEn smallint Not Null Default 0 	  	
) 
Go--#SQL   

Alter Table INT_MA__Elegibilidades 
	Add Constraint PK_INT_MA__Elegibilidades Primary Key ( Folio ) 
Go--#SQL   

