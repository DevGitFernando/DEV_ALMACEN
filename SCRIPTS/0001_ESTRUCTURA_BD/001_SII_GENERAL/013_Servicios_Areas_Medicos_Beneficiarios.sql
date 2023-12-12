------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMedicos' and xType = 'U' ) 
   Drop Table CatMedicos 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatServicios_Areas_Farmacias' and xType = 'U' ) 
   Drop Table CatServicios_Areas_Farmacias 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatServicios_Areas' and xType = 'U' ) 
   Drop Table CatServicios_Areas 
Go--#SQL 


--------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatServicios' and xType = 'U' ) 
   Drop Table CatServicios 
Go--#SQL  

Create Table CatServicios 
(
	IdServicio varchar(3) Not Null, 
	Descripcion varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL     

Alter Table CatServicios Add Constraint PK_CatServicios Primary Key ( IdServicio )  
Go--#SQL  

-------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatServicios_Areas' and xType = 'U' ) 
   Drop Table CatServicios_Areas 
Go--#SQL  

Create Table CatServicios_Areas 
(
	IdServicio varchar(3) Not Null, 
	IdArea varchar(3) Not Null, 	
	Descripcion varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL     

Alter Table CatServicios_Areas Add Constraint PK_CatServicios_Areas Primary Key ( IdServicio, IdArea )  
Go--#SQL  

Alter Table CatServicios_Areas Add Constraint FK_CatServicios_Areas_CatServicios 
	Foreign Key ( IdServicio ) References CatServicios ( IdServicio ) 
Go--#SQL  	


-------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatServicios_Areas_Farmacias' and xType = 'U' ) 
   Drop Table CatServicios_Areas_Farmacias 
Go--#SQL  

Create Table CatServicios_Areas_Farmacias 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdServicio varchar(3) Not Null, 
	IdArea varchar(3) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table CatServicios_Areas_Farmacias Add Constraint PK_CatServicios_Areas_Farmacias Primary Key ( IdEstado, IdFarmacia, IdServicio, IdArea )
Go--#SQL  

Alter Table CatServicios_Areas_Farmacias Add Constraint FK_CatServicios_Areas_Farmacias_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CatServicios_Areas_Farmacias Add Constraint FK_CatServicios_Areas_Farmacias_CatServicios_Areas  
	Foreign Key ( IdServicio, IdArea ) References CatServicios_Areas ( IdServicio, IdArea ) 
Go--#SQL  


/* 
------------- 
Insert Into CatServicios ( IdServicio, Descripcion ) values ( '001', 'Urgencias' ) 
Insert Into CatServicios ( IdServicio, Descripcion ) values ( '002', 'Ginecologia' ) 
Go 

Insert Into CatServicios_Areas ( IdServicio, IdArea, Descripcion ) values ( '001', '001', 'Urgencias pediatricas' ) 
Insert Into CatServicios_Areas ( IdServicio, IdArea, Descripcion ) values ( '001', '002', 'Urgencias adultos' ) 
Go 

Insert Into CatServicios_Areas_Farmacias values ( '25', '0001', '001', '001', 'A', 0 ) 
Insert Into CatServicios_Areas_Farmacias values ( '25', '0001', '001', '002', 'A', 0 ) 
Insert Into CatServicios_Areas_Farmacias values ( '25', '0002', '001', '002', 'A', 0 ) 
Go 
*/ 

---------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatEspecialidades' and xType = 'U' ) 
   Drop Table CatEspecialidades 
Go--#SQL  

Create Table CatEspecialidades 
( 
	IdEspecialidad varchar(4) Not Null, 
	Descripcion varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
) 
Go--#SQL  

Alter Table CatEspecialidades Add Constraint PK_CatEspecialidades Primary Key ( IdEspecialidad ) 
Go--#SQL  

If Not Exists ( Select * From CatEspecialidades Where IdEspecialidad = '0001' )  Insert Into CatEspecialidades Values ( '0001', 'SIN ESPECIALIDAD', 'A', 0 )    Else Update CatEspecialidades Set Descripcion = 'SIN ESPECIALIDAD', Status = 'A', Actualizado = 0 Where IdEspecialidad = '0001'
Go--#SQL  


-------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMedicos' and xType = 'U' ) 
   Drop Table CatMedicos 
Go--#SQL  

Create Table CatMedicos 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 	
	IdMedico varchar(6) Not Null, 
	Nombre varchar(150) Not Null, 
	ApPaterno varchar(150) Not Null, 
	ApMaterno varchar(150) Not Null, 
	NumCedula varchar(30) Not Null, 
	IdEspecialidad varchar(4) Not Null Default '0000', 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatMedicos Add Constraint PK_CatMedicos Primary Key ( IdEstado, IdFarmacia, IdMedico )  
Go--#SQL  

Alter Table CatMedicos Add Constraint FK_CatMedicos_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CatMedicos Add Constraint FK_CatMedicos_CatEspecialidades 
	Foreign Key ( IdEspecialidad ) References CatEspecialidades ( IdEspecialidad ) 
Go--#SQL  


-----------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMedicos_Direccion' and xType = 'U' )
   Drop Table CatMedicos_Direccion
Go--#SQL

Create Table CatMedicos_Direccion
(
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	IdMedico varchar(6) Not Null,
	IdDireccion varchar(2) Not Null, 	
	
	Pais varchar(100) Not Null, 
	-- IdEstado varchar(2) Not Null Default '',  
	IdMunicipio varchar(4) Not Null Default '', 
	IdColonia varchar(4) Not Null Default '', 		
	Calle varchar(200) Not Null Default '', 
	NumeroExterior varchar(20) Not Null Default '', 
	NumeroInterior varchar(20) Not Null Default '', 	
	CodigoPostal varchar(10) Not Null Default '', 		
	Referencia varchar(100) Not Null Default '', 	

	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL

Alter Table CatMedicos_Direccion Add Constraint PK_CatMedicos_Direccion Primary Key ( IdEstado, IdFarmacia, IdMedico, IdDireccion )
Go--#SQL

Alter Table CatMedicos_Direccion Add Constraint FK_CatMedicos_Direccion_CatMedicos
	Foreign Key ( IdEstado, IdFarmacia, IdMedico ) References CatMedicos ( IdEstado, IdFarmacia, IdMedico )
Go--#SQL


-------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMedicos_Historico' and xType = 'U' ) 
   Drop Table CatMedicos_Historico 
Go--#SQL  

Create Table CatMedicos_Historico 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 	
	IdMedico varchar(6) Not Null, 
	Nombre varchar(150) Not Null, 
	ApPaterno varchar(150) Not Null, 
	ApMaterno varchar(150) Not Null, 
	NumCedula varchar(30) Not Null, 
	IdEspecialidad varchar(4) Not Null Default '0000', 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	IdPersonal varchar(4) Not Null Default '', 
	FechaRegistroLog datetime Not Null Default getdate(), 
	Keyx int identity(1,1)  
)
Go--#SQL  

Alter Table CatMedicos_Historico Add Constraint PK_CatMedicos_Historico Primary Key ( IdEstado, IdFarmacia, IdMedico, FechaRegistroLog )  
Go--#SQL  

Alter Table CatMedicos_Historico Add Constraint FK_CatMedicos_Historico___CatMedicos 
	Foreign Key ( IdEstado, IdFarmacia, IdMedico ) References CatMedicos ( IdEstado, IdFarmacia, IdMedico ) 
Go--#SQL  



-----------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatTiposDeBeneficiarios' and xType = 'U' ) 
	Drop Table CatTiposDeBeneficiarios 
Go--#SQL  

Create Table CatTiposDeBeneficiarios 
( 
	IdTipoBeneficiario smallint Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
)
Go--#SQL 

Alter Table CatTiposDeBeneficiarios Add Constraint PK_CatTiposDeBeneficiarios Primary Key ( IdTipoBeneficiario )
Go--#SQL  


If Not Exists ( Select * From CatTiposDeBeneficiarios Where IdTipoBeneficiario = 0 )  Insert Into CatTiposDeBeneficiarios (  IdTipoBeneficiario, Descripcion, Status )  Values ( 0, 'NO ESPECIFICADO', 'A' ) 
 Else Update CatTiposDeBeneficiarios Set Descripcion = 'NO ESPECIFICADO', Status = 'A' Where IdTipoBeneficiario = 0
If Not Exists ( Select * From CatTiposDeBeneficiarios Where IdTipoBeneficiario = 1 )  Insert Into CatTiposDeBeneficiarios (  IdTipoBeneficiario, Descripcion, Status )  Values ( 1, 'FARMACIA', 'A' ) 
 Else Update CatTiposDeBeneficiarios Set Descripcion = 'FARMACIA', Status = 'A' Where IdTipoBeneficiario = 1
If Not Exists ( Select * From CatTiposDeBeneficiarios Where IdTipoBeneficiario = 2 )  Insert Into CatTiposDeBeneficiarios (  IdTipoBeneficiario, Descripcion, Status )  Values ( 2, 'HOSPITAL VENTA DIRECTA', 'A' ) 
 Else Update CatTiposDeBeneficiarios Set Descripcion = 'HOSPITAL VENTA DIRECTA', Status = 'A' Where IdTipoBeneficiario = 2
If Not Exists ( Select * From CatTiposDeBeneficiarios Where IdTipoBeneficiario = 3 )  Insert Into CatTiposDeBeneficiarios (  IdTipoBeneficiario, Descripcion, Status )  Values ( 3, 'JURISDICCION VENTA DIRECTA', 'A' ) 
 Else Update CatTiposDeBeneficiarios Set Descripcion = 'JURISDICCION VENTA DIRECTA', Status = 'A' Where IdTipoBeneficiario = 3

Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'CatTiposDeDerechohabiencia' and xType = 'U' ) 
Begin 

	Create Table CatTiposDeDerechohabiencia 
	( 
		IdTipoDerechoHabiencia varchar(3) Not Null, 
		Descripcion varchar(100) Not Null Default '', 
		Status varchar(1) Not Null Default 'A'  
	) 

	Alter Table CatTiposDeDerechohabiencia Add Constraint PK_CatTiposDeDerechohabiencia Primary Key  ( IdTipoDerechoHabiencia ) 

End 

Go--#SQL 


If Not Exists ( Select * From CatTiposDeDerechohabiencia Where IdTipoDerechoHabiencia = '001' )  Insert Into CatTiposDeDerechohabiencia (  IdTipoDerechoHabiencia, Descripcion, Status )  Values ( '001', 'SERVICIOS DE SALUD ESTALES (SSA)', 'A' )  Else Update CatTiposDeDerechohabiencia Set Descripcion = 'SERVICIOS DE SALUD ESTALES (SSA)', Status = 'A' Where IdTipoDerechoHabiencia = '001'  
If Not Exists ( Select * From CatTiposDeDerechohabiencia Where IdTipoDerechoHabiencia = '002' )  Insert Into CatTiposDeDerechohabiencia (  IdTipoDerechoHabiencia, Descripcion, Status )  Values ( '002', 'POBLACION ABIERTA', 'A' )  Else Update CatTiposDeDerechohabiencia Set Descripcion = 'POBLACION ABIERTA', Status = 'A' Where IdTipoDerechoHabiencia = '002'  
If Not Exists ( Select * From CatTiposDeDerechohabiencia Where IdTipoDerechoHabiencia = '003' )  Insert Into CatTiposDeDerechohabiencia (  IdTipoDerechoHabiencia, Descripcion, Status )  Values ( '003', 'IMSSS', 'A' )  Else Update CatTiposDeDerechohabiencia Set Descripcion = 'IMSSS', Status = 'A' Where IdTipoDerechoHabiencia = '003'  
If Not Exists ( Select * From CatTiposDeDerechohabiencia Where IdTipoDerechoHabiencia = '004' )  Insert Into CatTiposDeDerechohabiencia (  IdTipoDerechoHabiencia, Descripcion, Status )  Values ( '004', 'ISSTE', 'A' )  Else Update CatTiposDeDerechohabiencia Set Descripcion = 'ISSTE', Status = 'A' Where IdTipoDerechoHabiencia = '004'  
If Not Exists ( Select * From CatTiposDeDerechohabiencia Where IdTipoDerechoHabiencia = '005' )  Insert Into CatTiposDeDerechohabiencia (  IdTipoDerechoHabiencia, Descripcion, Status )  Values ( '005', 'SEDENA', 'A' )  Else Update CatTiposDeDerechohabiencia Set Descripcion = 'SEDENA', Status = 'A' Where IdTipoDerechoHabiencia = '005'  
If Not Exists ( Select * From CatTiposDeDerechohabiencia Where IdTipoDerechoHabiencia = '006' )  Insert Into CatTiposDeDerechohabiencia (  IdTipoDerechoHabiencia, Descripcion, Status )  Values ( '006', 'SEMAR', 'A' )  Else Update CatTiposDeDerechohabiencia Set Descripcion = 'SEMAR', Status = 'A' Where IdTipoDerechoHabiencia = '006'  
If Not Exists ( Select * From CatTiposDeDerechohabiencia Where IdTipoDerechoHabiencia = '007' )  Insert Into CatTiposDeDerechohabiencia (  IdTipoDerechoHabiencia, Descripcion, Status )  Values ( '007', 'CFE', 'A' )  Else Update CatTiposDeDerechohabiencia Set Descripcion = 'CFE', Status = 'A' Where IdTipoDerechoHabiencia = '007'  

Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'CatTiposDeIdentificaciones' and xType = 'U' ) 
Begin 

	Create Table CatTiposDeIdentificaciones 
	( 
		IdTipoDeIdentificacion varchar(3) Not Null, 
		Descripcion varchar(100) Not Null Default '', 
		Informacion varchar(100) Not Null Default '', 
		Status varchar(1) Not Null Default 'A'  
	) 

	Alter Table CatTiposDeIdentificaciones Add Constraint PK_CatTiposDeIdentificaciones Primary Key  ( IdTipoDeIdentificacion ) 

