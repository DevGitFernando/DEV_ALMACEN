---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_ActualizarCostoUnitarioTransferencias' and xType = 'P' )
   Drop Proc spp_Mtto_ActualizarCostoUnitarioTransferencias 
Go--#SQL 

Create Proc spp_Mtto_ActualizarCostoUnitarioTransferencias 
( 
    @IdEmpresa varchar(3) = '', 
	@IdEstadoEnvia varchar(2) = '', @IdFarmaciaEnvia varchar(4) = '', @FolioSalida varchar(18) = '',
	@IdEstadoRecibe varchar(2) = '', @IdFarmaciaRecibe varchar(4) = '', @FolioEntrada Varchar(18) = ''
) 
With Encryption 
As 
Begin 
Set NoCount On 

	Select 
		IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, 
		IdEstadoRecibe as IdEstado, IdFarmaciaRecibe as IdFarmacia, IdSubFarmaciaRecibe as IdSubFarmacia, 
		FolioTransferencia, @FolioEntrada as FolioTransferencia_Entrada,  
		IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, CantidadEnviada, Status, Actualizado, Cant_Enviada, Cant_Devuelta, 
		CostoUnitario 
	Into #tmp_Datos_TS 
	From TransferenciasEnvioDet_Lotes TL  
	Where TL.IdEmpresa = @IdEmpresa And TL.IdEstadoEnvia = @IdEstadoEnvia And TL.IdFarmaciaEnvia = @IdFarmaciaEnvia And TL.FolioTransferencia = @FolioSalida  


	Update TL 
		Set CostoUnitario = DT.CostoUnitario 
	From TransferenciasDet_Lotes TL (NoLock) 
	Inner Join #tmp_Datos_TS DT (NoLock) 
		On ( TL.IdEmpresa = DT.IdEmpresa and TL.IdEstado = DT.IdEstado and TL.IdFarmacia = DT.IdFarmacia and TL.FolioTransferencia = DT.FolioTransferencia_Entrada
			and TL.IdSubFarmaciaRecibe = DT.IdSubFarmacia and TL.IdProducto = DT.IdProducto and TL.CodigoEAN = DT.CodigoEAN and TL.ClaveLote = DT.ClaveLote ) 

	Update TL 
		Set Costo = DT.CostoUnitario 
	From MovtosInv_Det_CodigosEAN_Lotes TL (NoLock) 
	Inner Join #tmp_Datos_TS DT (NoLock) 
		On ( TL.IdEmpresa = DT.IdEmpresa and TL.IdEstado = DT.IdEstado and TL.IdFarmacia = DT.IdFarmacia and TL.FolioMovtoInv = DT.FolioTransferencia_Entrada
			and TL.IdSubFarmacia = DT.IdSubFarmacia and TL.IdProducto = DT.IdProducto and TL.CodigoEAN = DT.CodigoEAN and TL.ClaveLote = DT.ClaveLote ) 


	--Update TL 
	--	Set CostoUnitario = ( 
	--						 Select CostoUnitario 
	--						 From TransferenciasEnvioDet_Lotes TV 
	--						 Where IdEmpresa = TL.IdEmpresa And IdEstadoEnvia = @IdEstadoEnvia And IdFarmaciaEnvia = @IdFarmaciaEnvia 
	--								and TV.IdSubFarmaciaEnvia = TL.IdSubFarmaciaEnvia  
	--								And IdEstadoRecibe = TL.IdEstado And IdFarmaciaRecibe = Tl.IdFarmacia And FolioTransferencia = @FolioSalida 
	--								And IdProducto = Tl.IdProducto And CodigoEAN = TL.CodigoEAN And ClaveLote = TL.ClaveLote  
	--						 ) 
	--From TransferenciasDet_Lotes TL 
	--Where TL.IdEmpresa = @IdEmpresa And TL.IdEstado = @IdEstadoRecibe And TL.IdFarmacia = @IdFarmaciaRecibe And TL.FolioTransferencia = @FolioEntrada 

	--Update TL  
	--	Set Costo = ( 
	--				 Select TV.CostoUnitario 
	--				 From TransferenciasEnvioDet_Lotes TV 
	--				 Where IdEmpresa = TL.IdEmpresa And TV.IdEstadoEnvia = @IdEstadoEnvia And TV.IdFarmaciaEnvia = @IdFarmaciaEnvia 
	--						and TV.IdSubFarmaciaEnvia = TL.IdSubFarmacia 
	--						And TV.IdEstadoRecibe = TL.IdEstado And TV.IdFarmaciaRecibe = TL.IdFarmacia And TV.FolioTransferencia = @FolioSalida 
	--						And TV.IdProducto = TL.IdProducto And TV.CodigoEAN = TL.CodigoEAN And TV.ClaveLote = TL.ClaveLote 
	--				 ) 
	--From MovtosInv_Det_CodigosEAN_Lotes TL 
	--Where TL.IdEmpresa = @IdEmpresa And TL.IdEstado = @IdEstadoRecibe And TL.IdFarmacia = @IdFarmaciaRecibe And TL.FolioMovtoInv = @FolioEntrada 


End 
Go--#SQL

