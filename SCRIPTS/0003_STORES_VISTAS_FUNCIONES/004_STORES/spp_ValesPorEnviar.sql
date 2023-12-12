If Exists ( Select Name From Sysobjects Where Name = 'spp_ValesPorEnviar' and xType = 'P' )
	Drop Proc spp_ValesPorEnviar 
Go--#SQL 

--Exec spp_Consiliacion_AgregarActualizar @IdEmpresa = '001', @IdEstado = '', @IdProveedor = '0001', @TipoDeOrden = 0, @CantidadPagos = 981023.0231

Create Proc spp_ValesPorEnviar
With Encryption 
As
Begin 
Set NoCount On 

	Select top 200 E.*
	Into #Vales_EmisionEnc
	From Vales_EmisionEnc E (NoLock)
	Where Not Exists (Select *
					  From Vales_Emision_Envio X (NoLock)
					  Where E.IdEmpresa = X.IdEmpresa And E.IdEstado = X.IdEstado And E.IdFarmacia = X.IdFarmacia And E.FolioVale = X.FolioVale )
	
	Select
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVale As Folio_Vale, E.FechaRegistro As FechaEmision_Vale, E.IdPersonal,
		B.Nombre As Beneficiario_Nombre, B.ApPaterno As Beneficiario_ApPaterno, B.ApMaterno As Beneficiario_ApMaterno, B.Sexo As Beneficiario_Sexo,
		B.FechaNacimiento as Beneficiario_FechaNacimiento, B.FolioReferencia Beneficiario_FolioReferencia, B.FechaFinVigencia As Beneficiario_FechaFinVigencia,
		A.IdTipoDeDispensacion, A.NumReceta, A.FechaReceta,
		M.Nombre As Medico_Nombre, M.ApPaterno As Medico_ApPaterno, M.ApMaterno As Medico_ApMaterno, M.NumCedula As Medico_NumCedula,
		A.IdBeneficio, A.IdDiagnostico, A.RefObservaciones
	From #Vales_EmisionEnc E (NoLock)
	Inner Join Vales_Emision_InformacionAdicional A (NoLock)
		On (E.IdEmpresa = A.IdEmpresa And E.IdEstado = A.IdEstado And E.IdFarmacia = A.IdFarmacia And E.FolioVale = A.FolioVale)
	Inner Join CatBeneficiarios B (NoLock)
		On (E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And E.IdCliente = B.IdCliente And E.IdSubCliente = B.IdSubCliente And A.IdBeneficiario = B.IdBeneficiario)
	Inner Join CatMedicos M (NoLock)
		On (E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And A.IdMedico = M.IdMedico)
	Order BY 1,2,3,4
	
	
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVale As Folio_Vale, A.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionClave, Cast(A.Cantidad As Int) As Piezas
	From #Vales_EmisionEnc E
	Inner Join Vales_EmisionDet A (NOLock)
		On (E.IdEmpresa = A.IdEmpresa And E.IdEstado = A.IdEstado And E.IdFarmacia = A.IdFarmacia And E.FolioVale = A.FolioVale)
	Inner Join vw_ClavesSSA_Sales S (NoLock)
		On (A.IdClaveSSA_Sal = S.IdClaveSSA_Sal)
	Order BY 1,2,3,4
		
End 
Go--#SQL 