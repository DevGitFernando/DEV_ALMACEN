
	If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_RutasDistribucionDet' and xType = 'P')
    Drop Proc spp_Mtto_RutasDistribucionDet
Go--#SQL
  
Create Proc spp_Mtto_RutasDistribucionDet ( @IdEmpresa Varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @Folio varchar(8),
		@FolioTransferenciaVenta varchar(30), @bultos int, @Tipo Varchar(1), @IdPersonal Varchar(4), @iOpcion smallint )
With Encryption 
As
Begin
Set DateFormat YMD
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1),
		@TipoVT Varchar(2),
		@iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/
	Set @TipoVT = 'SV'
	
	If (@Tipo = 'T')
		Begin
			Set @TipoVT = 'TS'
		End
	

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select *
						   From RutasDistribucionDet (NoLock)
						   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio And FolioTransferenciaVenta = @FolioTransferenciaVenta And Tipo = @Tipo) 
			  Begin 
				 Insert Into RutasDistribucionDet ( IdEmpresa, IdEstado, IdFarmacia, Folio, FolioTransferenciaVenta, Bultos, Tipo, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @FolioTransferenciaVenta, @bultos, @Tipo, @sStatus, @iActualizado
				 

				 Update P
				 Set IdPersonalTransporte = @IdPersonal, Status = 'P'
				 From Pedidos_Cedis_Enc_Surtido P
				 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
						FolioTransferenciaReferencia = @TipoVT + @FolioTransferenciaVenta
				 
              End  
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @Folio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update RutasDistribucionDet Set Status = @sStatus, Actualizado = @iActualizado
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio And
				FolioTransferenciaVenta = @FolioTransferenciaVenta And Tipo = @Tipo
		   Set @sMensaje = 'La información de la ruta ' + @Folio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @Folio As Folio, @sMensaje as Mensaje 
End
Go--#SQL