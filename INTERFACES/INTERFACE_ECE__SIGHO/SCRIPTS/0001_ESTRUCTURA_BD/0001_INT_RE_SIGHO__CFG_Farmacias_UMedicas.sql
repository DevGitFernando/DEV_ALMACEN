Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_SIGHO__CFG_Farmacias_UMedicas' and xType = 'U' ) 
   Drop Table INT_RE_SIGHO__CFG_Farmacias_UMedicas 
Go--#SQL   

Create Table INT_RE_SIGHO__CFG_Farmacias_UMedicas 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Referencia_SIADISSEP varchar(20) Not Null Default '', 
	
	TipoDocumento varchar(2) Not Null Default '', 
	/* 
		 R ==> Recetas normales 
		 C ==> Colectivos normales
		 U ==> Recetas unidosis
		CU ==> Colectivos unidosis
	*/ 


	Servidor Varchar(100) Not Null Default '',
	WebService Varchar(100) Not Null Default '',
	PaginaWeb Varchar(100) Not Null Default '',
	SSL Bit Not Null Default 0
)
Go--#SQL   

Alter Table INT_RE_SIGHO__CFG_Farmacias_UMedicas 
	Add Constraint PK_INT_RE_SIGHO__CFG_Farmacias_UMedicas Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP, TipoDocumento ) 
Go--#SQL
 