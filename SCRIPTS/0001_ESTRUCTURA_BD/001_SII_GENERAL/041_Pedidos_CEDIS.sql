---------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc_Surtido_Atenciones' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Enc_Surtido_Atenciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido_Distribucion' and xType = 'U' ) 
	Drop Table Pedidos_Cedis_Det_Surtido_Distribucion 
Go--#SQL 	

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Det_Surtido 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc_Surtido' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Enc_Surtido 
Go--#SQL    

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Det_Surtido 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Det 
Go--#SQL   


----------------------------------------------------------------------------------------------   
---------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'CatCajasDistribucion' and So.xType = 'U' ) 
   Drop Table CatCajasDistribucion 
Go--#SQL 

Create Table CatCajasDistribucion 
( 
	IdEstado Varchar(2) Not Null,
	IdFarmacia Varchar(4) Not Null,
	IdCaja Varchar(8) Not Null,
	FechaRegistro Datetime Default GetDate(),
	Disponible bit Not Null Default 'true',
	Habilitada bit Not Null Default 'true',
	Status Varchar(1) Not Null Default 'A',
	Actualizado Tinyint Not Null Default 0
) 
Go--#SQL 

Alter Table CatCajasDistribucion Add Constraint PK_CatCajasDistribucion Primary Key ( IdEstado, IdFarmacia, IdCaja )     
Go--#SQL

Alter Table CatCajasDistribucion Add Constraint FK_CatCajasDistribucion__CatEstados
	Foreign Key ( IdEstado ) References CatEstados    
Go--#SQL

Alter Table CatCajasDistribucion Add Constraint FK_CatCajasDistribucion__CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias    
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (nolock) Where Name = 'Pedidos_Status' and xType = 'U' ) 
   Drop Table Pedidos_Status 
Go--#SQL 

Create Table Pedidos_Status 
( 
	IdPedidoStatus int Not Null, 
	ClaveStatus varchar(2) Not Null, 
	Descripcion varchar(100) Not Null Unique, 
	Status varchar(1) Not Null Default '' 
) 
Go--#SQL    

Alter Table Pedidos_Status Add Constraint PK_Pedidos_Status Primary Key ( IdPedidoStatus ) 
Go--#SQL    


	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 1, 'A', 'SURTIMIENTO', 'A' 

	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 2, 'S', 'SURTIDO', 'A' 
	
	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 3, 'V', 'EN VALIDACIÓN', 'A' 
	
	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 4, 'D', 'DOCUMENTACIÓN', 'A' 		

	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 5, 'E', 'EMBARQUES', 'A' 
		
	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 6, 'T', 'TRANSITO', 'A' 		
		
	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 7, 'R', 'REGISTRADO', 'A' 
	
	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 8, 'F', 'FINALIZADO', 'A' 	

	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 9, 'C', 'CANCELADO', 'A' 	
	
	Insert Into Pedidos_Status ( IdPedidoStatus, ClaveStatus, Descripcion, Status ) 
	Select 10, 'P', 'PEDIDO ENVIADO', 'A' 	
	
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'Pedidos_Prioridades' and xType = 'U' ) 
Begin 
	Create Table Pedidos_Prioridades  
	(
		Prioridad int Not Null Default 1, 
		Descripcion varchar(100) Not Null Default '', 
		Color varchar(50) Not Null Default '',  
		Status varchar(1) Not Null Default 'A'  
	) 

	Alter Table Pedidos_Prioridades Add Constraint PK_Pedidos_Prioridades Primary Key ( Prioridad ) 
End 
Go--#SQL 



If Not Exists ( Select * From Pedidos_Prioridades Where Prioridad = 1 )  Insert Into Pedidos_Prioridades (  Prioridad, Descripcion, Color, Status )  Values ( 1, 'NORMAL', '', 'A' ) Else Update Pedidos_Prioridades Set Descripcion = 'NORMAL', Color = '', Status = 'A' Where Prioridad = 1
If Not Exists ( Select * From Pedidos_Prioridades Where Prioridad = 2 )  Insert Into Pedidos_Prioridades (  Prioridad, Descripcion, Color, Status )  Values ( 2, 'COMPLEMENTO', '', 'A' ) Else Update Pedidos_Prioridades Set Descripcion = 'COMPLEMENTO', Color = '', Status = 'A' Where Prioridad = 2
If Not Exists ( Select * From Pedidos_Prioridades Where Prioridad = 3 )  Insert Into Pedidos_Prioridades (  Prioridad, Descripcion, Color, Status )  Values ( 3, 'URGENTE', '', 'A' ) Else Update Pedidos_Prioridades Set Descripcion = 'URGENTE', Color = '', Status = 'A' Where Prioridad = 3
Go--#SQL 



