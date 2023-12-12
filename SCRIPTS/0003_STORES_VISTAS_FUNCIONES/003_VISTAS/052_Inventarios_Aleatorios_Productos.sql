------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INV_Aleatorios_Productos_Det' and xType = 'U' ) 
   Drop Table INV_Aleatorios_Productos_Det 
Go--#SQL 


------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INV_Aleatorios_Productos_Enc' and xType = 'U' ) 
   Drop Table INV_Aleatorios_Productos_Enc 
Go--#SQL 

Create Table INV_Aleatorios_Productos_Enc 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Folio varchar(6) Not Null,
	IdPersonal varchar(4) Not Null, 
	FechaRegistro datetime Not Null default getdate(), 
	TipoInventario smallint Not Null Default 0, 
	Status varchar(2) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0
) 
Go--#SQL 
   
Alter Table INV_Aleatorios_Productos_Enc Add Constraint PK_INV_Aleatorios_Productos_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL 
   
Alter Table INV_Aleatorios_Productos_Enc Add Constraint FK_INV_Aleatorios_Productos_Enc___CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 
   
Alter Table INV_Aleatorios_Productos_Enc Add Constraint FK_INV_Aleatorios_Productos_Enc___CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL   

Alter Table INV_Aleatorios_Productos_Enc Add Constraint FK_INV_Aleatorios_Productos_Enc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 
   
------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INV_Aleatorios_Productos_Det' and xType = 'U' ) 
   Drop Table INV_Aleatorios_Productos_Det 
Go--#SQL 

Create Table INV_Aleatorios_Productos_Det 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Folio varchar(6) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 	

	ExistenciaLogica int Not Null Default 0,		---- Existencia de sistema 	
	Conteo_01 int Not Null Default 0,				---- 
	Conteo_02 int Not Null Default 0,				---- 
	Conteo_03 int Not Null Default 0,				---- 
	EsConteo_01 bit Not Null Default 'false',		---- 
	EsConteo_02 bit Not Null Default 'false',		---- 	
	EsConteo_03 bit Not Null Default 'false',		---- 
	Fecha_01 datetime Not Null Default getdate(),	---- 
	Fecha_02 datetime Not Null Default getdate(),	---- 
	Fecha_03 datetime Not Null Default getdate(), 	----	
	
	Conciliado bit Not Null Default 'false', 	---- Determina si alguno de los conteos coincidio con la existencia logica 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0
) 
Go--#SQL 
   
Alter Table INV_Aleatorios_Productos_Det Add Constraint PK_INV_Aleatorios_Productos_Det 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto, CodigoEAN ) 
Go--#SQL 


Alter Table INV_Aleatorios_Productos_Det Add Constraint FK_INV_Aleatorios_Productos_Det___INV_Aleatorios_Productos_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INV_Aleatorios_Productos_Enc ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL 

