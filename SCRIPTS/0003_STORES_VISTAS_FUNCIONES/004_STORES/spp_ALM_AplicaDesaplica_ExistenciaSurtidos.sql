---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_ALM_AplicaDesaplica_ExistenciaSurtidos' and xType = 'P' ) 
   Drop Proc spp_ALM_AplicaDesaplica_ExistenciaSurtidos 
Go--#SQL 

Create Proc spp_ALM_AplicaDesaplica_ExistenciaSurtidos
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', @FolioSurtido varchar(8) = '00000014', @TipoFactor tinyint = 1, @Validar tinyint = 1
) 
With Encryption  
As 
Begin 
Set NoCount On
	
Declare @iFactor smallint	
	
	
	-- Determinar el tipo de Efecto de existencia en transito
	Set @iFactor = 1 
	If @TipoFactor <> 1 
	   Set @iFactor = -1 

	--Drop Table #tmpDisponible
	--Drop Table #Productos_Aplica
	--Drop Table #Productos_CodigoEAN_Lotes
	--Drop Table #Productos_CodigoEAN
	--Drop table #Productos

	Select *
	Into #tmpDisponible
	From Pedidos_Cedis_Det_Surtido_Distribucion
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia  = @IdFarmacia And FolioSurtido = @FolioSurtido

	--Select * from #tmpDisponible


	--Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdSubFarmacia, E.IdProducto, E.CodigoEAN, E.ClaveLote, E.IdPasillo, E.IdEstante, E.IdEntrepaño, CantidadRequerida As CantidadAsignada
	Update E Set ExistenciaSurtidos = (E.ExistenciaSurtidos + (D.CantidadRequerida * @iFactor)) 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones E (NoLock) 
	Inner Join #tmpDisponible D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdSubFarmacia = D.IdSubFarmacia 
				and E.IdProducto = D.IdProducto And E.CodigoEAN = D.CodigoEAN and E.ClaveLote = D.ClaveLote 
				and E.IdPasillo = D.IdPasillo and E.IdEstante = D.IdEstante and E.IdEntrepaño = D.IdEntrepaño )


	--IdProductos de surtimiento
	Select Distinct E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdProducto
	Into #Productos_Aplica
	From #tmpDisponible E


	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdSubFarmacia, E.IdProducto, E.CodigoEAN, E.ClaveLote, Sum(ExistenciaSurtidos) As CantidadAsignada
	Into #Productos_CodigoEAN_Lotes
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones E (NoLock) 
	Inner Join #Productos_Aplica D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdProducto = D.IdProducto )
	Group BY E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdSubFarmacia, E.IdProducto, E.CodigoEAN, E.ClaveLote

	--Select E.*
	Update E Set ExistenciaSurtidos = D.CantidadAsignada
	From FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
	Inner Join #Productos_CodigoEAN_Lotes D (NoLock) 
		On (
			E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdSubFarmacia = D.IdSubFarmacia 
			And E.IdProducto = D.IdProducto And E.CodigoEAN = D.CodigoEAN and E.ClaveLote = D.ClaveLote 
		   )

	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdProducto, E.CodigoEAN, Sum(ExistenciaSurtidos) As CantidadAsignada
	Into #Productos_CodigoEAN
	From FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
	Inner Join #Productos_Aplica D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdProducto = D.IdProducto )
	Group BY E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdProducto, E.CodigoEAN
	 

	 --Select E.*
	Update E Set ExistenciaSurtidos = D.CantidadAsignada
	From FarmaciaProductos_CodigoEAN E (NoLock) 
	Inner Join #Productos_CodigoEAN D (NoLock) 
		On (
			E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia
			And E.IdProducto = D.IdProducto And E.CodigoEAN = D.CodigoEAN
		   )

	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdProducto, Sum(ExistenciaSurtidos) As CantidadAsignada
	Into #Productos
	From FarmaciaProductos_CodigoEAN E (NoLock) 
	Inner Join #Productos_Aplica D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdProducto = D.IdProducto )
	Group BY E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdProducto


	--Select E.*
	Update E Set ExistenciaSurtidos = D.CantidadAsignada
	From FarmaciaProductos E (NoLock) 
	Inner Join #Productos D (NoLock) 
		On (
			E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia
			And E.IdProducto = D.IdProducto
		   )

	if @Validar = 1
	Begin
		if @@error = 0 
		Begin 
			Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos__Validar @IdEmpresa, @IdEstado, @IdFarmacia
		End
	End 
			
End 
Go--#SQL 