-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_EX_Validacion_Titulos_Beneficiarios' and xType = 'U' )
   Drop Table CFG_EX_Validacion_Titulos_Beneficiarios  
Go--#SQL  

Create Table CFG_EX_Validacion_Titulos_Beneficiarios  
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 	
	FolioReferencia varchar(20) Not Null Default '', 
	ReemplazarFolioReferencia bit Not Null Default 'false', 		
	Beneficiario varchar(200) Not Null Default '', 
	ReemplazarBeneficiario bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CFG_EX_Validacion_Titulos_Beneficiarios 
	Add Constraint PK_CFG_EX_Validacion_Titulos_Beneficiarios Primary Key ( IdEstado, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma ) 
Go--#SQL 

		