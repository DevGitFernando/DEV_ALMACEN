 If Exists (Select * From Sysobjects Where name = 'FK_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas_CatPersonalHuellas' And Xtype = 'F')
	Begin
		Alter table Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas
		Drop Constraint FK_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas_CatPersonalHuellas
	End
Go--#SQL 
	
If Exists (Select * From Sysobjects Where name = 'FK_Net_Permisos_Operaciones_SupervisadasHuellas_CatPersonalHuellas' And Xtype = 'F')
	Begin
		Alter table Net_Permisos_Operaciones_SupervisadasHuellas
		Drop Constraint FK_Net_Permisos_Operaciones_SupervisadasHuellas_CatPersonalHuellas
	End
Go--#SQL


-------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonalHuellas' and xType = 'U' ) 
Begin 
	Create Table CatPersonalHuellas
	(
		IdPersonal varchar(8) Not Null Default '', 
		CURP varchar(20) Not Null Default '' Unique, 
		Nombre varchar(50) Not Null Default '', 
		ApPaterno varchar(50) Not Null Default '', 
		ApMaterno varchar(50) Not Null Default '', 		
		FechaNacimiento datetime Not Null Default getdate(), 
		IdEstado_Domicilio varchar(2) Not Null Default '',
		IdMunicipio_Domicilio varchar(4) Not Null Default '',  
		IdColonia_Domicilio varchar(4) Not Null Default '',  
		Calle_Domicilio varchar(100) Not Null Default '',
		Numero_Domicilio varchar(20) Not Null Default '',  
		CodigoPostal_Domicilio varchar(10) Not Null Default '', 

		IdEstado varchar(2) Not Null Default '',			--- PLAZA 
		IdFarmacia varchar(4) Not Null Default '', 			--- PLAZA 
		IdPuesto varchar(3) Not Null Default '',			--- Puesto 
		IdDepartamento varchar(3) Not Null Default '',		--- Departamento
		Sexo varchar(1) Not Null Default '',				--- Sexo 
		IdEscolaridad varchar(2) Not Null Default '',		--- Escolaridad 
		--IdEdad varchar(2) Not Null Default '',				--- Edad 
		IdTipoContrato varchar(2) Not Null Default '',		--- Contratación 
		--IdAntiguedad varchar(2) Not Null Default '',		--- Antiguedad 
		FechaIngreso datetime Not Null Default getdate(), 
		EMail varchar(100) Not Null Default '',				--- 
		Password varchar(500) Not Null Default '', 			--- 

		IdGrupoSanguineo varchar(2) Not Null Default '',	-- Datos Medicos 
		Alergias varchar(8000) Not Null Default '',			-- Datos Medicos 

		NombreFotoPersonal varchar(200) Default '',			-- Datos de Fotografia del personal
		FotoPersonal text default '',						-- Datos de Fotografia del personal
		IdJefe varchar(8) NOT NULL,
		DiasDeAguinaldo int Not Null Default 0,

		Status varchar(1) Not Null Default 'A',				--- 
		Actualizado tinyint Not Null Default 0,			---	---
	) 

	Alter Table CatPersonalHuellas Add Constraint PK_CatPersonalHuellas Primary Key ( IdPersonal ) 

End 	
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------
If Exists (Select * From Sysobjects Where name = 'Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas' And Xtype = 'U')
	Begin
		Alter Table Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas Add Constraint FK_Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas_CatPersonalHuellas
		Foreign Key ( IdPersonal ) References CatPersonalHuellas ( IdPersonal ) 
	End
Go--#SQL 

If Exists (Select * From Sysobjects Where name = 'Net_Permisos_Operaciones_SupervisadasHuellas' And Xtype = 'U')
	Begin
		Alter Table Net_Permisos_Operaciones_SupervisadasHuellas Add Constraint FK_Net_Permisos_Operaciones_SupervisadasHuellas_CatPersonalHuellas
		Foreign Key ( IdPersonal ) References CatPersonalHuellas ( IdPersonal )
	End  
Go--#SQL  