----------------------------------------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_INV_ConteoRapido_CodigoEAN_Enc' and xType = 'P' ) 
   Drop Proc spp_Mtto_INV_ConteoRapido_CodigoEAN_Enc 
Go--#SQL 

Create Proc spp_Mtto_INV_ConteoRapido_CodigoEAN_Enc
(
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0154', 
	@Folio varchar(8) = '00000001',  @IdPersonal varchar(4) = '0004', @Observaciones varchar(200) = ''  
) 
With Encryption 
As 
Begin 
Set NoCount On 
	Declare @sMensaje varchar(1000), 
	@sStatus varchar(1), @iActualizado smallint, @FechaInicio datetime, @FechaFinal datetime, @Conteos int  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0  
	Set @FechaInicio = getdate()
	Set @FechaFinal = getdate() 
	Set @Conteos = 1
	
	If Not Exists ( Select * From INV_ConteoRapido_CodigoEAN_Enc (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia )
		Begin 
			Set @FechaInicio = ( Select Min(FechaRegistro) as Fecha From MovtosInv_Enc (NoLock) 
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ) -- and IdTipoMovto_Inv = 'II' )
		End  
	Else
		Begin
			Set @FechaInicio = ( Select top 1 (FechaFinal + 1) as Fecha From INV_ConteoRapido_CodigoEAN_Enc (NoLock) 
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
								Order By Folio Desc )
		End
	

	If @Folio = '*' 
	   Begin 
	       Select @Folio = cast( (max(Folio) + 1) as varchar) From INV_ConteoRapido_CodigoEAN_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	   End 
	   
	Set @Folio = IsNull(@Folio, '1') 
	Set @Folio = right(replicate('0', 8) + @Folio, 8) 
	
	If Not Exists ( Select * From INV_ConteoRapido_CodigoEAN_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio )
	Begin 	
        Insert Into INV_ConteoRapido_CodigoEAN_Enc
		( IdEmpresa, IdEstado, IdFarmacia, Folio, IdPersonal, FechaRegistro, FechaInicio, FechaFinal, Observaciones, Conteos, Status, Actualizado ) 
        Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @IdPersonal, getdate(), @FechaInicio, @FechaFinal, @Observaciones, @Conteos, @sStatus, @iActualizado 
    End
    Else
    Begin 
		Update INV_ConteoRapido_CodigoEAN_Enc Set Conteos = (Conteos + 1), Status = @sStatus
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
		
		Set @Conteos = (Select Conteos From INV_ConteoRapido_CodigoEAN_Enc (NoLock) 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio)
    End 

	If @Conteos = 3
	Begin
		Set @sStatus = 'T'
		Update INV_ConteoRapido_CodigoEAN_Enc Set Status = @sStatus
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
	End  

	Set @sMensaje = 'La Información se guardo Satisfactoriamente con el Folio : ' + @Folio
-------------------- Salida final 
	Select @Folio as Folio, @sMensaje as Mensaje, @Conteos as Conteos 
		
End 
Go--#SQL 

