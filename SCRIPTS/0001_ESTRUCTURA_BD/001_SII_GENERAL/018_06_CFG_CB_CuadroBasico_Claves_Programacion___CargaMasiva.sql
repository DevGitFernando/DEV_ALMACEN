------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva' and xType = 'U' ) 
	Drop Table CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva 
Go--#SQL 	

Begin 
	Create Table CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva 
	(
		IdEstado varchar(2) Not Null Default '', 
		IdFarmacia varchar(4) Not Null Default '', 
		Farmacia varchar(200) Not Null Default '', 
		IdCliente varchar(4) Not Null Default '', 
		Cliente varchar(200) Not Null Default '', 
		IdSubCliente varchar(4) Not Null Default '', 	
		SubCliente varchar(200) Not Null Default '', 
		Año int Not Null Default 0, 
		Mes int Not Null Default 0, 	
		IdClaveSSA varchar(4) Not Null Default '', 
		ClaveSSA varchar(20) Not Null Default '', 
		ClaveSSA_Proceso varchar(20) Not Null Default '', 
		ClaveValida bit Not Null Default 'False', 
		DescripcionClaveSSA varchar(max) Not Null Default '', 		

		EsAmpliacion bit Not Null Default 'false', 
		IdExcepcion int Not Null Default 0, 
		FechaRegistro  datetime Not Null Default getdate(), 
			
		Cantidad int Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		
		IdEstadoRegistra varchar(2) Not Null Default '', 
		IdFarmaciaRegistra varchar(4) Not Null Default '', 
		IdPersonalRegistra varchar(4) Not Null Default ''  
	) 
End 
Go--#SQL 	

