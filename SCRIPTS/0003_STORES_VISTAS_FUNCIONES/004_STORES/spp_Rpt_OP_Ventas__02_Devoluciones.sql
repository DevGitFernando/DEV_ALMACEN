------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_OP_Ventas__02_Devoluciones' and xType = 'P' )  
    Drop Proc spp_Rpt_OP_Ventas__02_Devoluciones  
Go--#SQL

Create Proc spp_Rpt_OP_Ventas__02_Devoluciones
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia Varchar(4) = '113', 
	@IdCliente Varchar(4) = '0002', @IdSubCliente Varchar(4) = '0006', @IdPrograma Varchar(4) = '', @IdSubPrograma Varchar(4) = '',
	@FechaIncial varchar(10) = '2017-01-01', @FechaFinal varchar(10) = '2017-05-25', @IdBeneficiario Varchar(8) = '',  @ConcentradoReporte smallint = 1, 
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
	
---------------------------------------------------------- FILTROS 	
	if (@IdCliente <> '') 
    Begin
		Set @sWhereCliente = ' and IdCliente = ' + Char(39) + @IdCliente + Char(39)
    End
    
    if (@IdSubCliente <> '')
    Begin
		Set @sWhereSubCliente = ' and IdSubCliente = ' + Char(39) + @IdSubCliente + Char(39)
    End
	
    if (@IdPrograma <> '')
    Begin
		Set @sWherePrograma = ' and IdPrograma = ' + Char(39) + @IdPrograma + Char(39)
    End
    
    if (@IdSubPrograma <> '')
    Begin
		Set @sWhereSubPrograma = ' And IdSubPrograma = '  + Char(39) + @IdSubPrograma  + Char(39)
    End 
---------------------------------------------------------- FILTROS 	


--		spp_Rpt_OP_Ventas__04_NoSurtido    

----------------------------- GENERAR TABLAS DE PROCESO 
		Select Top 0 *  
		Into #TmpDevoluciones
		From DevolucionesEnc I (NoLock) 

		Select Top 0 
			I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.FechaRegistro, I.FolioDevolucion As Folio, E.FolioVenta, cast('' As Varchar(10)) As FechaReceta,
			I.Observaciones, cast( '' As Varchar(40)) As NumReceta, E.IdCliente, E.IdSubCliente,
			cast('' As Varchar(8)) As IdBeneficiario, cast( '' As Varchar(200)) As Beneficiario, E.IdPrograma,
			cast('' As Varchar(200)) As Programa, E.IdSubPrograma, 
			cast('' As Varchar(200)) As SubPrograma, I.IdPersonal, cast('' As Varchar(200)) As NombrePersonal,
			cast(P.DescripcionCortaClave as varchar(max)) as DescripcionCortaClave, P.IdProducto, P.DescripcionCorta, P.CodigoEAN, P.IdClaveSSA_Sal  As IdClaveSSA, P.ClaveSSA, D.ClaveLote,
			cast('' As Varchar(10)) As FechaCaducidad, ISNULL(U.Cant_Devuelta, D.Cant_Devuelta) As Cantidad,
			IsNull(cast(U.IdPasillo As Varchar(10)), '') As IdPasillo,
			IsNull(cast(U.IdEstante As Varchar(10)), '') As IdEstante,
			IsNull(cast(U.IdEntrepaño As Varchar(10)), '') As IdEntrepaño, 0 as Relacionada
		Into #tmpFinal
		From #TmpDevoluciones I (NoLock)
		Inner Join VentasEnc E (NoLock) 
			On (I.IdEmpresa = E.IdEmpresa And I.IdEstado = E.IdEstado And I.IdFarmacia = E.IdFarmacia And I.Referencia = E.FolioVenta)
		Inner Join DevolucionesDet_Lotes D (NoLock) On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And I.FolioDevolucion = D.FolioDevolucion)
		Left  Join DevolucionesDet_Lotes_Ubicaciones U (NoLock)
			On (I.IdEmpresa = U.IdEmpresa And I.IdEstado = U.IdEstado And I.IdFarmacia = U.IdFarmacia And I.FolioDevolucion = U.FolioDevolucion And
					D.CodigoEAN = U.CodigoEAN And D.ClaveLote = U.ClaveLote)
		Inner Join vw_productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN) 
