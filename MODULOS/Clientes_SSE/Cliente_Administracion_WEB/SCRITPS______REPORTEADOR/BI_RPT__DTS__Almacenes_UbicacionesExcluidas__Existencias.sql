---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__DTS__Almacenes_UbicacionesExcluidas__Existencias' and xType = 'U' ) 
   Drop Table BI_RPT__DTS__Almacenes_UbicacionesExcluidas__Existencias 
Go--#SQL 

Create Table BI_RPT__DTS__Almacenes_UbicacionesExcluidas__Existencias
(
	Keyx int identity(1,1), 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(4) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null  
)
Go--#SQL    

Insert Into BI_RPT__DTS__Almacenes_UbicacionesExcluidas__Existencias ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño)  
	Values ( '001', '21', '3182', 0, 0, 0 ) 

Insert Into BI_RPT__DTS__Almacenes_UbicacionesExcluidas__Existencias ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño)  
	Values ( '001', '21', '3182', 24, 1, 1 ) 

Insert Into BI_RPT__DTS__Almacenes_UbicacionesExcluidas__Existencias ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño)  
	Values ( '001', '21', '3182', 26, 1, 1 ) 

Insert Into BI_RPT__DTS__Almacenes_UbicacionesExcluidas__Existencias ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño)  
	Values ( '001', '21', '3182', 82, 1, 1 ) 

Insert Into BI_RPT__DTS__Almacenes_UbicacionesExcluidas__Existencias ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño)  
	Values ( '001', '21', '3182', 100, 1, 1 ) 
Go--#SQL    

