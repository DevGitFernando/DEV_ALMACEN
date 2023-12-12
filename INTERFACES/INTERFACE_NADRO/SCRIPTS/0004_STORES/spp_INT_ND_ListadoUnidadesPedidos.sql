------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_ListadoUnidadesPedidos' and xType = 'P') 
    Drop Proc spp_INT_ND_ListadoUnidadesPedidos
Go--#SQL 
  
--  Exec spp_INT_ND_ListadoUnidadesPedidos '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_ListadoUnidadesPedidos 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '', 
    @CodigoCliente varchar(20) = '*', 
	@EsDeSurtimiento smallint = 0, 
	@GenerarPedido smallint = 0, @IdPersonal varchar(4) = ''   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD  

Declare 
	@FolioIntegracion int, 
	@sMensaje varchar(1000),			
	@sStatus varchar(1), 
	@iLenEAN smallint, 
	@sCadena varchar(100) 
		
		
---------------- Preparar tabla base   		
	Select 
		convert(varchar(10), E.FechaRegistro, 120) as Fecha, E.FolioPedido, 
		E.IdEmpresa, 
		E.IdEstado, C.Estado, E.IdFarmacia, E.IdFarmaciaSolicita, C.Farmacia, 
		C.CodigoCliente, 
		'TipoDeUnidad' = (case when C.EsDeSurtimiento = 1 then 'NO ADMINISTRADA' Else 'ADMINISTRADA' End), 
		E.IdCliente, E.IdSubCliente, E.IdBeneficiario 
	Into #tmpPedidos 
	From Pedidos_Cedis_Enc E (NoLock) 	
	Inner Join vw_INT_ND_Clientes C (NoLock)   
		On ( E.IdEstado = C.IdEstado and E.IdFarmaciaSolicita = C.IdFarmacia and C.EsDeSurtimiento = @EsDeSurtimiento ) 
	Where 1 = 0 And E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado  
	
	
--		spp_INT_ND_ListadoUnidadesPedidos  	


	If @EsDeSurtimiento = 0 
	Begin 
		Insert Into #tmpPedidos 
		Select 
			convert(varchar(10), E.FechaRegistro, 120) as Fecha, E.FolioPedido, 
			E.IdEmpresa, E.IdEstado, C.Estado, E.IdFarmacia, E.IdFarmaciaSolicita, C.Farmacia, 
			C.CodigoCliente, 
			'Tipo de Unidad' = 'ADMINISTRADA' + space(20), 
			E.IdCliente, E.IdSubCliente, E.IdBeneficiario  
		From Pedidos_Cedis_Enc E (NoLock) 	
		Inner Join vw_INT_ND_Clientes C (NoLock) 
			On ( E.IdEstado = C.IdEstado and E.IdFarmaciaSolicita = C.IdFarmacia and C.EsDeSurtimiento = @EsDeSurtimiento ) 
		Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.EsTransferencia = 1 
			And Not Exists 
			( 
				Select * 
				From INT_ND_Pedidos_Generados_Detalles H (NoLock) 
				Where E.IdEmpresa = H.IdEmpresa and E.IdEstado = H.IdEstado And E.IdFarmacia = H.IdFarmaciaControl  
					 and E.IdFarmaciaSolicita = H.IdFarmaciaSolicita and E.FolioPedido = H.FolioPedidoSolicita 
			)  
	End 
 
 
	If @EsDeSurtimiento = 1 
	Begin 
		Insert Into #tmpPedidos 
		Select 
			convert(varchar(10), E.FechaRegistro, 120) as Fecha, E.FolioPedido, 
			E.IdEmpresa, E.IdEstado, '' as Estado, E.IdFarmacia, E.IdFarmaciaSolicita, '' as Farmacia, 
			IsNull(
			(
				Select FolioReferencia 
				From CatBeneficiarios B (NoLock) 
				Where E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia 
					  And E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente 
					  And E.IdBeneficiario = B.IdBeneficiario 
			), 'NA') as CodigoCliente, 
			'Tipo de Unidad' = 'NO ADMINISTRADA', 
			E.IdCliente, E.IdSubCliente, E.IdBeneficiario   
		From Pedidos_Cedis_Enc E (NoLock) 	
		Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.EsTransferencia = 0 
			And Not Exists 
			( 
				Select * 
				From INT_ND_Pedidos_Generados_Detalles H (NoLock) 
				Where E.IdEmpresa = H.IdEmpresa and E.IdEstado = H.IdEstado And E.IdFarmacia = H.IdFarmaciaControl  
					 and E.IdFarmaciaSolicita = H.IdFarmaciaSolicita and E.FolioPedido = H.FolioPedidoSolicita 
			) 		
		
		
