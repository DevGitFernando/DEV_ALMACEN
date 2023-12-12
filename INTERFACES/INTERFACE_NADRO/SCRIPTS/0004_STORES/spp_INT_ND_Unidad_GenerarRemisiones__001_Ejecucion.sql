------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion' and xType = 'P' ) 
    Drop Proc spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion
Go--#SQL 
  
/* 
	Exec spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion  
		@IdEmpresa = '003', @IdEstado = '16', @CodigoCliente = '2181428', 
		@FechaDeProceso = '2014-12-12', @GUID = '38bd84e6-0756-4c7f-ae22-4469c1efd0f4' 

*/   
  
Create Proc spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @CodigoCliente varchar(20) = '2180987', -- '2179105', 
    @FechaDeProceso varchar(10) = '2015-07-01', @GUID varchar(100) = 'c5cf57ab-03f8-4de6-a92b-ec3661d5e566', 
    @Año_Causes int = 2012, @SepararCauses int = 0 --- , @TipoFolioRemision Int = 1
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
	@sFolioRemision varchar(100), 
	@sPrefijo_NoEspecificado varchar(100)    
	
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
	Set @sPrefijo_NoEspecificado = '' 

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
	
----------------------------------- Detalles de anexo  
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
	


------------------------------- Temporal de Productos 
	Select *, ContenidoPaquete as ContenidoPaquete_Axiliar  
	Into #tmpProductos 
	From vw_Productos_CodigoEAN 
	
	Update P Set ContenidoPaquete_Axiliar = C.ContenidoPaquete 
	From #tmpProductos P 
	inner join INT_ND_CFG_ClavesSSA_ContenidoPaquete C On ( P.ClaveSSA = C.ClaveSSA and C.Status = 'A'  ) 
	---- Where ClaveSSA like '%060.345.0503%'	


---------------------- Intermedio 				 

---		spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion  	

------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	
------------------------ Obtener las ventas 
	----If Exists ( Select * From tempdb..Sysobjects (NoLock) Where Name like '#INT_ND__tmpRemisiones%' and xType = 'U' ) 
	   ----Drop Table tempdb..#INT_ND__tmpRemisiones  
	   
	Select 
		T.GUID as GUID, 	
		T.IdEmpresa, 
		T.IdEstado, T.IdFarmacia, 
		@CodigoCliente as CodigoCliente, '01' as Modulo, 
		-- replace(convert(varchar(10), @FechaDeProceso, 120), '-', '') as FechaRemision,  	
		@FechaDeProceso as FechaRemision, 
		Cast('' As Varchar(100)) as IdAnexo, 
		Cast('' As Varchar(200)) as NombreAnexo, 	
		Cast('' As Varchar(200)) as NombrePrograma, 
		0 as Prioridad, 
		Tipo,  
		Cast('' As Varchar(200)) as FolioRemision, 
		Cast('' As Varchar(200)) as FolioRemision_Auxiliar,  
		
		EsCauses, 
		ClaveSSA_Base, ClaveSSA as ClaveSSA, 
		Cast('' As Varchar(20)) as ClaveSSA_ND, Cast('' As Varchar(20)) as ClaveSSA_Mascara, 
		Cast('' As Varchar(7500)) as Descripcion_Mascara, Cast('' As Varchar(20)) as Lote,	
		Cantidad, EsEnResguardo, 
		0 as ManejaIva, 
		TipoInsumo, 
		cast(0 as numeric(14,2)) as PrecioVenta, cast(0 as numeric(14,2)) as PrecioServicio, 
		cast(0 as numeric(14,2)) as SubTotal, 
		cast(0 as numeric(14,2)) as Iva, 
		cast(0 as numeric(14,2)) as ImporteTotal, 		
		0 as Procesado, 
		0 as EnResguardo, 
		1 as Incluir, 
		0 as TipoRelacion, 
		space(4) as IdSubFarmacia, 
		Keyx, 0 as Keyx_Anexo   
	Into #INT_ND__tmpRemisiones 	
	From   
	(
		Select IdEmpresa, IdEstado, IdFarmacia, 
			GUID, Keyx,  
			TipoDispensacion as Tipo, EsCauses, ClaveSSA_Base, ClaveSSA, Cantidad, 
			(case when TipoInsumo = 1 then 0 else 1 end ) as ManejaIva, EsEnResguardo, TipoInsumo   
		From INT_ND_RptAdmonDispensacion V (NoLock) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
			and convert(varchar(10), V.FechaRegistro, 120) = @FechaDeProceso and V.GUID = @GUID 		
		-- Group By TipoDispensacion, ClaveSSA_Base, ClaveSSA  	
	) as T  
	--	Group by  ClaveSSA_Base, ClaveSSA, Tipo  
		

