If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Programas_SubProgramas' and xType = 'V' ) 
   Drop View vw_Programas_SubProgramas 
Go--#SQL	
 
Create View vw_Programas_SubProgramas 
With Encryption 
As 
	Select P.IdPrograma, P.Descripcion as Programa, P.Status as StatusPrograma, 
		 IsNull(S.IdSubPrograma, '') as IdSubPrograma, 
		 IsNull(S.Descripcion, '') as SubPrograma, IsNull(S.Status, '') as StatusSubPrograma  
	From CatProgramas P (NoLock) 
	Left Join CatSubProgramas S (NoLock) On ( P.IdPrograma = S.IdPrograma ) 
Go--#SQL	
 	

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Programas' and xType = 'V' ) 
   Drop View vw_Programas 
Go--#SQL	
 

Create View vw_Programas 
With Encryption 
As 
	Select Distinct P.IdPrograma, P.Programa, P.StatusPrograma 
	From vw_Programas_SubProgramas P (NoLock) 
Go--#SQL	
 
