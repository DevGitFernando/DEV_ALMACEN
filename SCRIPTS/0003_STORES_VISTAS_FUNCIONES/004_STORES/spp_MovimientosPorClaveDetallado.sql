If Exists (Select Name From SysObjects(NoLock) Where Name = 'SPP_MovimientosPorClaveDetallado' And xType = 'P')
    Drop Proc SPP_MovimientosPorClaveDetallado
Go--#SQL	

Create Proc SPP_MovimientosPorClaveDetallado (@IdEmpresa varchar(3),@IdEstado varchar(2),@IdFarmacia varchar(4),@FechaInic varchar(10),
			  @FechaFin varchar(10),@ClaveSSA varchar(20),@Efecto bit,@sTipo varchar (4) , @IdPersonal Varchar(4) = '0001') 
With Encryption
As 
Begin 
Set NoCount On 
Set DateFormat YMD

	Declare  @bBandera bit,
			 @sSql Varchar(1200),
			 @sWhere Varchar(300),
			 @sWhereAux Varchar(600)
			 
	Set @bBandera = 1
	Set @sWhere = ''
	Set @sWhereAux = ''
	
	Set @sWhereAux = ' Where K.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and k.IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + 
				' And k.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' And Convert(varchar(10),k.FechaSistema,120) Between ' + char(13) + 
				+ Char(39) + @FechaInic + Char(39) + ' And ' + Char(39) + @FechaFin + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39)
	
	
	If(@ClaveSSA <> '')
		Set @sWhere = ' And K.ClaveSSA =  '  + char(39) + @ClaveSSA + char(39)
	
	If (@IdPersonal <> '')
	    Set @sWhere = @sWhere + ' And K.IdPersonalRegistra =  ' + char(39) + @IdPersonal + char(39)

	If (@sTipo = 'TS' Or @sTipo = 'TE' Or @sTipo = 'TIS' Or @sTipo = 'TIE' Or @sTipo = 'EC' Or @sTipo = 'CC' Or @sTipo = 'ED' Or @sTipo = 'SV')
			Begin 	
				Set @bBandera = 0
			End

	-------------------------------------------------------------------------------------------------------------
	Select Top 0 *   
	Into #vw_Kardex_ProductoCodigoEAN 
	From vw_Kardex_ProductoCodigoEAN 

	Set @sWhereAux = @sWhereAux + @sWhere

	--print (@sWhereAux)

	Set @sSql = 'Insert Into #vw_Kardex_ProductoCodigoEAN ' + 
	' Select * ' + 
	' From vw_Kardex_ProductoCodigoEAN K (NoLock) ' + char(13) + 
	'  ' + @sWhereAux  
	Exec(@sSql) 
