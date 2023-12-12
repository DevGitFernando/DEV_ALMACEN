If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFG_VALES_Emision' and xType = 'U' ) 
Begin    
	Create Table CFG_VALES_Emision 
	( 
		IdEstado varchar(2) Not Null Default '', 
		IdFarmacia varchar(4) Not Null Default '', 
		
		EmiteVales bit Not Null Default 'false', 
		Observaciones varchar(200) Not Null Default '', 
		TipoEmision smallint Not Null Default 0, 	
		
		Status varchar(1) Not Null Default 'A', 
		Actualizado smallint Not Null Default 0 
	) 

	Alter Table CFG_VALES_Emision Add Constraint PK_CFG_VALES_Emision Primary Key ( IdEstado, IdFarmacia ) 

	Alter Table CFG_VALES_Emision Add Constraint FK_CFG_VALES_Emision__CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )   

End 
Go--#SQL 


--------------------------------------------------------------------------------------------------- 
-- Insert Into CFG_VALES_Emision Select '21', '0005', 1, 'Ninguna', 1, 'A', 0 


