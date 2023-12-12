Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_SIADISSEP__CFG_Farmacias_UMedicas' and xType = 'U' ) 
   Drop Table INT_SIADISSEP__CFG_Farmacias_UMedicas 
Go--#SQL   

Create Table INT_SIADISSEP__CFG_Farmacias_UMedicas 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Referencia_SIADISSEP varchar(20) Not Null Default '', 
	URL_Interface varchar(1000) Not Null Default ''  
)
Go--#SQL   

Alter Table INT_SIADISSEP__CFG_Farmacias_UMedicas 
	Add Constraint PK_INT_SIADISSEP__CFG_Farmacias_UMedicas Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP ) 
Go--#SQL   

--	sp_generainserts INT_SIADISSEP__CFG_Farmacias_UMedicas ,1 

Go--#SQL 


-- Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2224', '' )   
-- Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2224', '1001' )   
-- Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2224', '476' )   
 Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2383', 'PLSSA008770' )   
 Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2211', 'PLSSA009214' )   
 Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2193', 'PLSSA009223' )   
 Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2223', 'PLSSA009241' )   
 Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2196', 'PLSSA016572' )   
 Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2224', 'PLSSA015230' )   
 Insert Into INT_SIADISSEP__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP )  Values ( '001', '21', '2224', '123' )   
Go--#SQL 

 