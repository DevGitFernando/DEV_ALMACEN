
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_COM_GeneraPedido_Concentrado_OCEN' and xType = 'P' ) 
   Drop Proc spp_COM_GeneraPedido_Concentrado_OCEN 
Go--#SQL


Create Proc spp_COM_GeneraPedido_Concentrado_OCEN 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182', @IdPersonal varchar(4) = '0001',
	@Observaciones varchar(200), @FolioPedidoUnidad varchar(6), @IdEstadoRegistra varchar(2) = '21', @IdFarmaciaRegistra varchar(4) = '0001'
)
with encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @sMensaje varchar(1000), 
        @FolioPedido varchar(20), 
        @IdTipoPedido varchar(10),  
        @sStatus varchar(1), @iActualizado smallint 

    Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
    Set @IdTipoPedido = '04' 
    Set @FolioPedido = '*'
	Set @Observaciones = 'PEDIDO ESPECIAL CONCENTRADO ' + @Observaciones

--- update COM_OCEN_PedidosDet_Claves Set Actualizado = 3 

--- Tomar las Claves a solicitar 
    Select IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, 
        IdClaveSSA, Cantidad_EnviadaCentral, Actualizado   
    Into #tmpClaves_Pedido     
    From COM_OCEN_PedidosDet_Claves (NoLock) 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia   --- Toman todos los pedidos de todas las farmacias 
          and Actualizado = 3 

--- Marcar los registros procesados     
    Update D Set Status = 'P', Actualizado = @iActualizado 
    From COM_OCEN_PedidosDet_Claves D (NoLock) 
    Inner Join #tmpClaves_Pedido P (NoLock) 
        On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia
             and D.FolioPedido = P.FolioPedido and D.IdClaveSSA = P.IdClaveSSA  )
             

--- Se Inserta en la tabla COM_OCEN_Pedidos 
    If (select count(*) from #tmpClaves_Pedido Where Cantidad_EnviadaCentral > 0) > 0 
    Begin 
        /* 
            Delete from COM_REG_Pedidos_Productos 
            Delete from COM_REG_Pedidos 
        */ 
    
	    If @FolioPedido = '*' 
	    Begin
		    Select @FolioPedido = cast( (max(FolioPedido) + 1) as varchar) From COM_REG_Pedidos (NoLock)
		    Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdTipoPedido = @IdTipoPedido	 
	    End
	    -- Asegurar que FolioPedido sea valido y formatear la cadena 
	    Set @FolioPedido = IsNull(@FolioPedido, '1')
	    Set @FolioPedido = right(replicate('0', 6) + @FolioPedido, 6)
    	 
	    If Not Exists ( Select FolioPedido From COM_REG_Pedidos (NoLock)
		    Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		    And IdTipoPedido = @IdTipoPedido And FolioPedido = @FolioPedido )   
	    Begin 
    	
		    Insert Into COM_REG_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdPersonal, FechaSistema, FechaRegistro, 
					Observaciones, Status, Actualizado, FolioPedidoUnidad, IdEstadoRegistra, IdFarmaciaRegistra )
		    Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @FolioPedido, @IdPersonal, getdate(), getDate(), 
		           @Observaciones, @sStatus, @iActualizado, @FolioPedidoUnidad, @IdEstadoRegistra, @IdFarmaciaRegistra
    		
    		
		    Insert Into COM_REG_Pedidos_Claves 
	        Select IdEmpresa, IdEstado, @IdFarmacia as IdFarmacia, IdTipoPedido, @FolioPedido as FolioPedido, 
                IdClaveSSA, sum(Cantidad_EnviadaCentral) as CantidadPedido, 0 as CantidadSurtir, 'A' as Status, @iActualizado as Actualizado   
            From #tmpClaves_Pedido (NoLock) Where Cantidad_EnviadaCentral > 0
            Group by IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, IdClaveSSA    	
    		
    		Set @sMensaje = 'Folio para el Pedido a Compras Central, Generado Satisfactoriamente ' + @FolioPedido
    		--- Datos de Salida 
    		Select @FolioPedido  as FolioDePedidoRegional, @sMensaje As Mensaje
    		
	    End 	
	End 
End 
Go--#SQL
    