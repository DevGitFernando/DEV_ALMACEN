If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Validacion_De_Pedidos' and xType = 'P')
    Drop Proc spp_Mtto_INT_ND_Validacion_De_Pedidos
Go--#SQL 
  
Create Proc spp_Mtto_INT_ND_Validacion_De_Pedidos 
( 
	@IdEmpresa varchar(3) = '003', 
	@IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0002', 
	@ReferenciaPedido varchar(20) = '', @FolioPedido varchar(30) = '', @CodigoCliente varchar(20) = '', 	

	@IdProducto varchar(10) = '', @CodigoEAN varchar(32) = '', 	
	@CantidadRecibida numeric(14, 4) = 0, @CantidadExcedente numeric(14, 4) = 0, 
	@CantidadDañadoMalEstado numeric(14, 4) = 0, @CantidadCaducado numeric(14, 4) = 0	  
) 
With Encryption 
As
Begin  
Set NoCount On


	Update P Set Status = 'V', EsValidado = 1, 
		CantidadRecibida = @CantidadRecibida - @CantidadExcedente , 
		CantidadExcedente = @CantidadExcedente, 
		CantidadDañadoMalEstado = @CantidadDañadoMalEstado,   
		CantidadCaducado = @CantidadCaducado  
	From INT_ND_Pedidos_Enviados_Det P (NoLock) 
	Where 
		IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia =  @IdFarmacia 
		and FolioPedido = @FolioPedido and ReferenciaPedido = @ReferenciaPedido 
		and CodigoEAN = @CodigoEAN 
		and CodigoEAN_Existe = 1 

End
Go--#SQL




