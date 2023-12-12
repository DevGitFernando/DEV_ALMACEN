If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INT_ND_GenerarExistencias__001_Ejecucion_Detalle' and xType = 'P' ) 
    Drop Proc spp_INT_ND_GenerarExistencias__001_Ejecucion_Detalle 
Go--#SQL

---     Exec spp_INT_ND_GenerarExistencias__001_Ejecucion_Detalle '003', '16', '0011', '05', '2014-10-25'

Create Proc spp_INT_ND_GenerarExistencias__001_Ejecucion_Detalle 
( 
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0011', @IdSubFarmacia varchar(2) = '05',
	@FechaDeProceso varchar(10) = '2014-09-25', @ProcesarExistencia smallint = 0 -- , @SoloDiaSolicidato int = 1  
)
with Encryption 
As 
Begin 
Set NoCount On 	

Declare 
	@sSql varchar(max), 
	@bCrearTablaBase int 	 
	
	----Select * 
	----into vw_Productos_CodigoEAN___PRCS
	----from vw_Productos_CodigoEAN 
	
	
	Set @sSql = '' 
	Set @bCrearTablaBase = 1 
	
	
------------------------------------- Validar la creacion de la tabla base del proceso de existencias 
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN___PRCS' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_Productos_CodigoEAN___PRCS' and xType = 'U' and datediff(dd, crDate, getdate()) > 1
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 
	End 
		

	If @bCrearTablaBase = 1 
	Begin 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN___PRCS' and xType = 'U' ) 
		   Drop Table vw_Productos_CodigoEAN___PRCS 
	
		Select * 
		into vw_Productos_CodigoEAN___PRCS
		from vw_Productos_CodigoEAN 	
	End 

------------------------------------- Validar la creacion de la tabla base del proceso de existencias 		
	
	-------  Se genera la tabla de productos--lotes que se movieron en el dia --------------------------------------------------------------
	Select 
		@FechaDeProceso as FechaDeProceso, 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, L.IdSubFarmacia,  
		L.IdProducto, L.CodigoEAN, L.ClaveLote, Max(L.Keyx) as Keyx, 0 As Existencia, 
		max(P.ContenidoPaquete *  1.00) as ContenidoPaquete, 
		0 as PiezasTotales, 0 as PiezasSueltas 
	Into #Productos_Lotes_Existencia 
	from MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.FolioMovtoInv = L.FolioMovtoInv ) 
	Inner Join vw_Productos_CodigoEAN___PRCS P (NoLock) On ( P.IdProducto = L.IdProducto and P.CodigoEAN = L.CodigoEAN )  				
	Where 
		E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia 
		And Convert(varchar(10), E.FechaRegistro, 120) = @FechaDeProceso 
		and Not Exists 
			( 
				Select * 
				From INT_ND_SubFarmaciasConsigna C (NoLock) 
				Where L.IdEstado = C.IdEstado and L.IdSubFarmacia = C.IdSubFarmacia  		
			) 
	Group by E.IdEmpresa, E.IdEstado, E.IdFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote
	Order By L.IdProducto, L.CodigoEAN  





	
--	select * from #Productos_Lotes_Existencia 
	
------------  se genera la tabla por producto--lote concentrada  ----------------------------------------
----	Select Distinct 
----		E.IdEmpresa, E.IdEstado, E.IdFarmacia, space(200) as Farmacia, E.IdSubFarmacia, 
----		E.IdProducto, E.CodigoEAN, 
----		-- P.Descripcion, P.ClaveSSA, P.DescripcionClave, 
----		E.ClaveLote,
----		(P.ContenidoPaquete * 1.0) as ContenidoPaquete, 
----		E.Existencia, 0 as PiezasTotales, 0 as PiezasSueltas, Max(E.Keyx) as Keyx  
----	Into #tmpProductos_Concentrado_Existencia 
----	From #Productos_Lotes_Existencia E (Nolock) 
----		Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = E.IdProducto and P.CodigoEAN = E.CodigoEAN )
----	Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdSubFarmacia,
----		E.IdProducto, E.CodigoEAN, 
----		-- P.Descripcion, P.ClaveSSA, P.DescripcionClave, 
----		E.ClaveLote, 
----		P.ContenidoPaquete, E.Existencia 
------------  se genera la tabla por producto--lote concentrada  ----------------------------------------	
	

	
------------  se genera la tabla por producto--lote historico 	
		------Select 
		------	L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, 
		------	(P.ContenidoPaquete * 1.0) as ContenidoPaquete, 
		------	0 as Existencia, 0 as PiezasTotales, 0 as PiezasSueltas, 
		------	0 as Keyx 
		------Into #tmpProductos_NoUsados
		------From FarmaciaProductos_CodigoEAN_Lotes L (NoLock)  
		------	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = L.IdProducto and P.CodigoEAN = L.CodigoEAN )	
		------Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and L.IdSubFarmacia = @IdSubFarmacia 
		------And Not Exists 
		------	( 
		------		Select * 
		------		From #Productos_Lotes_Existencia E (NoLock) 
		------		Where L.IdEmpresa = E.IdEmpresa and L.IdEstado = E.IdEstado and L.IdFarmacia = E.IdFarmacia 
		------			  and L.IdSubFarmacia = E.IdSubFarmacia and L.IdProducto = E.IdProducto and L.CodigoEAN = E.CodigoEAN 
		------			  and L.ClaveLote = E.ClaveLote 
		------	) 
		
		
		------Update T Set Keyx = 
		------IsNull(
		------(	
		------	Select top 1 L.Keyx  
		------	From MovtosInv_Det_CodigosEAN D (NoLock)
		------	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		------		On ( L.IdEmpresa = D.IdEmpresa and L.IdEstado = D.IdEstado and L.IdFarmacia = D.IdFarmacia 
		------	  and L.FolioMovtoInv = D.FolioMovtoInv AND L.IdProducto = D.IdProducto and L.CodigoEAN = D.CodigoEAN ) 
		------	Where D.IdEmpresa = T.IdEmpresa and D.IdEstado = T.IdEstado and D.IdFarmacia = T.IdFarmacia 
		------	  and T.IdSubFarmacia = L.IdSubFarmacia         
		------	  and D.IdProducto = T.IdProducto and D.CodigoEAN = T.CodigoEAN 
		------	  and T.ClaveLote = L.ClaveLote 
		------	  and Convert(varchar(10), D.FechaSistema, 120) < @FechaDeProceso  
		------	  Order By D.FechaSistema Desc), 0) 
		------From #tmpProductos_NoUsados T (NoLock) 
    
 ----   Delete From #tmpProductos_NoUsados Where Keyx = 0  	
          	
          	
