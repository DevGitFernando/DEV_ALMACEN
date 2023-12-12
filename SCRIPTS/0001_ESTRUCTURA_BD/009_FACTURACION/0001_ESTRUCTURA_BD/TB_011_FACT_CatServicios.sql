

------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'FACT_CatServicios' and So.xType = 'U' ) 
   Drop Table FACT_CatServicios 
Go--#SQL 

Create Table FACT_CatServicios 
( 
    IdServicio varchar(3) Not Null, 
    Descripcion varchar(100) Not Null Default '',    
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table FACT_CatServicios Add Constraint PK_FACT_CatServicios Primary Key ( IdServicio )     
Go--#SQL 