--	Select * From OrdenesDeComprasEnc (NoLock) 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_OrdenesDeComprasEnc' And xType = 'P' )
	Drop Proc spp_Mtto_OrdenesDeComprasEnc
Go--#SQL 

Create Procedure spp_Mtto_OrdenesDeComprasEnc
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioOrdenCompra varchar(32), @FolioOrdenCompraReferencia varchar(32),
	@IdPersonal varchar(6), @FechaRegistro datetime, @FechaSistema datetime, @IdProveedor varchar(6), @ReferenciaDocto varchar(22), 
	@FechaDocto datetime, @FechaVenceDocto datetime, @Observaciones varchar(102), --@EsPromocionRegalo tinyint, 
	@SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), @ImporteFactura numeric(14, 4), 
	@FechaPromesaEntrega datetime, --@PorcSurtido numeric(14,4), 
	@iOpcion smallint, @EsFacturaOriginal tinyint 
) 
With Encryption 
As
Begin
Set NoCount On
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@FolioMovtoInv varchar(22),
		@PorcPuntualidad int

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @FolioMovtoInv = 'EOC'
	Set @PorcPuntualidad = 0

	-- Se obtiene el Folio de Orden de Compra
	If @FolioOrdenCompra = '*' 
	  Begin 
		Select @FolioOrdenCompra = cast( (max(FolioOrdenCompra) + 1) as varchar)  
		From OrdenesDeComprasEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioOrdenCompra = IsNull(@FolioOrdenCompra, '1')
	Set @FolioOrdenCompra = right(replicate('0', 8) + @FolioOrdenCompra, 8)

	-- Se obtiene el Porcentaje de Puntualidad.
	Select @PorcPuntualidad = Datediff( Day, GetDate(), @FechaPromesaEntrega ) 

	If @iOpcion = 1
	  Begin
		If Not Exists ( Select * From OrdenesDeComprasEnc(Nolock) 
						Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrdenCompra = @FolioOrdenCompra ) 
		  Begin
			Insert Into OrdenesDeComprasEnc
				( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, FolioOrdenCompraReferencia, FolioMovtoInv, IdPersonal, FechaRegistro,
				  FechaSistema, IdProveedor, ReferenciaDocto, FechaDocto, FechaVenceDocto, Observaciones, 
				  SubTotal, Iva, Total, ImporteFactura, FechaPromesaEntrega, Status, Actualizado, EsFacturaOriginal
				)
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrdenCompra, @FolioOrdenCompraReferencia, @FolioMovtoInv, @IdPersonal, @FechaRegistro,
				@FechaSistema, @IdProveedor, @ReferenciaDocto, @FechaDocto, @FechaVenceDocto, @Observaciones,  
				@SubTotal, @Iva, @Total, @ImporteFactura, @FechaPromesaEntrega, @sStatus, @iActualizado, @EsFacturaOriginal

			Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioOrdenCompra
		  End
	  End
	Else
	  Begin
		Set @sStatus = 'C'

		Update OrdenesDeComprasEnc Set Status = @sStatus, Actualizado = @iActualizado
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrdenCompra = @FolioOrdenCompra
 
		Set @sMensaje = 'La información del Folio ' + @FolioOrdenCompra + ' ha sido cancelada satisfactoriamente.' 
			
	  End

	-- Regresar la Clave Generada
    Select @FolioOrdenCompra as Clave, @sMensaje as Mensaje 

End
Go--#SQL 

