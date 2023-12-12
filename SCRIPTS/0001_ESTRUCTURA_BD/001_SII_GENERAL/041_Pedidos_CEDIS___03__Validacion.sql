----------------------------------------------------------------------------------------------   
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Validacion' and xType = 'U' ) 
Begin 
	Create Table Pedidos_Cedis_Validacion
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		FolioSurtido varchar(8) Not Null, 
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null, 
		ClaveLote varchar(30) Not Null, 
		Cantidad int Not Null Default 0
	) 

	Alter Table Pedidos_Cedis_Validacion Add Constraint PK_Pedidos_Cedis_Validacion
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdProducto, CodigoEAN, ClaveLote ) 

	Alter Table Pedidos_Cedis_Validacion Add Constraint FK_Pedidos_Cedis_Validacion___Pedidos_Cedis_Enc_Surtido 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido ) 
		References Pedidos_Cedis_Enc_Surtido ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido )  

End 
Go--#SQL 