--- Generar la Tabla con los dias del periodo solicitado  --- tempdb.. 
If Exists ( Select Name From sysobjects (nolock) Where Name = 'Rpt_CTE_Tipos_Sancion' and xType = 'U' ) 
   Drop Table Rpt_CTE_Tipos_Sancion 
Go--#xxSQL 
   
Create Table Rpt_CTE_Tipos_Sancion    
( 
	Tipo int Not Null Default 1, 
	Sancion varchar(50) Not Null Default '' 
)    
Go--#xxSQL 

/* 
Insert Into #tmp_Tipos select 1, 'Piezas'  
Insert Into #tmp_Tipos select 2, 'Sanción'  
Insert Into #tmp_Tipos select 3, 'Porcentaje sanción'  	
Go--#xxSQL 
*/

------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From tempdb..sysobjects (nolock) Where Name = 'Rpt_CTE_Sansiones' and xType = 'U' ) 
   Drop Table Rpt_CTE_Sansiones  
Go--#xxSQL 
	
Create Table Rpt_CTE_Sansiones 
( 
	Keyx int identity(1,1), 
	IdEstado varchar(2) Not Null Default '', 
	Estado varchar(200) Not Null Default '', 

	IdJurisdiccion  varchar(4) Not Null Default '', 
	Jurisdiccion varchar(200) Not Null Default '', 	
	IdFarmacia  varchar(4) Not Null Default '', 
	Farmacia varchar(200) Not Null Default '', 	

	ClaveSSA varchar(30) Not Null Default '', 
	DescripcionClave varchar(7800) Not Null Default '', 
	Presentacion varchar(50) Not Null Default '', 
	PrecioLicitacion numeric(14,4) Not Null Default 0, 
	Tipo int Not Null Default 1, 
	Año int Not Null Default 0,  
	Mes int Not Null Default 0,  		
	-- NombreMes varchar(20) Not Null Default '', 
	Total Numeric(14,4) Not Null Default 0,   
	Sancion Numeric(14,4) Not Null Default 0, 

	Dia_01 numeric(14,4), 
	Dia_02 numeric(14,4), 
	Dia_03 numeric(14,4), 
	Dia_04 numeric(14,4), 
	Dia_05 numeric(14,4), 
	Dia_06 numeric(14,4), 
	Dia_07 numeric(14,4), 
	Dia_08 numeric(14,4), 
	Dia_09 numeric(14,4), 
	Dia_10 numeric(14,4), 
	Dia_11 numeric(14,4),	
	Dia_12 numeric(14,4), 
	Dia_13 numeric(14,4), 
	Dia_14 numeric(14,4), 
	Dia_15 numeric(14,4), 
	Dia_16 numeric(14,4), 
	Dia_17 numeric(14,4), 
	Dia_18 numeric(14,4), 
	Dia_19 numeric(14,4), 
	Dia_20 numeric(14,4), 
	Dia_21 numeric(14,4), 
	Dia_22 numeric(14,4), 
	Dia_23 numeric(14,4), 
	Dia_24 numeric(14,4), 
	Dia_25 numeric(14,4), 
	Dia_26 numeric(14,4), 
	Dia_27 numeric(14,4), 
	Dia_28 numeric(14,4), 
	Dia_29 numeric(14,4), 
	Dia_30 numeric(14,4), 
	Dia_31 numeric(14,4)
)    
Go--#xxSQL 
