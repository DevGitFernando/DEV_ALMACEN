-------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'FACT_CFD_ConceptosEspeciales' and So.xType = 'U' ) 
   Drop Table FACT_CFD_ConceptosEspeciales  
Go--#SQL 

Create Table FACT_CFD_ConceptosEspeciales 
( 
	IdEstado varchar(2) Not Null Default '', 
	IdConcepto varchar(4) Not Null Default '', 	
    Descripcion varchar(300) Not Null Default '',    
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table FACT_CFD_ConceptosEspeciales Add Constraint PK_FACT_CFD_ConceptosEspeciales Primary Key ( IdEstado, IdConcepto )     
Go--#SQL 

Insert Into FACT_CFD_ConceptosEspeciales 
Select '11', '0001', 'SERVICIO INTEGRAL DE ABASTO, DISTRIBUCIÓN Y  DISPENSACIÓN DE MEDICINAS Y PRODUCTOS  FARMACÉUTICOS Y MATERIALES, ACCESORIOS Y  SUMINISTROS MÉDICOS.', 'A', 0 

-- Insert Into FACT_CFD_ConceptosEspeciales Select '11', '0002', 'NOMAS POR QUE SEPA', 'A', 0 

Go--#SQL 

