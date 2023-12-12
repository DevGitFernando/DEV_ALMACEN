
Set dateformat YMD 
Go--#SQL 

	If Exists ( Select * from sysobjects (nolock) Where Name = 'TempCompraEnc' and xType = 'U' ) Drop Table TempCompraEnc 
Go--#SQL 

	If Exists ( Select * from sysobjects (nolock) Where Name = 'TempCompraDet' and xType = 'U' ) Drop Table TempCompraDet 
Go--#SQL 



	Select
		E.IdEmpresa, E.IdEstado, Cast('' As Varchar(300)) As Estado, 
		
		E.IdFarmacia, Cast('' As Varchar(300)) As Farmacia, E.IdProveedor, Cast('' As Varchar(300)) As Proveedor,
		IdPersonal, Cast('' As Varchar(300)) As Personal, E.FolioCompra As Folio,
		convert(varchar(10), FechaDocto, 120) As FechaDocto,  convert(varchar(10), E.FechaRegistro, 120) As FechaRegistro,
		E.ReferenciaDocto, E.Total  
	Into TempCompraEnc
	From ComprasEnc E (Nolock)
	Where 
		--E.IdEstado = '09' And 
		Convert( varchar(10), E.FechaRegistro, 120) between '2018-01-10' and '2018-12-31' 
		and E.IdProveedor in ( '0042', '0232' )   







	Update E Set Estado = P.Nombre 
	From TempCompraEnc E (NoLock)
	Inner Join CatEstados P (NoLock) On (E.IdEstado = P.IdEstado)
	
	Update E Set Farmacia = P.NombreFarmacia
	From TempCompraEnc E (NoLock)
	Inner Join CatFarmacias P (NoLock) On (E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia)	
		
	Update E Set Proveedor = P.Nombre
	From TempCompraEnc E (NoLock)
	Inner Join CatProveedores P (NoLock) On (E.IdProveedor = P.IdProveedor)

	
	Update M Set Personal = (ApPaterno + ' ' + ApMaterno + ' ' + Nombre)
	From TempCompraEnc M (NoLock)
	Inner Join CatPersonal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	
	
Go--#SQL 


	Select
		E.IdEstado, E.IdFarmacia, 
		E.IdEmpresa, E.IdProveedor, E.Proveedor, IdPersonal, Personal, E.Folio,
		convert(varchar(10), FechaDocto, 120) As FechaDocto,  convert(varchar(10), E.FechaRegistro, 120) As FechaRegistro,
		E.ReferenciaDocto, E.Total, E.Estado, E.Farmacia, P.ClaveSSA_Base, P.ClaveSSA, P.DescripcionSal, P.TipoDeClave,
		P.Laboratorio, L.CodigoEAN, L.IdSubFarmacia, D.IdProducto, P.Presentacion, P.ContenidoPaquete, L.ClaveLote, D.CostoUnitario As Costo,
		L.CantidadRecibida As CantidadLote, P.TasaIva, GETDATE() As FechaCad, Cast(0 As int) as MesesParaCaducar
	Into TempCompraDet
	From TempCompraEnc E
	Inner Join ComprasDet D (Nolock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.FolioCompra)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN)
	Inner Join ComprasDet_Lotes L (Nolock)
		On (E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia
			And E.Folio = L.FolioCompra And D.CodigoEAN = L.CodigoEAN)



	Update E Set E.FechaCad = F.FechaCaducidad, MesesParaCaducar = datediff(mm, getdate(), IsNull(F.FechaCaducidad, cast('2000-01-01' as datetime)))
	From TempCompraDet E
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
		On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia
			 and E.IdSubFarmacia = F.IdSubFarmacia and  E.IdProducto = F.IdProducto and E.CodigoEAN = F.CodigoEAN and E.ClaveLote = F.ClaveLote )


Go--#SQL 

	select * 
	from TempCompraEnc  


	Select 
		E.Estado, E.Farmacia, E.Folio,
		E.IdProveedor As 'Clave Proveedor', E.Proveedor, E.IdPersonal As 'Clave Personal', E.Personal,
		E.FechaDocto As 'Fecha Documento',  E.FechaRegistro As 'Fecha Registro',
		E.ReferenciaDocto As Referencia, E.ClaveSSA_Base As 'Clave SSA Base', E.ClaveSSA As 'Clave SSA',
		E.DescripcionSal As Descripción, E.TipoDeClave As 'Tipo De Clave',
		E.Laboratorio, E.CodigoEAN As 'Código EAN', E.Presentacion As Presentación,
		E.ContenidoPaquete As 'Contenido Paquete', E.ClaveLote As 'Clave Lote',
		E.FechaCad As 'Fecha Caducidad', E.MesesParaCaducar As 'Meses Para Caducar',
		E.CantidadLote As Cantidad, E.Costo, E.TasaIva As 'Tasa Iva', ( E.CantidadLote * E.Costo ) * (1 + (E.TasaIva/100.0000)) as Total 
	From TempCompraDet E
	Order By E.Folio, E.FechaRegistro

Go--#SQL 

