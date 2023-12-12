If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CEDIS_Existencia_Enc' and xType = 'P' ) 
   Drop Proc spp_Mtto_CEDIS_Existencia_Enc 
Go--#SQL   

Create Proc spp_Mtto_CEDIS_Existencia_Enc 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmaciaCEDIS varchar(4) = '0008', 
    @Folio varchar(8) = '*', @IdFarmacia varchar(4) = '0008', @IdPersonal varchar(4) = '0001'
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

	--	SE CANCELAN LOS DEMAS FOLIOS PARA GENERAR UNO NUEVO

	Update CEDIS_Existencia_Enc Set Status = 'C'
	Update CEDIS_Existencia_Det Set Status = 'C'

	If @Folio = '*' 
	   Begin 
	       Select @Folio = cast( (max(Folio) + 1) as varchar) From CEDIS_Existencia_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmaciaCEDIS = @IdFarmaciaCEDIS 
	   End 
	   
	Set @Folio = IsNull(@Folio, '1') 
	Set @Folio = right(replicate('0', 8) + @Folio, 8)     
    
    
    If Not Exists 
        ( Select Folio From CEDIS_Existencia_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmaciaCEDIS = @IdFarmaciaCEDIS and Folio = @Folio
		)
	Begin 	
        Insert Into CEDIS_Existencia_Enc ( IdEmpresa, IdEstado, IdFarmaciaCEDIS, Folio, IdFarmacia, IdPersonal, FechaRegistro, Status, Actualizado ) 
        Select @IdEmpresa, @IdEstado, @IdFarmaciaCEDIS, @Folio, @IdFarmacia, @IdPersonal, getdate() as FechaRegistro, 
		@sStatus as Status, @iActualizado as Actualizado 
    End 
    
    Set @sMensaje = 'Información guardada satisfactoriamente con el Folio  ' + @Folio 
    Select @Folio as Folio, @sMensaje as Mensaje 
    
End 
Go--#SQL   


