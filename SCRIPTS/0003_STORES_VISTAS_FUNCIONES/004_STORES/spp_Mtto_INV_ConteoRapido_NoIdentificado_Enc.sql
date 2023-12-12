If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_INV_ConteoRapido_NoIdentificado_Enc' and xType = 'P' ) 
   Drop Proc spp_Mtto_INV_ConteoRapido_NoIdentificado_Enc 
Go--#SQL   

Create Proc spp_Mtto_INV_ConteoRapido_NoIdentificado_Enc
( 
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0113', 
    @Folio varchar(8) = '*', @IdPersonal varchar(4) = '0001', 
    @Observaciones varchar(200) = 'S.O', @MovtoAplicado varchar(2)= 'N', @Status varchar(2) = 'A', @Opcion int = 0
)  
With Encryption 
As 
Begin 
Set NoCount On 

	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	
	Set @sMensaje = ''
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     
	
	
	/*
		Opcion 0 -> Insertar
		Opcion 1 -> Actulizar
	*/
	If @Opcion = 0
	Begin
		If @Folio = '*' 
		   Begin 
			   Select @Folio = cast( (max(Folio) + 1) as varchar) From INV_ConteoRapido_NoIdentificado_Enc  (NoLock) 
			   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		   End 
		   
		Set @Folio = IsNull(@Folio, '1') 
		Set @Folio = right(replicate('0', 8) + @Folio, 8)     
		
		If Not Exists 
			( Select Folio From INV_ConteoRapido_NoIdentificado_Enc  (NoLock) 
			   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
			)
		Begin 	
			Insert Into INV_ConteoRapido_NoIdentificado_Enc  ( IdEmpresa, IdEstado, IdFarmacia, Folio, FechaRegistro, IdPersonal, Observaciones, 
			MovtoAplicado, Status, Actualizado ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, getdate() as FechaRegistro, @IdPersonal, @Observaciones, 
			@MovtoAplicado, @Status, @iActualizado as Actualizado 
		End 

		Set @sMensaje = 'Información guardada satisfactoriamente con el Folio ' + @Folio 
		-- Select @Folio as Folio, @sMensaje as Mensaje 
	End 
	
	If @Opcion = 1 
	Begin
		Update INV_ConteoRapido_NoIdentificado_Enc
			   Set IdPersonal = @IdPersonal, FechaRegistro = getdate(), MovtoAplicado = @MovtoAplicado, Observaciones = @Observaciones
		Where IdEmpresa =@IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio

		Set @sMensaje = 'Folio ' + @Folio + ' actualizado satisfactoriamente' 
		--Select @Folio as Folio, @sMensaje as Mensaje 
	End
    
	Select @Folio as Folio, @sMensaje as Mensaje 
	
End 
Go--#SQL   

