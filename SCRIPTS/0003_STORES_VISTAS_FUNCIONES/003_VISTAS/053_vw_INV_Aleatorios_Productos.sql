---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INV_Aleatorios_Productos_Enc' and xType = 'V' ) 
	Drop View vw_INV_Aleatorios_Productos_Enc 
Go--#SQL 
 	
Create View vw_INV_Aleatorios_Productos_Enc 
With Encryption 
As 
	Select 
		C.IdEmpresa, Ex.Nombre as Empresa, C.IdEstado, E.Nombre as Estado, 
		C.IdFarmacia, F.NombreFarmacia as Farmacia, C.Folio,	
		C.FechaRegistro, C.IdPersonal, P.NombreCompleto as Personal,
		C.TipoInventario, 
		( 
			Case When C.TipoInventario = 1 Then 'Inventario por Corte Parcial' 
				When C.TipoInventario = 2 Then 'Inventario por Cambio de Dia' 
				When C.TipoInventario = 3 Then 'Inventario por Cierre de Periodo' 
				Else 'Inventario de Forma Manual' End 
		) As DescTipoInventario, 
		C.Status 
	From INV_Aleatorios_Productos_Enc C (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( C.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia )	
	Inner Join vw_Personal P (Nolock) On ( C.IdEstado = P.IdEstado and C.IdFarmacia = P.IdFarmacia and C.IdPersonal = P.IdPersonal )
	 
Go--#SQL

--		Select * From vw_INV_AleatoriosEnc (Nolock)
    


---------------------------------------------------------------------------------------------------------


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INV_Aleatorios_Productos_Det' and xType = 'V' ) 
Drop View vw_INV_Aleatorios_Productos_Det 
Go--#SQL 
 	
Create View vw_INV_Aleatorios_Productos_Det 
With Encryption 
As

	Select 
		E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, E.Folio,
		E.FechaRegistro, E.IdPersonal, E.Personal, E.TipoInventario, E.DescTipoInventario, 
		P.ClaveSSA, 
		D.IdProducto, D.CodigoEAN, P.Descripcion, 
		-- D.ClaveSSA, ( Select Top 1 DescripcionCortaClave From vw_ClavesSSA_Sales S (Nolock) Where D.ClaveSSA = S.ClaveSSA ) As DescripcionSal,
		-- ( Select Top 1 DescripcionSal From vw_ClavesSSA_Sales S (Nolock) Where D.ClaveSSA = S.ClaveSSA ) As Descripcion,  
		D.ExistenciaLogica, D.Conteo_01, D.Conteo_02, D.Conteo_03,
		D.EsConteo_01, D.EsConteo_02, D.EsConteo_03, D.Fecha_01, D.Fecha_02, D.Fecha_03, D.Conciliado 
	From vw_INV_Aleatorios_Productos_Enc E (Nolock) 
	Inner Join INV_Aleatorios_Productos_Det D (Nolock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.Folio )	
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )  
	

Go--#SQL 

--	Select * From vw_INV_AleatoriosDet (Nolock) Order By ClaveSSA

