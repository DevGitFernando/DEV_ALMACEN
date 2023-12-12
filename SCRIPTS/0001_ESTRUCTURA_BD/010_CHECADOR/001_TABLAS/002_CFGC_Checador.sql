

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_Checador' and xType = 'U' )
Begin 
	Create Table CFGC_Checador 
	(	 
		Servidor varchar(100) Not Null Default '', 
		WebService varchar(100) Not Null Default '', 
		PaginaWeb varchar(100) Not Null Default '', 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CFGC_Checador Add Constraint PK_CFGC_Checador Primary Key ( Servidor ) 
End
Go--#SQL