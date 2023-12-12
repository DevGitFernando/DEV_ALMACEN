If Exists ( Select Name From Sysobjects Where Name = 'Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas' and xType = 'U' )
   Drop Table Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'Net_Operaciones_SupervisadasPorFarmaciaHuellas' and xType = 'U' )
   Drop Table Net_Operaciones_SupervisadasPorFarmaciaHuellas
Go--#SQL  

-----------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects Where Name = 'Net_Operaciones_SupervisadasPorFarmaciaHuellas' and xType = 'U' )
Begin 
	Create Table Net_Operaciones_SupervisadasPorFarmaciaHuellas 
	( 
		IdOperacion varchar(4) Not Null, 
		Nombre varchar(50) Not Null Default '', --  Unique Clustered, 
		Descripcion varchar(200) Not Null, 
		Status varchar(1) Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table Net_Operaciones_SupervisadasPorFarmaciaHuellas 
		Add Constraint PK_Net_Operaciones_SupervisadasPorFarmaciaHuellas Primary Key ( IdOperacion ) 

End 
Go--#SQL



-----------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects Where Name = 'Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas' and xType = 'U' )
Begin 
	Create Table Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas 
	( 
		IdPersonal varchar(8) Not Null,
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,
		IdOperacion varchar(4) Not Null, 
		FechaUpdate datetime Not Null Default getdate(), 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0
	) 

	Alter Table Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas Add Constraint PK_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas 
		Primary Key ( IdPersonal, IdEstado, IdFarmacia, IdOperacion ) 

	Alter Table Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas 
		Add Constraint FK_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas_Net_Operaciones_SupervisadasPorFarmaciaHuellas 
		Foreign Key ( IdOperacion ) References Net_Operaciones_SupervisadasPorFarmaciaHuellas ( IdOperacion ) 

	Alter Table Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas 
		Add Constraint FK_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas_CatPersonalHuellas
		Foreign Key ( IdPersonal ) References CatPersonalHuellas ( IdPersonal )  
		
End 	
Go--#SQL  	
