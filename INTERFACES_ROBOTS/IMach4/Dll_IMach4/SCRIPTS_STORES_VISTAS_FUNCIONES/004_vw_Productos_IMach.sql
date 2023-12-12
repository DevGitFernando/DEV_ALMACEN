If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_Productos_IMach' And xType = 'V' )
	Drop view vw_Productos_IMach
Go--#SQL  

Create view vw_Productos_IMach
As 
/*	
	Select CP.IdCliente, CP.IdProducto, P.Descripcion, CP.CodigoEAN, 
		CP.Status, (case when CP.Status = 1 then 'Asignado' else 'No Asignado' end) as StatusAsignacion, 
		P.Status as StatusProducto
	From IMach_CFGC_Clientes_Productos CP (NoLock)
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( CP.IdProducto = P.IdProducto And CP.CodigoEAN = P.CodigoEAN)
*/

	Select (case when IsNull(IP.IdProducto, 0) = 0 then 0 else 1 end) as EsMach4, 
	    IsNull(EP.ExistenciaIMach, 0) as ExistenciaIMach, 
		P.IdProducto, P.CodigoEAN, P.CodigoEAN_Interno, P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, P.IdGrupoTerapeutico, 
		P.GrupoTerapeutico, P.Descripcion, P.DescripcionCorta, P.IdClasificacion, P.Clasificacion, P.IdTipoProducto, 
		P.TipoDeProducto, P.TasaIva, P.EsControlado, P.EsSectorSalud, P.IdFamilia, P.Familia, P.IdSubFamilia, P.SubFamilia, 
		P.IdSegmento, P.Segmento, P.IdLaboratorio, P.Laboratorio, P.IdPresentacion, P.Presentacion, P.Despatilleo, P.ContenidoPaquete, 
		P.ManejaCodigosEAN, P.Status, 
		P.PrecioVenta  
    From vw_Productos_CodigoEAN P (NoLock) 
    Left Join IMach_CFGC_Clientes_Productos IP (NoLock) On ( P.IdProducto = IP.IdProducto and P.CodigoEAN = IP.CodigoEAN and IP.Status = 1 ) 
	Left Join IMach_StockProductos EP (NoLock) On ( IP.CodigoEAN = EP.CodigoEAN ) 
Go--#SQL  

--	sp_listacolumnas vw_Productos_CodigoEAN 

--	select * from vw_Productos_IMach   
