If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_DevolucionesDet' and xType = 'P' )
    Drop Proc spp_Mtto_DevolucionesDet
Go--#SQL
						  
Create Proc spp_Mtto_DevolucionesDet 
(	
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioDevolucion varchar(30), 
	@IdProducto varchar(8), @CodigoEAN varchar(30), @Unidad tinyint, 
	@Cant_Devuelta Numeric(14,4), @PrecioCosto_Unitario Numeric(14,4), @TasaIva Numeric(14,4), @ImpteIva Numeric(14,4) --, 
    -- @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4) -- , @iOpcion smallint 
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
	-- Set @FolioMovtoInv = '0'


	If Not Exists ( Select * From DevolucionesDet (NoLock) 
				   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevolucion = @FolioDevolucion and  
						 IdProducto = @IdProducto and CodigoEAN = @CodigoEAN ) 
	  Begin 
		 Insert Into DevolucionesDet ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN, 
				Cant_Devuelta, PrecioCosto_Unitario, TasaIva, ImpteIva, Status, Actualizado ) 
		 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioDevolucion, @IdProducto, @CodigoEAN, 
				@Cant_Devuelta, @PrecioCosto_Unitario, @TasaIva, @ImpteIva, @sStatus, @iActualizado 
	  End 	

End
Go--#SQL	


	