----------------------------------------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_INV_ConteoRapido_CodigoEAN_Det' and xType = 'P' ) 
   Drop Proc spp_Mtto_INV_ConteoRapido_CodigoEAN_Det 
Go--#SQL 

Create Proc spp_Mtto_INV_ConteoRapido_CodigoEAN_Det
(
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0154', 
	@Folio varchar(8) = '00000001',  @CodigoEAN varchar(30) = '7501446040023', @ExistenciaLogica int = 0,
	@Cantidad int = 0, @Tipo tinyint = 0, @Cant_Bodega int = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 
	Declare @sMensaje varchar(1000), @sStatus varchar(1), @iActualizado smallint, @EsConteo tinyint 

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @EsConteo = 1
	
	--Set @CodigoEAN_Externo = IsNull(( Select CodigoEAN_ND From INT_ND_Productos (Nolock) Where CodigoEAN = @CodigoEAN ), '')
		
	If @Tipo = 1
	Begin
		If Not Exists ( Select * From INV_ConteoRapido_CodigoEAN_Det (NoLock) 
			   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio And CodigoEAN = @CodigoEAN )
		Begin 	
			Insert Into INV_ConteoRapido_CodigoEAN_Det( IdEmpresa, IdEstado, IdFarmacia, Folio, CodigoEAN,
													   ExistenciaLogica, Conteo1, EsConteo1, ExistenciaFinal, Conteo1_Bodega ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @CodigoEAN, @ExistenciaLogica, @Cantidad, @EsConteo, (@Cantidad + @Cant_Bodega), @Cant_Bodega
		End 
	End
	
	If @Tipo = 2
	Begin
		If Not Exists ( Select * From INV_ConteoRapido_CodigoEAN_Det (NoLock) 
			   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio And CodigoEAN = @CodigoEAN )
		Begin 	
			Insert Into INV_ConteoRapido_CodigoEAN_Det	( IdEmpresa, IdEstado, IdFarmacia, Folio, CodigoEAN, 
												ExistenciaLogica, Conteo2, EsConteo1, EsConteo2, ExistenciaFinal, Conteo2_Bodega ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @CodigoEAN, @ExistenciaLogica, @Cantidad, @EsConteo, @EsConteo, 
			(@Cantidad + @Cant_Bodega), @Cant_Bodega
		End 
		Else
		Begin
			Update INV_ConteoRapido_CodigoEAN_Det Set Conteo2 = @Cantidad, EsConteo2 = 1, ExistenciaFinal = (@Cantidad + @Cant_Bodega), 
			Conteo2_Bodega = @Cant_Bodega 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio And CodigoEAN = @CodigoEAN
		End
	End
	
	If @Tipo = 3
	Begin
		If Not Exists ( Select * From INV_ConteoRapido_CodigoEAN_Det (NoLock) 
			   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio And CodigoEAN = @CodigoEAN )
		Begin 	
			Insert Into INV_ConteoRapido_CodigoEAN_Det	( IdEmpresa, IdEstado, IdFarmacia, Folio, CodigoEAN, 
												ExistenciaLogica, Conteo3, EsConteo1, EsConteo2, EsConteo3, ExistenciaFinal, Conteo3_Bodega )  
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @CodigoEAN, @ExistenciaLogica, @Cantidad, 
			@EsConteo, @EsConteo, @EsConteo, (@Cantidad + @Cant_Bodega), @Cant_Bodega
		End 
		Else
		Begin
			Update INV_ConteoRapido_CodigoEAN_Det Set Conteo3 = @Cantidad, EsConteo3 = 1, ExistenciaFinal = (@Cantidad + @Cant_Bodega), 
			Conteo3_Bodega = @Cant_Bodega
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio And CodigoEAN = @CodigoEAN
		End
	End

-------------------- Salida final 
	Select @Folio as Folio, @sMensaje as Mensaje 
		
End 
Go--#SQL 

