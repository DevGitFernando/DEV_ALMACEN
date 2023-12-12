------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_INT_ND_Unidad_GenerarRemisiones_Detallado' and xType = 'P' ) 
    Drop Proc spp_INT_ND_Unidad_GenerarRemisiones_Detallado
Go--#SQL 
  
/* 
	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_RptAdmonDispensacion_Detallado__General' and xType = 'U' ) 
		Truncate Table INT_ND_RptAdmonDispensacion_Detallado__General 		


Exec spp_INT_ND_Unidad_GenerarRemisiones_Detallado @IdEmpresa = '003', @IdEstado = '16', @CodigoCliente = '2181002', 
	@FechaInicial = '2015-01-01', @FechaFinal = '2015-01-31', @GUID = '123c6b4b-c202-4944-9197-a4f217ae9b9e', 
	@IdFarmacia = '0011', @Año_Causes = '2014', @TipoDeInventario = 2  

	select * from INT_ND_RptAdmonDispensacion_Detallado__General where ClaveSSA = '060.550.0446'   

*/ 

----    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', 
----    @CodigoCliente varchar(20) = '2179083', -- '2179105', 
----    @FechaInicial varchar(10) = '2015-01-05', @FechaFinal varchar(10) = '2015-01-05', 
----    @GUID varchar(100) = 'asee-do2', @IdFarmacia varchar(4) = '0014', @Año_Causes int = 2012, 
----    @TipoDeInventario smallint = 1      
----) 
  
Create Proc spp_INT_ND_Unidad_GenerarRemisiones_Detallado 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', 
    @CodigoCliente varchar(20) = '2179083', -- '2179105', 
    @FechaInicial varchar(10) = '2015-01-05', @FechaFinal varchar(10) = '2015-01-05', 
    @GUID varchar(100) = '2FA1B8D8-6663-4E1D-A296-B72421031BF5', @IdFarmacia varchar(4) = '0014', @Año_Causes int = 2012, 
    @SepararCauses int = 0, @TipoDeInventario smallint = 1       
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

Declare 
	-- @IdFarmacia varchar(4), 
	@NombreFarmacia varchar(200), 
	@NombreEmpresa varchar(200), 	
	@Folio varchar(8), 
	@sFCBha varchar(8), 
	@sConsCButivo varchar(3), 
	@sMensaje varchar(1000), 
	@dFechaRemision datetime, 
	@sFolioRemision varchar(100)   
	
Declare 
	@Existen_Datos int 	
	
	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	--Set @dFechaRemision = cast(@FechaDeProceso as datetime) 
	Set @dFechaRemision = cast(@FechaInicial as datetime) 	
	Set @sFolioRemision = right('0000' + cast(day(@dFechaRemision) as varchar), 2) 
	Set @sFolioRemision = @sFolioRemision + right('0000' + cast(month(@dFechaRemision) as varchar), 2) 
	Set @sFolioRemision = @sFolioRemision + right('0000' + cast(year(@dFechaRemision) as varchar), 2)  
	Set @Existen_Datos = 0 
	Set @NombreFarmacia = '' 
	Set @NombreEmpresa = '' 

--	Select @dFechaRemision as FechaRemision, @sFolioRemision as FolioRemision  
	
	-- Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 
	

---------------------- Intermedio 		
	Select  
		IdCliente, CodigoCliente, NombreCliente, 
		IdEstado, Estado, IdFarmacia, Farmacia, IdTipoUnidad, TipoDeUnidad, EsDeSurtimiento, Status 
	into #tmpClientes 
	From vw_INT_ND_Clientes F  	
	Where F.IdEstado = @IdEstado 

	Select IdFarmacia, EsAlmacen 
	into #tmpFarmacias 
	From CatFarmacias F 
	Where F.IdEstado = @IdEstado 
	
	Select -- @IdFarmacia = IdFarmacia, 
		@NombreFarmacia =  NombreCliente 
	From #tmpClientes F 
	Where CodigoCliente = @CodigoCliente and IdFarmacia = @IdFarmacia 
	
	---------- En el caso de utilizar mas de una vez el Codigo Cliente 
	----Select @IdFarmacia = IdFarmacia, @NombreFarmacia =  NombreCliente 
	----From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) 
	----	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado 
	----		and convert(varchar(10), V.FechaRegistro, 120) = @FechaDeProceso and V.GUID = @GUID 				
	
	Select @NombreEmpresa = Nombre 
	From CatEmpresas (NoLock) 
	Where IdEmpresa =  @IdEmpresa 
	

