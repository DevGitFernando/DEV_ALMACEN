
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Vales_Emitidos_Por_Mes_Excel' And xType = 'P' )
	Drop Proc spp_Rpt_Vales_Emitidos_Por_Mes_Excel
Go--#SQL

Create Procedure spp_Rpt_Vales_Emitidos_Por_Mes_Excel
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmacia varchar(4) = '111', @iAño int = 2018, @iMes int = 10
) 
With Encryption
As
Begin 
	Declare @sNombreMes varchar(50)

	Set DateFormat YMD 
	Set NoCount On
	Set @sNombreMes = ''  
	
	Set @IdEmpresa = right('00000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000' + @IdEstado, 2) 
	Set @IdFarmacia = right('00000' + @IdFarmacia, 4) 


	----------------------------------
	-- Se obtiene el nombre del Mes -- 
	----------------------------------
	Exec @sNombreMes = dbo.fg_NombresDeMesNumero @iMes
	
	-------------------------------------
	-- Se obtienen los Vales Emitidos  --
	-------------------------------------
	Select	E.IdEmpresa, space(100) as Empresa, 
			E.IdEstado, space(50) as Estado, 
			E.IdFarmacia, space(100) as Farmacia, 
			E.FolioVale, FolioVenta, space(20) as NumReceta,
			D.IdclaveSSA_Sal, 
			--cast('' as varchar(50)) as ClaveSSA, cast('' as varchar(5000)) as DescripcionSal,Cantidad, 
			P.ClaveSSA, P.DescripcionSal, Cantidad,  
			space(8) as IdBeneficiario, space(200) as Beneficiario,
			CAST('' As Varchar(6)) As IdMedico, CAST('' As Varchar(300)) As Medico,
			space(20) as Poliza, 
			Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, 
			Convert( varchar(10), FechaCanje, 120) as FechaCanje,
			CAST('' As Varchar(10)) As FechaReceta,
			IdCliente, space(100) as Cliente, 
			IdSubCliente, space(100) as SubCliente, 
			IdPrograma, space(100) as Programa, 
			IdSubPrograma, space(100) as SubPrograma, 
			( Case When E.Status = 'A' Then 'ACTIVO' Else ( Case When E.Status = 'C' Then 'CANCELADO' Else 'REGISTRADO' End ) End ) as Status,
			IdPersonal, CAST('' As varchar(200)) As PersonalRegistra
	Into #tmpEmitidos
	From Vales_EmisionEnc E (NoLock) 
	Inner Join Vales_EmisionDet D (NoLock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVale = D.FolioVale)
	Inner Join vw_ClavesSSA_Sales P (NoLock) On ( D.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia 
		And Year( FechaRegistro ) = @iAño And Month( FechaRegistro ) = @iMes
	Order By E.FolioVale 
 

	-------- Se obtienen los IdBeneficiarios de cada vale
	Update E
	Set IdBeneficiario = P.IdBeneficiario, NumReceta = P.NumReceta, E.FechaReceta = CONVERT(Varchar(10), P.FechaReceta, 120 ),
		E.IdMedico = P.IdMedico, E.Medico = (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) -- M.NombreCompleto
	From #tmpEmitidos E 
	Inner Join Vales_Emision_InformacionAdicional P (NoLock) 
		On ( E.IdEmpresa = P.IdEmpresa And E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.FolioVale = P.FolioVale )
	Inner Join CatMedicos M (NoLock)
		On ( E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And P.IdMedico = M.IdMedico ) 


---		spp_Rpt_Vales_Emitidos_Por_Mes_Excel


	------ Se obtienen los Beneficiarios de cada vale
	Update E
	Set Beneficiario = ( P.Nombre + ' ' + IsNull(P.ApPaterno, '') + ' ' + IsNull(P.ApMaterno, '') ),  
		Poliza = P.FolioReferencia
	From #tmpEmitidos E 
	Inner Join CatBeneficiarios P(NoLock) 
		On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia 
			And E.IdCliente = P.IdCliente And E.SubCliente = E.SubCliente And E.IdBeneficiario = P.IdBeneficiario )
			
	----------  Se obtienen los IdBeneficiarios de cada vale
	Update E
	Set PersonalRegistra = (P.ApPaterno + ' ' + P.ApMaterno + ' ' + P.Nombre) -- NombreCompleto
	From #tmpEmitidos E 
	Inner Join CatPersonal P(NoLock) 
		On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.IdPersonal = P.IdPersonal ) 


	-------- Se obtienen la cantidad emitida de cada vale
	------Select IdEmpresa, IdEstado, IdFarmacia, FolioVale, Sum(Cantidad) as Cantidad 
	------Into #tmpCantidadesEmitidas
	------From Vales_EmisionDet D(NoLock)
	------Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	------	And FolioVale In ( Select FolioVale From #tmpEmitidos(NoLock) )
	------Group By IdEmpresa, IdEstado, IdFarmacia, FolioVale
	------Order By FolioVale 

	------Update E
	------Set Cantidad = P.Cantidad
	------From #tmpEmitidos E 
	------Inner Join #tmpCantidadesEmitidas P(NoLock) 
	------	On ( E.IdEmpresa = P.IdEmpresa And E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.FolioVale = P.FolioVale ) 
 

	----------------------------------------------------------------------------
	-- Se obtienen los nombres de Cliente, SubCliente, Programa y SubPrograma --
	---------------------------------------------------------------------------- 
	Update E
	Set Cliente = Nombre 
	From #tmpEmitidos E 
	Inner Join CatClientes P(NoLock) On ( E.IdCliente = P.IdCliente ) 

	Update E
	Set SubCliente = Nombre 
	From #tmpEmitidos E 
	Inner Join CatSubClientes P(NoLock) On ( E.IdCliente = P.IdCliente And E.IdSubCliente = P.IdSubCliente ) 

	Update E
	Set Programa = Descripcion 
	From #tmpEmitidos E 
	Inner Join CatProgramas P(NoLock) On ( E.IdPrograma = P.IdPrograma ) 

	Update E
	Set SubPrograma = Descripcion 
	From #tmpEmitidos E 
	Inner Join CatSubProgramas P(NoLock) On ( E.IdPrograma = P.IdPrograma And E.IdSubPrograma = P.IdSubPrograma ) 
 


	---------------------------------------------------------------------
	-- Se obtienen los nombres de Empresa,Estado, Farmacia y Proveedor --
	---------------------------------------------------------------------
	-- Se obtiene el nombre de la empresa. 
	Update E
	Set Empresa = Nombre 
	From #tmpEmitidos E 
	Inner Join CatEmpresas P(NoLock) On ( E.IdEmpresa = P.IdEmpresa )

	-- Se obtiene el nombre del estado
	Update E
	Set Estado = Nombre
	From #tmpEmitidos E 
	Inner Join CatEstados P(NoLock) On ( E.IdEstado = P.IdEstado )


	-- Se obtiene el nombre de la farmacia
	Update E
	Set Farmacia = NombreFarmacia 
	From #tmpEmitidos E 
	Inner Join CatFarmacias P(NoLock) On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia )

		
	---------------------------------------------------
	Select * 
	From #tmpEmitidos(NoLock) 
	Order By IdFarmacia, FolioVale 


	-- spp_Rpt_Vales_Por_Mes
End
Go--#SQL

