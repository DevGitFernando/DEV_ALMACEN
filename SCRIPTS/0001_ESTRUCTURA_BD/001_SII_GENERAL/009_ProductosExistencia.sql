-- begin tran 
-- rollback tran 

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
	Drop Table FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN' and xType = 'U' ) 
	Drop Table FarmaciaProductos_CodigoEAN 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------------    
------------------------------------------------------------------------------------------------------------------------------------------------------    
------------------------------------------------------------------------------------------------------------------------------------------------------    
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_Skus' and xType = 'U' ) 
Begin 

	Create Table FarmaciaProductos_Skus 
	( 
		IdEmpresa varchar(3) Not Null,	
		IdEstado varchar(2) Not Null,	
		IdFarmacia varchar(4) Not Null,	
		IdTipoMovto_Inv varchar(6) Not Null, 
		Folio varchar(30) Not Null, 	

		FechaRegistro datetime Not Null default getdate(), 
		SKU varchar(100) Not Null Default '' Unique, 	

		HostName_Terminal varchar(500) Not Null Default host_name(), 
		Keyx int identity(1, 1) 
	) 

	Alter Table FarmaciaProductos_Skus Add Constraint PK_FarmaciaProductos_Skus 
		--Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoMovto_Inv, Folio ) 
		Primary Key ( SKU )

	Alter Table FarmaciaProductos_Skus Add Constraint FK_FarmaciaProductos_Skus___CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 

	Alter Table FarmaciaProductos_Skus Add Constraint FK_FarmaciaProductos_Skus__Movtos_Inv_Tipos 
		Foreign Key ( IdTipoMovto_Inv ) References Movtos_Inv_Tipos ( IdTipoMovto_Inv ) 

End 
Go--#SQL  


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
----------------------- Tablas para el manejo de Existencia
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos' and xType = 'U' ) 
	Drop Table FarmaciaProductos 
Go--#SQL  

Create Table FarmaciaProductos
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdProducto varchar(8) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	CostoPromedio Numeric(14,4) Not Null Default 0, 
	UltimoCosto Numeric(14,4) Not Null Default 0, 
	Existencia Numeric(14, 4) Not Null Default 0, 
	ExistenciaEnTransito numeric(14,4) Not Null Default 0, 
	ExistenciaSurtidos Numeric(14,4) Not Null Default 0,  
	StockMinimo Numeric(14,4) Not Null Default 0,
 	StockMaximo Numeric(14,4) Not Null Default 0,
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table FarmaciaProductos Add Constraint PK_FarmaciaProductos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdProducto )
Go--#SQL  

Alter Table FarmaciaProductos Add Constraint FK_FarmaciaProductos_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )
Go--#SQL 

---------------------------- Error garrafal 
Alter Table FarmaciaProductos Add Constraint FK_FarmaciaProductos_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )
Go--#SQL 
---------------------------- Error garrafal 

----Alter Table FarmaciaProductos Add Constraint FK_FarmaciaProductos_CatProductos_Estado 
----	Foreign Key ( IdEstado, IdProducto ) References CatProductos_Estado ( IdEstado, IdProducto )
------Go--x--#SQL 
 
Alter Table FarmaciaProductos Add Constraint FK_FarmaciaProductos_CatProductos 
	Foreign Key ( IdProducto ) References CatProductos ( IdProducto )
Go--#SQL 

Alter Table FarmaciaProductos With NoCheck 
	Add Constraint CK_FarmaciaProductos_Existencia Check Not For Replication (Existencia >= 0)
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---- Manejar Existencia por CodigoEAN 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN' and xType = 'U' ) 
	Drop Table FarmaciaProductos_CodigoEAN 
Go--#SQL 

Create Table FarmaciaProductos_CodigoEAN
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  
	Existencia Numeric(14, 4) Not Null Default 0, 
	ExistenciaEnTransito numeric(14,4) Not Null Default 0,  
	ExistenciaSurtidos Numeric(14,4) Not Null Default 0,  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table FarmaciaProductos_CodigoEAN Add Constraint PK_FarmaciaProductos_CodigoEAN 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN )
