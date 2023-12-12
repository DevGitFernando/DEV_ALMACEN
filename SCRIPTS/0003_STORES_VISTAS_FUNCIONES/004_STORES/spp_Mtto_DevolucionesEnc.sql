If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_DevolucionesEnc' and xType = 'P' )
    Drop Proc spp_Mtto_DevolucionesEnc
Go--#SQL
	  
Create Proc spp_Mtto_DevolucionesEnc 
(	
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioDevolucion varchar(30), 
	@TipoDeDevolucion tinyint, @Referencia varchar(50), @FolioMovtoInv varchar(30), 
	@FechaSistema datetime, @IdPersonal varchar(6), @Observaciones varchar(200), 	
    @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4) -- , @iOpcion smallint 
 ) 
With Encryption  
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		-- @FolioMovtoInv varchar(22),
		@Descuento numeric(14,4)
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	-- Set @FolioMovtoInv = '0'


	If @FolioDevolucion = '*' 
	   Select @FolioDevolucion = cast( (max(FolioDevolucion) + 1) as varchar)  From DevolucionesEnc (NoLock) 
			  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	-- Asegurar que FolioVenta sea valido y formatear la cadena 
	Set @FolioDevolucion = IsNull(@FolioDevolucion, '1')
	Set @FolioDevolucion = right(replicate('0', 8) + @FolioDevolucion, 8)
	
	Select @SubTotal = Sum(Importe), @Iva = Sum((Costo * Cantidad) * (TasaIva/100)), @Total = Sum(Importe * (1 +TasaIva/100))
	From MovtosInv_Det_CodigosEAN (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioMovtoInv = @FolioMovtoInv


	If Not Exists ( Select * From DevolucionesEnc (NoLock) 
				   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioDevolucion = @FolioDevolucion ) 
	  Begin 
		 Insert Into DevolucionesEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, TipoDeDevolucion, Referencia, FolioMovtoInv, 
				FechaSistema, IdPersonal, Observaciones, SubTotal, Iva, Total, Status, Actualizado ) 
		 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioDevolucion, @TipoDeDevolucion, @Referencia, @FolioMovtoInv, 
				@FechaSistema, @IdPersonal, @Observaciones, @SubTotal, @Iva, @Total, @sStatus, @iActualizado 
	  End 
                           
                           
	Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioDevolucion 
		   

	-- Regresar la Clave Generada
    Select @FolioDevolucion as Folio, @sMensaje as Mensaje 
End
Go--#SQL	
