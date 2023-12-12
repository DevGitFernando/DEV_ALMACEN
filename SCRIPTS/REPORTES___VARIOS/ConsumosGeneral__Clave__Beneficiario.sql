
----------------------------- Tabla Base 
	If exists ( select * from tempdb..sysobjects where name like '#tmpVentas%' and xType = 'U' ) 
		Drop Table #tmpVentas 

	Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, space(200) as Farmacia, 
		space(4) as IdJurisdiccion, space(150) as Jurisdiccion, 
		D.FolioVenta, 
		V.IdCliente, V.IdSubCliente, 
		space(10) as IdBeneficiario, space(200) as Beneficiario, space(100) as FolioReferencia, 
		space(50) as NumReceta, 
		getdate() as FechaReceta, 
		P.IdProducto, P.CodigoEAN, P.ClaveSSA, P.DescripcionClave, P.Laboratorio, cast(sum(CantidadVendida) as int) as CantidadVendida 
	Into #tmpVentas 	
	From VentasDet D (NoLock) 
	Inner Join VentasEnc V (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioVenta = V.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) 
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and P.ClaveSSA = '4158' ) 	
	Group by 
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioVenta,  V.IdCliente, V.IdSubCliente, 
		P.IdProducto, P.CodigoEAN, P.Laboratorio, P.ClaveSSA, P.DescripcionClave  
		
	
	Update V Set Farmacia = F.Farmacia, IdJurisdiccion = F.IdJurisdiccion, Jurisdiccion = F.Jurisdiccion 
	From #tmpVentas V 
	Inner Join vw_Farmacias F On ( F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia  ) 
----------------------------- Tabla Base 


----------------------------- Completar la informacion del Beneficiario  
	Update V Set IdBeneficiario = D.IdBeneficiario, NumReceta = D.NumReceta, FechaReceta = D.FechaReceta  
	From #tmpVentas V 
	Inner Join VentasInformacionAdicional D (NoLock) 
		On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioVenta = V.FolioVenta ) 

	Update V Set Beneficiario = (ApPaterno + ' ' + ApMaterno + ' ' + Nombre), FolioReferencia = B.FolioReferencia  
	From #tmpVentas V 
	Inner Join CatBeneficiarios B 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente 
			 and V.IdBeneficiario = B.IdBeneficiario ) 	
----------------------------- Completar la informacion del Beneficiario  


		
		
------------------------------ 		
	Select * 
	From #tmpVentas 
			