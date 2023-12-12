------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_ObtenerInformacionVenta' and xType = 'P' ) 
   Drop Proc spp_FACT_ObtenerInformacionVenta 
Go--#SQL 

Create Proc spp_FACT_ObtenerInformacionVenta 
(
	@IdEmpresa Varchar(3) = '1', @IdEstado Varchar(2)= '13', @IdFarmacia Varchar(4) = '3', @FolioVenta Varchar(8) = '00000099'
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

	--Declare @IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2)= '13', @IdFarmacia Varchar(4) = '0003', @FolioVenta Varchar(8) = '00000610'


Declare 
	@IdCliente Varchar(4), 
	@IdSubCliente Varchar(4), 
	@IdBeneficiario Varchar(8), 
	@IdMedico Varchar(6), 
	@Text Varchar(Max),
	@TextAUX Varchar(Max),
	@TextAUX2 Varchar(Max)


	Set @IdEmpresa = RIGHT('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = RIGHT('000000000000' + @IdFarmacia, 4) 
	Set @FolioVenta = RIGHT('000000000000' + @FolioVenta, 8) 



	Select @IdCliente = IdCliente, @IdSubCliente = IdSubCliente
	From VentasEnc E (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta

	Select @IdBeneficiario = IdBeneficiario, @IdMedico = IdMedico
	From VentasInformacionAdicional E (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta


	if (@IdBeneficiario <> '')
	Begin
			Set @TextAUX = 'Select IdProducto From VentasDet '
			Set @TextAUX2 =
				'[ Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And ' + 
				'IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And IdFarmacia = ' + Char(39) +  @IdFarmacia + Char(39) + ' And FolioVenta = ' + Char(39) + @FolioVenta + Char(39) + ' ) ]' 

			Set @Text = '[ Where IdProducto in (' + @TextAUX + ' ]' 
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'FarmaciaProductos' + Char(39) + ', ' + @Text + ', 0, ' + @TextAUX2
			exec (@Text)

			Set @Text = '[ Where IdProducto in (' + @TextAUX + ' ]' 
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'FarmaciaProductos_CodigoEAN' + Char(39) + ', ' + @Text + ', 0, ' + @TextAUX2
			Exec (@Text)

			Set @Text = '[ Where IdProducto in (' + @TextAUX + ' ]' 
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'FarmaciaProductos_CodigoEAN_Lotes' + Char(39) + ', ' + @Text + ', 0, ' + @TextAUX2
			Exec (@Text)

			Set @Text = '[ Where IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And IdMedico= ' + Char(39) + @IdMedico + Char(39) + ' ] ' 
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'CatMedicos' + Char(39) + ', ' + @Text 	
			Exec (@Text)


			Set @Text = '[ Where IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) +
						' And IdCliente= ' + Char(39) + @IdCliente + Char(39) + ' And IdSubCliente= ' + Char(39) + @IdSubCliente + Char(39) +
						' And IdBeneficiario= ' + Char(39) + @IdBeneficiario + Char(39) + ' ] ' 
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'CatBeneficiarios' + Char(39) + ', ' + @Text 	
			Exec (@Text)


			Set @Text = '[ Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And IdEstado = ' + Char(39) + @IdEstado + Char(39) +
						' And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And FolioVenta = ' + Char(39) + @FolioVenta + Char(39) + ' ] ' 
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'VentasEnc' + Char(39) + ', ' + @Text 	
			Exec (@Text)

			Set @Text = '[ Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And IdEstado = ' + Char(39) + @IdEstado + Char(39) +
						' And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And FolioVenta = ' + Char(39) + @FolioVenta + Char(39) + ' ] ' 
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'VentasInformacionAdicional' + Char(39) + ', ' + @Text 	
			Exec (@Text)

			Set @Text = '[ Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And IdEstado = ' + Char(39) + @IdEstado + Char(39) +
						' And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And FolioVenta= ' + Char(39) + @FolioVenta + Char(39) + ' ] ' 
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'VentasDet' + Char(39) + ', ' + @Text 	
			Exec (@Text)
	

			Set @Text = '[ Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And IdEstado = ' + Char(39) + @IdEstado + Char(39) +
						' And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And FolioVenta= ' + Char(39) + @FolioVenta + Char(39) + ' ] '  
			Set @Text = 'Exec spp_CFG_ObtenerDatos ' + Char(39) + 'VentasDet_Lotes' + Char(39) + ', ' + @Text
			Exec (@Text)
	End



End 
Go--#SQL