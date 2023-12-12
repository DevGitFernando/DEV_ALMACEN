----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_PRCS_AnalisisCB__Surtimiento' and xType = 'P' ) 
   Drop Proc spp_PRCS_AnalisisCB__Surtimiento  
Go--#SQL 

--		Exec spp_PRCS_AnalisisCB__Surtimiento @IdEmpresa = '1', @IdEstado = '13', @IdCliente = '2', @IdSubCliente = '10', @Año = 2017, @Mes = 3 

Create Proc spp_PRCS_AnalisisCB__Surtimiento 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '28', @IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '11', 
	@Año int = 2017, @Mes int = 6   
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 


-------------------- Formatear parametros 
	Set @IdEmpresa = RIGHT('00000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('00000000' + @IdEstado, 2) 
	Set @IdCliente = RIGHT('00000000' + @IdCliente, 4) 
	Set @IdSubCliente = RIGHT('00000000' + @IdSubCliente, 4) 
-------------------- Formatear parametros 

-------------------- Claves de Cuadro basico general 
	Select * 
	Into #tmpCB 
	From vw_Claves_Precios_Asignados 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and Status = 'A' 

	Select * 
	Into #tmpFarmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado and IdTipoUnidad Not In ( '000', '005' ) 


	Select 
		F.IdEstado, F.IdFarmacia, 
		C.ClaveSSA, C.DescripcionClave, 
		C.ClaveSSA as ClaveSSA_Base, C.DescripcionClave as DescripcionClave_Base, 		
		cast('' as varchar(1000)) as Presentacion, C.TipoClaveDescripcion, 
		C.ContenidoPaquete, 
		0 as CantidadProgramada, 
		@Año as Año, @Mes as Mes, 
		0 as T01, 0 as T02, 0 as T03, 0 as T04, 0 as T05, 0 as TotalTransferencias,  
		0 as D01, 0 as D02, 0 as D03, 0 as D04, 0 as D05, 0 as TotalDispensacion 		 
	Into #tmpMatriz 
	From #tmpCB C , #tmpFarmacias F 
	-- Where F.IdTipoUnidad <> '006' 

-------------------- Claves de Cuadro basico general 


-------------------- Obtener información de dispensacion 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
		year(V.FechaRegistro) as Año, month(FechaRegistro) as Mes, 
		((DATEPART(week, convert(varchar(10), V.FechaRegistro, 120)) - 
		DATEPART(week, DATEADD(dd, - DAY(convert(varchar(10), V.FechaRegistro, 120)) + 1, convert(varchar(10), V.FechaRegistro, 120)))) + 1 ) as SemanaMes, 	
		DATEPART(week, convert(varchar(10), V.FechaRegistro, 120)) as SemanaAño, 
		P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, 
		P.IdClaveSSA_Sal as IdClaveSSA_Base, P.ClaveSSA as ClaveSSA_Base, 
		sum(L.CantidadVendida) as Cantidad, 0 as Relacionada  
	Into #tmp_Ventas 
	From VentasEnc V (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	Inner Join VentasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
			and D.IdProducto = L.IdProducto  and D.CodigoEAN = L.CodigoEAN and L.ClaveLote not like '%*%' ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
	Where year(V.FechaRegistro) = @Año and month(V.FechaRegistro) = @Mes 
		-- and V.IdFarmacia >= 11  -- and V.IdFarmacia = 11 
		-- and P.ClaveSSA = '010.000.0624.00' 
	Group by 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
		year(V.FechaRegistro), month(FechaRegistro), 	
		convert(varchar(10), V.FechaRegistro, 120), 
		P.IdClaveSSA_Sal, P.ClaveSSA 


	Update V Set ClaveSSA = R.ClaveSSA, IdClaveSSA_Base = R.IdClaveSSA, Relacionada = 1 
	From #tmp_Ventas V 
	Inner Join vw_Relacion_ClavesSSA_Claves R (NoLock) 
		On ( V.IdEstado = R.IdEstado and V.ClaveSSA = R.ClaveSSA_Relacionada and R.Status = 'A' ) 
-------------------- Obtener información de dispensacion 



-------------------- Obtener información de transferencias  
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdEstadoRecibe, V.IdFarmaciaRecibe, 
		year(V.FechaRegistro) as Año, month(FechaRegistro) as Mes, 
		((DATEPART(week, convert(varchar(10), V.FechaRegistro, 120)) - 
		DATEPART(week, DATEADD(dd, - DAY(convert(varchar(10), V.FechaRegistro, 120)) + 1, convert(varchar(10), V.FechaRegistro, 120)))) + 1 ) as SemanaMes, 	
		DATEPART(week, convert(varchar(10), V.FechaRegistro, 120)) as SemanaAño, 
		P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, 
		P.IdClaveSSA_Sal as IdClaveSSA_Base, P.ClaveSSA as ClaveSSA_Base, 		
		sum(L.CantidadEnviada) as Cantidad, 0 as Relacionada  
	Into #tmp_Transferencias  
	From TransferenciasEnc V (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and F.IdTipoUnidad = '006' ) 
	Inner Join TransferenciasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioTransferencia = D.FolioTransferencia ) 
	Inner Join TransferenciasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioTransferencia = L.FolioTransferencia 
			and D.IdProducto = L.IdProducto  and D.CodigoEAN = L.CodigoEAN and L.ClaveLote not like '%*%' ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where year(V.FechaRegistro) = @Año and month(V.FechaRegistro) = @Mes 
		and V.IdFarmacia = 3 
		-- and V.IdFarmaciaRecibe = 14 
		and V.TipoTransferencia = 'TS'  
		-- and P.ClaveSSA = '010.000.0624.00' 
	Group by 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdEstadoRecibe, V.IdFarmaciaRecibe, 
		year(V.FechaRegistro), month(FechaRegistro), 	
		convert(varchar(10), V.FechaRegistro, 120), 
		P.IdClaveSSA_Sal, P.ClaveSSA 

	Update V Set ClaveSSA = R.ClaveSSA, IdClaveSSA_Base = R.IdClaveSSA, Relacionada = 1 
	From #tmp_Transferencias V 
	Inner Join vw_Relacion_ClavesSSA_Claves R (NoLock) 
		On ( V.IdEstado = R.IdEstado and V.ClaveSSA = R.ClaveSSA_Relacionada and R.Status = 'A' ) 


----		spp_PRCS_AnalisisCB__Surtimiento	


--	Select * From #tmp_Transferencias 
-------------------- Obtener información de transferencias 


-------------------- Completar la información de dispensación 
	Update M Set D01 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Ventas V (NoLock) 
			Where M.IdEstado = V.IdEstado and M.IdFarmacia = V.IdFarmacia and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 1 
		), 0 ) 
	From #tmpMatriz M 
		 
	Update M Set D02 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Ventas V (NoLock) 
			Where M.IdEstado = V.IdEstado and M.IdFarmacia = V.IdFarmacia and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 2 
		), 0 ) 
	From #tmpMatriz M 

	Update M Set D03 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Ventas V (NoLock) 
			Where M.IdEstado = V.IdEstado and M.IdFarmacia = V.IdFarmacia and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 3 
		), 0 ) 
	From #tmpMatriz M 

	Update M Set D04 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Ventas V (NoLock) 
			Where M.IdEstado = V.IdEstado and M.IdFarmacia = V.IdFarmacia and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 4 
		), 0 ) 
	From #tmpMatriz M 

	Update M Set D05 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Ventas V (NoLock) 
			Where M.IdEstado = V.IdEstado and M.IdFarmacia = V.IdFarmacia and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 5 
		), 0 ) 
	From #tmpMatriz M 

	Update M Set TotalDispensacion = 
		IsNull(
		(
			Select sum(Cantidad) From #tmp_Ventas V (NoLock) 
			Where M.IdEstado = V.IdEstado and M.IdFarmacia = V.IdFarmacia and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes 
		), 0 )
	From #tmpMatriz M 
