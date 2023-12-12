If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_DevolucionTransferenciasDet_Lotes' and xType = 'P' )
    Drop Proc spp_Mtto_DevolucionTransferenciasDet_Lotes
Go--#SQL
	  
Create Proc spp_Mtto_DevolucionTransferenciasDet_Lotes 
(	
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioDevolucion varchar(30), @IdProducto Varchar(8), @CodigoEAN Varchar(30),
	@Renglon Int, @Cant_Enviada Numeric(14, 4), @Cant_Devuelta Numeric(14, 4), @CantidadEnviada Numeric(14, 4), @IdSubFarmacia varchar(2),
	@ClaveLote varchar(30), @EsConsignacion bit
 ) 
With Encryption  
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	-- Set @FolioMovtoInv = '0'
	
	--Select @SubTotal = Sum(Importe), @Iva = Sum((Costo * Cantidad) * (TasaIva/100)), @Total = Sum(Importe * (1 +TasaIva/100))
	--From MovtosInv_Det_CodigosEAN (NoLock)
	--Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioMovtoInv


	If Not Exists ( Select * From DevolucionTransferenciasDet_Lotes (NoLock) 
				   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevolucion = @FolioDevolucion 
				   and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote ) 
	  Begin 
		 Insert Into DevolucionTransferenciasDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN,
				Renglon, Cant_Enviada, Cant_Devuelta, CantidadEnviada, IdSubFarmacia, ClaveLote, EsConsignacion, Status, Actualizado ) 
		 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioDevolucion, @IdProducto, @CodigoEAN,
				@Renglon, @Cant_Enviada, @Cant_Devuelta, @CantidadEnviada, @IdSubFarmacia, @ClaveLote, @EsConsignacion, @sStatus, @iActualizado 
	  End 
	                             
	Set @sMensaje = 'La informaci�n se guardo satisfactoriamente con el Folio ' + @FolioDevolucion 
		   

	-- Regresar la Clave Generada
    Select @FolioDevolucion as Folio, @sMensaje as Mensaje 
End
Go--#SQL	
