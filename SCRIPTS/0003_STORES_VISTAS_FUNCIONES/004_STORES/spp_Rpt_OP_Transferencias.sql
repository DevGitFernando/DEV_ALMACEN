-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_OP_Transferencias' and xType = 'P')
    Drop Proc spp_Rpt_OP_Transferencias
Go--#SQL

--Exec spp_Rpt_OP_Transferencias 'TS', '2015-05-05', '2015-06-21', '*', '*', '0'

Create Proc spp_Rpt_OP_Transferencias
( 
	@sEntradaSalida Varchar(2) = 'TS', @FechaIncial varchar(10) = '2017-02-01', @FechaFinal varchar(10) = '2017-02-01',
	@IdJurisdiccion Varchar(3) = '*', @IdFarmacia Varchar(4) = '2182',  @EsNoSurtido smallint = 0
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare  
	@FechaMinima varchar(10), 
	@Pedidos int, 
	@Movtos int, 
	@sWhere Varchar(200), 
	@sSql Varchar(5000)
	
	
	Set @sWhere = ''
	
    If ( @IdJurisdiccion <> '*' and @IdJurisdiccion <> '' )
    Begin 
        Set @sWhere = @sWhere +  ' And F.IdJurisdiccion =  ' + CHAR(39) + @IdJurisdiccion + CHAR(39)
        
        If ( @IdFarmacia <> '*' and @IdFarmacia <> '' )
        Begin 
            Set @sWhere = @sWhere + ' And F.IdFarmacia = ' + CHAR(39) + @IdFarmacia + CHAR(39)
        End
    End 
	
	
	Select * 
	Into #vw_Farmacias 
	From vw_Farmacias 
	
	Set @sSql = 'Select T.IdEmpresa, T.IdEstado,T.IdFarmacia, T.FolioTransferencia,' + Char(13) +
			'(F.IdJurisdiccion + ' + Char(39) + ' -- ' + Char(39) + ' + F.Jurisdiccion) As Jurisdiccion, ' + Char(13) +
			'Convert(Varchar(10), T.FechaRegistro, 120) As Fecha, T.FolioTransferencia As Folio, F.IdFarmacia As IdFarmaciaDestino, F.Farmacia As Farmacia,' + Char(13) +
			'T.Observaciones, T.IdPersonal, S.nombrecompleto As Personal ' + Char(13) +
			'Into #TransferenciasEnc ' + Char(13) +
			'From TransferenciasEnc T (NoLock) ' + Char(13) +
			'Inner Join #vw_Farmacias F (NoLock) On (F.IdEstado = T.IdEstadoRecibe And F.IdFarmacia = T.IdFarmaciaRecibe) ' + Char(13) +
			'Inner Join vw_Personal S (NoLock) On (T.IdEstado = S.IdEstado And T.IdFarmacia = S.IdFarmacia And T.IdPersonal = S.IdPersonal) ' + Char(13) +
			'Where  T.Status <> ' + Char(39) + 'C' + Char(39) +
			'	And Convert(Varchar(10), T.FechaRegistro, 120) between ' + Char(39) + @FechaIncial + Char(39) + ' And ' + Char(39) + @FechaFinal + Char(39) + ' And ' + Char(13) +
			'	TipoTransferencia =  ' + Char(39) + @sEntradaSalida + Char(39) +  @sWhere + Char(13) +
			'Order By Convert(Varchar(10), T.FechaRegistro, 120) ' + Char(13) + Char(13)
		

	Set @sSql = @sSql + 'Select L.* ' + Char(13) + 
		'Into #tmp__EAN  ' + Char(13) + 
		'From #TransferenciasEnc D ' + Char(13) +
		'Inner Join TransferenciasDet L (NoLock) ' + Char(13) +
		'	On (D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioTransferencia = L.FolioTransferencia ) ' + Char(13)  			

	Set @sSql = @sSql + 'Select L.* ' + Char(13) + 
		'Into #tmp__Lotes  ' + Char(13) + 
		'From #TransferenciasEnc D ' + Char(13) +
		'Inner Join TransferenciasDet_Lotes L (NoLock) ' + Char(13) +
		'	On (D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioTransferencia = L.FolioTransferencia ) ' + Char(13)  			


	Set @sSql = @sSql + 'Select  ' + Char(13) +
		'	T.Jurisdiccion, T.Fecha, T.Folio, T.IdFarmaciaDestino As ' + Char(39) + 'Clave Farmacia' + Char(39) + ', T.Farmacia, T.Observaciones, ' +
		'	T.IdPersonal As ' + Char(39) + 'Clave Personal' + Char(39) + ', T.Personal, ' + Char(13) +
		'	Cast(' + Char(39) + Char(39) + ' As varchar(30)) As ' + Char(39) + 'Clave SSA' + Char(39) + ', L.CodigoEAN, ' +
		'	Cast(' + Char(39) + Char(39) + ' As varchar(300)) As ' + Char(39) + 'Descripción' + Char(39) + ', L.ClaveLote As Lote, ' + Char(13) +
		'	IsNull(Cast(U.IdPasillo As Varchar(10)), ' + Char(39) + Char(39) + ') As Pasillo, IsNull(Cast(U.IdEstante As Varchar(10)), '+ Char(39) + Char(39) + ') As Estante, ' +
		'	IsNull(Cast(U.IdEntrepaño As Varchar(10)), '+ Char(39) + Char(39) + ') As Entrepaño, ' + Char(13) +
		'	IsNull(U.CantidadEnviada, L.CantidadEnviada) As Cantidad ' + Char(13) +
		'Into #Final ' + Char(13) + 
		'From #TransferenciasEnc T' + Char(13) + 
		'Inner Join #tmp__EAN D (NoLock) ' + Char(13) +
		'	On (T.IdEmpresa = D.IdEmpresa And T.IdEstado = D.IdEstado And T.IdFarmacia = D.IdFarmacia And T.FolioTransferencia = D.FolioTransferencia) ' + Char(13) +
		'Inner Join #tmp__Lotes L (NoLock) ' + Char(13) +
		'	On (D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioTransferencia = L.FolioTransferencia ' + Char(13) +
		'	And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN) ' + Char(13) +
		'Left Join TransferenciasDet_Lotes_Ubicaciones U (NoLock) ' + Char(13) +
		'	On (L.IdEmpresa = U.IdEmpresa And L.IdEstado = U.IdEstado And L.IdFarmacia = U.IdFarmacia And L.FolioTransferencia = U.FolioTransferencia ' + Char(13) +
		'	And L.IdProducto = U.IdProducto And L.CodigoEAN = U.CodigoEAN And L.ClaveLote = U.ClaveLote) ' + Char(13) +
		'Where L.CantidadEnviada > 0 ' + Char(13) + Char(13) +
		
		'Update F Set F.[Clave SSA] = P.ClaveSSA, F.Descripción = P.DescripcionCorta ' + Char(13) +
		'From #Final F ' + Char(13) +
		'Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.CodigoEAN = P.CodigoEAN) ' + Char(13) + Char(13) +
		
		'Select * From #Final '
	--L.CantidadEnviada > 0 And
	
	--Set @sSql = 'Select ' + Char(13) +
	--	'(F.IdJurisdiccion + ' + Char(39) + ' -- ' + Char(39) + ' + F.Jurisdiccion) As Jurisdiccion, ' + Char(13) +
	--	'Convert(Varchar(10), T.FechaRegistro, 120) As Fecha, T.FolioTransferencia As Folio, F.IdFarmacia, F.Farmacia As Farmacia,' + Char(13) +
	--	'T.Observaciones, T.IdPersonal, S.nombrecompleto As Personal, ' + Char(13) +
	--	'P.ClaveSSA, L.CodigoEAN, DescripcionCorta, L.ClaveLote, '  + Char(13) +
	--	'IsNull(Cast(U.IdPasillo As Varchar(10)), ' + Char(39) + Char(39) + ') As IdPasillo, IsNull(Cast(U.IdEstante As Varchar(10)), '+ Char(39) + Char(39) + ') As IdEstante, ' +
	--	'IsNull(Cast(U.IdEntrepaño As Varchar(10)), '+ Char(39) + Char(39) + ') As IdEntrepaño, ' + Char(13) +
	--	'IsNull(U.CantidadEnviada, L.CantidadEnviada) As Cantidad ' + Char(13) +
	--'From TransferenciasEnc T (NoLock) ' + Char(13) +
	--'Inner Join TransferenciasDet D (NoLock) ' + Char(13) +
	--'  On (T.IdEmpresa = D.IdEmpresa And T.IdEstado = D.IdEstado And T.IdFarmacia = D.IdFarmacia And T.FolioTransferencia = D.FolioTransferencia) ' + Char(13) +
	--'Inner Join TransferenciasDet_Lotes L (NoLock) ' + Char(13) +
	--'	On (D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioTransferencia = L.FolioTransferencia ' + Char(13) +
	--'		And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN) ' + Char(13) +
	--'Left Join TransferenciasDet_Lotes_Ubicaciones U (NoLock)  ' + Char(13) +
	--'	On (L.IdEmpresa = U.IdEmpresa And L.IdEstado = U.IdEstado And L.IdFarmacia = U.IdFarmacia And L.FolioTransferencia = U.FolioTransferencia  ' + Char(13) +
	--	'And L.IdProducto = U.IdProducto And L.CodigoEAN = U.CodigoEAN And L.ClaveLote = U.ClaveLote) ' + Char(13) +
	--'Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN) ' + Char(13) +
	--'Inner Join #vw_Farmacias F (NoLock) On (F.IdEstado = T.IdEstadoRecibe And F.IdFarmacia = T.IdFarmaciaRecibe) ' + Char(13) +
	--'Inner Join vw_Personal S (NoLock) On (T.IdEstado = S.IdEstado And T.IdFarmacia = S.IdFarmacia And T.IdPersonal = S.IdPersonal)'  + Char(13) +
	--'Where L.CantidadEnviada > 0 And T.Status <> ' + Char(39) + 'C' + Char(39) +
	--'	And Convert(Varchar(10), T.FechaRegistro, 120) between ' + Char(39) + @FechaIncial + Char(39) + ' And ' + Char(39) + @FechaFinal + Char(39) + ' And ' + Char(13) +
	--'	TipoTransferencia =  ' + Char(39) + @sEntradaSalida + Char(39) +  @sWhere + Char(13) +
	----'Group By Convert(Varchar(10), T.FechaRegistro, 120), T.FolioTransferencia, ' + Char(13) +
	----'   F.IdJurisdiccion, F.Jurisdiccion, ' + Char(13) +
	----'   F.IdFarmacia, F.Farmacia, P.ClaveSSA, L.CodigoEAN, DescripcionCorta, L.ClaveLote, L.ClaveLote, U.IdPasillo, U.idEstante, U.IdEntrepaño ' + Char(13) +
	--'Order By Convert(Varchar(10), T.FechaRegistro, 120) '
	
	
	if (@EsNoSurtido = 1)
	Begin	
		Set @sSql = 'Select  (F.IdJurisdiccion + ' + Char(39) + ' -- ' + Char(39) + ' + F.Jurisdiccion) As Jurisdicción, ' + Char(13) +
				'Convert(Varchar(10), T.FechaRegistro, 120) As Fecha, T.FolioTransferencia As Folio, ' +
				'F.IdFarmacia As ' + Char(39) + 'Clave Farmacia' + Char(39) + ' , F.Farmacia As Farmacia, ' + Char(13) +
				'N.Observaciones, T.IdPersonal As ' + Char(39) + 'Clave Personal' + Char(39) + ', S.nombrecompleto As Personal, ' + Char(13) +
				'P.ClaveSSA, P.DescripcionCortaClave As Descripción, Sum(N.CantidadRequerida) As Cantidad ' + Char(13) +
				'From TransferenciasEnc T (NoLock)  ' + Char(13) +
				'Inner Join TransferenciasEstadisticaClavesDispensadas N (NoLock)  ' + Char(13) + 
				'	On (T.IdEmpresa = N.IdEmpresa And T.IdEstado = N.IdEstado And T.IdFarmacia = N.IdFarmacia And T.FolioTransferencia = N.FolioTransferencia)  ' + Char(13) + 
				'Inner Join vw_ClavesSSA_Sales P (NoLock) On (N.IdClaveSSA = P.IdClaveSSA_Sal)  ' + Char(13) + 
				'Inner Join #vw_Farmacias F (NoLock) On (F.IdEstado = T.IdEstadoRecibe And F.IdFarmacia = T.IdFarmaciaRecibe) ' + Char(13) +
				'Inner Join vw_Personal S (NoLock) On (T.IdEstado = S.IdEstado And T.IdFarmacia = S.IdFarmacia And T.IdPersonal = S.IdPersonal) ' + Char(13) +
				'Where  N.EsCapturada = 1 And T.Status <> ' + Char(39) + 'C' + Char(39) + Char(13) +
				'	And Convert(Varchar(10), T.FechaRegistro, 120) between ' + Char(39) + @FechaIncial + Char(39) + ' And ' + Char(39) + @FechaFinal + Char(39) + ' And ' + Char(13) +
				'	TipoTransferencia =  ' + Char(39) + @sEntradaSalida + Char(39) +  @sWhere + Char(13) +
				'Group By Convert(Varchar(10), T.FechaRegistro, 120), T.FolioTransferencia,   ' + Char(13) +
				'F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia, '  + Char(13) +
				'N.Observaciones, T.IdPersonal, S.nombrecompleto, ' + Char(13) +
				'P.ClaveSSA, P.DescripcionCortaClave  ' + Char(13) +
				'Order By Convert(Varchar(10), T.FechaRegistro, 120)'
	End
	print (@sSql)
	Exec (@sSql)



End
Go--#SQL