If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'CFG_VAL_BeneficiariosAlertas' and xType = 'U' ) 
Begin 
	Create Table CFG_VAL_BeneficiariosAlertas 
	(
		FolioReferencia varchar(20) Not Null Default '', 
		NombreBeneficiario varchar(300) Not Null Default '', 
		Status varchar(1) Not Null Default 'A'  
	) 
End 
Go--#SQL 


------ Insert Into CFG_VAL_BeneficiariosAlertas (  NombreBeneficiario, FolioReferencia, Status )  Values ( 'PANCHO LOPEZ', '520920', 'A' )   
------ Insert Into CFG_VAL_BeneficiariosAlertas (  NombreBeneficiario, FolioReferencia, Status )  Values ( 'PANCRACHI LARA', '520920', 'A' )   
------ Insert Into CFG_VAL_BeneficiariosAlertas (  NombreBeneficiario, FolioReferencia, Status )  Values ( 'PANCHO PANTERA', '520920', 'A' )   
------ Insert Into CFG_VAL_BeneficiariosAlertas (  NombreBeneficiario, FolioReferencia, Status )  Values ( 'ROSITA ELVIREZ', '520920', 'A' )   

------sp_generainserts 'CFG_VAL_BeneficiariosAlertas' , 1 
