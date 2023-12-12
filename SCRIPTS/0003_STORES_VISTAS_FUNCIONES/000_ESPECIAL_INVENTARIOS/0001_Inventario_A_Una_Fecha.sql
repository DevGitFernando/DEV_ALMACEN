

--	select *  	from FarmaciaProductos_CodigoEAN (NoLock) 
	
--	select top 10 * from MovtosInv_Det_CodigosEAN order by keyx desc 


--	select top 1 * from MovtosInv_Det_CodigosEAN_Lotes 
	
/* 
	select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, space(20) as Folio, datepart(yy, FechaSistema) as Año   
	into #tmpDatos 
	from MovtosInv_Det_CodigosEAN (NoLock) 
	Where -- IdProducto = '00000008' 
	      -- and 
	      datepart(yy, FechaSistema) = 2009  -- and datepart(mm, FechaSistema) = 12 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, datepart(yy, FechaSistema)  
	order by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN -- , keyx 
	
--  SV00009246 
	
	select * 
	from MovtosInv_Det_CodigosEAN (NoLock) 
	Where IdProducto = '00000167' 
	      and 
	      datepart(yy, FechaSistema) = 2009  -- and datepart(mm, FechaSistema) = 12 
--	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, datepart(yy, FechaSistema)  
	order by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, keyx 
		
*/	
		
/* 		
	select *,  
	(
		Select top 1 FolioMovtoInv 
		From MovtosInv_Det_CodigosEAN F (NoLock) 
		Where F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia 
		      and F.IdProducto = D.IdProducto and F.CodigoEAN = D.CodigoEAN and datepart(yy, F.FechaSistema) = D.Año 
		Order by Keyx desc 
	) as Folio 
	from #tmpDatos D 
*/ 	
	
----	select *,  
----	from #tmpDatos D 
----	inner join 	
----	(
----		Select top 1 * 
----		From MovtosInv_Det_CodigosEAN F (NoLock) 
----		Where F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia 
----		      and F.IdProducto = D.IdProducto and F.CodigoEAN = D.CodigoEAN and datepart(yy, F.FechaSistema) = D.Año 
----		Order by Keyx desc 
----	) as Folio 		
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Inventario_A_Una_Fecha' and  xType = 'P' ) 
   Drop Proc spp_Inventario_A_Una_Fecha 
Go--#SQL 	
	
