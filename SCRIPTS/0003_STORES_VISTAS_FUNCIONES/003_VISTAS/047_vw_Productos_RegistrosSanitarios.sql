----------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Status_RegistrosSanitarios' and xType = 'V' )
	Drop View vw_Status_RegistrosSanitarios
Go--#SQL 

Create View vw_Status_RegistrosSanitarios 
With Encryption 
As 
    --Select 0 as Status, 'Sin especificar' as Descripcion 
    --Union
    --Select 1 as Status, 'Registro Sanitario' as Descripcion -- 'Vigente' as Descripcion 
    --Union
    --Select 2 as Status, 'Tramite' as Descripcion -- 'Renovacion' as Descripcion 
    --Union
    --Select 3 as Status, 'Prorroga' as Descripcion 
    --Union
    --Select 4 as Status, 'Revocada' as Descripcion
    --Union
    --Select 6 as Status, 'Oficio' as Descripcion
    
    
    Select * From CatTipo_RegistrosSanitarios    
    
Go--#SQL 


----------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Productos_RegistrosSanitarios' and xType = 'V' )
	Drop View vw_Productos_RegistrosSanitarios
Go--#SQL

Create View vw_Productos_RegistrosSanitarios 
With Encryption 
As 
	Select 
        P.IdProducto, P.CodigoEAN, P.CodigoEAN_Interno, 
        (IsNull(R.Consecutivo, '0000') + '-' + IsNull(R.Tipo, '000') + '-' + IsNull(R.Año, '0000')) as RegistroSanitario,  
        -- IsNull(R.FechaRegistro, getdate()) as FechaRegistro, 
        convert(varchar(10), IsNull(R.FechaVigencia, getdate()), 120) as FechaVigencia, 
		(case when datediff(dd, getdate(), cast(R.FechaVigencia as datetime)) < 0 And S.TipoCaduca = 1 then 'NO' Else 'SI' End) as Vigente, 
        IsNull(R.Status, -1) as StatusRegistro,         
----       	(  
----		   case when R.Status = 0 then 'Sin especificar'    
----				when R.Status = 1 then 'Vigente' 
----				when R.Status = 2 then 'Renovacion' 
----				when R.Status = 3 then 'Prorroga' 
----				when R.Status = 4 then 'Revocada' 
----				else 'Desconocido'  
----		   end       
----		) as StatusRegistroAux,         
		S.IdTipos,
        IsNull(S.Descripcion, 'Desconocido') as StatusRegistroAux,         
        P.IdClaveSSA_Sal, P.ClaveSSA_Base, P.ClaveSSA, P.DescripcionSal, P.DescripcionClave, P.DescripcionCortaClave, 
        P.IdGrupoTerapeutico, P.GrupoTerapeutico, P.Descripcion, P.DescripcionCorta, P.IdClasificacion, P.Clasificacion, P.IdTipoProducto, P.TipoDeProducto, 
        P.TasaIva, P.EsControlado, P.EsSectorSalud, P.IdFamilia, P.Familia, P.IdSubFamilia, P.SubFamilia, P.IdSegmento, P.Segmento, 
        P.IdLaboratorio, P.Laboratorio, P.IdPresentacion, P.Presentacion, P.Despatilleo, P.ContenidoPaquete, P.ManejaCodigosEAN, P.Status, P.StatusCodigoRel 		   
	From vw_Productos_CodigoEAN P (NoLock) 
	Left Join CatProductos_RegistrosSanitarios R (NoLock) On ( R.IdProducto = P.IdProducto And P.CodigoEAN = R.CodigoEAN ) 
	Left Join vw_Status_RegistrosSanitarios S (NoLock) On ( R.Status = S.IdTipos ) 
Go--#SQL 


----------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_RegistrosSanitarios_CodigoEAN' and xType = 'V' )
	Drop View vw_RegistrosSanitarios_CodigoEAN
Go--#SQL

