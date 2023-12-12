------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_OP_Ventas__04_Demanda' and xType = 'P' )  
    Drop Proc spp_Rpt_OP_Ventas__04_Demanda  
Go--#SQL 

Create Proc spp_Rpt_OP_Ventas__04_Demanda 
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia Varchar(4) = '0088', 
	@IdCliente Varchar(4) = '0002', @IdSubCliente Varchar(4) = '0006', @IdPrograma Varchar(4) = '', @IdSubPrograma Varchar(4) = '',
	@FechaIncial varchar(10) = '2017-01-01', @FechaFinal varchar(10) = '2017-05-25', @IdBeneficiario Varchar(8) = '',  @ConcentradoReporte smallint = 0, 
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

    if (@IdBeneficiario <> '')
    Begin
		Set @sWhereBeneficiario = ' And IdBeneficiario = '  + Char(39) + @IdBeneficiario  + Char(39)
    End 
---------------------------------------------------------- FILTROS 	


------------------------------------------------------------ GENERAR TABLA DE FOLIOS BASE 
		Select Top 0 
			I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.FechaRegistro, Cast('' As Varchar(8)) As IdBeneficiario, Cast('' As Varchar(200)) As Beneficiario, 
			I.IdCliente, I.IdSubCliente, I.IdPrograma, Cast('' As Varchar(150)) As Programa, IdSubPrograma, Cast('' As Varchar(150)) As SubPrograma, 
			I.FolioVenta, P.IdClaveSSA_Sal  As IdClaveSSA, P.ClaveSSA, cast(P.DescripcionCortaClave as varchar(max)) as DescripcionCortaClave, Sum(D.CantidadVendida) As Cantidad, 
			0 as Relacionada 
		Into #TmpVentas 
		From VentasEnc I (NoLock) 
		Inner Join VentasDet D (NoLock) 
			On ( I.IdEmpresa = D.IdEmpresa And I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.FolioVenta = D.FolioVenta ) 
		Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN) 
		Group By I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.FechaRegistro, I.IdCliente, I.IdSubCliente, I.IdPrograma, IdSubPrograma, I.FolioVenta, P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionCortaClave



