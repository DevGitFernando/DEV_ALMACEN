------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_OP_Ventas__01_Salidas' and xType = 'P' )  
    Drop Proc spp_Rpt_OP_Ventas__01_Salidas  
Go--#SQL

Create Proc spp_Rpt_OP_Ventas__01_Salidas
( 
	@IdEmpresa Varchar(3) = '1', @IdEstado varchar(2) = '13', @IdFarmacia Varchar(4) = '113', 
	@IdCliente Varchar(4) = '2', @IdSubCliente Varchar(4) = '10', @IdPrograma Varchar(4) = '', @IdSubPrograma Varchar(4) = '',
	@FechaIncial varchar(10) = '2018-06-01', @FechaFinal varchar(10) = '2018-06-05', @IdBeneficiario Varchar(8) = '',  @ConcentradoReporte smallint = 0, 
	@AplicarMascara smallint = 1 
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sWhereCliente Varchar(200), 
	@sWhereSubCliente Varchar(200), 
	@sWherePrograma Varchar(200), 
	@sWhereSubPrograma Varchar(200),
	@sWhereBeneficiario Varchar(200),
	@sSql Varchar(Max)  
	
	Set @sWhereCliente = ''
	Set @sWhereSubCliente = ''
	Set @sWherePrograma  = ''
	Set @sWhereSubPrograma = ''
	Set @sWhereBeneficiario  = ''
	
	
	/*
		TipoReporte 
		1 = Venta
		2 = Devolucion
		3 = No Surtido	
	*/


	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3)
	Set @IdEstado = right('00000000' + @IdEstado, 2)
	Set @IdFarmacia = right('00000000' + @IdFarmacia, 4)

---------------------------------------------------------- FILTROS 	
	if (@IdCliente <> '') 
    Begin
		Set @IdCliente = right('00000000' + @IdCliente, 4) 
		Set @sWhereCliente = ' and IdCliente = ' + Char(39) + @IdCliente + Char(39)
    End
    
    if (@IdSubCliente <> '')
    Begin 
		Set @IdSubCliente = right('00000000' + @IdSubCliente, 4)  
		Set @sWhereSubCliente = ' and IdSubCliente = ' + Char(39) + @IdSubCliente + Char(39)
    End
	
    if (@IdPrograma <> '')
    Begin
		Set @IdPrograma = right('00000000' + @IdPrograma, 4)  
		Set @sWherePrograma = ' and IdPrograma = ' + Char(39) + @IdPrograma + Char(39)
    End
    
    if (@IdSubPrograma <> '')
    Begin
		Set @IdSubPrograma = right('00000000' + @IdSubPrograma, 4) 
		Set @sWhereSubPrograma = ' And IdSubPrograma = '  + Char(39) + @IdSubPrograma  + Char(39)
    End 

    if (@IdBeneficiario <> '')
    Begin
		Set @IdBeneficiario = right('00000000000' + @IdBeneficiario, 8) 
		Set @sWhereBeneficiario = ' And IdBeneficiario = '  + Char(39) + @IdBeneficiario  + Char(39)
    End 
---------------------------------------------------------- FILTROS 	


--		spp_Rpt_OP_Ventas__04_NoSurtido    

----------------------------- GENERAR TABLAS DE PROCESO 		
		Select Top 0 *
		Into #TmpVentas
		From VentasEnc I (NoLock) 
		
		
		Select Top 0 *
		Into #TmpVentasInformacionAdicional
		From VentasInformacionAdicional I (NoLock) 
