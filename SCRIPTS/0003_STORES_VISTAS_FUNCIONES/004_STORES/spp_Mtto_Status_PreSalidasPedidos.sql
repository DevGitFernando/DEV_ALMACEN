If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Status_PreSalidasPedidos' and xType = 'P')
    Drop Proc spp_Mtto_Status_PreSalidasPedidos
Go--#SQL
  
Create Proc spp_Mtto_Status_PreSalidasPedidos 
( 
    @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), 
    @FolioPreSalida varchar(8), @iOpcion smallint 
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Activo.
	Opcion 2.- Cancelar.
	Opcion 3.- Procesado.
	Opcion 4.- Terminado.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	


	If @iOpcion = 1 
       Set @sStatus = 'A'	

	If @iOpcion = 2 
       Set @sStatus = 'C'	
       
	If @iOpcion = 3 
       Set @sStatus = 'P'	
       
	If @iOpcion = 4 
       Set @sStatus = 'T'	              	

---- Actualizar el Status de todo el Registro 
     Update PreSalidasPedidosEnc Set Status = @sStatus, Actualizado = @iActualizado
     Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
     And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia And FolioPreSalida = @FolioPreSalida  

     Update PreSalidasPedidosDet Set Status = @sStatus, Actualizado = @iActualizado
     Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
     And IdFarmacia = @IdFarmacia And FolioPreSalida = @FolioPreSalida  

     Update PreSalidasPedidosDet_Lotes_Ubicaciones Set Status = @sStatus, Actualizado = @iActualizado
     Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
     And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia And FolioPreSalida = @FolioPreSalida  


----	If Exists ( Select * From PreSalidasPedidosEnc (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
----							And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia And FolioPreSalida = @FolioPreSalida )			   
----			  Begin 
----			     Update PreSalidasPedidosEnc Set Status = @sStatus, Actualizado = @iActualizado
----				 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
----				 And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia And FolioPreSalida = @FolioPreSalida  
----              End
----
----	If Exists ( Select * From PreSalidasPedidosDet (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
----							And IdFarmacia = @IdFarmacia And FolioPreSalida = @FolioPreSalida )			   
----			  Begin 
----			     Update PreSalidasPedidosDet Set Status = @sStatus, Actualizado = @iActualizado
----				 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
----				 And IdFarmacia = @IdFarmacia And FolioPreSalida = @FolioPreSalida  
----              End
----
----	If Exists ( Select * From PreSalidasPedidosDet_Lotes_Ubicaciones (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
----							And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia And FolioPreSalida = @FolioPreSalida )			   
----			  Begin 
----			     Update PreSalidasPedidosDet_Lotes_Ubicaciones Set Status = @sStatus, Actualizado = @iActualizado
----				 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
----				 And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia And FolioPreSalida = @FolioPreSalida  
----              End 
	
	Set @sMensaje = 'La información se Actualizo satisfactoriamente '

	-- Regresar la Clave Generada
    Select @FolioPreSalida as Clave, @sMensaje as Mensaje 
End
Go--#SQL
