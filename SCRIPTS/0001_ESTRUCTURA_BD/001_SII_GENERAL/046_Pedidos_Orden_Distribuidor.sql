
---------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosOrdenDist_Det_Surtido' and xType = 'U' ) 
   Drop Table PedidosOrdenDist_Det_Surtido 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosOrdenDist_Det' and xType = 'U' ) 
   Drop Table PedidosOrdenDist_Det 
Go--#SQL   


----------------------------------------------------------------------------------------------   
---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosOrdenDist_Enc' and xType = 'U' ) 
   Drop Table PedidosOrdenDist_Enc 
Go--#SQL   

Create Table PedidosOrdenDist_Enc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioPedido varchar(6) Not Null, 
	FechaRegistro datetime Not Null Default getdate(), 
	IdPersonal varchar(4) Not Null, 	
	Observaciones varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table PedidosOrdenDist_Enc Add Constraint PK_PedidosOrdenDist_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table PedidosOrdenDist_Enc Add Constraint FK_PedidosOrdenDist_Enc_CatEmpresas  
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL  	

Alter Table PedidosOrdenDist_Enc Add Constraint FK_PedidosOrdenDist_Enc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) 
	References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table PedidosOrdenDist_Enc Add Constraint FK_PedidosOrdenDist_Enc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  



----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosOrdenDist_Det' and xType = 'U' ) 
   Drop Table PedidosOrdenDist_Det 
Go--#SQL   

Create Table PedidosOrdenDist_Det 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioPedido varchar(6) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	Cantidad int Not Null Default 0,   -- Cajas Completas 
	Existencia numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  ---- El default 1 es conciente, este campo se actualiza en un proceso posterior a la captura de la informacion  
) 
Go--#SQL  

Alter Table PedidosOrdenDist_Det Add Constraint PK_PedidosOrdenDist_Det Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdClaveSSA ) 
Go--#SQL  

Alter Table PedidosOrdenDist_Det Add Constraint FK_PedidosOrdenDist_Det_PedidosOrdenDist_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
	References PedidosOrdenDist_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table PedidosOrdenDist_Det Add Constraint FK_PedidosOrdenDist_Det_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL  


----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosOrdenDist_Det_Surtido' and xType = 'U' ) 
   Drop Table PedidosOrdenDist_Det_Surtido 
Go--#SQL   

Create Table PedidosOrdenDist_Det_Surtido 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioPedido varchar(6) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	CantidadSolicitada int Not Null Default 0,   -- Cajas Completas 
    CantidadSugerida int Not Null Default 0, 
    CantidadAsignada int Not Null Default 0,     	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  ---- El default 1 es conciente, este campo se actualiza en un proceso posterior a la captura de la informacion  
) 
Go--#SQL  

Alter Table PedidosOrdenDist_Det_Surtido Add Constraint PK_PedidosOrdenDist_Det_Surtido Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdClaveSSA ) 
Go--#SQL  

Alter Table PedidosOrdenDist_Det_Surtido Add Constraint FK_PedidosOrdenDist_Det_Surtido_PedidosOrdenDist_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido,IdClaveSSA ) 
	References PedidosOrdenDist_Det ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdClaveSSA ) 
Go--#SQL  


