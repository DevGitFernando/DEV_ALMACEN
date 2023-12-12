If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IGPI_Log' and xType = 'U' ) 
   Drop Table IGPI_Log
Go--#SQL   

Create Table IGPI_Log 
(   
    Keyx int identity, 
	Mensaje varchar(7500) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate()
) 
Go--#SQL  



