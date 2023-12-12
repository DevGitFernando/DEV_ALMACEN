
Set dateformat YMD 
Go--#SQL 
	
	If Exists ( Select * From sysobjects (nolock) where name = 'INT_SESEQ__CFG_Replicacion' and xType = 'U' ) Drop Table INT_SESEQ__CFG_Replicacion 
Go--#SQL 


	Select Top 0 Identity(int, 1, 1) as Orden, 
		cast('' as varchar(1000)) as NombreTabla 
	Into INT_SESEQ__CFG_Replicacion 

Go--#SQL 

	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__CFG_Farmacias_UMedicas'  
	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__RecetasElectronicas_XML__Log'  
	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__RecetasElectronicas_XML'  
	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__RecetasElectronicas_0001_General' 
	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__RecetasElectronicas_0002_Causes'  
	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__RecetasElectronicas_0003_Diagnosticos'  
	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__RecetasElectronicas_0004_Insumos'  
	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas'  
	Insert Into INT_SESEQ__CFG_Replicacion ( NombreTabla ) Select 'INT_SESEQ__CGF_ClavesSIAM'  

----INT_SESEQ__CFG_Farmacias_UMedicas
----INT_SESEQ__RecetasElectronicas_XML
----INT_SESEQ__RecetasElectronicas_XML__Log
----INT_SESEQ__RecetasElectronicas_0001_General
----INT_SESEQ__RecetasElectronicas_0002_Causes
----INT_SESEQ__RecetasElectronicas_0003_Diagnosticos
----INT_SESEQ__RecetasElectronicas_0004_Insumos
----INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas
----INT_SESEQ__CGF_ClavesSIAM

Go--#SQL 



	select * 
	from INT_SESEQ__CFG_Replicacion 
