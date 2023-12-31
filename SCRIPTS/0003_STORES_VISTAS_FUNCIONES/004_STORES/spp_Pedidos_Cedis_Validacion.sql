If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Pedidos_Cedis_Validacion' and xType = 'P' ) 
   Drop Proc spp_Pedidos_Cedis_Validacion 
Go--#SQL

Create Proc spp_Pedidos_Cedis_Validacion ( 
    @IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioSurtido varchar(8),
	@IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30), @Cantidad int )
With Encryption 
As 
Begin 
Set DateFormat YMD 
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


	If Not Exists ( Select * From Pedidos_Cedis_Validacion (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioSurtido = @FolioSurtido And 
			 IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote)
	   Begin 
	       Insert Into Pedidos_Cedis_Validacion ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdProducto, CodigoEAN, ClaveLote, Cantidad ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioSurtido, @IdProducto, @CodigoEAN, @ClaveLote, @Cantidad
       End
	Else
		Begin
			UpDate V Set Cantidad = @Cantidad
			From Pedidos_Cedis_Validacion V (NoLock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioSurtido = @FolioSurtido And 
				 IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote
		End
	       
End 
Go--#SQL
