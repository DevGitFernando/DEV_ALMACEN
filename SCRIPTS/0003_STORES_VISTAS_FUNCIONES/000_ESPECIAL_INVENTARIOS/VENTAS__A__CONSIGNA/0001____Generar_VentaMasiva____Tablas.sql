

	
	If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'VentaMasiva_ExcluirClaves' and xType = 'U' )  
	Begin 	

		Create Table VentaMasiva_ExcluirClaves  
		(
			IdEstado Varchar(2) Not Null,
			IdFarmacia Varchar(4) Not Null,
			IdClaveSSA varchar(4) Not Null
		) 

		Alter Table VentaMasiva_ExcluirClaves Add Constraint PK_VentaMasiva_ExcluirClaves
			Primary Key ( IdEstado, IdFarmacia, IdClaveSSA) 

		Alter Table VentaMasiva_ExcluirClaves Add Constraint FK_VentaMasiva_ExcluirClaves___CatClavesSSA_Sales
			Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 

	End 
	Go--#SQL
	
	--Select * From VentaMasiva_ExcluirClaves
	--Insert Into VentaMasiva_ExcluirClaves
	--Select '13', '0003', '0032' union All
	--Select '13', '0003', '1525'


		If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'VentaMasiva_ExcluirUbicaciones' and xType = 'U' )  
	Begin 	

		Create Table VentaMasiva_ExcluirUbicaciones  
		(
			IdEmpresa Varchar(3) Not Null,
			IdEstado Varchar(2) Not Null,
			IdFarmacia Varchar(4) Not Null,
			IdPasillo int Not Null,
			IdEstante int Not Null,
			IdEntrepaño int Not Null
		) 

		Alter Table VentaMasiva_ExcluirUbicaciones Add Constraint PK_VentaMasiva_ExcluirUbicaciones
			Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño) 

		Alter Table VentaMasiva_ExcluirUbicaciones Add Constraint FK_VentaMasiva_ExcluirUbicaciones_InformacionAdicional___CatProductos 
			Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) References CatPasillos_Estantes_Entrepaños ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 

		--Select * From VentaMasiva_ExcluirUbicaciones  (NoLock)
		--Insert Into VentaMasiva_ExcluirUbicaciones
		--Select '001', '13', '0003', 0, 0, 0

	End 
	Go--#SQL 



----		drop table VentaMasiva_Plantilla  

		If Not Exists ( Select So.* From sysobjects So (NoLock) Where So.Name = 'VentaMasiva_Plantilla' and xType = 'U' )  
	Begin 	

		Create Table VentaMasiva_Plantilla  
		(
			IdEmpresa Varchar(3) Not Null Default '',
			IdEstado Varchar(2) Not Null Default '',
			IdFarmacia Varchar(4) Not Null Default '',
			IdSubFarmacia varchar(4) Not Null Default '', 
			SKU varchar(100) Not Null Default '', 
			CodigoEAN varchar(30) Not Null Default '', 
			ClaveLote varchar(30) Not Null Default '',
			IdPasillo int Not Null Default 0,
			IdEstante int Not Null Default 0,
			IdEntrepaño int Not Null Default 0, 
			Cantidad int not null default 0 
		) 

		Alter Table VentaMasiva_Plantilla Add Constraint PK_VentaMasiva_Plantilla
			Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, SKU, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 

		--Alter Table VentaMasiva_Plantilla Add Constraint FK_VentaMasiva_Plantilla_InformacionAdicional___CatProductos 
		--	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
		--	References CatPasillos_Estantes_Entrepaños ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 

		--Select * From VentaMasiva_Plantilla  (NoLock)
		--Insert Into VentaMasiva_Plantilla
		--Select '001', '13', '0003', 0, 0, 0

	End 
	Go--#SQL 


