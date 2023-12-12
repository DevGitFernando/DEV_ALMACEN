

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Generar_Entradas_Salidas_Inventario' and xType = 'P' ) 
   Drop Proc spp_Generar_Entradas_Salidas_Inventario 
Go--#SQL 

Create Proc spp_Generar_Entradas_Salidas_Inventario
(
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0113', @Folio varchar(8) = '00000001'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
	Declare @sMensaje varchar(1000), 
	@sStatus varchar(1), @iActualizado smallint, @FechaInicio varchar(10), @FechaFinal varchar(10),
	@FolioAnt varchar(8), @Cont smallint 

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @FechaInicio = ''
	Set @FechaFinal = '' 
	Set @FolioAnt = ''
	Set @Cont = 0 
	
	Select @Cont = Count(Folio) From INV_ConteoRapido_CodigoEAN_Enc (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
	
	Select @FechaInicio = convert(varchar(10), FechaInicio, 120), @FechaFinal = convert(varchar(10), FechaFinal, 120) 
	From INV_ConteoRapido_CodigoEAN_Enc (nolock)	
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
	
	--- Se genera la tabla de productos--ean    -------------
	Select CodigoEAN, Inv_Inicial, Entradas, Salidas
	Into #tmp_ProductosInventario
	From INV_ConteoRapido_CodigoEAN_Det (nolock)	
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
	
	
	Select top 0 CodigoEAN, Inv_Inicial
	Into #tmp_ProductosII
	From INV_ConteoRapido_CodigoEAN_Det (nolock)
	
	
	
	If @Cont = 1
		Begin 
			--- se genera la tabla del inventario Inicial de productos  ----------------------------------
			Insert Into #tmp_ProductosII
			Select D.CodigoEAN, Sum(D.Cantidad) as Inv_Inicial			
			From MovtosInv_Enc E (NoLock) 
			Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )
			Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia AND E.IdTipoMovto_Inv = 'II'
			And convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicio and @FechaFinal
			Group By D.CodigoEAN
			
		End
	Else
		Begin
			Set @FolioAnt = ( Select top 1 Folio From INV_ConteoRapido_CodigoEAN_Enc (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio < @Folio
							Order By Folio Desc )
			
			Insert Into #tmp_ProductosII
			Select CodigoEAN, Inv_Inicial 
			From INV_ConteoRapido_CodigoEAN_Det (NoLock) 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @FolioAnt
		End
		
		
		
	--- se genera la tabla de las Entradas de productos  ----------------------------------
	Select D.CodigoEAN, Sum(D.Cantidad) as Entradas
	Into #tmp_Productos_Entradas
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia AND E.TipoES = 'E' and E.MovtoAplicado = 'S'
	AND E.IdTipoMovto_Inv not In ('II', 'IIC') And convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicio and @FechaFinal
	Group By D.CodigoEAN
	
	
	--- se genera la tabla de las Salidas de productos  ----------------------------------
	Select D.CodigoEAN, Sum(D.Cantidad) as Salidas
	Into #tmp_Productos_Salidas
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia AND E.TipoES = 'S' and E.MovtoAplicado = 'S'
	AND E.IdTipoMovto_Inv not In ('II', 'IIC') And convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicio and @FechaFinal
	Group By D.CodigoEAN
	
	----============================================================================================================================================
	
	Update T Set T.Inv_Inicial = M.Inv_Inicial
	From #tmp_ProductosInventario T (Nolock)
	Inner Join #tmp_ProductosII M (Nolock) On (M.CodigoEAN = T.CodigoEAN)
	
	Update T Set T.Entradas = M.Entradas
	From #tmp_ProductosInventario T (Nolock)
	Inner Join #tmp_Productos_Entradas M (Nolock) On (M.CodigoEAN = T.CodigoEAN)
	
	Update T Set T.Salidas = M.Salidas
	From #tmp_ProductosInventario T (Nolock)
	Inner Join #tmp_Productos_Salidas M (Nolock) On (M.CodigoEAN = T.CodigoEAN)
	
	Update D Set D.Inv_Inicial = T.Inv_Inicial, D.Entradas = T.Entradas, D.Salidas = T.Salidas 
	From INV_ConteoRapido_CodigoEAN_Det D (nolock)
	Inner Join #tmp_ProductosInventario T (Nolock)	
		On ( D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.Folio = @Folio and D.CodigoEAN = T.CodigoEAN )
		
	
	
	
End 
Go--#SQL 