Create Proc spp_Inventario_A_Una_Fecha ( @Año int = 2009 ) 	
As 
Begin 
	
	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN, X.Folio, M.Existencia, M.Costo as CostoII 
	Into #tmpInventarioInicial     
	From 
	( 	
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN, 	
			(
				Select top 1 FolioMovtoInv 
				From MovtosInv_Det_CodigosEAN F (NoLock) 
				Where Left(F.FolioMovtoInv, 2) = 'II' 
					  and F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia 
					  and F.IdProducto = D.IdProducto and F.CodigoEAN = D.CodigoEAN and datepart(yy, F.FechaSistema) = D.Año 
				Order by FolioMovtoInv desc 
			) as Folio  
		From 
		( 
			select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, -- space(20) as Folio, 
				datepart(yy, FechaSistema) as Año   
			-- into #tmpDatos 
			from MovtosInv_Det_CodigosEAN (NoLock) 
			Where -- IdProducto = '00000008' 
				  -- and 
				  Left(FolioMovtoInv, 2) = 'II' and
				  datepart(yy, FechaSistema) = @Año  -- and datepart(mm, FechaSistema) = 12 
			Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, datepart(yy, FechaSistema)  
		) as D 	
	) as X 
	Inner Join MovtosInv_Det_CodigosEAN M (NoLock) 
		On ( X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia and X.Folio = M.FolioMovtoInv 
		     and X.IdProducto = M.IdProducto and X.CodigoEAN = M.CodigoEAN )  
	order by X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN 



	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN, X.Folio, M.Existencia, M.Costo as CostoTE 
	Into #tmpTE     
	From 
	( 	
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN, 	
			(
				Select top 1 FolioMovtoInv 
				From MovtosInv_Det_CodigosEAN F (NoLock) 
				Where Left(F.FolioMovtoInv, 2) = 'TE' 
					  and F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia 
					  and F.IdProducto = D.IdProducto and F.CodigoEAN = D.CodigoEAN and datepart(yy, F.FechaSistema) = D.Año 
				Order by FolioMovtoInv desc 
			) as Folio  
		From 
		( 
			select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, -- space(20) as Folio, 
				datepart(yy, FechaSistema) as Año   
			-- into #tmpDatos 
			from MovtosInv_Det_CodigosEAN (NoLock) 
			Where -- IdProducto = '00000008' 
				  -- and 
				  Left(FolioMovtoInv, 2) = 'TE' and
				  datepart(yy, FechaSistema) = @Año  -- and datepart(mm, FechaSistema) = 12 
			Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, datepart(yy, FechaSistema)  
		) as D 	
	) as X 
	Inner Join MovtosInv_Det_CodigosEAN M (NoLock) 
		On ( X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia and X.Folio = M.FolioMovtoInv 
		     and X.IdProducto = M.IdProducto and X.CodigoEAN = M.CodigoEAN )  
	order by X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN 
	

	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN, X.Folio, M.Existencia, M.Costo as CostoEE 
	Into #tmpEE     
	From 
	( 	
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN, 	
			(
				Select top 1 FolioMovtoInv 
				From MovtosInv_Det_CodigosEAN F (NoLock) 
				Where Left(F.FolioMovtoInv, 2) = 'EE' 
					  and F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia 
					  and F.IdProducto = D.IdProducto and F.CodigoEAN = D.CodigoEAN and datepart(yy, F.FechaSistema) = D.Año 
				Order by FolioMovtoInv desc 
			) as Folio  
		From 
		( 
			select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, -- space(20) as Folio, 
				datepart(yy, FechaSistema) as Año   
			-- into #tmpDatos 
			from MovtosInv_Det_CodigosEAN (NoLock) 
			Where -- IdProducto = '00000008' 
				  -- and 
				  Left(FolioMovtoInv, 2) = 'EE' and
				  datepart(yy, FechaSistema) = @Año  -- and datepart(mm, FechaSistema) = 12 
			Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, datepart(yy, FechaSistema)  
		) as D 	
	) as X 
	Inner Join MovtosInv_Det_CodigosEAN M (NoLock) 
		On ( X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia and X.Folio = M.FolioMovtoInv 
		     and X.IdProducto = M.IdProducto and X.CodigoEAN = M.CodigoEAN )  
	order by X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN 



	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN, X.Folio, X.TasaIva, M.Existencia, M.Costo 
	Into #tmpInventario    
	From 
	( 	
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN, TasaIva, 	
			(
				Select top 1 FolioMovtoInv 
				From MovtosInv_Det_CodigosEAN F (NoLock) 
				Where F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia 
					  and F.IdProducto = D.IdProducto and F.CodigoEAN = D.CodigoEAN and datepart(yy, F.FechaSistema) = D.Año 
				Order by Keyx desc 
			) as Folio  
		From 
		( 
			select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, TasaIva, -- space(20) as Folio, 
				datepart(yy, FechaSistema) as Año   
			-- into #tmpDatos 
			from MovtosInv_Det_CodigosEAN (NoLock) 
			Where -- IdProducto = '00000008' 
				  -- and 
				  datepart(yy, FechaSistema) = @Año  -- and datepart(mm, FechaSistema) = 12 
			Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, TasaIva, datepart(yy, FechaSistema)  
		) as D 	
	) as X 
	Inner Join MovtosInv_Det_CodigosEAN M (NoLock) 
		On ( X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia and X.Folio = M.FolioMovtoInv 
		     and X.IdProducto = M.IdProducto and X.CodigoEAN = M.CodigoEAN )  
	order by X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN 
	

	Select X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN, X.Folio, M.CostoUnitario as Costo 
	Into #tmpCompras     
	From 
	( 	
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdProducto, D.CodigoEAN, 	
			(
				Select top 1 F.Folio 
				From vw_Impresion_Compras F (NoLock) 
				Where F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia 
					  and F.IdProducto = D.IdProducto and F.CodigoEAN = D.CodigoEAN and datepart(yy, F.FechaSistema) = D.Año 
				Order by F.Folio desc 
			) as Folio  
		From 
		( 
			select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, -- space(20) as Folio, 
				datepart(yy, FechaSistema) as Año   
			-- into #tmpDatos 
			from vw_Impresion_Compras (NoLock) 
			Where -- IdProducto = '00000008' 
				  -- and 
				  datepart(yy, FechaSistema) = @Año  -- and datepart(mm, FechaSistema) = 12 
			Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, datepart(yy, FechaSistema)  
		) as D 	
	) as X 
	Inner Join vw_Impresion_Compras M (NoLock) 
		On ( X.IdEmpresa = M.IdEmpresa and X.IdEstado = M.IdEstado and X.IdFarmacia = M.IdFarmacia and X.Folio = M.Folio 
		     and X.IdProducto = M.IdProducto and X.CodigoEAN = M.CodigoEAN )  
	order by X.IdEmpresa, X.IdEstado, X.IdFarmacia, X.IdProducto, X.CodigoEAN 
	

	




