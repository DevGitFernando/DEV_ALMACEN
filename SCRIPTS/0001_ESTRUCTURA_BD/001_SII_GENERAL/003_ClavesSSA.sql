-------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatClavesSSA_Sales' and xType = 'U' )
	Drop Table CatClavesSSA_Sales 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatTiposDeProducto' and xType = 'U' ) 
	Drop Table CatTiposDeProducto 
Go--#SQL  

Create Table CatTiposDeProducto 
( 
	IdTipoProducto varchar(2) Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	PorcIva int Null Default 15,   
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatTiposDeProducto Add Constraint PK_CatTiposDeProducto Primary Key ( IdTipoProducto )
Go--#SQL  

If Not Exists ( Select * From CatTiposDeProducto Where IdTipoProducto = '00' )  Insert Into CatTiposDeProducto (  IdTipoProducto, Descripcion, PorcIva, Status, Actualizado )  Values ( '00', 'SIN ESPECIFICAR', 0, 'A', 0 )    Else Update CatTiposDeProducto Set Descripcion = 'SIN ESPECIFICAR', PorcIva = 0, Status = 'A', Actualizado = 0 Where IdTipoProducto = '00'
If Not Exists ( Select * From CatTiposDeProducto Where IdTipoProducto = '01' )  Insert Into CatTiposDeProducto (  IdTipoProducto, Descripcion, PorcIva, Status, Actualizado )  Values ( '01', 'MATERIAL DE CURACION', 16, 'A', 0 )    Else Update CatTiposDeProducto Set Descripcion = 'MATERIAL DE CURACION', PorcIva = 16, Status = 'A', Actualizado = 0 Where IdTipoProducto = '01'
If Not Exists ( Select * From CatTiposDeProducto Where IdTipoProducto = '02' )  Insert Into CatTiposDeProducto (  IdTipoProducto, Descripcion, PorcIva, Status, Actualizado )  Values ( '02', 'MEDICAMENTO', 0, 'A', 0 )    Else Update CatTiposDeProducto Set Descripcion = 'MEDICAMENTO', PorcIva = 0, Status = 'A', Actualizado = 0 Where IdTipoProducto = '02'
If Not Exists ( Select * From CatTiposDeProducto Where IdTipoProducto = '03' )  Insert Into CatTiposDeProducto (  IdTipoProducto, Descripcion, PorcIva, Status, Actualizado )  Values ( '03', 'COSMETICOS', 16, 'A', 0 )    Else Update CatTiposDeProducto Set Descripcion = 'COSMETICOS', PorcIva = 16, Status = 'A', Actualizado = 0 Where IdTipoProducto = '03'
If Not Exists ( Select * From CatTiposDeProducto Where IdTipoProducto = '04' )  Insert Into CatTiposDeProducto (  IdTipoProducto, Descripcion, PorcIva, Status, Actualizado )  Values ( '04', 'HIGIENE', 16, 'A', 0 )    Else Update CatTiposDeProducto Set Descripcion = 'HIGIENE', PorcIva = 16, Status = 'A', Actualizado = 0 Where IdTipoProducto = '04'
If Not Exists ( Select * From CatTiposDeProducto Where IdTipoProducto = '05' )  Insert Into CatTiposDeProducto (  IdTipoProducto, Descripcion, PorcIva, Status, Actualizado )  Values ( '05', 'FORMULAS LACTEAS Y ALIMENTOS INFANTILES', 0, 'A', 0 )    Else Update CatTiposDeProducto Set Descripcion = 'FORMULAS LACTEAS Y ALIMENTOS INFANTILES', PorcIva = 0, Status = 'A', Actualizado = 0 Where IdTipoProducto = '05'
If Not Exists ( Select * From CatTiposDeProducto Where IdTipoProducto = '06' )  Insert Into CatTiposDeProducto (  IdTipoProducto, Descripcion, PorcIva, Status, Actualizado )  Values ( '06', 'BEBIDAS', 16, 'A', 0 )    Else Update CatTiposDeProducto Set Descripcion = 'BEBIDAS', PorcIva = 16, Status = 'A', Actualizado = 0 Where IdTipoProducto = '06'
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatGruposTerapeuticos' and xType = 'U' )
	Drop Table CatGruposTerapeuticos  
Go--#SQL 

Create Table CatGruposTerapeuticos 
(
	IdGrupoTerapeutico varchar(3) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	Tipo smallint Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatGruposTerapeuticos Add Constraint PK_CatGruposTerapeuticos Primary Key ( IdGrupoTerapeutico ) 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatTipoCatalogoClaves' and xType = 'U' )
	Drop Table CatTipoCatalogoClaves  
Go--#SQL 

Create Table CatTipoCatalogoClaves 
(
	TipoCatalogo varchar(2) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
)
Go--#SQL

Alter Table CatTipoCatalogoClaves Add Constraint PK_CatTipoCatalogoClaves Primary Key ( TipoCatalogo ) 
Go--#SQL 

Insert Into CatTipoCatalogoClaves ( TipoCatalogo, Descripcion ) Values ( '00', 'GENERAL' ) 
Insert Into CatTipoCatalogoClaves ( TipoCatalogo, Descripcion ) Values ( '01', 'CAUSES' ) 
Insert Into CatTipoCatalogoClaves ( TipoCatalogo, Descripcion ) Values ( '02', 'BASICO(LICICITACION)' ) 
Insert Into CatTipoCatalogoClaves ( TipoCatalogo, Descripcion ) Values ( '03', 'CONVENIADO' ) 
Go--#SQL 

-------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatClavesSSA_Sales' and xType = 'U' )
	Drop Table CatClavesSSA_Sales 
Go--#SQL 

Create Table CatClavesSSA_Sales 
(
	IdClaveSSA_Sal varchar(4) Not Null, 
	ClaveSSA_Base varchar(50) Not Null Default '',	
	ClaveSSA varchar(50) Not Null Unique,  ---- No permitir Claves Duplicadas  
	Descripcion varchar(7500) Not Null Default '', 
	DescripcionCortaClave varchar(50) Not Null Default '', 
	IdGrupoTerapeutico varchar(3) Not Null Default '', 
	TipoCatalogo varchar(2) Not Null, 
	IdPresentacion varchar(3) Not Null,	
	ContenidoPaquete int Not Null Default 0, 
	EsControlado bit Not Null Default 'false',
	EsAntibiotico bit Not Null Default 'false',  
	EsRefrigerado bit Not Null Default 'false', 

	IdTipoProducto varchar(2) Not Null Default '00',	
	Ordenamiento int Not Null Default 0, 	
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table CatClavesSSA_Sales Add Constraint PK_CatClavesSSA_Sales Primary Key ( IdClaveSSA_Sal ) 
Go--#SQL 

Alter Table CatClavesSSA_Sales Add Constraint FK_CatClavesSSA_Sales_CatGruposTerapeuticos 
	Foreign Key ( IdGrupoTerapeutico ) References CatGruposTerapeuticos ( IdGrupoTerapeutico ) 
Go--#SQL 

Alter Table CatClavesSSA_Sales Add Constraint FK_CatClavesSSA_Sales_CatTipoCatalogoClaves 
	Foreign Key ( TipoCatalogo ) References CatTipoCatalogoClaves ( TipoCatalogo ) 
Go--#SQL 

Alter Table CatClavesSSA_Sales Add Constraint FK_CatClavesSSA_Sales_CatTiposDeProducto 
    Foreign Key ( IdTipoProducto ) References CatTiposDeProducto ( IdTipoProducto ) 
Go--#SQL 


----If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'UQ__ClaveSSA' and xType = 'UQ' ) 
----Begin 
----	Alter Table CatClavesSSA_Sales Add Constraint UQ__ClaveSSA UNIQUE ( ClaveSSA ) 
----End 	
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatClavesSSA_Sales_Historico' and xType = 'U' ) 
	Drop Table CatClavesSSA_Sales_Historico 
Go--#SQL  

Create Table CatClavesSSA_Sales_Historico 
(	 
	IdClaveSSA_Sal varchar(4) Not Null,
	ClaveSSA_Base varchar(50) Not Null Default '',
	ClaveSSA varchar(50) Not Null Default '', 
	Descripcion varchar(7500) Not Null Default '', 
	DescripcionCortaClave varchar(30) Not Null Default '', 	
	IdGrupoTerapeutico varchar(3) Not Null Default '', 
	TipoCatalogo varchar(2) Not Null Default '',
	IdPresentacion varchar(3) Not Null,	
	ContenidoPaquete int Not Null Default 0, 
	EsControlado bit default 'False',
	EsAntibiotico bit default 'False',		 
	
	IdTipoProducto varchar(2) Not Null,	
	Ordenamiento int Not Null Default 0, 		
	
    FechaRegistro datetime Not Null Default getdate(),    
    IdPersonal varchar(4) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	Keyx int identity(1,1)  
)
Go--#SQL 

-------------------------------------------------------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatClasificacionesSSA' and xType = 'U' )
	Drop Table CatClasificacionesSSA 
Go--#SQL 

Create Table CatClasificacionesSSA 
(
	IdClasificacion varchar(4) Not Null, 
	Descripcion varchar(150) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table CatClasificacionesSSA Add Constraint PK_CatClasificacionesSSA Primary Key ( IdClasificacion ) 
Go--#SQL 


