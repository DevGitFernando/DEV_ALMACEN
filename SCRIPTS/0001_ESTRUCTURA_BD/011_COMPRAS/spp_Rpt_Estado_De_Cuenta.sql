If Exists ( Select Name From Sysobjects Where Name = 'spp_Rpt_Estado_De_Cuenta' and xType = 'P' )
	Drop Proc spp_Rpt_Estado_De_Cuenta 
Go--#SQL 

--Exec spp_Rpt_Estado_De_Cuenta @IdEmpresa = '001', @IdEstado = '', @IdProveedor = '', @TipoDeOrden = 2, @TipoDeSaldo = '2'

Create Proc spp_Rpt_Estado_De_Cuenta
(
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '', @IdProveedor varchar(4) = '', @TipoDeOrden int = 2, @TipoDeSaldo Varchar(1) = '0'
)
With Encryption 
As
Begin 
Set NoCount On
	Declare @sSql Varchar(8000),
			@sWhereProveedor varchar(200),	
			@sWhereEstado varchar(200),
			@sWhereTipoDeOrden Varchar(200)
	Set @sWhereEstado = ''
	Set @sWhereTipoDeOrden = ''
	Set @sWhereProveedor = ''
	
	/* TipoDeOrden
		
		0 = Regional
		1 = Central
		2 = ambos
	*/	
	
	/* TipoDeSaldo
		
		0 = Liquidados
		1 = Con saldo
		2 = ambos
	*/
	
	
	If (@IdProveedor <> '')
	Begin
		Set @sWhereProveedor = ' And IdProveedor = ' + CHAR(39) + @IdProveedor + CHAR(39)
	End
	
	If (@IdEstado <> '')
	Begin
		Set @sWhereEstado = ' And C.IdEstado = ' + CHAR(39) + @IdEstado + CHAR(39)
	End
	
	If (@TipoDeOrden <> 2)
	Begin
		Set @sWhereTipoDeOrden = ' And dbo.fg_EsCentralFolioOrdenDeCompra(C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.OrdenCompra) = ' + 
			Cast(@TipoDeOrden As Varchar(1))
	End	


	Set @sSql = 'Select
		C.IdEmpresa, C.IdEstado, space(50) As Estado, C.OrdenCompra,
		(Case When dbo.fg_EsCentralFolioOrdenDeCompra(C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.OrdenCompra)  = 1
		Then ' + Char(39) + 'CENTRAL' + CHAR(39) + ' Else ' + CHAR(39) + 'REGIONAL' + Char(39) + ' End) As TipoDeCompra, 
		C.IdProveedor, space(100) As Proveedor, C.Factura, C.FechaRegistro, C.FechaColocacion, C.FechaDocto, C.FechaVenceDocto,
		C.SubTotal, C.Iva, C.Total,Cast(0.0000 As numeric(38, 4)) As Abonos, C.FechaUpdate
	Into #Conciliacion
	From CPXP_Conciliacion C
	Where C.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + @sWhereProveedor + @sWhereEstado + @sWhereTipoDeOrden + '
	
	Delete #Conciliacion Where Total = 0 And Abonos = 0
	
	Update P
	Set P.Abonos = Isnull((Select Sum(D.Pago)
						  From CPXP_PagosDet D (NoLock)
						  Where P.Idestado = D.IdEstado And P.OrdenCompra = D.FolioOrdeneCompra And P.Factura = D.Factura And
							    D.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + @sWhereProveedor + '), 0.0000)
	From #Conciliacion P
	
	if (' + @TipoDeSaldo + ' = 0)
	Begin
		Delete #Conciliacion Where Total - Abonos <> 0
	End
	
	if (' + @TipoDeSaldo + ' = 1)
	Begin
		Delete #Conciliacion Where Total - Abonos = 0
	End
	
	Update C
	Set C.Estado = F.Nombre
	From #Conciliacion  C
	Inner Join Catestados F (NoLock) On (C.IdEstado = F.IdEstado)

	Update C
	Set C.Proveedor = F.Nombre
	From #Conciliacion  C
	Inner Join CatProveedores F (NoLock) On (C.IdProveedor = F.IdProveedor)
		
	Select IdEmpresa, IdEstado, Estado, TipoDeCompra, IdProveedor, Proveedor, Sum(Total) As Total, Sum(Abonos) As Abonos
	From #Conciliacion
	Group By IdEmpresa, IdEstado, Estado, TipoDeCompra, IdProveedor, Proveedor
	
	Select *
	From #Conciliacion'
	 
	Print (@sSql)
	Exec (@sSql)
	
End 
Go--#SQL