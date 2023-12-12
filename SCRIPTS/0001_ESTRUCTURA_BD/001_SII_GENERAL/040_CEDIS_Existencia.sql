------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CEDIS_Existencia_Det' and xType = 'U' ) 
   Drop Table CEDIS_Existencia_Det 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CEDIS_Existencia_Det_CodigoEAN' and xType = 'U' ) 
   Drop Table CEDIS_Existencia_Det_CodigoEAN  
Go--#SQL 

------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CEDIS_Existencia_Enc' and xType = 'U' ) 
   Drop Table CEDIS_Existencia_Enc 
Go--#SQL 

Create Table CEDIS_Existencia_Enc 
( 
    IdEmpresa varchar(3) Not Null, 
    IdEstado varchar(2) Not Null,  
    IdFarmaciaCEDIS varchar(4) Not Null, 
    Folio varchar(8) Not Null,               
    IdFarmacia varchar(4) Not Null,    
    IdPersonal varchar(4) Not Null, 
    FechaRegistro datetime Not Null Default getdate(),     
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 
 
Alter Table CEDIS_Existencia_Enc Add Constraint PK_CEDIS_Existencia_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio ) 
Go--#SQL 

Alter Table CEDIS_Existencia_Enc Add Constraint FK_CEDIS_Existencia_Enc_CatEmpresas  
    Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL  

Alter Table CEDIS_Existencia_Enc Add Constraint FK_CEDIS_Existencia_Enc_CatFarmacias  
    Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CEDIS_Existencia_Enc Add Constraint FK_CEDIS_Existencia_Enc_CatFarmacias_Aux  
    Foreign Key ( IdEstado, IdFarmaciaCEDIS ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CEDIS_Existencia_Enc Add Constraint FK_CEDIS_Existencia_Enc_CatPersonal  
    Foreign Key ( IdEstado, IdFarmacia,IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CEDIS_Existencia_Det' and xType = 'U' ) 
   Drop Table CEDIS_Existencia_Det 
Go--#SQL 

Create Table CEDIS_Existencia_Det 
( 
    IdEmpresa varchar(3) Not Null, 
    IdEstado varchar(2) Not Null,  
    IdFarmaciaCEDIS varchar(4) Not Null, 
    Folio varchar(8) Not Null,                
    
    IdClaveSSA varchar(4) Not Null, 
    Existencia numeric(14,4) Not Null Default 0, 
    ExistenciaDisponible numeric(14,4) Not Null Default 0,     
    
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0     
)
Go--#SQL 
     
Alter Table CEDIS_Existencia_Det Add Constraint PK_CEDIS_Existencia_Det Primary Key ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio, IdClaveSSA ) 
Go--#SQL 
 
Alter Table CEDIS_Existencia_Det Add Constraint FK_CEDIS_Existencia_Det_CEDIS_Existencia_Enc   
    Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio ) References CEDIS_Existencia_Enc ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio ) 
Go--#SQL  

Alter Table CEDIS_Existencia_Det Add Constraint FK_CEDIS_Existencia_Det_CatClavesSSA_Sales    
    Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL      


------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CEDIS_Existencia_Det_CodigoEAN' and xType = 'U' ) 
   Drop Table CEDIS_Existencia_Det_CodigoEAN  
Go--#SQL 

Create Table CEDIS_Existencia_Det_CodigoEAN 
( 
    IdEmpresa varchar(3) Not Null, 
    IdEstado varchar(2) Not Null,  
    IdFarmaciaCEDIS varchar(4) Not Null, 
    Folio varchar(8) Not Null,                
    
    IdProducto varchar(8) Not Null, 
    CodigoEAN varchar(30) Not Null, 
    Existencia numeric(14,4) Not Null Default 0, 
    ExistenciaDisponible numeric(14,4) Not Null Default 0,     
    
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0     
)
Go--#SQL 

Alter Table CEDIS_Existencia_Det_CodigoEAN Add Constraint PK_CEDIS_Existencia_Det_CodigoEAN 
    Primary Key ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio, IdProducto, CodigoEAN ) 
Go--#SQL 
 
Alter Table CEDIS_Existencia_Det_CodigoEAN Add Constraint FK_CEDIS_Existencia_Det_CodigoEAN_CEDIS_Existencia_Enc   
    Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio ) References CEDIS_Existencia_Enc ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio ) 
Go--#SQL  

Alter Table CEDIS_Existencia_Det_CodigoEAN Add Constraint FK_CEDIS_Existencia_Det_CatProductos_CodigosRelacionados    
    Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )
Go--#SQL      