----------------------------------------------------------------------------------------------   
---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Enc 
Go--#SQL   

Create Table Pedidos_Cedis_Enc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	IdEstadoSolicita varchar(2) Not Null Default '', 
	IdFarmaciaSolicita varchar(4) Not Null default '', 
	FolioPedido varchar(6) Not Null, 
	FechaRegistro datetime Not Null Default getdate(), 
	EsTransferencia bit Not Null Default 1, 
	IdCliente varchar(4) Not Null Default '', 
	IdSubCliente varchar(4) Not Null Default '', 
	IdPrograma varchar(4) Not Null Default '', 
	IdSubPrograma varchar(4) Not Null Default '', 	
	IdBeneficiario varchar(8) Not Null Default '',   
	IdPersonal varchar(4) Not Null, 	
	Observaciones varchar(200) Not Null Default '', 

	TipoDeClavesDePedido int Not Null Default 0, 
	ReferenciaInterna varchar(100) Not Null Default '', 
	FechaEntrega datetime Not Null Default GetDate(), 
	CajasCompletas Bit Not Null Default 0,

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table Pedidos_Cedis_Enc Add Constraint PK_Pedidos_Cedis_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Enc Add Constraint FK_Pedidos_Cedis_Enc_CatEmpresas  
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL  	

Alter Table Pedidos_Cedis_Enc Add Constraint FK_Pedidos_Cedis_Enc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) 
	References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Enc Add Constraint FK_Pedidos_Cedis_Enc__CatFarmacias_Solicita 
	Foreign Key ( IdEstadoSolicita, IdFarmaciaSolicita ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Enc Add Constraint FK_Pedidos_Cedis_Enc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  


----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Det 
Go--#SQL   

Create Table Pedidos_Cedis_Det 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioPedido varchar(6) Not Null, 
	IdClaveSSA varchar(4) Not Null,
	ClaveSSA varchar(50) Not Null Default '',  
	ContenidoPaquete int Not Null Default 0, 
	Cantidad int Not Null Default 0,   -- Cajas Completas 
	Existencia numeric(14,4) Not Null Default 0, 	
	CantidadEnCajas int Not Null Default 0, 
	PCM Numeric(14,4) Not Null Default 0,  
	PcmAplicado bit Not Null Default 0, 		

	ExistenciaSugerida Numeric(14, 4) Not Null default 0.0000, 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  ---- El default 1 es conciente, este campo se actualiza en un proceso posterior a la captura de la informacion  
) 
Go--#SQL  

Alter Table Pedidos_Cedis_Det Add Constraint PK_Pedidos_Cedis_Det Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdClaveSSA ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Det Add Constraint FK_Pedidos_Cedis_Det_Pedidos_Cedis_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
	References Pedidos_Cedis_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Det Add Constraint FK_Pedidos_Cedis_Det_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL  
 


----------------------------------------------------------------------------------------------   
---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc_Surtido' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Enc_Surtido 
Go--#SQL   

