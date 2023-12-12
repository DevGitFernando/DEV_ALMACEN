
---------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------- 


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'FACT_CFD_Documentos_Generados' and Sc.Name = 'UsoDeCFDI' ) 
Begin 	
	Alter Table FACT_CFD_Documentos_Generados Add UsoDeCFDI varchar(6)  Not Null Default '' 
End 
Go--#SQL  	


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'FACT_CFD_Documentos_Generados' and Sc.Name = 'TipoRelacion' ) 
Begin 	
	Alter Table FACT_CFD_Documentos_Generados Add TipoRelacion varchar(6)  Not Null Default '' 
End 
Go--#SQL  	


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'FACT_CFD_Documentos_Generados_Detalles' and Sc.Name = 'SAT_ClaveProducto_Servicio' ) 
Begin 	
	Alter Table FACT_CFD_Documentos_Generados_Detalles Add SAT_ClaveProducto_Servicio varchar(20)  Not Null Default '' 
End 
Go--#SQL  	

---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'FACT_CFD_Documentos_Generados_Detalles' and Sc.Name = 'SAT_UnidadDeMedida' ) 
Begin 	
	Alter Table FACT_CFD_Documentos_Generados_Detalles Add SAT_UnidadDeMedida varchar(5)  Not Null Default '' 
End 
Go--#SQL  	

---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'FACT_CFD_Documentos_Generados_VP' and Sc.Name = 'UsoDeCFDI' ) 
Begin 	
	Alter Table FACT_CFD_Documentos_Generados_VP Add UsoDeCFDI varchar(6)  Not Null Default '' 
End 
Go--#SQL  	


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'FACT_CFD_Documentos_Generados_VP' and Sc.Name = 'TipoRelacion' ) 
Begin 	
	Alter Table FACT_CFD_Documentos_Generados_VP Add TipoRelacion varchar(6)  Not Null Default '' 
End 
Go--#SQL  	


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'FACT_CFD_Documentos_Generados_Detalles_VP' and Sc.Name = 'SAT_ClaveProducto_Servicio' ) 
Begin 	
	Alter Table FACT_CFD_Documentos_Generados_Detalles_VP Add SAT_ClaveProducto_Servicio varchar(20)  Not Null Default '' 
End 
Go--#SQL  	

---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'FACT_CFD_Documentos_Generados_Detalles_VP' and Sc.Name = 'SAT_UnidadDeMedida' ) 
Begin 	
	Alter Table FACT_CFD_Documentos_Generados_Detalles_VP Add SAT_UnidadDeMedida varchar(5)  Not Null Default '' 
End 
Go--#SQL  	