----------------------------- GENERAR TABLAS DE PROCESO  



----------------------------- GENERAR LISTA DE FOLIOS DE DEVOLUCION 
		Set @sSql =
		'Insert Into #TmpDevoluciones	' + char(13) + 
		'Select *  ' + char(13) + 
		'From DevolucionesEnc I (NoLock) ' + char(13) + 
		'Where I.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' and I.IdEstado = '  + Char(39) + @IdEstado + Char(39) +
		'  and I.IdFarmacia = '+ Char(39) + @IdFarmacia + Char(39) +
		'  And Convert(Varchar(10), I.FechaRegistro, 120) between ' + Char(39) + @FechaIncial + Char(39) + ' And ' + Char(39) + @FechaFinal + Char(39)
		 Exec(@sSql)




		Set @sSql = 'Insert Into #tmpFinal '  + 
		'Select
			I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.FechaRegistro, I.FolioDevolucion As Folio, E.FolioVenta, cast( ' + Char(39) + Char(39) + ' As Varchar(10)) As FechaReceta,
			I.Observaciones, cast( ' + Char(39) + Char(39) + ' As Varchar(40)) As NumReceta, E.IdCliente, E.IdSubCliente,
			cast( ' + Char(39) + Char(39) + ' As Varchar(8)) As IdBeneficiario, cast( ' + Char(39) + Char(39) + ' As Varchar(200)) As Beneficiario, E.IdPrograma,
			cast( ' + Char(39) + Char(39) + ' As Varchar(200)) As Programa, E.IdSubPrograma, 
			cast( ' + Char(39) + Char(39) + ' As Varchar(200)) As SubPrograma, I.IdPersonal, cast( ' + Char(39) + Char(39) + ' As Varchar(200)) As NombrePersonal,
			cast(P.DescripcionCortaClave as varchar(max)) as DescripcionCortaClave, P.IdProducto, P.DescripcionCorta, P.CodigoEAN, P.IdClaveSSA_Sal  As IdClaveSSA, P.ClaveSSA, D.ClaveLote,
			cast( ' + Char(39) + Char(39) + ' As Varchar(10)) As FechaCaducidad, ISNULL(U.Cant_Devuelta, D.Cant_Devuelta) As Cantidad,
			IsNull(cast(U.IdPasillo As Varchar(10)), ' + Char(39) + Char(39) + ') As IdPasillo,
			IsNull(cast(U.IdEstante As Varchar(10)), ' + Char(39) + Char(39) + ') As IdEstante,
			IsNull(cast(U.IdEntrepaño As Varchar(10)), ' + Char(39) + Char(39) + ') As IdEntrepaño, 0 as Relacionada 	
		From #TmpDevoluciones I (NoLock)
		Inner Join VentasEnc E (NoLock) 
			On (I.IdEmpresa = E.IdEmpresa And I.IdEstado = E.IdEstado And I.IdFarmacia = E.IdFarmacia And I.Referencia = E.FolioVenta)
		Inner Join DevolucionesDet_Lotes D (NoLock) On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And I.FolioDevolucion = D.FolioDevolucion)
		Left  Join DevolucionesDet_Lotes_Ubicaciones U (NoLock)
			On (I.IdEmpresa = U.IdEmpresa And I.IdEstado = U.IdEstado And I.IdFarmacia = U.IdFarmacia And I.FolioDevolucion = U.FolioDevolucion And
					D.CodigoEAN = U.CodigoEAN And D.ClaveLote = U.ClaveLote)
		Inner Join vw_productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN)
		Where 1 = 1 ' + @sWhereCliente + @sWhereSubCliente + @sWherePrograma + @sWhereSubPrograma  
		Exec(@sSql) 



		Update B Set 
			IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionCortaClave = C.Descripcion, Relacionada = 1, 
			Cantidad = ( Cantidad / (Multiplo * 1.0))   
		From #tmpFinal B (NoLock) 
		Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) 
			On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	

		If (@IdBeneficiario <> '') 
			Begin
				Delete I
				From #tmpFinal I(NoLock)
				Where IdBeneficiario <> @IdBeneficiario
			End 
    
		Update I Set IdBeneficiario = D.IdBeneficiario, FechaReceta = Convert(Varchar(10), D.FechaReceta, 120), NumReceta = D.NumReceta
		From #tmpFinal I(NoLock)
		Inner Join VentasInformacionAdicional D (NoLock) On (I.IdEmpresa = D.IdEmpresa And I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.FolioVenta = D.FolioVenta)
				
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

		Update I Set FechaCaducidad = Convert(varchar(10), D.FechaCaducidad, 120)
		From #tmpFinal I(NoLock)
		Inner Join FarmaciaProductos_CodigoEAN_Lotes D (NoLock)
				On (I.IdEmpresa = D.IdEmpresa And I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.CodigoEAN = D.CodigoEAN And I.ClaveLote = D.ClaveLote) 
				
				
				
	If @AplicarMascara = 1 
		Begin 
			Update B Set ClaveSSA = PC.Mascara, DescripcionCortaClave = PC.Descripcion
			From #tmpFinal B (NoLock) 
			Inner Join CFG_clavessa_Mascara PC (NoLock) 
				On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA and PC.Status = 'A' ) 
		End 


		
		If (@ConcentradoReporte = 1)
			Begin		
				Select 'Fecha Devolución' = Convert(varchar(10), FechaRegistro, 120), 
				'Folio Devolución' = Folio, 'Folio Salida' = FolioVenta, 
				'Núm. Beneficiario' = IdBeneficiario, Beneficiario, 'Núm. Programa' = IdPrograma, Programa, 
				'Núm. Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma 
				From #tmpFinal I   
				Order By FechaRegistro 
			End
		Else
			Begin
				Select
					--I.IdEmpresa, I.IdEstado, I.IdFarmacia, Convert(Varchar(10), I.FechaRegistro, 120) As FechaRegistro, I.Folio, I.FolioVenta,
					--I.FechaReceta, I.Observaciones, I.NumReceta,
					--I.IdBeneficiario, I.Beneficiario, I.IdPrograma, I.Programa, I.IdSubPrograma, I.SubPrograma, I.IdPersonal, I.NombrePersonal,
					--I.DescripcionCortaClave, I.IdProducto, I.DescripcionCorta, I.CodigoEAN, I.ClaveSSA, I.ClaveLote, I.FechaCaducidad, I.Cantidad,
					--IdPasillo, IdEstante, IdEntrepaño

					Convert(Varchar(10), I.FechaRegistro, 120) As 'Fecha Registro', I.Folio, I.FolioVenta As 'Folio Venta', I.FechaReceta As 'Fecha Receta',
					I.Observaciones, I.NumReceta As 'Número Receta',
					I.IdBeneficiario As 'Clave Beneficiario', I.Beneficiario, I.IdPrograma As 'Clave Programa', I.Programa,
					I.IdSubPrograma As 'Clave Sub-Programa', I.SubPrograma As 'Sub-Programa', I.IdPersonal As 'Clave Personal', I.NombrePersonal As Personal,
					I.ClaveSSA As ClaveSSA, I.DescripcionCortaClave As 'Descripción Clave',
					I.IdProducto As 'Clave Producto', I.CodigoEAN As 'Código EAN', I.DescripcionCorta As 'Descripción Producto',
					I.ClaveLote As Lote, I.FechaCaducidad As 'Fecha Caducidad', I.Cantidad, IdPasillo As Pasillo, I.IdEstante As Estante, I.IdEntrepaño As Entrepaño


				From #tmpFinal I
				Order By I.Folio, I.FechaRegistro 
			End


--	 print (@sSql) 
--	 Exec  (@sSql)


End
Go--#SQL