-------------------- Completar la información de dispensación 


-------------------- Completar la información de dispensación 
	Update M Set T01 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Transferencias V (NoLock) 
			Where M.IdEstado = V.IdEstadoRecibe and M.IdFarmacia = V.IdFarmaciaRecibe and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 1 
		), 0 ) 
	From #tmpMatriz M 
		 
	Update M Set T02 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Transferencias V (NoLock) 
			Where M.IdEstado = V.IdEstadoRecibe and M.IdFarmacia = V.IdFarmaciaRecibe and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 2 
		), 0 ) 
	From #tmpMatriz M 

	Update M Set T03 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Transferencias V (NoLock) 
			Where M.IdEstado = V.IdEstadoRecibe and M.IdFarmacia = V.IdFarmaciaRecibe and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 3 
		), 0 ) 
	From #tmpMatriz M 

	Update M Set T04 = 
		IsNull(
		(
			Select sum(Cantidad) From #tmp_Transferencias V (NoLock) 
			Where M.IdEstado = V.IdEstadoRecibe and M.IdFarmacia = V.IdFarmaciaRecibe and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 4 
		), 0 ) 
	From #tmpMatriz M 

	Update M Set T05 = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Transferencias V (NoLock) 
			Where M.IdEstado = V.IdEstadoRecibe and M.IdFarmacia = V.IdFarmaciaRecibe and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes and V.SemanaMes = 5 
		), 0 ) 
	From #tmpMatriz M 

	Update M Set TotalTransferencias = 
		IsNull( 
		(
			Select sum(Cantidad) From #tmp_Transferencias V (NoLock) 
			Where M.IdEstado = V.IdEstadoRecibe and M.IdFarmacia = V.IdFarmaciaRecibe and M.ClaveSSA = V.ClaveSSA and M.Año = V.Año and M.Mes = V.Mes 
		), 0 )
	From #tmpMatriz M 
-------------------- Completar la información de dispensación 




----		spp_PRCS_AnalisisCB__Surtimiento	


------------------------------------------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------------------------------------------ 
-------------------------------------------------- Salida final 
	Update M Set ClaveSSA = C.Mascara, DescripcionClave = C.Descripcion, Presentacion = C.Presentacion 
	From #tmpMatriz M 
	Inner Join vw_ClaveSSA_Mascara C On ( M.IdEstado = C.IdEstado and M.ClaveSSA = C.ClaveSSA ) 


	Select * 
	From #tmpMatriz 


	--Select * 
	--From #tmp_Ventas  
	----Where Relacionada = 1 

	--Select * 
	--From #tmp_Transferencias 
	----Where Relacionada = 1 


	--Select IdEmpresa, IdEstado, IdFarmacia, Año, Mes, SemanaMes, SemanaAño  
	--From #tmp_Transferencias  
	--Group by IdEmpresa, IdEstado, IdFarmacia, Año, Mes, SemanaMes, SemanaAño 
	--Order by IdEmpresa, IdEstado, IdFarmacia, Año, Mes, SemanaMes 


	--Select IdEmpresa, IdEstado, IdFarmacia, Año, Mes, SemanaMes, SemanaAño  
	--From #tmp_Ventas  
	--Group by IdEmpresa, IdEstado, IdFarmacia, Año, Mes, SemanaMes, SemanaAño 
	--Order by IdEmpresa, IdEstado, IdFarmacia, Año, Mes, SemanaMes 

-------------------------------------------------- Salida final 



End 
Go--#SQL 
	