--		spp_Rpt_OP_Ventas__04_NoSurtido    

		Set @sSql = 'Insert Into #TmpVentas ' +  Char(13) +  
		'Select ' + Char(13) +
		'		I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.FechaRegistro, Cast( ' + Char(39) +  Char(39) + ' As Varchar(8)) As IdBeneficiario, Cast( ' + Char(39) +  Char(39) + ' As Varchar(200)) As Beneficiario, ' +
		'I.IdCliente, I.IdSubCliente, I.IdPrograma, Cast( ' + Char(39) +  Char(39) + ' As Varchar(150)) As Programa, IdSubPrograma, Cast( ' + Char(39) +  Char(39) + ' As Varchar(150)) As SubPrograma, ' +
		'I.FolioVenta, P.IdClaveSSA_Sal  As IdClaveSSA, P.ClaveSSA, cast(P.DescripcionCortaClave as varchar(max)) as DescripcionCortaClave, Sum(D.CantidadVendida) As Cantidad, 0 as Relacionada  ' +  Char(13) + 
		'From VentasEnc I (NoLock) ' +  Char(13) + 
		'Inner Join VentasDet D (NoLock) ' +  Char(13) +
		'	On (I.IdEmpresa = D.IdEmpresa And I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.FolioVenta = D.FolioVenta) ' +  Char(13) +
		'Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN) ' +  Char(13) +
		'Where D.CantidadVendida > 0 And I.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' and I.IdEstado = '  + Char(39) + @IdEstado + Char(39) +
		'  and I.IdFarmacia = '+ Char(39) + @IdFarmacia + Char(39) +
		'  And Convert(Varchar(10), I.FechaRegistro, 120) between ' + Char(39) + @FechaIncial + Char(39) + ' And ' + Char(39) + @FechaFinal + Char(39) +
		@sWhereCliente + @sWhereSubCliente + @sWherePrograma + @sWhereSubPrograma + Char(13) + 
		'Group By I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.FechaRegistro, I.IdCliente, I.IdSubCliente, I.IdPrograma, IdSubPrograma, I.FolioVenta, P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionCortaClave  ' 
		Exec( @sSql ) 


		Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionCortaClave = C.Descripcion, Relacionada = 1   
		From #TmpVentas B (NoLock) 
		Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) 
			On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' )
			
		If (@IdBeneficiario <> '')
			Begin
				Delete I
				From #TmpVentas I(NoLock)
				Where IdBeneficiario <> @IdBeneficiario
			End 	

		Update I Set IdBeneficiario = D.IdBeneficiario 
		From #TmpVentas I(NoLock) 
		Inner Join VentasInformacionAdicional D (NoLock) 
				On (I.IdEmpresa = D.IdEmpresa And I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.FolioVenta = D.FolioVenta) 
				

		Update I Set Beneficiario = (D.ApPaterno + ' ' + D.ApMaterno + ' ' + D.Nombre) 
		From #TmpVentas I(NoLock) 
		Inner Join CatBeneficiarios D (NoLock) 
				On (I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.IdCliente = D.IdCliente And I.IdSubCliente = D.IdSubCliente And I.IdBeneficiario = D.IdBeneficiario) 


		Update I Set Programa = D.Programa, SubPrograma = D.SubPrograma 
		From #TmpVentas I(NoLock) 
		Inner Join vw_Programas_SubProgramas D (NoLock) On (I.IdPrograma = D.IdPrograma And I.IdSubPrograma = D.IdSubPrograma) 
	

		Select Distinct N.IdEmpresa, N.IdEstado, N.IdFarmacia, V.IdClaveSSA, V.ClaveSSA, DescripcionCortaClave, Sum(N.CantidadRequerida) As CantidadRequerida 
		Into #TmpClavesSolicitadas 
		From VentasEstadisticaClavesDispensadas N (NoLock) 
		Inner Join #TmpVentas V (Nolock) On ( V.IdEmpresa = N.IdEmpresa and V.IdEstado = N.IdEstado and V.IdFarmacia = N.IdFarmacia and V.FolioVenta = N.FolioVenta ) 
		Group By N.IdEmpresa, N.IdEstado, N.IdFarmacia, V.IdClaveSSA, V.ClaveSSA, DescripcionCortaClave 
		

		Select Distinct D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdClaveSSA, D.ClaveSSA, DescripcionCortaClave 
		Into #Tmp 
		From #TmpVentas D 
		

		Insert Into #Tmp 
		Select Distinct D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdClaveSSA, D.ClaveSSA, DescripcionCortaClave 
		From #TmpClavesSolicitadas D 
		

		Select Distinct D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdClaveSSA, D.ClaveSSA, DescripcionCortaClave, CAST(0 as Int) As Cantidad, CAST(0 as Int) As CantidadNegada 
		Into #TmpFinal 
		From #Tmp D 
		

		Update D Set D.Cantidad = ( Select sum(C.Cantidad) From #TmpVentas C (NoLock) Where D.IdEmpresa = C.IdEmpresa And D.IdEstado = C.IdEstado And D.IdFarmacia = C.IdFarmacia And D.ClaveSSA = C.ClaveSSA ) 
		From #TmpFinal D  
		

		Update D Set D.CantidadNegada = ( Select sum(C.CantidadRequerida ) From #TmpClavesSolicitadas C (NoLock) Where D.IdEmpresa = C.IdEmpresa And D.IdEstado = C.IdEstado And D.IdFarmacia = C.IdFarmacia And D.ClaveSSA = C.ClaveSSA ) 
		From #TmpFinal D 
		
		
		If @AplicarMascara = 1 
		Begin 
			Update B Set ClaveSSA = PC.Mascara, DescripcionCortaClave = PC.Descripcion
			From #TmpFinal B (NoLock) 
			Inner Join CFG_clavessa_Mascara PC (NoLock) 
				On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA and PC.Status = 'A' ) 
		End 
		
		If (@ConcentradoReporte = 1)
			Begin		
				Select 'Fecha No Surtido' = Convert(varchar(10), V.FechaRegistro, 120), 'Folio No Surtido' = V.FolioVenta, 
				 'Núm. Beneficiario' = V.IdBeneficiario, V.Beneficiario, 'Núm. Programa' = V.IdPrograma, V.Programa, 
				 'Núm. Sub-Programa' = V.IdSubPrograma, 'Sub-Programa' = V.SubPrograma  
				From #TmpVentas V (Nolock) 
				Group By V.FechaRegistro, V.FolioVenta, V.IdBeneficiario, V.Beneficiario, V.IdPrograma, V.Programa, V.IdSubPrograma, V.SubPrograma  
				Order By V.FolioVenta, V.FechaRegistro  
			End
		Else
			Begin
				Select 
					T.ClaveSSA As 'Clave SSA', T.DescripcionCortaClave As 'Descripción Clave SSA',
					T.Cantidad As 'Cantidad Requerida', T.CantidadNegada As 'Cantidad Negada', (T.Cantidad + T.CantidadNegada) As Demanda 
				From #TmpFinal T 
				Inner Join CatEmpresas E (NoLock) On (T.IdEmpresa = E.Idempresa) 
				Inner Join vw_Farmacias F (NoLock) On (T.IdEstado = F.Idestado And T.IdFarmacia = F.IdFarmacia) 
				Order By T.ClaveSSA, T.DescripcionCortaClave 
			End


	 --- print (@sSql) 
	 --- Exec  (@sSql)


End
Go--#SQL 
