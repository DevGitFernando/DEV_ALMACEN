If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_IGPI_CFGC_Clientes_Productos' And xType = 'V' )
	Drop view vw_IGPI_CFGC_Clientes_Productos
Go--#SQL  

Create view vw_IGPI_CFGC_Clientes_Productos
As
	
	Select CP.IdCliente, CP.IdProducto, P.Descripcion, CP.CodigoEAN, 
		CP.Status, (case when CP.Status = 1 then 'Asignado' else 'No Asignado' end) as StatusAsignacion, 
		P.Status as StatusProducto 
	From IGPI_CFGC_Clientes_Productos CP (NoLock)
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( CP.IdProducto = P.IdProducto And CP.CodigoEAN = P.CodigoEAN)
Go--#SQL  


--	select top 10 * 	from IGPI_CFGC_Clientes_Productos 

-- select * from vw_IGPI_CFGC_Clientes_Productos 


-- select * from IGPI_CFGC_Clientes_Productos 
