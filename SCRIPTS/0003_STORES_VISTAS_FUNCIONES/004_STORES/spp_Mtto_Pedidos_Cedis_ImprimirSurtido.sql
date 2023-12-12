
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ImprimirSurtido_Pedidos_CEDIS' and xType = 'P' ) 
   Drop Proc spp_ImprimirSurtido_Pedidos_CEDIS 
Go--#SQL   

Create Proc spp_ImprimirSurtido_Pedidos_CEDIS 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0103',  
--	@IdFarmaciaPedido varchar(4) = '0103', 
	@FolioPedido varchar(6) = '000001' 
) 
----( 
----	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0103', @FolioPedido varchar(6) = '000001' 
----) 
With Encryption 
As 
Begin 
Set NoCount On 

	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdFarmaciaSolicita, FarmaciaSolicita, Folio, FechaRegistro, 
		Observaciones, IdPersonal, Personal, Status, IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion, 
		Cantidad, 0 as CantidadSurtida, 
		Existencia, StatusClave
	Into #tmpPedido 	
	From vw_Impresion_Pedidos_Cedis (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @FolioPedido 

	Select IdClaveSSA, sum(CantidadAsignada) as Cantidad 
	Into #tmpSurtido 
    From Pedidos_Cedis_Enc_Surtido E (NoLock) 
    Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioSurtido = D.FolioSurtido ) 	
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmaciaPedido = @IdFarmacia and E.FolioPedido = @FolioPedido 	
	Group by IdClaveSSA 
	
	
	Update P Set CantidadSurtida = S.Cantidad 
	From #tmpPedido P (NoLock)
	Inner Join #tmpSurtido S (NoLock) On ( P.IdClaveSSA = S.IdClaveSSA )  

--- Salida Final 
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdFarmaciaSolicita, FarmaciaSolicita, Folio, FechaRegistro, 
		Observaciones, IdPersonal, Personal, Status, IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion, 
		Cantidad, CantidadSurtida, Existencia, StatusClave 
	From #tmpPedido 

End 
Go--#SQL 

--	sp_listacolumnas vw_Impresion_Pedidos_Cedis 
	