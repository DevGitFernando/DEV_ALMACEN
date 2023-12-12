-----------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPuestosCEDIS' and xType = 'U' ) 
   Drop Table CatPuestosCEDIS 
Go--#SQL 

Create Table CatPuestosCEDIS 
( 
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	Nombre varchar(100) Not Null Default '', 
	IdPuesto varchar(2) Not Null Default '00', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)

Alter Table CatPersonalCEDIS Add Constraint PK_CatPersonalCEDIS Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  
	
Alter Table CatPersonalCEDIS Add Constraint FK_CatPersonalCEDIS_CatPuestosCEDIS 
	Foreign Key ( IdPuesto ) References CatPuestosCEDIS ( IdPuesto ) 
Go--#SQL  

----If Not Exists ( Select * From CatPuestosCEDIS Where IdPuesto = '00' )  Insert Into CatPuestosCEDIS Values ( '00', 'SIN ESPECIFICAR', 'A', 0 )    Else Update CatPuestosCEDIS Set Descripcion = 'SIN ESPECIFICAR', Status = 'A', Actualizado = 0 Where IdPuesto = '00' 
----If Not Exists ( Select * From CatPuestosCEDIS Where IdPuesto = '01' )  Insert Into CatPuestosCEDIS Values ( '01', 'SURTIDOR', 'A', 0 )    Else Update CatPuestosCEDIS Set Descripcion = 'SURTIDOR', Status = 'A', Actualizado = 0 Where IdPuesto = '01' 
----If Not Exists ( Select * From CatPuestosCEDIS Where IdPuesto = '02' )  Insert Into CatPuestosCEDIS Values ( '02', 'CHOFER', 'A', 0 )    Else Update CatPuestosCEDIS Set Descripcion = 'CHOFER', Status = 'A', Actualizado = 0 Where IdPuesto = '02' 
----Go--#SxQL  