End 

Go--#SQL 

If Not Exists ( Select * From CatTiposDeIdentificaciones Where IdTipoDeIdentificacion = '000' )  Insert Into CatTiposDeIdentificaciones (  IdTipoDeIdentificacion, Descripcion, Informacion, Status )  Values ( '000', 'NO ESPECIFICADO', 'Dato requerido', 'A' )  Else Update CatTiposDeIdentificaciones Set Descripcion = 'NO ESPECIFICADO', Informacion = 'Dato requerido', Status = 'A' Where IdTipoDeIdentificacion = '000'  
If Not Exists ( Select * From CatTiposDeIdentificaciones Where IdTipoDeIdentificacion = '001' )  Insert Into CatTiposDeIdentificaciones (  IdTipoDeIdentificacion, Descripcion, Informacion, Status )  Values ( '001', 'INE', 'Dato requerido', 'A' )  Else Update CatTiposDeIdentificaciones Set Descripcion = 'INE', Informacion = 'Dato requerido', Status = 'A' Where IdTipoDeIdentificacion = '001'  
If Not Exists ( Select * From CatTiposDeIdentificaciones Where IdTipoDeIdentificacion = '002' )  Insert Into CatTiposDeIdentificaciones (  IdTipoDeIdentificacion, Descripcion, Informacion, Status )  Values ( '002', 'CURP', '18 Carácteres', 'A' )  Else Update CatTiposDeIdentificaciones Set Descripcion = 'CURP', Informacion = '18 Carácteres', Status = 'A' Where IdTipoDeIdentificacion = '002'  
If Not Exists ( Select * From CatTiposDeIdentificaciones Where IdTipoDeIdentificacion = '003' )  Insert Into CatTiposDeIdentificaciones (  IdTipoDeIdentificacion, Descripcion, Informacion, Status )  Values ( '003', 'ACTA DE NACIMIENTO', 'Dato requerido', 'A' )  Else Update CatTiposDeIdentificaciones Set Descripcion = 'ACTA DE NACIMIENTO', Informacion = 'Dato requerido', Status = 'A' Where IdTipoDeIdentificacion = '003'  

Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_CurpsGenericas' and xType = 'U' ) 
Begin 

	Create Table CFGC_CurpsGenericas 
	( 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdCurp varchar(8) Not Null, --- Consecutivo interno 
		Nombre varchar(150) Not Null, 
		ApPaterno varchar(150) Not Null, 
		ApMaterno varchar(150) Not Null, 
		Sexo varchar(1) Not Null Default '', 
		FechaNacimiento datetime Not Null Default getdate(), 

		CURP varchar(18) Not Null Default '' UNIQUE, 

		FechaRegistro datetime Not Null Default getdate(), 
		Status varchar(1) Not Null Default 'A'  
	) 

	Alter Table CFGC_CurpsGenericas Add Constraint PK_CFGC_CurpsGenericas Primary Key  ( IdEstado, IdFarmacia, IdCurp ) 

