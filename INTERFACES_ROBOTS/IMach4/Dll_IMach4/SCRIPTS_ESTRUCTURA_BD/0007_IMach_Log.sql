If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_Log' and xType = 'U' ) 
   Drop Table IMach_Log
Go--#SQL   

Create Table IMach_Log 
(   
    Keyx int identity, 
	Mensaje varchar(7500) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate()
) 
Go--#SQL  



