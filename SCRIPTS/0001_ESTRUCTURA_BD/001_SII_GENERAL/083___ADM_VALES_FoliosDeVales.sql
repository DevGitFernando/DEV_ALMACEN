---------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades' and xType = 'U' ) 
Begin 
	Create Table ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 	
		FolioTimbre int Not Null, 	
		Disponible smallint Not Null Default 1, 
		CadenaInteligente varchar(1000) Not Null,     
		FechaValidez datetime Not Null Default getdate()  
	) 

	Alter Table ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades Add Constraint PK_ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTimbre ) 

End 
Go--#SQL 