----------------  Marcar las Claves CAUSES 
	Update D Set EsCauses = 1 
	From #INT_ND__tmpRemisiones D 
	Inner Join #tmp_ClavesSSA_Causes C On ( D.ClaveSSA = C.ClaveSSA )  	
	
		
	-------------- Marcar la propiedad del Cliente 			
	Update I Set EsEnResguardo = 1 
	From #INT_ND__tmpRemisiones I (NoLock) 
	Inner Join INT_ND_SubFarmaciasConsigna C (NoLock) On ( I.IdEstado = C.IdEstado and I.IdSubFarmacia = C.IdSubFarmacia ) 		
		
		
	--------------- Excluir claves no licitdas 
	Update R Set Incluir = 0 
	From #INT_ND__tmpRemisiones R 
	Inner Join INT_ND_CFG_ClavesSSA_Excluir E On ( R.IdEstado = E.IdEstado and R.ClaveSSA = E.ClaveSSA and E.Status = 'A' )   
	
--	Delete From #INT_ND__tmpRemisiones Where Incluir = 0 and EsEnResguardo = 0   

		
	-------------- Asignar Clave SSA Nadro 
	----select  IdEstado, IdClaveSSA, ClaveSSA, ClaveSSA_ND, Status, 0 as Existe 
	----into  #INT_ND_CFG_ClavesSSA  
	----from INT_ND_CFG_ClavesSSA T  
	----where ClaveSSA in 
	----( 
	----	select ClaveSSA  -- , COUNT(*)  
	----	from INT_ND_CFG_ClavesSSA 
	----	group by ClaveSSA 
	----	having COUNT(*) > 1 
	----) 
	
	----Update E Set Existe = 1 	
	----from #INT_ND_CFG_ClavesSSA E 
	----inner join INT_ND_CFG_CB_CuadrosBasicos C On ( E.ClaveSSA_ND = C.ClaveSSA_ND ) 
	
	----delete from #INT_ND_CFG_ClavesSSA where Existe = 0 
	
		
	------Update R Set ClaveSSA_ND = C.ClaveSSA_ND 
	------From #INT_ND__tmpRemisiones R 
	------Inner Join #INT_ND_CFG_ClavesSSA C On ( R.ClaveSSA = C.ClaveSSA ) 
	
	Update R Set ClaveSSA_ND = C.ClaveSSA_ND 
	From #INT_ND__tmpRemisiones R 
	Inner Join INT_ND_CFG_ClavesSSA C On ( R.ClaveSSA = C.ClaveSSA ) 	
	-------------- Asignar Clave SSA Nadro 


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
	

-------	QUITAR	
---	select * from #INT_ND__tmpRemisiones 
	



------------------------ Obtener las ventas 	

------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	


   
---		spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion  	
	
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
   


--------------------------------------------- ASIGNAR INFORMACION DEL ANEXO Y GENERAR FOLIO DE REMISION 
----------------- En los anexos marcados como CAUSES el material de curación se marca por defecto 
----------------  Marcar las Claves CAUSES 
	Update D Set EsCauses = 1 
	From #INT_ND__tmpRemisiones D 
	Inner Join #tmp_ClavesSSA_Causes C On ( D.ClaveSSA = C.ClaveSSA ) 	
	
	Update V Set IdAnexo = D.IdAnexo 
	From #INT_ND__tmpRemisiones V (NoLock) 
	Inner Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 		
	
	Update I Set EsCauses = 1 
	From #INT_ND__tmpRemisiones I (NoLock) 	
	Inner Join 	INT_ND_CFG_CB_Anexos_Causes C (NoLock) On ( I.IdEstado = C.IdEstado and I.IdAnexo = C.IdAnexo ) 
	Where TipoInsumo = 2 --- MATERIAL DE CURACION   

