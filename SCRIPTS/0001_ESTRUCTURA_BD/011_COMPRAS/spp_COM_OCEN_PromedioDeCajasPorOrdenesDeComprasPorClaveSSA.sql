
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_COM_OCEN_PromedioDeCajasPorOrdenesDeComprasPorClaveSSA' and xType = 'P' ) 
   Drop Proc spp_COM_OCEN_PromedioDeCajasPorOrdenesDeComprasPorClaveSSA
Go--#SQL

Create Proc spp_COM_OCEN_PromedioDeCajasPorOrdenesDeComprasPorClaveSSA
( 
    @IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '11' ,@IdFarmacia Varchar(4) = '0001', 
    @ClaveSSA Varchar(100) =  '010.000.4433.00', @NumCompras varchar(4) = '3'
)  
With Encryption 
As 
Begin 
Set NoCount On

	Declare
			@IdClaveSSA Varchar(8),
			@iPzas Numeric(14, 4),
			@iProm Numeric(14, 4),
			@sSql Varchar(5000)
	
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Temp_OrdenesCompras_CodigosEAN_Det' and xType = 'U' )
	Begin 
		Drop Table Temp_OrdenesCompras_CodigosEAN_Det
	End

	Set @sSql = 
	'Select Top ' + @NumCompras + ' * Into Temp_OrdenesCompras_CodigosEAN_Det
	From vw_OrdenesCompras_CodigosEAN_Det D
	Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' And IdEstado = ' + Char(39) + @IdEstado + Char(39) + 
	' And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' And ClaveSSA = ' + Char(39) + @ClaveSSA + Char(39) + ' Order By Folio Desc'
	
	Exec (@sSql)
	
	Select @NumCompras = COUNT (*) From Temp_OrdenesCompras_CodigosEAN_Det
	Select @iPzas = Sum(CantidadCajas) From Temp_OrdenesCompras_CodigosEAN_Det
	Select @IdClaveSSA = IdClaveSSA_Sal From Temp_OrdenesCompras_CodigosEAN_Det
	
	Set @iPzas = IsNull(@iPzas, 0)

	If (@iPzas > 0)
	Begin
		Select @iProm = @iPzas / @NumCompras
	End

	Select @IdClaveSSA As IdClaveSSA, @ClaveSSA As ClaveSSA, @iPzas As Piezas, @NumCompras As NumCompras, IsNull(@iProm, 0) As Promedio
	
End
Go--#SQL