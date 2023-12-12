If Exists ( Select Name From Sysobjects (noLock) Where Name = 'CatEmpresas' and xType = 'U' ) 
   Drop Table CatEmpresas 
Go--#SQL 

Create Table CatEmpresas 
(
	IdEmpresa varchar(3) Not Null, 
	Nombre varchar(100) Not Null Default '', 
	NombreCorto varchar(50) Not Null Default '', 
	EsDeConsignacion bit Not Null Default 'false', 
    RFC varchar(15) Not Null, 
    EdoCiudad varchar(100) Not Null Default '', 
	Colonia varchar(100) Not Null Default '', 
    Domicilio varchar(200) Not Null, 
	CodigoPostal varchar(20) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatEmpresas Add Constraint PK_CatEmpresas Primary Key ( IdEmpresa ) 
Go--#SQL 
--------------


------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select *  From Sysobjects (NoLock) Where Name = 'CFG_Empresas' and xType = 'U' ) 
Begin 
Create Table CFG_Empresas 
( 
	IdEmpresa varchar(3) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)

Alter Table CFG_Empresas Add Constraint PK_CFG_Empresas Primary Key ( IdEmpresa ) 

Alter Table CFG_Empresas Add Constraint FK_CFG_Empresas__CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 

End 
Go--#SQL  



/* 
If Exists ( Select top 1 * From FarmaciaProductos (NoLock) Where IdEmpresa = '001' ) 
Begin 
	If Not Exists ( Select * From CFG_Empresas Where IdEmpresa = '001' )  Insert Into CFG_Empresas (  IdEmpresa, Status, Actualizado )  Values ( '001', 'A', 0 )    Else Update CFG_Empresas Set Status = 'A', Actualizado = 0 Where IdEmpresa = '001'  
End 
--Go--#SQL  


If Exists ( Select top 1 * From FarmaciaProductos (NoLock) Where IdEmpresa = '002' ) 
Begin 
	If Not Exists ( Select * From CFG_Empresas Where IdEmpresa = '002' )  Insert Into CFG_Empresas (  IdEmpresa, Status, Actualizado )  Values ( '002', 'A', 0 )    Else Update CFG_Empresas Set Status = 'A', Actualizado = 0 Where IdEmpresa = '002'  
End 
--Go--#SQL  

If Exists ( Select top 1 * From FarmaciaProductos (NoLock) Where IdEmpresa = '003' ) 
Begin 
	If Not Exists ( Select * From CFG_Empresas Where IdEmpresa = '003' )  Insert Into CFG_Empresas (  IdEmpresa, Status, Actualizado )  Values ( '003', 'A', 0 )    Else Update CFG_Empresas Set Status = 'A', Actualizado = 0 Where IdEmpresa = '003'  
End 
--Go--#SQL  
*/ 

