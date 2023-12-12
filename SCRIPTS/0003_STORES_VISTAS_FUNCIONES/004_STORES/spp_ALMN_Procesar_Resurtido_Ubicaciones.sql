--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ALMN_Procesar_Resurtido_Ubicaciones' and xType = 'P' ) 
	Drop Proc spp_ALMN_Procesar_Resurtido_Ubicaciones
Go--#SQL 

----	Exec  spp_ALMN_Procesar_Resurtido_Ubicaciones  '001', '11', '0005', 4, '0001'  

Create Proc spp_ALMN_Procesar_Resurtido_Ubicaciones 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', 
	@MesesParaCaducar int = 1, @IdPersonal varchar(4) = '0001'
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 
Declare 
	@iVenta int, @NombrePersonal varchar(300)  
	
	
	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 	
	Set @IdPersonal = right('0000' + @IdPersonal, 4) 		
	
		
	
	Set @NombrePersonal = '' 	
	Select @NombrePersonal = NombreCompleto From vw_Personal Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal
	
	-----  se obtiene el concentrado del consumo de las claves.
	Select 
		U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdClaveSSA_Sal, sum(U.Existencia) as Existencia, C.Piezas_Mes, C.Piezas_Semana, C.Piezas_Dia 
	into #tmpClavesPickeo
	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U
	Inner Join ALMN_Consumos_Estatales C
		On ( U.IdEmpresa = C.IdEmpresa AND U.IdEstado = C.IdEstado AND U.IdFarmacia = C.IdFarmacia AND U.IdClaveSSA_Sal = C.IdClaveSSA )
	Where U.IdEmpresa = @IdEmpresa AND U.IdEstado = @IdEstado AND U.IdFarmacia = @IdFarmacia --and C.Año = @Año and C.Mes = @Mes
		AND U.EsDePickeo = 1
	group by U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdClaveSSA_Sal, C.Piezas_Mes, C.Piezas_Semana, C.Piezas_Dia, C.Piezas_StockSegurida
	having (sum(U.Existencia)) <= C.Piezas_StockSegurida 
	Order By U.IdClaveSSA_Sal	
	
	
	----- se obtienen las claves y sus ubicaciones donde hace falta resurtir.
	Select 
		U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdClaveSSA_Sal, 
		U.IdPasillo, U.IdEstante, U.IdEntrepaño, Sum(U.Existencia) as Existencia, 
		C.Existencia AS Existencia_Clave, C.Piezas_Mes, C.Piezas_Semana, C.Piezas_Dia, ( C.Piezas_Semana - C.Existencia ) as Piezas_A_Resurtir
	Into #tmpClavesUbicaciones_Faltante	
	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U 
	Inner Join #tmpClavesPickeo C
		On ( U.IdEmpresa = C.IdEmpresa AND U.IdEstado = C.IdEstado AND U.IdFarmacia = C.IdFarmacia AND U.IdClaveSSA_Sal = C.IdClaveSSA_Sal )
	Where U.IdEmpresa = @IdEmpresa AND U.IdEstado = @IdEstado AND U.IdFarmacia = @IdFarmacia AND U.EsDePickeo = 1	
	Group By U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdClaveSSA_Sal, 
		U.IdPasillo, U.IdEstante, U.IdEntrepaño, C.Existencia, C.Piezas_Mes, C.Piezas_Semana, C.Piezas_Dia
	Order By U.IdClaveSSA_Sal
	
	
	--------  se concentran las claves de faltantes
	Select Distinct 
		IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA_Sal, 
		IdPasillo, IdEstante, IdEntrepaño, 
		Piezas_Mes, Piezas_Semana, Piezas_Dia,
		Piezas_A_Resurtir, SUM(Existencia_Clave) AS Existencia_Clave 
	into #tmpClaves_Faltantes
	From #tmpClavesUbicaciones_Faltante
	Group By IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA_Sal, 
	IdPasillo, IdEstante, IdEntrepaño, Piezas_Mes, Piezas_Semana, Piezas_Dia, Piezas_A_Resurtir
	
	
	Select 
		IdEmpresa, space(100) as Empresa, IdEstado, space(50) as Estado, IdFarmacia, space(100) as Farmacia, 
		IdClaveSSA_Sal, space(50) as ClaveSSA_Base, space(50) as ClaveSSA, space(50) as ClaveSSA_Aux, space(7500) as DescripcionSal,
		space(100) as Presentacion, 0 as ContenidoPaquete, Existencia_Clave, 0 as Existencia, 
		Piezas_Mes, Piezas_Semana, Piezas_Dia,
		Piezas_A_Resurtir, 1 as Nivel,
		( convert(varchar(6), IdPasillo) + ' - ' + convert(varchar(6), IdEstante) + ' - ' + convert(varchar(6), IdEntrepaño) ) as Ubicacion_Destino,
		SPACE(100) as Ubicacion_Origen
	Into #tmpClaves_Resurtido_Final
	From #tmpClaves_Faltantes 


	Insert Into #tmpClaves_Resurtido_Final 
	Select 
		U.IdEmpresa, '', U.IdEstado, '', U.IdFarmacia, '', U.IdClaveSSA_Sal, '', '', '', '',
		'', 0, 0, Sum(U.Existencia) as Existencia, 
		--C.Piezas_Mes, C.Piezas_Semana, C.Piezas_Dia,
		0, 0, 0,
		0, 2, '', 
		( convert(varchar(6), U.IdPasillo) + ' - ' + convert(varchar(6), U.IdEstante) + ' - ' + convert(varchar(6), U.IdEntrepaño) ) as Ubicacion_Origen		
	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U
	Inner Join #tmpClaves_Faltantes C
		On ( U.IdEmpresa = C.IdEmpresa AND U.IdEstado = C.IdEstado AND U.IdFarmacia = C.IdFarmacia AND U.IdClaveSSA_Sal = C.IdClaveSSA_Sal )
	Where U.IdEmpresa = @IdEmpresa AND U.IdEstado = @IdEstado AND U.IdFarmacia = @IdFarmacia AND U.EsDePickeo = 0
		and U.MesesParaCaducar Between 1 and @MesesParaCaducar	
	Group By U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdClaveSSA_Sal, 
		U.IdPasillo, U.IdEstante, U.IdEntrepaño, C.Piezas_Mes, C.Piezas_Semana, C.Piezas_Dia
	Having Sum(U.Existencia) > 0
	--Order By U.IdClaveSSA_Sal, U.MesesParaCaducar ASC	

	
	------------ se actualizan datos de empresa, estado, farmacia y de claves ------------------------------------------------------------ 
	Update T Set T.Empresa = E.NombreEmpresa, T.Estado = E.Estado, T.Farmacia = E.Farmacia
	From #tmpClaves_Resurtido_Final T (Nolock)
	Inner Join vw_EmpresasFarmacias E (Nolock)
		On ( T.IdEmpresa = E.IdEmpresa AND T.IdEstado = E.IdEstado AND T.IdFarmacia = E.IdFarmacia )
		
		
		
	Update T Set T.ClaveSSA_Base = S.ClaveSSA_Base, T.ClaveSSA = S.ClaveSSA, T.ClaveSSA_Aux = S.ClaveSSA_Aux,
	T.DescripcionSal = S.DescripcionCortaClave, T.Presentacion = S.Presentacion, T.ContenidoPaquete = S.ContenidoPaquete
	From #tmpClaves_Resurtido_Final T (Nolock)
	Inner Join vw_ClavesSSA_Sales S (Nolock) On ( T.IdClaveSSA_Sal = S.IdClaveSSA_Sal )
	

	--------------------------------------------------------------------------------------------------------------------------------------	
	------------------ SALIDA FINAL 
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionSal,
		Presentacion, ContenidoPaquete, 
		SUM(Existencia_Clave) AS Existencia_Clave, Sum(Existencia) as Existencia, 
		0 as ExistenciaEnAlmacenaje, 
		0 as DisponibleEnAlmacenaje, cast('' as varchar(200)) as DisponibleEnAlmacenajeDescripcion, 
		Piezas_Mes, Piezas_Semana, Piezas_Dia,
		Piezas_A_Resurtir, Nivel, Ubicacion_Destino, Ubicacion_Origen,
		@IdPersonal as IdPersonal, @NombrePersonal as Personal, @MesesParaCaducar as MesesParaCaducar,
		(GetDate()-180) as FechaInicio, GetDate() as FechaFin 
	Into #tmpClaves_Resurtido_Final___Almacenaje	
	From #tmpClaves_Resurtido_Final  
	-- Where ClaveSSA like '%2823%'  -- 2823		--	0291
	Group By IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionSal,
		Presentacion, ContenidoPaquete, 
		Piezas_Mes, Piezas_Semana, Piezas_Dia,
		Piezas_A_Resurtir, Nivel, Ubicacion_Destino, Ubicacion_Origen 

			
	
	Update A Set ExistenciaEnAlmacenaje = 
	IsNull
	(
	( 
		select Sum(Existencia) 
		From #tmpClaves_Resurtido_Final X Where X.ClaveSSA = A.ClaveSSA and Nivel <> 1 
	), 0)  
	From #tmpClaves_Resurtido_Final___Almacenaje A 

	
	Update A Set DisponibleEnAlmacenaje = (case when ExistenciaEnAlmacenaje > 0 then 1 else 2 end), 
		DisponibleEnAlmacenajeDescripcion = (case when ExistenciaEnAlmacenaje > 0 then 'CON	EXISTENCIA EN ALMACENAJE' else 'SIN EXISTENCIA EN ALMACENAJE' end) 
	From #tmpClaves_Resurtido_Final___Almacenaje A 
	
	
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionSal,
		Presentacion, ContenidoPaquete, 
		Existencia_Clave, Existencia, 
		ExistenciaEnAlmacenaje, DisponibleEnAlmacenaje, DisponibleEnAlmacenajeDescripcion, 
		Piezas_Mes, Piezas_Semana, Piezas_Dia,
		Piezas_A_Resurtir, Nivel, Ubicacion_Destino, Ubicacion_Origen,
		@IdPersonal as IdPersonal, @NombrePersonal as Personal, @MesesParaCaducar as MesesParaCaducar,
		(GetDate()-180) as FechaInicio, GetDate() as FechaFin
	From #tmpClaves_Resurtido_Final___Almacenaje  
	Where 
		-- ClaveSSA like '%0291%'  -- 2823		--	0291
		DisponibleEnAlmacenaje = 1
	------Group By IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
	------	IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionSal,
	------	Presentacion, ContenidoPaquete, 
	------	Piezas_Mes, Piezas_Semana, Piezas_Dia,
	------	Piezas_A_Resurtir, Nivel, Ubicacion_Destino, Ubicacion_Origen 
	Order By DisponibleEnAlmacenaje, ClaveSSA, DescripcionSal 
	
		
	----select distinct ClaveSSA 	
	----From #tmpClaves_Resurtido_Final___Almacenaje  
	----Where 
	----	-- ClaveSSA like '%0291%'  -- 2823		--	0291
	----	DisponibleEnAlmacenaje = 1 and nivel > 1 
				
		-----		spp_ALMN_Procesar_Resurtido_Ubicaciones
		
End	
Go--#SQL 


	---sp_ListaColumnas 'tmpClavesParaResurtir'