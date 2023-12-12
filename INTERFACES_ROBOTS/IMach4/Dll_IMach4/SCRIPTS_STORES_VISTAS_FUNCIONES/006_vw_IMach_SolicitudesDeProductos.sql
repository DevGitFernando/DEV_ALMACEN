If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_IMach_SolicitudesDeProductos' and xType = 'V' ) 
   Drop View vw_IMach_SolicitudesDeProductos 
Go--#SQL      
  
Create View vw_IMach_SolicitudesDeProductos   
As   
  
	Select S.FolioSolicitud as Folio, S.Consecutivo, P.IdProducto, S.CodigoEAN,   
		P.Descripcion, P.DescripcionClave,   
		S.CantidadSolicitada, S.CantidadSurtida, FechaRegistro, FechaSurtido,   
	    S.StatusIMach,   
		(  
		   case when S.StatusIMach = -5 then 'Cancelado ERP'    
				when S.StatusIMach = -2 then 'Registrado'    
				when S.StatusIMach = -1 then 'Solicitado'   
				when S.StatusIMach = 0 then 'En operación'   
				when S.StatusIMach = 1 then 'En espera'   
				when S.StatusIMach = 2 then 'Cola de espera llena'   
				when S.StatusIMach = 3 then 'Interrumpido'   
				when S.StatusIMach = 4 then 'Terminado'   
				when S.StatusIMach = 5 then 'Cambio de Cantidad'   
				else 'Desconocido'  
		   end       
		) as StatusIMach_Aux     
	  -- S.*, P.*   
	 From IMach_SolicitudesProductos S (NoLock)   
	 Inner Join vw_Productos_CodigoEAN P (NoLock) On ( S.IdProducto = P.IdProducto and S.CodigoEAN = P.CodigoEAN )   

 Go--#SQL   
  
  