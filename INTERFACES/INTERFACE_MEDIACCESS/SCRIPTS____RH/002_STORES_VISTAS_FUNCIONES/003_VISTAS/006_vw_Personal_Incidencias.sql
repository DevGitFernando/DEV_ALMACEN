


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Personal_Incidencias' and xType = 'V' ) 
   Drop View vw_Personal_Incidencias
Go--#SQL

Create View vw_Personal_Incidencias 
With Encryption 
As 
	Select P.IdPersonal, F.NombreCompleto as Personal, P.IdIncidencia, I.Descripcion as Incidencia,
	P.FechaInicio, P.FechaFin, P.FechaRegistro, P.Status
	From CatPersonal_Incidencias P (noLock) 
	Inner Join vw_Personal F (NoLock) On ( P.IdPersonal = F.IdPersonal  ) 
	Inner Join CatIncidencias I(NoLock) On ( P.IdIncidencia = I.IdIncidencia ) 
	
Go--#SQL