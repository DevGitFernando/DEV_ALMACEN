If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Adt_VentasEnc' and xType = 'P' )
    Drop Proc spp_Mtto_Adt_VentasEnc
Go--#SQL
  
Create Proc spp_Mtto_Adt_VentasEnc 
(	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVenta varchar(32), @IdCliente varchar(6), 
	@IdSubCliente varchar(4), @IdPrograma varchar(4), @IdSubPrograma varchar(4), @iOpcion smallint, @FolioMovto varchar(8),
	@IdPersonal varchar(4)
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@TipoDeVenta smallint	

	/*Opciones
	
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @TipoDeVenta = 2
	Set @iOpcion = 0

	If @FolioMovto = '*' 
	   Select @FolioMovto = cast( (max(FolioMovto) + 1) as varchar)  From Adt_VentasEnc (NoLock) 

	-- Asegurar que FolioMovto sea valido y formatear la cadena 
	Set @FolioMovto = IsNull(@FolioMovto, '1')
	Set @FolioMovto = right(replicate('0', 8) + @FolioMovto, 8)
	
	Begin		    
		If Exists ( Select * From VentasEnc (NoLock) 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta ) 
			Begin 
				
				Insert Into Adt_VentasEnc 
					( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovto, FechaRegistro, IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, TipoDeVenta, Status, Actualizado ) 				
				Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, @FolioMovto as FolioMovto, GetDate() as FechaRegistro, @IdPersonal as IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, TipoDeVenta, Status, 0 as Actualizado
				From VentasEnc (NoLock) 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
								
				Update VentasEnc Set IdCliente = @IdCliente, IdSubCliente = @IdSubCliente, 
					IdPrograma = @IdPrograma, IdSubPrograma = @IdSubPrograma, Status = @sStatus, Actualizado = @iActualizado 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta

----				Update VentasEnc Set Status = @sStatus, Actualizado = @iActualizado 
----				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta

				Set @sMensaje = 'La información del Folio ' + @FolioVenta + ' ha sido actualizada satisfactoriamente.' 
			End
	End

----	Begin
---- 
----		If Not Exists ( Select *  From  Adt_VentasEnc (Nolock)
----				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta And FolioMovto = @FolioMovto )
----			Begin
----			
----				Insert Into Adt_VentasEnc 
----				Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @FolioMovto, GetDate(), @IdPersonal, @IdCliente, @IdSubCliente, @IdPrograma, 
----				@IdSubPrograma, @TipoDeVenta, @sStatus, @iActualizado				
----
----			End
----	End

	-- Regresar la Clave Generada
    Select @FolioVenta as Clave, @sMensaje as Mensaje, @FolioMovto As FolioMovto 
End
Go--#SQL 
