---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__CatFarmacias_A_Procesar' and xType = 'U' ) 
   Drop Table BI_RPT__CatFarmacias_A_Procesar 
Go--#SQL 

Create Table BI_RPT__CatFarmacias_A_Procesar
(
	IdEstado Varchar(2) Not Null,
	IdFarmacia Varchar(4) Not NUll, 
	ProcesarInformacion bit not null default 'false', 
	MostrarEnListado bit not null default 'false' 
)
Go--#SQL


Alter Table BI_RPT__CatFarmacias_A_Procesar Add Constraint PK_BI_RPT__CatFarmacias_A_Procesar
	Primary Key ( IdEstado, IdFarmacia) 
Go--#SQL   

Alter Table BI_RPT__CatFarmacias_A_Procesar Add Constraint FK_BI_RPT__CatFarmacias_A_Procesar___CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL

--		sp_generainserts BI_RPT__CatFarmacias_A_Procesar , 1 

--		sp_listcolumnas BI_RPT__CatFarmacias_A_Procesar 

--Select * From BI_RPT__CatFarmacias_A_Procesar

--Insert Into BI_RPT__CatFarmacias_A_Procesar
--Select Idestado, IdFarmacia
--From CatFarmacias
--Where Idestado = '21' And IdFarmacia > 3000 


/* 

--	Insert Into BI_RPT__CatFarmacias_A_Procesar ( IdEstado, IdFarmacia, ProcesarInformacion, MostrarEnListado ) 
	select IdEstado, IdFarmacia, (case when IDFarmacia >= 3000 then 1 else 0 end) as ProcesarInformacion, 1 as MostrarEnListado
	from catFarmacias 
	where 
		( 
		IdEstado = 22 
		and 
		IdFarmacia >= 100 
		) 


*/ 