End 

Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatBeneficiarios' and xType = 'U' ) 
   Drop Table CatBeneficiarios 
Go--#SQL  

Create Table CatBeneficiarios 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, --- Heredado: Cliente al cual se le esta surtiendo en Dispensacion de medicamento 
	IdSubCliente varchar(4) Not Null,  --- Heredado: Sub-Cliente al cual se le esta surtiendo en Dispensacion de medicamento 
	IdBeneficiario varchar(8) Not Null, --- Consecutivo interno 
	Nombre varchar(150) Not Null, 
	ApPaterno varchar(150) Not Null, 
	ApMaterno varchar(150) Not Null, 
	Sexo varchar(1) Not Null Default '', 
	FechaNacimiento datetime Not Null Default getdate(), 

	CURP varchar(18) Not Null Default '', 
	IdEstadoResidencia varchar(2) Not Null Default '00', 
	IdTipoDerechoHabiencia varchar(3) Not Null Default '001', 
	IdTipoDeIdentificacion varchar(3) Not Null Default '000', 


	FolioReferencia varchar(20) Not Null Default '',    --- Folio : Num. Poliza seguro, Num. Empleado, Num. Placa Policia, etc, es el folio de referencia proporcionado por la institucion que respalda al Beneficiario. 
	FolioReferenciaAuxiliar varchar(20) Not Null Default '', --- Folio : Num. Poliza seguro, Num. Empleado, Num. Placa Policia, etc, es el folio de referencia proporcionado por la institucion que respalda al Beneficiario. 

	IdJurisdiccion varchar(3) Not Null Default '001',  

	FechaInicioVigencia datetime Not Null Default getdate(),
	FechaFinVigencia datetime Not Null Default getdate(),  --- Fecha de vigencia de la cobertura 
	TipoDeBeneficiario smallint Not Null Default 1,    -- 1 ===> Farmacia / Hospital | 2 ==> Jurisdicciones / Ventas directas 
	Domicilio varchar(200) Not Null Default '', 
	
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatBeneficiarios Add Constraint PK_CatBeneficiarios Primary Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 
Go--#SQL  