--		spp_INT_ND_GenerarRemisiones_Periodo


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


	Update V Set 
		IdAnexo = D.IdAnexo, NombreAnexo = D.NombreAnexo, Prioridad = D.Prioridad, 
		NombrePrograma = D.NombrePrograma, 
		ClaveSSA_ND = D.ClaveSSA_ND, 
		ClaveSSA_Mascara = (case when D.ClaveSSA_Mascara <> '' Then D.ClaveSSA_Mascara Else D.ClaveSSA_ND End), 
		Descripcion_Mascara = D.Descripcion_Mascara, 
		ManejaIva = D.ManejaIva, 
		PrecioVenta = D.PrecioVenta, PrecioServicio = D.PrecioServicio, 
		FolioRemision = D.IdAnexo + '/' + Modulo + '/' + @sFolioRemision + '/' + Tipo,   
		SubTotal = (D.PrecioVenta * V.Cantidad), 
		Iva = (case when D.ManejaIva = 0 then 0 else round((D.PrecioVenta * V.Cantidad) * ( 16 / 100.00 ), 2, 1) end),  
		ImporteTotal = 
			(case when D.ManejaIva = 0 then (D.PrecioVenta * V.Cantidad) 
			else 
			round((D.PrecioVenta * V.Cantidad) * ( 1 + ( 16 / 100.00) ), 2, 1) end),
		V.Lote = D.Lote				
	From #INT_ND__tmpRemisiones V (NoLock) 
	Inner Join #tmpAnexos_Detalles D (NoLock) On ( V.Keyx_Anexo = D.Keyx  ) 

	Update I Set PrecioVenta = 0, SubTotal = 0, Iva = 0, ImporteTotal = 0 
	From #INT_ND__tmpRemisiones I (NoLock) 	
	Where EsEnResguardo = 1 

	Update D Set Procesado = 1, IdAnexo = '0', NombreAnexo = 'NO ESPECIFICADO', Prioridad = 0, FolioRemision = '__NO_ESPECIFICADO' 
	From #INT_ND__tmpRemisiones D (NoLock) 
	Where D.Procesado = 0 		


-------------------------------- Aplicar cambio para remisiones mixtas ( Medicamento y Material de Curacion 
	----Update R Set 
	----	FolioRemision = 
	----		IdAnexo + '/' + (case when cast(TipoInsumo as int) = 1 Then '02' Else '01' end) + '/' + @sFolioRemision + '/' + Tipo 
	----From #INT_ND__tmpRemisiones R (NoLock) 
	----Inner Join INT_ND_CFG_Remisiones_Separar L (NoLock) 
	----	On ( R.IdEstado = L.IdEstado and R.IdFarmacia = L.IdFarmacia and R.FolioRemision = L.FolioRemision ) 	
	----Where IdAnexo <> '0' 	

	Set @sPrefijo_NoEspecificado = '__NE' 
	--Set @sPrefijo_NoEspecificado = '' 		
	Update R Set 
		Modulo = (case when cast(TipoInsumo as int) = 1 Then '02' Else '01' end) , 
		FolioRemision = 
			(IdAnexo + '/' + (case when cast(TipoInsumo as int) = 1 Then '02' Else '01' end) + '/' + @sFolioRemision + '/' + Tipo) + 
			(case when IdAnexo = '0' then @sPrefijo_NoEspecificado else '' end)
	From #INT_ND__tmpRemisiones R (NoLock) 
	-- Where IdAnexo <> '0' -- and FechaRemision >= '2015-03-01' 

-------------------------------- Aplicar cambio para remisiones mixtas ( Medicamento y Material de Curacion 	
	
	
--------------------------------------------- Cambiar Folio Remisión 
	If (@IdEstado = '07')
	Begin 
		
		Update D 
			Set FolioRemision = 
			(
				(
					case When IdAnexo = 'CAUSES' Then '60' 
						 When IdAnexo = 'CAUSES.' Then '63' 
						 When IdAnexo = 'NO CAUSES' Then '61'  
						 When IdAnexo = 'MATERIAL DE CURACION' Then '62'  
						 When IdAnexo = 'MATERIAL_DE_CURACION' Then '64'  						 					
					Else 
						 '0000'
					End
				) 
				+ '-' + IdFarmacia + '-' + Replace(D.FechaRemision, '-', '') + '-' + D.Tipo 
			) + (case when IdAnexo = '0' then @sPrefijo_NoEspecificado else '' end) 
		From #INT_ND__tmpRemisiones D (NoLock) 
		
		----Update D Set FolioRemision = FolioRemision + '__NE' 
		----From #INT_ND__tmpRemisiones D (NoLock) 
		----Where IdAnexo = '0'  
	End 	
--------------------------------------------- Cambiar Folio Remisión 
		

	
	
	Update D Set CodigoCliente = @CodigoCliente,  
		 Procesado = 1, IdAnexo = R.IdAnexo, NombreAnexo = R.NombreAnexo, Prioridad = R.Prioridad, NombrePrograma = R.NombrePrograma, 
		 EsCauses = R.EsCauses, 
		 ClaveSSA_ND = R.ClaveSSA_ND, ClaveSSA_Mascara = R.ClaveSSA_Mascara, Descripcion_Mascara = R.Descripcion_Mascara, 
		 ManejaIva = R.ManejaIva, PrecioVenta = R.PrecioVenta, PrecioServicio = R.PrecioServicio, 
		 FolioRemision = R.FolioRemision, 
		 FolioRemision_Auxiliar = R.FolioRemision_Auxiliar, 
		 --TipoDispensacion = R.Tipo, 
		 SubTotal = R.SubTotal, Iva = R.Iva, ImporteTotal = R.ImporteTotal, 
		 FechaGeneracion = replace(convert(varchar(10), @FechaDeProceso, 120), '-', ''), 
		 Incluir = R.Incluir, TipoRelacion = R.TipoRelacion, D.Lote = R.Lote   
	From INT_ND_RptAdmonDispensacion D (NoLock)
	Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx and D.GUID = @GUID ) 
	Where R.Procesado = 1 
	
	

	Update D Set CodigoCliente = @CodigoCliente, 
		 Procesado = 1, IdAnexo = R.IdAnexo, NombreAnexo = R.NombreAnexo, Prioridad = R.Prioridad, NombrePrograma = R.NombrePrograma, 
		 EsCauses = R.EsCauses, 		 
		 ClaveSSA_ND = R.ClaveSSA_ND, ClaveSSA_Mascara = R.ClaveSSA_Mascara, Descripcion_Mascara = R.Descripcion_Mascara, 
		 ManejaIva = R.ManejaIva, PrecioVenta = R.PrecioVenta, PrecioServicio = R.PrecioServicio, 
		 FolioRemision = R.FolioRemision, 
		 FolioRemision_Auxiliar = R.FolioRemision_Auxiliar, 
		 --TipoDispensacion = R.Tipo, 
		 SubTotal = R.SubTotal, Iva = R.Iva, ImporteTotal = R.ImporteTotal, 
		 FechaGeneracion = replace(convert(varchar(10), @FechaDeProceso, 120), '-', ''), 
		 Incluir = R.Incluir, TipoRelacion = R.TipoRelacion,
		 D.Lote = R.Lote  
	From INT_ND_RptAdmonDispensacion_Detallado__General D (NoLock) 
	Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx_GUID = R.Keyx and D.GUID = @GUID ) 
	Where R.Procesado = 1 
