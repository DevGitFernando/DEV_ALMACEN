------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_Almacenes_Regionales' and xType = 'U' ) 
   Drop Table COM_OCEN_Almacenes_Regionales   
Go--#SQL 

Create Table COM_OCEN_Almacenes_Regionales 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Status varchar(1) default 'A', 
	Actualizado tinyint default 0 
) 
Go--#SQL 

Alter Table COM_OCEN_Almacenes_Regionales Add Constraint PK_COM_OCEN_Almacenes_Regionales Primary Key ( IdEstado, IdFarmacia ) 
Go--#SQL 


------ 
/* 
If Not Exists ( Select * From COM_OCEN_Almacenes_Regionales Where IdEstado = '20' and IdFarmacia = '0002' )  Insert Into COM_OCEN_Almacenes_Regionales Values ( '20', '0002', 'A', 0 )    Else Update COM_OCEN_Almacenes_Regionales Set Status = 'A', Actualizado = 0 Where IdEstado = '20' and IdFarmacia = '0002' 
If Not Exists ( Select * From COM_OCEN_Almacenes_Regionales Where IdEstado = '25' and IdFarmacia = '0011' )  Insert Into COM_OCEN_Almacenes_Regionales Values ( '25', '0011', 'A', 0 )    Else Update COM_OCEN_Almacenes_Regionales Set Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0011' 
*/ 
Go--#SQL 

    