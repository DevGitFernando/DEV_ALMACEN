------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarUnidadesPedidos' and xType = 'P') 
    Drop Proc spp_INT_ND_GenerarUnidadesPedidos
Go--#SQL 
  
--  Exec spp_INT_ND_GenerarUnidadesPedidos '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarUnidadesPedidos 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '', @IdPersonal varchar(4) = ''   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD  

Declare 
	@Folio varchar(8), 
	@sFecha varchar(8), 
	@sConsecutivo varchar(3), 
	@sMensaje varchar(1000) 
	

---------------------------------------- Registrar la generacion de pedidos    	
	Select @Folio = max(cast(FolioPedido as int)) + 1 
	From INT_ND_Pedidos_Generados (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Set @Folio = right(replicate('00000000000000' + IsNull(@Folio, 1), 8), 8)  
	

	Set @sFecha = replace(convert(varchar(10), getdate(), 120), '-', '') 
	Select @sConsecutivo = max(cast(Consecutivo as int)) + 1 
	From INT_ND_Pedidos_Generados (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Fecha = @sFecha 
	Set @sConsecutivo = right(replicate('00000000000000' + IsNull(@sConsecutivo, 1), 3), 3)   
	
	
	----		Select @Folio, @sFecha, @sConsecutivo  
---------------------------------------- Registrar la generacion de pedidos    		
		
		
---------------------------------------- OBTENER LOS DETALLES DE LOS PEDIDOS   
	Insert Into #tmpDetallesPedido 
	Select 
		@Folio as FolioGenerado, @sFecha as FechaGeneracion, @sConsecutivo as Consecutivo,  
		E.Fecha, E.FolioPedido, 
		E.IdEmpresa, E.IdEstado, E.Estado, E.IdFarmacia, E.IdFarmaciaSolicita, E.Farmacia, E.CodigoCliente, E.TipoDeUnidad, 
		D.ClaveSSA, D.CantidadEnCajas as Cantidad, space(100) as ClaveSSA_ND, 0 as CantidadAsignada   
	From #tmpPedidos E (NoLock) 
	Inner Join Pedidos_Cedis_Det D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioPedido = D.FolioPedido ) 

	Update P Set 
		-- ClaveSSA_ND = E.ClaveSSA, 
		ClaveSSA_ND = E.ClaveSSA_ND, 	
		CantidadAsignada = P.Cantidad 
	From #tmpDetallesPedido P (NoLock) 	
	Inner Join INT_ND_Existencias E (NoLock) On ( P.ClaveSSA = E.ClaveSSA )  
	
	Delete From #tmpDetallesPedido Where CantidadAsignada = 0 
---------------------------------------- OBTENER LOS DETALLES DE LOS PEDIDOS   

	

---------------------------------------- Insertar los datos del pedido 	
	Insert Into INT_ND_Pedidos_Generados 
	( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, Fecha, Consecutivo, IdPersonal, FechaRegistro, Status, Actualizado ) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @sFecha, @sConsecutivo, @IdPersonal, getdate(), 'A', 0 

	Insert Into INT_ND_Pedidos_Generados_Detalles 
	( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdFarmaciaControl, IdFarmaciaSolicita, FolioPedidoSolicita, Status, Actualizado ) 
	Select Distinct @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, IdFarmacia, IdFarmaciaSolicita, FolioPedido, 'A', 0  
	From #tmpPedidos (NoLock) 


---		sp_listacolumnas INT_ND_Pedidos_Generados_Detalles 

---------------------------------------- Insertar los datos del pedido 			
	
	
--		spp_INT_ND_GenerarUnidadesPedidos 	 
	
--		select * from vw_INT_ND_Clientes 
			
--		select * from INT_ND_Existencias  
	
	
End  
Go--#SQL 

