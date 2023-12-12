If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CIE10_Diagnosticos' and xType = 'V' ) 
   Drop View vw_CIE10_Diagnosticos 
Go--#SQL	

Create View vw_CIE10_Diagnosticos 
With Encryption 
As 

	Select X.IdGrupo, X.GrupoClaves, X.DescripcionGrupo, X.SubGrupo, X.ClaveSubGrupo, X.DescripcionSubGrupo, 
		C.IdDiagnostico, C.ClaveDiagnostico, C.Descripcion as Diagnostico  
	From 
	( 
	Select G.IdGrupo as IdGrupo, G.Claves as GrupoClaves, G.Descripcion as DescripcionGrupo, 
		   D.IdDiagnostico as SubGrupo, D.ClaveDiagnostico as ClaveSubGrupo, D.Descripcion as DescripcionSubGrupo
	From CatCIE10_Grupos G (noLock) 
	Inner Join CatCIE10_Diagnosticos D (NoLock) On ( G.IdGrupo = D.nPadre ) 
	-- Where G.IdGrupo = '1100' 
	) X 
	Inner Join CatCIE10_Diagnosticos C (NoLock) On ( X.ClaveSubGrupo = C.cPadre ) 
	
Go--#SQL	

	
	