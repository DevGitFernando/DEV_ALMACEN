


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Cuentas_X_Pagar' and xType = 'V' ) 
Drop View vw_FACT_Cuentas_X_Pagar 
Go--#SQL 
 	
Create View vw_FACT_Cuentas_X_Pagar 
With Encryption 
As 
	Select C.IdEmpresa, Ex.Nombre as Empresa, C.IdEstado, E.Nombre as Estado, 
	C.IdFarmacia, F.NombreFarmacia as Farmacia, C.FolioCuenta as Folio,
	C.IdServicio, S.Descripcion as Servicio, C.IdAcreedor, A.Nombre as Acreedor,
	C.FechaRegistro, C.IdPersonal, P.NombreCompleto as Personal, C.ReferenciaDocumento as ReferenciaDocto,
	C.FechaDocumento, C.IdMetodoPago, M.Descripcion as MetodoPago, C.SubTotal, C.TasaIva, C.Iva, C.Total,
	C.Observaciones, C.Status
	From FACT_Cuentas_X_Pagar C (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( C.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia )
	Inner Join FACT_CatServicios S (Nolock) On ( C.IdServicio = S.IdServicio )
	Inner Join FACT_CatAcreedores A (Nolock) On ( C.IdEstado = A.IdEstado and C.IdAcreedor = A.IdAcreedor )
	Inner Join vw_Personal P (Nolock) On ( C.IdEstado = P.IdEstado and C.IdFarmacia = P.IdFarmacia and C.IdPersonal = P.IdPersonal )
	Inner Join FACT_CFD_MetodosPago M (Nolock) On ( C.IdMetodoPago = M.IdMetodoPago ) 
Go--#SQL



