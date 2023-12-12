If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_IGPI_SolicitudesDeProductos' and xType = 'V' ) 
   Drop View vw_IGPI_SolicitudesDeProductos 
Go--#SQL      
  
Create View vw_IGPI_SolicitudesDeProductos   
As   
  
	Select S.FolioSolicitud as Folio, S.Consecutivo, P.IdProducto, S.CodigoEAN,   
		P.Descripcion, P.DescripcionClave,   
		S.CantidadSolicitada, S.CantidadSurtida, FechaRegistro, FechaSurtido,   
	    S.StatusIGPI,   
		(  
		   case when S.StatusIGPI = -5 then 'Cancelado ERP'    
				when S.StatusIGPI = -2 then 'Registrado'    
				when S.StatusIGPI = -1 then 'Solicitado'   
				when S.StatusIGPI = 0 then 'En operación'   
				when S.StatusIGPI = 1 then 'En espera'   
				when S.StatusIGPI = 2 then 'Cola de espera llena'   
				when S.StatusIGPI = 3 then 'Interrumpido'   
				when S.StatusIGPI = 4 then 'Terminado'   
				when S.StatusIGPI = 5 then 'Cambio de Cantidad'   
				else 'Desconocido'  
		   end       
		) as StatusIGPI_Aux     
	  -- S.*, P.*   
	 From IGPI_SolicitudesProductos S (NoLock)   
	 Inner Join vw_Productos_CodigoEAN P (NoLock) On ( S.IdProducto = P.IdProducto and S.CodigoEAN = P.CodigoEAN )   

 Go--#SQL   
  
  