--------------------------------------------------- Obtener la lista de Claves a considerar como CAUSES 
	Select 
		IdClaveSSA, ClaveSSA, Descripcion, Presentacion, EsSeguroPopular, Año, PrecioBase, 
		0 as Porcentaje, PrecioAdmon, 0 as PrecioNeto, EsDollar, Status 
	Into #tmp_ClavesSSA_Causes 	
	From CatClavesSSA_SeguroPopular_Anual 
	Where Año = @Año_Causes 	
	
	
		
--------------------------------------------------- Determinar los anexos de la Unidad 		
	Select Distinct  
		M.IdEstado, M.CodigoCliente, C.IdFarmacia, C.Nombre, 
		-- replace(M.IdAnexo, ' ', '') as IdAnexo, 
		M.IdAnexo, 
		A.NombreAnexo, A.NombrePrograma, A.Prioridad, 
		0 as TieneDatosAProcesar, 
		identity(int, 1, 1) as Keyx     
	Into #tmpAnexos 
	From INT_ND_CFG_CB_Anexos_Miembros M (NoLock) 
	Inner Join INT_ND_CFG_CB_Anexos A (NoLock) 
		-- On ( M.IdEstado = A.IdEstado and replace(M.IdAnexo, ' ', '') = replace(A.IdAnexo, ' ', '')  )	
		On ( M.IdEstado = A.IdEstado and M.IdAnexo = A.IdAnexo )			
	Inner Join INT_ND_Clientes C (NoLock) 
		On ( M.IdEstado = C.IdEstado and M.CodigoCliente = C.CodigoCliente and C.EsDeSurtimiento = 0 and C.Status = 'A' ) 
	Where M.IdEstado = @IdEstado And M.CodigoCliente = @CodigoCliente 
		And M.Status = 'A' and A.Status = 'A' 
	Order by Prioridad 

	--	select * from #tmpAnexos 

/* 
	---		VIEJO 
	Select Distinct 
		A.IdEstado, A.IdFarmacia, 
		-- replace(A.IdAnexo, ' ', '') as IdAnexo, 
		A.IdAnexo, 
		A.NombreAnexo, A.NombrePrograma, A.Prioridad, 
		C.ClaveSSA, C.ClaveSSA_ND, C.ClaveSSA_Mascara, C.ManejaIva, C.PrecioVenta, C.PrecioServicio, C.Descripcion_Mascara, 
		identity(int, 1, 1) as Keyx   
	Into #tmpAnexos_Detalles 
	From #tmpAnexos A (NoLock) 
	Inner Join INT_ND_CFG_CB_CuadrosBasicos C (NoLock) 
		-- On ( A.IdEstado = C.IdEstado and replace(A.IdAnexo, ' ', '') = replace(C.IdAnexo, ' ', '') and A.Prioridad = C.Prioridad )  
		On ( A.IdEstado = C.IdEstado and A.IdAnexo = C.IdAnexo and A.Prioridad = C.Prioridad )  		
	-- Where C.ClaveSSA_ND = '9980000266' 	
	Order by Prioridad, C.Descripcion_Mascara   	
*/ 	
	
	
	Select Distinct 
		A.IdEstado, @IdFarmacia as IdFarmacia, 
		-- replace(A.IdAnexo, ' ', '') as IdAnexo, 
		A.IdAnexo, 
		A.NombreAnexo, A.NombrePrograma, A.Prioridad, 
		C.ClaveSSA, C.ClaveSSA_ND, C.ClaveSSA_Mascara, C.ManejaIva, C.PrecioVenta, C.PrecioServicio, C.Descripcion_Mascara, 
		0 as TieneDatosAProcesar, IsNull(Lote, '') As Lote, 
		identity(int, 1, 1) as Keyx   
	Into #tmpAnexos_Detalles 
	From INT_ND_CFG_CB_CuadrosBasicos C (NoLock) 
	Inner Join INT_ND_CFG_CB_Anexos A (NoLock) 
		On ( A.IdAnexo = C.IdAnexo and A.Prioridad = C.Prioridad )  		
	-- Where C.ClaveSSA_ND = '9980000266' 	
	Order by Prioridad, C.Descripcion_Mascara   	
--------------------------------------------------- Determinar los anexos de la Unidad 	
	
