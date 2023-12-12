
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_Secretaria_Vales' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_Secretaria_Vales
Go--#SQL 

Create Proc spp_Rpt_Administrativos_Secretaria_Vales 
(   @IdEstado varchar(2) = '25', --@IdFarmacia varchar(4) = '0010', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001', 
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2011-11-31', @TipoInsumo tinyint = 0,
	@TipoInsumoMedicamento tinyint = 0, 
	@EncPrincipal varchar(500) = '', @EncSecundario varchar(500) = '' 
)  
With Encryption 
As 
Begin 
Set NoCount On
	
		--------------------------------------------
		-- Se obtienen los vales que tiene receta --
		--------------------------------------------
		Select Distinct @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
			S.IdEstado, S.Estado, S.IdFarmacia, S.Farmacia, S.Folio as FolioVenta, E.FolioVale, 
			E.IdCliente, S.NombreCliente, E.IdSubCliente, S.NombreSubCliente, 
			E.IdPrograma, S.Programa, E.IdSubPrograma, S.SubPrograma,  S.NumReceta, S.FechaReceta, 
			S.IdBeneficiario, S.Beneficiario, S.FolioReferencia, 
			D.IdClaveSSA_Sal, C.ClaveSSA_Base as ClaveSSA, C.DescripcionCortaClave, D.Cantidad, 0 as PrecioLicitacion, 0 as Importe, 
			D.IdPresentacion, P.Descripcion as Presentacion, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 1 as TieneReceta  
		Into #tmpVales 
		From RptAdmonDispensacion_Secretaria S(NoLock) 
		Inner Join Vales_EmisionEnc E(NoLock) On ( S.IdEstado = E.IdEstado And S.IdFarmacia = E.IdFarmacia And S.Folio = E.FolioVenta ) 
		Inner Join Vales_EmisionDet D(NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVale = D.FolioVale )
		Inner Join CatClavesSSA_Sales C(NoLock) On ( D.IdClaveSSA_Sal = C.IdClaveSSA_Sal )
		Inner Join CatPresentaciones P (NoLock) On (D.IdPresentacion = P.IdPresentacion ) 
		Where E.Status = 'R' And E.FolioVenta <> ''
		Order By FolioVale, FolioVenta, IdClaveSSA_Sal

		------------------------------------------------
		-- Se obtienen los vales que no tienen receta --
		------------------------------------------------
		Insert Into #tmpVales
		Select	@EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
				E.IdEstado, Es.Nombre as Estado, E.IdFarmacia, F.Farmacia, E.FolioVenta, E.FolioVale, 
				E.IdCliente, '' as NombreCliente, E.IdSubCliente, '' as NombreSubCliente, 
				E.IdPrograma, '' as Programa, E.IdSubPrograma, '' as SubPrograma, I.NumReceta, I.FechaReceta, 
				I.IdBeneficiario, '' as Beneficiario, '' as FolioReferencia, 
				D.IdClaveSSA_Sal, S.ClaveSSA_Base as ClaveSSA, S.DescripcionCortaClave, D.Cantidad,  0 as PrecioLicitacion, 0 as Importe, 
				D.IdPresentacion, P.Descripcion as Presentacion, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, 0 as TieneReceta   
		From Vales_EmisionEnc E(NoLock)
		Inner Join Vales_EmisionDet D(NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVale = D.FolioVale )
		Inner Join Vales_Emision_InformacionAdicional I On ( E.IdEmpresa = I.IdEmpresa And E.IdEstado = I.IdEstado And E.IdFarmacia = I.IdFarmacia And E.FolioVale = I.FolioVale )
		Inner Join vw_Farmacias F(NoLock) On (E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia ) 
		Inner Join CatClavesSSA_Sales S(NoLock) On ( D.IdClaveSSA_Sal = S.IdClaveSSA_Sal )
		Inner Join CatPresentaciones P (NoLock) On (D.IdPresentacion = P.IdPresentacion ) 
		Inner Join CatEstados Es(NoLock) On ( E.IdEstado = Es.IdEstado )
		Where E.Status = 'R' And E.FolioVenta = '' And Convert(varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal
		Order By FolioVale, FolioVenta, IdClaveSSA_Sal
		
		----------------------------------------
		-- Se actualizan los campos sin valor --
		----------------------------------------
		Update E Set NombreCliente = S.NombreCliente, E.NombreSubCliente = S.NombreSubCliente, E.Programa = P.Programa, E.SubPrograma = P.SubPrograma
		From #tmpVales E(NoLock)
		Inner Join vw_Clientes_SubClientes S(NoLock) On ( E.IdCliente = S.IdCliente And E.IdSubCliente = S.IdSubCliente ) 
		Inner Join vw_Programas_SubProgramas P(NoLock) On ( E.IdPrograma = P.IdPrograma And E.IdSubPrograma = P.IdSubPrograma ) 

		Update E Set Beneficiario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre), FolioReferencia = B.FolioReferencia
		From #tmpVales E(NoLock) 
		Inner Join CatBeneficiarios B(NoLock) On ( E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia 
			And E.IdCliente = B.IdCliente And E.IdSubCliente = B.IdSubCliente And E.IdBeneficiario = B.IdBeneficiario )

		----------------------------
		-- Se devuelven los datos --
		----------------------------
		If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Rpt_Admon_No_Dispensado_Secretaria' And xType = 'U' )
		  Begin
			Drop Table Rpt_Admon_No_Dispensado_Secretaria
		  End	
		
		Select *
		Into Rpt_Admon_No_Dispensado_Secretaria
		From #tmpVales(NoLock)
		--Order By IdEstado, IdFarmacia, FolioVenta

		Select * From Rpt_Admon_No_Dispensado_Secretaria(NoLock) Order By IdEstado, IdFarmacia, FolioVenta
		
		-- Select * From RptAdmonDispensacion_Secretaria (NoLock)
		-- spp_Rpt_Administrativos_Secretaria_Vales
		-- Select * From CatClavesSSA_Sales

End 
Go--#SQL


