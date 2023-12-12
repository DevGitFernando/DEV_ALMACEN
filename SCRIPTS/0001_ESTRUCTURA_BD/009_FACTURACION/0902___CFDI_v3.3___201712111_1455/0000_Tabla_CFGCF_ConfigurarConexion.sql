If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGCF_ConfigurarConexion' and xType = 'U' )
Begin 
	Create Table CFGCF_ConfigurarConexion 
	(	 
		Servidor varchar(100) Not Null Default '', 
		WebService varchar(100) Not Null Default '', 
		PaginaWeb varchar(100) Not Null Default '', 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CFGCF_ConfigurarConexion Add Constraint PK_CFGCF_ConfigurarConexion Primary Key ( Servidor, WebService ) 
End 
Go--#SQL