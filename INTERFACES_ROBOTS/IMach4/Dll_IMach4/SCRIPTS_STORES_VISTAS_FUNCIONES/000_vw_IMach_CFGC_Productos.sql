If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_IMach_CFGC_Productos' and xType = 'V' ) 
   Drop View vw_IMach_CFGC_Productos 
Go--#SQL   

Create View vw_IMach_CFGC_Productos 
As 
	Select replace(convert(varchar(10), getdate(), 104 ), '.', '') as ERP_Date, 
		 PC.IdProducto, PC.CodigoEAN, 
		 I.EsMultipicking, 
		 P.Descripcion, P.ClaveSSA, P.DescripcionClave,  
		 Isnull(I.Status, 'C') as Status, (Case when IsNull(I.Status, 'C') = 'A' Then 'Activo' Else 'Cancelado' End )as StatusAux, 
		 ( case when I.Status = 'A' Then I.StatusIMach Else 1 end ) as StatusIMach, 
----	 (
----		 case when I.Status = 'A' Then 
----			       ( 
----					   case when I.StatusIMach = 0 then 'Ingreso permitido' 
----					        when I.StatusIMach = 1 then 'Ingreso no permitido'
----					        when I.StatusIMach = 2 then 'Producto debe ser almacenado con caducidad'
----					        when I.StatusIMach = 4 then 'Sin intrucciones de almacenamiento'
----					        when I.StatusIMach = 5 then 'Almacenar producto en refrigerador' 
----					   else 
----					        'Ingreso no permitido' 
----					   end 
----			       )
----			  else 
----			     'Ingreso no permitido' 
----			  end
----		 ) as StatusIMachAux 
    (case when I.Status = 'A' Then S.Descripcion Else 'Ingreso no permitido' End) as StatusIMachAux 
	From IMach_CFGC_Clientes_Productos PC (NoLock) 
	Left Join IMach_CFGC_Productos I (NoLock) On ( I.IdProducto = PC.IdProducto and I.CodigoEAN = PC.CodigoEAN ) 
	Left Join IMach_CFGC_Productos_Status S (NoLock) On ( I.StatusIMach = S.StatusIMach ) 
	Left Join vw_Productos_CodigoEAN P (NoLock) On ( PC.IdProducto = P.IdProducto and PC.CodigoEAN = P.CodigoEAN ) 


Go--#SQL   	

/* 
	select * 
	from vw_IMach_CFGC_Productos 
	-- where CodigoEAN = '7501626700242' 
	order by StatusIMach desc 
*/ 		
