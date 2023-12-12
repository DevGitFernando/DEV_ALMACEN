


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CatPersonal_Doctos' and xType = 'V' ) 
Drop View vw_CatPersonal_Doctos
Go--#SQL 
 	
Create View vw_CatPersonal_Doctos 
With Encryption 
As 
	Select C.IdPersonal, P.NombreCompleto, C.IdDocumento, D.Descripcion as Documento,
	C.NombreArchivo, C.Archivo, C.Status 
	From CatPersonal_Doctos  C (NoLock)	 
	Inner Join vw_Personal P (Nolock) On ( C.IdPersonal = P.IdPersonal )
	Inner Join CatTipoDocumentos D (Nolock) On ( C.IdDocumento = D.IdDocumento )
	
Go--#SQL

--		Select * From vw_CatPersonal_Doctos (Nolock)


	