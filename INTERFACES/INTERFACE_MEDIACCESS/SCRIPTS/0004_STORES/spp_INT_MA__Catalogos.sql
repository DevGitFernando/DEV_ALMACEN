-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__CAT_Productos' and xType = 'P' ) 
   Drop Proc spp_INT_MA__CAT_Productos 
Go--#SQL 

Create Proc spp_INT_MA__CAT_Productos 
With Encryption 
As 
Begin 
Set NoCount On 	

	Select 
		P.IdEmpresa, P.IdEstado, P.IdFarmacia, F.Referencia_MA as Referencia_Clinica_MA,
		P.IdProducto, P.CodigoEAN, P.DescripcionProducto as NombreComercial, P.DescripcionSal as DescripcionClave, 'SI' as Preferente 
	From vw_ExistenciaPorCodigoEAN P (NoLock) 
	Inner Join INT_MA__CFG_FarmaciasClinicas F (NoLock) 
		On ( P.IdEmpresa = F.IdEmpresa and P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 
	Where P.IdProducto > 0 
	Order by P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.IdProducto 
	
End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__CAT_ProductosConPresentacion' and xType = 'P' ) 
   Drop Proc spp_INT_MA__CAT_ProductosConPresentacion   
Go--#SQL 

Create Proc spp_INT_MA__CAT_ProductosConPresentacion 
With Encryption 
As 
Begin 
Set NoCount On 	

	Select 
		P.IdEmpresa, P.IdEstado, P.IdFarmacia, F.Referencia_MA as Referencia_Clinica_MA,	
		P.IdProducto as IdProductoPCP, P.CodigoEAN, P.DescripcionProducto as NombreComercial, P.DescripcionSal as DescripcionClave, P.IdProducto, 
		'SI' as Preferente  
	From vw_ExistenciaPorCodigoEAN P (NoLock) 
	Inner Join INT_MA__CFG_FarmaciasClinicas F (NoLock) 
		On ( P.IdEmpresa = F.IdEmpresa and P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 	
	Where P.IdProducto > 0 
	Order by P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.IdProducto 	
	
End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__CAT_Productos_ListaDePrecios' and xType = 'P' ) 
   Drop Proc spp_INT_MA__CAT_Productos_ListaDePrecios
Go--#SQL 

Create Proc spp_INT_MA__CAT_Productos_ListaDePrecios 
With Encryption 
As 
Begin 
Set NoCount On 	
Set Dateformat YMD 
Declare 
	@Referencia_MA varchar(100), 
	@FechaVigencia datetime  


	--Set @FechaVigencia = dateadd(dd, 1, getdate()) 
	Set @FechaVigencia = getdate() 	
	Select Top 1 @Referencia_MA = Referencia_MA_Facturacion  
	From INT_MA__CFG_FarmaciasClinicas 


--	select top 1 * from vw_Productos_CodigoEAN 


	Select Distinct  
		@Referencia_MA as Referencia_Clinica_MA,	
		
		P.IdProducto as IdProducto, P.CodigoEAN, P.ClaveSSA, 	
		upper(P.Descripcion) as NombreComercial, upper(P.DescripcionClave) as DescripcionClave, 
		P.GrupoTerapeutico, 
		
		P.Laboratorio, P.DescuentoGral as Descuento, 
		P.PrecioMaxPublico as PrecioMaximoPublico, 
		
		(case when P.EsControlado = 1 then 'SI' else 'NO' end) as EsControlado, 
		(case when P.EsAntibiotico = 1 then 'SI' else 'NO' end) as EsAntibiotico, 
		(case when P.EsRefrigerado = 1 then 'SI' else 'NO' end) as EsRefrigerado, 
		P.CodigoEAN as KeyWord, 
		(case when P.TasaIva <> 0 then 'SI' else 'NO' end) as GravaIVA, 
		convert(varchar(10), @FechaVigencia, 112) as FechaVigencia, 
		year(@FechaVigencia) as AñoVigencia, month(@FechaVigencia) as MesVigencia, day(@FechaVigencia) as DiaVigencia 
	From vw_Productos_CodigoEAN P (NoLock) 
	Where Exists 
	(	
		Select IdProducto 
		From vw_ExistenciaPorCodigoEAN PX (NoLock) 
		Where PX.IdEmpresa <> '' and PX.IdProducto > 0 and P.IdProducto = PX.IdProducto 
	) 
	Order by P.ClaveSSA, P.Laboratorio, P.IdProducto 	
	
	
--	Select * From #tmpProductos 
	
	
	
End 
Go--#SQL 