Alter Table CatBeneficiarios Add Constraint FK_CatBeneficiarios_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CatBeneficiarios Add Constraint FK_CatBeneficiarios_CatSubClientes 
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente ) 
Go--#SQL  

Alter Table CatBeneficiarios Add Constraint FK_CatBeneficiarios___CatFarmacias
			Foreign Key ( IdEstado, IdJurisdiccion ) References CatJurisdicciones ( IdEstado, IdJurisdiccion ) 
Go--#SQL  

Alter Table CatBeneficiarios Add Constraint FK_CatBeneficiarios___CatTiposDeBeneficiarios 
	Foreign Key ( TipoDeBeneficiario ) References CatTiposDeBeneficiarios ( IdTipoBeneficiario )  
Go--#SQL 

Alter Table CatBeneficiarios Add Constraint FK_CatBeneficiarios___CatTiposDeDerechohabiencia 
	Foreign Key ( IdTipoDerechoHabiencia ) References CatTiposDeDerechohabiencia ( IdTipoDerechoHabiencia )   
Go--#SQL 

Alter Table CatBeneficiarios_Historico Add Constraint FK_CatBeneficiarios_Historico___CatTiposDeDerechohabiencia 
	Foreign Key ( IdTipoDerechoHabiencia ) References CatTiposDeDerechohabiencia ( IdTipoDerechoHabiencia )   
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'CatBeneficiarios_Identificacion' and xType = 'U' ) 
Begin 

	Create Table CatBeneficiarios_Identificacion 
	( 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdCliente varchar(4) Not Null, 
		IdSubCliente varchar(4) Not Null, 
		IdBeneficiario varchar(8) Not Null, 

		IMG_01_Frontal varchar(max) Not Null Default '', 
		FechaModificacion_Frontal datetime Not Null Default getdate(), 
		IMG_02_Reverso varchar(max) Not Null Default '', 
		FechaModificacion_Reverso datetime Not Null Default getdate(), 

		Status varchar(1) Not Null Default 'A'  
	) 

	Alter Table CatBeneficiarios_Identificacion Add Constraint PK_CatBeneficiarios_Identificacion Primary Key  ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 