-----------------------------------------------------------------------------------------------------------------


	If (@sTipo = 'TS')
		Begin		
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10), K.FechaRegistro,120) As Fecha, Cast(Sum(Salida) As Int) As Cantidad, ' + Char(13) +
			'     (F.IdFarmacia + ' + Char(39) + ' -- ' + Char(39)+ ' + Farmacia) As '+ Char(39) + 'Farmacia destino' + Char(39) + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)' + Char(13) +
			'Inner Join TransferenciasEnc T(Nolock)' + Char(13) +
			'	On (K.IdEmpresa = T.IdEmpresa And K.IdEstado = T.IdEstado And K.IdFarmacia = T.IdFarmacia And K.Folio = T.FolioTransferencia)' + Char(13) +
			'Inner Join vw_Farmacias F (NoLock) On (T.IdFarmaciaRecibe = F.IdFarmacia And T.IdEstadoRecibe = F.IdEstado )' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
			'     K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'	  And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By K.Folio, K.FechaRegistro, F.IdFarmacia, Farmacia' + Char(13) +
			'Order By K.Folio'
		End

	If (@sTipo = 'TE')
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10),K.FechaRegistro,120) As Fecha, Cast(Sum(Entrada) As Int) As Cantidad, ' + Char(13) +
			'	(F.IdFarmacia + ' + Char(39) + ' -- ' + Char(39)+ ' + Farmacia) As '+ Char(39) + 'Farmacia recibe' + Char(39) + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)' + Char(13) +
			'Inner Join TransferenciasEnc T(Nolock)' + Char(13) +
			'	On (K.IdEmpresa = T.IdEmpresa And K.IdEstado = T.IdEstado And K.IdFarmacia = T.IdFarmacia And K.Folio = T.FolioTransferencia)' + Char(13) +
			'Inner Join vw_Farmacias F (NoLock) On (T.IdFarmaciaRecibe = F.IdFarmacia And T.IdEstadoRecibe = F.IdEstado )'  + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
				  'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By K.Folio, K.FechaRegistro, F.IdFarmacia, Farmacia' + Char(13) +
			'Order By K.Folio'
		End

	If (@sTipo = 'TIS')
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10),K.FechaRegistro,120) As Fecha, Cast(Sum(Salida) As Int) Cantidad, ' + Char(13) +
				'(' + Char(39) + 'De la Sub-Farmacia de ' + Char(39) + ' + Lower(SubfarmaciaOrigen) + ' + Char(39) + ' a la de ' + Char(39) +
				' + Lower(SubfarmaciaDestino) + ' + Char(39) + ' ==> ' + Char(39) + ' + Observaciones) As Observaciones ' + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)' + Char(13) +
			'Inner Join Vw_TraspasosEnc TE (Nolock)' + Char(13) +
			'	On (K.IdEmpresa = TE.IdEmpresa And K.IdEstado = TE.IdEstado And K.IdFarmacia = TE.IdFarmacia And K.Folio = TE.Folio) ' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
					'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By k.Folio, K.FechaRegistro, SubfarmaciaOrigen, SubfarmaciaDestino, Observaciones' + Char(13) +
			'Order By k.Folio'
		End

	If (@sTipo = 'TIE')
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10),K.FechaRegistro,120) Fecha, Cast(Sum(Entrada) As Int) Cantidad, ' + Char(13) +
				'(' + Char(39) + 'De la Sub-Farmacia de ' + Char(39) + ' + Lower(SubfarmaciaOrigen) + ' + Char(39) + ' a la de ' + Char(39) +
				' + Lower(SubfarmaciaDestino) + ' + Char(39) + ' ==> ' + Char(39) + ' + Observaciones) As Observaciones ' + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)' + Char(13) +
			'Inner Join Vw_TraspasosEnc TE (Nolock)' + Char(13) +
			'	On (K.IdEmpresa = TE.IdEmpresa And K.IdEstado = TE.IdEstado And K.IdFarmacia = TE.IdFarmacia And K.Folio = TE.Folio)' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
			'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By k.Folio, K.FechaRegistro, SubfarmaciaOrigen, SubfarmaciaDestino, Observaciones' + Char(13) +
			'Order By k.Folio'
		End

	If (@sTipo = 'EC')
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10),K.FechaRegistro,120) As Fecha, Cast(Sum(Entrada) As Int) As Cantidad, ' + Char(13) +
				'(' + Char(39) + 'Referencia: ' + Char(39) + ' + ReferenciaDocto + ' + Char(39) + ' -- ' + Char(39) + ' + P.Nombre) As Factura'  + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)' + Char(13) +
			'Inner Join ComprasEnc CE(Nolock)' + Char(13) +
			'		On (K.IdEmpresa = CE.IdEmpresa And K.IdEstado = CE.IdEstado And K.IdFarmacia = CE.IdFarmacia And right(K.Folio,8) = CE.FolioCompra)' + Char(13) +
			'Inner Join CatProveedores P (Nolock)ON CE.IdProveedor = p.IdProveedor ' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
			'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By K.Folio, K.FechaRegistro, ReferenciaDocto, P.Nombre' + Char(13) +
			'Order By K.Folio'
		End

	If (@sTipo = 'CC')
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10),K.FechaRegistro,120) As Fecha, Cast(Sum(Salida) As Int) As Cantidad, ' + Char(13) +
				'  (' + Char(39) + 'Referencia: ' + Char(39) + ' + ReferenciaDocto + ' + Char(39) + ' -- ' + Char(39) + ' + P.Nombre) as ' + Char(39) + 'Factura cancelada' + Char(39) + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)'+ Char(13) +
			'Inner Join DevolucionesEnc DE(Nolock)' + Char(13) +
			'		On (K.IdEmpresa = DE.IdEmpresa And K.IdEstado = DE.IdEstado And K.IdFarmacia = DE.IdFarmacia And K.Folio = DE.FolioMovtoInv)' + Char(13) +
			'Inner Join ComprasEnc CE(Nolock)' + Char(13) +
			'		On (K.IdEmpresa = CE.IdEmpresa And K.IdEstado = CE.IdEstado And K.IdFarmacia = CE.IdFarmacia And right(K.Folio,8) = CE.FolioCompra)' + Char(13) +
			'Inner Join CatProveedores P (Nolock)ON CE.IdProveedor = p.IdProveedor' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
			'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By K.Folio, K.FechaRegistro, K.FechaRegistro, ReferenciaDocto, P.Nombre' + Char(13) +
			'Order By K.Folio'
		End

	If (@sTipo = 'ED')
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10),K.FechaRegistro,120) As Fecha, Cast(Sum(Entrada) As Int) As Cantidad, ' + Char(13) +
				'  (' + Char(39) + 'Folio devuelto: ' + Char(39) + ' + DE.Referencia + ' + Char(39) + ' -- ' + Char(39) + ' + Observaciones) As Observaciones' + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)' + Char(13) +
			'Inner Join DevolucionesEnc DE(Nolock) ' + Char(13) +
			'		On (K.IdEmpresa = DE.IdEmpresa And K.IdEstado = DE.IdEstado And K.IdFarmacia = DE.IdFarmacia And K.Folio = DE.FolioMovtoInv)' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
			'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By K.Folio, K.FechaRegistro, K.FechaRegistro, DE.Referencia, Observaciones' + Char(13) +
			'Order By K.Folio'
		End

	If (@sTipo = 'SV')
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10), K.FechaRegistro,120) As Fecha, Cast(Sum(Salida) As Int) As Cantidad, ' + Char(13) +
				'	(FolioReferencia + ' + Char(39) + ' -- ' + Char(39) + ' + Nombre + ' + Char(39) + ' ' +
				Char(39) + '+ ApPaterno + ' + Char(39) + ' ' + Char(39) + ' + ApMaterno) As Beneficiario' + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)' + Char(13) +
			'Inner join VentasEnc VE (NoLock)' + Char(13) +
			'	On (K.IdEmpresa = VE.IdEmpresa And K.IdEstado = VE.IdEstado And K.IdFarmacia = VE.IdFarmacia And right(K.Folio,8) = VE.FolioVenta)' + Char(13) +
			'Inner Join VentasInformacionAdicional VIA(Nolock)' + Char(13) +
			'	On (K.IdEmpresa = VIA.IdEmpresa And K.IdEstado = VIA.IdEstado And K.IdFarmacia = VIA.IdFarmacia And right(K.Folio,8) = VIA.FolioVenta)' + Char(13) +
			'Inner Join CatBeneficiarios B(NoLock)' + Char(13) +
			'	ON (K.IdEstado = B.IdEstado And K.IdFarmacia = B.IdFarmacia And VE.IdCliente = B.IdCliente And VE.IdSubCliente = B.IdSubCliente' + Char(13) +
			'	   And VIA.IdBeneficiario = B.IdBeneficiario)' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
			'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By K.Folio, K.FechaRegistro, K.FechaRegistro, FolioReferencia, Nombre, ApPaterno, ApMaterno' + Char(13) +
			'Order By K.Folio'
		End 
		
	If (@Efecto = 0 And @bBandera = 1)
