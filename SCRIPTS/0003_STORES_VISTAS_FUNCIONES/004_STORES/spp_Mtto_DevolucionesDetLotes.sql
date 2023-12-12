If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_DevolucionesDet_Lotes' and xType = 'P' )
    Drop Proc spp_Mtto_DevolucionesDet_Lotes
Go--#SQL
						  
Create Proc spp_Mtto_DevolucionesDet_Lotes 
(	
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), @FolioDevolucion varchar(30), 
	@IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30), @Cant_Devuelta Numeric(14)  
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint, 
	    @EsConsignacion bit 		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end) 	
	-- Set @FolioMovtoInv = '0'


	If Not Exists ( Select * From DevolucionesDet_Lotes (NoLock) 
				   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
						And FolioDevolucion = @FolioDevolucion and  IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote ) 
	  Begin 
		 Insert Into DevolucionesDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, 
				IdProducto, CodigoEAN, ClaveLote, Cant_Devuelta, EsConsignacion, Status, Actualizado ) 
		 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioDevolucion, @IdProducto, @CodigoEAN, @ClaveLote, 
				@Cant_Devuelta, @EsConsignacion, @sStatus, @iActualizado 
	  End 	

End
Go--#SQL
