------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_INT_ND_Pedidos' and xType = 'P') 
    Drop Proc spp_Proceso_INT_ND_Pedidos
Go--#SQL 
  
--  Exec spp_Proceso_INT_ND_Pedidos '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Proceso_INT_ND_Pedidos 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0002', 
	@IdProveedor varchar(4) = '0001', @IdPersonal varchar(4) = '0001', @TipoDeFarmacias tinyint = 1,
	@FechaRequeridaEntrega varchar(10) = '2014-11-20', @TipoRemision tinyint = 1
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare 
	@FolioPedido varchar(8), @sMensaje varchar(1000),			
	@sStatus varchar(1), @iActualizado smallint, 
	@bErorres int 

	Set @sMensaje = '' 	
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @FolioPedido = '0'	
	Set @bErorres = 0 
	
	----- TipoRemision = 1 es Traspasos   TipoRemision = 2 es Venta Directa
	
	Select * Into #tmp_vw_Productos_CodigoEAN From vw_Productos_CodigoEAN (Nolock) 
		
	---------------------------------------------------------- Integrar la información de los pedidos  	
	If @bErorres = 0 
		Begin 		
			Select @FolioPedido = cast((max(FolioPedido) + 1) as varchar)  From INT_ND_Pedidos_Enviados (NoLock) 
			
			-- Asegurar que FolioPedido sea valido y formatear la cadena 
			Set @FolioPedido = IsNull(@FolioPedido, '1')
			Set @FolioPedido = right(replicate('0', 8) + @FolioPedido, 8)
			
			If Not Exists ( Select * From INT_ND_Pedidos_Enviados (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
							and IdFarmacia = @IdFarmacia and FolioPedido = @FolioPedido) 
			Begin 
				Insert Into INT_ND_Pedidos_Enviados 
					( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, TipoDeFarmacias, IdProveedor, 
					IdPersonal, FechaRegistro, FechaRequeridaEntrega, Status, Actualizado, TipoRemision ) 
				Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @TipoDeFarmacias, @IdProveedor, 
					@IdPersonal, GETDATE(), @FechaRequeridaEntrega, @sStatus, @iActualizado, @TipoRemision 
			End 
				
				
			----  Se arma la tabla para llenar los detalles  --------------------
			---		Select * From INT_ND_Pedidos_Enviados_Det   -----------------	
			Select 
				@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @FolioPedido as FolioPedido, 	
				CM.CodigoCliente, CM.ReferenciaPedido, C.IdFarmacia as IdFarmaciaPedido, 
				IsNull(P.ClaveSSA, '') as ClaveSSA, CM.CodigoProducto, CM.CodigoEAN, CM.CodigoEAN_Existe, 
				IsNull(P.ContenidoPaquete, 1) as ContenidoPaquete, CM.Cantidad, CM.Precio,
				Cast(0 as Numeric(14, 4)) as PrecioUnitario, 
				IsNull(P.TasaIva, 0) as TasaIva, Cast(0 as Numeric(14, 4)) as Iva, 
				Cast(0 as Numeric(14, 4)) as ImpteIva, 'A' as Status, 0 as Actualizado
			Into #tmpDetalles_Pedido	
			From INT_ND_Pedidos_CargaMasiva CM (nolock)
			Inner Join INT_ND_Clientes C (Nolock) On (CM.CodigoCliente = C.CodigoCliente)  
			Left Join #tmp_vw_Productos_CodigoEAN P (nolock) On ( P.CodigoEAN  = CM.CodigoEAN )
				
				
			Update T Set T.PrecioUnitario = ( T.Precio / T.ContenidoPaquete )
			From #tmpDetalles_Pedido T  
			Where T.ContenidoPaquete > 0 
			
			Insert Into INT_ND_Pedidos_Enviados_Det 
			(
				IdEmpresa, IdEstado, IdFarmacia, FolioPedido, 	
				CodigoCliente, ReferenciaPedido, IdFarmaciaPedido, ClaveSSA, CodigoProducto, CodigoEAN, CodigoEAN_Existe, 
				Cantidad, CantidadRecibida, Precio, PrecioUnitario, TasaIva, Iva, ImpteIva, Status, Actualizado
			)
			Select 
				IdEmpresa, IdEstado, IdFarmacia, FolioPedido, 	
				CodigoCliente, ReferenciaPedido, IdFarmaciaPedido, ClaveSSA, CodigoProducto, CodigoEAN, CodigoEAN_Existe, 
				Sum(Cantidad) as Cantidad, 0 as CantidadRecibida, Precio, PrecioUnitario, TasaIva, Iva, ImpteIva, Status, Actualizado
			From #tmpDetalles_Pedido (Nolock) 
			Group By IdEmpresa, IdEstado, IdFarmacia, FolioPedido, 	
				CodigoCliente, ReferenciaPedido, IdFarmaciaPedido, ClaveSSA, CodigoProducto, CodigoEAN, CodigoEAN_Existe, 
				Precio, PrecioUnitario, TasaIva, Iva, ImpteIva, Status, Actualizado
			
			
			
			Set @sMensaje = 'La Información se genero satisfactoriamente con el Folio : ' + @FolioPedido
			Select @FolioPedido As FolioPedido, @sMensaje As Mensaje

		End 
	---------------------------------------------------------- Integrar la información de los pedidos  
			
	---------------------------------------------------------- RESULTADOS 	
	If @bErorres <>  0 
		Begin
					
			Set @sMensaje = 'Se encontraron Codigos EAN ó Codigo de Clientes que no existen en el Sistema'
			Select @FolioPedido As FolioPedido, @sMensaje As Mensaje
			
			Select * From Rpt_INT_ND_Pedidos_CodigoClientes (Nolock)
			Select * From Rpt_INT_ND_Pedidos_CodigoEAN (Nolock)   
						
		End
	---------------------------------------------------------- RESULTADOS 		
	
End
Go--#SQL 