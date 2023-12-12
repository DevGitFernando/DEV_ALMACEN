Set NoCount On
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatJurisdicciones' and xType = 'U' ) 
   Drop Table CatJurisdicciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatEmpresasEstados' and xType = 'U' ) 
   Drop Table CatEmpresasEstados 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NOLock) Where Name = 'CatColonias' and xType = 'U' ) 
	Drop Table CatColonias 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NOLock) Where Name = 'CatMunicipios' and xType = 'U' ) 
	Drop Table CatMunicipios 
Go--#SQL 


----------------------------------------------------

--If Exists ( Select * from dbo.sysobjects where id = object_id(N'[dbo].[CatEstados]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
--	drop table [dbo].[CatEstados]
--GO

If Exists ( Select Name From Sysobjects (NOLock) Where Name = 'CatEstados' and xType = 'U' ) 
	Drop Table CatEstados 
Go--#SQL 

CREATE TABLE CatEstados
(
	IdEstado varchar(2) Not Null, 
	Nombre varchar(50) Not Null, 
	ClaveRENAPO varchar(2) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) ON [PRIMARY]
Go--#SQL

Alter Table CatEstados add constraint PK_CatEstados Primary Key (IdEstado)
Go--#SQL


---------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatEmpresasEstados' and xType = 'U' ) 
   Drop Table CatEmpresasEstados 
Go--#SQL 

Create Table CatEmpresasEstados 
(
	IdEmpresaEdo varchar(4) Not Null, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
)
Go--#SQL 

Alter Table CatEmpresasEstados Add Constraint PK_CatEmpresasEstados Primary Key ( IdEmpresaEdo ) 
Go--#SQL 

Alter Table CatEmpresasEstados Add Constraint FK_CatEmpresasEstados_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 	 

Alter Table CatEmpresasEstados Add Constraint FK_CatEmpresasEstados_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL 	 



/*
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatEmpresasFarmacias' and xType = 'U' ) 
   Drop Table CatEmpresasFarmacias 
Go 

Create Table CatEmpresasFarmacias 
(
	IdEmpresaFarmacia varchar(4) Not Null, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
)
Go 

Alter Table CatEmpresasFarmacias Add Constraint PK_CatEmpresasFarmacias Primary Key ( IdEmpresaFarmacia ) 
Go 
*/

---------------------------------------------------------------------------


/*
If Not Exists ( Select * From CatEstados Where IdEstado = '01' ) Insert Into CatEstados Values ('01', 'AGUASCALIENTES', 'AS', 'C', '0' )
If Not Exists ( Select * From CatEstados Where IdEstado = '02' ) Insert Into CatEstados Values ('02', 'BAJA CALIFORNIA', 'BC', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '03' ) Insert Into CatEstados Values ('03', 'BAJA CALIFORNIA SUR', 'BS', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '04' ) Insert Into CatEstados Values ('04', 'CAMPECHE', 'CC', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '05' ) Insert Into CatEstados Values ('05', 'COAHUILA', 'CL', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '06' ) Insert Into CatEstados Values ('06', 'COLIMA', 'CM', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '07' ) Insert Into CatEstados Values ('07', 'CHIAPAS', 'CS', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '08' ) Insert Into CatEstados Values ('08', 'CHIHUAHUA', 'CH', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '09' ) Insert Into CatEstados Values ('09', 'DISTRITO FEDERAL', 'DF', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '10' ) Insert Into CatEstados Values ('10', 'DURANGO', 'DG', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '11' ) Insert Into CatEstados Values ('11', 'GUANAJUATO', 'GT', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '12' ) Insert Into CatEstados Values ('12', 'GUERRERO', 'GR', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '13' ) Insert Into CatEstados Values ('13', 'HIDALGO', 'HG', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '14' ) Insert Into CatEstados Values ('14', 'JALISCO', 'JC', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '15' ) Insert Into CatEstados Values ('15', 'MEXICO', 'MC', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '16' ) Insert Into CatEstados Values ('16', 'MICHOACAN', 'MN', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '17' ) Insert Into CatEstados Values ('17', 'MORELOS', 'MS', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '18' ) Insert Into CatEstados Values ('18', 'NAYARIT', 'NT', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '19' ) Insert Into CatEstados Values ('19', 'NUEVO LEON', 'NL', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '20' ) Insert Into CatEstados Values ('20', 'OAXACA', 'OC', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '21' ) Insert Into CatEstados Values ('21', 'PUEBLA', 'PL', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '22' ) Insert Into CatEstados Values ('22', 'QUERETARO', 'QT', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '23' ) Insert Into CatEstados Values ('23', 'QUINTANA ROO', 'QR', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '24' ) Insert Into CatEstados Values ('24', 'SAN LUIS POTOSI', 'SP', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '25' ) Insert Into CatEstados Values ('25', 'SINALOA', 'SL', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '26' ) Insert Into CatEstados Values ('26', 'SONORA', 'SR', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '27' ) Insert Into CatEstados Values ('27', 'TABASCO', 'TC', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '28' ) Insert Into CatEstados Values ('28', 'TAMAULIPAS', 'TS', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '29' ) Insert Into CatEstados Values ('29', 'TLAXCALA', 'TL', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '30' ) Insert Into CatEstados Values ('30', 'VERACRUZ', 'VZ', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '31' ) Insert Into CatEstados Values ('31', 'YUCATAN', 'YN', 'C', '0'  )
If Not Exists ( Select * From CatEstados Where IdEstado = '32' ) Insert Into CatEstados Values ('32', 'ZACATECAS', 'ZS', 'C', '0'  )
Go 
*/

If Exists ( Select Name From Sysobjects (NOLock) Where Name = 'CatMunicipios' and xType = 'U' ) 
	Drop Table CatMunicipios 
Go--#SQL 

CREATE TABLE [dbo].[CatMunicipios](
	[IdEstado] [varchar](2) NOT NULL,
	[IdMunicipio] [varchar](4) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Status] [varchar](1) NOT NULL Default 'A',
	[Actualizado] [tinyint] NOT NULL Default 0, 
 CONSTRAINT [Pk_CatMunicipios] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC,
	[IdMunicipio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

Go--#SQL
SET ANSI_PADDING OFF
Go--#SQL
ALTER TABLE [dbo].[CatMunicipios]  WITH NOCHECK ADD  CONSTRAINT [FK_CatMunicipios_CatEstados] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[CatEstados] ([IdEstado])
Go--#SQL
ALTER TABLE [dbo].[CatMunicipios] CHECK CONSTRAINT [FK_CatMunicipios_CatEstados]
Go--#SQL



If Exists ( Select Name From Sysobjects (NOLock) Where Name = 'CatColonias' and xType = 'U' ) 
	Drop Table CatColonias 
Go--#SQL 

CREATE TABLE [dbo].[CatColonias](
	[IdEstado] [varchar](2) NOT NULL,
	[IdMunicipio] [varchar](4) NOT NULL,
	[IdColonia] [varchar](4) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Status] [varchar](1) NOT NULL Default 'A',
	[Actualizado] [tinyint] NOT NULL Default 0, 	
 CONSTRAINT [Pk_CatColonias] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC,
	[IdMunicipio] ASC,
	[IdColonia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

Go--#SQL
SET ANSI_PADDING OFF
Go--#SQL
ALTER TABLE [dbo].[CatColonias]  WITH NOCHECK ADD  CONSTRAINT [FK_CatColonias_CatMunicipios] FOREIGN KEY([IdEstado], [IdMunicipio])
REFERENCES [dbo].[CatMunicipios] ([IdEstado], [IdMunicipio])
Go--#SQL
ALTER TABLE [dbo].[CatColonias] CHECK CONSTRAINT [FK_CatColonias_CatMunicipios]
Go--#SQL


------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatJurisdicciones' and xType = 'U' ) 
   Drop Table CatJurisdicciones 
Go--#SQL 

Create Table CatJurisdicciones 
(
	IdEstado varchar(2) Not Null, 
	IdJurisdiccion varchar(3) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	Status varchar(1) Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL     

Alter Table CatJurisdicciones Add Constraint PK_CatJurisdicciones Primary Key ( IdEstado, IdJurisdiccion ) 
Go--#SQL 

Alter Table CatJurisdicciones Add Constraint FK_CatJurisdicciones_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL 	 

