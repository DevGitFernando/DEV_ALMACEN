-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse_Detalles' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse_Detalles
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse_Detalles 
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '113', 
	@FolioDevolucion varchar(20) = '00000076' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@sFolioReceta varchar(100), 
	@bResurtible bit, 
	@iTipoDeProceso int 


	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2)  
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4)  
	Set @FolioDevolucion = RIGHT('000000000000' + @FolioDevolucion, 8) 
	Set @sFolioReceta = '' 
	Set @bResurtible = 0 
	Set @iTipoDeProceso = 0 

------------------------------------------------- OBTENER LA INFORMACION  
	------------------ Obtener todos los folios de venta relacionados al Número de Receta 
	Select G.IdEmpresa, G.IdEstado, G.IdFarmacia, G.FolioDevolucion 
	Into #tmp__FolioDevolucion 
	From DevolucionesEnc G (NoLock) 
	Where G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.FolioDevolucion = @FolioDevolucion 
		and G.TipoDeDevolucion = 1 





	------------------ Obtener todos los folios de venta relacionados al Número de Receta 

	Select 
		G.IdEmpresa, G.IdEstado, G.IdFarmacia, G.Folio as FolioInterface, 
		E.IdCliente, E.IdSubCliente, 
		G.FolioReceta as Folio_SESEQ, 
		G.TipoDeProceso, 
		G.FolioDevolucion, G.FolioSurtido as FolioVenta,  
		G.FechaDeDevolucion, 
		G.FolioReceta, G.FechaReceta, 
		identity(int, 1, 1) as Keyx  
	Into #tmp_01_General 
	From INT_SESEQ__RecetasElectronicas_0007_Devoluciones G (NoLock) 
	Inner Join #tmp__FolioDevolucion V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioDevolucion = V.FolioDevolucion ) 		
	Inner Join VentasEnc E (NoLock) 
		On ( G.IdEmpresa = E.IdEmpresa and G.IdEstado = E.IdEstado and G.IdFarmacia = E.IdFarmacia and G.FolioSurtido = E.FolioVenta ) 		
	Where 
		G.Procesado = 0 
		and G.FolioDevolucion = @FolioDevolucion 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		--and G.EsResurtible = 0 


		--select  top 10 * from #tmp_01_General

