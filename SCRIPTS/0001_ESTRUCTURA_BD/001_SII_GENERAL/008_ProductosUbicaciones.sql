If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepa�o' and xType = 'U' ) 
   Drop Table CatPasillos_Estantes_Entrepa�o 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrea�o' and xType = 'U' ) 
   Drop Table CatPasillos_Estantes_Entrea�o 
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
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepa�os' and xType = 'U' ) 
Begin 
Create Table CatPasillos_Estantes_Entrepa�os
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	
	IdEntrepa�o int Not Null, 
	DescripcionEntrepa�o varchar(50) Not Null Default '', 
    EsExclusiva bit Not Null Default 'false', 
	IdOrden int Not Null Default 9999, 

	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0	

) 
End 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_CatPasillos_Estantes_Entrepa�o' and xType = 'PK' ) 
Begin 
    Alter Table CatPasillos_Estantes_Entrepa�os Drop Constraint PK_CatPasillos_Estantes_Entrepa�o 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_CatPasillos_Estantes_Entrepa�os' and xType = 'PK' ) 
Begin 
    Alter Table CatPasillos_Estantes_Entrepa�os Add Constraint PK_CatPasillos_Estantes_Entrepa�os 
        Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepa�o ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_CatPasillos_Estantes_Entrepa�os_CatPasillos_Estantes' and xType = 'F' ) 
Begin 
    Alter Table CatPasillos_Estantes_Entrepa�os Add Constraint FK_CatPasillos_Estantes_Entrepa�os_CatPasillos_Estantes 
        Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante ) 
        References CatPasillos_Estantes ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante ) 
End 
Go--#SQL	