Go--#SQL 

Alter Table FarmaciaProductos_CodigoEAN Add Constraint FK_FarmaciaProductos_CodigoEAN_FarmaciaProductos 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdProducto ) References FarmaciaProductos ( IdEmpresa, IdEstado, IdFarmacia, IdProducto ) 
Go--#SQL 

Alter Table FarmaciaProductos_CodigoEAN Add Constraint FK_FarmaciaProductos_CodigoEAN_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 

Alter Table FarmaciaProductos_CodigoEAN With NoCheck 
	Add Constraint CK_FarmaciaProductos_CodigoEAN_Existencia Check Not For Replication (Existencia >= 0)
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---- Manejar Existencia por CodigoEAN Lotes 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes' and xType = 'U' ) 
	Drop Table FarmaciaProductos_CodigoEAN_Lotes 
Go--#SQL 

Create Table FarmaciaProductos_CodigoEAN_Lotes
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	EsConsignacion bit Not Null Default 'false',  	
	CostoPromedio Numeric(14,4) Not Null Default 0, 
	UltimoCosto Numeric(14,4) Not Null Default 0, 	
	Existencia Numeric(14, 4) Not Null Default 0, 
	ExistenciaEnTransito numeric(14,4) Not Null Default 0, 
	ExistenciaSurtidos Numeric(14,4) Not Null Default 0,  
	FechaCaducidad datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),  
    IdPersonal varchar(4) Not Null, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table FarmaciaProductos_CodigoEAN_Lotes Add Constraint PK_FarmaciaProductos_CodigoEAN_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote )
Go--#SQL 

Alter Table FarmaciaProductos_CodigoEAN_Lotes Add Constraint FK_FarmaciaProductos_CodigoEAN_Lotes_FarmaciaProductos_CodigoEAN 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN ) 
	References FarmaciaProductos_CodigoEAN ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN ) 
Go--#SQL  

Alter Table FarmaciaProductos_CodigoEAN_Lotes Add Constraint FK_FarmaciaProductos_CodigoEAN_Lotes_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

Alter Table FarmaciaProductos_CodigoEAN_Lotes With NoCheck 
	Add Constraint CK_FarmaciaProductos_CodigoEAN_Lotes_Existencia Check Not For Replication (Existencia >= 0)
Go--#SQL 


Alter Table FarmaciaProductos_CodigoEAN_Lotes Add Constraint FK_FarmaciaProductos_CodigoEAN_Lotes_CatFarmacias_SubFarmacias 
	Foreign Key ( IdEstado, IdFarmacia, IdSubFarmacia ) References CatFarmacias_SubFarmacias ( IdEstado, IdFarmacia, IdSubFarmacia ) 
Go--#SQL  



If Exists ( Select So.Name From Sysobjects So (NoLock) Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id )  
		Where So.Name = 'FarmaciaProductos_CodigoEAN_Lotes' and Sc.Name = 'SKU'  )
	Begin 

		If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'FK_FarmaciaProductos_CodigoEAN_Lotes___FarmaciaProductos_Skus' and xType = 'F' ) 
		Begin 

			Alter Table FarmaciaProductos_CodigoEAN_Lotes Add Constraint FK_FarmaciaProductos_CodigoEAN_Lotes___FarmaciaProductos_Skus 
				Foreign Key ( SKU ) References FarmaciaProductos_Skus ( SKU ) 

		End 

	End 

Go--#SQL  



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---- Manejar Existencia por Ubicacion CodigoEAN Lotes 
---- Manejar Existencia por CodigoEAN Lotes con Ubicaciones 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
Go--#SQL  

Create Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	EsConsignacion bit Not Null Default 'false',  	
	
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null, 
		
	Existencia Numeric(14, 4) Not Null Default 0,  
	ExistenciaEnTransito numeric(14,4) Not Null Default 0, 
	ExistenciaSurtidos Numeric(14,4) Not Null Default 0,  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones Add Constraint PK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL 

Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones Add Constraint FK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_FarmaciaProductos_CodigoEAN 
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
    References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  

Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones With NoCheck 
    Add Constraint CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_Existencia Check Not For Replication (Existencia >= 0) 