--	If (@sTipo = 'EPD' Or @sTipo = 'EE' Or @sTipo = 'EPC' Or @sTipo = 'EPD' Or @sTipo = 'IAE' Or @sTipo = 'PDC' Or @sTipo = 'II' Or @sTipo = 'IIC')
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10), K.FechaRegistro, 120) As Fecha, Cast(Sum(Entrada) As Int) Cantidad,' +
				'Observaciones ' + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock) ' + Char(13) +
			'Inner Join vw_MovtosInv_Enc ME (NoLock) ' + Char(13) +
			'	On (K.IdEmpresa = ME.IdEmpresa And K.IdEstado = ME.IdEstado And K.IdFarmacia = ME.IdFarmacia And K.Folio = ME.Folio)  ' + Char(13) +
			'Inner Join vw_MovtosInv_Tipos Tm (Nolock) On (Tm.TipoMovto = K.TipoMovto) ' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
			'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By K.Folio, K.FechaRegistro, Observaciones ' + Char(13) +
			'Order By K.Folio '
		End 
		
	If (@Efecto = 1 And @bBandera = 1)
		Begin
			Set @sSql = 'Select Right(K.Folio,8) As Folio, Convert(varchar(10), K.FechaRegistro, 120) As Fecha, ' + Char(13) +
				'   Cast(Sum(Salida) As Int) As Cantidad, Observaciones  ' + Char(13) +
			'From #vw_Kardex_ProductoCodigoEAN K (Nolock)' + Char(13) +
			'Inner Join vw_MovtosInv_Enc ME (NoLock)' + Char(13) +
			'	On(K.IdEmpresa = ME.IdEmpresa And K.IdEstado = ME.IdEstado And K.IdFarmacia = ME.IdFarmacia And K.Folio = ME.Folio) ' + Char(13) +
			'Inner Join vw_MovtosInv_Tipos Tm (Nolock) On (Tm.TipoMovto = K.TipoMovto)' + Char(13) +
			'Where K.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And K.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And ' +
			'K.IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And K.TipoMovto = '+ Char(39) + @sTipo+ Char(39) + Char(13) +
			'      And Convert(varchar(10),K.FechaSistema,120) Between ' + Char(39) + @FechaInic + Char(39) +  ' And ' + Char(39) + @FechaFin + Char(39) +
				  @sWhere + Char(13) +
			'Group By K.Folio, K.FechaRegistro, Observaciones ' + Char(13) +
			'Order By K.Folio'
		End
		Exec(@sSql)
End 
Go--#SQL