--	select * from #tmpAnexos_Detalles Where ClaveSSA_ND = '9980000266' 

	
	
	------Update D Set ClaveSSA = C.ClaveSSA 
	------From #tmpAnexos_Detalles D 
	------Inner Join CatClavesSSA_Sales C (NoLock) 
	------	On 
	------	( 
	------		D.ClaveSSA_ND = C.ClaveSSA_Base 
	------		or  
	------		right(replicate('0', 20) + replace(D.ClaveSSA_ND, '.', ''), 20) = right(replicate('0', 20) + replace(C.ClaveSSA_Base, '.', ''), 20)
	------	) 

	------Delete From #tmpAnexos_Detalles Where ClaveSSA = '' 

---		spp_INT_ND_Unidad_GenerarRemisiones_Detallado  	
	

------------------------------- Temporal de Productos 
	Select *, ContenidoPaquete as ContenidoPaquete_Axiliar  
	Into #tmpProductos 
	From vw_Productos_CodigoEAN 
	
	Update P Set ContenidoPaquete_Axiliar = C.ContenidoPaquete 
	From #tmpProductos P 
	inner join INT_ND_CFG_ClavesSSA_ContenidoPaquete C On ( P.ClaveSSA = C.ClaveSSA and C.Status = 'A'  ) 
	---- Where ClaveSSA like '%060.345.0503%'	

	
------------------------------- Temporal de SubFarmacias  	
	Select C.IdEstado, C.IdSubFarmacia 
	Into #tmpSubFarmacias 
	From CatEstados_SubFarmacias C 
	Where C.IdEstado = @IdEstado 


	If @TipoDeInventario = 1  -- VENTA 
	Begin 
		Delete From #tmpSubFarmacias 

		Insert Into #tmpSubFarmacias ( IdEstado, IdSubFarmacia ) 
		Select IdEstado, IdSubFarmacia 
		From CatEstados_SubFarmacias C (NoLock) 
		Where C.IdEstado = @IdEstado and 
		Not Exists 
		( 
			Select * 
			From INT_ND_SubFarmaciasConsigna L (NoLock) 
			Where L.IdEstado = C.IdEstado and L.IdSubFarmacia = C.IdSubFarmacia  		
		) 
		
	End 

	If @TipoDeInventario = 2  -- CONSIGNACION  
	Begin 
		Delete From #tmpSubFarmacias 

		Insert Into #tmpSubFarmacias ( IdEstado, IdSubFarmacia ) 
		Select IdEstado, IdSubFarmacia 
		From CatEstados_SubFarmacias C (NoLock) 
		Where C.IdEstado = @IdEstado and 
		Exists 
		( 
			Select * 
			From INT_ND_SubFarmaciasConsigna L (NoLock) 
			Where L.IdEstado = C.IdEstado and L.IdSubFarmacia = C.IdSubFarmacia  		
		) 
		
	End 
	
------------------------------- Temporal de SubFarmacias  					

	------select * 
	------from INT_ND_CFG_CB_CuadrosBasicos 
	------Where ClaveSSA like '%060.345.0503%'		


---------------------- Intermedio 				 

---		spp_INT_ND_Unidad_GenerarRemisiones_Detallado  	

