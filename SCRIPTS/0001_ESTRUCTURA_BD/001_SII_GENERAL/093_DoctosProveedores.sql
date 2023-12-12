------------------------------------------------------------------------------------------------------------------ 
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProveedores_Certificacion_Doctos' and xType = 'U' ) 
----	Drop Table CatProveedores_Certificacion_Doctos
----Go--#xxxSQL 

------------------------------------------------------------------------------------------------------------------ 
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProveedores_Certificacion' and xType = 'U' ) 
----	Drop Table CatProveedores_Certificacion
----Go--#xxxSQL  

-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProveedores_TipoDoctos' and xType = 'U' ) 
Begin 
	Create Table CatProveedores_TipoDoctos 
	(
		IdDocumento varchar(2) Not Null, 
		Descripcion varchar(100) Not Null,	
			
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CatProveedores_TipoDoctos Add Constraint PK_CatProveedores_TipoDoctos Primary Key ( IdDocumento ) 

End 
Go--#SQL  

	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '01' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '01', 'RFC', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'RFC', Status = 'A', Actualizado = 0 Where IdDocumento = '01'  
	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '02' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '02', 'ACTA CONSTITUTIVA O ACTA NACIMIENTO (PERSONA FISICA).', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'ACTA CONSTITUTIVA O ACTA NACIMIENTO (PERSONA FISICA).', Status = 'A', Actualizado = 0 Where IdDocumento = '02'  
	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '03' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '03', 'PODER DEL REPRESENTANTE LEGAL.', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'PODER DEL REPRESENTANTE LEGAL.', Status = 'A', Actualizado = 0 Where IdDocumento = '03'  
	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '04' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '04', 'IDENTIFICACION OFICIAL DEL REPRESENTANTE LEGAL.', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'IDENTIFICACION OFICIAL DEL REPRESENTANTE LEGAL.', Status = 'A', Actualizado = 0 Where IdDocumento = '04'  
	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '05' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '05', 'COMPROBANTE DE DOMICILIO.', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'COMPROBANTE DE DOMICILIO.', Status = 'A', Actualizado = 0 Where IdDocumento = '05'  
	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '06' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '06', 'AVISO DE RESPONSABLE SANITARIO.', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'AVISO DE RESPONSABLE SANITARIO.', Status = 'A', Actualizado = 0 Where IdDocumento = '06'  
	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '07' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '07', 'AVISO DE FUNCIONAMIENTO.', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'AVISO DE FUNCIONAMIENTO.', Status = 'A', Actualizado = 0 Where IdDocumento = '07'  
	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '08' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '08', 'LICENCIA SANITARIA.', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'LICENCIA SANITARIA.', Status = 'A', Actualizado = 0 Where IdDocumento = '08'  
	If Not Exists ( Select * From CatProveedores_TipoDoctos Where IdDocumento = '09' )  Insert Into CatProveedores_TipoDoctos (  IdDocumento, Descripcion, Status, Actualizado )  Values ( '09', 'ESTADO DE CUENTA BANCARIO.', 'A', 0 )    Else Update CatProveedores_TipoDoctos Set Descripcion = 'ESTADO DE CUENTA BANCARIO.', Status = 'A', Actualizado = 0 Where IdDocumento = '09'  
Go--#SQL 



-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProveedores_Certificacion' and xType = 'U' ) 
Begin 
	Create Table CatProveedores_Certificacion 
	(
		IdProveedor varchar(4) Not Null, 
			
		FechaRegistro datetime Not Null Default GetDate(),
		
		IdPersonalLegal varchar(8) Not Null Default '',
		PersonalLegal varchar(200) Not Null Default '',
		FechaLegal datetime Not Null Default GetDate(),
		
		IdPersonalSanitario varchar(8) Not Null Default '',
		PersonalSanitario varchar(200) Not Null Default '',
		FechaSanitario datetime Not Null Default GetDate(),	
		
		EsCertificado bit Not Null Default 0,
		
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CatProveedores_Certificacion Add Constraint PK_CatProveedores_Certificacion Primary Key ( IdProveedor ) 

	Alter Table CatProveedores_Certificacion Add Constraint FK_CatProveedores_Certificacion_CatProveedores
		Foreign Key ( IdProveedor )  References CatProveedores  ( IdProveedor )

End 
Go--#SQL  

	
	Insert Into CatProveedores_Certificacion ( IdProveedor )
	Select IdProveedor From CatProveedores P
	Where Not Exists ( Select * From CatProveedores_Certificacion C Where C.IdProveedor = P.IdProveedor )
Go--#SQL	

--------------------------------------------------------------------------------------------------------------------- 

-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProveedores_Certificacion_Doctos' and xType = 'U' ) 
Begin 
	Create Table CatProveedores_Certificacion_Doctos 
	(
		IdProveedor varchar(4) Not Null, 
		IdDocumento	varchar(2) Not Null,
		
		NombreDocumento varchar(200) Not Null Default '',
		Documento varchar(max) Not Null Default '', 
		FechaRegistro datetime Not Null Default GetDate(),
		
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CatProveedores_Certificacion_Doctos Add Constraint PK_CatProveedores_Certificacion_Doctos Primary Key ( IdProveedor, IdDocumento ) 

	Alter Table CatProveedores_Certificacion_Doctos Add Constraint FK_CatProveedores_Certificacion_Doctos_CatProveedores_Certificacion
		Foreign Key ( IdProveedor )  References CatProveedores_Certificacion  ( IdProveedor )

	Alter Table CatProveedores_Certificacion_Doctos Add Constraint FK_CatProveedores_Certificacion_Doctos_CatProveedores_TipoDoctos
		Foreign Key ( IdDocumento )  References CatProveedores_TipoDoctos  ( IdDocumento ) 
	
End 	
Go--#SQL 
