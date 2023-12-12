If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_IFarmatel_Conexion' and xType = 'U' ) 
   Drop Table INT_IFarmatel_Conexion 
Go--#SQL 

Create Table INT_IFarmatel_Conexion 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Login varchar(50) Not Null Default '', 
	Password varchar(1000) Not Null Default '', 
	Url_Servicio varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default 'A'   
) 
Go--#SQL 

Alter Table INT_IFarmatel_Conexion Add Constraint PK_INT_IFarmatel_Conexion Primary Key ( IdEstado, IdFarmacia, Status ) 
Go--#SQL 

Alter Table INT_IFarmatel_Conexion Add Constraint FK_INT_IFarmatel_Conexion___CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 


Insert Into INT_IFarmatel_Conexion 
Select '09', '0011', 'Test', 'prueba', 'http://efarmacaprepa.cloudapp.net/wsSolicitudesDeServicio/wsRecepcionDeSolicitudes.asmx', 'A'  
Go--#SQL 

