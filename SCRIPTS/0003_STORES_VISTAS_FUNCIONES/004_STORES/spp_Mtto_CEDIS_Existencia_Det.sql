If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CEDIS_Existencia_Det' and xType = 'P' ) 
   Drop Proc spp_Mtto_CEDIS_Existencia_Det 
Go--#SQL   

Create Proc spp_Mtto_CEDIS_Existencia_Det 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmaciaCEDIS varchar(4) = '0008', 
    @Folio varchar(8) = '*', @IdClaveSSA varchar(4), @Existencia int = 0 
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
    
    If Not Exists 
        ( Select Folio From CEDIS_Existencia_Det (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmaciaCEDIS = @IdFarmaciaCEDIS 
		   and Folio = @Folio and IdClaveSSA = @IdClaveSSA 
		)
	Begin 	
        Insert Into CEDIS_Existencia_Det ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio, IdClaveSSA, Existencia, ExistenciaDisponible, Status, Actualizado ) 
        Select @IdEmpresa, @IdEstado, @IdFarmaciaCEDIS, @Folio, @IdClaveSSA, @Existencia, @Existencia, @sStatus as Status, @iActualizado as Actualizado 
    End 
    
End 
Go--#SQL   