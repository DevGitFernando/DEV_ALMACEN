-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_CPXP_PagosEnc' and xType = 'P' )
    Drop Proc spp_Mtto_CPXP_PagosEnc
Go--#SQL 
  
Create Proc spp_Mtto_CPXP_PagosEnc
(
	@IdEmpresa varchar(3) = '', @IdProveedor varchar(4), @Folio varchar(8), @Observaciones varchar(400), @Importe Numeric(38, 4), @iOpcion Int
) 
With Encryption 
As
Begin 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(2), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion 
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	If @Folio = '*' 
	   Select @Folio = cast( (max(Folio) + 1) as varchar)  From CPXP_PagosEnc (NoLock)
	   Where IdEmpresa = @IdEmpresa And IdProveedor = @IdProveedor

	-- Asegurar que FolioOrden sea valido y formatear la cadena 
	Set @Folio = IsNull(@Folio, '1')
	Set @Folio = right(replicate('0', 8) + @Folio, 8)

	If @iOpcion = 1 
		Begin 
			If Not Exists ( Select *
							From CPXP_PagosEnc  (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEmpresa = @IdEmpresa And IdProveedor = @IdProveedor And Folio = @Folio )
				Begin 
					Insert Into CPXP_PagosEnc  
					   (IdEmpresa,  IdProveedor,  Folio,  Observaciones,  Importe, FechaRegistro, Status, Actualizado ) 
					Select 
						@IdEmpresa, @IdProveedor, @Folio, @Observaciones, @Importe, GETDATE(),  @sStatus, @iActualizado
				End 
			Else 
				Begin 
					Update CPXP_PagosEnc 
					Set Observaciones = @Observaciones, Importe = @Importe, FechaRegistro = GETDATE() 
					Where IdEmpresa = @IdEmpresa And IdEmpresa = @IdEmpresa And IdProveedor = @IdProveedor And Folio = @Folio
				End 

				Set @sMensaje = 'La información se guardo satisfactoriamente con el folio ' + @Folio 
		   
	   End 
	Else
		Begin 
			Set @sStatus = 'C' 
			Update CPXP_PagosEnc  Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEmpresa = @IdEmpresa And IdProveedor = @IdProveedor And Folio = @Folio
			
			Set @sMensaje = 'La información del folio ' + @Folio + ' ha sido cancelado satisfactoriamente.'			
			
		End			 

	---- Regresar la Clave Generada
    Select @Folio as Folio, @sMensaje as Mensaje

End
Go--#SQL

