
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_DevolucionesProveedorEnc' and xType = 'P')
    Drop Proc spp_Mtto_DevolucionesProveedorEnc
Go--#SQL
  
Create Proc spp_Mtto_DevolucionesProveedorEnc 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioDevProv varchar(32), 
	@IdPersonal varchar(6), @IdProveedor varchar(6), @ReferenciaDocto varchar(22), 
	--	@EsPromocionRegalo tinyint, 
	@FechaDocto datetime, @FechaVenceDocto datetime, @Observaciones varchar(102), 
    @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), 
	@FechaRegistro datetime,  @iOpcion smallint 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@FolioMovtoInv varchar(22)--,
		----@EsCompraAlmacen smallint,
		----@IdAlmacen varchar(4)

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	--Set @EsCompraAlmacen = 1; -- Esta linea es de prueba.
	Set @FolioMovtoInv = ''
	--Set @EsCompraAlmacen = 0
	--Set @IdAlmacen = '00'


	If @FolioDevProv = '*' 
	  Begin 
		Select @FolioDevProv = cast( (max(FolioDevProv) + 1) as varchar)  
		From DevolucionesProveedorEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioDevProv = IsNull(@FolioDevProv, '1')
	Set @FolioDevProv = right(replicate('0', 8) + @FolioDevProv, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From DevolucionesProveedorEnc (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevProv = @FolioDevProv ) 
			  Begin 
				 Insert Into DevolucionesProveedorEnc 
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioDevProv, FolioMovtoInv, IdPersonal, 
					   IdProveedor, ReferenciaDocto, FechaDocto, FechaVenceDocto, Observaciones, SubTotal, Iva, 
					   Total, -- FechaRegistro, 
					   Status, Actualizado
					 ) 
				 Select 
					   @IdEmpresa, @IdEstado, @IdFarmacia, @FolioDevProv, @FolioMovtoInv, @IdPersonal, 
					   @IdProveedor, @ReferenciaDocto, @FechaDocto, @FechaVenceDocto, @Observaciones,@SubTotal, @Iva, 
					   @Total, -- @FechaRegistro, 
					   @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update DevolucionesProveedorEnc Set 
					IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia,					
					FolioDevProv = @FolioDevProv, FolioMovtoInv = @FolioMovtoInv, 
					IdPersonal = @IdPersonal, IdProveedor = @IdProveedor, ReferenciaDocto = @ReferenciaDocto, 
					FechaDocto = @FechaDocto, FechaVenceDocto = @FechaVenceDocto, Observaciones = @Observaciones, 
					SubTotal = @SubTotal, Iva = @Iva, Total = @Total, -- FechaRegistro = @FechaRegistro, 
					Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevProv = @FolioDevProv

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioDevProv 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update DevolucionesProveedorEnc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevProv = @FolioDevProv
 
		   Set @sMensaje = 'La información del Folio ' + @FolioDevProv + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioDevProv as Folio, @sMensaje as Mensaje 
End
Go--#SQL 	