Create Table Pedidos_Cedis_Enc_Surtido 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioSurtido varchar(8) Not Null, 
	IdFarmaciaPedido varchar(4) Not Null, 
	FolioPedido varchar(6) Not Null, 
	FolioTransferenciaReferencia varchar(30) Not Null Default '', 	
	FechaRegistro datetime Not Null Default getdate(), 
	IdPersonal varchar(4) Not Null, 	
	Observaciones varchar(200) Not Null Default '', 
	IdPersonalCEDIS varchar(4) Not Null,  

	MesesCaducidad smallint Not Null Default 0, 
	MesesCaducidad_Consigna smallint Not Null Default 0,   

	PedidoNoAdministrado bit Not Null Default 'false', 
	ModificacionesCaptura int Not Null Default 0, 
	IdPersonalSurtido varchar(4) Not Null Default '0000', 
	IdPersonalTransporte varchar(4) Not Null Default '0000', 

	EsManual bit DEFAULT 0  Not Null, 
	TipoDeUbicaciones int Not Null Default 0, 
	Prioridad int Not Null Default 1, 
	TipoDeInventario int Not Null Default 0, 
	IdGrupo Varchar(3) Not Null Default '000', 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table Pedidos_Cedis_Enc_Surtido Add Constraint PK_Pedidos_Cedis_Enc_Surtido Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Enc_Surtido Add Constraint FK_Pedidos_Cedis_Enc_Surtido_Pedidos_Cedis_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaPedido, FolioPedido ) 
	References Pedidos_Cedis_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Enc_Surtido Add Constraint FK_Pedidos_Cedis_Enc_Surtido_CatEmpresas  
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL  	

Alter Table Pedidos_Cedis_Enc_Surtido Add Constraint FK_Pedidos_Cedis_Enc_Surtido_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) 
	References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Enc_Surtido Add Constraint FK_Pedidos_Cedis_Enc_Surtido_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  


Alter Table Pedidos_Cedis_Enc_Surtido With NoCheck Add Constraint FK_Pedidos_Cedis_Enc___CatPersonalCEDIS_Surtido  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPersonalSurtido ) 
	References CatPersonalCEDIS (  IdEmpresa, IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  
	
Alter Table Pedidos_Cedis_Enc_Surtido With NoCheck Add Constraint FK_Pedidos_Cedis_Enc___CatPersonalCEDIS_Transporte  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPersonalTransporte ) 
	References CatPersonalCEDIS ( IdEmpresa, IdEstado, IdFarmacia, IdPersonal )  
Go--#SQL  


----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Det_Surtido 
Go--#SQL   

Create Table Pedidos_Cedis_Det_Surtido 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioSurtido varchar(8) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	ClaveSSA varchar(20) Not Null Default '', 
	--CantidadSolicitada int Not Null Default 0,   -- Cajas Completas 
    --CantidadSugerida int Not Null Default 0, 
    CantidadAsignada int Not Null Default 0,     	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  ---- El default 1 es conciente, este campo se actualiza en un proceso posterior a la captura de la informacion  
) 
Go--#SQL  

Alter Table Pedidos_Cedis_Det_Surtido Add Constraint PK_Pedidos_Cedis_Det_Surtido 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdClaveSSA ) 
Go--#SQL  

Alter Table Pedidos_Cedis_Det_Surtido Add Constraint FK_Pedidos_Cedis_Det_Surtido__Pedidos_Cedis_Enc_Surtido 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido ) 
	References Pedidos_Cedis_Enc_Surtido ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido )  
Go--#SQL 


/* 
Alter Table Pedidos_Cedis_Det_Surtido Add Constraint FK_Pedidos_Cedis_Det_Surtido_Pedidos_Cedis_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdFolioSurtido, IdClaveSSA ) 
	References Pedidos_Cedis_Det ( IdEmpresa, IdEstado, IdFarmacia, IdFolioSurtido, IdClaveSSA ) 
Go--#--SQL  
*/ 


----------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido_Distribucion' and xType = 'U' ) 
	Drop Table Pedidos_Cedis_Det_Surtido_Distribucion 
Go--#SQL 	 

Create Table Pedidos_Cedis_Det_Surtido_Distribucion 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		FolioSurtido varchar(8) Not Null, 
		IdSurtimiento int Not Null, 
		ClaveSSA varchar(20) Not Null, 
		IdSubFarmacia varchar(2) Not Null, 
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null, 
		ClaveLote varchar(30) Not Null, 
		FechaCaducidad datetime Not Null Default getdate(),
		EsConsignacion bit Not Null Default 0, 
		IdPasillo int Not Null, 
		IdEstante int Not Null, 
		IdEntrepaño int Not Null, 	
		CantidadRequerida int Not Null Default 0, 
		CantidadAsignada int Not Null Default 0, 
		Observaciones varchar(100) Not Null Default '', 
		IdCaja Varchar(8) Not Null Default '00000000', 
		Validado bit Not Null Default 'false', 
		Keyx int identity (1,1), 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0  ---- El default 1 es conciente, este campo se actualiza en un proceso posterior a la captura de la informacion  
	) 
