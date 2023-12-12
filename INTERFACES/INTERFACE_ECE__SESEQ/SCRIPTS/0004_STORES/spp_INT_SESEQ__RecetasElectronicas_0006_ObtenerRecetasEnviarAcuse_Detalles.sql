-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles 
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '113', 
	@FolioVenta varchar(20) = '00005847' 
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
	Set @FolioVenta = RIGHT('000000000000' + @FolioVenta, 8) 
	Set @sFolioReceta = '' 
	Set @bResurtible = 0 
	Set @iTipoDeProceso = 0 

------------------------------------------------- OBTENER LA INFORMACION  
	------------------ Obtener todos los folios de venta relacionados al Número de Receta 
	Select G.IdEmpresa, G.IdEstado, G.IdFarmacia, G.FolioVenta,  G.NumReceta 
	Into #tmp__FolioVenta 
	From VentasInformacionAdicional G (NoLock) 
	Where G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.FolioVenta = @FolioVenta 


	Select G.*, V.NumReceta as FolioReceta 
	Into #tmp__Recetas_FolioVenta  
	From VentasEnc G (NoLock) 
	Inner Join VentasInformacionAdicional V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioVenta = V.FolioVenta ) 		 
	Where G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and Exists 
		( 
			Select * 
			From #tmp__FolioVenta I (NoLock) 
			Where V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.NumReceta = I.NumReceta
		) 


	---------------- Informacion de la receta 
	Select top 1 @sFolioReceta = NumReceta 
	From #tmp__FolioVenta  

	Select top 1 @bResurtible = EsResurtible, @iTipoDeProceso = TipoDeProceso  
	from INT_SESEQ__RecetasElectronicas_0001_General 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioReceta = @sFolioReceta 
	---------------- Informacion de la receta 


	------------- Sólo enviar información del folio requerido 
	If @bResurtible = 1 and @iTipoDeProceso = 1  
	Begin 
		
		print 'Borrando' 
		Delete from #tmp__Recetas_FolioVenta 

		Insert Into #tmp__Recetas_FolioVenta 
		Select G.*, V.NumReceta as FolioReceta 
		From VentasEnc G (NoLock) 
		Inner Join VentasInformacionAdicional V (NoLock) 
			On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioVenta = V.FolioVenta ) 		 
		Where G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia and G.FolioVenta = @FolioVenta 
	End 

	if @iTipoDeProceso = 2 
	Begin 
		Print '' 
	End 

	------------------ Obtener todos los folios de venta relacionados al Número de Receta 



	Select 
		G.EsResurtible, 
		X.IdEmpresa, X.IdEstado, X.IdFarmacia, G.Folio as FolioInterface, 
		X.UMedica, 
		G.FolioReceta as Folio_SESEQ, 
		X.TipoDeProceso, X.DisponibleSurtido, X.Surtidos, X.Surtidos_Aplicados, 
		G.EsSurtido, 0 as Partida, G.FolioSurtido as FolioVenta, V.FolioVenta as FolioVenta_Relacionado, 
		G.FechaDeSurtido as FechaDeSurtimiento, 
		G.FolioReceta, G.FechaReceta, G.FechaEnvioReceta, 
		G.FolioAfiliacionSPSS, G.FechaIniciaVigencia, G.FechaTerminaVigencia, G.Expediente, 
		G.idEpisodio, G.idTipoServicio, G.idServicio, G.idPaciente, G.FolioColectivo,
		G.NombreBeneficiario, G.ApPaternoBeneficiario, G.ApMaternoBeneficiario, G.Sexo, G.FechaNacimientoBeneficiario, 
		G.CamaPaciente,
		G.FolioAfiliacionOportunidades, G.EsPoblacionAbierta, 
		G.ClaveDeMedico, G.NombreMedico, G.ApPaternoMedico, G.ApMaternoMedico, G.CedulaDeMedico, 

		convert(varchar(10), V.FechaRegistro, 120) as FechaSurtido,  

		--V.IdCliente, V.IdSubCliente, 
		V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
		--cast(V.IdCliente as varchar(100)) as IdCliente,  
		--cast('' as varchar(100)) as IdSubCliente, 
		cast('' as varchar(100)) as IdBeneficiario, 		
		
		cast(V.IdPersonal as varchar(100)) as IdPersonalSurte,  
		cast('' as varchar(100)) as NombrePersonaSurte, 
		cast('' as varchar(100)) as ApPaternoPersonaSurte, 
		cast('' as varchar(100)) as ApMaternoPersonaSurte, 
		cast('' as varchar(200)) as Observaciones,  
		cast('' as varchar(100)) as NombrePersonaRecibe, 
		cast('' as varchar(100)) as ApPaternoPersonaRecibe, 
		cast('' as varchar(100)) as ApMaternoPersonaRecibe, 	
		identity(int, 1, 1) as Keyx  
	Into #tmp_01_General 
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
	Inner Join #tmp__Recetas_FolioVenta V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioReceta = V.FolioReceta ) 		
	Where 
		G.EsSurtido = 1 and 
		G.Procesado = 0 and 
		EsCancelado = 0 and G.FolioSurtido = @FolioVenta 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.EsResurtible = 0 


	Insert Into #tmp_01_General 
	Select 
		G.EsResurtible, 
		X.IdEmpresa, X.IdEstado, X.IdFarmacia, G.Folio as FolioInterface, 
		X.UMedica, 
		G.FolioReceta as Folio_SESEQ, 
		X.TipoDeProceso, X.DisponibleSurtido, X.Surtidos, X.Surtidos_Aplicados, 
		G.EsSurtido, GD.Partida, G.FolioSurtido as FolioVenta, V.FolioVenta as FolioVenta_Relacionado, 
		G.FechaDeSurtido as FechaDeSurtimiento, 
		G.FolioReceta, G.FechaReceta, G.FechaEnvioReceta, 
		G.FolioAfiliacionSPSS, G.FechaIniciaVigencia, G.FechaTerminaVigencia, G.Expediente, 
		G.idEpisodio, G.idTipoServicio, G.idServicio, G.idPaciente, G.FolioColectivo,
		G.NombreBeneficiario, G.ApPaternoBeneficiario, G.ApMaternoBeneficiario, G.Sexo, G.FechaNacimientoBeneficiario, 
		G.CamaPaciente,
		G.FolioAfiliacionOportunidades, G.EsPoblacionAbierta, 
		G.ClaveDeMedico, G.NombreMedico, G.ApPaternoMedico, G.ApMaternoMedico, G.CedulaDeMedico, 

		convert(varchar(10), V.FechaRegistro, 120) as FechaSurtido,  

		--V.IdCliente, V.IdSubCliente, 
		V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
		--cast(V.IdCliente as varchar(100)) as IdCliente,  
		--cast('' as varchar(100)) as IdSubCliente, 
		cast('' as varchar(100)) as IdBeneficiario, 		
		
		cast(V.IdPersonal as varchar(100)) as IdPersonalSurte,  
		cast('' as varchar(100)) as NombrePersonaSurte, 
		cast('' as varchar(100)) as ApPaternoPersonaSurte, 
		cast('' as varchar(100)) as ApMaternoPersonaSurte, 
		cast('' as varchar(200)) as Observaciones,  
		cast('' as varchar(100)) as NombrePersonaRecibe, 
		cast('' as varchar(100)) as ApPaternoPersonaRecibe, 
		cast('' as varchar(100)) as ApMaternoPersonaRecibe 
	From INT_SESEQ__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.FolioXML ) 
	Inner Join INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas GD (NoLock) 
		On ( G.IdEmpresa = GD.IdEmpresa and G.IdEstado = GD.IdEstado and G.IdFarmacia = GD.IdFarmacia and G.Folio = GD.Folio ) 
	Inner Join #tmp__Recetas_FolioVenta V (NoLock) 
		On ( GD.IdEmpresa = V.IdEmpresa and GD.IdEstado = V.IdEstado and GD.IdFarmacia = V.IdFarmacia and GD.FolioSurtido = V.FolioVenta ) 		
	Where 
		GD.EsSurtido = 1 and 
		GD.Procesado = 0 and 
		EsCancelado = 0 and GD.FolioSurtido = @FolioVenta 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and G.EsResurtible = 1 


	------------------------------------------------------------------------------------------------------------------------------------- 	
	Update G Set NombrePersonaSurte = P.Nombre, ApPaternoPersonaSurte = P.ApPaterno, ApMaternoPersonaSurte = P.ApMaterno
	From #tmp_01_General G (NoLock)  
	Inner Join CatPersonal P (NoLock) On ( G.IdEstado = P.IdEstado and G.IdFarmacia = P.IdFarmacia and G.IdPersonalSurte = P.IdPersonal ) 
	

	------------------------------------------------------------------------------------------------------------------------------------- 	
	Select 
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, 
		G.IdCliente, G.IdSubCliente, 
		G.FechaSurtido, 
		G.FolioVenta, G.FolioVenta_Relacionado, 
		cast('' as varchar(30)) as ClaveSSA, 
		cast('' as varchar(30)) as ClaveSSA_Base, 		
		cast('' as varchar(7500)) As DescripcionSal, cast('' as varchar(30)) as IdClaveSSA, 
		L.IdProducto, L.CodigoEAN, 
		replace(L.ClaveLote, '*', '') as ClaveLote, 
		convert(varchar(10), I.FechaCaducidad, 120) as Caducidad, 
		0 as CantidadRecetada, cast(L.Cant_Vendida as int) as CantidadSurtida, 0 as CantidadVale, 0 as Tipo, 0 as Eliminar, 0 as TipoDeInsumo    
	Into #tmp_02_Medicamentos 
	From VentasDet_Lotes L (NoLock) 
	Inner Join #tmp_01_General G (NoLock) 
		On ( L.IdEmpresa = G.IdEmpresa and L.IdEstado = G.IdEstado and L.IdFarmacia = G.IdFarmacia and L.FolioVenta = G.FolioVenta_Relacionado ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes I (NoLock) 
		On ( L.IdEmpresa = I.IdEmpresa and L.IdEstado = I.IdEstado and L.IdFarmacia = I.IdFarmacia and L.IdSubFarmacia = I.IdSubFarmacia
			and L.IdProducto = I.IdProducto and L.CodigoEAN = I.CodigoEAN and L.ClaveLote = I.ClaveLote ) 
	Where G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 	
	

--		spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles  



	--select * from #tmp_02_Medicamentos 


	
	
	Insert into #tmp_02_Medicamentos 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, 
		FechaSurtido, 
		FolioVenta, ClaveSSA, ClaveSSA_Base, IdClaveSSA, DescripcionSal, IdProducto, CodigoEAN, ClaveLote, Caducidad, 
		CantidadRecetada, CantidadSurtida, CantidadVale, Tipo, Eliminar, TipoDeInsumo  
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, 
		E.IdCliente, E.IdSubCliente,
		E.FechaSurtido, 
		@FolioVenta as FolioVenta, 
		cast('' as varchar(30)) as ClaveSSA, 
		cast('' as varchar(30)) as ClaveSSA, 		
		cast('' as varchar(30)) as IdClaveSSA, cast('' as varchar(7500)) As DescripcionSal, 
		'' as IdProducto, '' as CodigoEAN, '' as ClaveLote, cast('' as varchar(20)) as Caducidad, 
		cast(I.CantidadRequerida as int) as CantidadRecetada, 0 as CantidadSurtida, 0 as CantidadVale, 1 as Tipo, 0 as Eliminar, TipoDeInsumo     
	From INT_SESEQ__RecetasElectronicas_0004_Insumos I 
	Inner Join #tmp_01_General E (NoLock) 
		On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 	
	Where 1 = 0  


--		spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles  


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


	---- Quitar el detalle de lo solicitado 
	Delete from #tmp_02_Medicamentos Where Eliminar = 1 



	Select 
		FechaSurtido, 
		FolioVenta, FolioVenta_Relacionado, ClaveSSA, ClaveSSA_Base, DescripcionSal, 
		ClaveLote, Caducidad, 
		sum(CantidadRecetada) as CantidadRecetada, sum(CantidadSurtida) as CantidadSurtida 
	Into #tmp_02_Medicamentos_Concentrado  
	From #tmp_02_Medicamentos 
	Where TipoDeInsumo <> 2 
	Group by 
		FechaSurtido, 
		FolioVenta, FolioVenta_Relacionado, ClaveSSA, ClaveSSA_Base, DescripcionSal, ClaveLote, Caducidad 
	Having sum(CantidadSurtida) > 0 
		

------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * 
	From #tmp_01_General E 
	where Exists 
	( 
		select * 
		From #tmp_02_Medicamentos_Concentrado D 
		Where E.FolioVenta = D.FolioVenta 
	) 

	Select 
		FechaSurtido, 
		FolioVenta, FolioVenta_Relacionado, ClaveSSA, ClaveSSA_Base, DescripcionSal, ClaveLote, Caducidad, CantidadRecetada, CantidadSurtida 
	From #tmp_02_Medicamentos_Concentrado 
	Order by ClaveSSA 

	--Select * From #tmp_02_Medicamentos Where TipoDeInsumo = 2 Order by ClaveSSA 
	--Select * From #tmp_03_Vales_Medicamentos Order by ClaveSSA 	
	--Select * From #tmp_04_SurtidoOtraUnidad Order by ClaveSSA 
	--Select * From #tmp_05_SurtidoOtraUnidad_Medicamentos Order by ClaveSSA 
	
End 
Go--#SQL 
	



--------------- REVISAR 20220510.1218 
/*  
	select 
		R.IdEmpresa, R.IdFarmacia, 
		R.Folio, R.FolioSurtido, R.FolioReceta, D.ClaveSSA, D.CantidadRequerida, D.CantidadEntregada 
	--Into #tmp_Recetas  
	from INT_SESEQ__RecetasElectronicas_0001_General R 
	Inner Join INT_SESEQ__RecetasElectronicas_0004_Insumos D (noLock) On ( R.Folio = D.Folio ) 
	where EsSurtido = 1 
		--and FolioSurtido = 00005847 
	order by R.folio 


	drop table #tmp_Dispensacion 

	Select 
		G.Folio, G.FolioReceta, 
		cast('' as varchar(30)) as ClaveSSA, cast('' as varchar(30)) as ClaveSSA_SESEQ, 
		L.IdProducto, L.CodigoEAN, sum(L.CantidadVendida) as Cantidad 
	Into #tmp_Dispensacion 
	From VentasDet_Lotes L (NoLock) 
	Inner Join INT_SESEQ__RecetasElectronicas_0001_General G (NoLock) 
		On ( L.IdEmpresa = G.IdEmpresa and L.IdEstado = G.IdEstado and L.IdFarmacia = G.IdFarmacia and L.FolioVenta = G.FolioSurtido ) 
	--Where G.Folio =  000000000042 
	Group by 
		G.Folio, G.FolioReceta,  
		L.IdProducto, L.CodigoEAN 
	Order by Folio 



	Update M Set 
		ClaveSSA = P.ClaveSSA
		---, ClaveSSA_Mascara = MC.Mascara 
	From #tmp_Dispensacion M (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( M.IdProducto = P.IdProducto and M.CodigoEAN = P.CodigoEAN ) 

	
	Update M Set 
		ClaveSSA_SESEQ = MC.Mascara 
	From #tmp_Dispensacion M (NoLock) 
	Inner Join vw_ClaveSSA_Mascara MC (NoLock) 
		On ( 
			'22' = MC.IdEstado and '0042' = MC.IdCliente and '0003' = MC.IdSubCliente 
			and M.ClaveSSA = MC.ClaveSSA 
			--and replace(replace(M.ClaveSSA, '.', ''), '-', '') = replace(replace(MC.ClaveSSA, '.', ''), '-', '')  ---- Forzar la compabilidad de los datos 
			) 



	select * 
	from #tmp_Dispensacion R 
	Inner Join INT_SESEQ__RecetasElectronicas_0004_Insumos D (noLock) On ( R.Folio = D.Folio and R.ClaveSSA_SESEQ = D.ClaveSSA ) 
	Where R.Folio = 000000000105 


	Update D Set CantidadEntregada = R.Cantidad 
	from #tmp_Dispensacion R 
	Inner Join INT_SESEQ__RecetasElectronicas_0004_Insumos D (noLock) On ( R.Folio = D.Folio and R.ClaveSSA_SESEQ = D.ClaveSSA ) 


	Select * 
	from INT_SESEQ__RecetasElectronicas_0004_Insumos 
	Where Folio = 000000000042 

*/

