If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepaño' and xType = 'U' ) 
   Drop Table CatPasillos_Estantes_Entrepaño 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entreaño' and xType = 'U' ) 
   Drop Table CatPasillos_Estantes_Entreaño 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatUbicaciones_CodigosEAN_Lotes' and xType = 'U' ) 
   Drop Table CatUbicaciones_CodigosEAN_Lotes 
Go--#SQL 


--------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos' and xType = 'U' ) 
Begin 
Create Table CatPasillos
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	
	IdPasillo int Not Null, 
	DescripcionPasillo varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0	

) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_CatPasillos' and xType = 'PK' ) 
Begin 
    Alter Table CatPasillos Add Constraint PK_CatPasillos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo ) 
End 
Go--#SQL

---------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes' and xType = 'U' ) 
Begin 
Create Table CatPasillos_Estantes
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	IdPasillo int Not Null, 
	
	IdEstante int Not Null, 
	DescripcionEstante varchar(50) Not Null Default '', 

	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0	

) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_CatPasillos_Estantes' and xType = 'PK' ) 
Begin 
    Alter Table CatPasillos_Estantes Add Constraint PK_CatPasillos_Estantes Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_CatPasillos_Estantes_CatPasillos' and xType = 'F' ) 
Begin 
    Alter Table CatPasillos_Estantes Add Constraint FK_CatPasillos_Estantes_CatPasillos 
        Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo ) 
        References CatPasillos ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo ) 
End 
Go--#SQL

---------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepaños' and xType = 'U' ) 
Begin 
Create Table CatPasillos_Estantes_Entrepaños
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	
	IdEntrepaño int Not Null, 
	DescripcionEntrepaño varchar(50) Not Null Default '', 
    EsExclusiva bit Not Null Default 'false', 
	IdOrden int Not Null Default 9999, 

	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0	

) 
End 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_CatPasillos_Estantes_Entrepaño' and xType = 'PK' ) 
Begin 
    Alter Table CatPasillos_Estantes_Entrepaños Drop Constraint PK_CatPasillos_Estantes_Entrepaño 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_CatPasillos_Estantes_Entrepaños' and xType = 'PK' ) 
Begin 
    Alter Table CatPasillos_Estantes_Entrepaños Add Constraint PK_CatPasillos_Estantes_Entrepaños 
        Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_CatPasillos_Estantes_Entrepaños_CatPasillos_Estantes' and xType = 'F' ) 
Begin 
    Alter Table CatPasillos_Estantes_Entrepaños Add Constraint FK_CatPasillos_Estantes_Entrepaños_CatPasillos_Estantes 
        Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante ) 
        References CatPasillos_Estantes ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante ) 
End 
Go--#SQL	

