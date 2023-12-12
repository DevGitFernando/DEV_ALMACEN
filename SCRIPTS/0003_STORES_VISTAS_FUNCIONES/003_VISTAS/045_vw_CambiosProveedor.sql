---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From sysobjects (NoLock) Where Name = 'vw_CambiosProv_Enc' and xType='V' )
	Drop View vw_CambiosProv_Enc 
Go--#SQL

Create View vw_CambiosProv_Enc 
With Encryption
As
	Select cpe.IdEmpresa, cem.Nombre As NombreEmpresa, cpe.IdEstado, ces.Nombre As NombreEstado, cpe.IdFarmacia, cfa.NombreFarmacia, cpe.FolioCambio,
		   cpe.IdProveedor, cpr.Nombre As NombreProveedor, cpe.TipoMovto, cpe.FechaRegistro, cpe.IdPersonal, 
		   per.Nombre + ' ' + per.ApPaterno + ' ' + per.ApMaterno As NombrePersonal, cpe.Observaciones, cpe.SubTotal, cpe.Iva, cpe.Total  
	From CambiosProv_Enc cpe 
	Inner Join CatEmpresas cem On ( cpe.IdEmpresa = cem.IdEmpresa)
	Inner Join CatEstados ces On ( cpe.IdEstado = ces.IdEstado )
	Inner Join CatFarmacias cfa On ( cpe.IdEstado = cfa.IdEstado  And cpe.IdFarmacia = cfa.IdFarmacia )
	Inner Join CatProveedores cpr On (cpe.IdProveedor = cpr.IdProveedor  )
	Inner Join CatPersonal per On ( cpe.IdEstado = per.IdEstado And cpe.IdFarmacia = per.IdFarmacia And cpe.IdPersonal = per.IdPersonal ) 
	
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CambiosProv_Det_CodigosEAN' and xType = 'V' ) 
	Drop View vw_CambiosProv_Det_CodigosEAN
Go--#SQL

Create View vw_CambiosProv_Det_CodigosEAN
With Encryption 
As 

----    Select cpd.IdEmpresa, cem.Nombre As NombreEmpresa, cpd.IdEstado, ces.Nombre As NombreEstado, cpd.IdFarmacia, far.NombreFarmacia, cpd.FolioCambio,
----	       pro.IdClaveSSA_Sal, cpd.IdProducto, pro.Descripcion As DescripcionProducto, ccs.ClaveSSA, ccs.Descripcion As DescripcionSal, pro.IdPresentacion, 
----	       pro.ContenidoPaquete, pre.Descripcion As Presentacion, cpd.CodigoEAN, cpd.Cantidad, cpd.Costo, cpd.TasaIva, cpd.Importe, cpd.Status, cpe.Keyx   
----    From CambiosProv_Det_CodigosEAN cpd (NoLock) 
----    Inner Join CatEmpresas cem (NoLock) On ( cem.IdEmpresa = cpd.IdEmpresa)
----    Inner Join CatEstados ces (NoLock) On ( ces.IdEstado = cpd.IdEstado )
----    Inner Join CatFarmacias far (NoLock) On ( far.IdEstado = cpd.IdEstado And far.IdFarmacia = cpd.IdFarmacia )
----    Inner Join CatProductos pro (NoLock) On ( pro.IdProducto = cpd.IdProducto ) 
----    Inner Join CatClavesSSA_Sales ccs (NoLock) On ( ccs.IdClaveSSA_Sal = pro.IdClaveSSA_Sal ) 
----    Inner Join CatPresentaciones pre (NoLock) On ( pre.IdPresentacion = pro.IdPresentacion )
----    Inner Join vw_CambiosProv_Enc cpe (NoLock) On (cpe.IdEmpresa = cpd.IdEmpresa And cpe.IdEstado = cpd.IdEstado And cpe.IdFarmacia = cpd.IdFarmacia And
----	    cpe.FolioCambio = cpd.FolioCambio)

    Select cpe.IdEmpresa, cpe.NombreEmpresa, cpe.IdEstado, cpe.NombreEstado, 
           cpe.IdFarmacia, cpe.NombreFarmacia, cpe.FolioCambio,
	       P.IdClaveSSA_Sal, P.ClaveSSA, 
	       P.IdProducto, cpd.CodigoEAN, 
	       P.Descripcion As DescripcionProducto, P.Descripcion As DescripcionSal, 
	       P.IdPresentacion, P.ContenidoPaquete, P.Presentacion, cpd.Cantidad, cpd.Costo, cpd.TasaIva, cpd.Importe, cpd.Status, 0 as Keyx   
    From CambiosProv_Det_CodigosEAN cpd (NoLock) 
    Inner Join vw_CambiosProv_Enc cpe (NoLock)  
        On ( cpe.IdEmpresa = cpd.IdEmpresa And cpe.IdEstado = cpd.IdEstado And cpe.IdFarmacia = cpd.IdFarmacia And cpe.FolioCambio = cpd.FolioCambio ) 
    Inner Join vw_Productos_CodigoEAN P (NoLock) 
        On ( cpd.IdProducto = P.IdProducto and cpd.CodigoEAN = P.CodigoEAN ) 

Go--#SQL 


---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CambiosProv_Det_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_CambiosProv_Det_CodigosEAN_Lotes
Go--#SQL

Create View vw_CambiosProv_Det_CodigosEAN_Lotes 
With Encryption 
As

    Select cpl.IdEmpresa, cem.Nombre As NombreEmpresa, cpl.IdEstado, ces.Nombre As NombreEstado, cpl.IdFarmacia, far.NombreFarmacia, cpl.FolioCambio, 
	    cpl.IdProducto, cpl.CodigoEAN, cpl.IdSubFarmacia, sfa.Descripcion As SubFarmacia, cpl.ClaveLote, cpl.EsConsignacion, 
	    cast(cpl.Cantidad as int) as Cantidad, fcl.FechaRegistro as FechaReg, fcl.FechaCaducidad as FechaCad, fcl.Status as Status, 
	    cast(fcl.Existencia as Int) as Existencia 
    From CambiosProv_Det_CodigosEAN_Lotes cpl (NoLock) 
    Inner Join CatEmpresas cem (NoLock) On ( cem.IdEmpresa = cpl.IdEmpresa) 
    Inner Join CatEstados ces (NoLock) On ( ces.IdEstado = cpl.IdEstado ) 
    Inner Join CatFarmacias far (NoLock) On ( far.IdEstado = cpl.IdEstado And far.IdFarmacia = cpl.IdFarmacia ) 
    Inner Join CatFarmacias_SubFarmacias sfa (NoLock) On (sfa.IdEstado = cpl.IdEstado And sfa.IdFarmacia = cpl.IdFarmacia 
	    And sfa.IdSubFarmacia = cpl.IdSubFarmacia) 
    Inner Join FarmaciaProductos_CodigoEAN_Lotes  fcl (NoLock) On (  fcl.IdEmpresa = cpl.IdEmpresa and fcl.IdEstado = cpl.IdEstado 
	    and fcl.IdFarmacia = cpl.IdFarmacia And fcl.IdProducto = cpl.IdProducto and fcl.CodigoEAN = cpl.CodigoEAN and fcl.ClaveLote = cpl.ClaveLote )

Go--#SQL
