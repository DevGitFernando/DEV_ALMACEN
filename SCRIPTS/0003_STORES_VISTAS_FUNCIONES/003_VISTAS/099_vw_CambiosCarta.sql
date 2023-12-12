---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From sysobjects (NoLock) Where Name = 'vw_CambiosCarta_Enc' and xType='V' )
	Drop View vw_CambiosCarta_Enc 
Go--#SQL

Create View vw_CambiosCarta_Enc 
With Encryption
As
	Select cpe.IdEmpresa, cem.Nombre As NombreEmpresa, cpe.IdEstado, ces.Nombre As NombreEstado, cpe.IdFarmacia, cfa.NombreFarmacia, cpe.FolioCambio,
		   cpe.FolioCarta, cpe.Tipo, cpe.FolioTransferenciaVenta, cpe.TipoMovtoInv, cpe.FechaRegistro, cpe.IdPersonal, 
		   per.Nombre + ' ' + per.ApPaterno + ' ' + per.ApMaterno As NombrePersonal, cpe.Observaciones, cpe.SubTotal, cpe.Iva, cpe.Total  
	From CambiosCartasCanje_Enc cpe 
	Inner Join CatEmpresas cem On ( cpe.IdEmpresa = cem.IdEmpresa)
	Inner Join CatEstados ces On ( cpe.IdEstado = ces.IdEstado )
	Inner Join CatFarmacias cfa On ( cpe.IdEstado = cfa.IdEstado  And cpe.IdFarmacia = cfa.IdFarmacia )
	Inner Join CatPersonal per On ( cpe.IdEstado = per.IdEstado And cpe.IdFarmacia = per.IdFarmacia And cpe.IdPersonal = per.IdPersonal ) 
	
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CambiosCarta_Det_CodigosEAN' and xType = 'V' ) 
	Drop View vw_CambiosCarta_Det_CodigosEAN
Go--#SQL

Create View vw_CambiosCarta_Det_CodigosEAN
With Encryption 
As 

    Select cpe.IdEmpresa, cpe.NombreEmpresa, cpe.IdEstado, cpe.NombreEstado, 
           cpe.IdFarmacia, cpe.NombreFarmacia, cpe.FolioCambio,
	       P.IdClaveSSA_Sal, P.ClaveSSA, 
	       P.IdProducto, cpd.CodigoEAN, 
	       P.Descripcion As DescripcionProducto, P.Descripcion As DescripcionSal, 
	       P.IdPresentacion, P.ContenidoPaquete, P.Presentacion, cpd.Cantidad, cpd.Costo, cpd.TasaIva, cpd.Importe, cpd.Status, 0 as Keyx   
    From CambiosCartasCanje_Det_CodigosEAN cpd (NoLock) 
    Inner Join vw_CambiosCarta_Enc cpe (NoLock)  
        On ( cpe.IdEmpresa = cpd.IdEmpresa And cpe.IdEstado = cpd.IdEstado And cpe.IdFarmacia = cpd.IdFarmacia And cpe.FolioCambio = cpd.FolioCambio ) 
    Inner Join vw_Productos_CodigoEAN P (NoLock) 
        On ( cpd.IdProducto = P.IdProducto and cpd.CodigoEAN = P.CodigoEAN ) 

Go--#SQL 


---------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CambiosCarta_Det_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_CambiosCarta_Det_CodigosEAN_Lotes
Go--#SQL

Create View vw_CambiosCarta_Det_CodigosEAN_Lotes 
With Encryption 
As

    Select cpl.IdEmpresa, cem.Nombre As NombreEmpresa, cpl.IdEstado, ces.Nombre As NombreEstado, cpl.IdFarmacia, far.NombreFarmacia, cpl.FolioCambio, 
	    cpl.IdProducto, cpl.CodigoEAN, cpl.IdSubFarmacia, sfa.Descripcion As SubFarmacia, cpl.ClaveLote, cpl.EsConsignacion, 
	    cast(cpl.Cantidad as int) as Cantidad, fcl.FechaRegistro as FechaReg, fcl.FechaCaducidad as FechaCad, fcl.Status as Status, 
	    cast(fcl.Existencia as Int) as Existencia 
    From CambiosCartasCanje_Det_CodigosEAN_Lotes cpl (NoLock) 
    Inner Join CatEmpresas cem (NoLock) On ( cem.IdEmpresa = cpl.IdEmpresa) 
    Inner Join CatEstados ces (NoLock) On ( ces.IdEstado = cpl.IdEstado ) 
    Inner Join CatFarmacias far (NoLock) On ( far.IdEstado = cpl.IdEstado And far.IdFarmacia = cpl.IdFarmacia ) 
    Inner Join CatFarmacias_SubFarmacias sfa (NoLock) On (sfa.IdEstado = cpl.IdEstado And sfa.IdFarmacia = cpl.IdFarmacia 
	    And sfa.IdSubFarmacia = cpl.IdSubFarmacia) 
    Inner Join FarmaciaProductos_CodigoEAN_Lotes  fcl (NoLock) On (  fcl.IdEmpresa = cpl.IdEmpresa and fcl.IdEstado = cpl.IdEstado 
	    and fcl.IdFarmacia = cpl.IdFarmacia And fcl.IdProducto = cpl.IdProducto and fcl.CodigoEAN = cpl.CodigoEAN and fcl.ClaveLote = cpl.ClaveLote )

Go--#SQL
