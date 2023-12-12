-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_InventarioInicial' and xType = 'V' ) 
	Drop View vw_Impresion_InventarioInicial 
Go--#SQL 	
  
Create View vw_Impresion_InventarioInicial 
With Encryption 
As 
	Select -- L.*, vP.*  
		   Distinct 
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia,
		   E.Folio, E.TipoMovto, E.Efecto, upper(M.DescMovimiento) as TipoDeMovto, 
		   E.FechaSistema, E.FechaReg, E.Referencia, E.MovtoAplicado, E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.SubTotal, E.Iva, E.Total, E.Status, E.Keyx as KeyEncabezado, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA_Base, vP.ClaveSSA, vP.ClaveSSA_Aux, vP.DescripcionSal, 		   
		   D.CodigoEAN, 
		   D.Cantidad, 		  
		   D.Costo, D.Importe, 
		   L.ClaveLote, L.Cantidad as CantidadLote, 
		   round((L.Cantidad / (vP.ContenidoPaquete * 1.0)), 2)  as CantidadLoteCajas, 
		   (D.Costo * L.Cantidad) as ImporteLote, 
		   F.FechaCaducidad, F.FechaRegistro  
	From vw_MovtosInv_Enc E (NoLock) 
	Inner Join vw_MovtosInv_Tipos_Farmacia M (NoLock) On ( E.IdEstado = M.IdEstado and E.IdFarmacia = E.IdFarmacia and E.TipoMovto = M.TipoMovto ) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioMovtoInv ) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos_CodigoEAN vP On ( D.IdProducto = vP.IdProducto and D.CodigoEAN = D.CodigoEAN ) 

Go--#SQL
 
