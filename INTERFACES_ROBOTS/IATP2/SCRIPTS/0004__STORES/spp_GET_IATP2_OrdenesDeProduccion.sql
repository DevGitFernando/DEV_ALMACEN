-------------------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_GET_IATP2_OrdenesDeProduccion' and xType = 'P' ) 
   Drop Proc spp_GET_IATP2_OrdenesDeProduccion
Go--#SQL 

Create Proc spp_GET_IATP2_OrdenesDeProduccion 
( 
	@FolioSolicitud varchar(10) = '1', 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '3132'  


) 
With Encryption
As 
Begin 
Set DateFormat YMD  
Declare @sMensaje varchar(500)  


--------------------- FORMATEAR DATOS 
	Set @FolioSolicitud = right('000000000000000000' + @FolioSolicitud, 10) 
	Set @IdEmpresa = right('000000000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000000000000' + @IdFarmacia, 4) 
--------------------- FORMATEAR DATOS 


--------------------- OBTENER INFORMACION 
	Select 
		O.FolioSolicitud, O.IdEmpresa, O.IdEstado, O.IdFarmacia, 
		O.IdCliente, C.NombreCliente, O.IdSubCliente, C.NombreSubCliente, 
		O.IdPrograma, P.Programa, O.IdSubPrograma, P.SubPrograma, 
		O.FechaRegistro, O.NumeroDeDocumento, O.Observaciones, O.Status  
	Into #tmp__01_IATP2_OrdenesDeProduccion 
	From IATP2_OrdenesDeProduccion O (NoLock) 
	Inner Join vw_Clientes_SubClientes C (NoLock) On ( O.IdCliente = C.IdCliente and O.IdSubCliente = C.IdSubCliente ) 
	Inner Join vw_Programas_SubProgramas P (NoLock) On ( O.IdPrograma = P.IdPrograma and O.IdSubPrograma = P.IdSubPrograma ) 
	Where O.FolioSolicitud = @FolioSolicitud and O.IdEmpresa = @IdEmpresa and O.IdEstado = @IdEstado and O.IdFarmacia = @IdFarmacia 

