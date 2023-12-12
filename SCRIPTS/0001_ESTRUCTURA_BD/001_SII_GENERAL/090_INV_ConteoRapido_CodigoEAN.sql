----------------------------------------------------------------------------------------------------------------------------     
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INV_ConteoRapido_CodigoEAN_Enc' and xType = 'U' ) 
Begin 
	Create Table INV_ConteoRapido_CodigoEAN_Enc 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		Folio varchar(8) Not Null,
		IdPersonal varchar(4) Not Null, 
		FechaRegistro datetime Not Null default getdate(),
		FechaInicio datetime Not Null default getdate(),
		FechaFinal datetime Not Null default getdate(), 
		Observaciones varchar(200) Not Null Default '',
		Conteos int Not Null Default 0,
		Status varchar(2) Not Null Default 'A', 
		Actualizado smallint Not Null Default 0
	) 
	   
	Alter Table INV_ConteoRapido_CodigoEAN_Enc Add Constraint PK_INV_ConteoRapido_CodigoEAN_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	   
	Alter Table INV_ConteoRapido_CodigoEAN_Enc Add Constraint FK_INV_ConteoRapido_CodigoEAN_Enc__CatEmpresas 
		Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
	   
	Alter Table INV_ConteoRapido_CodigoEAN_Enc Add Constraint FK_INV_ConteoRapido_CodigoEAN_Enc__CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  

	Alter Table INV_ConteoRapido_CodigoEAN_Enc Add Constraint FK_INV_ConteoRapido_CodigoEAN_Enc_CatPersonal  
		Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 

End 
Go--#SQL 
   
   
----------------------------------------------------------------------------------------------------------------------------     
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INV_ConteoRapido_CodigoEAN_Det' and xType = 'U' ) 
Begin 
	Create Table INV_ConteoRapido_CodigoEAN_Det 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		Folio varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null,
		 
		ExistenciaLogica int Not Null Default 0,	
		Inv_Inicial int Not Null Default 0,
		Entradas int Not Null Default 0,
		Salidas int Not Null Default 0,
		ExistenciaFinal int Not Null Default 0,
		 	
		Conteo1 int Not Null Default 0,				
		Conteo2 int Not Null Default 0,				
		Conteo3 int Not Null Default 0,
						
		Conteo1_Bodega int Not Null Default 0, 
		Conteo2_Bodega int Not Null Default 0, 
		Conteo3_Bodega int Not Null Default 0, 

		EsConteo1 bit Not Null Default 'false',		
		EsConteo2 bit Not Null Default 'false',			
		EsConteo3 bit Not Null Default 'false',		
		
		Status varchar(1) Not Null Default 'A', 
		Actualizado smallint Not Null Default 0
	) 
   
	Alter Table INV_ConteoRapido_CodigoEAN_Det Add Constraint PK_INV_ConteoRapido_CodigoEAN_Det Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, CodigoEAN ) 

	Alter Table INV_ConteoRapido_CodigoEAN_Det Add Constraint FK_INV_ConteoRapido_CodigoEAN_Det__INV_ConteoRapido_CodigoEAN_Enc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) References INV_ConteoRapido_CodigoEAN_Enc ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 

End 
Go--#SQL 