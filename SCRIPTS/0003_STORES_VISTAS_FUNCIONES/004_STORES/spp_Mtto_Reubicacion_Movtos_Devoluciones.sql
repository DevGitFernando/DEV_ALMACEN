

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Reubicacion_Movtos_Devoluciones' and xType = 'P' ) 
   Drop Proc spp_Mtto_Reubicacion_Movtos_Devoluciones
Go--#SQL	

Create Proc spp_Mtto_Reubicacion_Movtos_Devoluciones 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182', @FolioMovtoInv varchar(30) = 'EDT00000003',
	@IdPersonalRegistra varchar(4) = '0085', @IdTipoMovto_Inv varchar(6) = 'SRDT', @TipoES varchar(1) = 'S',  
	@NombrePosicion varchar(100) = 'TS_DEVOLUCION', @TipoMovto smallint = 0
) 
With Encryption 
As 
Begin 
Set NoCount On 

	/*
		@TipoMovto = 0 : Salidas
		@TipoMovto = 1 : Entradas
	*/

	Declare @IdRack int, @IdNivel int, @IdEntrepaño int,
			@FolioMovtoAux varchar(30), @sMensaje varchar(8000)	
	
	Set @IdRack = 0
	Set @IdNivel = 0
	Set @IdEntrepaño = 0
	Set @FolioMovtoAux = ''
	Set @sMensaje = ''
	
	
	
	-------- OBTENER EL FOLIO SEGUN EL TIPO DE MOVTO --------------------------------------------------------------------------------
	
	Select @FolioMovtoAux = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 From MovtosInv_Enc (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdTipoMovto_Inv
	
	Set @FolioMovtoAux = IsNull(@FolioMovtoAux, '1') 
	Set @FolioMovtoAux = @IdTipoMovto_Inv + right(replicate('0', 8) + @FolioMovtoAux, 8) 
	
	---------------------------------------------------------------------------------------------------------------------------------
	
------------------------- 
	-- Obtener la informacion de la Devolucion 
	Select IdEmpresa, IdEstado, IdFarmacia, @FolioMovtoAux as FolioMovtoInv, @IdTipoMovto_Inv as IdTipoMovto_Inv, @TipoES as TipoES, 
	Observaciones, SubTotal, Iva, Total, @IdPersonalRegistra as IdPersonalRegistra
	Into #tmpMovtosInv_Enc 
	From MovtosInv_Enc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoInv 	
	
	Select IdEmpresa, IdEstado, IdFarmacia, @FolioMovtoAux as FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, 
	TasaIva, Cantidad, Costo, Importe
	Into #tmpMovtosInv_Det 
	From MovtosInv_Det_CodigosEAN D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoInv 
	
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioMovtoAux as FolioMovtoInv, IdProducto, CodigoEAN, 
	cast(EsConsignacion as Int) as EsConsignacion, ClaveLote, Cantidad, Costo, Importe
	Into #tmpMovtosInv_Lotes 
	From MovtosInv_Det_CodigosEAN_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoInv 

----	----  Ubicaciones
	Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioMovtoAux as FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
	cast(EsConsignacion as Int) as EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Cantidad
	Into #tmpMovtosInv_Lotes_Ubicaciones
	From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoInv	
	--Group By IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion
------------------------- 
	
--	select top 1 * from MovtosInv_Enc (nolock) 	
--	select top 1 * from MovtosInv_Det_CodigosEAN (nolock) 	
--	select top 1 * from MovtosInv_Det_CodigosEAN_Lotes (nolock) 
--	select top 1 * from MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones (nolock) 


	--------------------------------  INSERTAR EL FOLIO DE MOVIMIENTO -----------------------------------------------------------------

	Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, MovtoAplicado, Observaciones, SubTotal, Iva, Total, IdPersonalRegistra )
	Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 'S', Observaciones, SubTotal, Iva, Total, IdPersonalRegistra
	From #tmpMovtosInv_Enc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoAux	
	
	Insert Into MovtosInv_Det_CodigosEAN (IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, 
					UnidadDeSalida, TasaIva, Cantidad, Costo, Importe)
	Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, 
	UnidadDeSalida, TasaIva, Cantidad, Costo, Importe
	From #tmpMovtosInv_Det D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoAux 
	
	
	Insert Into MovtosInv_Det_CodigosEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, 
	EsConsignacion, ClaveLote, Cantidad, Costo, Importe )
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, 
	EsConsignacion, ClaveLote, Cantidad, Costo, Importe
	From #tmpMovtosInv_Lotes D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoAux 

----	----  Ubicaciones

	---			select top 1 * from FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (nolock) 

	If @TipoMovto = 1
		Begin
			
			Select @IdRack = IdRack, @IdNivel = IdNivel, @IdEntrepaño = IdEntrepaño From CFG_ALMN_Ubicaciones_Estandar D
			Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.NombrePosicion = @NombrePosicion
			
			Update D Set D.IdPasillo = @IdRack, D.IdEstante = @IdNivel, IdEntrepaño = @IdEntrepaño
			From #tmpMovtosInv_Lotes_Ubicaciones D 
			
			Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
			( 
				IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño 
			)
			Select Distinct IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño 
			From #tmpMovtosInv_Lotes_Ubicaciones D 
			Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoAux
			and Not Exists 
			(
				Select * From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U Where U.IdEmpresa = D.IdEmpresa and U.IdEstado = D.IdEstado
				and U.IdFarmacia = D.IdFarmacia and U.IdSubFarmacia = D.IdSubFarmacia AND U.IdProducto = D.IdProducto AND U.CodigoEAN = D.CodigoEAN
				AND U.ClaveLote = D.ClaveLote AND U.IdPasillo = D.IdPasillo AND U.IdEstante = D.IdEstante AND U.IdEntrepaño = D.IdEntrepaño
			)
			
		   Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
	          EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Cantidad ) 
	       Select  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
	         EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Sum(Cantidad) As Cantidad
	       From #tmpMovtosInv_Lotes_Ubicaciones D (NoLock) 
	       Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoAux	
		   Group By IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote,
		     EsConsignacion, IdPasillo, IdEstante, IdEntrepaño
		End
	Else
	 Begin
	    Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
	        EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Cantidad ) 
	    Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
	         EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Cantidad
	    From #tmpMovtosInv_Lotes_Ubicaciones D (NoLock) 
	    Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioMovtoInv = @FolioMovtoAux
	 End

		
	Select @FolioMovtoAux as Folio 
	
End 
Go--#SQL	

	