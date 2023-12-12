

-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_FuentesFinanciamiento__Admon_Seccionado___LOAD'  and xType = 'U' ) 
   Drop Table FACT_FuentesFinanciamiento__Admon_Seccionado___LOAD   
Go--#SQL   

Create Table FACT_FuentesFinanciamiento__Admon_Seccionado___LOAD 
( 
	IdFuenteFinanciamiento varchar(20) Not Null, 
	IdFinanciamiento varchar(20) Not Null, 

	IdEstado varchar(20) Not Null, 
	IdFarmacia varchar(20) Not Null, 

	ClaveSSA varchar(50) Not Null, 
	Referencia_01 varchar(100) Not Null Default '',  --- H's en el caso de HGO 
	Referencia_02 varchar(100) Not Null Default '', 
	Referencia_03 varchar(100) Not Null Default '', 
	Referencia_04 varchar(100) Not Null Default '', 

	Partida int Not Null Default 0, 
	AfectaEstadistica int Not Null Default 0, 

	TasaIva Numeric(14,4) Not Null Default 0,
	Costo Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0,
	ImporteNeto Numeric(14,4) Not Null Default 0,

	Status varchar(1) Not Null Default 'A' 
)
Go--#SQL 




