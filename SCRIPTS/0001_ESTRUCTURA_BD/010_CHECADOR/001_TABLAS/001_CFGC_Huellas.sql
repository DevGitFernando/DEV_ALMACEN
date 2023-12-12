

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_Huellas' and xType = 'U' ) 
Begin 
	Create Table CFGC_Huellas 
	(	 
		Servidor varchar(100) Not Null Default '', 
		WebService varchar(100) Not Null Default '', 
		PaginaWeb varchar(100) Not Null Default '', 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CFGC_Huellas Add Constraint PK_CFGC_Huellas Primary Key ( Servidor ) 
End 
Go--#SQL


-- Insert Into CFGC_Huellas Select 'intermed.homeip.net:8090', 'wsAdministrativos', 'wsChecador', 'A', 0