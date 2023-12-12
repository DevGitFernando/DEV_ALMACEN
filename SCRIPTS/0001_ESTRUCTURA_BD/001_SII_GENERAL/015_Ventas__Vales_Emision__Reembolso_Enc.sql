-------------------------------------------
---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Emision_Reembolso_Enc' and xType = 'U' ) 
Begin 
Create Table Vales_Emision_Reembolso_Enc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioValeReembolso varchar(30) Not Null, 
	FolioVale varchar(30) Not Null, 

	FechaRegistro datetime Default getdate(), 
	IdPersonal varchar(4) Not Null, 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_Vales_Emision_Reembolso_Enc' and xType = 'PK' ) 
Begin 
Alter Table Vales_Emision_Reembolso_Enc Add Constraint PK_Vales_Emision_Reembolso_Enc 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioValeReembolso ) 
End 
Go--#SQL  


