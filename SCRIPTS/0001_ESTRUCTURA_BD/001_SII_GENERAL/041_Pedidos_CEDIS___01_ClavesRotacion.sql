------------------------------------------------------------------------------------------------------------------------------------------------------    
------------------------------------------------------------------------------------------------------------------------------------------------------    
------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__Rotacion_Claves' and xType = 'U' ) 
   Drop Table CFGC_ALMN__Rotacion_Claves 
Go--#SQL   



----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__Rotacion' and xType = 'U' ) 
   Drop Table CFGC_ALMN__Rotacion 
Go--#SQL   

Create Table CFGC_ALMN__Rotacion 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdRotacion varchar(3) Not Null, 
	NombreRotacion varchar(500) Not Null Default '', 
	Orden int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL 


Alter Table CFGC_ALMN__Rotacion Add Constraint PK_CFGC_ALMN__Rotacion Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdRotacion ) 
Go--#SQL  

Alter Table CFGC_ALMN__Rotacion Add Constraint FK_CFGC_ALMN__Rotacion___CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL  

Alter Table CFGC_ALMN__Rotacion Add Constraint FK_CFGC_ALMN__Rotacion___CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  



If Not Exists ( Select * From CFGC_ALMN__Rotacion Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0003' and IdRotacion = '1' )  Insert Into CFGC_ALMN__Rotacion (  IdEmpresa, IdEstado, IdFarmacia, IdRotacion, NombreRotacion, Orden, Status, Actualizado )  Values ( '001', '13', '0003', '1', 'ALTA', 1, 'A', 0 ) 
 Else Update CFGC_ALMN__Rotacion Set NombreRotacion = 'ALTA', Orden = 1, Status = 'A', Actualizado = 0 Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0003' and IdRotacion = '1'
If Not Exists ( Select * From CFGC_ALMN__Rotacion Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0003' and IdRotacion = '2' )  Insert Into CFGC_ALMN__Rotacion (  IdEmpresa, IdEstado, IdFarmacia, IdRotacion, NombreRotacion, Orden, Status, Actualizado )  Values ( '001', '13', '0003', '2', 'MEDIA', 2, 'A', 0 ) 
 Else Update CFGC_ALMN__Rotacion Set NombreRotacion = 'MEDIA', Orden = 2, Status = 'A', Actualizado = 0 Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0003' and IdRotacion = '2'
If Not Exists ( Select * From CFGC_ALMN__Rotacion Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0003' and IdRotacion = '3' )  Insert Into CFGC_ALMN__Rotacion (  IdEmpresa, IdEstado, IdFarmacia, IdRotacion, NombreRotacion, Orden, Status, Actualizado )  Values ( '001', '13', '0003', '3', 'BAJA', 3, 'A', 0 ) 
 Else Update CFGC_ALMN__Rotacion Set NombreRotacion = 'BAJA', Orden = 3, Status = 'A', Actualizado = 0 Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0003' and IdRotacion = '3'
If Not Exists ( Select * From CFGC_ALMN__Rotacion Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0003' and IdRotacion = '4' )  Insert Into CFGC_ALMN__Rotacion (  IdEmpresa, IdEstado, IdFarmacia, IdRotacion, NombreRotacion, Orden, Status, Actualizado )  Values ( '001', '13', '0003', '4', 'NULA -  NUEVO', 4, 'A', 0 ) 
 Else Update CFGC_ALMN__Rotacion Set NombreRotacion = 'NULA -  NUEVO', Orden = 4, Status = 'A', Actualizado = 0 Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0003' and IdRotacion = '4'
Go--#SQL  




------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_ALMN__Rotacion_Claves' and xType = 'U' ) 
   Drop Table CFGC_ALMN__Rotacion_Claves 
Go--#SQL   

Create Table CFGC_ALMN__Rotacion_Claves 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdRotacion varchar(3) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL  

Alter Table CFGC_ALMN__Rotacion_Claves Add Constraint PK_CFGC_ALMN__Rotacion_Claves Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdRotacion, IdClaveSSA ) 
Go--#SQL  

Alter Table CFGC_ALMN__Rotacion Add Constraint FK_CFGC_ALMN__Rotacion_Claves___CFGC_ALMN__Rotacion 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdRotacion ) 
	References CFGC_ALMN__Rotacion ( IdEmpresa, IdEstado, IdFarmacia, IdRotacion ) 
Go--#SQL  

Alter Table CFGC_ALMN__Rotacion_Claves Add Constraint FK_CFGC_ALMN__Rotacion_Claves___CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL  
 

