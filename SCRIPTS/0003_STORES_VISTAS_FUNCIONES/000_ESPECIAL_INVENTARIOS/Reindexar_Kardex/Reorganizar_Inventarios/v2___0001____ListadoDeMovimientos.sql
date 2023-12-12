

	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'MovtosInv_Enc____Secuencia' and xType = 'U' ) 
	   Drop Table MovtosInv_Enc____Secuencia 

	Select  
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 0 as EsEntrada, 
		dateadd(mm, -7, FechaSistema) as FechaSistema, 
		dateadd(mm, -7, FechaRegistro) as FechaRegistro, 
		Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, 
		identity(int, 1, 1) as Keyx, 
		Actualizado, FolioCierre, Cierre, IdPersonalHuella 
	Into MovtosInv_Enc____Secuencia 
	From MovtosInv_Enc (NoLock) 
	where IdFarmacia = 105 
		and IdTipoMovto_Inv not in ( 'SV' ) 
		and 1= 0 
	order by Keyx, left(FolioMovtoInv, 8)   

	
---		sp_listacolumnas MovtosInv_Det_CodigosEAN_Lotes 

Go--#SQL 


	Insert Into MovtosInv_Enc____Secuencia 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, EsEntrada, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, IdPersonalRegistra, 
		Observaciones, SubTotal, Iva, Total, Status, Actualizado, FolioCierre, Cierre, IdPersonalHuella 
	) 
	Select  
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, (case when TipoES = 'E' then 1 else 0 end) as EsEntrada, 
		dateadd(mm, -7, FechaSistema) as FechaSistema, 
		dateadd(mm, -7, FechaRegistro) as FechaRegistro, 
		Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, 
		-- identity(int, 1, 1) as Keyx, 
		Actualizado, FolioCierre, Cierre, IdPersonalHuella 
	From MovtosInv_Enc (NoLock) 
	where IdFarmacia = 3 
		--and IdTipoMovto_Inv not in ( 'SV' ) 
	order by keyx, left(FolioMovtoInv, 8)   
Go--#SQL 



	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'MovtosInv_Enc____Secuencia_Ventas' and xType = 'U' ) 
	   Drop Table MovtosInv_Enc____Secuencia_Ventas  

	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, EsEntrada, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, IdPersonalRegistra, 
		Observaciones, SubTotal, Iva, Total, Status, Actualizado, FolioCierre, Cierre, IdPersonalHuella 
	Into MovtosInv_Enc____Secuencia_Ventas 
	From 
	( 
		Select  
			IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, (case when TipoES = 'E' then 1 else 0 end) as EsEntrada, 
			-- dateadd(mm, -7, FechaSistema) as FechaSistema, 
			-- dateadd(mm, -7, FechaRegistro) as FechaRegistro, 
			FechaSistema, 
			FechaRegistro, 
			Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, 
			-- identity(int, 1, 1) as Keyx, 
			Keyx, 
			Actualizado, FolioCierre, Cierre, IdPersonalHuella,
			left(FolioMovtoInv, 8) as Folio  
		From MovtosInv_Enc (NoLock) 
		where IdFarmacia = 105 
			and 1 = 0 
			and IdTipoMovto_Inv in ( 'SV' ) 
	) R 
	--order by left(FolioMovtoInv, 8)   
	Order by Folio-- , Keyx  


Go--#SQL 


	Insert Into MovtosInv_Enc____Secuencia 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, EsEntrada, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, IdPersonalRegistra, 
		Observaciones, SubTotal, Iva, Total, Status, Actualizado, FolioCierre, Cierre, IdPersonalHuella 
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, EsEntrada, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, IdPersonalRegistra, 
		Observaciones, SubTotal, Iva, Total, Status, Actualizado, FolioCierre, Cierre, IdPersonalHuella 
	From MovtosInv_Enc____Secuencia_Ventas 
	Order by FolioMovtoInv 
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'MovtosInv_Enc____Secuencia_EAN' and xType = 'U' ) 
	   Drop Table MovtosInv_Enc____Secuencia_EAN 


	Select 
		(case when TipoES = 'E' then 1 else 0 end) as EsEntrada, 
		0 as AcumuladoEntradas, 
		0 as AcumuladoSalidas, 
		0 as ExistenciaAcumulada, 
		0 as ExistenciaFinal,  
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioMovtoInv, D.IdProducto, D.CodigoEAN, D.FechaSistema, D.UnidadDeSalida, 
		D.TasaIva, D.Cantidad, D.Costo, D.Importe, D.Existencia, D.Status, identity(int, 1, 1) as Keyx, D.Actualizado 
	Into MovtosInv_Enc____Secuencia_EAN 
	From MovtosInv_Enc____Secuencia M (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioMovtoInv = D.FolioMovtoInv ) 
	Where D.Cantidad > 0 
	Order by M.Keyx, D.IdProducto, D.CodigoEAN 
	 
