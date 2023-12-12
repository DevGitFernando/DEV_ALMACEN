Set NoCount On 
Go--#SQL   

--------------------------------------------------------------------------------------------------------------------------------------------  
--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__PROV_Mayoristas__Farmacias' and xType = 'U' ) 
   Drop Table INT_MA__PROV_Mayoristas__Farmacias  
Go--#SQL  



--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__PROV_Mayoristas' and xType = 'U' ) 
   Drop Table INT_MA__PROV_Mayoristas  
Go--#SQL   

Create Table INT_MA__PROV_Mayoristas   
( 
	IdProveedor varchar(4) Not Null Default '', 
	UrlEnvio varchar(200) Not Null Default '', 
	Usuario varchar(100) Not Null Default '', 
	Password varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default ''  	
) 
Go--#SQL   

Alter Table INT_MA__PROV_Mayoristas Add Constraint PK_INT_MA__PROV_Mayoristas Primary Key ( IdProveedor ) 
Go--#SQL   



--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__PROV_Mayoristas__Farmacias' and xType = 'U' ) 
   Drop Table INT_MA__PROV_Mayoristas__Farmacias  
Go--#SQL   

Create Table INT_MA__PROV_Mayoristas__Farmacias   
( 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	IdProveedor varchar(4) Not Null Default '', 
	CodigoCliente varchar(50) Not Null Default '', 	
	NombreCliente varchar(200) Not Null Default '' 
) 
Go--#SQL   
	
Alter Table INT_MA__PROV_Mayoristas__Farmacias 
	Add Constraint PK_INT_MA__PROV_Mayoristas__Farmacias 
	Primary Key ( IdEstado, IdFarmacia, IdProveedor, CodigoCliente ) 
Go--#SQL   	

Alter Table INT_MA__PROV_Mayoristas__Farmacias 
	Add Constraint FK_INT_MA__PROV_Mayoristas__Farmacias_____CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL   	

Alter Table INT_MA__PROV_Mayoristas__Farmacias 
	Add Constraint FK_INT_MA__PROV_Mayoristas__Farmacias_____INT_MA__PROV_Mayoristas 
	Foreign Key ( IdProveedor ) References INT_MA__PROV_Mayoristas ( IdProveedor )  
Go--#SQL   	

