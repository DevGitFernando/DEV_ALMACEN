If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_OrdenDeCompraPermiteDescarga' and xType = 'FN' ) 
   Drop Function fg_OrdenDeCompraPermiteDescarga 
Go--#SQL

Create Function dbo.fg_OrdenDeCompraPermiteDescarga
(
	@IdEmpresa Varchar(3) = '001', @IdEstado varchar(2) = '11', @EntregarEn varchar(4) = '0003', @FolioOrden varchar(8) = '00000005'
)  
Returns Bit  
With Encryption 
As 
Begin 
Declare 
	@Return Bit,
	@IdFarmacia Varchar(4)

	Select @Return = 0 
	Select @IdFarmacia = IdFarmacia
	From COM_OCEN_OrdenesCompra_Claves_Enc
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And EntregarEn = @EntregarEn And FolioOrden = @FolioOrden

	
	If Exists (Select * From SysObjects Where Xtype = 'U' And Name = 'COM_OCEN_OrdenesCompra_Claves_Status')
		Begin
			--Set @Return = 1
			Select @Return = PermiteDescarga
			From vw_COM_OCEN_OrdenesCompra_Claves_Status
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden
		End
	
	
	Return @Return
	
End  
Go--#SQL