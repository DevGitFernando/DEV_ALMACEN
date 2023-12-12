----------------------------------------------------------  ----------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rec_Recetas_Indicaciones' and xType = 'U' ) 
   Drop Table Rec_Recetas_Indicaciones   
Go--#SQL  

----------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rec_Recetas_ClavesSSA' and xType = 'U' ) 
   Drop Table Rec_Recetas_ClavesSSA  
Go--#SQL  



----------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rec_Recetas_Diagnosticos' and xType = 'U' ) 
   Drop Table Rec_Recetas_Diagnosticos   
Go--#SQL  


-----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------- 
----------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rec_Recetas' and xType = 'U' ) 
   Drop Table Rec_Recetas  
Go--#SQL  

Create Table Rec_Recetas 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdReceta varchar(8) Not Null, --- Consecutivo interno 
	IdMedico varchar(6) Not Null, 
	IdBeneficiario varchar(8) Not Null, 	
	
	--- Campos Adicionales 
	
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table Rec_Recetas Add Constraint PK_Rec_Recetas Primary Key ( IdEstado, IdFarmacia, IdReceta ) 
Go--#SQL  


----------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rec_Recetas_ClavesSSA' and xType = 'U' ) 
   Drop Table Rec_Recetas_ClavesSSA  
Go--#SQL  

Create Table Rec_Recetas_ClavesSSA 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdReceta varchar(8) Not Null, --- Consecutivo interno 
	IdClaveSSA varchar(4) Not Null, 
	Cantidad int Not Null Default 0, 
	Renglon smallint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 	
) 
Go--#SQL  	

Alter Table Rec_Recetas_ClavesSSA Add Constraint PK_Rec_Recetas_ClavesSSA Primary Key ( IdEstado, IdFarmacia, IdReceta, IdClaveSSA ) 
Go--#SQL  

Alter Table Rec_Recetas_ClavesSSA Add Constraint FK_Rec_Recetas_ClavesSSA_Rec_Recetas 
	Foreign Key ( IdEstado, IdFarmacia, IdReceta ) References Rec_Recetas ( IdEstado, IdFarmacia, IdReceta )  
Go--#SQL  

----------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rec_Recetas_Indicaciones' and xType = 'U' ) 
   Drop Table Rec_Recetas_Indicaciones   
Go--#SQL  

Create Table Rec_Recetas_Indicaciones 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdReceta varchar(8) Not Null, --- Consecutivo interno 
	-- IdClaveSSA varchar(4) Not Null, 
	IdIndicacion smallint Not Null Default 0, 
	Indicacion varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 	
) 
Go--#SQL  	

--Alter Table Rec_Recetas_Indicaciones Add Constraint PK_Rec_Recetas_Indicaciones Primary Key ( IdEstado, IdFarmacia, IdReceta, IdClaveSSA, IdIndicacion ) 
--Go 

Alter Table Rec_Recetas_Indicaciones Add Constraint PK_Rec_Recetas_Indicaciones Primary Key ( IdEstado, IdFarmacia, IdReceta, IdIndicacion ) 
Go--#SQL  

----Alter Table Rec_Recetas_Indicaciones Add Constraint FK_Rec_Recetas_Indicaciones_Rec_Recetas_ClavesSSA 
----	Foreign Key ( IdEstado, IdFarmacia, IdReceta, IdClaveSSA ) References Rec_Recetas_ClavesSSA ( IdEstado, IdFarmacia, IdReceta, IdClaveSSA )  
----Go 
Alter Table Rec_Recetas_Indicaciones Add Constraint FK_Rec_Recetas_Indicaciones_Rec_Recetas 
	Foreign Key ( IdEstado, IdFarmacia, IdReceta ) References Rec_Recetas ( IdEstado, IdFarmacia, IdReceta  )  
Go--#SQL  


----------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rec_Recetas_Diagnosticos' and xType = 'U' ) 
   Drop Table Rec_Recetas_Diagnosticos   
Go--#SQL  

Create Table Rec_Recetas_Diagnosticos 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdReceta varchar(8) Not Null, --- Consecutivo interno 
	IdDiagnostico varchar(6) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 	
) 
Go--#SQL  	

Alter Table Rec_Recetas_Diagnosticos Add Constraint PK_Rec_Recetas_Diagnosticos Primary Key ( IdEstado, IdFarmacia, IdReceta, IdDiagnostico ) 
Go--#SQL  

Alter Table Rec_Recetas_Diagnosticos Add Constraint FK_Rec_Recetas_Diagnosticos_Rec_Recetas 
	Foreign Key ( IdEstado, IdFarmacia, IdReceta ) References Rec_Recetas ( IdEstado, IdFarmacia, IdReceta )  
Go--#SQL  
