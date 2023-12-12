

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_PedidosEnc_Consignacion' and xType = 'P')
    Drop Proc spp_Mtto_PedidosEnc_Consignacion
Go--#SQL

  
Create Proc spp_Mtto_PedidosEnc_Consignacion 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @Folio varchar(32), @IdPersonal varchar(6), @IdProveedor varchar(4),
	@FechaPedido varchar(10), @FechaEntrega Varchar(10), @ReferenciaPedido varchar(22), @Observaciones varchar(102) , @iOpcion smallint 
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
	Set @iActualizado = 0
	--Set @FolioMovtoInv = ''	


	If @Folio = '*' 
	  Begin 
		Select @Folio = cast( (max(Folio) + 1) as varchar)  
		From PedidosEnc_Consignacion (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que Folio sea valido y formatear la cadena 
	Set @Folio = IsNull(@Folio, '1')
	Set @Folio = right(replicate('0', 8) + @Folio, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From PedidosEnc_Consignacion (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio ) 
			  Begin 
				 Insert Into PedidosEnc_Consignacion 
					 ( IdEmpresa, IdEstado, IdFarmacia, Folio, IdPersonal, FechaRegistro,  
					   ReferenciaPedido, IdProveedor, FechaPedido, FechaEntrega, Observaciones, Status, Actualizado
					 ) 
				 Select 
					   @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @IdPersonal, GetDate(),  
					   @ReferenciaPedido, @IdProveedor, @FechaPedido, @FechaEntrega, @Observaciones, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update PedidosEnc_Consignacion Set 
					IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, Folio = @Folio, IdPersonal = @IdPersonal,
					ReferenciaPedido = @ReferenciaPedido, IdProveedor = @IdProveedor, @FechaPedido = @FechaPedido,
					FechaEntrega = @FechaEntrega, Observaciones = @Observaciones, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @Folio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update PedidosEnc_Consignacion Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio
 
		   Set @sMensaje = 'La información del Folio ' + @Folio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @Folio as Folio, @sMensaje as Mensaje 
End
Go--#SQL 	
