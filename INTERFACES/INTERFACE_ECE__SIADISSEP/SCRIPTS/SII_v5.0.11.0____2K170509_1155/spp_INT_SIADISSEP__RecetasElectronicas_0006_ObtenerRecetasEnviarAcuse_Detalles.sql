-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles' and xType = 'P' ) 
   Drop Proc spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles
Go--#SQL 

Create Proc spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '2406', 
	@FolioVenta varchar(20) = '00049096' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


------------------------------------------------- OBTENER LA INFORMACION   
	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.Folio as FolioInterface, 
		X.UMedica, X.Folio_SIADISSEP, X.TipoDeProceso, X.DisponibleSurtido, X.Surtidos, X.Surtidos_Aplicados, 
		G.EsSurtido, G.FolioSurtido as FolioVenta, G.FechaDeSurtido as FechaDeSurtimiento, 
		G.FolioReceta, G.FechaReceta, G.FechaEnvioReceta, 
		G.FolioAfiliacionSPSS, G.FechaIniciaVigencia, G.FechaTerminaVigencia, G.Expediente, 
		G.NombreBeneficiario, G.ApPaternoBeneficiario, G.ApMaternoBeneficiario, G.Sexo, 
		G.FolioAfiliacionOportunidades, G.EsPoblacionAbierta, 
		G.ClaveDeMedico, G.NombreMedico, G.ApPaternoMedico, G.ApMaternoMedico, G.CedulaDeMedico, 
		
		V.IdCliente, V.IdSubCliente, 
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
	From INT_SIADISSEP__RecetasElectronicas_XML X (NoLock)
	Inner Join INT_SIADISSEP__RecetasElectronicas_0001_General G (NoLock) 
		On ( X.IdEmpresa = G.IdEmpresa and X.IdEstado = G.IdEstado and X.IdFarmacia = G.IdFarmacia and X.Folio = G.Folio ) 
	Inner Join VentasEnc V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
	Where G.EsSurtido = 1 and G.Procesado = 0 and EsCancelado = 0 and G.FolioSurtido = @FolioVenta 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 

	
	
	--Select I.* 
	--From INT_SIADISSEP__RecetasElectronicas_0004_Insumos I 
	--Inner Join #tmp_01_General E (NoLock) 
	--	On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 
	
	

	Update G Set NombrePersonaSurte = P.Nombre, ApPaternoPersonaSurte = P.ApPaterno, ApMaternoPersonaSurte = P.ApMaterno
	From #tmp_01_General G (NoLock)  
	Inner Join CatPersonal P (NoLock) On ( G.IdEstado = P.IdEstado and G.IdFarmacia = P.IdFarmacia and G.IdPersonalSurte = P.IdPersonal ) 
	

	------------------------------------------------------------------------------------------------------------------------------------- 	
	Select 
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.FolioVenta, 
		cast('' as varchar(30)) as ClaveSSA, cast('' as varchar(30)) as IdClaveSSA, 
		L.IdProducto, L.CodigoEAN, L.ClaveLote, convert(varchar(10), I.FechaCaducidad, 120) as Caducidad, 
		0 as CantidadRecetada, cast(L.CantidadVendida as int) as CantidadSurtida, 0 as CantidadVale, 0 as Tipo, 0 as Eliminar    
	Into #tmp_02_Medicamentos 
	From VentasDet_Lotes L (NoLock) 
	Inner Join #tmp_01_General G (NoLock) 
		On ( L.IdEmpresa = G.IdEmpresa and L.IdEstado = G.IdEstado and L.IdFarmacia = G.IdFarmacia and L.FolioVenta = G.FolioVenta ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes I (NoLock) 
		On ( L.IdEmpresa = I.IdEmpresa and L.IdEstado = I.IdEstado and L.IdFarmacia = I.IdFarmacia and L.IdSubFarmacia = I.IdSubFarmacia
			and L.IdProducto = I.IdProducto and L.CodigoEAN = I.CodigoEAN and L.ClaveLote = I.ClaveLote ) 
	Where G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 	
	
	
	Update M Set IdClaveSSA = P.IdClaveSSA_Sal, ClaveSSA = P.ClaveSSA
	From #tmp_02_Medicamentos M (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( M.IdProducto = P.IdProducto and M.CodigoEAN = P.CodigoEAN ) 
	
	

	----Select @IdEmpresa, @IdEstado, @IdFarmacia, '' as FolioVenta, 
	----	cast(I.ClaveSSA as varchar(30)) as ClaveSSA, cast('' as varchar(30)) as IdClaveSSA, 
	----	'' as IdProducto, '' as CodigoEAN, '' as ClaveLote, cast('' as varchar(20)) as Caducidad, 
	----	cast(I.CantidadRequerida as int) as CantidadRecetada, 0 as CantidadSurtida, 0 as CantidadVale, 1 as Tipo  
	----From INT_SIADISSEP__RecetasElectronicas_0004_Insumos I 
	----Inner Join #tmp_01_General E (NoLock) 
	----	On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 
	
	
	Insert into #tmp_02_Medicamentos 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioVenta, ClaveSSA, IdClaveSSA, IdProducto, CodigoEAN, ClaveLote, Caducidad, 
		CantidadRecetada, CantidadSurtida, CantidadVale, Tipo, Eliminar  
	) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta as FolioVenta, 
		cast(I.ClaveSSA as varchar(30)) as ClaveSSA, cast('' as varchar(30)) as IdClaveSSA, 
		'' as IdProducto, '' as CodigoEAN, '' as ClaveLote, cast('' as varchar(20)) as Caducidad, 
		cast(I.CantidadRequerida as int) as CantidadRecetada, 0 as CantidadSurtida, 0 as CantidadVale, 1 as Tipo, 0 as Eliminar      
	From INT_SIADISSEP__RecetasElectronicas_0004_Insumos I 
	Inner Join #tmp_01_General E (NoLock) 
		On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 	
	----Where Not Exists 
	----(
	----	Select * 
	----	From #tmp_02_Medicamentos F (NoLock) 
	----	Where F.IdEmpresa = E.IdEmpresa and F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and F.FolioVenta = E.FolioInterface 
	----) 

	Update E Set CantidadRecetada = IsNull(( select top 1 CantidadRecetada 
		From #tmp_02_Medicamentos I (NoLock) Where I.ClaveSSA = E.ClaveSSA and I.Tipo = 1 ), 0)
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




	
	------------------------------------------------------------------------------------------------------------------------------------- 
	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVale, E.FolioVenta, 
		L.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, 
		cast(L.Cantidad as int) as CantidadVale, cast('SI' as varchar(10)) as SurtidoConVale, 0 as Tipo, 0 as Eliminar    
	Into #tmp_03_Vales_Medicamentos 
	From Vales_EmisionEnc E (NoLock) 
	Inner Join  Vales_EmisionDet L (NoLock) 
		On ( L.IdEmpresa = E.IdEmpresa and L.IdEstado = E.IdEstado and L.IdFarmacia = E.IdFarmacia and L.FolioVale = E.FolioVale  ) 	
	Inner Join vw_ClavesSSA_Sales P (NoLock) On ( L.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) 			
	----Inner Join #tmp_02_Medicamentos G (NoLock) 
	----	On ( E.IdEmpresa = G.IdEmpresa and E.IdEstado = G.IdEstado and E.IdFarmacia = G.IdFarmacia and E.FolioVenta = G.FolioVenta 
	----		And L.IdClaveSSA_Sal = G.IdClaveSSA) 			
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and E.FolioVenta = @FolioVenta 


	Insert into #tmp_03_Vales_Medicamentos 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioVale, FolioVenta, IdClaveSSA, ClaveSSA, CantidadVale, SurtidoConVale, Tipo, Eliminar   
	) 
	Select Distinct 
		@IdEmpresa, @IdEstado, @IdFarmacia, '' as FolioVale, @FolioVenta as FolioVenta, 
		cast('' as varchar(30)) as IdClaveSSA, cast(I.ClaveSSA as varchar(30)) as ClaveSSA, 	
		cast(0 as int) as CantidadVale, cast('NO' as varchar(10)) as SurtidoConVale, 1 as Tipo, 0 as Eliminar 
	From INT_SIADISSEP__RecetasElectronicas_0004_Insumos I 
	Inner Join #tmp_01_General E (NoLock) 
		On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 	
	Where Not Exists 
	(
		Select * 
		From #tmp_03_Vales_Medicamentos F (NoLock) 
		Where F.IdEmpresa = E.IdEmpresa and F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and F.FolioVenta = E.FolioInterface 
			and F.ClaveSSA = I.ClaveSSA 
	) 	
	
	
	Update E Set CantidadVale = IsNull(( select top 1 CantidadVale 
		From #tmp_03_Vales_Medicamentos I (NoLock) Where I.ClaveSSA = E.ClaveSSA and I.Tipo = 1 ), 0) 
	From #tmp_02_Medicamentos E 
	
	

	Update T Set Eliminar = 1 
	From #tmp_03_Vales_Medicamentos T 
	Where Tipo = 1 
	And Exists 
		(
			Select * 
			From #tmp_03_Vales_Medicamentos T_01 
			Where T.ClaveSSA = T_01.ClaveSSA and Tipo = 0 
		)
	And Exists 
		(
			Select * 
			From #tmp_03_Vales_Medicamentos T_02 
			Where T.ClaveSSA = T_02.ClaveSSA and Tipo = 1 
		)	


	---- Quitar el detalle de lo solicitado 
	Delete from #tmp_03_Vales_Medicamentos Where Eliminar = 1 	
	
	
	
	
	Update M Set CantidadVale = IsNull(P.CantidadVale, 0) -- , CantidadRecetada = ( M.CantidadSurtida + IsNull(P.CantidadVale, 0) )
	From #tmp_02_Medicamentos M (NoLock) 
	Inner Join #tmp_03_Vales_Medicamentos P (NoLock) 
		On ( M.IdEmpresa = P.IdEmpresa and M.IdEstado = P.IdEstado and M.IdFarmacia = P.IdFarmacia and M.FolioVenta = P.FolioVenta and M.ClaveSSA = P.ClaveSSA ) 		
		-- On ( I.IdEmpresa = E.IdEmpresa and I.IdEstado = E.IdEstado and I.IdFarmacia = E.IdFarmacia and I.Folio = E.FolioInterface ) 		
	
	
	
	
	
	Select distinct -- Top 0 
		@IdEmpresa as IdEmpresa,@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		cast('NO' as varchar(10)) as SurteEnOtraUnidad, 
		-- cast('010.000.0891.00' as varchar(30)) as ClaveSSA, cast('00021600' as varchar(30)) as FolioVenta 
		ClaveSSA, FolioVenta 		
	Into #tmp_04_SurtidoOtraUnidad 
	From #tmp_02_Medicamentos 
	--Where 1 = 0 
		
	Select distinct -- Top 0 
		@IdEmpresa as IdEmpresa,@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		cast('NO' as varchar(10)) as SurteEnOtraUnidad, 
		-- cast('010.000.0891.00' as varchar(30)) as ClaveSSA, cast('00021600' as varchar(30)) as FolioVenta 
		ClaveSSA, FolioVenta 		
	Into #tmp_05_SurtidoOtraUnidad_Medicamentos 	
	From #tmp_02_Medicamentos 	
	--Where 1 = 0 
------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles 	
	
	
--------------------------- SALIDA FINAL DE DATOS 
	Select * From #tmp_01_General 
	Select * From #tmp_02_Medicamentos Order by ClaveSSA 
	Select * From #tmp_03_Vales_Medicamentos Order by ClaveSSA 	
	Select * From #tmp_04_SurtidoOtraUnidad Order by ClaveSSA 
	Select * From #tmp_05_SurtidoOtraUnidad_Medicamentos Order by ClaveSSA 
	
End 
Go--#SQL 
	