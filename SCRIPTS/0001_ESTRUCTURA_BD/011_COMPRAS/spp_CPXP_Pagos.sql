
If Exists ( Select Name From Sysobjects Where Name = 'spp_CPXP_Pagos' and xType = 'P' )
	Drop Proc spp_CPXP_Pagos 
Go--#SQL 

Create Proc spp_CPXP_Pagos
(
    @IdEmpresa varchar(3) = '001', 
	@IdProveedor varchar(4) = '0001',
	@Folio Varchar(8) = '00000001'
)
With Encryption 
As
Begin 
Set NoCount On 
Declare @sSql varchar(max)

	Set @sSql = '
	Select *
	From CPXP_PagosEnc E
	Where E.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And E.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' And E.Folio = ' + Char(39) + @Folio + Char(39) + '


	Select D.IdEstado, E.Nombre As Estado, D.FolioOrdeneCompra,  TipoDeCompra, Factura,
		Cast(' + Char(39) + Char(39) + ' As Varchar(10)) As  FechaRegistro,
		Cast(' + Char(39) + Char(39) + ' As Varchar(10)) As  FechaColocacion,
		Cast(' + Char(39) + Char(39) + ' As Varchar(10)) As FechaVenceDocto,
		Cast(0.0000 As Numeric(38, 4)) As Total, Cast(0.0000 As Numeric(38, 4)) As Pagos, Pago, Pago As PagoRef
	Into #Folio
	From CPXP_PagosDet D (NoLock)
	Inner Join CatEstados E (NoLock) On (D.IdEstado = E.IdEstado)
	Where D.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And D.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' And Folio = ' + Char(39) + @Folio + Char(39) + '
	
	Update P
	Set P.Pagos = Isnull((Select Sum(D.Pago)
				 From CPXP_PagosDet D (NoLock)
				 Where P.Idestado = D.IdEstado And P.FolioOrdeneCompra = D.FolioOrdeneCompra And P.Factura = D.Factura And
					   D.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And D.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + '), 0.0000)
	From #Folio P
	
	Update P
	Set P.Pagos = Pagos - Pago
	From #Folio P
	

	Update F
	Set F.FechaRegistro = (Select Convert(Varchar(10), Min(O.FechaRegistro), 120)
						   From OrdenesDeComprasEnc O (NoLock)
						   Where O.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And O.IdEstado = F.IdEstado And FolioOrdenCompraReferencia = F.FolioOrdeneCompra
								And O.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' And O.ReferenciaDocto = F.Factura)
	From #Folio F
	
	Update F
	Set F.FechaColocacion = (Select Convert(Varchar(10), Min(O.FechaColocacion), 120)
						   From COM_OCEN_OrdenesCompra_Claves_Enc O (NoLock)
						   Where O.IdEstado = F.IdEstado And O.FolioOrden = F.FolioOrdeneCompra And
								O.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And O.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' )
	From #Folio F
	
	Update F
	Set F.FechaVenceDocto = (Select Convert(Varchar(10), Min(O.FechaVenceDocto), 120)
						   From OrdenesDeComprasEnc O (NoLock)
						   Where O.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And O.IdEstado = F.IdEstado And FolioOrdenCompraReferencia = F.FolioOrdeneCompra
						   And O.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' And O.ReferenciaDocto = F.Factura)
	From #Folio F
		
	Update F
	Set F.Total = (Select Sum(Total)
				   From OrdenesDeComprasEnc O (NoLock)
				   Where O.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And O.IdEstado = F.IdEstado And FolioOrdenCompraReferencia = F.FolioOrdeneCompra
				   And O.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + ' And O.ReferenciaDocto = F.Factura)
	From #Folio F
		
	
	Select *
	From #Folio F
	Order By FechaRegistro, Pago Desc'
	
	Exec(@sSql)
	
End 
Go--#SQL 