If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_AIE_Consumos' and xType = 'P' ) 
   Drop Proc spp_AIE_Consumos 
Go--#SQL 

Create Proc spp_AIE_Consumos 
(
	@IdAccesoExterno varchar(4) = '0001', @FechaInicial varchar(10) = '2012-01-01', @FechaFinal varchar(10) = '2012-02-15' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD     
	
	
--- Lista de Claves acceso permitido 	
	Select ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, EsControlado, EsAntibiotico, Status
	Into #tmpClaves 
	From vw_AIE_Claves_Licitadas  
	Where IdAccesoExterno = @IdAccesoExterno and Status = 'A'


--- Obtener cantidades 
	Select P.ClaveSSA, floor(sum(L.Cantidad / (P.ContenidoPaquete * 1.0))) as Cantidad, 
		 convert(varchar(10), min(E.FechaRegistro), 120) as ConsumoInicial,  
		 convert(varchar(10), max(E.FechaRegistro), 120) as ConsumoFinal 		 
	Into #tmp_Claves_Cantidades 
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 	
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On 
		( 
			D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
			and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN
		) 	
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = D.IdProducto and P.CodigoEAN = D.CodigoEAN ) 	
	Inner Join #tmpClaves C (NoLock) On ( P.ClaveSSA = C.ClaveSSA ) 
	Where 
		E.IdEmpresa = '001' and E.IdEstado = '21' -- and E.IdFarmacia = '0182' 
		and E.IdTipoMovto_Inv In ( 'SV' )  -- 'TS'
		and convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and  @FechaFinal 
		and L.ClaveLote like '%*%' 
		and L.IdSubFarmacia not in ( '01' ) 
	Group by P.ClaveSSA 
--- Obtener cantidades 


--- Salida Final 
--	Select sum(Cantidad) as Cantidad From #tmp_Claves_Cantidades C 	


	Select L.ClaveSSA, L.DescripcionClave, L.Presentacion, C.Cantidad, ConsumoInicial, ConsumoFinal     
	From #tmp_Claves_Cantidades C 
	Inner Join #tmpClaves L On ( C.ClaveSSA = L.ClaveSSA ) 
	Order by L.DescripcionClave	
	
--	sp_listacolumnas vw_AIE_Claves_Licitadas 
End 
Go--#SQL 