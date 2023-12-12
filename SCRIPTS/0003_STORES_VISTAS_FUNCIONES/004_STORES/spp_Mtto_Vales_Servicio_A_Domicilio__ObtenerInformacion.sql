If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_Servicio_A_Domicilio__ObtenerInformacion' and xType = 'P' )
    Drop Proc spp_Mtto_Vales_Servicio_A_Domicilio__ObtenerInformacion
Go--#SQL 
  
Create Proc spp_Mtto_Vales_Servicio_A_Domicilio__ObtenerInformacion 
(	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(4) = '21', @IdFarmacia varchar(6) = '2132',   
	@DiasRevision int = 10 
 )
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@FechaInicial varchar(10), 
	@FechaFinal varchar(10) 
	

Declare 
	@sFiltro varchar(max), 
	@sSql__Servicios varchar(max), 	
	@sSql__Vales varchar(max), 	
	@sSql__ValesDetalles varchar(max), 	
	@sSql__ValesInformacion varchar(max), 	
	@sSql__Medicos varchar(max),  
	@sSql__Beneficiario varchar(max), 
	@sSql__General varchar(max) 
		
	
	Set @sFiltro = '' 
	Set @sSql__General = '' 
	Set @FechaInicial = convert(varchar(10), dateadd(dd, -1 * @DiasRevision, getdate()), 120) 	
	Set @FechaFinal = convert(varchar(10), getdate(), 120) 

	----Set @FechaInicial = convert(varchar(10), dateadd(dd, -1 * (@DiasRevision * 5), getdate()), 120) 	
	----Set @FechaFinal = convert(varchar(10), dateadd(dd, -1 * (@DiasRevision * 5), getdate()), 120) 		

--	Select @FechaInicial, @FechaFinal 

