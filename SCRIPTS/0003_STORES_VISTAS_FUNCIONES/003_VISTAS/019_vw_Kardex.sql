------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select  Name From Sysobjects (NoLock) Where Name = 'vw_Kardex_ProductoCodigoEAN_Lotes' and xType = 'V' )
   Drop View vw_Kardex_ProductoCodigoEAN_Lotes 
Go--#SQL	
 
Create View vw_Kardex_ProductoCodigoEAN_Lotes 
With Encryption 
As 	
	Select 
		 Ce.IdEmpresa, Ce.Nombre as Empresa, 	
		 E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, L.IdSubFarmacia, 
		 -- E.FechaRegistro, 
		   cast(convert(varchar(10), E.FechaSistema, 112) as datetime) as FechaSistema, 
		 E.FechaRegistro, 
		 E.FolioMovtoInv as Folio, Tm.TipoMovto,
		 ( Case When len(E.Referencia) > 0 Then Tm.DescMovimiento + ':  Referencia ==> ' + E.Referencia + '' Else Tm.DescMovimiento End ) as DescMovimiento, 
		 E.Referencia, 
		 D.IdProducto, 
		 P.IdClaveSSA_Sal, P.ClaveSSA_Base, P.ClaveSSA, P.ClaveSSA_Aux, P.DescripcionSal, 
		 D.CodigoEAN, D.ClaveLote, P.Descripcion as DescProducto, 
		 (Case When E.TipoES = 'E' Then D.Cantidad Else 0 End) as Entrada, 
		 (Case When E.TipoES = 'S' Then D.Cantidad Else 0 End) as Salida, 		 
		 D.Existencia, L.FechaRegistro as FechaRegistroLote, L.FechaCaducidad, 
		 D.Costo, P.TasaIva, D.Importe, D.Status, 
		 E.Keyx as KeyxMovto, D.Keyx as Keyx   
	From MovtosInv_Enc E (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (NoLock) 
	    On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
    Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
        On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia 
             and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join vw_MovtosInv_Tipos_Farmacia Tm (NoLock) 
	    On ( E.IdEstado = Tm.IdEstado and E.IdFarmacia = Tm.IdFarmacia and E.IdTipoMovto_Inv = Tm.TipoMovto ) 	
	Inner join CatEmpresas Ce (NoLock) On ( Ce.IdEmpresa = E.IdEmpresa ) 
	Where E.MovtoAplicado = 'S' 
	-- Order by D.Keyx 
Go--#SQL 
 	
------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select  Name From Sysobjects (NoLock) Where Name = 'vw_Kardex_ProductoCodigoEAN' and xType = 'V' )
   Drop View vw_Kardex_ProductoCodigoEAN 
Go--#SQL	 

Create View vw_Kardex_ProductoCodigoEAN 
With Encryption 
As 
	Select 
		 Ce.IdEmpresa, Ce.Nombre as Empresa, 
		 E.IdEstado, E.IdFarmacia, E.IdPersonalRegistra, U.NombreCompleto As PersonalRegistraNombre,
		 -- E.FechaRegistro, 
		   cast(convert(varchar(10), E.FechaSistema, 112) as datetime) as FechaSistema, 
		 E.FechaRegistro,   
		 E.FolioMovtoInv as Folio, Tm.TipoMovto, 
		 ( Case When len(E.Referencia) > 0 Then Tm.DescMovimiento + ':  Referencia ==> ' + E.Referencia + '' Else Tm.DescMovimiento End ) as DescMovimiento, 
		 E.Referencia, 
		 D.IdProducto, 
		 P.IdClaveSSA_Sal, P.ClaveSSA_Base, P.ClaveSSA, P.ClaveSSA_Aux, P.DescripcionSal, 		 
		 D.CodigoEAN, -- D.ClaveLote, 
		 P.Descripcion as DescProducto, 
		 (Case When E.TipoES = 'E' Then D.Cantidad Else 0 End) as Entrada, 
		 (Case When E.TipoES = 'S' Then D.Cantidad Else 0 End) as Salida, 		 
		 D.Existencia, D.Costo, P.TasaIva, D.Importe, D.Status, D.Keyx as Keyx   
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join vw_MovtosInv_Tipos_Farmacia Tm (NoLock) On ( E.IdEstado = Tm.IdEstado and E.IdFarmacia = Tm.IdFarmacia and E.IdTipoMovto_Inv = Tm.TipoMovto ) 	
	Inner join CatEmpresas Ce (NoLock) On ( Ce.IdEmpresa = E.IdEmpresa )
	Inner Join vw_Personal U (NoLock) On (E.IdEstado = U.IdEstado And E.IdFarmacia = U.IdFarmacia And E.IdPersonalRegistra = U.IdPersonal )
	Where E.MovtoAplicado = 'S' 	
		
Go--#SQL

------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select  Name From Sysobjects (NoLock) Where Name = 'vw_Kardex_Producto' and xType = 'V' )
   Drop View vw_Kardex_Producto 
Go--#SQL	 

Create View vw_Kardex_Producto 
With Encryption 
As 
	Select V.IdEmpresa, V.Empresa, 
		   V.IdEstado, F.Estado, 
		   V.IdFarmacia, F.Farmacia, 
		   -- V.FechaRegistro, 
		   cast(convert(varchar(10), V.FechaSistema, 112) as datetime) as FechaSistema, 
		   V.FechaRegistro, 
		   V.Folio, Tm.TipoMovto, V.DescMovimiento, V.Referencia, 
		   V.IdProducto, V.CodigoEAN, V.DescProducto, 
		   V.IdClaveSSA_Sal, V.ClaveSSA_Base, V.ClaveSSA, V.ClaveSSA_Aux, V.DescripcionSal, 		   
		   sum(Entrada) as Entrada, sum(Salida) as Salida, sum(Existencia) as Existencia, 
		   max(V.Costo) as Costo, max(V.TasaIva) as TasaIva,  sum(V.Importe) as Importe, V.Status, max(V.Keyx) as Keyx 
	From vw_Kardex_ProductoCodigoEAN V (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )  
	Inner Join vw_MovtosInv_Tipos_Farmacia Tm (NoLock)
		On ( V.IdEstado = Tm.IdEstado and V.IdFarmacia = Tm.IdFarmacia and V.TipoMovto = Tm.TipoMovto ) 	
	Group by V.IdEmpresa, V.Empresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia, 
		-- V.FechaRegistro, 
		V.FechaSistema, V.FechaRegistro, V.Folio, Tm.TipoMovto, V.DescMovimiento, V.Referencia, V.IdProducto, V.CodigoEAN, 
		V.IdClaveSSA_Sal, V.ClaveSSA_Base, V.ClaveSSA, V.ClaveSSA_Aux, V.DescripcionSal, 
		V.DescProducto, V.Status 
		
Go--#SQL
 
