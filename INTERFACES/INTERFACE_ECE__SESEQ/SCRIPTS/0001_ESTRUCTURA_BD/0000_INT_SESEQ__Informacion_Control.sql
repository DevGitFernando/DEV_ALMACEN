---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_SESEQ__Informacion_Control' and xType = 'U' ) 
	Drop Table INT_SESEQ__Informacion_Control 
Go--#SQL  

Create Table INT_SESEQ__Informacion_Control
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,

	FechaValidacion Smalldatetime Not Null Default GetDate(),
	FechaRegistro datetime Default getdate(), 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table INT_SESEQ__Informacion_Control Add Constraint PK_INT_SESEQ__Informacion_Control Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FechaValidacion ) 
Go--#SQL  

Alter Table INT_SESEQ__Informacion_Control Add Constraint FK_INT_SESEQ__Informacion_Control_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table INT_SESEQ__Informacion_Control Add Constraint FK_INT_SESEQ__Informacion_Control_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Set DateFormat YMD

Insert Into INT_SESEQ__Informacion_Control (IdEmpresa, IdEstado, IdFarmacia, FechaValidacion)
Select distinct '001' As IdEmpresa, F.IdEstado, F.IdFarmacia, '2021-09-05' As FechaValidacion
From CatFarmacias F 
Inner Join FarmaciaProductos E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia )
Where F.IdEstado = '22' And F.IdFarmacia > '0001'

Go--#SQL 