End 
Go--#SQL 



If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'FK_CatBeneficiarios_Identificacion___CatBeneficiarios' and xType = 'F' ) 
Begin 

	Alter Table CatBeneficiarios_Identificacion Add Constraint FK_CatBeneficiarios_Identificacion___CatBeneficiarios 
		Foreign Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) References CatBeneficiarios ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 

End 
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatBeneficiarios_Domicilios' and xType = 'U' ) 
Begin 
Create Table CatBeneficiarios_Domicilios 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, --- Heredado: Cliente al cual se le esta surtiendo en Dispensacion de medicamento 
	IdSubCliente varchar(4) Not Null,  --- Heredado: Sub-Cliente al cual se le esta surtiendo en Dispensacion de medicamento 
	IdBeneficiario varchar(8) Not Null,  --- Consecutivo interno 
	IdEstado_D varchar(2) Not Null, 
	IdMunicipio_D varchar(4) Not Null, 
	IdColonia_D varchar(4) Not Null, 
	CodigoPostal varchar(10) Not Null Default '', 
	Direccion varchar(200) Not Null Default '', 
	Referencia varchar(200) Not Null Default '', 
	Telefonos varchar(50) Not Null Default '' 	 
) 

Alter Table CatBeneficiarios_Domicilios Add Constraint 
	PK_CatBeneficiarios_Domicilios Primary Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 

