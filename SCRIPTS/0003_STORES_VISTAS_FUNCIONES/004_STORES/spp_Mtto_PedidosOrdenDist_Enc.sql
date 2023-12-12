
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_PedidosOrdenDist_Enc' and xType = 'P' ) 
   Drop Proc spp_Mtto_PedidosOrdenDist_Enc 
Go--#SQL   

Create Proc spp_Mtto_PedidosOrdenDist_Enc 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008', 
    @FolioPedido varchar(6) = '*', @IdPersonal varchar(4) = '0001', 
    @Observaciones varchar(200) = 'S.O', @Status varchar(2) = 'A'
)  
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
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     

	If @FolioPedido = '*' 
	   Begin 
	       Select @FolioPedido = cast( (max(FolioPedido) + 1) as varchar) From PedidosOrdenDist_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	   End 
	   
	Set @FolioPedido = IsNull(@FolioPedido, '1') 
	Set @FolioPedido = right(replicate('0', 6) + @FolioPedido, 6)     
    
    
    If Not Exists ( Select FolioPedido From PedidosOrdenDist_Enc (NoLock) 
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioPedido = @FolioPedido )
	Begin 	
        Insert Into PedidosOrdenDist_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, FechaRegistro, IdPersonal, Observaciones, Status, Actualizado ) 
        Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, getdate() as FechaRegistro, @IdPersonal, @Observaciones, @sStatus as Status, @iActualizado as Actualizado 
    End 
    
    Set @sMensaje = 'Información guardada satisfactoriamente con el Folio  ' + @FolioPedido 
    Select @FolioPedido as Folio, @sMensaje as Mensaje 
    
End 
Go--#SQL   