------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	
------------------------ Obtener las ventas 
	----If Exists ( Select * From tempdb..Sysobjects (NoLock) Where Name like '#INT_ND__tmpRemisiones%' and xType = 'U' ) 
	----   Drop Table tempdb..#INT_ND__tmpRemisiones  
	   
	----If Exists ( Select * From Sysobjects (NoLock) Where Name like '#INT_ND__tmpRemisiones%' and xType = 'U' ) 
	----   Drop Table #INT_ND__tmpRemisiones  	   
	   
	Select 
		@GUID as GUID, 
		@NombreEmpresa as Empresa, 
		IdEstado, @IdFarmacia as IdFarmacia, @NombreFarmacia as NombreFarmacia, 
		@CodigoCliente as CodigoCliente, '01' as Modulo, 
		FechaRemision, 
		cast(FechaRemision + ' 00:00:00.000' as datetime) as FechaRemision_Aux, 
		space(100) as IdAnexo, 
		space(500) as NombreAnexo, 			
		space(500) as NombrePrograma, 			
		0 as Prioridad, 
		Tipo,  
		space(50) as FolioRemision, 
		0 as EsCauses, 
		TipoDeClave, TipoDeClaveDescripcion, 
		ClaveSSA as ClaveSSA_Original,  
		ClaveSSA_Base, ClaveSSA as ClaveSSA,		
		DescripcionSal, 
		space(50) as ClaveSSA_ND, space(50) as ClaveSSA_Mascara, 
		space(7500) as Descripcion_Mascara, Cast('' As Varchar(20)) as Lote, 	
		sum(Cantidad) as Cantidad, 
		0 as ManejaIva, 
		TipoInsumo, 
		cast(0 as numeric(14,2)) as PrecioVenta, 
		cast(0 as numeric(14,2)) as PrecioServicio, 
		cast(0 as numeric(14,2)) as SubTotal, 
		cast(0 as numeric(14,2)) as Iva, 
		cast(0 as numeric(14,2)) as ImporteTotal, 				
		0 as Procesado, 
		EsEnResguardo as EnResguardo, 
		1 as Incluir, 
		0 as TipoRelacion, 
		IdSubFarmacia,  
		Keyx, 
		-- min(Keyx_GUID) as Keyx_GUID, 		
		0 as Keyx_Anexo   
	Into #INT_ND__tmpRemisiones 	
	From   
	(
		Select IdEmpresa, IdEstado, IdFarmacia, 
			GUID, Keyx, 
			TipoDispensacion as Tipo, EsCauses, ClaveSSA_Base, ClaveSSA, DescripcionSal, Cantidad_Proceso as Cantidad, 
			(case when TipoInsumo = 1 then 0 else 1 end ) as ManejaIva, EsEnResguardo, TipoInsumo, 
			FechaRegistro as FechaRemision, '' as ClaveSSA_ND, 
			'' as TipoDeClave, '' as TipoDeClaveDescripcion, '' as IdSubFarmacia          
		From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) --- INT_ND_RptAdmonDispensacion V (NoLock) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
			and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal and V.GUID = @GUID 
			-- and ClaveSSA like '%4165%' 
			-- and ClaveSSA = '060.550.0446' 
			-- and ClaveSSA = '060.034.0103' 
	) as T  
	Group by  
		IdEstado, IdFarmacia, FechaRemision, 
		TipoDeClave, TipoDeClaveDescripcion, 
		ClaveSSA_Base, ClaveSSA, DescripcionSal, ClaveSSA_ND, Tipo, TipoInsumo, EsEnResguardo, IdSubFarmacia, Keyx    
	
	
---	select count(*) from #INT_ND__tmpRemisiones 
	

--		spp_INT_ND_Unidad_GenerarRemisiones_Detallado 
	
	
--	select * from #INT_ND__tmpRemisiones 
----------------  Marcar las Claves CAUSES 
	Update D Set EsCauses = 1 
	From #INT_ND__tmpRemisiones D 
	Inner Join #tmp_ClavesSSA_Causes C On ( D.ClaveSSA = C.ClaveSSA ) 	
	
	
----------------  Excluir claves no licitdas / no determinado el origen 
	Update R Set Incluir = 0 
	From #INT_ND__tmpRemisiones R 
	Inner Join INT_ND_CFG_ClavesSSA_Excluir E On ( R.IdEstado = E.IdEstado and R.ClaveSSA_Original = E.ClaveSSA and E.Status = 'A' )   

	---Delete From #INT_ND__tmpRemisiones Where Incluir = 0   
	
		
		
	----------	select * from #INT_ND__tmpRemisiones  
		
		
	-------------------- Marcar la propiedad del Cliente 	
	------Update I Set EnResguardo = 1 
	------From #INT_ND__tmpRemisiones I (NoLock) 
	------Inner Join INT_ND_SubFarmaciasConsigna C (NoLock) 
	------	On ( I.IdEstado = C.IdEstado and I.IdSubFarmacia = C.IdSubFarmacia ) 		


	------select * 
	------From #INT_ND__tmpRemisiones I (NoLock) 
	------where EnResguardo = 1 


	-------------- Asignar Clave SSA Nadro 
	select  IdEstado, IdClaveSSA, ClaveSSA, ClaveSSA_ND, Status, 0 as Existe 
	into  #INT_ND_CFG_ClavesSSA  
	from INT_ND_CFG_ClavesSSA T  
	where ClaveSSA in 
	( 
		select ClaveSSA  -- , COUNT(*)  
		from INT_ND_CFG_ClavesSSA 
		group by ClaveSSA 
		having COUNT(*) > 1 
	) 
	
	Update E Set Existe = 1 	
	from #INT_ND_CFG_ClavesSSA E 
	inner join INT_ND_CFG_CB_CuadrosBasicos C On ( E.ClaveSSA_ND = C.ClaveSSA_ND ) 
	
	delete from #INT_ND_CFG_ClavesSSA where Existe = 0 
	
		
	------Update R Set ClaveSSA_ND = C.ClaveSSA_ND 
	------From #INT_ND__tmpRemisiones R 
	------Inner Join #INT_ND_CFG_ClavesSSA C On ( R.ClaveSSA = C.ClaveSSA ) 
	
	Update R Set ClaveSSA_ND = C.ClaveSSA_ND 
	From #INT_ND__tmpRemisiones R 
	Inner Join INT_ND_CFG_ClavesSSA C On ( R.ClaveSSA = C.ClaveSSA ) 	
	-------------- Asignar Clave SSA Nadro 
	
	
	