Go--#SQL 




---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'MovtosInv_Enc____Secuencia_EAN_Lotes' and xType = 'U' ) 
	   Drop Table MovtosInv_Enc____Secuencia_EAN_Lotes 

	Select 
		(case when TipoES = 'E' then 1 else 0 end) as EsEntrada, 
		0 as AcumuladoEntradas,
		0 as AcumuladoSalidas, 
		0 as ExistenciaAcumulada, 
		0 as ExistenciaFinal,  
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioMovtoInv, 
		L.IdProducto, L.CodigoEAN, L.ClaveLote, L.EsConsignacion, 
		L.Cantidad, L.Costo, L.Importe, L.Existencia, L.Status, identity(int, 1, 1) as Keyx, L.Actualizado 
	Into MovtosInv_Enc____Secuencia_EAN_Lotes  
	From MovtosInv_Enc____Secuencia M (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioMovtoInv = D.FolioMovtoInv ) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
			and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN  ) 
	Where D.Cantidad > 0 
	Order by M.Keyx, L.IdSubFarmacia, D.IdProducto, D.CodigoEAN, ClaveLote 
	 
Go--#SQL 



---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'MovtosInv_Enc____Secuencia_EAN_Lotes_Ubicaciones' and xType = 'U' ) 
	   Drop Table MovtosInv_Enc____Secuencia_EAN_Lotes_Ubicaciones 

	Select 
		(case when TipoES = 'E' then 1 else 0 end) as EsEntrada, 
		0 as AcumuladoEntradas,
		0 as AcumuladoSalidas, 
		0 as ExistenciaAcumulada, 
		0 as ExistenciaFinal,  
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.FolioMovtoInv, 
		L.IdProducto, L.CodigoEAN, L.ClaveLote, L.EsConsignacion, 
		U.IdPasillo, U.IdEstante, U.IdEntrepaño, 
		L.Cantidad, L.Costo, L.Importe, L.Existencia, L.Status, identity(int, 1, 1) as Keyx, L.Actualizado 
	Into MovtosInv_Enc____Secuencia_EAN_Lotes_Ubicaciones  
	From MovtosInv_Enc____Secuencia M (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioMovtoInv = D.FolioMovtoInv ) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
			and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN  ) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones U (NoLock) 
		On ( L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia and L.FolioMovtoInv = U.FolioMovtoInv 
			and L.IdProducto = U.IdProducto and L.CodigoEAN = U.CodigoEAN ) 
	Where D.Cantidad > 0 
	Order by M.Keyx, L.IdSubFarmacia, D.IdProducto, D.CodigoEAN, ClaveLote 
	 
Go--#SQL 





---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'Existencia__Secuencia__01_Producto' and xType = 'U' ) 
	   Drop Table Existencia__Secuencia__01_Producto 

	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, 0 as Existencia  
	Into Existencia__Secuencia__01_Producto 
	From MovtosInv_Enc____Secuencia_EAN_Lotes (NoLock) 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto 



---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'Existencia__Secuencia__02_EAN' and xType = 'U' ) 
	   Drop Table Existencia__Secuencia__02_EAN 

	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, 0 as Existencia, 0 as Procesado 
	Into Existencia__Secuencia__02_EAN 
	From MovtosInv_Enc____Secuencia_EAN_Lotes (NoLock) 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN 


---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'Existencia__Secuencia__03_Lotes' and xType = 'U' ) 
	   Drop Table Existencia__Secuencia__03_Lotes  

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 0 as Existencia, 0 as Procesado 
	Into Existencia__Secuencia__03_Lotes 
	From MovtosInv_Enc____Secuencia_EAN_Lotes (NoLock) 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote  

Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	If Exists ( Select * From Sysobjects (Nolock) Where Name = 'Existencia__Secuencia__03_Lotes_Ubicaciones' and xType = 'U' ) 
	   Drop Table Existencia__Secuencia__03_Lotes_Ubicaciones  

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, 0 as Existencia, 0 as Procesado 
	Into Existencia__Secuencia__03_Lotes_Ubicaciones 
	From MovtosInv_Enc____Secuencia_EAN_Lotes_Ubicaciones (NoLock) 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño  

Go--#SQL 
