
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ComprasEnc' and xType = 'P')
    Drop Proc spp_Mtto_ComprasEnc
Go--#SQL
  
Create Proc spp_Mtto_ComprasEnc 
(	@IdEstado varchar(4), @IdFarmacia varchar(6), @FolioCompra varchar(32), 
	@IdPersonal varchar(6), @IdProveedor varchar(6), 
	@ReferenciaDocto varchar(22), 
	@EsPromocionRegalo tinyint, 
	@FechaDocto datetime, @FechaVenceDocto datetime, @Observaciones varchar(102), 
    @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), 
	@FechaRegistro datetime, @IdEmpresa varchar(5), @iOpcion smallint 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@FolioMovtoInv varchar(22),
		@EsCompraAlmacen smallint,
		@IdAlmacen varchar(4)

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @EsCompraAlmacen = 1; -- Esta linea es de prueba.
	Set @FolioMovtoInv = ''
	Set @EsCompraAlmacen = 0
	Set @IdAlmacen = '00'


	If @FolioCompra = '*' 
	  Begin 
		Select @FolioCompra = cast( (max(FolioCompra) + 1) as varchar)  
		From ComprasEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioCompra = IsNull(@FolioCompra, '1')
	Set @FolioCompra = right(replicate('0', 8) + @FolioCompra, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From ComprasEnc (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra ) 
			  Begin 
				 Insert Into ComprasEnc 
					 ( IdEmpresa, IdEstado, IdFarmacia, IdAlmacen, EsCompraAlmacen, EsPromocionRegalo, FolioCompra, FolioMovtoInv, IdPersonal, 
					   IdProveedor, ReferenciaDocto, FechaDocto, FechaVenceDocto, Observaciones, SubTotal, Iva, 
					   Total, -- FechaRegistro, 
					   Status, Actualizado
					 ) 
				 Select 
					   @IdEmpresa, @IdEstado, @IdFarmacia, @IdAlmacen, @EsCompraAlmacen, @EsPromocionRegalo, @FolioCompra, @FolioMovtoInv, @IdPersonal, 
					   @IdProveedor, @ReferenciaDocto, @FechaDocto, @FechaVenceDocto, @Observaciones,@SubTotal, @Iva, 
					   @Total, -- @FechaRegistro, 
					   @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update ComprasEnc Set 
					IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, IdAlmacen = @IdAlmacen, 
					EsCompraAlmacen = @EsCompraAlmacen, EsPromocionRegalo = @EsPromocionRegalo, 
					FolioCompra = @FolioCompra, FolioMovtoInv = @FolioMovtoInv, 
					IdPersonal = @IdPersonal, IdProveedor = @IdProveedor, ReferenciaDocto = @ReferenciaDocto, 
					FechaDocto = @FechaDocto, FechaVenceDocto = @FechaVenceDocto, Observaciones = @Observaciones, 
					SubTotal = @SubTotal, Iva = @Iva, Total = @Total, -- FechaRegistro = @FechaRegistro, 
					Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioCompra 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update ComprasEnc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra
 
		   Set @sMensaje = 'La información del Folio ' + @FolioCompra + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioCompra as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
