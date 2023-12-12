If Exists ( Select Name From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_Conciliar_OrdenesDeCompra' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_Conciliar_OrdenesDeCompra 
Go--#SQL    

--	CatClavesSSA_SeguroPopular_Anual 

Create Proc spp_Mtto_FACT_Conciliar_OrdenesDeCompra 
(  
	@Dias_Revision int = 30 
)  
With Encryption
As 
Begin 
Set NoCount On 

Declare 
	@FechaInicial varchar(10), 
	@FechaFinal varchar(10) 


	Set @FechaInicial = convert(varchar(10), dateadd(dd, @Dias_Revision * -1, getdate()  ), 120) 
	Set @FechaFinal = convert(varchar(10), getdate(), 120) 


--	select * 	from OrdenesDeComprasEnc   
--------------------------- Obtener las Ordenes de Compras Recibidas     
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompraReferencia as FolioOrden, FolioOrdenCompra   
	Into #tmp_OrdenesDeCompra_Detalles  
	From OrdenesDeComprasEnc (NoLock) 
	Where convert(varchar(10), FechaRegistro, 120) between @FechaInicial and @FechaFinal  
	Group by IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompraReferencia, FolioOrdenCompra  
	
	
	Select Distinct 
		IdEmpresa, IdEstado, IdFarmacia, FolioOrden, 
		space(4) as IdProveedor,  
		getdate() as FechaRegistro, getdate() as  FechaColocacion, 0 as EsContado, 
		cast(0 as numeric(14,4)) as SubTotal_SinGrabar, 
		cast(0 as numeric(14,4)) as SubTotal_Grabado, 
		cast(0 as numeric(14,4)) as Iva, 
		cast(0 as numeric(14,4)) as Total, 
		cast(0 as numeric(14,4)) as SubTotal_SinGrabar_Recibido, 
		cast(0 as numeric(14,4)) as SubTotal_Grabado_Recibido, 
		cast(0 as numeric(14,4)) as Iva_Recibido, 
		cast(0 as numeric(14,4)) as Total_Recibido, space(4) as Status, 100 as Actualizado    		
	Into #tmp_OrdenesDeCompra 	
	From #tmp_OrdenesDeCompra_Detalles 	
--------------------------- Obtener las Ordenes de Compras Recibidas     

--		spp_Mtto_FACT_Conciliar_OrdenesDeCompra  


--------------------------- Valorizar las Ordenes de Compras Recibidas     		
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioOrden,  
		sum(SubTotal_SinGrabar) as SubTotal_SinGrabar, 
		sum(SubTotal_Grabado) as SubTotal_Grabado, 
		sum(Iva) as Iva, 
		( sum(SubTotal_SinGrabar) + sum(SubTotal_Grabado) +sum(Iva) ) as Total 
	Into #tmpTotales_OC 	
	From 
	( 
		Select 
			M.IdEmpresa, M.EstadoEntrega as IdEstado, M.EntregarEn as IdFarmacia, M.FolioOrden as FolioOrden, 
			(case when D.TasaIva = 0 Then (D.Cantidad * (D.Precio - ( D.Precio * (D.Descuento/100.00)))) Else 0 End) as SubTotal_SinGrabar, 
			(case when D.TasaIva > 0 Then (D.Cantidad * (D.Precio - ( D.Precio * (D.Descuento/100.00)))) Else 0 End) as SubTotal_Grabado, 			
			(case when D.TasaIva > 0 Then ( (D.Cantidad * (D.Precio - ( D.Precio * (D.Descuento/100.00)))) * (D.TasaIva / 100.00) ) Else 0 End) as Iva 

		From COM_OCEN_OrdenesCompra_Claves_Enc M (NoLock) 
		Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (NoLock) 
			On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioOrden = D.FolioOrden ) 
		Inner Join #tmp_OrdenesDeCompra OC 	
			On ( M.EstadoEntrega = OC.IdEstado and M.EntregarEn = OC.IdFarmacia and M.FolioOrden = OC.FolioOrden ) 
		-- where M.FolioOrden = 71 	
	) as T_OC 
	Group by IdEmpresa, IdEstado, IdFarmacia, FolioOrden  
	

	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioOrden,  
		sum(SubTotal_SinGrabar) as SubTotal_SinGrabar, 
		sum(SubTotal_Grabado) as SubTotal_Grabado, 
		sum(Iva) as Iva, 
		( sum(SubTotal_SinGrabar) + sum(SubTotal_Grabado) +sum(Iva) ) as Total  
	Into #tmpTotales_OC_Detalles  			
	From 
	( 
		Select 
			M.IdEmpresa, M.IdEstado, M.IdFarmacia, M.FolioOrdenCompraReferencia as FolioOrden, 
			(case when D.TasaIva = 0 Then ( D.CantidadRecibida * D.CostoUnitario ) Else 0 End) as SubTotal_SinGrabar,  
			(case when D.TasaIva > 0 Then ( D.CantidadRecibida * D.CostoUnitario ) Else 0 End) as SubTotal_Grabado, 
			(case when D.TasaIva > 0 Then ( (D.CantidadRecibida * D.CostoUnitario) * (D.TasaIva / 100.00) ) Else 0 End) as Iva 
		From OrdenesDeComprasEnc M (NoLock) 
		Inner Join OrdenesDeComprasDet D (NoLock) 
			On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioOrdenCompra = D.FolioOrdenCompra )  
		Inner Join #tmp_OrdenesDeCompra OC 	
			On ( M.IdEmpresa = OC.IdEmpresa and M.IdEstado = OC.IdEstado and M.IdFarmacia = OC.IdFarmacia 
				 and M.FolioOrdenCompraReferencia = OC.FolioOrden ) 			
	) as T_OC 
	Group by IdEmpresa, IdEstado, IdFarmacia, FolioOrden	
