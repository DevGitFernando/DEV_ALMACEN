If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_MovtosInv_Tipos' and xType = 'V' ) 
	Drop View vw_MovtosInv_Tipos  
Go--#SQL	
 	

Create View vw_MovtosInv_Tipos 
With Encryption 
As 
	Select E.IdTipoMovto_Inv as TipoMovto, E.Descripcion as DescMovimiento, E.EsMovtoGral, 
		E.Efecto_Movto, (Case When E.Efecto_Movto = 'E' Then 'Entrada' Else 'Salida' End ) as Efecto, 
		E.PermiteCaducados, E.Status, (case when E.Status = 'A' then 'ACTIVO' else 'CANCELADO' end) as StatusAux,
		E.Keyx as Consecutivo 
	From Movtos_Inv_Tipos E (NoLock) 
Go--#SQL	
 

--------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_MovtosInv_Tipos_Farmacia' and xType = 'V' ) 
	Drop View vw_MovtosInv_Tipos_Farmacia  
Go--#SQL	
 	

Create View vw_MovtosInv_Tipos_Farmacia 
With Encryption 
As 
	Select D.IdEstado, D.IdFarmacia, E.IdTipoMovto_Inv as TipoMovto, E.EsMovtoGral, E.Descripcion as DescMovimiento, 
	E.Efecto_Movto, (Case When E.Efecto_Movto = 'E' Then 'Entrada' Else 'Salida' End ) as Efecto, 
	E.PermiteCaducados, D.Consecutivo    
	From Movtos_Inv_Tipos E (NoLock) 
	Inner Join Movtos_Inv_Tipos_Farmacia D (NoLock) On ( E.IdTipoMovto_Inv = D.IdTipoMovto_Inv ) 
Go--#SQL	
 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_MovtosInv_Enc' and xType = 'V' ) 
	Drop View vw_MovtosInv_Enc 
Go--#SQL	
 	

Create View vw_MovtosInv_Enc 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		M.IdFarmacia, F.NombreFarmacia as Farmacia, 
		M.FolioMovtoInv as Folio, M.IdTipoMovto_Inv as TipoMovto, T.Descripcion As DescTipoMovto,  M.TipoES as Efecto, 
		M.FechaSistema, 
		M.FechaRegistro as FechaReg, 
		M.Referencia, M.MovtoAplicado, 
		M.IdPersonalRegistra as IdPersonal, 
		vP.NombreCompleto as NombrePersonal, 
		M.Observaciones, 
		M.SubTotal, M.Iva, M.Total, M.Status, M.Keyx   
	From MovtosInv_Enc M (NoLock) 
	Inner Join CatEmpresas Ex On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonalRegistra = vP.IdPersonal )
	Inner Join Movtos_Inv_Tipos T (Nolock) On ( M.IdTipoMovto_Inv = T.IdTipoMovto_Inv)
Go--#SQL	
 
-------------  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_MovtosInv_Det_CodigosEAN' and xType = 'V' ) 
	Drop View vw_MovtosInv_Det_CodigosEAN 
Go--#SQL	
 	

Create View vw_MovtosInv_Det_CodigosEAN 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		D.UnidadDeSalida, D.TasaIva, D.Cantidad, D.Costo, D.Importe, D.Status, D.Keyx as KeyxDetalle  
	From vw_MovtosInv_Enc M (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioMovtoInv ) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL	
 	


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_MovtosInv_Det_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_MovtosInv_Det_CodigosEAN_Lotes 
Go--#SQL
 	

Create View vw_MovtosInv_Det_CodigosEAN_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 	
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		L.Status as Status, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as StatusAux, 
		cast(L.Existencia as Int) as Existencia, cast(D.Cantidad as int) as Cantidad, D.Keyx as KeyxDetalleLote 
	From vw_MovtosInv_Det_CodigosEAN E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioMovtoInv and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia  and 
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
Go--#SQL	
 	