--	select * from #INT_ND__tmpRemisiones  	
	
--		spp_INT_ND_Unidad_GenerarRemisiones_Detallado 		

	------select C.* 
	------From #INT_ND__tmpRemisiones R 
	------Inner Join INT_ND_CFG_ClavesSSA C On ( R.ClaveSSA = C.ClaveSSA ) 
	
		

	-------------- Validar que anexos se ejecutaran 
	Update A Set TieneDatosAProcesar = 1 
	From #tmpAnexos A 
	Inner Join #tmpAnexos_Detalles D On ( A.IdAnexo = D.IdAnexo  ) 
	Inner Join #INT_ND__tmpRemisiones R On ( R.ClaveSSA = D.ClaveSSA Or R.ClaveSSA_ND = D.ClaveSSA_ND ) 
	
	
	-- Select count(*) From #tmpAnexos 		
	Delete From #tmpAnexos Where TieneDatosAProcesar = 0 	
	-- Select count(*) From #tmpAnexos 
	
	
	

	--	select * from #INT_ND__tmpRemisiones  		
		
	------- Revisar si existen datos para la fecha seleccionada 	
	Select @Existen_Datos = count(*) from #INT_ND__tmpRemisiones   
	
	-- Select * From #tmpAnexos 
	

------------------------ Obtener las ventas 	

------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	



---		spp_INT_ND_Unidad_GenerarRemisiones_Detallado  	
	
