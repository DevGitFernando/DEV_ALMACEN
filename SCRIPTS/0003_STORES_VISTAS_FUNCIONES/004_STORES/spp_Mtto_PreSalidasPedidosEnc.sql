If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_PreSalidasPedidosEnc' and xType = 'P')
    Drop Proc spp_Mtto_PreSalidasPedidosEnc
Go--#SQL
  
Create Proc spp_Mtto_PreSalidasPedidosEnc ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), 
	@FolioPreSalida varchar(8), @IdPersonal varchar(4), @Observaciones varchar(500), @iOpcion smallint )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @FolioPreSalida = '*' 
	   Select @FolioPreSalida = cast( (max(FolioPreSalida) + 1) as varchar)  From PreSalidasPedidosEnc (NoLock)
	   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	-- Asegurar que FolioPreSalida sea valido y formatear la cadena 
	Set @FolioPreSalida = IsNull(@FolioPreSalida, '1')
	Set @FolioPreSalida = right(replicate('0', 8) + @FolioPreSalida, 8)


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From PreSalidasPedidosEnc (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
							And IdFarmacia = @IdFarmacia And FolioPreSalida = @FolioPreSalida ) 
			  Begin 
				 Insert Into PreSalidasPedidosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida, IdSubFarmacia, 
													FechaPreSalida, FechaRegistro, IdPersonal, Observaciones, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPreSalida, @IdSubFarmacia, GetDate(), GetDate(), @IdPersonal, 
						@Observaciones, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update PreSalidasPedidosEnc Set Status = @sStatus, Actualizado = @iActualizado
				 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
				 And IdFarmacia = @IdFarmacia And FolioPreSalida = @FolioPreSalida  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPreSalida 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update PreSalidasPedidosEnc Set Status = @sStatus, Actualizado = @iActualizado Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
				 And IdFarmacia = @IdFarmacia And FolioPreSalida = @FolioPreSalida 
		   Set @sMensaje = 'La información de la PreSalida ' + @FolioPreSalida + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPreSalida as Clave, @sMensaje as Mensaje 
End
Go--#SQL
