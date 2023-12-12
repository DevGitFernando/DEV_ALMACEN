If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_RemisionesEnc' And xType = 'V' )
	Drop view vw_RemisionesEnc
Go--#SQL

Create View vw_RemisionesEnc 
With Encryption 
As
	Select R.IdEmpresa, Ex.Nombre as Empresa,  
	     R.IdEstado, F.Estado as Estado, F.ClaveRenapo, R.IdFarmacia, F.Farmacia as Farmacia, 
		 F.IdMunicipio, F.Municipio, F.IdColonia, F.Colonia, F.Domicilio,    
		 R.FolioRemision, R.FolioVenta, R.FolioMovtoInv, 
		 R.FechaSistema, R.FechaRegistro, R.IdCaja, R.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		 R.IdCliente, IsNull(C.NombreCliente, '') as NombreCliente, 
		 R.IdSubCliente, IsNull(C.NombreSubCliente, '') as NombreSubCliente, 
		 R.IdPrograma, IsNull(Pr.Programa, '') as Programa, 
		 R.IdSubPrograma, IsNull(Pr.SubPrograma, '') as SubPrograma, 	
		 Ia.IdBeneficiario, Ia.Beneficiario,	 
		 R.SubTotal, R.Descuento, R.Iva, R.Total, R.TipoDeVenta, R.Status
	From RemisionesEnc R (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( R.IdEmpresa = Ex.IdEmpresa ) 		
	Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Personal vP (NoLock) On ( R.IdEstado = vP.IdEstado and R.IdFarmacia = vP.IdFarmacia and R.IdPersonal = vP.IdPersonal )  	
	Inner Join vw_Clientes_SubClientes C (NoLock) On ( R.IdCliente = C.IdCliente and R.IdSubCliente = C.IdSubCliente )
	Inner Join vw_Programas_SubProgramas Pr (NoLock) On ( R.IdPrograma = Pr.IdPrograma and R.IdSubPrograma = Pr.IdSubPrograma )
	Inner Join vw_VentasDispensacion_InformacionAdicional Ia(NoLock) On ( R.IdEmpresa = Ia.IdEmpresa And R.IdEstado = Ia.IdEstado
		And R.IdFarmacia = Ia.IdFarmacia And R.FolioVenta = Ia.Folio ) 
Go--#SQL


If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_RemisionesDet' And xType = 'V' )
	Drop view vw_RemisionesDet
Go--#SQL	


Create View vw_RemisionesDet 
With Encryption  
As 
	Select	R.IdEmpresa, R.Empresa, R.IdEstado, R.Estado, R.IdFarmacia, R.Farmacia, R.FolioRemision, R.FolioVenta,
			D.IdProducto, D.CodigoEAN, 
			L.IdSubFarmacia, 
			L.ClaveLote, P.Descripcion as DescripcionProducto, P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, 
			D.Renglon, D.UnidadDeSalida, 
			
			D.CantidadVendida, 
			L.CantidadVendida as CantidadVendidaLote,
			
			D.CostoUnitario, D.PrecioUnitario, D.TasaIva,
			D.ImpteIva, D.PorcDescto, D.ImpteDescto
	From vw_RemisionesEnc R(NoLock)
	Inner Join RemisionesDet D (NoLock) On ( R.IdEmpresa = D.IdEmpresa And R.IdEstado = D.IdEstado And R.IdFarmacia = D.IdFarmacia And R.FolioRemision = D.FolioRemision )
	Inner Join RemisionesDet_Lotes L(NoLock) On ( D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioRemision = L.FolioRemision
		And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN )
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto And D.CodigoEAN = P.CodigoEAN)
	
Go--#SQL

