
---------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------- 


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'CFDI_Documentos' and Sc.Name = 'UsoDeCFDI' ) 
Begin 	
	Alter Table CFDI_Documentos Add UsoDeCFDI varchar(6)  Not Null Default '' 
End 
Go--#SQL  	


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'CFDI_Documentos' and Sc.Name = 'TipoRelacion' ) 
Begin 	
	Alter Table CFDI_Documentos Add TipoRelacion varchar(6)  Not Null Default '' 
End 
Go--#SQL  	


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'CFDI_Documentos_Conceptos' and Sc.Name = 'SAT_ClaveProducto_Servicio' ) 
Begin 	
	Alter Table CFDI_Documentos_Conceptos Add SAT_ClaveProducto_Servicio varchar(20)  Not Null Default '' 
End 
Go--#SQL  	

---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'CFDI_Documentos_Conceptos' and Sc.Name = 'SAT_UnidadDeMedida' ) 
Begin 	
	Alter Table CFDI_Documentos_Conceptos Add SAT_UnidadDeMedida varchar(5)  Not Null Default '' 
End 
Go--#SQL  	



------------------------------------------------------------------------------------------------------------------------------ 
--If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
--	Where So.Name = 'FACT_CFD_Conceptos' and Sc.Name = 'SAT_ClaveProducto_Servicio' ) 
--Begin 	
--	Alter Table FACT_CFD_Conceptos Add SAT_ClaveProducto_Servicio varchar(20)  Not Null Default '' 
--End 
--Go--#xSQL  	

------------------------------------------------------------------------------------------------------------------------------ 
--If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
--	Where So.Name = 'FACT_CFD_Conceptos' and Sc.Name = 'SAT_UnidadDeMedida' ) 
--Begin 	
--	Alter Table FACT_CFD_Conceptos Add SAT_UnidadDeMedida varchar(5)  Not Null Default '' 
--End 
--Go--#xSQL  	

