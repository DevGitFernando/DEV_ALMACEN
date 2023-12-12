If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_Remisiones' and xType = 'V' ) 
	Drop View vw_Impresion_Remisiones 
Go--#SQL
 	

Create View vw_Impresion_Remisiones 
With Encryption 
As 

	Select 
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, E.FolioRemision, E.FolioVenta, 
		   E.IdPersonal, E.NombrePersonal, E.IdCliente, E.NombreCliente, E.IdSubCliente, E.NombreSubCliente, 
		   E.IdPrograma, E.Programa, E.IdSubPrograma, E.SubPrograma, E.IdBeneficiario, E.Beneficiario, 
		   E.FechaSistema, E.FechaRegistro, E.TipoDeVenta, E.SubTotal, E.Descuento, E.Iva, E.Total, E.Status,
		   D.Renglon, D.IdProducto, D.DescripcionProducto, D.CodigoEAN, D.IdClaveSSA_Sal, D.ClaveSSA, D.DescripcionSal, 
		   D.ClaveLote, D.PrecioUnitario, D.CantidadVendidaLote as CantidadLote, 
		   D.TasaIva, (D.PrecioUnitario * D.CantidadVendidaLote) as SubTotalLote, 
		   (D.PrecioUnitario * D.CantidadVendidaLote) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.PrecioUnitario * D.CantidadVendidaLote) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad 
	From vw_RemisionesEnc E (NoLock) 
	Inner Join vw_RemisionesDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioRemision = D.FolioRemision ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( D.IdEmpresa = F.IdEmpresa and D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia and D.IdSubFarmacia = F.IdSubFarmacia
			 and D.IdProducto = F.IdProducto and D.CodigoEAN = F.CodigoEAN and D.ClaveLote = F.ClaveLote )  		
Go--#SQL	

