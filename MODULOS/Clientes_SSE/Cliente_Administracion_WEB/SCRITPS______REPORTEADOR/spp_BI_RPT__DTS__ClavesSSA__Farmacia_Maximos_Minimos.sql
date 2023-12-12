------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__DTS__Farmacia_ClavesSSA__Maximos_Minimos' and xType = 'U' ) 
   Drop Table BI_RPT__DTS__Farmacia_ClavesSSA__Maximos_Minimos 
Go--#SQL 

Create Table BI_RPT__DTS__Farmacia_ClavesSSA__Maximos_Minimos 
(
	Keyx int Identity(1, 1), 
	IdEstado varchar(4) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	ClaveSSA varchar(20) Not Null Default '', 
	DescripcionSSA varchar(max) Not Null Default '', 
	Presentacion varchar(500) Not Null Default '', 
	CantidadMinima int Not Null Default 0,  
	CantidadMaxima int Not Null Default 0, 
	MesesContenidos numeric(14,4) Not Null Default 0  	
)
Go--#SQL    

Alter Table BI_RPT__DTS__Farmacia_ClavesSSA__Maximos_Minimos Add Constraint PK_BI_RPT__DTS__Farmacia_ClavesSSA__Maximos_Minimos 
	Primary Key ( IdEstado, IdFarmacia, ClaveSSA ) 
Go--#SQL    


/* 
----- HIDALGO 

	Insert Into BI_RPT__DTS__Farmacia_ClavesSSA__Maximos_Minimos ( IdEstado, IdFarmacia, ClaveSSA, DescripcionSSA, Presentacion, CantidadMinima, CantidadMaxima, MesesContenidos ) 
	select IdEstado, IdFarmacia, ClaveSSA, '' as Descripcion, '' as Presentacion, 
		sum(CantidadPresupuestadaPiezas) as CantidadMinima, sum(CantidadPresupuestadaPiezas) as CantidadMaxima, 10 as MesesContenidos 
	from SII_Facturacion_Hidalgo..FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia (NoLock) 
	-- where IdFarmacia = 11 
	where IdFuenteFinanciamiento = 1 
	Group by IdEstado, IdFarmacia, ClaveSSA 



*/
