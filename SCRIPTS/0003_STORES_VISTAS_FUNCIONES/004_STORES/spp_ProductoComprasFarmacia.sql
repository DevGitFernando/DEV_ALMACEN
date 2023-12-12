If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Spp_ProductoComprasFarmacia' And xType = 'P' )
	Drop Proc Spp_ProductoComprasFarmacia
Go--#SQL

Create Procedure dbo.Spp_ProductoComprasFarmacia ( @IdCodigo varchar(30), @CodigoEAN varchar(30), @IdEstado varchar(2) )
With Encryption 
As
Begin
Declare 
    @iLenCodEAN int  

	Set @iLenCodEAN = 15 
	
	Select Top 1 CR.CodigoEAN, P.IdProducto, P.Descripcion, TP.PorcIva
	From CatProductos_CodigosRelacionados CR (NoLock)
	Inner Join CatProductos P (NoLock) On ( CR.IdProducto = P.IdProducto )
	--Inner Join CatProductos_Estado PE On ( P.IdProducto = PE.IdProducto )
	Inner Join CatTiposDeProducto TP On ( P.IdTipoProducto = TP.IdTipoProducto )
	Where 
		( 
			right(replicate('0', @iLenCodEAN) + CR.CodigoEAN_Interno, @iLenCodEAN) = right(replicate('0', @iLenCodEAN) + @IdCodigo, @iLenCodEAN) 
			Or 
			right(replicate('0', @iLenCodEAN) + CR.CodigoEAN, @iLenCodEAN) = right(replicate('0', @iLenCodEAN) + @CodigoEAN, @iLenCodEAN )  
		) 				
		--And PE.IdEstado = @IdEstado
		And CR.Status = 'A'

End
Go--#SQL