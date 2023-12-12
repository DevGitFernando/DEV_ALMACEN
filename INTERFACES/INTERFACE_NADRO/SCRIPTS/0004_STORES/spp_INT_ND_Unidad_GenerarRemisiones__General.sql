------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_Unidad_GenerarRemisiones___General' and xType = 'P') 
    Drop Proc spp_INT_ND_Unidad_GenerarRemisiones___General
Go--#SQL 
  
--		ExCB spp_INT_ND_Unidad_GenerarRemisiones___General '001', '11', '2181002', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_Unidad_GenerarRemisiones___General 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @CodigoCliente varchar(20) = '2181428', -- '2179105', 
    @FechaDeProceso varchar(10) = '2014-09-23', @GUID varchar(100) = 'DB45979F-0B76-43EF-8104-A36EF3C8C72A'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

Declare 
	@IdFarmacia varchar(4), 
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
	Set @dFechaRemision = cast(@FechaDeProceso as datetime) 
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
	
	Select @IdFarmacia = IdFarmacia, @NombreFarmacia =  NombreCliente 
	From #tmpClientes F 
	Where CodigoCliente = @CodigoCliente 	
	
	
	------ En el caso de utilizar mas de una vez el Codigo Cliente 
	Select @IdFarmacia = IdFarmacia, @NombreFarmacia =  NombreCliente 
	From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado 
			and convert(varchar(10), V.FechaRegistro, 120) = @FechaDeProceso and V.GUID = @GUID 			
	
	
	Select @NombreEmpresa = Nombre 
	From CatEmpresas (NoLock) 
	Where IdEmpresa =  @IdEmpresa 
	
	
	
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
	-- Where C.ClaveSSA_ND like '%9980000231%'	
	Order by Prioridad, C.Descripcion_Mascara   
--------------------------------------------------- Determinar los anexos de la Unidad 	



------------------------------- Temporal de Productos 
	Select *, ContenidoPaquete as ContenidoPaquete_Axiliar 
	Into #tmpProductos 
	From vw_Productos_CodigoEAN 

	Update P Set ContenidoPaquete_Axiliar = C.ContenidoPaquete 
	From #tmpProductos P 
	inner join INT_ND_CFG_ClavesSSA_ContenidoPaquete C On ( P.ClaveSSA = C.ClaveSSA and C.Status = 'A'  ) 



---------------------- Intermedio 				 

---		spp_INT_ND_Unidad_GenerarRemisiones___General  	

