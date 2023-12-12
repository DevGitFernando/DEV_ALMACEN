Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__CFG_FarmaciasClinicas' and xType = 'U' ) 
   Drop Table INT_MA__CFG_FarmaciasClinicas 
Go--#SQL   

Create Table INT_MA__CFG_FarmaciasClinicas 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Referencia_MA varchar(20) Not Null Default '',  
	Referencia_MA_Facturacion varchar(20) Not Null Default '', 
	AtiendeRecetasManuales bit Not Null Default 'False', 
	CargaAutomaticaProductos bit Not Null Default 'False' 
)
Go--#SQL   

Alter Table INT_MA__CFG_FarmaciasClinicas 
	Add Constraint PK_INT_MA__CFG_FarmaciasClinicas Primary Key ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL   

--	sp_generainserts INT_MA__CFG_FarmaciasClinicas ,1 

If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0011' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '09', '0011', '61204' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '61204' Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0011'  
If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0012' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '09', '0012', '61206' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '61206' Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0012'  
If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0013' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '09', '0013', '61207' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '61207' Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0013'  
If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0014' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '09', '0014', '59341' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '59341' Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0014'  
If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0015' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '09', '0015', '59343' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '59343' Where IdEmpresa = '001' and IdEstado = '09' and IdFarmacia = '0015'  
If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '14' and IdFarmacia = '0011' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '14', '0011', '60837' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '60837' Where IdEmpresa = '001' and IdEstado = '14' and IdFarmacia = '0011'  
If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '19' and IdFarmacia = '0011' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '19', '0011', '61096' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '61096' Where IdEmpresa = '001' and IdEstado = '19' and IdFarmacia = '0011'  
If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '23' and IdFarmacia = '0011' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '23', '0011', '59347' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '59347' Where IdEmpresa = '001' and IdEstado = '23' and IdFarmacia = '0011'  
If Not Exists ( Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '001' and IdEstado = '27' and IdFarmacia = '0011' )  Insert Into INT_MA__CFG_FarmaciasClinicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA )  Values ( '001', '27', '0011', '61384' )    Else Update INT_MA__CFG_FarmaciasClinicas Set Referencia_MA = '61384' Where IdEmpresa = '001' and IdEstado = '27' and IdFarmacia = '0011'  
Go--#SQL 