--------------------------------------------- OBTENER INFORMACION 
------------------- SERVICIOS 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio, FolioVale, FechaRegistro, IdPersonal, 
		HoraVisita_Desde, HoraVisita_Hasta, ServicioConfirmado, FechaConfirmacion, IdPersonalConfirma, 
		TipoSurtimiento, ReferenciaSurtimiento, Status, Actualizado, FechaControl 
	Into #tmpServicios 	
	From Vales_Servicio_A_Domicilio (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ServicioConfirmado = 0 
		and convert(varchar(10), FechaRegistro, 120) Between @FechaInicial and @FechaFinal 

------------------- VALES  	
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioVale, FolioVenta, FechaRegistro, FechaCanje, 
		IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Status, Actualizado, 
		FolioCierre, EsSegundoVale, FechaControl, IdPersonaFirma, FolioTimbre 
	Into #tmpVales 	
	From Vales_EmisionEnc V (NoLock) 	
	Where Status = 'A' and 
	Exists 
	(
		Select * 
		From #tmpServicios S (NoLock) 
		Where V.IdEmpresa = S.IdEmpresa and V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.FolioVale = S.FolioVale 
	) 
	
------------------- VALES DETALLES  	
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVale, V.IdClaveSSA_Sal, V.Cantidad, V.IdPresentacion, V.Status, V.Actualizado, 
		V.Cantidad_2, V.FechaControl
	Into #tmpValesDetalles  	
	From Vales_EmisionDet V (NoLock) 	
	Inner Join #tmpVales E (NoLock) 
		On ( V.IdEmpresa = E.IdEmpresa and V.IdEstado = E.IdEstado and V.IdFarmacia = E.IdFarmacia and V.FolioVale = E.FolioVale )  	
	----Where Exists 
	----(
	----	Select * 
	----	From #tmpServicios S (NoLock) 
	----	Where V.IdEmpresa = S.IdEmpresa and V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.FolioVale = S.FolioVale 
	----) 	
	
------------------- VALES INFORMACION ADICIONAL 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVale, V.IdBeneficiario, V.IdTipoDeDispensacion, V.NumReceta, V.FechaReceta, 
		V.IdMedico, V.IdBeneficio, V.IdDiagnostico, V.IdUMedica, V.IdServicio, V.IdArea, V.RefObservaciones, V.Status, V.Actualizado, V.FechaControl, 
		E.IdCliente, E.IdSubCliente 
	Into #tmpValesInformacionAdicional   	
	From Vales_Emision_InformacionAdicional V (NoLock) 	
	Inner Join #tmpVales E (NoLock) 
		On ( V.IdEmpresa = E.IdEmpresa and V.IdEstado = E.IdEstado and V.IdFarmacia = E.IdFarmacia and V.FolioVale = E.FolioVale )  
	----Where Exists 
	----(
	----	Select * 
	----	From #tmpServicios S (NoLock) 
	----	Where V.IdEmpresa = S.IdEmpresa and V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.FolioVale = S.FolioVale 
	----)
	
	
------------------- MÉDICOS 
	Select 
		IdEstado, IdFarmacia, IdMedico, Nombre, ApPaterno, ApMaterno, NumCedula, 
		IdEspecialidad, Status, Actualizado, FechaControl
	Into #tmpMedicos 	
	From CatMedicos V (NoLock) 	
	Where Exists 
	(
		Select * 
		From #tmpValesInformacionAdicional S (NoLock) 
		Where V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.IdMedico = S.IdMedico 
	)

-------------------	BENEFICIARIOS 	
	Select 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Nombre, ApPaterno, ApMaterno, 
		Sexo, FechaNacimiento, FolioReferencia, 
		FechaInicioVigencia, FechaFinVigencia, FechaRegistro, Status, Actualizado, 
		Domicilio, FechaControl, FolioReferenciaAuxiliar
	Into #tmpBeneficiarios  	
	From CatBeneficiarios V (NoLock) 	
	Where Exists 
	(
		Select * 
		From #tmpValesInformacionAdicional S (NoLock) 
		Where V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.IdCliente = S.IdCliente and V.IdSubCliente = S.IdSubCliente 
			and V.IdBeneficiario = S.IdBeneficiario 
	)
			
--------------------------------------------- OBTENER INFORMACION 

---			spp_Mtto_Vales_Servicio_A_Domicilio__ObtenerInformacion  

----------------------------------------------------------------------------------------
--------------------------------------------  SALIDA FINAL  
	Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + 
		' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' and ServicioConfirmado = 0 
		and convert(varchar(10), FechaRegistro, 120) Between ' + char(39) + @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal + char(39) 	
	Exec spp_CFG_ObtenerDatos @Tabla = 'Vales_Servicio_A_Domicilio', @Criterio = @sFiltro, @Alias = 'S', @Ejecutar = 0, @Salida = @sSql__Servicios output  
	----Exec( @sSql__Servicios ) 


	--	Status = ' + char(39) + 'A' + char(39) + ' and  
	--	#tmpServicios 
	Set @sFiltro = '
	Where 
	Exists 
	(
		Select * 
		From #tmpVales S (NoLock) 
		Where V.IdEmpresa = S.IdEmpresa and V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.FolioVale = S.FolioVale 
	)'  
	Exec spp_CFG_ObtenerDatos @Tabla = 'Vales_EmisionEnc', @Criterio = @sFiltro, @Alias = 'V', @Ejecutar = 0, @Salida = @sSql__Vales output  
	----Exec( @sSql__Vales ) 
		
		
	Set @sFiltro = '
	Where  
	Exists 
	(
		Select * 
		From #tmpVales S (NoLock) 
		Where V.IdEmpresa = S.IdEmpresa and V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.FolioVale = S.FolioVale 
	)'  
	Exec spp_CFG_ObtenerDatos @Tabla = 'Vales_EmisionDet', @Criterio = @sFiltro, @Alias = 'V', @Ejecutar = 0, @Salida = @sSql__ValesDetalles output  
	----Exec( @sSql__ValesDetalles ) 
	

	Set @sFiltro = '
	Where 
	Exists 
	(
		Select * 
		From #tmpVales S (NoLock) 
		Where V.IdEmpresa = S.IdEmpresa and V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.FolioVale = S.FolioVale 
	)'  
	Exec spp_CFG_ObtenerDatos @Tabla = 'Vales_Emision_InformacionAdicional', @Criterio = @sFiltro, @Alias = 'V', @Ejecutar = 0, @Salida = @sSql__ValesInformacion output  
	---- Exec( @sSql__ValesInformacion )
	
	
	
	Set @sFiltro = '
	Where Exists 
	(
		Select * 
		From #tmpValesInformacionAdicional S (NoLock) 
		Where V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.IdMedico = S.IdMedico 
	)'  
	Exec spp_CFG_ObtenerDatos @Tabla = 'CatMedicos', @Criterio = @sFiltro, @Alias = 'V', @Ejecutar = 0, @Salida = @sSql__Medicos output  
	----Exec( @sSql__Medicos ) 
	
	
	Set @sFiltro = '
	Where Exists 
	(
		Select * 
		From #tmpValesInformacionAdicional S (NoLock) 
		Where V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.IdCliente = S.IdCliente and V.IdSubCliente = S.IdSubCliente 
			and V.IdBeneficiario = S.IdBeneficiario 
	)'  
	Exec spp_CFG_ObtenerDatos @Tabla = 'CatBeneficiarios', @Criterio = @sFiltro, @Alias = 'V', @Ejecutar = 0, @Salida = @sSql__Beneficiario output  
	----Exec( @sSql__Beneficiario ) 
		
		
------------------------------------- SALIDA 
	Exec( @sSql__Beneficiario ) 
	Exec( @sSql__Medicos ) 
	Exec( @sSql__Servicios ) 			
	Exec( @sSql__Vales ) 
	Exec( @sSql__ValesDetalles ) 	
	Exec( @sSql__ValesInformacion ) 
		
	----Set @sSql__General = @sSql__Beneficiario + char(13) + char(10) + @sSql__Medicos + char(13) + char(10) + @sSql__Servicios + char(13) + char(10) + 
	----@sSql__Vales + char(13) + char(10) + @sSql__ValesDetalles + char(13) + char(10) + @sSql__ValesInformacion + char(13) 
	----Print @sSql__General 
		
---			spp_Mtto_Vales_Servicio_A_Domicilio__ObtenerInformacion  	
	
	
	----@sSql__Medicos varchar(max),  
	----@sSql__Beneficiario varchar(max) 


/* 
	Select 
		IdEstado, IdFarmacia, IdMedico, Nombre, ApPaterno, ApMaterno, NumCedula, 
		IdEspecialidad, Status, Actualizado, FechaControl
	From #tmpMedicos 	
	
	Select 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Nombre, ApPaterno, ApMaterno, 
		Sexo, FechaNacimiento, FolioReferencia, 
		FechaInicioVigencia, FechaFinVigencia, FechaRegistro, Status, Actualizado, 
		Domicilio, FechaControl, FolioReferenciaAuxiliar
	From #tmpBeneficiarios  		
	
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioVale, FolioVenta, FechaRegistro, FechaCanje, 
		IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Status, Actualizado, 
		FolioCierre, EsSegundoVale, FechaControl, IdPersonaFirma, FolioTimbre 
	From #tmpVales 	

	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioVale, IdClaveSSA_Sal, Cantidad, IdPresentacion, Status, Actualizado, Cantidad_2, FechaControl
	From #tmpValesDetalles  	

	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioVale, IdBeneficiario, IdTipoDeDispensacion, NumReceta, FechaReceta, 
		IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, RefObservaciones, Status, Actualizado, FechaControl
	From #tmpValesInformacionAdicional   	

	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio, FolioVale, FechaRegistro, IdPersonal, 
		HoraVisita_Desde, HoraVisita_Hasta, ServicioConfirmado, FechaConfirmacion, IdPersonalConfirma, 
		TipoSurtimiento, ReferenciaSurtimiento, Status, Actualizado, FechaControl 
	From #tmpServicios 	
*/ 


---			spp_Mtto_Vales_Servicio_A_Domicilio__ObtenerInformacion  


End 
Go--#SQL	

--	sp_listacolumnas CatBeneficiarios   
