If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Versiones' and xType = 'U' ) 
   Drop Table Net_Versiones 
Go--#xxSQL   

Create Table Net_Versiones 
( 
	IdVersion int identity(1,1), 
	NombreVersion varchar(100) Not Null Default 0, 
	Version varchar(20) Not Null Default 0, 
	FechaRegistro datetime Not Null Default getdate() 
) 
Go--#xxSQL   
   