--	select * from #INT_ND__tmpRemisiones 
--	select * from #tmpAnexos 	
--	select * from #tmpAnexos_Detalles 	
	
	
Declare 
	@sIdAnexo varchar(100),  	
	@sClaveSSA varchar(50), 
	@sClaveSSA_Auxiliar varchar(50), 		
	@sClaveSSA_ND varchar(50), 
	@sClaveSSA_Mascara varchar(50), 
	@sDescripcion_Mascara varchar(max), 			
	@iKeyx_Anexo int 
	
	Set @sIdAnexo = '' 
	Set @sClaveSSA = '' 
	Set @sClaveSSA_Auxiliar = '' 	
	Set @sClaveSSA_ND = '' 
	Set @sClaveSSA_Mascara = ''
	Set @sDescripcion_Mascara = '' 
	Set @iKeyx_Anexo = 0 


	---------------------------------------------------------------------------------------------------------------- 
	--------------------------------------------- ANEXOS ASOCIADOS		
	Declare #cursorAnexos  
	Cursor For 
		Select IdAnexo 
		From #tmpAnexos 
		Where @Existen_Datos > 0 
		Order By Prioridad 
	Open #cursorAnexos 
	FETCH NEXT FROM #cursorAnexos Into @sIdAnexo
		WHILE @@FETCH_STATUS = 0 and @Existen_Datos > 0 
		BEGIN 
		
			---		spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion  		
			Print @sIdAnexo 
			
			
			---------------------------------------------------------------------------------------------------------------- 
			Declare #cursorClaves	
			Cursor For 
				Select 
					V.ClaveSSA, D.Keyx   
				From #tmpAnexos_Detalles D (NoLock) 
				Inner Join #INT_ND__tmpRemisiones V (NoLock) 
					On ( ( D.ClaveSSA = V.ClaveSSA ) or ( D.ClaveSSA_ND = V.ClaveSSA_ND ) ) 
				Where D.IdAnexo = @sIdAnexo and V.Procesado = 0 
				Order By D.ClaveSSA_Mascara 
			Open #cursorClaves 
			FETCH NEXT FROM #cursorClaves Into @sClaveSSA, @iKeyx_Anexo 
				WHILE @@FETCH_STATUS = 0 
				BEGIN 
					
					-- Print @sClaveSSA 			
					--- PROCESAMIENTO A NIVEL REGISTRO 
					Update V Set Procesado = 1, Keyx_Anexo = @iKeyx_Anexo, TipoRelacion = 1  
					From #INT_ND__tmpRemisiones V (NoLock) 
					Where V.ClaveSSA = @sClaveSSA and V.Procesado = 0 
					
					Update V Set Procesado = 1 
					From #INT_ND__tmpRemisiones V (NoLock) 
					Where V.ClaveSSA = @sClaveSSA and V.Procesado = 0 					
					
					
					FETCH NEXT FROM #cursorClaves Into @sClaveSSA, @iKeyx_Anexo 
				END	 
			Close #cursorClaves 
			Deallocate #cursorClaves 
			
			Select @Existen_Datos = count(*) from #INT_ND__tmpRemisiones Where Procesado = 0 
			---------------------------------------------------------------------------------------------------------------- 
			
			
			FETCH NEXT FROM #cursorAnexos Into @sIdAnexo  
		END	 
	Close #cursorAnexos 
	Deallocate #cursorAnexos 		
	--------------------------------------------- ANEXOS ASOCIADOS	
	
	
	--------------------------------------------- ANEXOS NO ASOCIADOS	
	Declare #cursorClaves_NoAsociadas  
	Cursor For 
		Select ClaveSSA_ND   
		From #INT_ND__tmpRemisiones V 
		Where V.Procesado = 0 
	Open #cursorClaves_NoAsociadas 
	FETCH NEXT FROM #cursorClaves_NoAsociadas Into @sClaveSSA_Auxiliar
		WHILE @@FETCH_STATUS = 0 and @Existen_Datos > 0   
		BEGIN 
					
			---------------------------------------------------------------------------------------------------------------- 
			Declare #cursorClaves_02  
			Cursor For 
				Select 
					V.ClaveSSA, D.Keyx   
				From #tmpAnexos_Detalles D (NoLock) 
				Inner Join #INT_ND__tmpRemisiones V (NoLock) 
					On ( 
						( D.ClaveSSA = V.ClaveSSA ) 
						or 
						( D.ClaveSSA_ND = V.ClaveSSA_ND ) 
						) 
				Where D.ClaveSSA_ND = @sClaveSSA_Auxiliar and V.Procesado = 0
				Order By D.Prioridad -- D.ClaveSSA_Mascara 
			Open #cursorClaves_02 
			FETCH NEXT FROM #cursorClaves_02 Into @sClaveSSA, @iKeyx_Anexo 
				WHILE @@FETCH_STATUS = 0 
				BEGIN  
					--- PROCESAMIENTO A NIVEL REGISTRO 
					Update V Set Procesado = 1, Keyx_Anexo = @iKeyx_Anexo, TipoRelacion = 2 
					From #INT_ND__tmpRemisiones V (NoLock) 
					Where V.ClaveSSA = @sClaveSSA and V.Procesado = 0 

					----Update V Set Procesado = 1 
					----From #INT_ND__tmpRemisiones V (NoLock) 
					----Where V.ClaveSSA = @sClaveSSA and V.Procesado = 0 
					
					
					FETCH NEXT FROM #cursorClaves_02 Into @sClaveSSA, @iKeyx_Anexo 
				END	 
			Close #cursorClaves_02 
			Deallocate #cursorClaves_02 
			
			Select @Existen_Datos = count(*) from #INT_ND__tmpRemisiones Where Procesado = 0 
			---------------------------------------------------------------------------------------------------------------- 
			
			
			FETCH NEXT FROM #cursorClaves_NoAsociadas Into @sClaveSSA_Auxiliar  
		END	 
	Close #cursorClaves_NoAsociadas 
	Deallocate #cursorClaves_NoAsociadas 	
	--------------------------------------------- ANEXOS NO ASOCIADOS				
	---------------------------------------------------------------------------------------------------------------- 
	

--	select * from #INT_ND__tmpRemisiones 


--------------------------------------------- ASIGNAR INFORMACION DEL ANEXO Y GENERAR FOLIO DE REMISION 
----------------- En los anexos marcados como CAUSES el material de curación se marca por defecto 
----------------  Marcar las Claves CAUSES 
	----Update D Set EsCauses = 1 
	----From #INT_ND__tmpRemisiones D 
	----Inner Join #tmp_ClavesSSA_Causes C On ( D.ClaveSSA = C.ClaveSSA ) 	

	Update V Set IdAnexo = D.IdAnexo 
	From #INT_ND__tmpRemisiones V (NoLock) 
	Inner Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 		

	
	Update I Set EsCauses = 1 
	From #INT_ND__tmpRemisiones I (NoLock) 	
	Inner Join 	INT_ND_CFG_CB_Anexos_Causes C (NoLock) On ( I.IdEstado = C.IdEstado and I.IdAnexo = C.IdAnexo ) 
	Where TipoInsumo = 2 --- MATERIAL DE CURACION   

--		spp_INT_ND_Unidad_GenerarRemisiones_Detallado

