If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_VentasEnc' and xType = 'P' )
    Drop Proc spp_Mtto_VentasEnc
Go--#SQL
  
Create Proc spp_Mtto_VentasEnc 
(	
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(4) = '', @IdFarmacia varchar(6) = '', @FolioVenta varchar(32) = '', @FechaSistema varchar(10) = '', 
	@IdCaja varchar(2) = '', 
	@IdPersonal varchar(6) = '', @IdCliente varchar(6) = '', @IdSubCliente varchar(4) = '', @IdPrograma varchar(4) = '', 
	@IdSubPrograma varchar(4) = '', -- @IdPaciente varchar(22), @FolioDerHab varchar(32), @FolioReceta varchar(22), 
    @SubTotal numeric(14, 4) = 0, @Iva numeric(14, 4) = 0, @Total numeric(14, 4) = 0, 
    @TipoDeVenta smallint = 0, @iOpcion smallint = 0, @Descuento numeric(14, 4) = 0   
) 
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@FolioMovtoInv varchar(22) 
		-- @Descuento numeric(14,4)
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	Set @FolioMovtoInv = '0'
	-- Set @Descuento = 0
	Set @IdCaja = '01'
	-- Set @IdPaciente = '01'


	If @FolioVenta = '*' 
	   Select @FolioVenta = cast( (max(FolioVenta) + 1) as varchar)  From VentasEnc (NoLock) 
			  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	-- Asegurar que FolioVenta sea valido y formatear la cadena 
	Set @FolioVenta = IsNull(@FolioVenta, '1')
	Set @FolioVenta = right(replicate('0', 8) + @FolioVenta, 8) 


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From VentasEnc (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta ) 
			  Begin 
				 Insert Into VentasEnc 
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovtoInv, FechaSistema, 
					   IdCaja, IdPersonal, IdCliente, IdSubCliente, 
					   IdPrograma, IdSubPrograma, -- IdPaciente, FolioDerechoHabiencia, FolioReceta, 
					   SubTotal, Descuento, Iva, Total, TipoDeVenta, Status, Actualizado
					 ) 
				 Select @IdEmpresa, 
					   @IdEstado, @IdFarmacia, @FolioVenta, @FolioMovtoInv, @FechaSistema, 
					   @IdCaja, @IdPersonal, @IdCliente, @IdSubCliente, 
					   @IdPrograma, @IdSubPrograma, -- @IdPaciente, @FolioDerHab, @FolioReceta, 
					   @SubTotal, @Descuento, @Iva, @Total, @TipoDeVenta, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 

				Update VentasEnc Set
					-- IdEmpresa = @IdEmpresa ,  
					-- IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioVenta = @FolioVenta, FolioMovtoInv = @FolioMovtoInv, 
					-- IdCaja = @IdCaja, IdPersonal = @IdPersonal, IdPrograma = @IdPrograma, IdSubPrograma = @IdSubPrograma, 
					-- IdPaciente = @IdPaciente, FolioDerechoHabiencia = @FolioDerHab, FolioReceta = @FolioReceta, 
					SubTotal = @SubTotal, Descuento = @Descuento, Iva = @Iva, Total = @Total, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioVenta 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update VentasEnc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
 
		   Set @sMensaje = 'La información del Folio ' + @FolioVenta + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioVenta as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