------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	
------------------------ Obtener las ventas 
	If Exists ( Select * From tempdb..Sysobjects (NoLock) Where Name like '#INT_ND__tmpRemisiones%' and xType = 'U' ) 
	   Drop Table tempdb..#INT_ND__tmpRemisiones  
	   
	Select 
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, 
		T.GUID as GUID, 
		@CodigoCliente as CodigoCliente, '01' as Modulo, 
		-- replace(convert(varchar(10), @FechaDeProceso, 120), '-', '') as FechaRemision,  	
		@FechaDeProceso as FechaRemision, 
		space(100) as IdAnexo, 
		space(200) as NombreAnexo, 	
		space(200) as NombrePrograma, 
		0 as Prioridad, 
		Tipo,  
		space(50) as FolioRemision, 		
		ClaveSSA_Base, ClaveSSA as ClaveSSA,		
		space(50) as ClaveSSA_ND, space(50) as ClaveSSA_Mascara, 
		space(7000) as Descripcion_Mascara, 	
		Cantidad, EsEnResguardo, 
		ManejaIva, 
		cast(0 as numeric(14,2)) as PrecioVenta, cast(0 as numeric(14,2)) as PrecioServicio, 
		cast(0 as numeric(14,2)) as SubTotal, cast(0 as numeric(14,2)) as Iva, 
		cast(0 as numeric(14,2)) as ImporteTotal, 		
		0 as Procesado,  	
		0 as EnResguardo, 
		1 as Incluir, 
		space(4) as IdSubFarmacia, 			
		Keyx, 0 as Keyx_Anexo   
	Into #INT_ND__tmpRemisiones 	
	From 
	(
		Select IdEmpresa, IdEstado, IdFarmacia, 
			GUID, Keyx,  
			TipoDispensacion as Tipo, ClaveSSA_Base, ClaveSSA, Cantidad, 
			(case when TipoInsumo = 1 then 0 else 1 end ) as ManejaIva, EsEnResguardo   
		From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
			and convert(varchar(10), V.FechaRegistro, 120) = @FechaDeProceso and V.GUID = @GUID 
			-- and V.ClaveSSA like '%9008%' 
		-- Group By TipoDispensacion, ClaveSSA_Base, ClaveSSA  	
	) as T
	--	Group by  ClaveSSA_Base, ClaveSSA, Tipo  
		
	-------------- Marcar la propiedad del Cliente 			
	Update I Set EsEnResguardo = 1 
	From #INT_ND__tmpRemisiones I (NoLock) 
	Inner Join INT_ND_SubFarmaciasConsigna C (NoLock) On ( I.IdEstado = C.IdEstado and I.IdSubFarmacia = C.IdSubFarmacia ) 		
		
		
	--------------- Excluir claves no licitdas / no determinado el origen 
	Update R Set Incluir = 0 
	From #INT_ND__tmpRemisiones R 
	Inner Join INT_ND_CFG_ClavesSSA_Excluir E 
		On ( R.IdEstado = E.IdEstado and R.ClaveSSA = E.ClaveSSA and E.Status = 'A' )   
		-- On ( R.IdEstado = E.IdEstado and R.ClaveSSA = E.ClaveSSA )   
			
	Delete From #INT_ND__tmpRemisiones Where Incluir = 0 and EsEnResguardo = 0   		
		


	-------------- Asignar Clave SSA Nadro 
	Update R Set ClaveSSA_ND = C.ClaveSSA_ND 
	From #INT_ND__tmpRemisiones R 
	Inner Join INT_ND_CFG_ClavesSSA C On ( R.ClaveSSA = C.ClaveSSA ) 
	

	-------------- Validar que anexos se ejecutaran 
	Update A Set TieneDatosAProcesar = 1 
	From #tmpAnexos A 
	Inner Join #tmpAnexos_Detalles D On ( A.IdAnexo = D.IdAnexo  ) 
	Inner Join #INT_ND__tmpRemisiones R On ( R.ClaveSSA = D.ClaveSSA Or R.ClaveSSA_ND = D.ClaveSSA_ND ) 
	
	----Select * 
	----From #tmpAnexos A 
	----Inner Join #tmpAnexos_Detalles D On ( A.IdAnexo = D.IdAnexo  ) 
	----Inner Join #INT_ND__tmpRemisiones R On ( R.ClaveSSA = D.ClaveSSA Or R.ClaveSSA_ND = D.ClaveSSA_ND ) 	
	
	-- Select count(*) From #tmpAnexos 		
	Delete From #tmpAnexos Where TieneDatosAProcesar = 0 	
	-- Select count(*) From #tmpAnexos 
	
			
	------- Revisar si existen datos para la fecha seleccionada 	
	Select @Existen_Datos = count(*) from #INT_ND__tmpRemisiones 
	
	-- Select * From #tmpAnexos 
	
	
-----	QUITAR	
	-- select * from #INT_ND__tmpRemisiones 
	

	------Select count(*) as Anexos from #tmpAnexos 
	------Select count(*) as Anexos_Detalles from #tmpAnexos_Detalles 
	------Select count(*) as Remisiones from #INT_ND__tmpRemisiones 	

------------------------ Obtener las ventas 	

------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	


   
---		spp_INT_ND_Unidad_GenerarRemisiones___General  	
	
--	select * from #INT_ND__tmpRemisiones 
--	select * from #tmpAnexos 	
--	select * from #tmpAnexos_Detalles 	
	
 	
Declare 
	@sIdAnexo varchar(100),  	
	@sClaveSSA varchar(50), 
	@sClaveSSA_ND varchar(50), 
	@sClaveSSA_Mascara varchar(50), 
	@sDescripcion_Mascara varchar(max), 			
	@iKeyx_Anexo int 
	
	Set @sIdAnexo = '' 
	Set @sClaveSSA = '' 
	Set @sClaveSSA_ND = '' 
	Set @sClaveSSA_Mascara = ''
	Set @sDescripcion_Mascara = '' 
	Set @iKeyx_Anexo = 0 


