------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_ProcesoRemisiones' and xType = 'P' )
    Drop Proc spp_FACT_ProcesoRemisiones
Go--#SQL  
  
Create Proc spp_FACT_ProcesoRemisiones 
With Encryption 
As 
Begin  
Set NoCount On 


------------------------------------------------- REMISIONES GENERADAS   
	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_PRCS__RemisionesGeneradas' and xtype = 'U' and datediff(dd, crdate, getdate()) > 0 ) 
	Begin 
		Drop Table FACT_PRCS__RemisionesGeneradas 
	End 

	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_PRCS__RemisionesGeneradas' and xtype = 'U' ) 
	Begin 
		Select Top 0 
			cast('' as varchar(20)) as IdEstado, 
			cast('' as varchar(20)) as IdFarmaciaGenera, 
			cast('' as varchar(20)) as FolioRemision, 
			cast(0 as numeric(14,4)) as SubTotal_SinGrabar,  
			cast(0 as numeric(14,4)) as SubTotal_Grabado,  
			cast(0 as numeric(14,4)) as Iva,  
			cast(0 as numeric(14,4)) as Importe,  
			cast('' as varchar(100)) as ID_Genera 
		Into FACT_PRCS__RemisionesGeneradas 	 
	End 


------------------------------------------------- CLAVES NO REMISIONADAS 
	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_PRCS__ClavesNoRemisionadas' and xtype = 'U' and datediff(dd, crdate, getdate()) > 0 ) 
	Begin 
		Drop Table FACT_PRCS__ClavesNoRemisionadas 
	End 

	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_PRCS__ClavesNoRemisionadas' and xtype = 'U' ) 
	Begin 
		Select Top 0 
			cast('' as varchar(100)) as ClaveSSA, 
			cast('' as varchar(max)) as DescripcionClaveSSA, 
			cast('' as varchar(100)) as ID_Genera 
		Into FACT_PRCS__ClavesNoRemisionadas 	 
	End 


End
Go--#SQL
