If Exists ( Select Name From Sysobjects Where Name = 'spp_Consiliacion_AgregarActualizar' and xType = 'P' )
	Drop Proc spp_Consiliacion_AgregarActualizar 
Go--#SQL 

--Exec spp_Consiliacion_AgregarActualizar @IdEmpresa = '001', @IdEstado = '', @IdProveedor = '0001', @TipoDeOrden = 0, @CantidadPagos = 981023.0231

Create Proc spp_Consiliacion_AgregarActualizar
(
    @IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '', 
	@IdProveedor varchar(4) = '0001', 
	@TipoDeOrden Int = 2,
	@CantidadPagos Numeric(38, 4) = 1023.0231
	--@OrdenCompra varchar(8) = '*',  
	--@IdTipoDePedido int = 3, 
	--@IdTipoDePago int = 3
)
With Encryption 
As
Begin 
Set NoCount On 
Declare
	@sSql varchar(max),
	@sWhereEstado varchar(200),
	@sWhereTipoDeOrden Varchar(200)
	
	Set @sWhereEstado = ''
	Set @sWhereTipoDeOrden = ''
	
	If (@IdEstado <> '')
	Begin
		Set @sWhereEstado = ' And E.IdEstado = ' + CHAR(39) + @IdEstado + CHAR(39)
	End
	
	if (@TipoDeOrden <> 2)
	Begin
		Set @sWhereTipoDeOrden = ' And dbo.fg_EsCentralFolioOrdenDeCompra(E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioOrdenComprareferencia) = ' + 
			Cast(@TipoDeOrden As Varchar(1))
	End	
	
	CREATE TABLE #TempCPXP_Conciliacion
	(
	IdEmpresa varchar(3) NOT NULL,
	IdEstado varchar(2) NOT NULL,
	IdFarmacia varchar(4) NOT NULL,
	OrdenCompra varchar(30) NOT NULL,
	IdProveedor varchar(4) NOT NULL,
	Factura varchar(20) NOT NULL,
	FechaRegistro varchar(10) NULL,
	FechaColocacion varchar(10) NULL,
	FechaDocto varchar(10) NULL,
	FechaVenceDocto varchar(10) NULL,
	SubTotal numeric(38, 4) NULL,
	Iva numeric(38, 4) NULL,
	Total numeric(38, 4) NULL,
	FechaUpdate datetime NOT NULL
	)	
	
	Set @sSql = 'Select
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioOrdenComprareferencia As OrdenCompra, E.IdProveedor, E.ReferenciaDocto As Factura,
		Convert(Varchar(10), Min(E.FechaRegistro), 120)  As FechaRegistro,
		Convert(Varchar(10), Min(C.FechaColocacion), 120)  As FechaColocacion,
		Convert(Varchar(10), Min(E.FechaDocto), 120) As FechaDocto,
		Convert(Varchar(10), Min(E.FechaVenceDocto), 120) As FechaVenceDocto,
		Sum(SubTotal) As SubTotal, Sum(Iva) As Iva, Sum(Total) As Total,
		GETDATE() As FechaUpdate
	From OrdenesDeComprasEnc E (NoLock)
	Inner Join COM_OCEN_OrdenesCompra_Claves_Enc C (NoLock)
		On (E.IdEmpresa = C.IdEmpresa And E.IdEstado = C.EstadoEntrega And E.IdFarmacia = C.EntregarEn And E.FolioOrdenCompraReferencia = C.FolioOrden)
	Where C.HabilitadoParaPago = 1
	Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioOrdenComprareferencia, E.IdProveedor, E.ReferenciaDocto'

	Insert Into #TempCPXP_Conciliacion
	Exec (@sSql)
				
	----------------Actualizar
	Select T.*
	Into #Actualizar
	From #TempCPXP_Conciliacion T (NoLock) 
	Inner Join CPXP_Conciliacion E (NoLock) 
		On (E.IdEmpresa = T.IdEmpresa And E.IdEstado = T.IdEstado And E.IdFarmacia = T.IdFarmacia And E.OrdenCompra = T.OrdenCompra And
			E.IdProveedor = T.IdProveedor And E.Factura = T.Factura)
	Where T.SubTotal <> E.SubTotal Or T.Iva <> E.Iva Or T.Total <> E.Total
			
	
	Insert Into CPXP_Conciliacion_Historico
	Select * From #Actualizar		
	
	Update E
	Set E.SubTotal = T.SubTotal, E.Iva = T.Iva, E.Total = T.Total, E.FechaUpdate = T.FechaUpdate
	From CPXP_Conciliacion E (NoLock) 
	Inner Join #Actualizar T (NoLock) 
		On (E.IdEmpresa = T.IdEmpresa And E.IdEstado = T.IdEstado And E.IdFarmacia = T.IdFarmacia And E.OrdenCompra = T.OrdenCompra And
			E.IdProveedor = T.IdProveedor And E.Factura = T.Factura)


	------------------Insertar
	Select *
	Into #Inserts
	From #TempCPXP_Conciliacion T (NoLock) 
	Where Not Exists(
					 Select *
					 From CPXP_Conciliacion E (NoLock) 
					 Where E.IdEmpresa = T.IdEmpresa And E.IdEstado = T.IdEstado And E.IdFarmacia = T.IdFarmacia And E.OrdenCompra = T.OrdenCompra 
					)
			
	
	Insert Into CPXP_Conciliacion_Historico
	Select * From #Inserts
		
	Insert Into CPXP_Conciliacion
	Select * From #Inserts
	
	if (@TipoDeOrden <> 2)
	Begin
		Set @sWhereTipoDeOrden = ' And dbo.fg_EsCentralFolioOrdenDeCompra(E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.OrdenCompra) = ' + 
			Cast(@TipoDeOrden As Varchar(1))
	End	
	
	
	Set @sSql = '
	Select
		E.IdEstado, S.Nombre As Estado, E.OrdenCompra, 
		(Case When dbo.fg_EsCentralFolioOrdenDeCompra(E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.OrdenCompra)  = 1
		Then ' + Char(39) + 'CENTRAL' + CHAR(39) + ' Else ' + CHAR(39) + 'REGIONAL' + Char(39) + ' End) As TipoDeCompra, 
		E.Factura, E.FechaRegistro, E.FechaColocacion ,E.FechaVenceDocto, E.Total, Cast(0.0000 As Numeric(38, 4)) As Pagos, 0.0000 As Dif,
		Cast(0.0000 As Numeric(38, 4)) As Pago
	Into #Pagos
	From CPXP_Conciliacion E (NoLock) 
	Inner Join CatEstados S (NoLock) On (S.IdEstado = E.IdEstado) 
	Where E.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And E.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) +  @sWhereEstado + @sWhereTipoDeOrden + '
	Order By 1, 3 
	
	Update P
	Set P.Pagos = Isnull((Select Sum(D.Pago)
				 From CPXP_PagosDet D (NoLock)
				 Where P.Idestado = D.IdEstado And P.OrdenCompra = D.FolioOrdeneCompra And P.Factura = D.Factura And
					   D.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And D.IdProveedor = ' + Char(39) + @IdProveedor + Char(39) + '), 0.0000)
	From #Pagos P
	
	Declare @Cantidad Numeric(38, 4) = ' + CAST(@CantidadPagos As varchar(200)) + '
	Declare @IdEstado_Cursor Varchar(2), @Factura varchar(20), @OrdenCompra varchar(30), 
				@Total Numeric(38, 4), @Pagos Numeric(38, 4), @Cantidad_Local Numeric(38, 4)
	
	Declare Llave_Tablas Cursor For
		Select IdEstado, OrdenCompra, Factura, Total, Pagos
		From #Pagos
		Order By FechaRegistro, Total Desc
	Open Llave_Tablas 
	Fetch From Llave_Tablas into @IdEstado_Cursor, @OrdenCompra, @Factura, @Total, @Pagos
	While @@Fetch_status = 0 And @Cantidad >= 0.0000
		Begin
			If ((@Total - @Pagos) > 0)
				Begin
					if ((@Total - @Pagos) <= @Cantidad)
						Set @Cantidad_Local = (@Total - @Pagos)
					Else
						Set @Cantidad_Local = 0
				End
			Else
				Begin
					Set @Cantidad_Local = 0
				End

			Set @Cantidad = @Cantidad - @Cantidad_Local
			
			Update P
			Set P.Pago = @Cantidad_Local
			From #Pagos P
			Where IdEstado = @IdEstado_Cursor And Factura = @Factura And OrdenCompra = @OrdenCompra
			
			Fetch From Llave_Tablas into @IdEstado_Cursor, @OrdenCompra, @Factura, @Total, @Pagos
		End 
    Close Llave_Tablas  
	Deallocate Llave_Tablas
	
	Select *, 0.0000 As PagRegPago,(case when Pago > 0 then 1 else 0 End) AplicarPago From #Pagos Where (Total - Pagos) <> 0  Order By FechaRegistro, Total Desc '

	Exec (@sSql)
	Print (@sSql)
	
	----Select * From #TempCPXP_Conciliacion
	----Select * From CPXP_Conciliacion
	----Select * From CPXP_Conciliacion_Historico Order By 1,2,3,4,5,6, 13 --Desc
	
End 
Go--#SQL 