------------------------------------------------------------------ 
	----Select '' as Cruze, V.*, D.* 
	----From #INT_ND__tmpRemisiones D (NoLock) 
	----Left Join  #tmpAnexos_Detalles V (NoLock) 
	----	On ( D.ClaveSSA = V.ClaveSSA and D.Procesado = 0 ) 
					
--	select * from #INT_ND__tmpRemisiones 	
--	select * from #tmpAnexos_Detalles 
	
	---------------------------------------------------------------------------------------------------------------- 
	Declare #cursorAnexos  
	Cursor For 
		Select IdAnexo 
		From #tmpAnexos 
		Where @Existen_Datos > 0 -- and 1 = 0 
		Order By Prioridad 
	Open #cursorAnexos 
	FETCH NEXT FROM #cursorAnexos Into @sIdAnexo
		WHILE @@FETCH_STATUS = 0 and @Existen_Datos > 0 
		BEGIN 
		
			---		spp_INT_ND_Unidad_GenerarRemisiones___General  
			-- Print 'Anexo   ' + @sIdAnexo 
			
			---------------------------------------------------------------------------------------------------------------- 
			Declare #cursorClaves  
			Cursor For 
				Select 
					V.ClaveSSA, D.Keyx   
				From #tmpAnexos_Detalles D (NoLock) 
				Inner Join #INT_ND__tmpRemisiones V (NoLock) 
					-- On ( D.ClaveSSA = V.ClaveSSA and V.Procesado = 0 ) 
					On ( ( D.ClaveSSA = V.ClaveSSA ) or ( D.ClaveSSA_ND = V.ClaveSSA_ND ) ) 				
				Where D.IdAnexo = @sIdAnexo and V.Procesado = 0  
				Order By D.ClaveSSA_Mascara 
			Open #cursorClaves 
			FETCH NEXT FROM #cursorClaves Into @sClaveSSA, @iKeyx_Anexo 
				WHILE @@FETCH_STATUS = 0 
				BEGIN 
					-- Print '			Clave SSA   ' + @sClaveSSA   
								
					--- PROCESAMIENTO A NIVEL REGISTRO 
					Update V Set Procesado = 1, Keyx_Anexo = @iKeyx_Anexo 
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
	---------------------------------------------------------------------------------------------------------------- 
   
--	select * from #INT_ND__tmpRemisiones 	