Alter Table CatBeneficiarios_Domicilios Add Constraint FK_CatBeneficiarios_Domicilios___CatBeneficiarios 
	Foreign Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 
	References CatBeneficiarios ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 
	
End 	
Go--#SQL  

-------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatBeneficiarios_Historico' and xType = 'U' ) 
   Drop Table CatBeneficiarios_Historico 
Go--#SQL  

Create Table CatBeneficiarios_Historico 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, --- Heredado: Cliente al cual se le esta surtiendo en Dispensacion de medicamento 
	IdSubCliente varchar(4) Not Null,  --- Heredado: Sub-Cliente al cual se le esta surtiendo en Dispensacion de medicamento 
	IdBeneficiario varchar(8) Not Null, --- Consecutivo interno 
	Nombre varchar(150) Not Null, 
	ApPaterno varchar(150) Not Null, 
	ApMaterno varchar(150) Not Null, 
	Sexo varchar(1) Not Null Default '', 

	CURP varchar(18) Not Null Default '', 
	
	FechaNacimiento datetime Not Null Default getdate(), 
	FolioReferencia varchar(20) Not Null Default '',    --- Folio : Num. Poliza seguro, Num. Empleado, Num. Placa Policia, etc, es el folio de referencia proporcionado por la institucion que respalda al Beneficiario. 
	
	FechaInicioVigencia datetime Not Null Default getdate(),
	FechaFinVigencia datetime Not Null Default getdate(),  --- Fecha de vigencia de la cobertura 
	
	Domicilio varchar(200) Not Null Default '', 
	FolioReferenciaAuxiliar varchar(20) Not Null Default '', 
	
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	IdPersonal varchar(4) Not Null Default '', 
	FechaRegistroLog datetime Not Null Default getdate(), 
	Keyx int identity(1,1)  	 
)
Go--#SQL  

Alter Table CatBeneficiarios_Historico Add Constraint 
	PK_CatBeneficiarios_Historico Primary Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, FechaRegistroLog ) 
Go--#SQL  

Alter Table CatBeneficiarios_Historico Add Constraint FK_CatBeneficiarios_Historico___CatBeneficiarios 
	Foreign Key ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 
	References CatBeneficiarios ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario ) 
Go--#SQL  


