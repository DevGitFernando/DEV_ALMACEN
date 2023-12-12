----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_SubFarmaciasConsigna' and xType = 'U' ) 
   Drop Table INT_ND_SubFarmaciasConsigna 
Go--#SQL 

Create Table INT_ND_SubFarmaciasConsigna 
(	
	IdEstado varchar(2) Not Null, 
	IdSubFarmacia varchar(4) Not Null,
	
	Status varchar(1)  Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 	
Go--#SQL 

Alter Table INT_ND_SubFarmaciasConsigna Add Constraint PK_INT_ND_SubFarmaciasConsigna Primary Key ( IdEstado, IdSubFarmacia ) 
Go--#SQL   

If Not Exists ( Select * From INT_ND_SubFarmaciasConsigna Where IdEstado = '16' and IdSubFarmacia = '02' )  Insert Into INT_ND_SubFarmaciasConsigna (  IdEstado, IdSubFarmacia, Status, Actualizado )  Values ( '16', '02', 'A', 0 )    Else Update INT_ND_SubFarmaciasConsigna Set Status = 'A', Actualizado = 0 Where IdEstado = '16' and IdSubFarmacia = '02'  
If Not Exists ( Select * From INT_ND_SubFarmaciasConsigna Where IdEstado = '16' and IdSubFarmacia = '03' )  Insert Into INT_ND_SubFarmaciasConsigna (  IdEstado, IdSubFarmacia, Status, Actualizado )  Values ( '16', '03', 'A', 0 )    Else Update INT_ND_SubFarmaciasConsigna Set Status = 'A', Actualizado = 0 Where IdEstado = '16' and IdSubFarmacia = '03'  
If Not Exists ( Select * From INT_ND_SubFarmaciasConsigna Where IdEstado = '16' and IdSubFarmacia = '04' )  Insert Into INT_ND_SubFarmaciasConsigna (  IdEstado, IdSubFarmacia, Status, Actualizado )  Values ( '16', '04', 'A', 0 )    Else Update INT_ND_SubFarmaciasConsigna Set Status = 'A', Actualizado = 0 Where IdEstado = '16' and IdSubFarmacia = '04'  
Go--#SQL   
