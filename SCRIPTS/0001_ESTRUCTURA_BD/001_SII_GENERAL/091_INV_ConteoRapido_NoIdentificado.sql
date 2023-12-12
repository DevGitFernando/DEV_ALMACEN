----------------------------------------------------------------------------------------------------------------------------     
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INV_ConteoRapido_NoIdentificado_Enc' and xType = 'U' ) 
Begin 
	Create Table INV_ConteoRapido_NoIdentificado_Enc
	(
		IdEmpresa varchar(3) NOT NULL,
		IdEstado varchar(2) NOT NULL,
		IdFarmacia varchar(4) NOT NULL,
		Folio varchar(8) NOT NULL,
		FechaRegistro datetime NOT NULL DEFAULT getdate(),
		IdPersonal varchar(4) NOT NULL,
		Observaciones varchar(200) NOT NULL DEFAULT '',
		MovtoAplicado varchar(1) NOT NULL DEFAULT 'N',
		Status varchar(1) NOT NULL DEFAULT 'A',
		Actualizado tinyint NOT NULL DEFAULT 0
	)

	Alter Table INV_ConteoRapido_NoIdentificado_Enc Add Constraint PK_INV_ConteoRapido_NoIdentificado_Enc
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 

	Alter Table INV_ConteoRapido_NoIdentificado_Enc Add Constraint FK_INV_ConteoRapido_NoIdentificado_Enc_CatEmpresas  
		Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  

	Alter Table INV_ConteoRapido_NoIdentificado_Enc Add Constraint FK_INV_ConteoRapido_NoIdentificado_Enc_CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) 
		References CatFarmacias ( IdEstado, IdFarmacia ) 

	Alter Table INV_ConteoRapido_NoIdentificado_Enc Add Constraint FK_INV_ConteoRapido_NoIdentificado_Enc_CatPersonal  
		Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
		References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 

End 
Go--#SQL   


----------------------------------------------------------------------------------------------------------------------------     
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INV_ConteoRapido_NoIdentificado_Det' and xType = 'U' ) 
Begin 
	Create Table INV_ConteoRapido_NoIdentificado_Det
	(
		IdEmpresa varchar(3) NOT NULL,
		IdEstado varchar(2) NOT NULL,
		IdFarmacia varchar(4) NOT NULL,
		Folio varchar(8) NOT NULL,
		ClaveSSA varchar(30) NOT NULL,
		Descripcion varchar(7500) NOT NULL DEFAULT (''),
		CodigoEAN varchar(30) NOT NULL,
		NombreComercial varchar(7500) NOT NULL DEFAULT (''),
		Laboratorio varchar(100) NOT NULL DEFAULT (''),
		Cantidad int NOT NULL DEFAULT 0,
		FechaRegistro datetime NOT NULL DEFAULT getdate(),
		Status varchar(1) NOT NULL DEFAULT 'A',
		Actualizado tinyint NOT NULL DEFAULT 0
	) 

	Alter Table INV_ConteoRapido_NoIdentificado_Det Add Constraint PK_INV_ConteoRapido_NoIdentificado_Det 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, ClaveSSA, CodigoEAN) 
		
	Alter Table INV_ConteoRapido_NoIdentificado_Det Add Constraint FK_INV_ConteoRapido_NoIdentificado_Det_INV_ConteoRapido_NoIdentificado_Enc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
		References INV_ConteoRapido_NoIdentificado_Enc ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 

End 
Go--#SQL  
