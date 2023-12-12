


If Exists ( Select Name From Sysobjects (nolock) Where Name = 'fg_Obtener_Costo_ClaveSSA_OrdenCompra' and xType = 'FN' ) 
   Drop Function fg_Obtener_Costo_ClaveSSA_OrdenCompra 
Go--#SQL
 

Create Function dbo.fg_Obtener_Costo_ClaveSSA_OrdenCompra 
(
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioOrden varchar(30), @ClaveSSA varchar(30)
)  
Returns numeric(14, 4)  
With Encryption 
As 
Begin 
Declare @iValor numeric(14, 4)

	Set @iValor = 0

	Select @iValor = IsNull(MIN(PrecioUnitario), 0) From vw_OrdenesCompras_CodigosEAN_Det
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @FolioOrden and ClaveSSA = @ClaveSSA
	
	Return @iValor
	
	
End 
Go--#SQL



----		Select dbo.fg_Obtener_Costo_ClaveSSA_OrdenCompra('001', '21', '2132', '00004303', '010.000.2410.00')