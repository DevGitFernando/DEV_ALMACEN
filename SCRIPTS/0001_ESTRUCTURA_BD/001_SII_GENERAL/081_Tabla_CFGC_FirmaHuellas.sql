If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_FirmaHuellas' and xType = 'U' )
Begin 
	Create Table CFGC_FirmaHuellas 
	(	 
		Servidor varchar(100) Not Null Default '', 
		WebService varchar(100) Not Null Default '', 
		PaginaWeb varchar(100) Not Null Default '', 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CFGC_FirmaHuellas Add Constraint PK_CFGC_FirmaHuellas Primary Key ( Servidor ) 
End 
Go--#SQL 

Delete From CFGC_FirmaHuellas 
Go--#SQL 

Insert Into CFGC_FirmaHuellas ( Servidor, WebService, PaginaWeb, Status, Actualizado ) 
Select host_name() as Servidor, 'wsInt-PuntoDeVenta' as WebService, 'wsValidarHuellas' as PaginaWeb, 'A' as Status, 0 as Actualizado 
Go--#SQL 
