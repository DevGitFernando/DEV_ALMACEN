If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Net_Modulos_Relacionados' and xType = 'V' ) 
   Drop View vw_Net_Modulos_Relacionados 
Go--#SQL     

Create View vw_Net_Modulos_Relacionados 
As  
	Select R.IdModuloRelacion as IdRelacion, 
		R.IdModulo, M1.Nombre as Modulo, M1.Version, 
		R.IdModuloRelacionado, M2.Nombre as ModuloRelacionado,  M2.Version as VersionRelacionado,
		M2.FechaActualizacion as UltimaActualizacion, R.Status 
	From Net_Modulos_Relacionados R (NoLock) 
	Inner Join Net_Modulos M1 (NoLock) On ( R.IdModulo = M1.IdModulo ) 
	Inner Join Net_Modulos M2 (NoLock) On ( R.IdModuloRelacionado = M2.IdModulo ) 

Go--#SQL   