Go--#SQL 	 

Alter Table Pedidos_Cedis_Det_Surtido_Distribucion Add Constraint PK_Pedidos_Cedis_Det_Surtido_Distribucion 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioSurtido, IdSurtimiento, 
				  ClaveSSA, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL 	 

Alter Table Pedidos_Cedis_Det_Surtido_Distribucion Add Constraint FK_Pedidos_Cedis_Det_Surtido_Distribucion__Pedidos_Cedis_Enc_Surtido 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido ) 
	References Pedidos_Cedis_Enc_Surtido ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido )  
Go--#SQL 

----Alter Table Pedidos_Cedis_Det_Surtido_Distribucion Add Constraint FK_Pedidos_Cedis_Det_Surtido_Distribucion__CatCajasDistribucion
----	Foreign Key ( IdEstado, IdFarmacia, IdCaja ) 
----	References CatCajasDistribucion ( IdEstado, IdFarmacia, IdCaja )  
----Go--x#SQL 		

Alter Table Pedidos_Cedis_Det_Surtido_Distribucion WITH NOCHECK 
	ADD CONSTRAINT CK_Pedidos_Cedis_Det_Surtido_Distribucion____CantidadRequerida CHECK NOT FOR REPLICATION ( [CantidadRequerida] >= [CantidadAsignada]  )
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso 
Go--#SQL    

Begin 
	Create Table Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso  
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		FolioSurtido varchar(8) Not Null, 
		IdSurtimiento int Not Null, 	
		FechaRegistro datetime Not Null Default getdate(),  	
		IdPersonal varchar(4) Not Null Default '', 
		ClaveSSA varchar(20) Not Null, 
		IdSubFarmacia varchar(2) Not Null, 
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null, 
		ClaveLote varchar(30) Not Null, 
		FechaCaducidad datetime Not Null Default getdate(),
		EsConsignacion bit Not Null Default 0, 
		IdPasillo int Not Null, 
		IdEstante int Not Null, 
		IdEntrepaño int Not Null, 	
		CantidadRequerida int Not Null Default 0, 
		CantidadAsignada int Not Null Default 0, 
		Keyx int Not Null Default 0, 
		Keyx_Reproceso int identity (1,1) 
	) 

	Alter Table Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso 
		Add Constraint PK_Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioSurtido, IdSurtimiento, FechaRegistro, 
					  ClaveSSA, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 

	Alter Table Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso 
		Add Constraint FK_Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso____Pedidos_Cedis_Det_Surtido_Distribucion 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioSurtido, IdSurtimiento, 
					  ClaveSSA, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 
		References Pedidos_Cedis_Det_Surtido_Distribucion ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioSurtido, IdSurtimiento, 
					  ClaveSSA, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño  )  

End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc_Surtido_Atenciones' and xType = 'U' ) 
   Drop Table Pedidos_Cedis_Enc_Surtido_Atenciones 
Go--#SQL 

Create Table Pedidos_Cedis_Enc_Surtido_Atenciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioSurtido varchar(8) Not Null, 
	IdPersonal varchar(4) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate(), 
	 HostName varchar(100) Not Null Default host_name(), 
	Keyx int identity (1,1), 
	Status varchar(1) Not Null Default 'A',  ---- Se toma de la tabla  Pedidos_Cedis_Enc_Surtido
	Actualizado tinyint Not Null Default 0  ---- El default 1 es conciente, este campo se actualiza en un proceso posterior a la captura de la informacion 
) 
Go--#SQL	

Alter Table Pedidos_Cedis_Enc_Surtido_Atenciones Add Constraint PK_Pedidos_Cedis_Enc_Surtido_Atenciones 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, FechaRegistro )  
Go--#SQL
		
Alter Table Pedidos_Cedis_Enc_Surtido_Atenciones Add Constraint FK_Pedidos_Cedis_Enc_Surtido_Atenciones___Pedidos_Cedis_Enc_Surtido  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido ) 
	References Pedidos_Cedis_Enc_Surtido ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido )   
Go--#SQL


