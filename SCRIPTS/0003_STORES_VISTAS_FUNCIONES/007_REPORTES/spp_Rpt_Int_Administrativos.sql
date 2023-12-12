
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Int_Administrativos' and xType = 'P' ) 
   Drop Proc spp_Rpt_Int_Administrativos 
Go--#SQL

Create Proc spp_Rpt_Int_Administrativos ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0004', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001', @IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@TipoDispensacion tinyint = 1, @FechaInicial varchar(10), @FechaFinal varchar(10), @TipoInsumo tinyint = 0 
	) 
With Encryption 	
As 
Begin 
Set NoCount On 

	-------------------------------------
	-- Solo tomar la Ventas de Credito --
	-------------------------------------

	Select 
		v.IdEmpresa, v.Empresa, v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.Folio, v.FechaSistema, v.FechaRegistro, 
		v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		B.NombreCompleto as Beneficiario, B.FolioReferencia, I.NumReceta, I.FechaReceta, 
		v.EsCorte, v.StatusVenta, v.SubTotal, v.Descuento, v.Iva, v.Total, v.TipoDeVenta, v.NombreTipoDeVenta, 
		v.IdProducto, v.CodigoEAN, v.DescProducto, v.DescripcionCorta, v.UnidadDeSalida, v.TasaIva, v.Cantidad, v.Costo, v.Importe, 
		v.IdMunicipio, v.Municipio, v.IdColonia, v.Colonia, v.Domicilio, I.IdMedico, M.NombreCompleto as Medico, 
		D.IdGrupo, D.GrupoClaves, D.DescripcionGrupo, D.SubGrupo, D.ClaveSubGrupo, D.DescripcionSubGrupo, 
		D.IdDiagnostico, D.ClaveDiagnostico, D.Diagnostico, I.IdServicio, S.Servicio, I.IdArea, S.Area_Servicio as Area  
	Into #tmpRptAdministrativos 
	From vw_Impresion_Ventas v (NoLock) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.FolioVenta ) 
	Inner Join vw_Servicios_Areas S (NoLock)  On ( I.IdServicio = S.IdServicio and I.IdArea = S.IdArea ) 
	Inner Join vw_Medicos M (NoLock) On ( I.IdEstado = M.IdEstado and I.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico ) 
	Inner Join vw_CIE10_Diagnosticos D (NoLock) On ( I.IdDiagnostico = D.ClaveDiagnostico )
	Left Join vw_Beneficiarios B (NoLock) 
		On ( I.IdEstado = B.IdEstado and I.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente 
			 and I.IdBeneficiario = B.IdBeneficiario ) 	
	Where TipoDeVenta = 2 and v.IdEmpresa = @IdEmpresa and v.IdEstado = @IdEstado and v.IdFarmacia = @IdFarmacia 
		  And Convert( varchar(10), FechaSistema, 120) Between @FechaInicial And @FechaFinal

	---------------------------------
	-- Resultado final del Proceso --
	---------------------------------

	Select Folio, Convert( varchar(10), FechaSistema, 120), NombreCliente, Programa, NumReceta, FolioReferencia, Beneficiario, 
		DescripcionCorta, Cantidad
	From #tmpRptAdministrativos (NoLock) 
	
End 
Go--#SQL


