
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal_HistorialOperaciones' and xType = 'U' ) 
Drop Table CatPersonal_HistorialOperaciones
Go--#SQL 


----------------------------------------------------------------------------------------------     
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal' and xType = 'U' ) 
   Drop Table CatPersonal 
Go--#SQL 

Create Table CatPersonal 
(
	IdPersonal varchar(8) Not Null Default '', 
	IdJefe varchar(8) Null Default '', 
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

	DiasDeAguinaldo int Not Null Default 0,

	Status varchar(1) Not Null Default 'A',				--- 
	Actualizado tinyint Not Null Default 0				--- 
) 
Go--#SQL 

Alter Table CatPersonal Add Constraint PK_CatPersonal Primary Key ( IdPersonal ) 
Go--#SQL 


Alter Table CatPersonal Add Constraint FK_CatPersonal_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL 

Alter Table CatPersonal Add Constraint FK_CatPersonal_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table CatPersonal Add Constraint FK_CatPersonal_CatPuestos 
	Foreign Key ( IdPuesto ) References CatPuestos ( IdPuesto ) 
Go--#SQL 

Alter Table CatPersonal Add Constraint FK_CatPersonal_CatDepartamentos 
	Foreign Key ( IdDepartamento ) References CatDepartamentos ( IdDepartamento ) 
Go--#SQL 

Alter Table CatPersonal Add Constraint FK_CatPersonal_CatEscolaridades 
	Foreign Key ( IdEscolaridad ) References CatEscolaridades ( IdEscolaridad ) 
Go--#SQL 

Alter Table CatPersonal Add Constraint FK_CatPersonal_CatTipoContrato 
	Foreign Key ( IdTipoContrato ) References CatTipoContrato ( IdTipoContrato ) 
Go--#SQL 

Alter Table CatPersonal Add Constraint FK_CatPersonal_CatEstadosDomicilio 
	Foreign Key ( IdEstado_Domicilio ) References CatEstados ( IdEstado ) 
Go--#SQL 

Alter Table CatPersonal Add Constraint FK_CatPersonal_CatMunicipio 
	Foreign Key ( IdEstado_Domicilio, IdMunicipio ) References CatMunicipios ( IdEstado, IdMunicipio ) 
Go--#SQL 

Alter Table CatPersonal Add Constraint FK_CatPersonal_CatColonias 
	Foreign Key ( IdEstado_Domicilio, IdMunicipio, IdColonia ) References CatColonias ( IdEstado, IdMunicipio, IdColonia ) 
Go--#SQL 


Alter Table CatPersonal Add Constraint FK_CatPersonal_CatGruposSanguineos
	Foreign Key ( IdGrupoSanguineo ) References CatGruposSanguineos ( IdGrupoSanguineo ) 
Go--#SQL 



----Alter Table CatPersonal Add Constraint FK_CatPersonal_CatEdades 
----	Foreign Key ( IdEdad ) References CatEdades ( IdEdad ) 
----Go--#SQL 

----Alter Table CatPersonal Add Constraint FK_CatPersonal_CatAntiguedades 
----	Foreign Key ( IdAntiguedad ) References CatAntiguedades ( IdAntiguedad ) 
----Go--#SQL 

--------------------------------------------------------------------------------------------------


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal_HistorialOperaciones' and xType = 'U' ) 
Drop Table CatPersonal_HistorialOperaciones
Go--#SQL 

Create Table CatPersonal_HistorialOperaciones  
(
	IdPersonal varchar(8) Not Null,
	IdEstado varchar(2) Not Null,		
	FechaRegistro datetime Not Null,
	Keyx int identity (1, 1) 
)
Go--#SQL 
    
Alter Table CatPersonal_HistorialOperaciones Add Constraint PK_CatPersonal_HistorialOperaciones Primary key ( IdPersonal, FechaRegistro ) 
Go--#SQL     


Alter Table CatPersonal_HistorialOperaciones Add Constraint FK_CatPersonal_HistorialOperaciones_CatPersonal 
	Foreign Key ( IdPersonal ) References CatPersonal ( IdPersonal ) 
Go--#SQL 