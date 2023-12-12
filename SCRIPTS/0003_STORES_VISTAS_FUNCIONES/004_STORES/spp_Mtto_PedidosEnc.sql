

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_PedidosEnc' and xType = 'P')
    Drop Proc spp_Mtto_PedidosEnc
Go--#SQL

  
Create Proc spp_Mtto_PedidosEnc 
(	
    @IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), 
    @FolioPedido varchar(32), @TipoEntrada varchar(8),  
	@FolioMovtoInv varchar(22), @IdPersonal varchar(6), @IdDistribuidor varchar(6), 
	@ReferenciaPedido varchar(22), 
	@Observaciones varchar(102), 
    @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), 
	@iOpcion smallint 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint
		
Declare 
    @EntradaNormal varchar(4), 
    @EntradaConsignacion varchar(4),  		
	@TipoDeEntradaEsConsignacion bit 	
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 3	
	--Set @FolioMovtoInv = ''	
    Set @EntradaNormal = 'EPD' 
    Set @EntradaConsignacion = 'PDC' 
    Set @TipoDeEntradaEsConsignacion = 0 
    
    -- 'EPD', 'PDC' 
    
    if ( @TipoEntrada <> @EntradaNormal ) And ( @TipoEntrada <> @EntradaConsignacion )
    Begin 
        RaisError ('Tipo de Movimiento de Entrada inválido', 16,10 )        
    End 

    If @TipoEntrada = @EntradaConsignacion 
       Set @TipoDeEntradaEsConsignacion = 1 


	If @FolioPedido = '*' 
	  Begin 
		Select @FolioPedido = max(cast(right(FolioPedido, 8) as int)) + 1  
		From PedidosEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		      and EsConsignacion = @TipoDeEntradaEsConsignacion 
	  End 


	-- Asegurar que FolioPedido sea valido y formatear la cadena 
	Set @FolioPedido = IsNull(@FolioPedido, '1')
--	Set @FolioPedido = right(replicate('0', 8) + @FolioPedido, 8)
	Set @FolioPedido = @TipoEntrada + right( replicate('0', 8) + @FolioPedido, 8 )


	If @iOpcion = 1 
       Begin 

		   If Not Exists 
		        ( 
		            Select * From PedidosEnc (NoLock) 
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido
					And TipoDeEntrada =  @TipoEntrada
				) 
			  Begin 
				 Insert Into PedidosEnc 
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, TipoDeEntrada, EsConsignacion, FolioMovtoInv, IdPersonal, FechaRegistro,  
					   IdDistribuidor, ReferenciaPedido, Observaciones, SubTotal, Iva, Total, Status, Actualizado
					 ) 
				 Select 
					   @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @TipoEntrada, @TipoDeEntradaEsConsignacion, @FolioMovtoInv, @IdPersonal, GetDate(),  
					   @IdDistribuidor, @ReferenciaPedido, @Observaciones,@SubTotal, @Iva, @Total, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update PedidosEnc Set 
					IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, 
					FolioPedido = @FolioPedido, FolioMovtoInv = @FolioMovtoInv, 
					IdPersonal = @IdPersonal, IdDistribuidor = @IdDistribuidor, ReferenciaPedido = @ReferenciaPedido, 
					Observaciones = @Observaciones, 
					SubTotal = @SubTotal, Iva = @Iva, Total = @Total, 
					Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido
				And TipoDeEntrada =  @TipoEntrada 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update PedidosEnc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido
		   And TipoDeEntrada =  @TipoEntrada
 
		   Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPedido as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
