------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IATP2_Log' and xType = 'U' ) 
   Drop Table IATP2_Log
Go--#SQL   

Create Table IATP2_Log 
(   
    Keyx int identity, 
	Mensaje varchar(7500) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate()
) 
Go--#SQL  