--------------------------------------------- ASIGNAR INFORMACION DEL ANEXO Y GENERAR FOLIO DE REMISION 
	
---------------------------------------------------------- SALIDA FINAL  	
	Select GUID, 
		CodigoCliente, Prioridad, IdAnexo, NombreAnexo, Tipo, FolioRemision 
	From #INT_ND__tmpRemisiones D 
	-- Where D.IdAnexo in ( '1.3.3.1', '1.1.5'  ) 
	Group by GUID, CodigoCliente, Prioridad, IdAnexo, NombreAnexo, Tipo, FolioRemision 
	Having sum(D.Cantidad) > 0 
	Order by Prioridad, IdAnexo 


	Select D.GUID, D.TipoDispensacion, D.TipoInsumo, D.EsEnResguardo -- , R.EsCauses  	
	From INT_ND_RptAdmonDispensacion D (NoLock) 
	Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx ) 	 
	--Where D.IdAnexo in ( '1.3.3.1', ''  ) 
	Group by D.GUID, D.TipoDispensacion, D.TipoInsumo, D.EsEnResguardo -- , R.EsCauses  
	Having sum(D.Cantidad) > 0 	
	Order By D.EsEnResguardo, D.TipoDispensacion, D.TipoInsumo -- , R.EsCauses 


	Select D.* 	
	From INT_ND_RptAdmonDispensacion D (NoLock) 
	Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx ) 	 
	Where D.Cantidad > 0  --and 
		-- D.IdAnexo in ( '1.3.3.1', '1.1.5'  ) 
		-- R.Prioridad >= 20 
	Order By D.EsEnResguardo, D.Prioridad, D.TipoDispensacion, D.TipoInsumo			
		
	----------------------- Analizar el número total de remisiones a generar 	
	Select 
		D.GUID, 
		D.CodigoCliente, D.Prioridad, D.IdAnexo, D.NombreAnexo, R.Tipo, D.FolioRemision, 
		D.TipoDispensacion, D.TipoInsumo, D.EsEnResguardo -- , R.EsCauses  	
	From INT_ND_RptAdmonDispensacion D (NoLock) 
	Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx ) 	 
	Group by 
		D.GUID, 
		D.CodigoCliente, D.Prioridad, D.IdAnexo, D.NombreAnexo, R.Tipo, D.FolioRemision, 
		D.TipoDispensacion, D.TipoInsumo, D.EsEnResguardo -- , R.EsCauses  	
	Having sum(D.Cantidad) > 0 	
	Order By D.EsEnResguardo, D.Prioridad, D.TipoDispensacion, D.TipoInsumo -- , R.EsCauses 
			
---------------------------------------------------------- SALIDA FINAL  
	
---		spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion  	

	
End  
Go--#SQL 

