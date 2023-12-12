------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Cargar__OrdenDeSurtido' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Cargar__OrdenDeSurtido 
Go--#SQL   

--		Exec spp_Mtto_Pedidos_Cedis_Cargar__OrdenDeSurtido	@IdEmpresa = '001', @IdEstado = '11', @IdFarmacia = '1005', @FolioSurtido = '00000178', @EsManual = '0', @EsValidacion = '0'  

Create Proc spp_Mtto_Pedidos_Cedis_Cargar__OrdenDeSurtido 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1005', 
	@FolioSurtido varchar(8) = '178', 
	@EsManual bit = 0, @EsValidacion bit = 0   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@StatusPedido bit, 
	@Prioridad int  

	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('00000000' + @IdEstado, 2)  
	Set @IdFarmacia = right('00000000' + @IdFarmacia, 4)  
	Set @FolioSurtido = right('0000000000' + @FolioSurtido, 8)  



	----------------------- TABLA BASE 
	Select 		 
		IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, Presentacion, ClaveLote,   
		Convert(varchar(7), FechaCaducidad, 120) as Caducidad, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, 
		Caja, CantidadRequerida_Cajas, CantidadRequerida, CantidadAsignada, 
		ObservacionesSurtimiento as Observaciones, cast(Validado as int) as Validado, 
		0 as Orden_Distribucion, IdOrden  
	Into #tmp__SurtidoDistribucion 
	From vw_Pedidos_Cedis_Det_Surtido_Distribucion (NoLock)  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido   
		and 1 = 0 
	Order by IdOrden 
	----------------------- TABLA BASE 

---		spp_Mtto_Pedidos_Cedis_Cargar__OrdenDeSurtido 

	----------------------- OBTENER INFORMACION 
	If @EsManual = 0 
	Begin 
		Insert Into #tmp__SurtidoDistribucion  
		( 
			IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, Presentacion, ClaveLote,  
			Caducidad, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, Caja, CantidadRequerida_Cajas, CantidadRequerida, CantidadAsignada, 
			Observaciones, Validado, Orden_Distribucion, IdOrden 
		) 
		Select 
			IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, Presentacion, ClaveLote,   
			Convert(varchar(7), FechaCaducidad, 120) as Caducidad, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, 
			Caja, CantidadRequerida_Cajas, CantidadRequerida, CantidadAsignada, 
			ObservacionesSurtimiento as Observaciones, cast(Validado as int) as Validado, 0 as Orden_Distribucion, IdOrden  
		From vw_Pedidos_Cedis_Det_Surtido_Distribucion (NoLock)  
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido   
			-- and 1 = 0 
		Order by IdOrden, IdProducto 
	End 


	If @EsManual = 1  
	Begin 
		Insert Into #tmp__SurtidoDistribucion  
		( 
			IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, Presentacion, ClaveLote,  
			Caducidad, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, Caja, CantidadRequerida_Cajas, CantidadRequerida, CantidadAsignada, 
			Observaciones, Validado, Orden_Distribucion, IdOrden 
		) 
		Select 
			IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, Presentacion, ClaveLote,   
			Convert(varchar(7), FechaCaducidad, 120) as Caducidad, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, 
			Caja, CantidadRequerida_Cajas, Existencia As CantidadRequerida,  (Case When Existencia < CantidadAsignada Then Existencia Else CantidadAsignada End) As CantidadAsignada, 
			ObservacionesSurtimiento as Observaciones, cast(Validado as int) as Validado, 0 as Orden_Distribucion, IdOrden   
		From vw_Pedidos_Cedis_Det_Surtido_Distribucion (NoLock)  
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido   
			--and 1 = 0 
		Order by IdOrden, IdProducto 
	End 


	If @EsValidacion = 1 
	Begin 
		Delete From #tmp__SurtidoDistribucion Where CantidadAsignada <= 0  
	End 
	----------------------- OBTENER INFORMACION 

	

--------------------------------------------------- SALIDA FINAL 

	Select identity(int, 1, 1) as Id, *
	Into #tmp__SurtidoDistribucion___Secuencial  
	From #tmp__SurtidoDistribucion 
	Order By IdOrden, IdProducto
	

	Select * 
	From #tmp__SurtidoDistribucion___Secuencial 
	order by IdOrden, IdProducto


--------------------------------------------------- SALIDA FINAL 


---		spp_Mtto_Pedidos_Cedis_Cargar__OrdenDeSurtido 

End 
Go--#SQL 



	