----------------- Separar en base al causes 
	If @SepararCauses = 1 
		Begin 
			Update D Set Tipo = ltrim(rtrim(Tipo)) + 'N' 
			From #INT_ND__tmpRemisiones D 
			Where EsCauses = 0 
		End 
	Else 
		Begin 
			Update D Set EsCauses = 1 
			From #INT_ND__tmpRemisiones D 		
		End 
----------------- Separar en base al causes 		
	
----------------- En los anexos marcados como CAUSES el material de curación se marca por defecto 

	----Select D.* , V.* 
	----From #INT_ND__tmpRemisiones V (NoLock) 
	----left Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 
	

	Update V Set 
		IdAnexo = D.IdAnexo, NombreAnexo = D.NombreAnexo, Prioridad = D.Prioridad, 
		NombrePrograma = D.NombrePrograma, 
		ClaveSSA_ND = D.ClaveSSA_ND, 
		ClaveSSA_Mascara = (case when D.ClaveSSA_Mascara <> '' Then D.ClaveSSA_Mascara Else D.ClaveSSA_ND End), 
		Descripcion_Mascara = D.Descripcion_Mascara, 
		ManejaIva = D.ManejaIva, 
		PrecioVenta = D.PrecioVenta, PrecioServicio = D.PrecioServicio, 
		-- FolioRemision = D.IdAnexo + '/' + Modulo + '/' + @sFolioRemision + '/' + Tipo, 
		FolioRemision = 
			D.IdAnexo + '/' + V.Modulo + '/' + 
			right('0000' + cast(day(FechaRemision) as varchar), 2) + 
			right('0000' + cast(month(FechaRemision) as varchar), 2) + 
			right('0000' + cast(year(FechaRemision) as varchar), 2) + 						
			+ '/' + Tipo, 		
		SubTotal = (D.PrecioVenta * V.Cantidad), 
		----Iva = (case when D.ManejaIva = 0 then 0 else (D.PrecioVenta * V.Cantidad) * ( 16 / 100.00 ) end),  
		----ImporteTotal = (case when D.ManejaIva = 0 then (D.PrecioVenta * V.Cantidad) else (D.PrecioVenta * V.Cantidad) * ( 1 + ( 16 / 100.00) ) end) 		
		Iva = (case when D.ManejaIva = 0 then 0 else round((D.PrecioVenta * Cantidad) * ( 16 / 100.00 ), 2, 1) end),  
		ImporteTotal = (case when D.ManejaIva = 0 then (D.PrecioVenta * Cantidad) 
			else round((D.PrecioVenta * Cantidad) * ( 1 + ( 16 / 100.00) ), 2, 1) end), 
		Lote = D.Lote 
	From #INT_ND__tmpRemisiones V (NoLock) 
	Inner Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 
	
	Update I Set PrecioVenta = 0, SubTotal = 0, Iva = 0, ImporteTotal = 0 
	From #INT_ND__tmpRemisiones I (NoLock) 	
	Where EnResguardo = 1 
	
	Update D Set Procesado = 1, IdAnexo = '0', NombreAnexo = 'NO ESPECIFICADO', Prioridad = 0, FolioRemision = '__NO_ESPECIFICADO'
	From #INT_ND__tmpRemisiones D (NoLock) 
	Where D.Procesado = 0 		
	
		
