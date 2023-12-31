----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_VentasDigitalizacion' and xType = 'P' ) 
   Drop Proc spp_Mtto_VentasDigitalizacion 
Go--#SQL 

Create Proc spp_Mtto_VentasDigitalizacion 
( 
    @IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @FolioVenta varchar(30) = '', 
	@IdImagen smallint = 0, @TipoDeImagen smallint = 0,   -- 1 ==> Ticket, 2 ==> Receta 
    @ImagenComprimida varchar(max) = '', @ImagenOriginal varchar(max) = '', @Ancho int = 0, @Alto int = 0, 
	@iOpcion smallint = 1 
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
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

	If @IdImagen = 0 
	Begin 
		Select @IdImagen = max(IdImagen) + 1 
		From SII_Digitalizacion.dbo.VentasDigitalizacion (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
	End 
	Set @IdImagen = IsNull(@IdImagen, 1) 


	If Not Exists ( Select * From SII_Digitalizacion.dbo.VentasDigitalizacion (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta and IdImagen = @IdImagen ) 
	   Begin 
	       Insert Into SII_Digitalizacion.dbo.VentasDigitalizacion ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdImagen, TipoDeImagen, FechaDigitalizacion, 
				  ImagenComprimida, ImagenOriginal, Ancho, Alto, Status, Actualizado ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @IdImagen, @TipoDeImagen, getdate() as FechaDigitalizacion, 
				  @ImagenComprimida, @ImagenOriginal, @Ancho, @Alto, @sStatus, @iActualizado 
       End    
	       
End 
Go--#SQL  



