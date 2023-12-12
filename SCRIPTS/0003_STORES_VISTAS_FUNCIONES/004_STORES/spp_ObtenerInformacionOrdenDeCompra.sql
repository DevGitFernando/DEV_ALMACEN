------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_ObtenerInformacionOrdenDeCompra' and xType = 'P' ) 
   Drop Proc spp_ObtenerInformacionOrdenDeCompra 
Go--#SQL 

Create Proc spp_ObtenerInformacionOrdenDeCompra
(
	@IdEmpresa Varchar(3) = '2', @IdEstado Varchar(2)= '14', @IdFarmacia Varchar(4) = '4', @EntregarEn Varchar(4) = '3', @Folio Varchar(8) = '123'
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

	--Declare @IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2)= '13', @IdFarmacia Varchar(4) = '0003', @FolioVenta Varchar(8) = '00000610'


Declare 
	@Text Varchar(Max),
	@TextAUX Varchar(Max)--,
	--@TextAUX2 Varchar(Max)


	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4)
	Set @EntregarEn = RIGHT('000000000000' + @EntregarEn, 4) 
	Set @Folio = RIGHT('000000000000' + @Folio, 8) 






	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, Folio, IdProveedor, Proveedor, EstadoEntrega, NomEstadoEntrega, EntregarEn, FarmaciaEntregarEn, IdPersonal, NombrePersonal,
           FechaRegistro, FechaRequeridaEntrega, Status, 1 As PermiteDescarga
	From vw_OrdenesCompras_Claves_Enc E (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And EntregarEn = @EntregarEn And Folio = @Folio

	Select CodigoEAN, IdProducto, Descripcion, TasaIva, CantidadCajas, Cantidad as Piezas, 0 As Cantidad, PrecioUnitario As Costo, 0 As Importe, 0 As ImporteIva, 0 As ImporteTotal 
	From vw_OrdenesCompras_CodigosEAN_Det  (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And EntregarEn = @EntregarEn
	And Folio = @Folio


	--Set @TextAUX = 'Select IdProducto From VentasDet '
	Set @TextAUX =
		'[ Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And ' + 
		'IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And IdFarmacia = ' + Char(39) +  @IdFarmacia + Char(39) + ' And FolioOrden = ' + Char(39) + @Folio + Char(39) + '  ]' 

	--Set @Text = '[ Where IdProducto in (' + @TextAUX + ' ]' 
	Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'COM_OCEN_OrdenesCompra_Claves_Enc' + Char(39) + ', ' + @TextAUX --+ ', 0, ' + @TextAUX2
	exec (@Text)

	----Set @Text = '[ Where IdProducto in (' + @TextAUX + ' ]' 
	Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'COM_OCEN_OrdenesCompra_CodigosEAN_Det' + Char(39) + ', ' + @TextAUX --+ ', 0, ' + @TextAUX2
	exec (@Text)



End 
Go--#SQL