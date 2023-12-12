------------------------------------------------------------------------------------------------------------------------------------------------------    
------------------------------------------------------------------------------------------------------------------------------------------------------    
------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__GruposDeUbicaciones_Det' and xType = 'U' ) 
   Drop Table CFGC_ALMN__GruposDeUbicaciones_Det 
Go--#SQL   



----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__GruposDeUbicaciones' and xType = 'U' ) 
   Drop Table CFGC_ALMN__GruposDeUbicaciones 
Go--#SQL   

Create Table CFGC_ALMN__GruposDeUbicaciones
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdGrupo varchar(3) Not Null, 
	NombreGrupo varchar(500) Not Null Default '', 
	--Orden int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL 


Alter Table CFGC_ALMN__GruposDeUbicaciones Add Constraint PK_CFGC_ALMN__GruposDeUbicaciones Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdGrupo ) 
Go--#SQL  

Alter Table CFGC_ALMN__GruposDeUbicaciones Add Constraint FK_CFGC_ALMN__GruposDeUbicaciones___CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL  

Alter Table CFGC_ALMN__GruposDeUbicaciones Add Constraint FK_CFGC_ALMN__GruposDeUbicaciones___CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  




------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__GruposDeUbicaciones_Det' and xType = 'U' ) 
   Drop Table CFGC_ALMN__GruposDeUbicaciones_Det 
Go--#SQL   

Create Table CFGC_ALMN__GruposDeUbicaciones_Det 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdGrupo varchar(3) Not Null, 
	IdPasillo int Not Null,
	IdEstante int Not Null,
	IdEntrepaño int Not Null,
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL  

Alter Table CFGC_ALMN__GruposDeUbicaciones_Det Add Constraint PK_CFGC_ALMN__GruposDeUbicaciones_Det Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdGrupo, IdPasillo, IdEstante, IdEntrepaño) 
Go--#SQL  

Alter Table CFGC_ALMN__GruposDeUbicaciones_Det Add Constraint FK_CFGC_ALMN__GruposDeUbicaciones_Det___CFGC_ALMN__GruposDeUbicaciones
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdGrupo ) 
	References CFGC_ALMN__GruposDeUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdGrupo ) 
Go--#SQL  

Alter Table CFGC_ALMN__GruposDeUbicaciones_Det Add Constraint FK_CFGC_ALMN__GruposDeUbicaciones_Det___CatPasillos_Estantes_Entrepaños
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) References CatPasillos_Estantes_Entrepaños ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL  
 

