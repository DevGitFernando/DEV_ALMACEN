---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFG_Farmacias' and xType = 'U' ) 
   Drop Table FACT_CFG_Farmacias 
Go--#SQL 

Create Table FACT_CFG_Farmacias
(
	IdEstado Varchar(2) Not Null,
	IdFarmacia Varchar(4) Not NUll, 
	Status varchar(1) Not Null Default 'A' 
)
Go--#SQL


Alter Table FACT_CFG_Farmacias Add Constraint PK_FACT_CFG_Farmacias
	Primary Key ( IdEstado, IdFarmacia) 
Go--#SQL   

Alter Table FACT_CFG_Farmacias Add Constraint FK_FACT_CFG_Farmacias___CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL

--		sp_generainserts FACT_CFG_Farmacias , 1 

--		sp_listcolumnas FACT_CFG_Farmacias 

--Select * From FACT_CFG_Farmacias

--Insert Into FACT_CFG_Farmacias
--Select Idestado, IdFarmacia
--From CatFarmacias
--Where Idestado = '21' And IdFarmacia > 3000 


/* 

--	Insert Into FACT_CFG_Farmacias ( IdEstado, IdFarmacia, Status ) 
	select IdEstado, IdFarmacia, 'A' 
	from catFarmacias 
	where 
		( 
		IdEstado = 11 
		and 
		IdFarmacia >= 4000 
		) 


*/ 
