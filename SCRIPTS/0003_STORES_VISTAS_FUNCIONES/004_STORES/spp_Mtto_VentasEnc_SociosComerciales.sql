-----------------------------------------------------------------------------------------------------------------------
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_VentasEnc_SociosComerciales' and xType = 'P')
    Drop Proc spp_Mtto_VentasEnc_SociosComerciales
Go--#SQL

Create Proc spp_Mtto_VentasEnc_SociosComerciales
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVenta varchar(8), 
	@FolioMovtoInv varchar(22), @IdSocioComercial  varchar(8), @IdSucursal  varchar(8), @IdPersonal varchar(6),	@Observaciones varchar(102), 
    @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), @iOpcion smallint
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


	If @FolioVenta = '*'
	  Begin
		Select @FolioVenta = cast( (max(FolioVenta) + 1) as varchar)
		From VentasEnc_SociosComerciales (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	  End

	-- Asegurar que FolioVenta sea valido y formatear la cadena
	Set @FolioVenta = IsNull(@FolioVenta, '1')
	Set @FolioVenta = right(replicate('0', 8) + @FolioVenta, 8)


	If @iOpcion = 1
       Begin

		   If Not Exists ( Select * From VentasEnc_SociosComerciales (NoLock)
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta )
			  Begin
				 Insert Into VentasEnc_SociosComerciales
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovtoInv, FechaRegistro,
					   IdSocioComercial, IdSucursal, IdPersonal, Observaciones, SubTotal, Iva, Total,
					   Status, Actualizado
					 )
				 Select
					   @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @FolioMovtoInv, GetDate(),
					   @IdSocioComercial, @IdSucursal, @IdPersonal, @Observaciones,@SubTotal, @Iva, @Total,
					   @sStatus, @iActualizado
              End
		   Else
			  Begin
				Update VentasEnc_SociosComerciales
				Set IdSocioComercial = @IdSocioComercial, IdSucursal = @IdSucursal,
					IdPersonal = @IdPersonal,Observaciones = @Observaciones,
					SubTotal = @SubTotal, Iva = @Iva, Total = @Total,
					Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta

              End
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioVenta
	   End
    Else
       Begin
           Set @sStatus = 'C'
	       Update VentasEnc_SociosComerciales
	       Set Status = @sStatus, Actualizado = @iActualizado
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
 
		   Set @sMensaje = 'La información del Folio ' + @FolioVenta + ' ha sido cancelada satisfactoriamente.'
	   End

	-- Regresar la Clave Generada
    Select @FolioVenta as Folio, @sMensaje as Mensaje
End
Go--#SQL