Go--#SQL 

Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones Add Constraint FK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_CatPasillos_Estantes_Entrepaños
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
	References CatPasillos_Estantes_Entrepaños ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño )
Go--#SQL 



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_CodigoEAN_Lotes__Historico' and xType = 'U' ) 
Begin 
	Create Table FarmaciaProductos_CodigoEAN_Lotes__Historico
	(
		FechaOperacion datetime Not Null Default getdate(), 	
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdSubFarmacia varchar(2) Not Null, 	
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null, 
		ClaveLote varchar(30) Not Null, 		
		EsConsignacion bit Not Null Default 'false',  	
		ExistenciaDisponible Numeric(14, 4) Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0, 
		FechaControl datetime Not Null Default getdate()  
	)

	--	Drop table FarmaciaProductos_CodigoEAN_Lotes__Historico 

	Alter Table FarmaciaProductos_CodigoEAN_Lotes__Historico Add Constraint PK_FarmaciaProductos_CodigoEAN_Lotes__Historico   
		Primary Key ( FechaOperacion, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote )  

	Alter Table FarmaciaProductos_CodigoEAN_Lotes__Historico 
		Add Constraint FK_FarmaciaProductos_CodigoEAN_Lotes__Historico______FarmaciaProductos_CodigoEAN_Lotes   
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote )   
		References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote )  

	Alter Table FarmaciaProductos_CodigoEAN_Lotes__Historico With NoCheck 
		Add Constraint CK_FarmaciaProductos_CodigoEAN_Lotes__Historico___ExistenciaDisponible 
		Check Not For Replication (ExistenciaDisponible >= 0)  

End 
Go--#SQL 



-----------------------------------------------------------------------------------------------------------------------------------------------------  
-----------------------------------------------------------------------------------------------------------------------------------------------------  
-----------------------------------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------
------------------------------------------------- AGREGAR CONSTRAINT 
-------------------------------------------------

-----------------------------------------------------------------------------------------------------------------------------------------------------  
-----------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects Where Name = 'CK_FarmaciaProductos_ExistenciaEnTransito' and xType = 'C' )  
	Alter Table FarmaciaProductos drop CONSTRAINT CK_FarmaciaProductos_ExistenciaEnTransito
Go--#SQL  
	Alter Table FarmaciaProductos WITH CHECK 
		ADD CONSTRAINT CK_FarmaciaProductos_ExistenciaEnTransito CHECK NOT FOR REPLICATION (([ExistenciaEnTransito]>=(0) And [ExistenciaEnTransito]<=[Existencia]))

	Alter Table FarmaciaProductos CHECK CONSTRAINT CK_FarmaciaProductos_ExistenciaEnTransito

Go--#SQL 

If Exists ( Select * From Sysobjects Where Name = 'CK_FarmaciaProductos_CodigoEAN_ExistenciaEnTransito' and xType = 'C' )  
	Alter Table FarmaciaProductos_CodigoEAN drop CONSTRAINT CK_FarmaciaProductos_CodigoEAN_ExistenciaEnTransito
Go--#SQL  
	Alter Table FarmaciaProductos_CodigoEAN WITH CHECK 
		ADD CONSTRAINT CK_FarmaciaProductos_CodigoEAN_ExistenciaEnTransito CHECK NOT FOR REPLICATION (([ExistenciaEnTransito]>=(0) And [ExistenciaEnTransito]<=[Existencia]))

	Alter Table FarmaciaProductos_CodigoEAN CHECK CONSTRAINT CK_FarmaciaProductos_CodigoEAN_ExistenciaEnTransito
Go--#SQL 

If Exists ( Select * From Sysobjects Where Name = 'CK_FarmaciaProductos_CodigoEAN_Lotes_ExistenciaEnTransito' and xType = 'C' )  
	Alter Table FarmaciaProductos_CodigoEAN_Lotes drop CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_ExistenciaEnTransito
