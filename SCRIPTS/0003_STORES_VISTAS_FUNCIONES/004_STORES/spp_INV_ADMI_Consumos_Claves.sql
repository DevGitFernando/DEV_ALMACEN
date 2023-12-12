If Exists ( Select * From Sysobjects (NoLock) where Name = 'spp_INV_ADMI_Consumos_Claves' and xType = 'P' ) 
   Drop Proc spp_INV_ADMI_Consumos_Claves 
Go--#SQL  

Create Proc spp_INV_ADMI_Consumos_Claves 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @Año int = 2012, @Mes int = 2 
) 
With Encryption 
As 
Begin 
--Set NoCount On 


	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, -- @Año as Año, @Mes as Mes,  
		D.IdProducto, D.CodigoEAN, sum(D.CantidadVendida) as Cantidad   
	into #tmpConsumos 
	From VentasEnc V (NoLock) 
	Inner Join VentasDet D (NoLock) On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	-- Inner Join vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = D.IdProducto and P.CodigoEAN = D.CodigoEAN ) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado 
		  and Year(V.FechaRegistro) = @Año and Month(V.FechaRegistro) = @Mes 
	Group by V.IdEmpresa, V.IdEstado, V.IdFarmacia, D.IdProducto, D.CodigoEAN  


	Select  
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, F.IdJurisdiccion, @Año as Año, @Mes as Mes,
		P.ClaveSSA, sum(V.Cantidad) as Cantidad  
	Into #tmpConsumos_Resumen   	
	From #tmpConsumos V   
	Inner Join CatFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = V.IdProducto and P.CodigoEAN = V.CodigoEAN )    
	Group by V.IdEmpresa, V.IdEstado, V.IdFarmacia, F.IdJurisdiccion, P.ClaveSSA   
		  
		  
------------------------ 
	Delete From ADMI_Consumos_Claves 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and Año = @Año and Mes = @Mes 
		  
	Insert Into ADMI_Consumos_Claves ( IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, Año, Mes, ClaveSSA, Cantidad ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, Año, Mes, ClaveSSA, Cantidad 
	From #tmpConsumos_Resumen 	  


		  
End 
Go--#SQL 
