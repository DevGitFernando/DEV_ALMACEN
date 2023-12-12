If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_IMach_StockProductos' and xType = 'V' ) 
   Drop View vw_IMach_StockProductos
Go--#SQL      

Create View vw_IMach_StockProductos 
As 
	Select S.CodigoEAN, S.Existencia, S.ExistenciaIMach, S.Status, S.Actualizado, 
		P.Descripcion, P.DescripcionClave, TipoDeProducto  
	From IMach_StockProductos S (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( S.CodigoEAN = P.CodigoEAN ) 
Go--#SQL   
	
	
