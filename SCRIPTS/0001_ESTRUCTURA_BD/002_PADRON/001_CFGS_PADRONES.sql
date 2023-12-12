If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_BD_PADRONES' and xType = 'U' ) 
   Drop Table CFGS_BD_PADRONES 
Go--#SQL  

Create Table CFGS_BD_PADRONES 
( 
	Id int identity, 
	NombreBD varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A'  
) 
Go--#SQL

Alter Table CFGS_BD_PADRONES Add Constraint PK_CFGS_BD_PADRONES Primary Key ( NombreBD ) 
Go--#SQL

--------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_PADRON_ESTADOS' and xType = 'U' ) 
   Drop Table CFGS_PADRON_ESTADOS  
Go--#SQL

Create Table CFGS_PADRON_ESTADOS 
(
	Id int identity, 
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null, 
	NombreBD varchar(100) Not Null, 	
	NombreTabla varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	EsLocal bit Not Null Default 'false' 
) 
Go--#SQL

Alter Table CFGS_PADRON_ESTADOS Add Constraint PK_CFGS_PADRON_ESTADOS Primary Key ( IdEstado, NombreBD, NombreTabla )  
Go--#SQL

Alter Table CFGS_PADRON_ESTADOS Add Constraint FK_CFGS_PADRON_ESTADOS_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL

Alter Table CFGS_PADRON_ESTADOS Add Constraint FK_CFGS_PADRON_ESTADOS_CFGS_BD_PADRONES  
	Foreign Key ( NombreBD ) References CFGS_BD_PADRONES ( NombreBD ) 
Go--#SQL


-------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------- 


--------------------------------------------- 
---  Insert Into CFGS_BD_PADRONES ( NombreBD ) values ( 'SII_OficinaCentral' )  
---  Insert Into CFGS_PADRON_ESTADOS ( IdEstado, NombreTabla ) Values ( '', '' )    

/* 

  Insert Into CFGS_BD_PADRONES ( NombreBD ) values ( 'SII_PadronesSP' ) 
  Insert Into CFGS_PADRON_ESTADOS ( IdEstado, IdCliente, NombreTabla ) Values ( '25', '0002', 'PadronSP_Sinaloa' ) 
  Insert Into CFGS_PADRON_ESTADOS ( IdEstado, IdCliente, NombreTabla ) Values ( '25', '0003', 'PadronEmpleadosIntermed' ) 

*/ 

-- Select * from CFGS_BD_PADRONES
-- Select * from CFGS_PADRON_ESTADOS 
--------------------------------------------- 
