----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_EsCentralFolioOrdenDeCompra' and xType = 'FN' ) 
   Drop Function fg_EsCentralFolioOrdenDeCompra
Go--#SQL  

Create Function dbo.fg_EsCentralFolioOrdenDeCompra
(
	@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '11', @IdFarmacia Varchar(4) = '0005', @FolioOrdenCompra Varchar(8) = '00003420'
)  
Returns Bit
With Encryption 
As 
Begin
Declare 
	@EsCentral bit


	Select @EsCentral = EsCentral
	From COM_OCEN_OrdenesCompra_Claves_Enc C (NoLock)
	Where C.IdEmpresa  = @IdEmpresa And C.EstadoEntrega  = @IdEstado And C.EntregarEn  = @IdFarmacia  And C.FolioOrden = @FolioOrdenCompra
	
	
	Return IsNull(@EsCentral, 0)
	
End 
Go--#SQL 

