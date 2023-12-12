-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__DescargarInventarios' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__DescargarInventarios
Go--#SQL 

Create Proc spp_INT_SESEQ__DescargarInventarios 
( 
	----@IdEmpresa varchar(3) = '1', 
	----@IdEstado varchar(2) = '22', 
	----@IdFarmacia varchar(4) = '3',  
	----@FolioVenta varchar(8) = '4' 
	@ListaDeCLUES varchar(max)  = '', --QTSSA002901', -- 'QTSSA012935', 'QTSSA001752', 
	@TipoUnidad int = 1,  --	1 ==> CEDIS | 2 ==> Farmacias 
	@MostrarResumen int = 0, 
	@ClaveSSA varchar(50) = '', 
	@MostrarInventario_Propio int = 0, 
	@MostrarDescripciones int = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
	
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200),   	
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '0104',    
	@IdCliente varchar(4) = '0042', 
	@IdSubCliente varchar(4) = '0003'  

	Select Top 0 IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño 
	Into #tmp__ExclusionUbicaciones 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 

	Select * 
	into #tmp__Claves_CB 
	From vw_Claves_Precios_Asignados (NoLock) 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 


	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '0' as IdPasillo, '0' as IdEstante, '0' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '0' as IdPasillo, '0' as IdEstante, '1' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '0' as IdPasillo, '0' as IdEstante, '2' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '0' as IdPasillo, '0' as IdEstante, '3' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '0' as IdPasillo, '0' as IdEstante, '4' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '0' as IdPasillo, '0' as IdEstante, '5' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '0' as IdPasillo, '0' as IdEstante, '13' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '0' as IdPasillo, '0' as IdEstante, '14' as IdEntrepaño 


	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '40' as IdPasillo, '0' as IdEstante, '0' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '41' as IdPasillo, '0' as IdEstante, '0' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '50' as IdPasillo, '0' as IdEstante, '0' as IdEntrepaño 

	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '100' as IdPasillo, '0' as IdEstante, '0' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '100' as IdPasillo, '0' as IdEstante, '1' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '100' as IdPasillo, '0' as IdEstante, '2' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '100' as IdPasillo, '0' as IdEstante, '3' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '100' as IdPasillo, '0' as IdEstante, '4' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '100' as IdPasillo, '0' as IdEstante, '5' as IdEntrepaño 


	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '101' as IdPasillo, '0' as IdEstante, '1' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '101' as IdPasillo, '0' as IdEstante, '2' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '101' as IdPasillo, '0' as IdEstante, '3' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '101' as IdPasillo, '0' as IdEstante, '4' as IdEntrepaño 
	Insert Into #tmp__ExclusionUbicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) Select @IdEmpresa, @IdEstado, @IdFarmacia, '101' as IdPasillo, '0' as IdEstante, '5' as IdEntrepaño 

	----Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	----Set @IdEstado = right('000000000000' + @IdEstado, 2) 
	----Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4) 
	----Set @FolioVenta = right('000000000000' + @FolioVenta, 8) 
	


	Select Valor as CLUES  
	Into #tmp_CLUES 
	From dbo.fg_SplitCadena( @ListaDeCLUES ) 


	Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, F.EsAlmacen, L.CLUES    
	Into #tmp_Farmacias 
	From INT_SESEQ__CFG_Farmacias_UMedicas C (NoLock)  
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
	Inner Join #tmp_CLUES L (NoLock) On ( L.CLUES like '%' + C.Referencia_SESEQ + '%' )  


	If Not Exists ( Select * from #tmp_Farmacias ) 
	Begin 
		Insert Into #tmp_Farmacias ( IdEmpresa, IdEstado, IdFarmacia, EsAlmacen, CLUES ) 
		Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, F.EsAlmacen, C.Referencia_SESEQ as CLUES    
		From INT_SESEQ__CFG_Farmacias_UMedicas C (NoLock)  
		Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
	End 

	--select * from #tmp_Farmacias 

--		spp_INT_SESEQ__DescargarInventarios 

	--------------------------------- INFORMACION  
	Select 
		@IdCliente as IdCliente, @IdSubCliente as IdSubCliente,  
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, 
		F.CLUES, cast('' as varchar(1000)) as NombreUnidad, 
		L.CodigoEAN, 
		cast(0 as numeric(14,4)) as ContenidoPaquete, 
		cast(0 as numeric(14,4)) as ContenidoPaquete_Licitado, 
		cast(-1 as numeric(14,4)) as Factor, 
		0 as EnCuadroLicitado, 
		cast('' as varchar(50)) as ClaveSSA, 
		cast('' as varchar(50)) as ClaveSSA_Base, 
		cast('' as varchar(50)) as Mascara, 
		cast('' as varchar(max)) as Descripcion, 
		cast('' as varchar(500)) as Presentacion, 
		cast('' as varchar(500)) as TipoDeInsumo, 
		0 as EsRelacionado, 
		0 as TieneMascara, 
		(case when ClaveLote like '%*%' then 1 else 0 end) as EsConsigna,  
		L.IdSubFarmacia, L.ClaveLote, cast('' as varchar(10)) as FechaCaducidad,  
		sum(Existencia - ( ExistenciaEnTransito + ExistenciaSurtidos )) as Existencia_Piezas, 
		cast(0 as numeric(14,4)) as Existencia, 
		0 as EsAlmacen 
	Into #tmp_Detalles 
	From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
	--Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.CodigoEAN = P.CodigoEAN And P.ClaveSSA like '%0247%' ) 
	Inner Join #tmp_Farmacias F (NoLock) On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and F.EsAlmacen = 0 ) 
	Where 1 = 1 and @TipoUnidad = 2 
		--and L.IdFarmacia = 125 
	Group by 
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, F.CLUES, L.CodigoEAN, (case when ClaveLote like '%*%' then 1 else 0 end), L.IdSubFarmacia, L.ClaveLote 
	Order by L.IdEmpresa, L.IdEstado, L.IdFarmacia 	
	

	Insert Into #tmp_Detalles 
	Select 
		@IdCliente as IdCliente, @IdSubCliente as IdSubCliente,  
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, 
		F.CLUES, cast('' as varchar(1000)) as NombreUnidad, 
		L.CodigoEAN, 
		cast(0 as numeric(14,4)) as ContenidoPaquete, 
		cast(0 as numeric(14,4)) as ContenidoPaquete_Licitado, 
		cast(-1 as numeric(14,4)) as Factor, 
		0 as EnCuadroLicitado, 
		cast('' as varchar(50)) as ClaveSSA, 
		cast('' as varchar(50)) as ClaveSSA_Base, 
		cast('' as varchar(50)) as Mascara, 
		cast('' as varchar(max)) as Descripcion, 
		cast('' as varchar(500)) as Presentacion, 
		cast('' as varchar(500)) as TipoDeInsumo, 
		0 as EsRelacionado, 
		0 as TieneMascara, 
		(case when ClaveLote like '%*%' then 1 else 0 end) as EsConsigna,  
		L.IdSubFarmacia, L.ClaveLote, cast('' as varchar(10)) as FechaCaducidad,  
		sum(Existencia - ( ExistenciaEnTransito + ExistenciaSurtidos )) as Existencia_Piezas, 
		cast(0 as numeric(14,4)) as Existencia, 
		1 as EsAlmacen 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 
	--Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.CodigoEAN = P.CodigoEAN And P.ClaveSSA like '%0247%' ) 
	Inner Join #tmp_Farmacias F (NoLock) On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and F.EsAlmacen = 1 ) 
	Where 1 = 1 and @TipoUnidad = 1 and 	
	Not Exists 
	( 
		Select * 
		From #tmp__ExclusionUbicaciones E (NoLock) 
		Where L.IdEmpresa = E.IdEmpresa and L.IdEstado = E.IdEstado and L.IdFarmacia = E.IdFarmacia 
			and L.IdPasillo = E.IdPasillo and L.IdEstante = E.IdEstante and L.IdEntrepaño = E.IdEntrepaño 
	) 
	--and 1=0
	Group by 
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, F.CLUES, L.CodigoEAN, (case when ClaveLote like '%*%' then 1 else 0 end), L.IdSubFarmacia, L.ClaveLote 
	Order by L.IdEmpresa, L.IdEstado, L.IdFarmacia 	


	------------ Limpiar datos en CEROS 
	Delete From #tmp_Detalles Where Existencia_Piezas <= 0 
	------------ Limpiar datos en CEROS 


	------------ Asignar caducidades  
	Update L set FechaCaducidad = convert(varchar(7),  F.FechaCaducidad, 120) 
	From #tmp_Detalles L (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( 
				L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia and 
				--L.IdProducto = F.IdProducto and 
				L.CodigoEAN = F.CodigoEAN and L.ClaveLote = F.ClaveLote   
		   )
	------------ Asignar caducidades  



	Update L Set NombreUnidad = F.NombreFarmacia 
	From #tmp_Detalles L 
	Inner Join CatFarmacias F (NoLock) On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia ) 

	Update I Set ClaveSSA = P.ClaveSSA, ClaveSSA_Base = P.ClaveSSA, Mascara = P.ClaveSSA, ContenidoPaquete = P.ContenidoPaquete, Descripcion = P.DescripcionClave, Presentacion = P.Presentacion_ClaveSSA  
	From #tmp_Detalles I 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( I.CodigoEAN = P.CodigoEAN ) 	 

	Update I Set Existencia = (Existencia_Piezas / ContenidoPaquete)
	From #tmp_Detalles I 
	--------------------------------- INFORMACION  



	--		spp_INT_SESEQ__DescargarInventarios			@MostrarResumen = 1 


	--select * from #tmp_Detalles 


	--------------------------------- RELACION DE CLAVES 
	Update V Set ClaveSSA = M.ClaveSSA, EsRelacionado = 1 
	From #tmp_Detalles V (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves M (NoLock) 
		On ( V.IdEstado = M.IdEstado and V.IdCliente = M.IdCliente and V.IdSubCliente = M.IdSubCliente and V.ClaveSSA = M.ClaveSSA_Relacionada )  
	--------------------------------- RELACION DE CLAVES 

	--------------------------------- ASIGNACION DE INFORMACION GENERAL 
	Update V Set 
		EnCuadroLicitado = 1, 
		Descripcion = C.DescripcionClave, Presentacion = C.Presentacion, TipoDeInsumo = C.TipoClaveDescripcion, 
		V.Factor = C.Factor, V.ContenidoPaquete_Licitado = C.ContenidoPaquete_Licitado  
	From #tmp_Detalles V (NoLock) 
	Inner Join #tmp__Claves_CB C (NoLock) On ( V.ClaveSSA = C.ClaveSSA ) 

	Update I Set Existencia = ((Existencia_Piezas * Factor) / ContenidoPaquete_Licitado)
	From #tmp_Detalles I 
	Where ContenidoPaquete_Licitado > 0 
	--------------------------------- ASIGNACION DE INFORMACION GENERAL 

	--select * from #tmp_Detalles 

	--------------------------------- MASCARAS 
	Update V Set Mascara = M.Mascara, TieneMascara = 1 
	From #tmp_Detalles V (NoLock) 
	Inner Join vw_ClaveSSA_Mascara M (NoLock) On ( V.IdEstado = M.IdEstado and V.IdCliente = M.IdCliente and V.IdSubCliente = M.IdSubCliente and V.ClaveSSA = M.ClaveSSA )  
	--------------------------------- MASCARAS 
	
	--select * from #tmp_Detalles 

	--------------------------------- MASCARAS SIAM 
	--Update V set EsRelacionado = 0 
	--From #tmp_Detalles V (NoLock) 

	--Update V Set Mascara = M.Clave_SIAM, EsRelacionado = 1  
	--From #tmp_Detalles V (NoLock) 
	--Inner Join INT_SESEQ__CGF_ClavesSIAM M (NoLock) On ( V.ClaveSSA = M.ClaveSSA )  
	--------------------------------- MASCARAS SIAM 



	--------------------------------- INFORMACION DE CONCENTRADA  
	Update D Set Existencia = 0, Existencia_Piezas = 0 
	From #tmp_Detalles D 
	where @MostrarInventario_Propio = 0 and EsConsigna = 0 


	Select 
		-- IdFarmacia, 
		CLUES, 
		IdFarmacia, 
		cast(NombreUnidad as varchar(2000)) as NombreUnidad, 
		EnCuadroLicitado, 
		Mascara, TieneMascara, 
		(case when TipoDeInsumo = 'MEDICAMENTO' then 'MD' else 'MC' end) as TipoDeInsumo, 
		CodigoEAN, 
		ClaveSSA, Descripcion, Presentacion, 
		ContenidoPaquete_Licitado, Factor, 
		ClaveLote, FechaCaducidad, 
		sum(Existencia) as Existencia, 
		sum(Existencia_Piezas) as Existencia_Piezas,   

		(case when max(EsConsigna) = 0 then sum(Existencia) else 0 end) as Existencia_PROVEEDOR,   
		(case when max(EsConsigna) = 1 then sum(Existencia) else 0 end) as Existencia_SESEQ,   
		(case when max(EsConsigna) = 0 then sum(Existencia_Piezas) else 0 end) as Existencia_Piezas_PROVEEDOR,   
		(case when max(EsConsigna) = 1 then sum(Existencia_Piezas) else 0 end) as Existencia_Piezas_SESEQ  


	Into #tmp_Concentrado 
	From #tmp_Detalles 
	Where ClaveSSA like '%' + @ClaveSSA + '%' 
	Group by 
		-- IdFarmacia, 
		CLUES, 
		IdFarmacia, 
		NombreUnidad, 
		EnCuadroLicitado, 
		Mascara, TieneMascara, 
		--TipoDeInsumo, 
		(case when TipoDeInsumo = 'MEDICAMENTO' then 'MD' else 'MC' end), 
		CodigoEAN, 
		ClaveSSA, Descripcion, Presentacion, 
		ContenidoPaquete_Licitado, Factor, 
		ClaveLote, FechaCaducidad  


	Update C Set NombreUnidad = NombrePropio_UMedica 
	From #tmp_Concentrado C 
	Inner Join CatFarmacias F (NoLock) On ( F.IdEstado = '22' and C.Clues = F.Clues ) 
	Where 1 = 0 
	--------------------------------- INFORMACION DE CONCENTRADA   
	

	--		select * from vw_ClaveSSA_Mascara 


	--		spp_INT_SESEQ__DescargarInventarios  @MostrarResumen = 1  



	---------------- SALIDA FINAL 
	--Select * from #tmp_Detalles  
	Select -- top 10 
		'CLUES' = CLUES, 
		'Id Unidad' = IdFarmacia, 
		'Nombre_Unidad' = NombreUnidad, 

		--'Nombre_Unidad' = '', 
		'En cuadro licitado' = (case when EnCuadroLicitado = 1 then 'SI' else 'NO' end), 
		'Tiene_Relacion' = (case when TieneMascara = 1 then 'SI' else 'NO' end), 
		'Tipo_Insumo' = TipoDeInsumo, 
		'Clave_SESEQ' =  Mascara, 
		'Clave_PROVEEDOR' =  ClaveSSA, 
		
		'Descripcion' =  (case when @MostrarDescripciones = 1 then Descripcion else '' end), 
		'Presentacion' =  (case when @MostrarDescripciones = 1 then Presentacion else '' end), 

		'Contenido paquete licitado' = ContenidoPaquete_Licitado, 
		'Factor' = Factor, 
		
		'CodigoEAN' = CodigoEAN, 
		'ClaveLote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 

		--'Descripcion' =  Descripcion, 
		--'Presentacion' = Presentacion, 
	

	--CodigoEAN, ClaveLote, convert(varchar(5), FechaCaducidad, 120) as FechaCaducidad,  

		--'Existencia_General' =  (Existencia_SESEQ + Existencia_PROVEEDOR), 
		--'Existencia' =  ( Existencia_SESEQ + Existencia_PROVEEDOR ),   
		'Existencia' =  ( Existencia_SESEQ  ),  
		
		--'Existencia en piezas' = ( Existencia_Piezas_SESEQ + Existencia_Piezas_PROVEEDOR ), 
		'Existencia en piezas' = Existencia_Piezas_SESEQ 

		-- 'Existencia_SESEQ' =  0, -- Existencia_SESEQ, 
		, 'Existencia Proveedor' = Existencia_PROVEEDOR 
		, 'Existencia en piezas Proveedor' = Existencia_Piezas_PROVEEDOR 

	From #tmp_Concentrado 
	--Where TieneMascara = 0 	
	----	--CLUES = 'QTSSA001740' 
	Order by CLUES, TieneMascara 


	--If @MostrarResumen = 1 
	--Begin
	--	Select 
	--		'Tiene_Relacion' = TieneMascara, 
	--		'Tipo_Insumo' = TipoDeInsumo, 
	--		'Clave_SESEQ' =  Mascara, 
	--		'Clave_PROVEEDOR' =  ClaveSSA  
	--	From #tmp_Concentrado 
	--	Where TieneMascara = 0 
	--	Group by  TieneMascara, TipoDeInsumo, Mascara, ClaveSSA 


	--	----Where TieneMascara = 0 			 
	--End 


	---------------- SALIDA FINAL 


End 
Go--#SQL 

	