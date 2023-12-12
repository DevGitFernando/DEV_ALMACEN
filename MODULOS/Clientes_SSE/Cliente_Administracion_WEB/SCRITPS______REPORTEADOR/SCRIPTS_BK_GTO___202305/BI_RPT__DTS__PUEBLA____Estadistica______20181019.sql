---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__DTS__PL___Estadisticas' and xType = 'U' ) 
   Drop Table BI_RPT__DTS__PL___Estadisticas 
Go--#SQL 

Create Table BI_RPT__DTS__PL___Estadisticas 
(
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(4) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	Farmacia varchar(500) Not Null Default '', 


	NivelAtencion varchar(100) Not Null Default '', 
	TipoDeClave varchar(100) Not Null Default '', 
	Año int Not Null Default 0, 
	Mes int Not Null Default 0, 

	  
	PiezasSolicitadas int Not Null Default 0, 


	PiezasSurtidas int Not Null Default 0, 
	PorcentajeSurtido numeric(14,4) Not Null Default 0, 


	PiezasVales int Not Null Default 0, 
	PorcentajeVales numeric(14,4) Not Null Default 0, 

	PiezasNoSurtido int Not Null Default 0, 
	PorcentajeNoSurtido numeric(14,4) Not Null Default 0, 

	Porcentaje__Surtido_Vales numeric(14,4) Not Null Default 0 

)
Go--#SQL    


