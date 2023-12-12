--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'Spp_Rpt_ListadoDeOrdenDeCompra' And xType = 'P' )
	Drop Proc Spp_Rpt_ListadoDeOrdenDeCompra 
Go--#SQL 

--Exec Spp_Rpt_ListadoDeOrdenDeCompra @IdEmpresa = '001', @IdEstado = '*', @IdProveedor = '', @IdPersonal = '', @TodasLasFechas = 0, @FechaInicial = '2015-04-28', @FechaFinal = '2015-04-29', @iStatus = '0', @sTipoReporte  = 0

Create Procedure Spp_Rpt_ListadoDeOrdenDeCompra
( 
	@IdEmpresa Varchar(3)= '*', @IdEstado Varchar(2) = '*', @IdProveedor Varchar(4) = '', @IdPersonal Varchar(4) = '',
	@TodasLasFechas Bit = 0, @FechaInicial varchar(10) = '2016-04-28', @FechaFinal Varchar(10) = '2016-04-28',
	@iStatus int = 1, @sTipoReporte bit = 0
) 
With Encryption 	
As
Begin 
Declare 
    @sSql varchar(7500),
    @sWhereOrigen Varchar(200),
    @sWhereProv  Varchar(200),
    @sWhereFechas  Varchar(200),
    @sWherePer  Varchar(200),
    @sWhereStatus Varchar(200)
    
    Set @sWhereOrigen = ''
    Set @sWhereProv = ''
    Set @sWhereFechas = ''
    Set @sWherePer = ''
    Set @sWhereStatus = ''
    
    
    
    if (@IdEmpresa <> '*' )
    Begin
        Set @sWhereOrigen = ' And M.IdEmpresa = ' + CHAR(39) + @IdEmpresa + CHAR(39)
    End

    if (@IdEstado <> '*')
    Begin
        Set @sWhereOrigen = @sWhereOrigen + ' And M.IdEstado = ' + CHAR(39) + @IdEstado + CHAR(39)
    End

    if (@IdProveedor <> '')
    Begin
        Set @sWhereProv = ' And IdProveedor = ' + CHAR(39) + @IdProveedor + CHAR(39)
    End

    if (@IdPersonal <> '')
    Begin
        Set @sWherePer = ' And IdPersonal = ' + CHAR(39) + @IdPersonal + CHAR(39)
    End

    if (@TodasLasFechas <> 1)
    Begin
        Set @sWhereFechas = ' And Convert(varchar(10), FechaRegistro, 120) Between ' + CHAR(39) + @FechaInicial + CHAR(39) + ' and ' + CHAR(39) + @FechaFinal + CHAR(39)
    End
    
    If(@iStatus = 1)
    Begin
		Set @sWhereStatus = ' And Status = ' + Char(39) + 'OC' + CHAR(39)
    End
    
    If(@iStatus = 2)
    Begin
		Set @sWhereStatus = ' And Status = ' + Char(39) + 'A' + CHAR(39)
    End
    
    If(@iStatus = 3)
    Begin
		Set @sWhereStatus = ' And Status Not In ( ' + CHAR(39) + 'OC' + Char(39) +', ' + CHAR(39) + 'A' + CHAR(39) + ' )'
    End
    
    


    Set @sSql = 'Select 	' + CHAR(13) +
		'M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, IdFarmacia, Farmacia, IdPersonal as IdComprador, NombrePersonal as Comprador, Folio, IdProveedor, Proveedor, Observaciones, ' + CHAR(13) +
		'	convert(varchar(10), FechaRegistro, 120) as FechaRegistro,  ' + CHAR(13) +
		'	Case When Status = ' +Char(39) + 'C' + Char(39) + 'Then '  + Char(39) + 'CANCELADA'  + Char(39) + 'When Status = ' + Char(39) + 'OC'  + Char(39) + 'Then ' + Char(39) + 'ORDEN COLOCADA' + Char(39) + ' Else ' + Char(39) + 'ORDEN NO COLOCADA'  + Char(39) + ' End As StatusOrdenCompra,' + CHAR(13) +
		'	convert(varchar(10), FechaColocacion, 120) as FechaColocacion ' + CHAR(13) +
		'Into #TMP_vw_OrdenesCompras_Claves_Enc ' + CHAR(13) +
		'From vw_OrdenesCompras_Claves_Enc  M (Nolock) ' + CHAR(13) +
		'Where 1 = 1 ' + @sWhereOrigen + @sWhereProv + @sWhereFechas + @sWherePer + @sWhereStatus


	If ( @sTipoReporte = 0 )
		Begin
			Set @sSql = @sSql + CHAR(13) + CHAR(13) +
			'Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, IdComprador, Comprador, Folio, IdProveedor, Proveedor, Observaciones, ' + CHAR(13) +
			'	FechaRegistro, StatusOrdenCompra, FechaColocacion,  ClaveSSA, DescripcionSal,  MIN(Precio * (1 + (D.tasaiva/100))) as Precio_Minimo, ' + CHAR(13) +
			'	MAX(Precio * (1 + (D.tasaiva/100))) as Precio_Maximo, AVG(Precio * (1 + (D.tasaiva/100))) as Precio_Promedio,  SUM(Cantidad) AS Cantidad, ' + CHAR(13) +
			'	round(sum(  (D.Cantidad * (D.Precio - ( D.Precio * (D.Descuento/100.00)))) * (1 + (D.TasaIva / 100.0)) ), 2) AS Importe ' + CHAR(13) +
			'From #TMP_vw_OrdenesCompras_Claves_Enc M (NoLock)' + CHAR(13) +
			'Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (NoLock) ' + CHAR(13) +
			'	On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioOrden )' + CHAR(13) +
			'Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN ) ' + CHAR(13) +
			'Group By M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, IdComprador, Comprador, Folio, IdProveedor, Observaciones, Proveedor, FechaRegistro, StatusOrdenCompra, ' + CHAR(13) +
			'	FechaColocacion, ClaveSSA, DescripcionSal' + CHAR(13) +
			'Order By M.IdEstado, M.Folio '
		End
	Else
		Begin
	
			Set @sSql = @sSql + CHAR(13) + CHAR(13) +
				'	Select  M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, IdComprador, Comprador, Folio, IdProveedor, Proveedor, Observaciones, FechaRegistro, StatusOrdenCompra, '+ CHAR(13) +
				'	FechaColocacion,  ClaveSSA, DescripcionSal, D.CodigoEAN, Descripcion, '+ CHAR(13) +
				'	MIN(Precio * (1 + (D.tasaiva/100))) as Precio_Minimo, MAX(Precio * (1 + (D.tasaiva/100))) as Precio_Maximo, AVG(Precio * (1 + (D.tasaiva/100))) as Precio_Promedio, '+ CHAR(13) +
				'	cast(Precio * (1 + (D.tasaiva/100)) as numeric(14, 4)) as Precio, SUM(Cantidad) AS Cantidad,  ' +
				'	round(sum(  (D.Cantidad * (D.Precio - ( D.Precio * (D.Descuento/100.00)))) * (1 + (D.TasaIva / 100.0)) ), 2) AS Importe '+ CHAR(13) +
				'From #TMP_vw_OrdenesCompras_Claves_Enc M (NoLock) '+ CHAR(13) +
				'Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (NoLock) '+ CHAR(13) +
				'	On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioOrden ) '+ CHAR(13) +
				'Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN ) '+ CHAR(13) +
				'Group By  M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, IdComprador, Comprador, Folio, IdProveedor, Proveedor, Observaciones, FechaRegistro, StatusOrdenCompra, FechaColocacion, '+ CHAR(13) +
				'	ClaveSSA, DescripcionSal, D.CodigoEAN, Descripcion, Precio, D.TasaIva '+ CHAR(13) +
				'Order By Folio'
		End
	print(@sSql)
	Exec (@sSql) 


	   	   
End 
Go--#SQL
 

	