--------------------------- Valorizar las Ordenes de Compras Recibidas     					


--------------------------- Totalizar las Ordenes de Compras Recibidas     					
	Update O Set IdProveedor = OC.IdProveedor, FechaRegistro = OC.FechaRegistro, FechaColocacion = OC.FechaColocacion, Status = OC.Status  
	From #tmp_OrdenesDeCompra O (NoLock) 
	Inner Join COM_OCEN_OrdenesCompra_Claves_Enc OC (NoLock) 
		On ( O.IdEstado = OC.EstadoEntrega and O.IdFarmacia = OC.EntregarEn and O.FolioOrden = OC.FolioOrden )			
			
--		spp_Mtto_FACT_Conciliar_OrdenesDeCompra  			
			
	Update O Set 
		SubTotal_SinGrabar = OC.SubTotal_SinGrabar, SubTotal_Grabado = OC.SubTotal_Grabado, Iva = OC.Iva, Total = OC.Total  		
	From #tmp_OrdenesDeCompra O (NoLock) 
	Inner Join #tmpTotales_OC OC (NoLock) 
		On ( O.IdEstado = OC.IdEstado and O.IdFarmacia = OC.IdFarmacia and O.FolioOrden = OC.FolioOrden )  


	Update O Set 
		SubTotal_SinGrabar_Recibido = OC.SubTotal_SinGrabar, SubTotal_Grabado_Recibido = OC.SubTotal_Grabado, 
		Iva_Recibido = OC.Iva, Total_Recibido = OC.Total  		
	From #tmp_OrdenesDeCompra O (NoLock) 
	Inner Join #tmpTotales_OC_Detalles OC (NoLock) 
		On ( O.IdEmpresa = OC.IdEmpresa and O.IdEstado = OC.IdEstado and O.IdFarmacia = OC.IdFarmacia and O.FolioOrden = OC.FolioOrden )   
--------------------------- Totalizar las Ordenes de Compras Recibidas     


--	select top 1 * 	from COM_OCEN_OrdenesCompra_Claves_Enc here FolioOrden = 630  



--------------------------- Registrar las Ordenes de Compras Recibidas         
    Insert Into FACT_Conciliacion_OrdenesDeCompra
    Select * 
    From #tmp_OrdenesDeCompra  O 
    Where Not Exists 
		( 
			Select * From FACT_Conciliacion_OrdenesDeCompra  F (NoLock) 
			Where O.IdEmpresa = F.IdEmpresa and O.IdEstado = F.IdEstado and O.IdFarmacia = F.IdFarmacia and O.FolioOrden = F.FolioOrden 
		)  
	
	Update O Set Actualizado = 100, 
		SubTotal_SinGrabar_Recibido = OC.SubTotal_SinGrabar, SubTotal_Grabado_Recibido = OC.SubTotal_Grabado, 
		Iva_Recibido = OC.Iva, Total_Recibido = OC.Total  		
	From FACT_Conciliacion_OrdenesDeCompra O (NoLock) 
	Inner Join #tmpTotales_OC_Detalles OC (NoLock) 
		On ( O.IdEmpresa = OC.IdEmpresa and O.IdEstado = OC.IdEstado and O.IdFarmacia = OC.IdFarmacia and O.FolioOrden = OC.FolioOrden )   	
	Where O.Total_Recibido <> OC.Total  	
--------------------------- Registrar las Ordenes de Compras Recibidas     



--		spp_Mtto_FACT_Conciliar_OrdenesDeCompra  

/*     
    Select * 
    From OrdenesDeComprasEnc 
    Where convert(varchar(10), FechaRegistro, 120) between @FechaInicial and @FechaFinal 
*/ 
    
--		delete from FACT_Conciliacion_OrdenesDeCompra 
    
--    select * from FACT_Conciliacion_OrdenesDeCompra where Total <> Total_Recibido 		
    

End 
Go--#SQL 