Go--#SQL  
	Alter Table FarmaciaProductos_CodigoEAN_Lotes WITH CHECK 
		ADD CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_ExistenciaEnTransito CHECK NOT FOR REPLICATION (([ExistenciaEnTransito]>=(0) And [ExistenciaEnTransito]<=[Existencia]))

	Alter Table FarmaciaProductos_CodigoEAN_Lotes CHECK CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_ExistenciaEnTransito
Go--#SQL 

	

If Exists ( Select * From Sysobjects Where Name = 'CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_ExistenciaEnTransito' and xType = 'C' )
	Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones drop CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_ExistenciaEnTransito
Go--#SQL  
	Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones WITH CHECK 
		ADD CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_ExistenciaEnTransito 
		CHECK NOT FOR REPLICATION (([ExistenciaEnTransito]>=(0) And [ExistenciaEnTransito]<=[Existencia]))

	Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
		CHECK CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_ExistenciaEnTransito
Go--#SQL 



-----------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects Where Name = 'CK_FarmaciaProductos_ExistenciaSurtidos' and xType = 'C' )  
	Alter Table FarmaciaProductos drop CONSTRAINT CK_FarmaciaProductos_ExistenciaSurtidos
Go--#SQL  
	Alter Table FarmaciaProductos WITH CHECK 
		ADD CONSTRAINT CK_FarmaciaProductos_ExistenciaSurtidos CHECK NOT FOR REPLICATION (([ExistenciaSurtidos]>=(0) And [ExistenciaSurtidos]<=([Existencia] - [ExistenciaEnTransito])))

	Alter Table FarmaciaProductos CHECK CONSTRAINT CK_FarmaciaProductos_ExistenciaSurtidos
Go--#SQL 

If Exists ( Select * From Sysobjects Where Name = 'CK_FarmaciaProductos_CodigoEAN_ExistenciaSurtidos' and xType = 'C' )  
	Alter Table FarmaciaProductos_CodigoEAN drop CONSTRAINT CK_FarmaciaProductos_CodigoEAN_ExistenciaSurtidos
Go--#SQL  
	Alter Table FarmaciaProductos_CodigoEAN WITH CHECK 
		ADD CONSTRAINT CK_FarmaciaProductos_CodigoEAN_ExistenciaSurtidos CHECK NOT FOR REPLICATION (([ExistenciaSurtidos]>=(0) And [ExistenciaSurtidos]<=([Existencia] - [ExistenciaEnTransito])))

	Alter Table FarmaciaProductos_CodigoEAN CHECK CONSTRAINT CK_FarmaciaProductos_CodigoEAN_ExistenciaSurtidos
Go--#SQL 

If Exists ( Select * From Sysobjects Where Name = 'CK_FarmaciaProductos_CodigoEAN_Lotes_ExistenciaSurtidos' and xType = 'C' )  
	Alter Table FarmaciaProductos_CodigoEAN_Lotes drop CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_ExistenciaSurtidos
Go--#SQL  
	Alter Table FarmaciaProductos_CodigoEAN_Lotes WITH CHECK 
		ADD CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_ExistenciaSurtidos CHECK NOT FOR REPLICATION (([ExistenciaSurtidos]>=(0) And [ExistenciaSurtidos]<=([Existencia] - [ExistenciaEnTransito])))

	Alter Table FarmaciaProductos_CodigoEAN_Lotes CHECK CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_ExistenciaSurtidos
Go--#SQL 

	

If Exists ( Select * From Sysobjects Where Name = 'CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_ExistenciaSurtidos' and xType = 'C' )
	Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones drop CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_ExistenciaSurtidos
Go--#SQL  
	Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones WITH CHECK 
		ADD CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_ExistenciaSurtidos 
		CHECK NOT FOR REPLICATION (([ExistenciaSurtidos]>=(0) And [ExistenciaSurtidos]<=([Existencia] - [ExistenciaEnTransito])))

	Alter Table FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
		CHECK CONSTRAINT CK_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones_ExistenciaSurtidos
Go--#SQL 

