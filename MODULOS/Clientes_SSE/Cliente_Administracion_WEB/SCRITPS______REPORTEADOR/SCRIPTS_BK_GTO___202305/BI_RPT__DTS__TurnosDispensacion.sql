---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__DTS__TurnosDispensacion' and xType = 'U' ) 
   Drop Table BI_RPT__DTS__TurnosDispensacion 
Go--#SQL 

Create Table BI_RPT__DTS__TurnosDispensacion
(
	Keyx int identity(1,1), 
	Descripcion Varchar(200) not null Default '',
	HoraEntrada int,
	HoraSalida int
	  
)
Go--#SQL    

Insert Into BI_RPT__DTS__TurnosDispensacion (  Descripcion, HoraEntrada, HoraSalida )  Values ( 'NOCTURNO', 0, 7 )
Insert Into BI_RPT__DTS__TurnosDispensacion (  Descripcion, HoraEntrada, HoraSalida )  Values ( 'MATUTINO', 8, 13 )   
Insert Into BI_RPT__DTS__TurnosDispensacion (  Descripcion, HoraEntrada, HoraSalida )  Values ( 'VESPERTINO', 14, 21 )
Insert Into BI_RPT__DTS__TurnosDispensacion (  Descripcion, HoraEntrada, HoraSalida )  Values ( 'NOCTURNO', 22, 24 )
Go--#SQL    

