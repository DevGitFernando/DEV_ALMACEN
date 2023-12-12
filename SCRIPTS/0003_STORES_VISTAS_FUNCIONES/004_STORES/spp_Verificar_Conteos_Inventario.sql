

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Verificar_Conteos_Inventario' and xType = 'P' ) 
   Drop Proc spp_Verificar_Conteos_Inventario 
Go--#SQL

Create Proc spp_Verificar_Conteos_Inventario
(
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0154', @Folio varchar(8) = '00000001'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
	Declare @sMensaje varchar(1000), 
	@sStatus varchar(1), @iActualizado smallint, @Resultado smallint

	Set @sMensaje = ''
	Set @sStatus = ''
	Set @iActualizado = 0
	Set @Resultado = 0  
	
	Set @sStatus = ( Select Status From INV_ConteoRapido_CodigoEAN_Enc (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
					and IdFarmacia = @IdFarmacia and Folio = @Folio )
					
	If @sStatus = 'T'
		Begin 
			Exec spp_Generar_Entradas_Salidas_Inventario @IdEmpresa, @IdEstado, @IdFarmacia, @Folio
			Set @Resultado = 1			
		End
	Else
		Begin
			If Not Exists ( Select * From INV_ConteoRapido_CodigoEAN_Det  (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
						and IdFarmacia = @IdFarmacia and Folio = @Folio and ExistenciaLogica <> ExistenciaFinal )
			Begin 
				Set @sStatus = 'T'
				
				Update INV_ConteoRapido_CodigoEAN_Enc Set Status = @sStatus
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
				
				Exec spp_Generar_Entradas_Salidas_Inventario @IdEmpresa, @IdEstado, @IdFarmacia, @Folio
				
				Set @Resultado = 1
			End
		End	
	 
	Select @Resultado as Resultado
		
End 
Go--#SQL 

