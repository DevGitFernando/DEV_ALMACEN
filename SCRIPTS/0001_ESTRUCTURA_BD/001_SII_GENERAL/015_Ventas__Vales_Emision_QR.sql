---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_EmisionEnc_GUID' and xType = 'U' ) 
Begin 
	Create Table Vales_EmisionEnc_GUID 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		FolioVale varchar(30) Not Null, 
		GUID varchar(100) Not Null Default '', 
		QR image null -- default '' 
	)

	Alter Table Vales_EmisionEnc_GUID Add Constraint PK_Vales_EmisionEnc_GUID Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) 
End 
Go--#SQL  

If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
			  Where So.Name = 'Vales_EmisionEnc_GUID' and Sc.Name = 'GUID' ) 
Begin 
	Alter Table Vales_EmisionEnc_GUID Add GUID varchar(100) Not Null Default ''  
End 
Go--#SQL  