-----		spp_GET_IATP2_OrdenesDeProduccion  


	Select 
		O.FolioSolicitud, O.Consecutivo, O.IdBeneficiario, O.IdTipoDeDispensacion, O.NumeroDeHabitacion, O.NumeroDeCama, O.NumReceta, 
		O.FechaReceta, 

		O.IdMedico, 
		(M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) as NombrePreescribe, 
		M.ApPaterno as ApPaterno_Medico, M.ApMaterno as ApMaterno_Medico, M.Nombre as Nombre_Medico, 
		M.NumCedula, 

		O.IdBeneficio, (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as NombreDeBeneficiario, B.FolioReferencia, 
		B.ApPaterno as ApPaterno_Paciente, B.ApMaterno as ApMaterno_Paciente, B.Nombre as Nombre_Paciente, 

		B.FechaNacimiento as FechaNacimiento_Paciente, '0000000000' as Telefono_Paciente, 
		O.IdDiagnostico, O.IdUMedica, 
		O.IdServicio, O.IdArea, O.RefObservaciones   
	Into #tmp__02_IATP2_OrdenesDeProduccion_InformacionAdicional 
	From IATP2_OrdenesDeProduccion_InformacionAdicional O (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) On ( O.IdEstado = B.IdEstado and O.IdFarmacia = B.IdFarmacia and O.IdBeneficiario = B.IdBeneficiario ) 
	Inner Join CatMedicos M (NoLock) On ( O.IdEstado = M.IdEstado and O.IdFarmacia = M.IdFarmacia and O.IdMedico = M.IdMedico )
	Where O.FolioSolicitud = @FolioSolicitud and O.IdEmpresa = @IdEmpresa and O.IdEstado = @IdEstado and O.IdFarmacia = @IdFarmacia 
	Order by O.FolioSolicitud, O.Consecutivo  


	Select 
		O.FolioSolicitud, O.Consecutivo, O.Partida, O.CodigoEAN, O.IdProducto, P.ClaveSSA, P.Descripcion, O.CantidadSolicitada, O.CantidadSurtida, 
		O.FechaHora_De_Administracion, 
		O.FechaRegistro, O.FechaSurtido, O.Status_ATP2, O.Status, O.Actualizado 
	Into #tmp__03_IATP2_OrdenesDeProduccion_Productos 
	From IATP2_OrdenesDeProduccion_Productos O (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( O.IdProducto = P.IdProducto and O.CodigoEAN = P.CodigoEAN )  
	Where O.FolioSolicitud = @FolioSolicitud 
	Order by O.FolioSolicitud, O.Consecutivo, O.Partida 
	 


	Select 
		(E.FolioSolicitud  + '' + right('0000' + cast(I.Consecutivo as varchar(4)), 4) + '' + E.IdEmpresa + '' + E.IdEstado + '' + E.IdFarmacia) as FolioAcondicionamiento, 

		E.FolioSolicitud, E.IdEmpresa, E.IdEstado, E.IdFarmacia, 
		E.IdCliente, E.NombreCliente, E.IdSubCliente, E.NombreSubCliente, 
		E.IdPrograma, E.Programa, E.IdSubPrograma, E.SubPrograma, 
		E.FechaRegistro, E.NumeroDeDocumento, E.Observaciones, E.Status, 
		
		I.Consecutivo, I.IdBeneficiario, I.IdTipoDeDispensacion, I.NumeroDeHabitacion, I.NumeroDeCama, I.NumReceta, 
		I.FechaReceta, 
		I.IdMedico, I.NombrePreescribe, 
		I.ApPaterno_Medico, I.ApMaterno_Medico, I.Nombre_Medico, I.NumCedula, 
		I.IdBeneficio, I.NombreDeBeneficiario, 
		I.ApPaterno_Paciente, I.ApMaterno_Paciente, I.Nombre_Paciente, I.FechaNacimiento_Paciente, I.Telefono_Paciente, 
		I.FolioReferencia, 
		I.IdDiagnostico, I.IdUMedica, 
		I.IdServicio, I.IdArea, I.RefObservaciones, 

		D.Partida, 			   
		D.CodigoEAN, D.IdProducto, D.ClaveSSA, D.Descripcion, 
		D.CantidadSolicitada as CantidadSolicitada_Base, cast(D.CantidadSolicitada as varchar(20)) as CantidadSolicitada, 
		D.FechaHora_De_Administracion 

	Into #tmp__04_IATP2_OrdenesDeProduccion_OrdenDeAcondicionamiento
	From #tmp__01_IATP2_OrdenesDeProduccion E (NoLock) 
	Inner Join #tmp__02_IATP2_OrdenesDeProduccion_InformacionAdicional I (NoLock) On ( E.FolioSolicitud = I.FolioSolicitud )   
	Inner Join #tmp__03_IATP2_OrdenesDeProduccion_Productos D (NoLock) On ( I.FolioSolicitud = D.FolioSolicitud and I.Consecutivo = D.Consecutivo )  
	Order by D.FolioSolicitud, D.Consecutivo, D.Partida 


--------------------- OBTENER INFORMACION 


-----		spp_GET_IATP2_OrdenesDeProduccion  


--------------------- SALIDA FINAL 

	Select * From #tmp__01_IATP2_OrdenesDeProduccion (NoLock) 
	Select * From #tmp__02_IATP2_OrdenesDeProduccion_InformacionAdicional (NoLock) 
	Select * From #tmp__03_IATP2_OrdenesDeProduccion_Productos (NoLock) 

	Select * From #tmp__04_IATP2_OrdenesDeProduccion_OrdenDeAcondicionamiento 

--------------------- SALIDA FINAL 


End 
Go--#SQL 



