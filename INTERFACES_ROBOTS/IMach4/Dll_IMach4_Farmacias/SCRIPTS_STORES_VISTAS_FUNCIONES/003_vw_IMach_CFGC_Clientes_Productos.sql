
If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_IMach_CFGC_Clientes_Productos' And xType = 'V' )
	Drop view vw_IMach_CFGC_Clientes_Productos
Go

Create view vw_IMach_CFGC_Clientes_Productos
As
	
	Select CP.IdCliente, CP.IdProducto, P.Descripcion, CP.CodigoEAN, 
		CP.Status, (case when CP.Status = 1 then 'Asignado' else 'No Asignado' end) as StatusAsignacion, 
		P.Status as StatusProducto
	From IMach_CFGC_Clientes_Productos CP (NoLock)
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( CP.IdProducto = P.IdProducto And CP.CodigoEAN = P.CodigoEAN)
Go


--	select top 10 * 	from IMach_CFGC_Clientes_Productos 
