--	Select * From Adt_OrdenesDeComprasEnc (NoLock) 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ADT_OrdenesDeComprasEnc' And xType = 'P' )
	Drop Proc spp_Mtto_ADT_OrdenesDeComprasEnc
Go--#SQL 

Create Procedure spp_Mtto_ADT_OrdenesDeComprasEnc
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioOrdenCompra varchar(32),
	@ReferenciaDocto varchar(22), @FechaDocto datetime, @FechaVenceDocto datetime, @ImporteFactura numeric(14, 4), 
	@ReferenciaDocto_Anterior varchar(22), @FechaDocto_Anterior datetime, @FechaVenceDocto_Anterior datetime, @ImporteFactura_Anterior numeric(14, 4), 
	@IdPersonal varchar(4) 
) 
With Encryption 
As
Begin
Set NoCount On
	Declare @sMensaje varchar(1000), 
		@FolioMovto varchar(30),
		@sStatus varchar(1), 
		@iActualizado smallint 

	Set DateFormat YMD

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0

	-- Se obtiene el Movimiento
	Select @FolioMovto = cast( (max(FolioMovto) + 1) as varchar)  
	From Adt_OrdenesDeComprasEnc (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioMovto = IsNull(@FolioMovto, '1')
	Set @FolioMovto = right(replicate('0', 8) + @FolioMovto, 8)

	-- Si existe la orden de compra, se actualiza y se inserta en Adt_OrdenesDeCompraEnc
	If Exists ( Select * From OrdenesDeComprasEnc (NoLock) 
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrdenCompra = @FolioOrdenCompra )  
	  Begin 
		-- Se inserta en el Historico de Modificaciones
		Insert Into Adt_OrdenesDeComprasEnc
			(	IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, FolioMovto,
				ReferenciaDocto, FechaDocto, FechaVenceDocto, ImporteFactura, 
				ReferenciaDocto_Anterior, FechaDocto_Anterior, FechaVenceDocto_Anterior, ImporteFactura_Anterior, 
				IdPersonal, FechaRegistro, Status, Actualizado 
			)  
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrdenCompra, @FolioMovto, 
			@ReferenciaDocto, @FechaDocto, @FechaVenceDocto, @ImporteFactura, 
			@ReferenciaDocto_Anterior, @FechaDocto_Anterior, @FechaVenceDocto_Anterior, @ImporteFactura_Anterior, 
			@IdPersonal, GetDate(), @sStatus, @iActualizado  


		-- Se actualiza el Folio de Orden de Compra
		Update OrdenesDeComprasEnc 
		Set ReferenciaDocto = @ReferenciaDocto, FechaDocto = @FechaDocto, FechaVenceDocto = @FechaVenceDocto, ImporteFactura = @ImporteFactura 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrdenCompra = @FolioOrdenCompra
		
	  End
	Set @sMensaje = 'La información del Folio de Orden de Compra ' + @FolioOrdenCompra + ' ha sido actualizada satisfactoriamente '

	-- Regresar la Clave Generada
    Select @FolioOrdenCompra as Clave, @sMensaje as Mensaje 

End
Go--#SQL 

