

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_ALMN_Unidades_Atendidas' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_ALMN_Unidades_Atendidas
Go--#SQL   

Create Proc spp_Mtto_CFG_ALMN_Unidades_Atendidas 
( 
    @IdEstado varchar(2) = '11', @IdAlmacen varchar(4) = '0003', @IdFarmacia varchar(4) = '0011', @EsAsignado tinyint = 0 
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
	Set @sStatus = (case when @EsAsignado = 1 then 'A' else 'C' end)
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     
    
	If Not Exists ( Select * From CFG_ALMN_Unidades_Atendidas  (NoLock) 
					   Where IdEstado = @IdEstado and IdAlmacen = @IdAlmacen and IdFarmacia = @IdFarmacia ) 
		Begin 
			Insert Into CFG_ALMN_Unidades_Atendidas  ( IdEstado, IdAlmacen, IdFarmacia, EsAsignado, Status, Actualizado ) 
			Select @IdEstado, @IdAlmacen, @IdFarmacia, @EsAsignado, @sStatus, @iActualizado 
		End 
	Else 
		Begin 
			Update C Set EsAsignado = @EsAsignado, Status = @sStatus, Actualizado = @iActualizado 
			From CFG_ALMN_Unidades_Atendidas  C 	
			Where IdEstado = @IdEstado and IdAlmacen = @IdAlmacen and IdFarmacia = @IdFarmacia
		End 				 	
    
	-- Se devuelve el resultado.
	Select @sMensaje as Mensaje 
    
End 
Go--#SQL   