


------------------------------------------------------------------------------------------------ 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Remisiones_Clientes' and xType = 'U' ) 
	
	Create Table Remisiones_Clientes 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdDistribuidor varchar(4) Not Null Default '', 
		CodigoCliente varchar(20) Not Null Default '', 
		Referencia varchar(20) Not Null Default '', 		
		FechaDocumento datetime Not Null Default getdate(), 
		EsConsignacion bit Not Null Default 0 
	) 
Go--#SQL 