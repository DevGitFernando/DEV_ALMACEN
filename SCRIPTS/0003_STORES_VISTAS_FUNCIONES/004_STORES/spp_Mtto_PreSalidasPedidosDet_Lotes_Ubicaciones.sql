--- Select * From PreSalidasPedidosDet (Nolock)
--- Select * From PreSalidasPedidosDet_Lotes_Ubicaciones (Nolock)
--- Delete From PreSalidasPedidosDet_Lotes_Ubicaciones
--- Exec spp_Mtto_PreSalidasPedidosDet_Lotes_Ubicaciones '002', '20', '0029', '01', '00000001'

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'P' )
   Drop Proc spp_Mtto_PreSalidasPedidosDet_Lotes_Ubicaciones
Go--#SQL

Create Proc spp_Mtto_PreSalidasPedidosDet_Lotes_Ubicaciones 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0029', @IdSubFarmacia varchar(2) = '01', 
	@FolioPreSalida varchar(8) = '00000001' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint,
		@IdClaveSSA varchar(4), @Tipo int, @Keyx int, @CantidadRequerida int,
		@CantidadPosicion int, @CantidadSatisfecha int, 
		@Residuo int,  
		@CantidadAsignada int

/* Status de las PreSalidas
	A ACTIVO
	C CANCELADO
	P PROCESADO
	T TERMINADO 		
*/
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0		
	Set @Tipo = 0
	Set @CantidadRequerida = 0
	Set @CantidadPosicion = 0
	Set @CantidadSatisfecha = 0
	Set @Residuo = 0
	Set @CantidadAsignada = 0 

		Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmacia As IdFarmacia, @FolioPreSalida As FolioPreSalida, 
			P.IdClaveSSA, P.CantidadRequerida, P.CantidadAsignada, 
			-- Sum(U.CantidadPosicion) As TotalDisponible, 
			sum(U.Existencia) as TotalDisponible, 
			0 As Tipo
		Into #tmpClavesPreSalidas		
		From PreSalidasPedidosDet P (Nolock)
		-- Inner Join vw_UbicacionProductosLotes_Existencia U (Nolock)
		Inner Join vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (Nolock)		
			On ( P.IdEmpresa = U.IdEmpresa And P.IdEstado = U.IdEstado And P.IdFarmacia = U.IdFarmacia And P.IdClaveSSA = U.IdClaveSSA_Sal )
		Where  P.IdEmpresa = @IdEmpresa And P.IdEstado = @IdEstado And P.IdFarmacia = @IdFarmacia And U.IdSubFarmacia = @IdSubFarmacia
		And P.FolioPreSalida = @FolioPreSalida
		Group By P.IdClaveSSA, P.CantidadRequerida, P.CantidadAsignada 
		Order By P.IdClaveSSA

		Update #tmpClavesPreSalidas Set Tipo = 1, CantidadAsignada = CantidadRequerida Where CantidadRequerida = TotalDisponible
		Update #tmpClavesPreSalidas Set Tipo = 2, CantidadAsignada = CantidadRequerida Where CantidadRequerida < TotalDisponible
		Update #tmpClavesPreSalidas Set Tipo = 3, CantidadAsignada = TotalDisponible Where CantidadRequerida > TotalDisponible

--		Select * From #tmpClavesPreSalidas (Nolock)
		--- Se Crea tabla para el detalle 

		Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmacia As IdFarmacia, @FolioPreSalida As FolioPreSalida,
			P.IdClaveSSA, U.IdPasillo, U.IdEstante, U.IdEntrepaño, 
			@IdSubFarmacia As IdSubFarmacia, U.IdProducto, U.CodigoEAN, U.ClaveLote, 
			-- U.CantidadPosicion, 
			U.Existencia as CantidadPosicion, 
			0 As Procesado, 0 As CantidadUtilizada, Identity(int, 1, 1) As Keyx
		Into #tmpClavesPreSalidasDetalle
		From PreSalidasPedidosDet P (Nolock)
		-- Inner Join vw_UbicacionProductosLotes_Existencia U (Nolock) 
		Inner Join vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 		
			On ( P.IdEmpresa = U.IdEmpresa And P.IdEstado = U.IdEstado And P.IdFarmacia = U.IdFarmacia And P.IdClaveSSA = U.IdClaveSSA_Sal )
		Where  P.IdEmpresa = @IdEmpresa And P.IdEstado = @IdEstado And P.IdFarmacia = @IdFarmacia And U.IdSubFarmacia = @IdSubFarmacia
		And P.FolioPreSalida = @FolioPreSalida
		Order By P.IdClaveSSA, U.MesesParaCaducar

