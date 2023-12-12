
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CEDIS_Pedidos_Cedis_Det_Pedido_Distribuidor' and xType = 'P' ) 
   Drop Proc spp_Mtto_CEDIS_Pedidos_Cedis_Det_Pedido_Distribuidor
Go--#SQL

Create Proc spp_Mtto_CEDIS_Pedidos_Cedis_Det_Pedido_Distribuidor ( @IdEmpresaDist varchar(3) = '001', @IdEstadoDist varchar(2) = '25', 
	@IdFarmaciaDist varchar(4) = '0001' ) 
With Encryption 
As 
Begin 
Set NoCount On 

	-- NOTA: La tabla tmpDistribucion es creada por el Sp: spp_CEDIS_Procesar_Claves_Surtir
	Declare
		@IdEmpresa varchar(3), 
		@IdEstado varchar(2), 
		@IdFarmacia varchar(4), 
		@FolioPedido varchar(8), 
		@IdClaveSSA_Sal varchar(4), 
		@IdProducto varchar(8),
		@CodigoEAN varchar(30),
		@CantidadAsignada numeric(14, 4),
		@Existencia numeric(14, 4),
		@CantidadSumada numeric(14, 4),
		@FolioDistribucion varchar(8),
		@IdClave_Anterior varchar(4),
		@Clave_Abastecida bit

	-- Se Inicializan las variables
	Set @IdEmpresa = ''
	Set @IdEstado = ''
	Set @IdFarmacia = ''
	Set @FolioPedido = ''
	Set @IdClaveSSA_Sal = ''
	Set @IdProducto = ''
	Set @CodigoEAN = ''
	Set @CantidadAsignada = 0
	Set @Existencia = 0
	Set @CantidadSumada = 0
	Set @FolioDistribucion = ''
	Set @IdClave_Anterior = ''
	Set @Clave_Abastecida = 0

	-----------------------------------------
	-- Se obtiene el Folio de Distribucion --
	-----------------------------------------
	Select @FolioDistribucion = Cast( (Max(FolioDistribucion) + 1) as varchar) 
	From Pedidos_Cedis_Det_Pedido_Distribuidor (NoLock) 
	Where IdEmpresaDist = @IdEmpresaDist And IdEstadoDist = @IdEstadoDist And IdFarmaciaDist = @IdFarmaciaDist

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioDistribucion = IsNull(@FolioDistribucion, '1')
	Set @FolioDistribucion = right(replicate('0', 8) + @FolioDistribucion, 8)

	--------------------------------------------
	-- Se obtienen las cantidades de cada sal --
	--------------------------------------------

	Declare tmpCol Cursor For 
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioPedido, D.IdClaveSSA_Sal, F.IdProducto, F.CodigoEAN, D.CantidadAsignada
		From tmpDistribucion D(NoLock) 
		Inner Join vw_Productos_CodigoEAN P(NoLock) On ( D.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) 
		Inner Join CEDIS_Existencia_Det_CodigoEAN F(NoLock) On ( D.IdEmpresa = F.IdEmpresa And D.IdEstado = F.IdEstado And P.CodigoEAN = F.CodigoEAN) 
		Where F.Existencia > 0 And F.Status = 'A' 
		Order by D.FolioPedido, D.IdClaveSSA_Sal, IdProducto	
    Open tmpCol
    FETCH NEXT FROM tmpCol Into @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @IdClaveSSA_Sal, @IdProducto, @CodigoEAN, @CantidadAsignada
        WHILE @@FETCH_STATUS = 0
        BEGIN

			If @IdClave_Anterior <> @IdClaveSSA_Sal
			  Begin
				-- Se inicializa la Cantidad Sumada.
				Set @CantidadSumada = 0	
				Set @Clave_Abastecida = 0	
				Set @IdClave_Anterior = @IdClaveSSA_Sal		
			  End

			If @Clave_Abastecida = 0
			  Begin
				-- Se obtiene la existencia del CodigoEAN debido a que se va actualizando.
				Select @Existencia = ExistenciaDisponible From CEDIS_Existencia_Det_CodigoEAN(NoLock) 
				Where IdEmpresa = @IdEmpresaDist And IdEstado = @IdEstadoDist And IdFarmaciaCEDIS = @IdFarmaciaDist 
					And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Status = 'A'

				If @Existencia > 0
				  Begin
					If( @CantidadSumada + @Existencia ) > @CantidadAsignada 
					  Begin
						Set @Existencia = @CantidadAsignada - @CantidadSumada
						Set @CantidadSumada = @CantidadSumada + @Existencia
						Set @Clave_Abastecida = 1
					  End
					Else
					  Begin
						 Set @CantidadSumada = @CantidadSumada + @Existencia
					  End

					-- Se inserta en Pedidos_Cedis_Det_Pedido_Distribuidor.
					Insert Into Pedidos_Cedis_Det_Pedido_Distribuidor
					Select	@IdEmpresaDist, @IdEstadoDist, @IdFarmaciaDist, @FolioDistribucion, 
							@IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @IdProducto, @CodigoEAN, @CodigoEAN, @Existencia, 0, 'A', 0

					-- Se actualiza la Existencia en CEDIS_Existencia_Det_CodigoEAN
					Update CEDIS_Existencia_Det_CodigoEAN 
						Set Existencia = ( Existencia - @Existencia ), ExistenciaDisponible = ( ExistenciaDisponible - @Existencia ), Actualizado = 0
					Where IdEmpresa = @IdEmpresaDist And IdEstado = @IdEstadoDist And IdFarmaciaCEDIS = @IdFarmaciaDist 
						And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Status = 'A'

					-- Se actualiza la existencia de la Clave en CEDIS_Existencia_Det.
					Update CEDIS_Existencia_Det
						Set Existencia = ( Existencia - @Existencia ), ExistenciaDisponible = ( ExistenciaDisponible - @Existencia ), Actualizado = 0
					Where IdEmpresa = @IdEmpresaDist And IdEstado = @IdEstadoDist And IdFarmaciaCEDIS = @IdFarmaciaDist 
						And IdClaveSSA = @IdClave_Anterior And Status = 'A'
				  End
				
			  End
           	           
           FETCH NEXT FROM tmpCol Into @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @IdClaveSSA_Sal, @IdProducto, @CodigoEAN, @CantidadAsignada
        END
    Close tmpCol
    Deallocate tmpCol 

	--------------------------------
	-- Se devuelve el resultado --
	--------------------------------

End 
Go--#SQL



