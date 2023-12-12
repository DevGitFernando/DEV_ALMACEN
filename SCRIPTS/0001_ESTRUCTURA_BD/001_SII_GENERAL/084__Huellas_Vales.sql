------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Manos' and xType = 'U' ) 
Begin 
	Create Table FP_Manos 
	(
		Mano smallint not null Default 0, 
		Descripcion varchar(100) Not Null Default '', 
		Actualizado tinyint Not Null Default 0	
	)
		
	Alter Table FP_Manos Add Constraint PK_FP_Manos Primary Key ( Mano ) 

	Insert Into FP_Manos (Mano, Descripcion) Values (  1, 'Mano izquierda' ) 
	Insert Into FP_Manos (Mano, Descripcion) Values (  2, 'Mano derecha' ) 
End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Dedos' and xType = 'U' ) 
Begin 
	Create Table FP_Dedos 
	(
		Dedo smallint Not Null Default 0, 
		Descripcion varchar(100) Not Null Default '',
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table FP_Dedos Add Constraint PK_FP_Dedos Primary Key ( Dedo ) 

	Insert Into FP_Dedos (Dedo, Descripcion) Values (  1, 'Meñique' ) 
	Insert Into FP_Dedos (Dedo, Descripcion) Values (  2, 'Anular' ) 
	Insert Into FP_Dedos (Dedo, Descripcion) Values (  3, 'Medio' ) 
	Insert Into FP_Dedos (Dedo, Descripcion) Values (  4, 'Indice' ) 
	Insert Into FP_Dedos (Dedo, Descripcion) Values (  5, 'Pulgar' ) 
	
End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Manos_Dedos' and xType = 'U' ) 
Begin 
	Create Table FP_Manos_Dedos 
	(
		NumDedo smallint Not Null Default 0, 
		Mano smallint Not Null Default 0,	
		Dedo smallint Not Null Default 0,
		Actualizado tinyint Not Null Default 0
	)

	Alter Table FP_Manos_Dedos Add Constraint PK_FP_Manos_Dedos Primary Key ( NumDedo ) 

	Alter Table FP_Manos_Dedos Add Constraint FK_FP_Manos_Dedos__FP_Manos 
		Foreign Key ( Mano ) References FP_Manos ( Mano ) 

	Alter Table FP_Manos_Dedos Add Constraint FK_FP_Manos_Dedos__FP_Dedos 
		Foreign Key ( Dedo ) References FP_Dedos ( Dedo ) 

	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  1, 1, 1  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  2, 1, 2  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  3, 1, 3  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  4, 1, 4  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  5, 1, 5  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  6, 2, 1  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  7, 2, 2  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  8, 2, 3  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values (  9, 2, 4  ) 
	Insert Into FP_Manos_Dedos (NumDedo, Mano, Dedo) Values ( 10, 2, 5  ) 
End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FP_Huellas_Vales' and xType = 'U' ) 
Begin 
	Create Table FP_Huellas_Vales
	( 
		IdHuella int Identity(1,1), 
		FechaRegistro datetime Not Null Default getdate(), 
		ReferenciaHuella varchar(100) Not Null Default '', 
		NumDedo smallint Not Null Default 0, 
		Huella varchar(max) Not Null Default '', -- Unique , --- text, -- varbinary(max), 
		Status varchar(2) Not Null Default 'A',
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table FP_Huellas_Vales Add Constraint PK_FP_Huellas_Vales Primary Key ( ReferenciaHuella, NumDedo ) 

	Alter Table FP_Huellas_Vales Add Constraint FK_FP_Huellas_Vales__FP_Manos_Dedos 
		Foreign Key ( NumDedo ) References FP_Manos_Dedos ( NumDedo ) 

End 
Go--#SQL 

