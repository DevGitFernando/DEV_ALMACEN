--------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
		        Where So.Name = 'VentasDet_Lotes' and Sc.Name = 'EnRemision_Insumo' )  
Begin 
	Alter Table VentasDet_Lotes Add EnRemision_Insumo bit Not Null Default 0 
End 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
		        Where So.Name = 'VentasDet_Lotes' and Sc.Name = 'EnRemision_Admon' )  
Begin 
	Alter Table VentasDet_Lotes Add EnRemision_Admon bit Not Null Default 0 
End 
Go--#SQL 



--------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
		        Where So.Name = 'VentasDet_Lotes' and Sc.Name = 'RemisionFinalizada' )  
Begin 
	Alter Table VentasDet_Lotes Add RemisionFinalizada bit Not Null Default 0 
End 
Go--#SQL 



---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id )   
		        Where So.Name = 'VentasDet_Lotes' and Sc.Name = 'CantidadRemision_Insumo' )    
Begin 
	Alter Table VentasDet_Lotes Add CantidadRemision_Insumo numeric(14,4) Not Null Default 0   
End 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id )    
		        Where So.Name = 'VentasDet_Lotes' and Sc.Name = 'CantidadRemision_Admon' )     
Begin 
	Alter Table VentasDet_Lotes Add CantidadRemision_Admon numeric(14,4)  Not Null Default 0   
End 
Go--#SQL 





---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id )   
		        Where So.Name = 'FACT_Incremento___VentasDet_Lotes' and Sc.Name = 'CantidadRemision_Insumo' )    
Begin 
	Alter Table FACT_Incremento___VentasDet_Lotes Add CantidadRemision_Insumo numeric(14,4) Not Null Default 0   
End 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id )    
		        Where So.Name = 'FACT_Incremento___VentasDet_Lotes' and Sc.Name = 'CantidadRemision_Admon' )     
Begin 
	Alter Table FACT_Incremento___VentasDet_Lotes Add CantidadRemision_Admon numeric(14,4)  Not Null Default 0   
End 
Go--#SQL 


