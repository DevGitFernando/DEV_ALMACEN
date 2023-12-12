-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse_Detalles' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse_Detalles
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse_Detalles 
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '113', 
	@FolioVenta varchar(20) = '00001543' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200),   
	@bEnviarDigitalizacion int 

	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2)  
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4)  
	Set @FolioVenta = RIGHT('000000000000' + @FolioVenta, 8) 
	Set @bEnviarDigitalizacion = 0 

	----------------------- Verificar que este activo el envio de imagenes 
	Select @bEnviarDigitalizacion = EnviarDigitalizacion 
	From INT_SESEQ__CFG_Farmacias_UMedicas (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 



------------------------------------------------- OBTENER LA INFORMACION   
	Select 
		X.IdEmpresa, X.IdEstado, X.IdFarmacia, G.Folio as FolioInterface, 
		X.UMedica, 
		G.FolioReceta as Folio_SESEQ, 
		X.TipoDeProceso, X.DisponibleSurtido, X.Surtidos, X.Surtidos_Aplicados, 
		G.EsSurtido, G.FolioSurtido as FolioVenta, G.FechaDeSurtido as FechaDeSurtimiento, 
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
	Inner Join VentasEnc V (NoLock) 
		On ( G.IdEmpresa = V.IdEmpresa and G.IdEstado = V.IdEstado and G.IdFarmacia = V.IdFarmacia and G.FolioSurtido = V.FolioVenta ) 		
	Where 
		G.EsSurtido = 1 and 
		G.Procesado = 1 and 
		G.Procesado_Digitalizacion = 0 and 
		EsCancelado = 0 and G.FolioSurtido = @FolioVenta 
		and G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 
		and @bEnviarDigitalizacion = 1 
	

	------------------------------------------------------------------------------------------------------------------------------------- 	
	Select 
		G.TipoDeProceso, 
		L.IdEmpresa, L.IdEstado, ----L.IdFarmacia, 
		G.IdCliente, G.IdSubCliente, 
		G.FechaSurtido, 
		G.FolioInterface, 
		G.Folio_SESEQ, 
		L.FolioVenta, 
		L.IdImagen, 
		L.TipoDeImagen, (case when L.TipoDeImagen = 1 then 'Tickect / Colectivo' else 'Receta' end ) as TipoDeImagen_Descripcion, 
		L.FechaDigitalizacion,
		ImagenComprimida as Imagen 
	Into #tmp_02_Imagenes 
	From SII_Digitalizacion..VentasDigitalizacion L (NoLock) 
	Inner Join #tmp_01_General G (NoLock) 
		On ( L.IdEmpresa = G.IdEmpresa and L.IdEstado = G.IdEstado and L.IdFarmacia = G.IdFarmacia and L.FolioVenta = G.FolioVenta ) 
	----Inner Join FarmaciaProductos_CodigoEAN_Lotes I (NoLock) 
	----	On ( L.IdEmpresa = I.IdEmpresa and L.IdEstado = I.IdEstado and L.IdFarmacia = I.IdFarmacia ----and L.IdSubFarmacia = I.IdSubFarmacia
	----		-- and L.IdProducto = I.IdProducto and L.CodigoEAN = I.CodigoEAN and L.ClaveLote = I.ClaveLote 
	----		) 
	Where G.IdEmpresa = @IdEmpresa and G.IdEstado = @IdEstado and G.IdFarmacia = @IdFarmacia 	
			and @bEnviarDigitalizacion = 1 

--		spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse_Detalles  



	--select * from #tmp_02_Imagenes 



------------------------------------------------- OBTENER LA INFORMACION   	


	
---		spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse_Detalles 	
	
	

--------------------------- SALIDA FINAL DE DATOS 
	Select * 
	From #tmp_02_Imagenes 

	
End 
Go--#SQL 
	
