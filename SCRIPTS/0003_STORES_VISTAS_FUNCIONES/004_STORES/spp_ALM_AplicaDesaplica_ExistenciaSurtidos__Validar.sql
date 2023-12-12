------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ALM_AplicaDesaplica_ExistenciaSurtidos__Validar' and xType = 'P' )  
   Drop Proc spp_ALM_AplicaDesaplica_ExistenciaSurtidos__Validar  
Go--#SQL 

--Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos__Validar '001', '11', '2005'

Create Proc spp_ALM_AplicaDesaplica_ExistenciaSurtidos__Validar 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001'
) 	
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sMensaje varchar(max), 
	@sLista varchar(max), 	
	@sCodigo varchar(100), 
	@iRegistros int, 
	@EsAlmacen int  
	
	Set @sMensaje = 'Ocurrió un error al validar las existencias'   
	Set @sLista = '' 
	Set @sCodigo = '' 
	Set @iRegistros = 0 
	Select @EsAlmacen = EsAlmacen From CatFarmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and EsAlmacen = 1
	Set @EsAlmacen = IsNull(@EsAlmacen, 0) 


	Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Sum(CantidadRequerida) As Cantidad, Sum(CantidadAsignada) As CantidadAsignada
	Into #Surtido
	From Pedidos_Cedis_Enc_Surtido E (NoLock)
	Inner Join Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioSurtido = D.FolioSurtido)
	Where E.Status Not In ('E', 'C', 'R', 'P') 
	Group By D.IdEmpresa, D.IdEstado, D.IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño


	Select F.*, F.Existencia - ExistenciaEnTransito As ExistenciaDisponible, S.Cantidad As CantidadRequerida, CantidadAsignada, ([Existencia] - [ExistenciaEnTransito]) - S.Cantidad As Dif
	into #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	Left Join #Surtido S
		On (F.IdEmpresa = S.IdEmpresa And F.IdEstado = S.IdEstado And F.IdFarmacia = S.IdFarmacia And
			F.IdSubFarmacia = S.IdSubFarmacia And F.IdProducto = S.IdProducto And F.CodigoEAN = S.CodigoEAN And F.ClaveLote = S.ClaveLote And
			F.IdPasillo = S.IdPasillo And F.IdEstante = S.IdEstante And F.IdEntrepaño = S.IdEntrepaño)
	Where F.ExistenciaSurtidos <> IsNull(S.Cantidad, 0) And F.IdEmpresa = @IdEmpresa And F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia
	
	---------------------------------------------------------------------------------------------------------------------------------------------  			

		

	------ Validación final 
	Select @iRegistros = count(*) From #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones


	if @iRegistros > 0 
	Begin 
		
		Declare #cursorProductos  
		Cursor For 
			Select Distinct IdProducto 
			From #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
		Open #cursorProductos 
		FETCH NEXT FROM #cursorProductos Into @sCodigo 
			WHILE @@FETCH_STATUS = 0 
			BEGIN 
				
				Set @sLista = @sLista + @sCodigo + '    ' + char(10) + char(13) 
				
				FETCH NEXT FROM #cursorProductos Into @sCodigo   
			END	 
		Close #cursorProductos 
		Deallocate #cursorProductos 			
	
		Set @sMensaje = @sMensaje + char(10) + char(13) + @sLista 
		RaisError (@sMensaje, 16, 1 )
		Return 
		
	End 

End    
Go--#SQL