--------------------------------------------- ASIGNAR INFORMACION DEL ANEXO Y GENERAR FOLIO DE REMISION 
---		spp_INT_ND_Unidad_GenerarRemisiones___General  	

	Update V Set 	
		IdAnexo = D.IdAnexo, NombreAnexo = D.NombreAnexo, Prioridad = D.Prioridad, 
		NombrePrograma = D.NombrePrograma, 
		ClaveSSA_ND = D.ClaveSSA_ND, 
		--ClaveSSA_Mascara = (case when D.ClaveSSA_Mascara <> '' Then D.ClaveSSA_Mascara Else D.ClaveSSA_ND End), 
		ClaveSSA_Mascara = D.ClaveSSA_Mascara, 
		Descripcion_Mascara = D.Descripcion_Mascara, 
		---- PrecioVenta = D.PrecioVenta, PrecioServicio = D.PrecioServicio, 
		FolioRemision = D.IdAnexo + '/' + Modulo + '/' + @sFolioRemision + '/' + Tipo  
		---- SubTotal = (D.PrecioVenta * Cantidad), 
		---- Iva = (case when V.ManejaIva = 0 then 0 else (D.PrecioVenta * Cantidad) * ( 16 / 100.00 ) end),  
		---- ImporteTotal = (case when V.ManejaIva = 0 then (D.PrecioVenta * Cantidad) else (D.PrecioVenta * Cantidad) * ( 1 + ( 16 / 100.00) ) end) 		
	From #INT_ND__tmpRemisiones V (NoLock) 
	Inner Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 


	Update V Set 
		PrecioVenta = D.PrecioVenta, PrecioServicio = D.PrecioServicio, 
		SubTotal = (D.PrecioVenta * Cantidad), 
		Iva = (case when V.ManejaIva = 0 then 0 else (D.PrecioVenta * Cantidad) * ( 16 / 100.00 ) end),  
		ImporteTotal = (case when V.ManejaIva = 0 then (D.PrecioVenta * Cantidad) else (D.PrecioVenta * Cantidad) * ( 1 + ( 16 / 100.00) ) end) 		
	From #INT_ND__tmpRemisiones V (NoLock) 
	Inner Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 
	where V.EsEnResguardo = 0 
		
	
	Update D Set Procesado = 1, IdAnexo = '0', NombreAnexo = 'NO ESPECIFICADO', Prioridad = 0, FolioRemision = '__NO_ESPECIFICADO'
	From #INT_ND__tmpRemisiones D (NoLock) 
	Where D.Procesado = 0 		
	
	
	Update D Set 
		 Keyx_Anexo = R.Keyx_Anexo, 
		 CodigoCliente = @CodigoCliente,  
		 Procesado = 1, IdAnexo = R.IdAnexo, NombreAnexo = R.NombreAnexo, Prioridad = R.Prioridad, NombrePrograma = R.NombrePrograma, 
		 ClaveSSA_ND = R.ClaveSSA_ND, ClaveSSA_Mascara = R.ClaveSSA_Mascara, Descripcion_Mascara = D.Descripcion_Mascara, 
		 ManejaIva = R.ManejaIva, PrecioVenta = R.PrecioVenta, PrecioServicio = R.PrecioServicio, 
		 FolioRemision = R.FolioRemision, SubTotal = R.SubTotal, Iva = R.Iva, ImporteTotal = R.ImporteTotal, 
		 FechaGeneracion = replace(convert(varchar(10), @FechaDeProceso, 120), '-', '')    
	From INT_ND_RptAdmonDispensacion_Detallado__General D (NoLock) 
	Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx and D.GUID = @GUID ) 
	Where R.Procesado = 1 
	

	------Update D Set Procesado = 1, IdAnexo = R.IdAnexo, NombreAnexo = R.NombreAnexo, Prioridad = R.Prioridad, NombrePrograma = R.NombrePrograma, 
	------	 ClaveSSA_ND = R.ClaveSSA_ND, ClaveSSA_Mascara = R.ClaveSSA_Mascara, Descripcion_Mascara = D.Descripcion_Mascara, 
	------	 ManejaIva = R.ManejaIva, PrecioVenta = R.PrecioVenta, PrecioServicio = R.PrecioServicio, 
	------	 FolioRemision = R.FolioRemision, SubTotal = R.SubTotal, Iva = R.Iva, ImporteTotal = R.ImporteTotal, 
	------	 FechaGeneracion = replace(convert(varchar(10), @FechaDeProceso, 120), '-', '')    
	------From INT_ND_RptAdmonDispensacion_Detallado__General D (NoLock) 
	------Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx_GUID = R.Keyx and D.GUID = @GUID ) 
	------Where R.Procesado = 1 
--------------------------------------------- ASIGNAR INFORMACION DEL ANEXO Y GENERAR FOLIO DE REMISION 



	
	
---------------------------------------------------------------- SALIDA FINAL  	
------	Select  
------		CodigoCliente, Prioridad, IdAnexo, NombreAnexo, Tipo, FolioRemision 
------	From #INT_ND__tmpRemisiones 
------	Group by CodigoCliente, Prioridad, IdAnexo, NombreAnexo, Tipo, FolioRemision 
------	Order by Prioridad, IdAnexo 


------	Select D.* 	
------	From INT_ND_RptAdmonDispensacion D (NoLock) 
------	Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx ) 	 
---------------------------------------------------------------- SALIDA FINAL  
	
---		spp_INT_ND_Unidad_GenerarRemisiones___General  	

	
End  
Go--#SQL 

