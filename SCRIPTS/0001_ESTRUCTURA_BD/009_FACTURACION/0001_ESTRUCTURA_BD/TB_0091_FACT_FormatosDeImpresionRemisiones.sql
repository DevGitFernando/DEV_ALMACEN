
------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_FormatosDeImpresion'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_FormatosDeImpresion  
Go--#SQL    

Create Table FACT_Remisiones_FormatosDeImpresion 
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmacia varchar(4) Not Null,			

	NombreFormato varchar(200) Not Null Default '', 
	DescripcionDeUso varchar(400) Not Null Default '',
	Orden int Not Null Default 0, 

	EsManual bit Not Null Default 'false', 

	Status varchar(4) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table FACT_Remisiones_FormatosDeImpresion Add Constraint PK_FACT_Remisiones_FormatosDeImpresion Primary Key ( IdEmpresa, IdEstado, IdFarmacia, NombreFormato ) 
Go--#SQL 


If Not Exists ( Select * From FACT_Remisiones_FormatosDeImpresion Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES__13_SSH____SinLogos__2018_B' )  Insert Into FACT_Remisiones_FormatosDeImpresion (  IdEmpresa, IdEstado, IdFarmacia, NombreFormato, DescripcionDeUso, Orden, EsManual, Status )  Values ( '001', '13', '0001', 'FACT_REMISIONES__13_SSH____SinLogos__2018_B', 'Segundo Nivel Contrato 2018 (Sin logos)', 0, 0, 'A' )  Else Update FACT_Remisiones_FormatosDeImpresion Set DescripcionDeUso = 'Segundo Nivel Contrato 2018 (Sin logos)', Orden = 0, EsManual = 0, Status = 'A' Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES__13_SSH____SinLogos__2018_B'  
If Not Exists ( Select * From FACT_Remisiones_FormatosDeImpresion Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES__13_SSH_1N___Contrato2019' )  Insert Into FACT_Remisiones_FormatosDeImpresion (  IdEmpresa, IdEstado, IdFarmacia, NombreFormato, DescripcionDeUso, Orden, EsManual, Status )  Values ( '001', '13', '0001', 'FACT_REMISIONES__13_SSH_1N___Contrato2019', 'Primer Nivel Contrato 2019', 1, 0, 'A' )  Else Update FACT_Remisiones_FormatosDeImpresion Set DescripcionDeUso = 'Primer Nivel Contrato 2019', Orden = 1, EsManual = 0, Status = 'A' Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES__13_SSH_1N___Contrato2019'  
If Not Exists ( Select * From FACT_Remisiones_FormatosDeImpresion Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES__13_SSH_2N___Contrato2019' )  Insert Into FACT_Remisiones_FormatosDeImpresion (  IdEmpresa, IdEstado, IdFarmacia, NombreFormato, DescripcionDeUso, Orden, EsManual, Status )  Values ( '001', '13', '0001', 'FACT_REMISIONES__13_SSH_2N___Contrato2019', 'Segundo Nivel Contrato 2019', 2, 0, 'A' )  Else Update FACT_Remisiones_FormatosDeImpresion Set DescripcionDeUso = 'Segundo Nivel Contrato 2019', Orden = 2, EsManual = 0, Status = 'A' Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES__13_SSH_2N___Contrato2019'  
If Not Exists ( Select * From FACT_Remisiones_FormatosDeImpresion Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES_MANUALES__13_SSH_1N___Contrato2019' )  Insert Into FACT_Remisiones_FormatosDeImpresion (  IdEmpresa, IdEstado, IdFarmacia, NombreFormato, DescripcionDeUso, Orden, EsManual, Status )  Values ( '001', '13', '0001', 'FACT_REMISIONES_MANUALES__13_SSH_1N___Contrato2019', 'Primer Nivel Contrato 2019', 1, 1, 'A' )  Else Update FACT_Remisiones_FormatosDeImpresion Set DescripcionDeUso = 'Primer Nivel Contrato 2019', Orden = 1, EsManual = 1, Status = 'A' Where IdEmpresa = '001' and IdEstado = '13' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES_MANUALES__13_SSH_1N___Contrato2019'  

Go--#SQL 

--	sp_generainserts 'FACT_Remisiones_FormatosDeImpresion' ,1 

If Not Exists ( Select * From FACT_Remisiones_FormatosDeImpresion Where IdEmpresa = '001' and IdEstado = '28' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES__28_SST' )  Insert Into FACT_Remisiones_FormatosDeImpresion (  IdEmpresa, IdEstado, IdFarmacia, NombreFormato, DescripcionDeUso, Orden, EsManual, Status )  Values ( '001', '28', '0001', 'FACT_REMISIONES__28_SST', 'Formato de impresión de remisiones 2019', 1, 0, 'A' )  Else Update FACT_Remisiones_FormatosDeImpresion Set DescripcionDeUso = 'Formato de impresión de remisiones 2019', Orden = 1, EsManual = 0, Status = 'A' Where IdEmpresa = '001' and IdEstado = '28' and IdFarmacia = '0001' and NombreFormato = 'FACT_REMISIONES__28_SST'  
Go--#SQL 

