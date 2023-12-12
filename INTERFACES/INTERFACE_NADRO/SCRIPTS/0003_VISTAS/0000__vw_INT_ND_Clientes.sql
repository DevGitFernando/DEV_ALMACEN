----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_INT_ND_Clientes' and xType = 'V' ) 
   Drop View vw_INT_ND_Clientes 
Go--#SQL  

Create View vw_INT_ND_Clientes 
With Encryption 
As 
	
	Select C.IdCliente, C.CodigoCliente, C.Nombre as NombreCliente, 
		C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia, F.IdTipoUnidad, F.TipoDeUnidad, 
		C.EsDeSurtimiento, C.Status  
	From INT_ND_Clientes C (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
	Where EsDeSurtimiento = 0  
	
	Union 
	
	Select C.IdCliente, C.CodigoCliente, C.Nombre as NombreCliente, 
		C.IdEstado, F.Nombre as Estado, C.IdFarmacia, 'NO ADMINISTRADO' As Farmacia, 
		'000' as IdTipoUnidad, 'NO IDENTIFICADO' as TipoDeUnidad, 
		 C.EsDeSurtimiento, C.Status  
	From INT_ND_Clientes C (NoLock) 
	Inner Join CatEstados F (NoLock) On ( C.IdEstado = F.IdEstado ) 
	Where EsDeSurtimiento = 1 	
	
	
Go--#SQL 	
	
--			select * from vw_INT_ND_Clientes 
	