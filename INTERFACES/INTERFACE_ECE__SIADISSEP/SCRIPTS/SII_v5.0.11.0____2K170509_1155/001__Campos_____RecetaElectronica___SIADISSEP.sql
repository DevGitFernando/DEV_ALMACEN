Set NoCount On
Go--#SQL   


	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_SIADISSEP__RecetasElectronicas_0004_Insumos' and xType = 'U' ) 
	Begin 
		If Not Exists ( Select * From Sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
			Where So.Name = 'INT_SIADISSEP__RecetasElectronicas_0004_Insumos' and Sc.Name = 'TipoDeInsumo' ) 
			Alter Table INT_SIADISSEP__RecetasElectronicas_0004_Insumos Add TipoDeInsumo smallint Not Null Default 0 
	End 

Go--#SQL   