--		spp_INT_ND_ListadoUnidadesPedidos 	 		
--		select  * from #tmpPedidos 
		
		Update P Set Estado = E.Nombre, Farmacia = 'VENTA DIRECTA'
		From #tmpPedidos P (NoLock) 
		Inner Join CatEstados E (NoLock) On ( P.IdEstado = E.IdEstado ) 
		
	
		Update P Set Farmacia =  C.NombreCliente 
		From #tmpPedidos P (NoLock) 
		Inner Join vw_INT_ND_Clientes C (NoLock) On ( P.CodigoCliente = C.CodigoCliente ) 

	End 
	
---------------------------------------- Depurar los pedidos a generar 	
	----Select * From #tmpPedidos 
	----Select * From INT_ND_Pedidos_Generados_Detalles  	
	
--		spp_INT_ND_ListadoUnidadesPedidos 	 	
	
	If @CodigoCliente <> '*' 
	Begin 
		Delete From #tmpPedidos Where CodigoCliente <> @CodigoCliente 
	End 
	
	----Delete #tmpPedidos  
	----From #tmpPedidos P 
	----Inner Join INT_ND_Pedidos_Generados_Detalles H (NoLock) 
	----	On ( P.IdEmpresa = H.IdEmpresa and P.IdEstado = H.IdEstado 
	----		 and P.IdFarmaciaSolicita = H.IdFarmaciaSolicita and P.FolioPedido = H.FolioPedidoSolicita ) 
	
	----Select * From #tmpPedidos 		
---------------------------------------- Depurar los pedidos a generar 		
	


	

---------------------------------------- OBTENER LOS DETALLES DE LOS PEDIDOS   		
--		spp_INT_ND_ListadoUnidadesPedidos 	 

	Select 
		space(20) as FolioGeneracion, space(10) as FechaGeneracion, space(3) as Consecutivo, 
		E.Fecha, E.FolioPedido, 
		E.IdEmpresa, E.IdEstado, E.Estado, E.IdFarmacia, E.IdFarmaciaSolicita, E.Farmacia, E.CodigoCliente, E.TipoDeUnidad, 
		D.ClaveSSA, D.CantidadEnCajas as Cantidad, space(100) as ClaveSSA_ND, 0 as CantidadAsignada     
	Into #tmpDetallesPedido  
	From #tmpPedidos E (NoLock) 
	Inner Join Pedidos_Cedis_Det D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioPedido = D.FolioPedido ) 
	Where 1 = 0 	
	
	Delete From #tmpDetallesPedido  


	If @GenerarPedido = 1 
	Begin 
		Exec spp_INT_ND_GenerarUnidadesPedidos @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal 
	End 
---------------------------------------- OBTENER LOS DETALLES DE LOS PEDIDOS   



----------------------------------------	SALIDA FINAL 
	If @GenerarPedido = 0 
		Begin 
			Select Fecha, 'Folio de Pedido' = E.FolioPedido, 
				'Id Estado' = E.IdEstado, E.Estado, 'Id Farmacia' = E.IdFarmacia, E.Farmacia, 
				'Número de Cliente' = E.CodigoCliente, 
				'Tipo de Unidad' = E.TipoDeUnidad 
			From #tmpPedidos E 
			Order By E.Fecha, E.IdFarmacia, E.FolioPedido 
		End 


	If @GenerarPedido = 1  
		Begin 
			Select Fecha, 'Folio de Pedido' = E.FolioPedido, 
				'Id Estado' = E.IdEstado, E.Estado, 'Id Farmacia' = E.IdFarmacia, E.Farmacia, 
				'Número de Cliente' = E.CodigoCliente, 
				'Tipo de Unidad' = E.TipoDeUnidad 
			From #tmpPedidos E 
			Order By E.Fecha, E.IdFarmacia, E.FolioPedido 
			
			Select 
				FolioGeneracion, FechaGeneracion, Consecutivo, 
				Fecha, FolioPedido, IdEmpresa, IdEstado, Estado, IdFarmacia, IdFarmaciaSolicita, 
				Farmacia, CodigoCliente, TipoDeUnidad, 
				ClaveSSA, Cantidad, ClaveSSA_ND, CantidadAsignada     			
			From #tmpDetallesPedido 
		End 

	
--	Select * From #tmpDetallesPedido 
----------------------------------------	SALIDA FINAL  	
		

	
--		spp_INT_ND_ListadoUnidadesPedidos 	 
	
--		select * from vw_INT_ND_Clientes 
		
		
	
End  
Go--#SQL 

