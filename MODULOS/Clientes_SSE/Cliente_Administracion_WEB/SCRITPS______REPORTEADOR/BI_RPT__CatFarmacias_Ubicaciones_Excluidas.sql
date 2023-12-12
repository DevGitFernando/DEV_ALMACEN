---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__CatFarmacias_Ubicaciones_Excluidas' and xType = 'U' ) 
   Drop Table BI_RPT__CatFarmacias_Ubicaciones_Excluidas 
Go--#SQL 

Create Table BI_RPT__CatFarmacias_Ubicaciones_Excluidas
(
	IdEmpresa varchar(3) NOT NULL,
	IdEstado varchar(2) NOT NULL,
	IdFarmacia varchar(4) NOT NULL,
	IdPasillo int NOT NULL,
	IdEstante int NOT NULL,
	IdEntrepa�o int NOT NULL,
	Status varchar(1) NOT NULL DEFAULT 'A'
)
Go--#SQL


Alter Table BI_RPT__CatFarmacias_Ubicaciones_Excluidas Add Constraint PK_BI_RPT__CatFarmacias_Ubicaciones_Excluidas
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepa�o) 
Go--#SQL   

Alter Table BI_RPT__CatFarmacias_Ubicaciones_Excluidas Add Constraint FK_BI_RPT__CatFarmacias_Ubicaciones_Excluidas__CatPasillos_Estantes_Entrepa�os
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepa�o ) References CatPasillos_Estantes_Entrepa�os
Go--#SQL