----------------------------- GENERAR TABLAS DE PROCESO 


		Set @sSql = 
			'Insert Into #TmpVentas 
			Select *  
			From VentasEnc I (NoLock) 
			Where I.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' and I.IdEstado = '  + Char(39) + @IdEstado + Char(39) +
			'  and I.IdFarmacia = '+ Char(39) + @IdFarmacia + Char(39) +
			'  And Convert(Varchar(10), I.FechaRegistro, 120) between ' + Char(39) + @FechaIncial + Char(39) + ' And ' + Char(39) + @FechaFinal + Char(39) +
			@sWhereCliente + @sWhereSubCliente + @sWherePrograma + @sWhereSubPrograma + Char(13) 
		Exec(@sSql)
		
		Set @sSql = 
			'Insert Into #TmpVentasInformacionAdicional
			Select *  
			From VentasInformacionAdicional I (NoLock) 
			Where I.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' and I.IdEstado = '  + Char(39) + @IdEstado + Char(39) +
			'  and I.IdFarmacia = '+ Char(39) + @IdFarmacia + Char(39) + @sWhereBeneficiario + Char(13) 
		Exec(@sSql)  


		Select 
				I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.IdCliente, I.IdSubCliente, I.FechaRegistro, I.FolioVenta As Folio, A.RefObservaciones, A.NumReceta,
				A.IdBeneficiario, cast('' as Varchar(200)) As Beneficiario,
				I.IdPrograma, cast('' as Varchar(200)) As Programa, I.IdSubPrograma, cast('' as Varchar(200)) As SubPrograma,
				I.IdPersonal, cast('' as Varchar(200)) As NombrePersonal, P.IdClaveSSA_Sal  As IdClaveSSA, P.ClaveSSA_Aux, 
				cast(P.DescripcionCortaClave as varchar(max)) as DescripcionCortaClave, 
				D.IdProducto, P.DescripcionCorta, D.CodigoEAN, D.ClaveLote, cast('' as Varchar(200)) As FechaCad, ISNULL(U.CantidadVendida, D.CantidadVendida) As Cantidad,
				IsNull(cast(U.IdPasillo As Varchar(10)), '') As IdPasillo, IsNull(cast(U.IdEstante As Varchar(10)), '') As IdEstante, 
				IsNull(cast(U.IdEntrepaño As Varchar(10)), '') As IdEntrepaño, 0 as Relacionada 
		Into #tmpFinal 
		From #TmpVentas I (NoLock)
		Inner Join #TmpVentasInformacionAdicional A (NoLock) 
				On ( A.IdEmpresa = I.IdEmpresa And A.IdEstado = I.IdEstado And A.IdFarmacia = I.IdFarmacia And A.FolioVenta = I.FolioVenta ) 
		Inner Join VentasDet_Lotes D (NoLock) On (I.IdEmpresa = D.IdEmpresa And I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.FolioVenta = D.FolioVenta)
		Left Join VentasDet_Lotes_Ubicaciones U (NoLock)
			On (D.IdEmpresa = U.IdEmpresa And D.IdEstado = U.IdEstado And D.IdFarmacia = U.IdFarmacia And D.FolioVenta = U.FolioVenta And
				D.CodigoEAN = U.CodigoEAN And D.ClaveLote = U.ClaveLote) 
		Inner Join vw_productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN ) 
		--Where DescripcionClave like '%olan%' 
		-- Exec(@sSql)  


		---------------------- Completar informacion  
		Update B Set 
			IdClaveSSA = C.IdClaveSSA, ClaveSSA_Aux = C.ClaveSSA, DescripcionCortaClave = C.Descripcion, Relacionada = 1, 
			Cantidad = ( Cantidad / (Multiplo * 1.0)) 
		From #tmpFinal B (NoLock) 
		Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) 
			On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	


		Update I Set IdBeneficiario = D.IdBeneficiario, NumReceta = D.NumReceta
		From #tmpFinal I(NoLock)
		Inner Join VentasInformacionAdicional D (NoLock)
				On (I.IdEmpresa = D.IdEmpresa And I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.Folio = D.FolioVenta)
						
		Update I Set Beneficiario = (D.ApPaterno + ' ' + D.ApMaterno + ' ' + D.Nombre)
		From #tmpFinal I(NoLock)
		Inner Join CatBeneficiarios D (NoLock)
				On (I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.IdCliente = D.IdCliente And I.IdSubCliente = D.IdSubCliente And I.IdBeneficiario = D.IdBeneficiario)

		Update I Set Programa = D.Programa, SubPrograma = D.SubPrograma
		From #tmpFinal I(NoLock)
		Inner Join vw_Programas_SubProgramas D (NoLock)
				On (I.IdPrograma = D.IdPrograma And I.IdSubPrograma = D.IdSubPrograma)
						
		Update I Set NombrePersonal = NombreCompleto
		From #tmpFinal I(NoLock)
		Inner Join vw_Personal D (NoLock)
				On (I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.IdPersonal = D.IdPersonal)

		Update I Set FechaCad = Convert(varchar(10), D.FechaCaducidad, 120)
		From #tmpFinal I(NoLock)
		Inner Join FarmaciaProductos_CodigoEAN_Lotes D (NoLock)
				On (I.IdEmpresa = D.IdEmpresa And I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.CodigoEAN = D.CodigoEAN And I.ClaveLote = D.ClaveLote) 



		If @AplicarMascara = 1 
		Begin 
			Update B Set ClaveSSA_Aux = PC.Mascara, DescripcionCortaClave = PC.Descripcion
			From #tmpFinal B (NoLock) 
			Inner Join CFG_clavessa_Mascara PC (NoLock) 
				On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA and PC.Status = 'A' ) 
		End 



		If (@ConcentradoReporte = 1)
			Begin		
				Select 'Fecha Salida' = Convert(varchar(10), FechaRegistro, 120), 'Folio Salida' = Folio, 
				'Núm. Beneficiario' = IdBeneficiario, Beneficiario, 'Núm. Programa' = IdPrograma, Programa, 
				'Núm. Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma
				From #tmpFinal I
				Group By FechaRegistro, Folio, IdBeneficiario, Beneficiario, IdPrograma, Programa, IdSubPrograma, SubPrograma 
				Order By Folio, FechaRegistro  
			End
		Else
			Begin
				Select 
					--I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.IdCliente, I.IdSubCliente, Convert(Varchar(10), I.FechaRegistro, 120) As FechaRegistro, 
					--I.Folio, I.RefObservaciones, I.NumReceta,
					--I.IdBeneficiario, I.Beneficiario, I.IdPrograma, I.Programa, I.IdSubPrograma, I.SubPrograma,
					--I.IdPersonal, I.NombrePersonal, I.ClaveSSA_Aux, I.DescripcionCortaClave, 
					--I.IdProducto, I.DescripcionCorta, I.CodigoEAN, I.ClaveLote, I.FechaCad, I.Cantidad,
					--I.IdPasillo, I.IdEstante, I.IdEntrepaño

					Convert(Varchar(10), I.FechaRegistro, 120) As 'Fecha Registro', I.Folio, I.RefObservaciones As Observaciones, I.NumReceta As 'Número Receta',
					I.IdBeneficiario As 'Clave Beneficiario', I.Beneficiario, I.IdPrograma As 'Clave Programa', I.Programa,
					I.IdSubPrograma As 'Clave Sub-Programa', I.SubPrograma As 'Sub-Programa', I.IdPersonal As 'Clave Personal', I.NombrePersonal As Personal,
					I.ClaveSSA_Aux As ClaveSSA, I.DescripcionCortaClave As 'Descripción Clave',
					I.IdProducto As 'Clave Producto', I.CodigoEAN As 'Código EAN', I.DescripcionCorta As 'Descripción Producto',
					I.ClaveLote As Lote, I.FechaCad As 'Fecha Caducidad', I.Cantidad, IdPasillo As Pasillo, I.IdEstante As Estante, I.IdEntrepaño As Entrepaño
				From #tmpFinal I
				Order By I.Folio, I.FechaRegistro 
			End 


--	 print (@sSql) 
--	 Exec  (@sSql)


End
Go--#SQL



