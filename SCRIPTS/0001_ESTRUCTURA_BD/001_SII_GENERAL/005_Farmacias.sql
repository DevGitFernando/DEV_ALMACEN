------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatTiposUnidades' and xType = 'U' )
	Drop Table CatTiposUnidades  
Go--#SQL 

Create Table CatTiposUnidades 
(
	IdTipoUnidad varchar(3) Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Abreviatura varchar(5) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatTiposUnidades Add Constraint PK_CatTiposUnidades Primary Key ( IdTipoUnidad ) 
Go--#SQL 

If Not Exists ( Select * From CatTiposUnidades Where IdTipoUnidad = '000' )  Insert Into CatTiposUnidades (  IdTipoUnidad, Descripcion, Abreviatura, Status, Actualizado )  Values ( '000', 'Sin especificar', 'SNE', 'A', 0 )    Else Update CatTiposUnidades Set Descripcion = 'Sin especificar', Abreviatura = 'SNE', Status = 'A', Actualizado = 0 Where IdTipoUnidad = '000' 
If Not Exists ( Select * From CatTiposUnidades Where IdTipoUnidad = '001' )  Insert Into CatTiposUnidades (  IdTipoUnidad, Descripcion, Abreviatura, Status, Actualizado )  Values ( '001', 'Hospital General', 'HG', 'A', 0 )    Else Update CatTiposUnidades Set Descripcion = 'Hospital General', Abreviatura = 'HG', Status = 'A', Actualizado = 0 Where IdTipoUnidad = '001' 
If Not Exists ( Select * From CatTiposUnidades Where IdTipoUnidad = '002' )  Insert Into CatTiposUnidades (  IdTipoUnidad, Descripcion, Abreviatura, Status, Actualizado )  Values ( '002', 'Hospital de Especialidades', 'HE', 'A', 0 )    Else Update CatTiposUnidades Set Descripcion = 'Hospital de Especialidades', Abreviatura = 'HE', Status = 'A', Actualizado = 0 Where IdTipoUnidad = '002' 
If Not Exists ( Select * From CatTiposUnidades Where IdTipoUnidad = '003' )  Insert Into CatTiposUnidades (  IdTipoUnidad, Descripcion, Abreviatura, Status, Actualizado )  Values ( '003', 'Hospital Integral', 'HI', 'A', 0 )    Else Update CatTiposUnidades Set Descripcion = 'Hospital Integral', Abreviatura = 'HI', Status = 'A', Actualizado = 0 Where IdTipoUnidad = '003' 
If Not Exists ( Select * From CatTiposUnidades Where IdTipoUnidad = '004' )  Insert Into CatTiposUnidades (  IdTipoUnidad, Descripcion, Abreviatura, Status, Actualizado )  Values ( '004', 'Centro de Salud', 'CS', 'A', 0 )    Else Update CatTiposUnidades Set Descripcion = 'Centro de Salud', Abreviatura = 'CS', Status = 'A', Actualizado = 0 Where IdTipoUnidad = '004' 
If Not Exists ( Select * From CatTiposUnidades Where IdTipoUnidad = '005' )  Insert Into CatTiposUnidades (  IdTipoUnidad, Descripcion, Abreviatura, Status, Actualizado )  Values ( '005', 'Oficina', 'OFCN', 'A', 0 )    Else Update CatTiposUnidades Set Descripcion = 'Oficina', Abreviatura = 'OFCN', Status = 'A', Actualizado = 0 Where IdTipoUnidad = '005' 
If Not Exists ( Select * From CatTiposUnidades Where IdTipoUnidad = '006' )  Insert Into CatTiposUnidades (  IdTipoUnidad, Descripcion, Abreviatura, Status, Actualizado )  Values ( '006', 'Almacen', 'ALMN', 'A', 0 )    Else Update CatTiposUnidades Set Descripcion = 'Almacen', Abreviatura = 'ALMN', Status = 'A', Actualizado = 0 Where IdTipoUnidad = '006' 
Go--#SQL   


------------------------------------------------------------------------------------------------------------------------------------------------------    
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatUnidadesMedicas_NivelesDeAtencion' and xType = 'U' ) 
	Drop Table CatUnidadesMedicas_NivelesDeAtencion 
Go--#SQL  

Create Table CatUnidadesMedicas_NivelesDeAtencion  
( 
	IdNivelDeAtencion int Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Status varchar(2) Not Null Default 'A' 
) 
Go--#SQL  

Alter Table CatUnidadesMedicas_NivelesDeAtencion Add Constraint PK_CatUnidadesMedicas_NivelesDeAtencion 
	Primary Key ( IdNivelDeAtencion )
Go--#SQL  


If Not Exists ( Select * From CatUnidadesMedicas_NivelesDeAtencion Where IdNivelDeAtencion = 0 )  Insert Into CatUnidadesMedicas_NivelesDeAtencion (  IdNivelDeAtencion, Descripcion, Status )  Values ( 0, 'NO ESPECIFICADO', 'A' )  Else Update CatUnidadesMedicas_NivelesDeAtencion Set Descripcion = 'NO ESPECIFICADO', Status = 'A' Where IdNivelDeAtencion = 0  
If Not Exists ( Select * From CatUnidadesMedicas_NivelesDeAtencion Where IdNivelDeAtencion = 1 )  Insert Into CatUnidadesMedicas_NivelesDeAtencion (  IdNivelDeAtencion, Descripcion, Status )  Values ( 1, 'PRIMER NIVEL', 'A' )  Else Update CatUnidadesMedicas_NivelesDeAtencion Set Descripcion = 'PRIMER NIVEL', Status = 'A' Where IdNivelDeAtencion = 1  
If Not Exists ( Select * From CatUnidadesMedicas_NivelesDeAtencion Where IdNivelDeAtencion = 2 )  Insert Into CatUnidadesMedicas_NivelesDeAtencion (  IdNivelDeAtencion, Descripcion, Status )  Values ( 2, 'SEGUNDO NIVEL', 'A' )  Else Update CatUnidadesMedicas_NivelesDeAtencion Set Descripcion = 'SEGUNDO NIVEL', Status = 'A' Where IdNivelDeAtencion = 2  
If Not Exists ( Select * From CatUnidadesMedicas_NivelesDeAtencion Where IdNivelDeAtencion = 3 )  Insert Into CatUnidadesMedicas_NivelesDeAtencion (  IdNivelDeAtencion, Descripcion, Status )  Values ( 3, 'TERCER NIVEL', 'A' )  Else Update CatUnidadesMedicas_NivelesDeAtencion Set Descripcion = 'TERCER NIVEL', Status = 'A' Where IdNivelDeAtencion = 3  

Go--#SQL  


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatFarmacias' and xType = 'U' )
	Drop Table CatFarmacias 
Go--#SQL 

Create Table CatFarmacias 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	NombreFarmacia varchar(100) Not Null Default '', 
	
	CLUES varchar(20) Not Null Default '', 
	NombrePropio_UMedica varchar(200) Not Null Default '', 	
	IdNivelDeAtencion int Not Null Default 0,  
	 
	IdTipoUnidad varchar(3) Not Null Default '000', 
	EsDeConsignacion bit Not Null Default 'false', 
	ManejaVtaPubGral bit Default 'false', 
	ManejaControlados bit Default 'false', 
	IdJurisdiccion varchar(3) Not Null Default '', 	
	IdRegion varchar(2) Not Null, 
	IdSubRegion varchar(2) Not Null,
	EsAlmacen bit Default 'false',  
	EsUnidosis Int Not Null Default 0, 
	IdAlmacen varchar(2) Not Null Default '', 
	EsFrontera bit Default 'false', 
	Transferencia_RecepcionHabilitada bit Not Null Default 'true', 


	-- IdEstado varchar(2) Not Null Default '',     --- Descripcion en Catalogo aparte 
	IdMunicipio varchar(4) Not Null Default '',  --- Descripcion en Catalogo aparte
	IdColonia varchar(4) Not Null Default '',    --- Descripcion en Catalogo aparte
	Domicilio varchar(100) Not Null Default '', 
	CodigoPostal varchar(10) Not Null Default '', 
	Telefonos varchar(30) Not Null Default '',	
	Email varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatFarmacias Add Constraint PK_CatFarmacias Primary Key ( IdEstado, IdFarmacia )
Go--#SQL 

Alter Table CatFarmacias Add Constraint FK_CatEstados_CatFarmacias 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL

Alter Table CatFarmacias Add Constraint FK_CatJurisdicciones_CatFarmacias  
	Foreign Key ( IdEstado, IdJurisdiccion ) References CatJurisdicciones ( IdEstado, IdJurisdiccion ) 
Go--#SQL

Alter Table CatFarmacias Add Constraint FK_CatEstados_CatSubRegiones 
	Foreign Key ( IdRegion, IdSubRegion ) References CatSubRegiones ( IdRegion, IdSubRegion ) 
Go--#SQL

Alter Table CatFarmacias Add Constraint FK_CatFarmacias_CatTiposUnidades 
	Foreign Key ( IdTipoUnidad ) References CatTiposUnidades ( IdTipoUnidad ) 
Go--#SQL

Alter Table CatFarmacias Add Constraint FK_CatFarmacias___CatUnidadesMedicas_NivelesDeAtencion 
	Foreign Key ( IdNivelDeAtencion ) References CatUnidadesMedicas_NivelesDeAtencion ( IdNivelDeAtencion ) 	
Go--#SQL


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatEstados_SubFarmacias' and xType = 'U' ) 
   Drop Table CatEstados_SubFarmacias 
Go--#SQL 

Create Table CatEstados_SubFarmacias 
( 
	IdEstado varchar(2) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	EsConsignacion bit Not Null Default 'false', 
	EmulaVenta Bit Not Null Default 'False', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatEstados_SubFarmacias Add Constraint PK_CatEstados_SubFarmacias Primary Key ( IdEstado, IdSubFarmacia )
Go--#SQL 

Alter Table CatEstados_SubFarmacias Add Constraint FK_CatEstados_SubFarmacias_CatEstados 
    Foreign Key ( IdEstado ) References CatEstados ( IdEstado )
Go--#SQL 		


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatFarmacias_SubFarmacias' and xType = 'U' ) 
   Drop Table CatFarmacias_SubFarmacias 
Go--#SQL 		

Create Table CatFarmacias_SubFarmacias 
( 
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	--EsConsignacion bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatFarmacias_SubFarmacias Add Constraint PK_CatFarmacias_SubFarmacias Primary Key ( IdEstado, IdFarmacia, IdSubFarmacia )
Go--#SQL 

Alter Table CatFarmacias_SubFarmacias Add Constraint FK_CatFarmacias_SubFarmacias_CatFarmacias 
    Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )
Go--#SQL 

Alter Table CatFarmacias_SubFarmacias Add Constraint FK_CatFarmacias_SubFarmacias_CatEstados_SubFarmacias
    Foreign Key ( IdEstado, IdSubFarmacia ) References CatEstados_SubFarmacias ( IdEstado, IdSubFarmacia ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatUnidadesMedicas' and xType = 'U' )
	Drop Table CatUnidadesMedicas   
Go--#SQL 

Create Table CatUnidadesMedicas 
(
	IdUMedica varchar(6) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdJurisdiccion varchar(3) Not Null, 
    	CLUES varchar(30) Not Null Unique, 
	NombreUnidadMedica varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CatUnidadesMedicas Add Constraint PK_CatUnidadesMedicas Primary Key ( IdUMedica ) 
Go--#SQL  
