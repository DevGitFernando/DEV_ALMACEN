If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_IGPI_CFGC_Productos' and xType = 'V' ) 
   Drop View vw_IGPI_CFGC_Productos 
Go--#SQL   

Create View vw_IGPI_CFGC_Productos 
As 
	Select replace(convert(varchar(10), getdate(), 104 ), '.', '') as ERP_Date, 
		 PC.IdProducto, PC.CodigoEAN, 
		 I.EsMultipicking, 
		 P.Descripcion, P.ClaveSSA, P.DescripcionClave,  
		 Isnull(I.Status, 'C') as Status, (Case when IsNull(I.Status, 'C') = 'A' Then 'Activo' Else 'Cancelado' End )as StatusAux, 
		 ( case when I.Status = 'A' Then I.StatusIGPI Else 1 end ) as StatusIGPI, 
----	 (
----		 case when I.Status = 'A' Then 
----			       ( 
----					   case when I.StatusIGPI = 0 then 'Ingreso permitido' 
----					        when I.StatusIGPI = 1 then 'Ingreso no permitido'
----					        when I.StatusIGPI = 2 then 'Producto debe ser almacenado con caducidad'
----					        when I.StatusIGPI = 4 then 'Sin intrucciones de almacenamiento'
----					        when I.StatusIGPI = 5 then 'Almacenar producto en refrigerador' 
----					   else 
----					        'Ingreso no permitido' 
----					   end 
----			       )
----			  else 
----			     'Ingreso no permitido' 
----			  end
----		 ) as StatusIGPIAux 
    (case when I.Status = 'A' Then S.Descripcion Else 'Ingreso no permitido' End) as StatusIGPIAux 
	From IGPI_CFGC_Clientes_Productos PC (NoLock) 
	Left Join IGPI_CFGC_Productos I (NoLock) On ( I.IdProducto = PC.IdProducto and I.CodigoEAN = PC.CodigoEAN ) 
	Left Join IGPI_CFGC_Productos_Status S (NoLock) On ( I.StatusIGPI = S.StatusIGPI ) 
	Left Join vw_Productos_CodigoEAN P (NoLock) On ( PC.IdProducto = P.IdProducto and PC.CodigoEAN = P.CodigoEAN ) 


Go--#SQL   	

/* 
	select * 
	from vw_IGPI_CFGC_Productos 
	-- where CodigoEAN = '7501626700242' 
	order by StatusIGPI desc 
*/ 		
