If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_Exclusiones_Sentencias' and xType = 'U' ) 
   Drop Table Ctl_Exclusiones_Sentencias 
Go--#SQL     

Create Table Ctl_Exclusiones_Sentencias 
( 
	Keyx int identity ( 1, 1 ),  
	Sentencia varchar(200) Not Null, 
	Status varchar(1) Not Null Default ''    
) 
Go--#SQL 
