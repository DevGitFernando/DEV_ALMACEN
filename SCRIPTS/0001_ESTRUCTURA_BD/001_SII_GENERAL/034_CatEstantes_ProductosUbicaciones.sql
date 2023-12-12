If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepaños' and xType = 'U' ) 
   Drop Table CatPasillos_Estantes_Entrepaños 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes' and xType = 'U' ) 
   Drop Table CatPasillos_Estantes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatUbicaciones_CodigosEAN_Lotes' and xType = 'U' ) 
   Drop Table CatUbicaciones_CodigosEAN_Lotes 
Go--#SQL 

--------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos' and xType = 'U' ) 
   Drop Table CatPasillos 
Go--#SQL 

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
Go--#SQL


Alter Table CatPasillos Add Constraint PK_CatPasillos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo ) 
Go--#SQL

---------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes' and xType = 'U' ) 
   Drop Table CatPasillos_Estantes 
Go--#SQL 

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
Go--#SQL

Alter Table CatPasillos_Estantes Add Constraint PK_CatPasillos_Estantes Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante ) 
Go--#SQL

Alter Table CatPasillos_Estantes Add Constraint FK_CatPasillos_Estantes_CatPasillos 
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo ) 
    References CatPasillos ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo ) 
Go--#SQL

---------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPasillos_Estantes_Entrepaños' and xType = 'U' ) 
   Drop Table CatPasillos_Estantes_Entrepaños 
Go--#SQL 

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
	EsDePickeo bit Not Null Default 'false',  

	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0	

) 
Go--#SQL

Alter Table CatPasillos_Estantes_Entrepaños Add Constraint PK_CatPasillos_Estantes_Entrepaños 
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL

Alter Table CatPasillos_Estantes_Entrepaños Add Constraint FK_CatPasillos_Estantes_Entrepaños_CatPasillos_Estantes 
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante ) 
    References CatPasillos_Estantes ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante ) 
Go--#SQL	


---------  Insertar Datos 
Declare 
    @sIdEmpresa varchar(3), 
    @sIdEstado varchar(2), 
    @sIdFarmacia varchar(4), 
    @Pasillo int, 
    @Estante int, 
    @Entrepaño int          

    Set @sIdEmpresa = '001' 
    Set @sIdEstado = '25' 
    Set @sIdFarmacia = '0010'     
    Set @Pasillo = 1 
    Set @Estante = 1 
    Set @Entrepaño = 1     

/* 

    Insert Into CatPasillos 
    Select @sIdEmpresa, @sIdEstado, @sIdFarmacia, @Pasillo, 'Pasillo ' + cast(@Pasillo as varchar), 'A', 0 
    --From CatPasillos 
    
    Insert Into CatPasillos_Estantes     
    Select @sIdEmpresa, @sIdEstado, @sIdFarmacia, @Pasillo, @Estante, 'Estante ' + cast(@Estante as varchar), 'A', 0 
    --From CatPasillos_Estantes 
    
    Insert Into CatPasillos_Estantes_Entrepaños     
    Select @sIdEmpresa, @sIdEstado, @sIdFarmacia, @Pasillo, @Estante, @Entrepaño, 'Entrepaño ' + cast(@Entrepaño as varchar), 'A', 0 
    --From CatPasillos_Estantes_Entrepaños     


    Select * 
    From CatPasillos 
    
    Select * 
    From CatPasillos_Estantes 
    
    Select * 
    From CatPasillos_Estantes_Entrepaños         
*/ 


 