--------  se genera la tabla por producto--lote historico 	
	
	
	
	
	------------------- Obtener datos de Existencias 
	If @IdEstado = '16' ---- MICHOACAN 
	Begin 
		Update T Set Existencia = floor((E.Existencia/(ContenidoPaquete*1.00))), PiezasTotales = E.Existencia   
		From #Productos_Lotes_Existencia T 
		Inner Join MovtosInv_Det_CodigosEAN_Lotes E  
			On ( E.IdEmpresa = T.IdEmpresa and E.IdEstado = T.IdEstado and E.IdFarmacia = T.IdFarmacia 
				AND E.IdSubFarmacia = T.IdSubFarmacia AND E.Keyx = T.Keyx )	
	End 
		
		
	If @IdEstado = '07' ---- CHIAPAS 
	Begin 
		Update T Set Existencia = E.Existencia, PiezasTotales = E.Existencia   
		From #Productos_Lotes_Existencia T 
		Inner Join MovtosInv_Det_CodigosEAN_Lotes E  
			On ( E.IdEmpresa = T.IdEmpresa and E.IdEstado = T.IdEstado and E.IdFarmacia = T.IdFarmacia 
				AND E.IdSubFarmacia = T.IdSubFarmacia AND E.Keyx = T.Keyx )	
	End 			
	------------------- Obtener datos de Existencias 
	
	
	


	----Update T Set Existencia = floor((E.Existencia/ContenidoPaquete)), PiezasTotales = E.Existencia   
	----From #tmpProductos_NoUsados T 
	----Inner Join MovtosInv_Det_CodigosEAN_Lotes E  
	----	On ( E.IdEmpresa = T.IdEmpresa and E.IdEstado = T.IdEstado and E.IdFarmacia = T.IdFarmacia 
	----		AND E.IdSubFarmacia = T.IdSubFarmacia AND E.Keyx = T.Keyx )	 


	----select * from  #Productos_Lotes_Existencia 
	----select * from  #tmpProductos_NoUsados 
	----select * from  #tmpProductos_Concentrado_Existencia 


------------------------------- Habilitar al final 
	If 	@ProcesarExistencia = 1  
	Begin 
		Set @sSql = ' 
			Update R Set MovtoDia = 1, CajasCompletas = E.Existencia 
			From #tmpExistencia R (NoLock) 
			Inner Join #Productos_Lotes_Existencia E (NoLock)  
				On ( R.IdSubFarmacia = E.IdSubFarmacia and R.IdProducto = E.IdProducto and R.CodigoEAN = E.CodigoEAN and R.ClaveLote = E.ClaveLote ) ' 
		Exec( @sSql ) 
		
		Set @sSql = ' 
			Update R Set MovtoDia = 2, CajasCompletas = E.Existencia 
			From #tmpExistencia R (NoLock) 
			Inner Join #Productos_Lotes_Existencia E (NoLock)  
				On ( R.IdSubFarmacia = E.IdSubFarmacia and R.IdProducto = E.IdProducto and R.CodigoEAN = E.CodigoEAN and R.ClaveLote = E.ClaveLote ) 
			Where MovtoDia = 0 ' 				
		--Exec( @sSql ) 
	End 	
	
	----------------------- Obtener datos de Existencias 
		
	
	
	-----			spp_INT_ND_GenerarExistencias__001_Ejecucion_Detalle 
	
	
End 
Go--#SQL
   