Create View vw_RegistrosSanitarios_CodigoEAN 
With Encryption 
As 
	Select 
        IsNull(C.Folio, '00000000') As Folio,  P.IdProducto, P.CodigoEAN, P.CodigoEAN_Interno, C.MD5, C.NombreDocto, C.Consecutivo, C.Tipo, C.Año,
        --(IsNull(C.Consecutivo, '0000') + '-' + IsNull(C.Tipo, '000') + '-' + IsNull(C.Año, '0000')) as RegistroSanitario, 
        FolioRegistroSanitario,
        IsNull(C.FechaRegistro, getdate()) as FechaRegistro, 
		IsNull(C.FechaUltimaActualizacion, getdate()) as FechaUltimaActualizacion,  
        convert(varchar(10), IsNull(C.FechaVigencia, getdate()), 120) as FechaVigencia, 
		(case when datediff(dd, getdate(), isNull(cast(C.FechaVigencia as datetime), getdate()-1)) < 0 And S.TipoCaduca = 1 Or S.IdTipos = 0  then 'NO' Else 'SI' End) as Vigente,
        IsNull(R.Status, -1) as StatusRegistro,
        S.IdTipos, IsNull(S.Descripcion, 'Desconocido') as StatusRegistroAux,
        P.IdClaveSSA_Sal, P.ClaveSSA_Base, P.ClaveSSA, P.DescripcionSal, P.DescripcionClave, P.DescripcionCortaClave,
        P.IdGrupoTerapeutico, P.GrupoTerapeutico, P.Descripcion, P.DescripcionCorta, P.IdClasificacion, P.Clasificacion, P.IdTipoProducto, P.TipoDeProducto,
        P.TasaIva, P.EsControlado, P.EsSectorSalud, P.IdFamilia, P.Familia, P.IdSubFamilia, P.SubFamilia, P.IdSegmento, P.Segmento,
        P.IdLaboratorio, P.Laboratorio, P.IdPresentacion, P.Presentacion, P.Despatilleo, P.ContenidoPaquete, P.ManejaCodigosEAN, P.Status, P.StatusCodigoRel,
		C.IdPaisFabricacion, I.NombrePais As PaisDeFabricacion		   
	From vw_Productos_CodigoEAN P (NoLock)
	Left Join CatRegistrosSanitarios_CodigoEAN R (NoLock) On ( R.IdProducto = P.IdProducto And P.CodigoEAN = R.CodigoEAN )
	Left Join CatRegistrosSanitarios C (NoLock) On (R.Folio = C.Folio)
	Left Join vw_Status_RegistrosSanitarios S (NoLock) On ( C.Status = S.IdTipos )
	Left Join CatRegistrosSanitarios_PaisFabricacion I (NoLock) On (C.IdPaisFabricacion = I.IdPais)
	
Go--#SQL


--------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_RegistrosSanitarios' and xType = 'V' )
	Drop View vw_RegistrosSanitarios
Go--#SQL

Create View vw_RegistrosSanitarios 
With Encryption 
As 
	Select C.Folio, C.FechaRegistro as FechaRegistro, C.FechaUltimaActualizacion, 
		C.IdLaboratorio, L.Descripcion As Laboratorio, C.IdClaveSSA_Sal, ClaveSSA, S.Descripcion,
		--C.Consecutivo, C.Tipo, C.Año, 
		C.FolioRegistroSanitario, C.FechaVigencia, 
        C.IdPaisFabricacion, PF.NombrePais, C.IdPresentacion, P.Descripcion as DescripcionPresentacion, C.TipoCaducidad, C.Caducidad, 		
		C.NombreDocto, C.MD5, IsNull(CRI.Documento, '') as Documento, C.Status, IsNull(R.Descripcion, 'Desconocido') as StatusRegistroAux,
	(case when datediff(dd, getdate(), isNull(cast(C.FechaVigencia as datetime), getdate()-1)) < 0 And R.TipoCaduca = 1 Or R.IdTipos = 0  then 'NO' Else 'SI' End) as Vigente,
	(case when datediff(dd, getdate(), isNull(cast(C.FechaVigencia as datetime), getdate()-1)) < 0 And R.TipoCaduca = 1 Or R.IdTipos = 0  then 0 Else 1 End) as EsVigente
	From CatRegistrosSanitarios C (NoLock) 
	Inner Join CatLaboratorios L (NoLock) On ( C.IdLaboratorio = L.IdLaboratorio )
	Inner Join CatClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal )
	Inner Join CatRegistrosSanitarios_PaisFabricacion PF (NoLock) On ( C.IdPaisFabricacion = PF.IdPais  )  
    Inner Join CatRegistrosSanitarios_Presentaciones P (NoLock) On ( C.IdPresentacion = P.IdPresentacion  )  
	Left Join SII_OficinaCentral__RegistrosSanitarios..CatRegistrosSanitarios CRI (NoLock) On ( C.Folio = CRI.Folio ) 
	Left Join vw_Status_RegistrosSanitarios R (NoLock) On ( C.Status = R.IdTipos ) 

	
Go--#SQL 

       