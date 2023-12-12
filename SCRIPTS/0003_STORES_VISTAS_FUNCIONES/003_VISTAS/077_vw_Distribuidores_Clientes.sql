If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Distribuidores_Clientes'  and xType = 'V' ) 
   Drop view vw_Distribuidores_Clientes 
Go--#SQL 

Create View vw_Distribuidores_Clientes 
As 
	Select 
		D.IdDistribuidor, D.NombreDistribuidor, 
		C.IdEstado, E.Nombre as Estado, 
		( case when IsNull(F.NombreFarmacia, '') = '' Then 0 else 1 end) as EsAdministrado,  
		C.CodigoCliente, C.NombreCliente, 
		C.IdFarmacia, IsNull(F.NombreFarmacia, 'SIN MODULO') as Farmacia, 
		C.Status, (case when C.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status_Aux       
	From CatDistribuidores D (NoLock)  
	Inner Join CatDistribuidores_Clientes C (NoLock) On ( D.IdDistribuidor = C.IdDistribuidor )  
	Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) 
	Left Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 

Go--#SQL 

--		select top 1000 * 	from vw_Distribuidores_Clientes 
