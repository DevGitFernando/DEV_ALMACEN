If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_AutorizacionHuellas' and xType = 'U' )
Begin 
	Create Table CFGC_AutorizacionHuellas 
	(	 
		Servidor varchar(100) Not Null Default '', 
		WebService varchar(100) Not Null Default '', 
		PaginaWeb varchar(100) Not Null Default '', 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CFGC_AutorizacionHuellas Add Constraint PK_CFGC_AutorizacionHuellas Primary Key ( Servidor ) 
End 
Go--#SQL 

Delete From CFGC_AutorizacionHuellas 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_FirmaHuellas' and xType = 'U' )
	Begin
		Insert Into CFGC_AutorizacionHuellas ( Servidor, WebService, PaginaWeb, Status, Actualizado )
		Select Servidor, WebService, PaginaWeb, Status, Actualizado From CFGC_FirmaHuellas
	End
else
	Begin
		Insert Into CFGC_AutorizacionHuellas ( Servidor, WebService, PaginaWeb, Status, Actualizado ) 
		Select host_name() as Servidor, 'wsInt-PuntoDeVenta' as WebService, 'wsValidarHuellas' as PaginaWeb, 'A' as Status, 0 as Actualizado 
	End
Go--#SQL 
