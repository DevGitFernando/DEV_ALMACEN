--------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Procesar_ALMN_Consumos_Estatales' and xType = 'P' ) 
	Drop Proc spp_Procesar_ALMN_Consumos_Estatales
Go--#SQL 

----	Exec  spp_Procesar_ALMN_Consumos_Estatales  '001', '11', '0005'

Create Proc spp_Procesar_ALMN_Consumos_Estatales 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005'
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 

Declare 
	@iVenta int
	
	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 	



	Select * Into #tmp_vw_Productos_CodigoEAN From vw_Productos_CodigoEAN (Nolock)

	Delete From ALMN_Consumos_Estatales 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia --and Año = @Año and Mes = @Mes
	
	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, P.IdClaveSSA_Sal,
		P.ContenidoPaquete_ClaveSSA, sum(D.CantidadVendida) as Piezas
	Into #tmpConsumoMes  
	From VentasEnc E (Nolock)
	Inner Join VentasDet D (Nolock)
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta )
	Inner Join #tmp_vw_Productos_CodigoEAN P (Nolock)
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia
		and E.FechaRegistro Between GetDate() and (GetDate()- 180) 
		--and P.IdClaveSSA_Sal = 7 
	Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, P.IdClaveSSA_Sal, P.ContenidoPaquete_ClaveSSA
	
	
	Insert Into #tmpConsumoMes
	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, P.IdClaveSSA_Sal,
		P.ContenidoPaquete_ClaveSSA, sum(D.CantidadEnviada) as Piezas  
	From TransferenciasEnc E (Nolock)
	Inner Join TransferenciasDet D (Nolock)
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioTransferencia = D.FolioTransferencia )
	Inner Join #tmp_vw_Productos_CodigoEAN P (Nolock)
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and TipoTransferencia = 'TS'
		and E.FechaRegistro Between (GetDate()- 180) and GetDate() 
		--and P.IdClaveSSA_Sal = 7 		
	Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, P.IdClaveSSA_Sal, P.ContenidoPaquete_ClaveSSA
	
	
	---Insert Into ALMN_ConsumoMensual_Historico
	Select 
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdClaveSSA_Sal AS IdClaveSSA, T.ContenidoPaquete_ClaveSSA AS ContenidoPaquete,
		(ceiling( sum(T.Piezas)/6)) as Piezas_Mes, 
		(ceiling( sum(T.Piezas)/30 )) as Piezas_Dia, 
		cast(0 as numeric(14, 4)) as Cajas_Mes, 
		0 as Cajas_Dia,
		cast(0 as numeric(14, 4)) as Piezas_Semana, 
		cast(0 as numeric(14, 4)) as Cajas_Semana,
		cast(0 as numeric(14, 4)) as Piezas_StockSegurida, 
		cast(0 as numeric(14, 4)) as Cajas_StockSegurida 
	Into #tmpConsumoMes_Borrador
	From #tmpConsumoMes T	
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdClaveSSA_Sal, T.ContenidoPaquete_ClaveSSA
	
	
	Update T Set T.Piezas_Dia = (ceiling((T.Piezas_Mes) / 30))
	From #tmpConsumoMes_Borrador T 
	
	Update T Set T.Cajas_Mes = (ceiling(T.Piezas_Mes / T.ContenidoPaquete))
	From #tmpConsumoMes_Borrador T 
	
	Update T Set T.Cajas_Dia = (ceiling(T.Cajas_Mes / 30))
	From #tmpConsumoMes_Borrador T 
	
	
	--------------------------------------------------------------------------------
	Update T Set T.Piezas_Semana = (T.Piezas_Dia * 7) 
	From #tmpConsumoMes_Borrador T
	
	Update T Set T.Cajas_Semana = (T.Cajas_Dia * 7) 
	From #tmpConsumoMes_Borrador T
	
	Update T Set T.Piezas_StockSegurida = (T.Piezas_Dia * 3) 
	From #tmpConsumoMes_Borrador T
	
	Update T Set T.Cajas_StockSegurida = (T.Cajas_Dia * 3) 
	From #tmpConsumoMes_Borrador T
	
	
	Insert Into ALMN_Consumos_Estatales 
	(
		IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ContenidoPaquete,
		Piezas_Mes, Piezas_Dia, Cajas_Mes, Cajas_Dia, Piezas_Semana, Cajas_Semana, Piezas_StockSegurida, Cajas_StockSegurida
	)
	Select 
		IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ContenidoPaquete,
		Piezas_Mes, Piezas_Dia, Cajas_Mes, Cajas_Dia, Piezas_Semana, Cajas_Semana, Piezas_StockSegurida, Cajas_StockSegurida
	From #tmpConsumoMes_Borrador
	
	
------------- SALIDA FINAL 	
	Select * From #tmpConsumoMes_Borrador 

End	
Go--#SQL 