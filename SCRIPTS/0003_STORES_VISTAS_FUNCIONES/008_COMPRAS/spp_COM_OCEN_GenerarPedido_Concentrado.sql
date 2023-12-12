If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_COM_OCEN_GenerarPedido_Concentrado' and xType = 'P' ) 
   Drop Proc spp_COM_OCEN_GenerarPedido_Concentrado 
Go--#SQL    
/* 
Begin tran 

    Exec spp_COM_OCEN_GenerarPedido_Concentrado 
    Select * From COM_REG_Pedidos (NoLock) 
    Select * From COM_REG_Pedidos_Productos (NoLock)     
    
rollback tran     


*/ 


Create Proc spp_COM_OCEN_GenerarPedido_Concentrado 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', @IdPersonal varchar(4) = '0001',
	@Observaciones varchar(200)
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
    Set @IdTipoPedido = '02' 
    Set @FolioPedido = '*'
	Set @Observaciones = 'PEDIDO ESPECIAL CONCENTRADO ' + @Observaciones

--- update COM_OCEN_PedidosDet Set Actualizado = 3 

--- Tomar las Claves y CodigosEAN a solicitar 
    Select IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, 
        IdClaveSSA, CodigoEAN, Cantidad_EnviadaCentral, Actualizado   
    Into #tmpClaves_Pedido     
    From COM_OCEN_PedidosDet (NoLock) 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado  --- and IdFarmacia = @IdFarmacia   --- Toman todos los pedidos de todas las farmacias 
          and Actualizado = 3 

--- Marcar los registros procesados     
    Update D Set Status = 'P', Actualizado = @iActualizado 
    From COM_OCEN_PedidosDet D (NoLock) 
    Inner Join #tmpClaves_Pedido P (NoLock) 
        On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia
             and D.FolioPedido = P.FolioPedido and D.IdClaveSSA = P.IdClaveSSA and D.CodigoEAN = P.CodigoEAN )
             

--- Se Inserta en la tabla COM_OCEN_Pedidos 
    If (select count(*) from #tmpClaves_Pedido) > 0 
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
    	
		    Insert Into COM_REG_Pedidos ( 
		           IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdPersonal, FechaSistema, FechaRegistro, Observaciones, Status, Actualizado )
		    Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @FolioPedido, @IdPersonal, getdate(), getDate(), 
		           @Observaciones, @sStatus, @iActualizado
    		
    		
		    Insert Into COM_REG_Pedidos_Productos 
	        Select IdEmpresa, IdEstado, @IdFarmacia as IdFarmacia, IdTipoPedido, @FolioPedido as FolioPedido, 
                IdClaveSSA, CodigoEAN, sum(Cantidad_EnviadaCentral) as CantidadPedido, 0 as CantidadSurtir, 'A' as Status, @iActualizado as Actualizado   
            From #tmpClaves_Pedido 
            Group by IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, IdClaveSSA, CodigoEAN    	
    		
    		Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido
    		--- Datos de Salida 
    		Select @FolioPedido  as FolioDePedidoRegional, @sMensaje As Mensaje
    		
	    End 	
	End 
End 
Go--#SQL
    