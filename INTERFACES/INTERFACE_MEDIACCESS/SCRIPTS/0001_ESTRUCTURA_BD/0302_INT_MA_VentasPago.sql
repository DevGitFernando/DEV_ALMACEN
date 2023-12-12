--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA_VentasPago' and xType = 'U' ) 
   Drop Table INT_MA_VentasPago  
Go--#SQL   

Create Table INT_MA_VentasPago   
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 
	IdFormasDePago varchar(2) Not Null Default '',  
	Importe Numeric(14,4) Not Null Default 0,
	Referencia Varchar(100) Not Null Default '',
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 


--Alter Table INT_MA_VentasPago Add Constraint PK_INT_MA_VentasPago Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
--Go--#SQL

Alter Table INT_MA_VentasPago Add Constraint FK_INT_MA_VentasPago_VentasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL

Alter Table INT_MA_VentasPago Add Constraint FK_INT_MA_VentasPago_CatFormasDePago
	Foreign Key ( IdFormasDePago ) References CatFormasDePago ( IdFormasDePago ) 
Go--#SQL  