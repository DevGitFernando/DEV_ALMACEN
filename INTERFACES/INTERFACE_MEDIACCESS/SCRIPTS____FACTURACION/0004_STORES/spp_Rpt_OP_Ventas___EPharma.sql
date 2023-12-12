If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_OP_Ventas___EPharma' and xType = 'P')
    Drop Proc spp_Rpt_OP_Ventas___EPharma
Go--#SQL

Create Proc spp_Rpt_OP_Ventas___EPharma
( 
	@IdEmpresa Varchar(3) = '002', @IdEstado varchar(2) = '', @IdFarmacia Varchar(4) = '',
	@IdCliente Varchar(4) = '', @IdSubCliente Varchar(4) = '', @IdPrograma Varchar(4) = '', @IdSubPrograma Varchar(4) = '',
	@FechaIncial varchar(10) = '2016-12-01', @FechaFinal varchar(10) = '2017-12-01', @IdBeneficiario Varchar(8) = '',  @TipoReporte smallint = 4,
	@EsReporte bit = 1, @TipoDeVenta int = 0
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On
	
	Select * Into #vw_Clientes_SubClientes From vw_Clientes_SubClientes F
	
	If (@IdCliente <> '')
	Begin
		Delete #vw_Clientes_SubClientes
		
		Insert Into #vw_Clientes_SubClientes
		Select * From vw_Clientes_SubClientes F
		Where IdCliente = @IdCliente
	End
	
	If (@IdCliente <> '' And @IdSubCliente <> '')
	Begin
		Delete #vw_Clientes_SubClientes
		
		Insert Into #vw_Clientes_SubClientes
		Select * From vw_Clientes_SubClientes F
		Where IdCliente = @IdCliente And IdSubCliente = @IdSubCliente
	End
	
	
	Select * Into #vw_Programas_SubProgramas From vw_Programas_SubProgramas F
	
	If (@IdPrograma <> '')
	Begin
		Delete #vw_Programas_SubProgramas
		
		Insert Into #vw_Programas_SubProgramas
		Select * From vw_Programas_SubProgramas F
		Where IdPrograma = @IdPrograma
	End
	
	If (@IdPrograma <> '' And @IdSubPrograma <> '')
	Begin
		Delete #vw_Programas_SubProgramas
		
		Insert Into #vw_Programas_SubProgramas
		Select * From vw_Programas_SubProgramas F
		Where IdPrograma = @IdPrograma And IdSubPrograma = @IdSubPrograma
	End

	Select * Into #Farmacia From CatFarmacias F

	If (@IdEstado <> '')
	Begin
		Delete #Farmacia
		
		Insert Into #Farmacia
		Select * From CatFarmacias F
		Where IdEstado = @IdEstado
	End

	If (@IdEstado <> '' And @IdFarmacia <> '')
	Begin
		Delete #Farmacia
		
		Insert Into #Farmacia
		Select *
		From CatFarmacias F
		Where IdEstado = @IdEstado And IdFarmacia =  @IdFarmacia
	End

	Select L.*
	Into #VentasEnc
	From VentasEnc  L (NoLock)
	Inner Join #Farmacia I (NoLock) On ( I.IdEstado = L.IdEstado and I.IdFarmacia = L.IdFarmacia)
	Where IdEmpresa = @IdEmpresa And Convert(Varchar(10), L.FechaRegistro, 120) between  @FechaIncial And @FechaFinal
	
	Select
		I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.FechaRegistro, I.FolioVenta As Folio,		IsNUll(A.RefObservaciones, '') As RefObservaciones, IsNull(A.NumReceta, '') As NumReceta, IsNull(A.FechaReceta, '') As FechaReceta,		IsNUll(A.IdBeneficiario, '') As IdBeneficiario, Cast('' As Varchar(200)) As Documento,  cast('' As Varchar(300)) As Beneficiario, 		I.IdCliente, R.NombreCliente, I.IdSubCliente, R.NombreSubCliente, C.IdPrograma, C.Programa, C.IdSubPrograma, C.SubPrograma,		I.IdPersonal, cast('' As Varchar(200)) As NombrePersonal, IsNUll(A.IdMedico, '') As IdMedico, cast('' As Varchar(200)) As NombreMedico, P.ClaveSSA_Aux,
		P.DescripcionCortaClave, P.IdProducto, P.DescripcionCorta,
		P.CodigoEAN, ClaveSSA, IdGrupoTerapeutico, Upper(GrupoTerapeutico) As GrupoTerapeutico, IdFamilia, Familia, L.IdSubFarmacia, L.ClaveLote,
		Cast('' As Varchar(10)) As FechaCad, L.CantidadVendida As Cantidad, 
		Cast(0 As Numeric(14,4)) As Porcentaje_Paciente, Cast(0 As Numeric(14,4)) As Porcentaje_Cliente, Cast(0 As Numeric(14,4)) As Importe_Paciente,
		Cast(0 As Numeric(14,4)) As Importe_Cliente, Cast(0 As Numeric(14,4)) As Importe_Total_SinIVA, TasaIva,
		Cast(0 As Numeric(14,4)) As IVA_Paciente, Cast(0 As Numeric(14,4)) As IVA_Cliente, Cast(0 As Numeric(14,4)) As Importe_Paciente_con_IVA,
		Cast(0 As Numeric(14,4)) As Importe_Cliente_con_IVA, Cast(0 As Numeric(14,4)) As Importe_Total_con_IVA,
		TipoDeventa
	Into #Ventas
	From #VentasEnc I (NoLock)
	Inner Join VentasDet_Lotes L (NoLock) 
		On ( I.IdEmpresa = L.IdEmpresa and I.IdEstado = L.IdEstado and I.IdFarmacia = L.IdFarmacia and I.FolioVenta = L.FolioVenta)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN )
	Inner Join  #vw_Clientes_SubClientes R (NoLock) On ( I.IdCliente = R.IdCliente And  I.IdSubCliente = R.IdSubCliente)
	Inner Join #vw_Programas_SubProgramas C (NoLock) On (I.IdPrograma = C.IdPrograma And I.IdSubPrograma = C.IdSubPrograma)
	Left Join VentasInformacionAdicional A (NoLock)
		On ( I.IdEmpresa = A.IdEmpresa and I.IdEstado = A.IdEstado and I.IdFarmacia = A.IdFarmacia and I.FolioVenta = A.FolioVenta )
			
	If (@TipoDeVenta <> 0)
	Begin
		Delete #Ventas Where TipoDeventa = @TipoDeVenta
	End

	UpDate V Set NombrePersonal =  NombreCompleto
	From #Ventas V
	Inner Join vw_personal P (NoLock) On (V.IdEstado = P.IdEstado And V.IdFarmacia = P.IdFarmacia And V.IdPersonal = P.IdPersonal)

	UpDate I Set I.Beneficiario =  P.NombreCompleto
	From #Ventas I
	Inner Join vw_Beneficiarios P (NoLock)
		On (I.IdEstado = P.IdEstado And I.IdFarmacia = P.IdFarmacia And I.IdCliente = P.IdCliente And  I.IdSubCliente = P.IdSubCliente And I.IdBeneficiario = P.IdBeneficiario)
		
	UpDate I Set I.FechaCad = Convert(Varchar(10), P.FechaCaducidad, 120)
	From #Ventas I
	Inner Join FarmaciaProductos_CodigoEAN_Lotes P (NoLock)
		On (I.IdEmpresa = P.IdEmpresa and I.IdEstado = P.IdEstado And I.IdFarmacia = P.IdFarmacia And
			I.Idproducto = P.Idproducto And I.CodigoEAN = P.CodigoEAN And I.IdSubFarmacia = P.IdSubFarmacia And I.ClaveLote = P.ClaveLote)
			
	UpDate V Set NombreMedico =  NombreCompleto
	From #Ventas V
	Inner Join vw_Medicos P (NoLock) On (V.IdEstado = P.IdEstado And V.IdFarmacia = P.IdFarmacia And V.IdMedico = P.IdMedico)
	
	Update A Set Documento =  Elegibilidad	From #Ventas A (NoLock)	Inner Join INT_MA__RecetasElectronicas_001_Encabezado I (NoLock)		On (A.IdEmpresa = I.IdEmpresaSurtido And A.IdEstado = I.IdEstadoSurtido And A.IdFarmacia = I.IdFarmaciaSurtido
			And (Left(NumReceta, CHARINDEX(' - ', A.NumReceta)) = I.Folio_Ma Or A.NumReceta = I.Folio_Ma))
	
	Update I
		Set
		Porcentaje_Paciente = Porcentaje_01, Porcentaje_Cliente = Porcentaje_02
	From #Ventas I
	Inner Join INT_MA_Ventas_Importes E (NoLock)
		On (I.IdEmpresa = E.IdEmpresa And I.IdEstado = E.IdEstado And I.IdFarmacia = E.IdFarmacia And I.Folio = E.FolioVenta)
		
			
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVenta, IdProducto, CodigoEAN, TasaIva, PrecioUnitario	Into #ConcentradoVenta	From VentasDet E (Nolock)	Order By E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVenta
	
	
	Update I
	Set
		Importe_Paciente = ((Porcentaje_Paciente/100.00000) * (Cantidad * PrecioUnitario)),
		Importe_Cliente = ((Porcentaje_Cliente/100.00000) * (Cantidad * PrecioUnitario))
	From #Ventas I
	Inner Join #ConcentradoVenta A
		On (A.IdEmpresa = I.IdEmpresa And A.IdEstado = I.IdEstado And A.IdFarmacia = I.IdFarmacia
			And A.FolioVenta = I.Folio And A.CodigoEAN = I.CodigoEAN )
	
	Update I
	Set Importe_Total_SinIva = Importe_Paciente + Importe_Cliente,
		Iva_Paciente = Importe_Paciente * (TasaIva / 100.0000),
		Iva_Cliente  = Importe_Cliente * (TasaIva / 100.0000),
		Importe_Paciente_Con_IVA = Importe_Paciente * (1 + (TasaIva / 100.0000)),
		Importe_Cliente_Con_IVA = Importe_Cliente * (1 + (TasaIva / 100.0000)),
		Importe_Total_Con_IVA = Importe_Paciente * (1 + (TasaIva / 100.0000)) + Importe_Cliente * (1 + (TasaIva / 100.0000))
	From #Ventas I
			

	If (@TipoReporte = 1)
	Begin
		If (@EsReporte = 1)
			Begin
				Select
					F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, 'Fecha Salida' = Convert(varchar(10), FechaRegistro, 120), 'Folio Salida' = Folio,
					'Núm. Beneficiario' = IdBeneficiario, Beneficiario, 'Núm. Programa' = IdPrograma, Programa,
					'Núm. Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma
				From #Ventas N (Nolock)
				Inner Join vw_Farmacias F (NoLock) On (N.IdEstado = F.IdEstado And N.IdFarmacia = F.IdFarmacia)
				Group By F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, FechaRegistro, Folio, IdBeneficiario, Beneficiario,
					IdPrograma, Programa, IdSubPrograma, SubPrograma
				Order By F.IdEstado, F.Estado, F.IdFarmacia, Folio, FechaRegistro 
			End
		Else
			Begin
				Select
					I.IdEmpresa, I.IdEstado, F.Estado, I.IdFarmacia, F.Farmacia, I.FechaRegistro, I.Folio, I.RefObservaciones, I.FechaReceta,
					I.NumReceta, I.Documento, I.IdBeneficiario, I.Beneficiario,
					I.IdCliente, I.NombreCliente, I.IdSubCliente, I.NombreSubCliente, I.IdPrograma, I.Programa, I.IdSubPrograma, I.SubPrograma,
					I.IdPersonal, I.NombrePersonal, I.IdMedico, I.NombreMedico, 
					I.ClaveSSA, I.DescripcionCortaClave, I.IdProducto, I.DescripcionCorta, I.CodigoEAN, I.IdSubFarmacia, IdGrupoTerapeutico, GrupoTerapeutico,
					IdFamilia, Familia, I.ClaveLote, I.FechaCad, Cantidad,
					Porcentaje_Paciente, Porcentaje_Cliente , Importe_Paciente, Importe_Cliente, Importe_Total_SinIVA, TasaIVA,
					IVA_Paciente, IVA_Cliente, Importe_Paciente_con_IVA, Importe_Cliente_con_IVA, Importe_Total_con_IVA
				From #Ventas I (Nolock)				Inner Join vw_Farmacias F (NoLock) On (I.IdEstado = F.IdEstado And F.IdFarmacia = I.IdFarmacia)				Order By I.IdEmpresa, I.IdEstado, I.IdFarmacia, Folio, FechaRegistro								Select  Distinct I.IdEstado, R.Estado, I.IdFarmacia, R.Farmacia, I.FolioVenta, I.IdFormasdePago, F.Descripcion As FormasdePago, I.Importe, I.PagoCon, I.Cambio, I.Referencia
				From #Ventas A (NoLock)
				Inner Join INT_MA_VentasPago I (NoLock) On ( I.IdEmpresa = A.IdEmpresa and I.IdEstado = A.IdEstado and I.IdFarmacia = A.IdFarmacia and I.FolioVenta = A.Folio )
				Inner Join CatFormasDePago F (NoLock) On (I.IdFormasDePago = F.IdFormasDePago)
				Inner Join vw_Farmacias R (NoLock) On (I.IdEstado = R.IdEstado And I.IdFarmacia = R.IdFarmacia)
				Order BY I.IdEstado, I.IdFarmacia, I.FolioVenta			End	End			If (@TipoReporte = 2)
	Begin
		If (@EsReporte = 1)
			Begin
				Select I.IdEstado, F.Estado, I.IdFarmacia, F.Farmacia, 'Fecha Devolución' = Convert(varchar(10), I.FechaRegistro, 120), 'Folio Devolución' = Folio, 'Folio Salida' = I.Folio,
	            'Núm. Beneficiario' = IdBeneficiario, Beneficiario, 'Núm. Programa' = IdPrograma, Programa,
                'Núm. Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma
				From #Ventas I (Nolock)				Inner Join vw_Farmacias F (NoLock) On (I.IdEstado = F.IdEstado And F.IdFarmacia = I.IdFarmacia)				Inner Join DevolucionesEnc E (NoLock) On (I.IdEmpresa = E.IdEmpresa And I.IdEstado = E.IdEstado And I.IdFarmacia = E.IdFarmacia And I.Folio = E.Referencia)				Inner Join DevolucionesDet D (NoLock) On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioDevolucion = D.FolioDevolucion)                Order By I.IdEstado, F.Estado, I.IdFarmacia, F.Farmacia, Folio, I.FechaRegistro
			End
		Else
			Begin
				Select
					I.IdEmpresa, I.IdEstado, F.Estado, I.IdFarmacia, F.Farmacia, I.FechaRegistro, E.Referencia As Folio, I.Folio As FolioVenta, I.FechaReceta, E.Observaciones, I.NumReceta,					I.IdBeneficiario, I.Beneficiario, 					I.IdCliente, I.NombreCliente, I.IdSubCliente, I.NombreSubCliente, I.IdPrograma, I.Programa, I.IdSubPrograma, I.SubPrograma,					I.IdPersonal, I.NombrePersonal, I.IdMedico, I.NombreMedico,					I.DescripcionCortaClave, I.IdProducto, I.DescripcionCorta, I.CodigoEAN, I.ClaveSSA, I.ClaveLote,					Convert(Varchar(10), I.FechaCad, 120) As FechaCaducidad, Cant_Devuelta
				From #Ventas I (Nolock)				Inner Join vw_Farmacias F (NoLock) On (I.IdEstado = F.IdEstado And F.IdFarmacia = I.IdFarmacia)				Inner Join DevolucionesEnc E (NoLock) On (I.IdEmpresa = E.IdEmpresa And I.IdEstado = E.IdEstado And I.IdFarmacia = E.IdFarmacia And I.Folio = E.Referencia)				Inner Join DevolucionesDet D (NoLock) On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioDevolucion = D.FolioDevolucion)				Order By I.IdEmpresa, I.IdEstado, I.IdFarmacia, Folio, FechaRegistro			End	End
	
	If (@TipoReporte = 3)
	Begin
		If (@EsReporte = 1)
			Begin	
				Select
					I.IdEstado, I.Estado, I.IdFarmacia, I.Farmacia, 'Fecha No Surtido' = Convert(varchar(10), I.FechaRegistro, 120), 'Folio No Surtido' = I.Folio,
					'Núm. Beneficiario' = V.IdBeneficiario, V.Beneficiario, 'Núm. Programa' = V.IdPrograma, V.Programa,
					'Núm. Sub-Programa' = V.IdSubPrograma, 'Sub-Programa' = V.SubPrograma
				From  vw_Impresion_Ventas_ClavesSolicitadas I (Nolock)
				Inner Join #Ventas V (Nolock)
					On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.Folio )
				Where I.EsCapturada = 1
				Group By I.IdEstado, I.Estado, I.IdFarmacia, I.Farmacia, I.FechaRegistro, I.Folio, V.IdBeneficiario, V.Beneficiario,
					V.IdPrograma, V.Programa, V.IdSubPrograma, V.SubPrograma
				Order By I.IdEstado, I.Estado, I.IdFarmacia, I.Farmacia, I.Folio, I.FechaRegistro
			End
		Else
			Begin
				Select
					 I.IdEmpresa, I.IdEstado, I.Estado, I.IdFarmacia, I.Farmacia,
					I.FechaRegistro, I.Folio, V.NumReceta, V.IdBeneficiario, V.Beneficiario, V.IdPrograma, V.Programa,  V.IdSubPrograma, V.SubPrograma,
					V.IdPersonal, V.NombrePersonal, I.ClaveSSA, I.DescripcionCortaClave, I.CantidadRequerida
				From vw_Impresion_Ventas_ClavesSolicitadas I (Nolock)
				Inner Join #Ventas V (Nolock)
					On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.Folio )
				Where I.EsCapturada = 1				Group By
					 I.IdEmpresa, I.IdEstado, I.Estado, I.IdFarmacia, I.Farmacia,
					I.FechaRegistro, I.Folio, V.NumReceta, V.IdBeneficiario, V.Beneficiario, V.IdPrograma, V.Programa,
					V.IdSubPrograma, V.SubPrograma, V.IdPersonal, V.NombrePersonal, I.ClaveSSA, I.DescripcionCortaClave, I.CantidadRequerida				Order By I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.Folio, I.FechaRegistro			End	End
	
	If (@TipoReporte = 4)
	Begin
	
				Select
					I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.ClaveSSA, Sum(I.Cantidad) As Cantidad
				Into #TmpVentas
				From #Ventas I (NoLock)
				Where I.Cantidad > 0
				Group By I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.ClaveSSA
			
				Select Distinct I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.ClaveSSA, Sum(I.CantidadRequerida) As CantidadRequerida
				Into #TmpClavesSolicitadas
				From vw_Impresion_Ventas_ClavesSolicitadas I (NoLock)
				Inner Join vw_Impresion_Ventas_Credito V (Nolock)
					On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.Folio )
				Where I.EsCapturada = 1
				Group By I.IdEmpresa, I.IdEstado, I.IdFarmacia, I.ClaveSSA
				
				Select Distinct D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.ClaveSSA
				Into #Tmp
				From #TmpVentas D
				
				Insert Into #Tmp
				Select Distinct D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.ClaveSSA
				From #TmpClavesSolicitadas D
				
				Select Distinct D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.ClaveSSA, CAST(0 as Int) As Cantidad, CAST(0 as Int) As CantidadNegada
				Into #TmpFinal
				From #Tmp D
				
				Update D Set D.Cantidad = C.Cantidad
				From #TmpFinal D
				Inner Join #TmpVentas C (NoLock)
					On (D.IdEmpresa = C.IdEmpresa And D.IdEstado = C.IdEstado And D.IdFarmacia = C.IdFarmacia And D.ClaveSSA = C.ClaveSSA)
				
				Update D Set D.CantidadNegada = C.CantidadRequerida
				From #TmpFinal D
				Inner Join #TmpClavesSolicitadas C (NoLock)
					On (D.IdEmpresa = C.IdEmpresa And D.IdEstado = C.IdEstado And D.IdFarmacia = C.IdFarmacia And D.ClaveSSA = C.ClaveSSA)
				If (@EsReporte = 1)
					Begin					
						Select							T.IdEstado, F.Estado, T.IdFarmacia, F.Farmacia,							T.ClaveSSA, S.DescripcionCortaClave As Descripcion, T.Cantidad, T.CantidadNegada , (T.Cantidad + T.CantidadNegada) As Demanda						From #TmpFinal T						Inner Join CatEmpresas E (NoLock) On (T.IdEmpresa = E.Idempresa)						Inner Join vw_Farmacias F (NoLock) On (T.IdEstado = F.Idestado And T.IdFarmacia = F.IdFarmacia)						Inner Join vw_ClavesSSA_Sales S (NoLock) On (T.ClaveSSA = S.ClaveSSA)
						Order By T.IdEstado, T.IdFarmacia, T.ClaveSSA, S.DescripcionCortaClave
					End
				Else
					Begin
						Select							T.IdEmpresa, E.nombre As Empresa, T.IdEstado, F.Estado, T.IdFarmacia, F.Farmacia,							T.ClaveSSA, S.DescripcionCortaClave As Descripcion, T.Cantidad, T.CantidadNegada , (T.Cantidad + T.CantidadNegada) As Demanda						From #TmpFinal T						Inner Join CatEmpresas E (NoLock) On (T.IdEmpresa = E.Idempresa)						Inner Join vw_Farmacias F (NoLock) On (T.IdEstado = F.Idestado And T.IdFarmacia = F.IdFarmacia)						Inner Join vw_ClavesSSA_Sales S (NoLock) On (T.ClaveSSA = S.ClaveSSA)
						Order By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, S.DescripcionCortaClave
					End
	End

End
Go--#SQL