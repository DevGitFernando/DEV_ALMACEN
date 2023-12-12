Set NoCount Off 
Go--#SQL   


	If Exists ( Select * From sysobjects (nolock) where name = 'INT_SESEQ__RecetasElectronicas_0001_General' ) 	
	Begin 
		If Not Exists ( Select * From sysobjects so (nolock) Inner Join syscolumns sc (nolock) On ( so.id = sc.id ) where so.name = 'INT_SESEQ__RecetasElectronicas_0001_General' and sc.name = 'EsSurtido_Electronico' ) 	
		Begin 
			Alter table INT_SESEQ__RecetasElectronicas_0001_General add	EsSurtido_Electronico bit Not Null Default 'False' 
		End 
	End  

Go--#SQL   


	If Exists ( Select * From sysobjects (nolock) where name = 'INT_SESEQ__RecetasElectronicas_0001_General' ) 	
	Begin 
		If Not Exists ( Select * From sysobjects so (nolock) Inner Join syscolumns sc (nolock) On ( so.id = sc.id ) where so.name = 'INT_SESEQ__RecetasElectronicas_0001_General' and sc.name = 'RecepcionDuplicada' ) 	
		Begin 
			Alter table INT_SESEQ__RecetasElectronicas_0001_General add	RecepcionDuplicada bit Not Null Default 'False' 
		End 
	End  

Go--#SQL   


	If Exists ( Select * From sysobjects (nolock) where name = 'INT_SESEQ__RecetasElectronicas_0001_General' ) 	
	Begin 
		If Not Exists ( Select * From sysobjects so (nolock) Inner Join syscolumns sc (nolock) On ( so.id = sc.id ) where so.name = 'INT_SESEQ__RecetasElectronicas_0001_General' and sc.name = 'NumeroDeRecepciones' ) 	
		Begin 
			Alter table INT_SESEQ__RecetasElectronicas_0001_General add	NumeroDeRecepciones int Not Null Default 1 
		End 
	End  

Go--#SQL   


	If Exists ( Select * From sysobjects (nolock) where name = 'INT_SESEQ__RecetasElectronicas_0001_General' ) 	
	Begin 
		If Not Exists ( Select * From sysobjects so (nolock) Inner Join syscolumns sc (nolock) On ( so.id = sc.id ) where so.name = 'INT_SESEQ__RecetasElectronicas_0001_General' and sc.name = 'IntentosDeEnvio' ) 	
		Begin 
			Alter table INT_SESEQ__RecetasElectronicas_0001_General add	IntentosDeEnvio int Not Null Default 0  
		End 
	End  

Go--#SQL   


	If Exists ( Select * From sysobjects (nolock) where name = 'INT_SESEQ__RecetasElectronicas_0004_Insumos' ) 	
	Begin 
		If Not Exists ( Select * From sysobjects so (nolock) Inner Join syscolumns sc (nolock) On ( so.id = sc.id ) where so.name = 'INT_SESEQ__RecetasElectronicas_0004_Insumos' and sc.name = 'RecepcionDuplicada' ) 	
		Begin 
			Alter table INT_SESEQ__RecetasElectronicas_0004_Insumos add	RecepcionDuplicada bit Not Null Default 'False' 
		End 
	End  

Go--#SQL   


	update T set EsSurtido_Electronico  = 1 
	From INT_SESEQ__RecetasElectronicas_0001_General T 
	where EsSurtido = 1


	update T set IntentosDeEnvio  = 1 
	From INT_SESEQ__RecetasElectronicas_0001_General T 
	where EsSurtido = 1 and Procesado = 1 


Go--#SQL   


