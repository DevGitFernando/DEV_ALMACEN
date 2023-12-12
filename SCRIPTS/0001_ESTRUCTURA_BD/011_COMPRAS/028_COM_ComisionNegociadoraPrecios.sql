-------------------------------------------------------------------------------------------------------------------------------------------- 
Set NoCount On 
Go--#SQL  

-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_ComisionNegociadoraPrecios' and xType = 'U' ) 
	Drop Table COM_OCEN_ComisionNegociadoraPrecios 
Go--#SQL  

Create Table COM_OCEN_ComisionNegociadoraPrecios
(	
	IdClaveSSA_Sal varchar(4) Not Null,
	IdLaboratorio varchar(4) Not Null Default '',
	Precio numeric(14, 4) Not Null Default 0,
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
)
Go--#SQL 

Alter Table COM_OCEN_ComisionNegociadoraPrecios Add Constraint PK_COM_OCEN_ComisionNegociadoraPrecios
	Primary Key ( IdClaveSSA_Sal, IdLaboratorio ) 
Go--#SQL  

	
Alter Table COM_OCEN_ComisionNegociadoraPrecios Add Constraint FK_COM_OCEN_ComisionNegociadoraPrecios_CatClavesSSA_Sales
	Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL 

Alter Table COM_OCEN_ComisionNegociadoraPrecios Add Constraint FK_COM_OCEN_ComisionNegociadoraPrecios_CatLaboratorios
	Foreign Key ( IdLaboratorio ) References CatLaboratorios ( IdLaboratorio )
Go--#SQL 

--------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_ComisionNegociadoraPrecios_Historico' and xType = 'U' ) 
	Drop Table COM_OCEN_ComisionNegociadoraPrecios_Historico
Go--#SQL  

Create Table COM_OCEN_ComisionNegociadoraPrecios_Historico
(	
	IdClaveSSA_Sal varchar(4) Not Null,
	IdLaboratorio varchar(4) Not Null Default '',
	Precio numeric(14, 4) Not Null Default 0,
	
	Status varchar(1) Not Null Default 'A', 
	FechaRegistroLog datetime Not Null Default getdate()
	
)
Go--#SQL 
