-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 
	Referencia_01 varchar(500) Not Null Default '', 
	IdGrupo int Not Null Default 1, 
	ClaveSSA varchar(50) Not Null Default '', 
	TipoRemision int Not Null Default 0,				-- 1 ==> Producto | 2 ==> Servicio   
	FechaVigencia varchar(10) Not Null Default convert(varchar(10), getdate(), 120), 
	Status varchar(1) Not Null Default 'A', 
) 
Go--#SQL 
 
Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones 
	Add Constraint PKFACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones With NoCheck 
	Add Constraint CK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones___IdGrupo Check Not For Replication (IdGrupo >= 1)
Go--#SQL 


If Not Exists ( Select * From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Where IdFuenteFinanciamiento = '0001' and IdFinanciamiento = '0001' and Referencia_01 = 'H23' and IdGrupo = 1 )  Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones (  IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo, ClaveSSA, FechaVigencia, Status, TipoRemision )  Values ( '0001', '0001', 'H23', 1, '', '2017-07-31', 'A', 1 ) 
 Else Update FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Set ClaveSSA = '', FechaVigencia = '2017-07-31', Status = 'A', TipoRemision = 1 Where IdFuenteFinanciamiento = '0001' and IdFinanciamiento = '0001' and Referencia_01 = 'H23' and IdGrupo = 1  
If Not Exists ( Select * From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Where IdFuenteFinanciamiento = '0001' and IdFinanciamiento = '0001' and Referencia_01 = 'H26' and IdGrupo = 1 )  Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones (  IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo, ClaveSSA, FechaVigencia, Status, TipoRemision )  Values ( '0001', '0001', 'H26', 1, '', '2017-07-31', 'A', 1 ) 
 Else Update FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Set ClaveSSA = '', FechaVigencia = '2017-07-31', Status = 'A', TipoRemision = 1 Where IdFuenteFinanciamiento = '0001' and IdFinanciamiento = '0001' and Referencia_01 = 'H26' and IdGrupo = 1  
If Not Exists ( Select * From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Where IdFuenteFinanciamiento = '0001' and IdFinanciamiento = '0001' and Referencia_01 = 'H61' and IdGrupo = 1 )  Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones (  IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo, ClaveSSA, FechaVigencia, Status, TipoRemision )  Values ( '0001', '0001', 'H61', 1, '', '2017-07-31', 'A', 1 ) 
 Else Update FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Set ClaveSSA = '', FechaVigencia = '2017-07-31', Status = 'A', TipoRemision = 1 Where IdFuenteFinanciamiento = '0001' and IdFinanciamiento = '0001' and Referencia_01 = 'H61' and IdGrupo = 1  
If Not Exists ( Select * From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Where IdFuenteFinanciamiento = '0002' and IdFinanciamiento = '0001' and Referencia_01 = 'H23' and IdGrupo = 1 )  Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones (  IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo, ClaveSSA, FechaVigencia, Status, TipoRemision )  Values ( '0002', '0001', 'H23', 1, '', '2017-07-31', 'A', 1 ) 
 Else Update FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Set ClaveSSA = '', FechaVigencia = '2017-07-31', Status = 'A', TipoRemision = 1 Where IdFuenteFinanciamiento = '0002' and IdFinanciamiento = '0001' and Referencia_01 = 'H23' and IdGrupo = 1  
If Not Exists ( Select * From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Where IdFuenteFinanciamiento = '0002' and IdFinanciamiento = '0001' and Referencia_01 = 'H26' and IdGrupo = 1 )  Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones (  IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo, ClaveSSA, FechaVigencia, Status, TipoRemision )  Values ( '0002', '0001', 'H26', 1, '', '2017-07-31', 'A', 1 ) 
 Else Update FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Set ClaveSSA = '', FechaVigencia = '2017-07-31', Status = 'A', TipoRemision = 1 Where IdFuenteFinanciamiento = '0002' and IdFinanciamiento = '0001' and Referencia_01 = 'H26' and IdGrupo = 1  
If Not Exists ( Select * From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Where IdFuenteFinanciamiento = '0002' and IdFinanciamiento = '0001' and Referencia_01 = 'H61' and IdGrupo = 1 )  Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones (  IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, IdGrupo, ClaveSSA, FechaVigencia, Status, TipoRemision )  Values ( '0002', '0001', 'H61', 1, '', '2017-07-31', 'A', 1 ) 
 Else Update FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones Set ClaveSSA = '', FechaVigencia = '2017-07-31', Status = 'A', TipoRemision = 1 Where IdFuenteFinanciamiento = '0002' and IdFinanciamiento = '0001' and Referencia_01 = 'H61' and IdGrupo = 1  

Go--#SQL 

