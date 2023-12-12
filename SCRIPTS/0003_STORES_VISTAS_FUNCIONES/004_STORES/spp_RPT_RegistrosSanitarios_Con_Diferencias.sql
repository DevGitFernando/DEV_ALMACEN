----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_RPT_RegistrosSanitarios_Con_Diferencias' and xType = 'P' ) 
   Drop Proc spp_RPT_RegistrosSanitarios_Con_Diferencias 
Go--#SQL 

Create Proc spp_RPT_RegistrosSanitarios_Con_Diferencias
With Encryption 
As 
Begin 
Set DateFormat YMD
Set NoCount On


	Select Distinct FolioRegistroSanitario, Convert(Varchar(10), FechaVigencia, 120) As FechaVigencia, StatusRegistroAux
	Into #Reg2
	From vw_RegistrosSanitarios
	
	Select FolioRegistroSanitario, Count(*) As Registros
	Into #Reg3
	From #Reg2
	Group By FolioRegistroSanitario
	having Count(*) > 1 
	
	Select R.FolioRegistroSanitario, FechaVigencia, StatusRegistroAux, Vigente, ClaveSSA, Descripcion, IdLaboratorio, Laboratorio 
	From vw_RegistrosSanitarios R
	Inner Join #Reg3 T On (T.FolioRegistroSanitario = R.FolioRegistroSanitario)
	Order BY R.FolioRegistroSanitario, Laboratorio

End 
Go--#SQL  