-------------------------------- Aplicar cambio para remisiones mixtas ( Medicamento y Material de Curacion 
	----Update R Set 
	----	FolioRemision = 
	----		IdAnexo + '/' + (case when cast(TipoInsumo as int) = 1 Then '01' Else '02' end) + '/' + 
	----		right('0000' + cast(day(FechaRemision) as varchar), 2) + 
	----		right('0000' + cast(month(FechaRemision) as varchar), 2) + 
	----		right('0000' + cast(year(FechaRemision) as varchar), 2) + 						
	----		+ '/' + Tipo 
	----From #INT_ND__tmpRemisiones R (NoLock) 
	----Inner Join INT_ND_CFG_Remisiones_Separar L (NoLock) 
	----	On ( R.IdEstado = L.IdEstado and R.IdFarmacia = L.IdFarmacia and R.FolioRemision = L.FolioRemision ) 	
	----Where IdAnexo <> '0'


	Update R Set 
		FolioRemision = 
			IdAnexo + '/' + (case when cast(TipoInsumo as int) = 1 Then '01' Else '02' end) + '/' + 
			right('0000' + cast(day(FechaRemision) as varchar), 2) + 
			right('0000' + cast(month(FechaRemision) as varchar), 2) + 
			right('0000' + cast(year(FechaRemision) as varchar), 2) + 						
			+ '/' + Tipo 
	From #INT_ND__tmpRemisiones R (NoLock) 
	Where IdAnexo <> '0' -- and FechaRemision >= '2015-03-01' 
-------------------------------- Aplicar cambio para remisiones mixtas ( Medicamento y Material de Curacion 		
--------------------------------------------- ASIGNAR INFORMACION DEL ANEXO Y GENERAR FOLIO DE REMISION 


--------------------------------------------- Cambiar Folio Remisión 
	If (@IdEstado = '07') 
	Begin 
		
		Update D 
		Set FolioRemision = (case When IdAnexo = 'CAUSES' Then '60' Else (Case When IdAnexo = 'NO CAUSES' Then '61' Else '62' End) End) +
							+ '-' + IdFarmacia + '-' + Replace(D.FechaRemision, '-', '') + '-' + D.Tipo
		From #INT_ND__tmpRemisiones D (NoLock) 
		
		----select * 
		----from #INT_ND__tmpRemisiones 
		
	End 	
--------------------------------------------- Cambiar Folio Remisión 


	----select * 
	----From #INT_ND__tmpRemisiones V (NoLock) 
	----Inner Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 


---		spp_INT_ND_Unidad_GenerarRemisiones_Detallado  	

--	select * from #INT_ND__tmpRemisiones 






----		spp_INT_ND_Unidad_GenerarRemisiones_Detallado

	------select * from #INT_ND__tmpRemisiones 	

	------select count(*) 
	------From #INT_ND__tmpRemisiones V (NoLock) 
	------Inner Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 



-------------------------------------------------- SALIDA FINAL 

/* 
---	Limpiar datos 	
	Update V Set 
		NombrePrograma = '', IdAnexo = '', NombreAnexo = '', Prioridad = '', 
		ClaveSSA_ND = '', ClaveSSA_Mascara = '', Descripcion_Mascara = '', 
		ManejaIva = 0, PrecioVenta = 0, PrecioServicio = 0, FolioRemision = '', 
		SubTotal = 0, Iva = 0,  ImporteTotal = 0 
	From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) 
	
*/ 	
	
	
	----select distinct ClaveSSA, DescripcionSal, ClaveSSA_Mascara, Descripcion_Mascara  
	----from #INT_ND__tmpRemisiones 
	----where ClaveSSA not like '%' + ClaveSSA_Mascara + '%' 	
	
	
	Update V Set 
		NombrePrograma = D.NombrePrograma, 
		IdAnexo = D.IdAnexo, NombreAnexo = D.NombreAnexo, Prioridad = D.Prioridad,  
		ClaveSSA_ND = D.ClaveSSA_ND, 
		ClaveSSA_Mascara = (case when D.ClaveSSA_Mascara <> '' Then D.ClaveSSA_Mascara Else D.ClaveSSA_ND End), 
		Descripcion_Mascara = D.Descripcion_Mascara, 
		ManejaIva = D.ManejaIva, 
		PrecioVenta = D.PrecioVenta, PrecioServicio = D.PrecioServicio, 
		-- FolioRemision = D.IdAnexo + '/' + Modulo + '/' + @sFolioRemision + '/' + Tipo, 
		FolioRemision = 	D.folioRemision, 
		SubTotal = (D.PrecioVenta * D.Cantidad), 
		Iva = (case when D.ManejaIva = 0 then 0 else round((D.PrecioVenta * D.Cantidad) * ( 16 / 100.00 ), 2, 1) end),  
		ImporteTotal = (case when D.ManejaIva = 0 then (D.PrecioVenta * D.Cantidad) 
			else round((D.PrecioVenta * D.Cantidad) * ( 1 + ( 16 / 100.00) ), 2, 1) end), 
		Incluir = D.Incluir, TipoRelacion = D.TipoRelacion, Lote = D.Lote 	
	From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) 
	Inner Join #INT_ND__tmpRemisiones D (NoLock) On ( V.Keyx = D.Keyx and V.GUID = V.GUID ) 	
	Where D.Procesado = 1 	


	
	----select count(*) 
	----From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) 
	----Inner Join #INT_ND__tmpRemisiones D (NoLock) On ( V.Keyx = D.Keyx and V.GUID = V.GUID ) 		
	
------------ SALIDA FINAL DEL PROCESO 	
-------------------------------------------------- SALIDA FINAL  
	
---		spp_INT_ND_Unidad_GenerarRemisiones_Detallado  	


--		select * from #INT_ND__tmpRemisiones 
	
End  
Go--#SQL 

