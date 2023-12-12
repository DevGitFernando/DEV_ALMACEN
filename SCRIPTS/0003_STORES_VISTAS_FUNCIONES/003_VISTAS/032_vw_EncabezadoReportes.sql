If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_EncabezadoReportes' and xType = 'V' ) 
   Drop view vw_EncabezadoReportes 
Go--#SQL	
 

Create View vw_EncabezadoReportes 
With Encryption 
As 
	Select top 1 EncPrin as EncPrincipal, EncSec as EncSecundario
	From fg_Central_EncabezadoReportes() 
Go--#SQL
 
   	 