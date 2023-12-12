
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Spp_SalesCapturaPedidosCentros' And xType = 'P' )
	Drop Proc Spp_SalesCapturaPedidosCentros
Go--#SQL

Create Procedure dbo.Spp_SalesCapturaPedidosCentros ( @IdClaveSSA_Sal varchar(4), @ClaveSSA varchar(50) )
As
Begin

		Select ClaveSSA, IdClaveSSA_Sal, Descripcion, 0 as Cantidad
		From CatClavesSSA_Sales(NoLock)
		Where ClaveSSA = @ClaveSSA Or IdClaveSSA_Sal = @IdClaveSSA_Sal And Status = 'A'
		And IdClaveSSA_Sal > 0
End
Go--#SQL
