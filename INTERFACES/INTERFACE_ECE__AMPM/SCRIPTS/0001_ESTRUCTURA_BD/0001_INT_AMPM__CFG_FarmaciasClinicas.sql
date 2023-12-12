Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__CFG_FarmaciasClinicas' and xType = 'U' ) 
   Drop Table INT_AMPM__CFG_FarmaciasClinicas 
Go--#SQL   

Create Table INT_AMPM__CFG_FarmaciasClinicas 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Referencia_AMPM varchar(20) Not Null Default '',  
	Referencia_AMPM_Facturacion varchar(20) Not Null Default '', 
	AtiendeRecetasManuales bit Not Null Default 'False', 
	CargaAutomaticaProductos bit Not Null Default 'False' 
)
Go--#SQL   

Alter Table INT_AMPM__CFG_FarmaciasClinicas 
	Add Constraint PK_INT_AMPM__CFG_FarmaciasClinicas Primary Key ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL   

--	sp_generainserts INT_AMPM__CFG_FarmaciasClinicas ,1 

If Not Exists ( Select * From INT_AMPM__CFG_FarmaciasClinicas Where IdEmpresa = '002' and IdEstado = '09' and IdFarmacia = '0011' )  Insert Into INT_AMPM__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_AMPM )  Values ( '002', '09', '0011', '0011' )    Else Update INT_AMPM__CFG_FarmaciasClinicas Set Referencia_AMPM = '0011' Where IdEmpresa = '002' and IdEstado = '09' and IdFarmacia = '0011'  
If Not Exists ( Select * From INT_AMPM__CFG_FarmaciasClinicas Where IdEmpresa = '002' and IdEstado = '09' and IdFarmacia = '0101' )  Insert Into INT_AMPM__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_AMPM )  Values ( '002', '09', '0101', '0101' )    Else Update INT_AMPM__CFG_FarmaciasClinicas Set Referencia_AMPM = '0101' Where IdEmpresa = '002' and IdEstado = '09' and IdFarmacia = '0101'  

Go--#SQL 