--- Salida Final 
	Update I  Set Costo = C.Costo 
	From #tmpInventario I 
	Inner Join #tmpCompras C On ( I.IdEmpresa = C.IdEmpresa and I.IdEstado = C.IdEstado and 
			I.IdFarmacia = C.IdFarmacia and I.IdProducto = C.IdProducto and I.CodigoEAN = C.CodigoEAN ) 	
	Where I.Costo = 0 


	Update I  Set Costo = C.CostoII 
	From #tmpInventario I 
	Inner Join #tmpInventarioInicial C On ( I.IdEmpresa = C.IdEmpresa and I.IdEstado = C.IdEstado and 
			I.IdFarmacia = C.IdFarmacia and I.IdProducto = C.IdProducto and I.CodigoEAN = C.CodigoEAN ) 	
	Where I.Costo = 0 
	
	
	Update I  Set Costo = C.CostoTE 
	From #tmpInventario I 
	Inner Join #tmpTE C On ( I.IdEmpresa = C.IdEmpresa and I.IdEstado = C.IdEstado and 
			I.IdFarmacia = C.IdFarmacia and I.IdProducto = C.IdProducto and I.CodigoEAN = C.CodigoEAN ) 	
	Where I.Costo = 0 	
			
	Update I  Set Costo = C.CostoEE 
	From #tmpInventario I 
	Inner Join #tmpEE C On ( I.IdEmpresa = C.IdEmpresa and I.IdEstado = C.IdEstado and 
			I.IdFarmacia = C.IdFarmacia and I.IdProducto = C.IdProducto and I.CodigoEAN = C.CodigoEAN ) 	
	Where I.Costo = 0 	


 
--	Select *, D.CostoII  
--	From 
--	( 
--		Select * 
--		From 
--		( 
--			Select I.*, IsNull(C.Costo, -1) as CostoCompra   
--			From #tmpInventario I  
--			Left Join #tmpCompras C On ( I.IdEmpresa = C.IdEmpresa and I.IdEstado = C.IdEstado and 
--					I.IdFarmacia = C.IdFarmacia and I.IdProducto = C.IdProducto and I.CodigoEAN = C.CodigoEAN ) 
--			Where I.Costo = 0 	
--		) as D 
--		Where CostoCompra <= 0 
--	) as X 
--	Left Join #tmpInventarioInicial D On ( X.IdEmpresa = D.IdEmpresa and X.IdEstado = D.IdEstado and 
--		X.IdFarmacia = D.IdFarmacia and X.IdProducto = D.IdProducto and X.CodigoEAN = D.CodigoEAN ) 	
 

	select I.IdEstado, F.Estado, I.IdFarmacia, F.Farmacia, I.IdProducto, I.CodigoEAN, P.Descripcion, P.TipoDeProducto, 
	     I.Existencia, I.Costo, I.TasaIva, 
	     (I.Existencia * I.Costo) as SubTotal, 
	     (I.Existencia * I.Costo) * ( (I.TasaIva / 100.0) )as Iva, 
	     (I.Existencia * I.Costo) * ( 1 + (I.TasaIva / 100.0) )  as Total  
	from #tmpInventario I 
	Inner Join vw_Farmacias F On ( I.IdEstado = F.IdEstado and I.IdFarmacia = F.IdFarmacia )  
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( I.IdProducto = P.IdProducto and I.CodigoEAN = P.CodigoEAN  )
	Where Existencia > 0  and Costo > 0 
	
End 
Go--#SQL 		


---  select top 1 * from vw_Farmacias 

---  select top 1 * from vw_Productos_CodigoEAN 


----   spp_Inventario_A_Una_Fecha  
		
		
--- SV00009246		-- 7501563010107  -- 7501563020373 

--		select * 		from MovtosInv_Det_CodigosEAN 		Where  CodigoEAN = '7503001007281'  -- FolioMovtoInv = 'SV00023844' 	order by FechaSistema 
	 
	