--		Select * From #tmpClavesPreSalidasDetalle (Nolock) Order By Keyx
		
--		Se declara el cursor de encabezado de claves
		Declare ClavesPreSalidas cursor for
		Select IdClaveSSA, CantidadRequerida, Tipo 
		From #tmpClavesPreSalidas (Nolock) 
		Open ClavesPreSalidas
		Fetch next from ClavesPreSalidas Into @IdClaveSSA, @CantidadRequerida, @Tipo				
		While @@fetch_status = 0
			Begin 					
				Set @CantidadSatisfecha = 0 
				Set @CantidadAsignada = 0 
				Set @Residuo = 0				

				Declare ClavesPreSalidasDetalles cursor for
				Select CantidadPosicion, Keyx 
				From #tmpClavesPreSalidasDetalle (Nolock) 
				Order By Keyx 
				Open ClavesPreSalidasDetalles 
				Fetch next from ClavesPreSalidasDetalles Into @CantidadPosicion, @Keyx									
				While @@fetch_status = 0 and @CantidadSatisfecha = 0 
					Begin						
						
						Set @CantidadAsignada = @CantidadAsignada + @CantidadPosicion
						Set @Residuo = @CantidadRequerida - @CantidadAsignada
						 
						if @CantidadAsignada >= @CantidadRequerida
							Begin 
								Set @CantidadSatisfecha = 1

								If @Residuo <> 0
									Begin 
										Update #tmpClavesPreSalidasDetalle Set Procesado = 1, CantidadUtilizada = @CantidadPosicion - ABS(@Residuo)
										Where Keyx = @Keyx
									End
								Else
									Begin
										Update #tmpClavesPreSalidasDetalle Set Procesado = 1, CantidadUtilizada = @CantidadPosicion
										Where Keyx = @Keyx
									End
							End
						Else
							Begin
								Update #tmpClavesPreSalidasDetalle Set Procesado = 1, CantidadUtilizada = @CantidadPosicion
								Where Keyx = @Keyx
							End
						-- Avanzamos otro registro
						Fetch next from ClavesPreSalidasDetalles
						Into @CantidadPosicion, @Keyx 
					End
					-- cerramos el cursor
				Close ClavesPreSalidasDetalles
				Deallocate ClavesPreSalidasDetalles
				
				-- Avanzamos otro registro
				Fetch next from ClavesPreSalidas
				Into @IdClaveSSA, @CantidadRequerida, @Tipo
			End
			-- cerramos el cursor
		Close ClavesPreSalidas
		Deallocate ClavesPreSalidas


------------------------------------------ 
		Update PreSalidasPedidosEnc Set Status = 'P'
		Where  IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And IdSubFarmacia = @IdSubFarmacia And FolioPreSalida = @FolioPreSalida
	
		Update P Set P.CantidadAsignada = T.CantidadAsignada, P.Status = 'P'
		From PreSalidasPedidosDet P (Nolock)
		Inner Join #tmpClavesPreSalidas T (Nolock)
			On ( P.IdEmpresa = T.IdEmpresa And P.IdEstado = T.IdEstado And P.IdFarmacia = T.IdFarmacia 
				And P.FolioPreSalida = T.FolioPreSalida And P.IdClaveSSA = T.IdClaveSSA )
		Where  P.IdEmpresa = @IdEmpresa And P.IdEstado = @IdEstado And P.IdFarmacia = @IdFarmacia And P.FolioPreSalida = @FolioPreSalida

		Insert Into PreSalidasPedidosDet_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida, IdClaveSSA, 
			IdPasillo, IdEstante, IdEntrepaño, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, Cantidad, Status, Actualizado  ) 
		Select IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida, IdClaveSSA, IdPasillo, IdEstante, IdEntrepaño, 
		IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, CantidadUtilizada, 'P', @iActualizado
		From #tmpClavesPreSalidasDetalle (Nolock) Where Procesado = 1

		--Select * From #tmpClavesPreSalidas (Nolock)	

End 
Go--#SQL