---		spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse_Detalles 

	------------------------------------------------------------------------------------------------------------------------------------- 	


	------------------------------------------------------------------------------------------------------------------------------------- 	
	Select 
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, 
		G.IdCliente, G.IdSubCliente, 
		----G.FechaSurtido, 
		G.FolioDevolucion, G.FechaDeDevolucion, convert(varchar(10), G.FechaDeDevolucion, 120) as FechaSurtido, 
		G.FolioVenta,  
		cast('' as varchar(30)) as ClaveSSA, 
		cast('' as varchar(30)) as ClaveSSA_Base, 		
		cast('' as varchar(7500)) As DescripcionSal, cast('' as varchar(30)) as IdClaveSSA, 
		L.IdProducto, L.CodigoEAN, 
		replace(L.ClaveLote, '*', '') as ClaveLote, 
		convert(varchar(10), I.FechaCaducidad, 120) as Caducidad, 
		0 as CantidadRecetada, cast((L.Cant_Devuelta * 1) as int) as CantidadSurtida, 0 as CantidadVale, 0 as Tipo, 0 as Eliminar, 0 as TipoDeInsumo    
	Into #tmp_02_Medicamentos 
	From DevolucionesDet_Lotes L (NoLock) 
	Inner Join #tmp_01_General G (NoLock) 
		On ( L.IdEmpresa = G.IdEmpresa and L.IdEstado = G.IdEstado and L.IdFarmacia = G.IdFarmacia and L.FolioDevolucion = G.FolioDevolucion ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes I (NoLock) 
		On ( L.IdEmpresa = I.IdEmpresa and L.IdEstado = I.IdEstado and L.IdFarmacia = I.IdFarmacia and L.IdSubFarmacia = I.IdSubFarmacia
			and L.IdProducto = I.IdProducto and L.CodigoEAN = I.CodigoEAN and L.ClaveLote = I.ClaveLote ) 
	Where G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 	
	

--		spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse_Detalles  


	----select * from #tmp_01_General 
	----select * from #tmp_02_Medicamentos 




--		spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse_Detalles  


	Update M Set 
		IdClaveSSA = P.IdClaveSSA_Sal, ClaveSSA = P.ClaveSSA, ClaveSSA_Base = P.ClaveSSA, DescripcionSal = P.DescripcionSal  
		---, ClaveSSA_Mascara = MC.Mascara 
	From #tmp_02_Medicamentos M (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( M.IdProducto = P.IdProducto and M.CodigoEAN = P.CodigoEAN ) 
	-- Inner Join vw_ClaveSSA_Mascara MC (NoLock) On ( M.IdEstado = MC.IdEstado and M.IdCliente = MC.IdCliente and M.IdSubCliente = MC.IdSubCliente and P.ClaveSSA = MC.ClaveSSA ) 

	-- Select * from #tmp_02_Medicamentos 

	Update M Set 
		ClaveSSA = MC.Mascara 
	From #tmp_02_Medicamentos M (NoLock) 
	Inner Join vw_ClaveSSA_Mascara MC (NoLock) 
		On ( 
			M.IdEstado = MC.IdEstado and M.IdCliente = MC.IdCliente and M.IdSubCliente = MC.IdSubCliente 
			and M.ClaveSSA = MC.ClaveSSA 
			--and replace(replace(M.ClaveSSA, '.', ''), '-', '') = replace(replace(MC.ClaveSSA, '.', ''), '-', '')  ---- Forzar la compabilidad de los datos 
			) 

	Update E Set CantidadRecetada = IsNull(( select top 1 CantidadRecetada 
		From #tmp_02_Medicamentos I (NoLock) Where I.ClaveSSA = E.ClaveSSA and I.Tipo = 1 ), 0)
	From #tmp_02_Medicamentos E
	
	Update E Set TipoDeInsumo = IsNull(( select top 1 TipoDeInsumo 
		From #tmp_02_Medicamentos I (NoLock) Where I.ClaveSSA = E.ClaveSSA and I.TipoDeInsumo = 2 ), 1)
	From #tmp_02_Medicamentos E 
	
	

	Update T Set Eliminar = 1 
	From #tmp_02_Medicamentos T 
	Where Tipo = 1 
	And Exists 
		(
			Select * 
			From #tmp_02_Medicamentos T_01 
			Where T.ClaveSSA = T_01.ClaveSSA and Tipo = 0 
		)
	And Exists 
		(
			Select * 
			From #tmp_02_Medicamentos T_02 
			Where T.ClaveSSA = T_02.ClaveSSA and Tipo = 1 
		)	


	-- Quitar el detalle de lo solicitado 
	Delete from #tmp_02_Medicamentos Where Eliminar = 1 



	Select 
		convert(varchar(10), FechaDeDevolucion, 120) as FechaSurtido, 
		FolioDevolucion, FolioVenta, ClaveSSA, ClaveSSA_Base, DescripcionSal, 
		ClaveLote, Caducidad, 
		sum(CantidadRecetada) as CantidadRecetada, sum(CantidadSurtida * -1) as CantidadSurtida 
	Into #tmp_02_Medicamentos_Concentrado  
	From #tmp_02_Medicamentos 
	Where TipoDeInsumo <> 2 
	Group by 
		convert(varchar(10), FechaDeDevolucion, 120), 
		FolioDevolucion, 
		FolioVenta, ClaveSSA, ClaveSSA_Base, DescripcionSal, ClaveLote, Caducidad 
	Having sum(CantidadSurtida) > 0 
		

------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse_Detalles 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * 
	From #tmp_01_General E 
	where Exists 
	( 
		select * 
		From #tmp_02_Medicamentos_Concentrado D 
		Where E.FolioDevolucion = D.FolioDevolucion 
	) 

	Select 
		FechaSurtido, 
		FolioDevolucion, FolioVenta as FolioVenta_Relacionado, ClaveSSA, ClaveSSA_Base, DescripcionSal, ClaveLote, Caducidad, CantidadRecetada, CantidadSurtida 
	From #tmp_02_Medicamentos_Concentrado 
	Order by ClaveSSA 

	--Select * From #tmp_02_Medicamentos Where TipoDeInsumo = 2 Order by ClaveSSA 
	--Select * From #tmp_03_Vales_Medicamentos Order by ClaveSSA 	
	--Select * From #tmp_04_SurtidoOtraUnidad Order by ClaveSSA 
	--Select * From #tmp_05_SurtidoOtraUnidad_Medicamentos Order by ClaveSSA 
	
End 
Go--#SQL 
	



