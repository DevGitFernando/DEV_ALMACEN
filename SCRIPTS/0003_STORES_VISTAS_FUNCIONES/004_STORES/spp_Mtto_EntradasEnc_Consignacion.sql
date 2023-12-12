-----------------------------------------------------------------------------------------------------------------------
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_EntradasEnc_Consignacion' and xType = 'P')
    Drop Proc spp_Mtto_EntradasEnc_Consignacion
Go--#SQL

Create Proc spp_Mtto_EntradasEnc_Consignacion 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioEntrada varchar(32), 
	@FolioMovtoInv varchar(22), @IdPersonal varchar(6), 
	@ReferenciaPedido varchar(22), 
	@Observaciones varchar(102), 
    @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), 
	@iOpcion smallint, @EsReferenciaDePedido bit = '', @FolioPedido varchar(8) = '', @EsPosFechado bit = 0, 
	@ReferenciaDePedidoOC varchar(20) = '', 
	@IdProveedor varchar(4) = '' 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 3	
	--Set @FolioMovtoInv = ''	

	If @ReferenciaDePedidoOC = ''
	Begin 
		Set @ReferenciaDePedidoOC = @ReferenciaPedido 
	End 


	If @FolioEntrada = '*' 
	  Begin 
		Select @FolioEntrada = cast( (max(FolioEntrada) + 1) as varchar)  
		From EntradasEnc_Consignacion (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que FolioEntrada sea valido y formatear la cadena 
	Set @FolioEntrada = IsNull(@FolioEntrada, '1')
	Set @FolioEntrada = right(replicate('0', 8) + @FolioEntrada, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From EntradasEnc_Consignacion (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioEntrada = @FolioEntrada ) 
			  Begin 
				 Insert Into EntradasEnc_Consignacion 
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioEntrada, FolioMovtoInv, IdPersonal, FechaRegistro,  
					   ReferenciaPedido, Observaciones, SubTotal, Iva, Total, Status, Actualizado, EsReferenciaDePedido, 
					   FolioPedido, EsPosFechado, ReferenciaDePedidoOC, IdProveedor
					 ) 
				 Select 
					   @IdEmpresa, @IdEstado, @IdFarmacia, @FolioEntrada, @FolioMovtoInv, @IdPersonal, GetDate(),  
					   @ReferenciaPedido, @Observaciones,@SubTotal, @Iva, @Total, @sStatus, @iActualizado, @EsReferenciaDePedido, 
					   @FolioPedido, @EsPosFechado, @ReferenciaDePedidoOC, @IdProveedor 
              End 
		   Else 
			  Begin 
				Update EntradasEnc_Consignacion Set 
					IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, 
					FolioEntrada = @FolioEntrada, FolioMovtoInv = @FolioMovtoInv, 
					IdPersonal = @IdPersonal, ReferenciaPedido = @ReferenciaPedido, 
					Observaciones = @Observaciones, 
					SubTotal = @SubTotal, Iva = @Iva, Total = @Total, 
					Status = @sStatus, Actualizado = @iActualizado, FolioPedido = @FolioPedido, ReferenciaDePedidoOC = @ReferenciaDePedidoOC 
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioEntrada = @FolioEntrada 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioEntrada 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update EntradasEnc_Consignacion Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioEntrada = @FolioEntrada
 
		   Set @sMensaje = 'La información del Folio ' + @FolioEntrada + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioEntrada as Folio, @sMensaje as Mensaje 

End
Go--#SQL 	
