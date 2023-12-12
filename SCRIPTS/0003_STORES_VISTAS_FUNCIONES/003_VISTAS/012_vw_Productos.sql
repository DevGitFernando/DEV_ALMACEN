------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ClavesSSA_Sales' and xType = 'V' ) 
   Drop View vw_ClavesSSA_Sales 
Go--#SQL  

Create View vw_ClavesSSA_Sales 
With Encryption 
As 

	Select 
		S.IdClaveSSA_Sal, 
		S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA As ClaveSSA_Aux, 
		S.Descripcion as DescripcionSal, S.Descripcion as DescripcionClave,
		(case when S.DescripcionCortaClave = '' then left(S.Descripcion, 50) else S.DescripcionCortaClave end) as DescripcionCortaClave,

		S.IdTipoProducto as TipoDeClave, 
		S.IdTipoProducto, 
		(case when S.IdTipoProducto = '00' Then 'OTROS' 
			else 
				case when S.IdTipoProducto = '01' Then 'MATERIAL DE CURACION' else 'MEDICAMENTO' end
		end) as TipoDeClaveDescripcion, 

        -- (case when ( S.ClaveSSA_Base like '%MC%' or S.ClaveSSA_Base like '%.%' ) Then 1 Else 2 End ) as TipoDeClave,      
        -- (case when ( S.ClaveSSA_Base like '%MC%' or S.ClaveSSA_Base like '%.%' ) Then 'MATERIAL DE CURACION -- OTROS' Else 'MEDICAMENTO' End ) as TipoDeClaveDescripcion,                      
		
		S.IdPresentacion, 
		Pp.Descripcion as Presentacion_Base, 
		S.ContenidoPaquete as ContenidoPaquete_Base, 

		Pp.Descripcion as Presentacion, 
		S.ContenidoPaquete as ContenidoPaquete, 

		----(Select dbo.fg_Maneja_Operacion_Maquila(S.ClaveSSA, 1, 1)) as Presentacion, 	
		----Cast((Select dbo.fg_Maneja_Operacion_Maquila(S.ClaveSSA, 1, 2)) as Int) As ContenidoPaquete, 
		
		S.EsControlado, S.EsAntibiotico, S.EsRefrigerado, 
		S.IdGrupoTerapeutico, Gt.Descripcion as GrupoTerapeutico, 
		S.TipoCatalogo, Tc.Descripcion as TipoDeCatalogo, S.Status as StatusClave   
	From CatClavesSSA_Sales S (NoLock) 
	Inner Join CatGruposTerapeuticos Gt (NoLock) On ( S.IdGrupoTerapeutico = Gt.IdGrupoTerapeutico ) 
	Inner Join CatTipoCatalogoClaves Tc (NoLock) On ( S.TipoCatalogo = Tc.TipoCatalogo ) 
	Inner Join CatPresentaciones Pp (NoLock) On ( S.IdPresentacion = Pp.IdPresentacion ) 

Go--#SQL 


------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Productos' and xType = 'V' )
   Drop View vw_Productos
Go--#SQL   

Create View vw_Productos
With Encryption 
As 

	Select 
		P.IdProducto, 
		S.TipoDeClave, S.TipoDeClaveDescripcion, 
		P.IdClaveSSA_Sal, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux,  
		S.DescripcionSal, S.DescripcionClave, S.DescripcionCortaClave,
		Gt.IdGrupoTerapeutico, Gt.Descripcion as GrupoTerapeutico, 
		P.Descripcion, P.DescripcionCorta, 
		P.IdClasificacion, C.Descripcion as Clasificacion,  
		P.IdTipoProducto, Tp.Descripcion as TipoDeProducto, Tp.PorcIva as TasaIva, 
		P.EsMedicamentoControlado as EsControlado, S.EsAntibiotico, 
		P.EsSectorSalud as EsSectorSalud, 		
		P.IdFamilia, F.Descripcion as Familia, 
		P.IdSubFamilia, Sf.Descripcion as SubFamilia, 
		P.IdSegmento, CSsf.Descripcion as Segmento,  
		(case when P.IdSegmento = '01' then 1 else 0 end) as EsRefrigerado, 
		P.IdLaboratorio, L.Descripcion as Laboratorio, 
		
		S.IdPresentacion as IdPresentacion_ClaveSSA, 
		S.Presentacion_Base as Presentacion_ClaveSSA_Base, 
		S.ContenidoPaquete_Base as ContenidoPaquete_ClaveSSA_Base, 
		
		S.Presentacion as Presentacion_ClaveSSA, 
		S.ContenidoPaquete as ContenidoPaquete_ClaveSSA, 		
		
		----(Select dbo.fg_Maneja_Operacion_Maquila(S.ClaveSSA, 1, 1)) as Presentacion_ClaveSSA_OPM, 
		----Cast((Select dbo.fg_Maneja_Operacion_Maquila(S.ClaveSSA, 1, 2)) as Int) As ContenidoPaquete_ClaveSSA_OPM, 
		
		P.IdPresentacion, Pp.Descripcion as Presentacion_Base, 
		cast(P.Descomponer as smallint) as Despatilleo, 		
		(Case When P.Descomponer = 1 Then P.ContenidoPaquete Else 1 End) as ContenidoPaquete_Base, 	

		Pp.Descripcion as Presentacion, 
		(Case When P.Descomponer = 1 Then P.ContenidoPaquete Else 1 End) as ContenidoPaquete, 	

		----(Select dbo.fg_Maneja_Operacion_Maquila(P.IdProducto, 2, 1)) as Presentacion, 
		----cast((Select dbo.fg_Maneja_Operacion_Maquila(P.IdProducto, 2, 2)) as Int) As ContenidoPaquete,


		cast(P.ManejaCodigoEAN as smallint ) as ManejaCodigosEAN, P.Status,  	
		P.UtilidadProducto, P.PrecioMaxPublico, P.DescuentoGral, 
		dbo.fg_CalcularPrecioVenta(1, '0001', '0001', P.IdProducto) as PrecioVenta  	
	From CatProductos P (NoLock) 
--	Inner Join CatClavesSSA_Sales S (NoLock) On ( P.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( P.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) 	
	Inner Join CatGruposTerapeuticos Gt (NoLock) On ( S.IdGrupoTerapeutico = Gt.IdGrupoTerapeutico ) 
	Inner Join CatClasificacionesSSA C (NoLock) On ( P.IdClasificacion = C.IdClasificacion ) 
	Inner Join CatTiposDeProducto Tp (NoLock) On ( P.IdTipoProducto = Tp.IdTipoProducto ) 
	Inner Join CatFamilias F (NoLock) On ( P.IdFamilia = F.IdFamilia ) 
	Inner Join CatSubFamilias Sf (NoLock) On ( P.IdFamilia = Sf.IdFamilia and P.IdSubFamilia = Sf.IdSubFamilia ) 
	Inner Join CatSegmentosSubFamilias CSsf (NoLock) On ( P.IdFamilia = CSsf.IdFamilia and P.IdSubFamilia = CSsf.IdSubFamilia and P.IdSegmento = CSsf.IdSegmento ) 
	Inner Join CatLaboratorios L (NoLock) On ( P.IdLaboratorio = L.IdLaboratorio )
	Inner Join CatPresentaciones Pp (NoLock) On ( P.IdPresentacion = Pp.IdPresentacion ) 

Go--#SQL	
 

------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN' and xType = 'V' )
   Drop View vw_Productos_CodigoEAN
Go--#SQL 	 

Create View vw_Productos_CodigoEAN 
With Encryption 
As 

	Select 
		vwP.IdProducto, 
		IsNull(C.CodigoEAN, '') as CodigoEAN, IsNull(C.CodigoEAN_Interno, '') as CodigoEAN_Interno, 
		vwP.TipoDeClave, vwP.TipoDeClaveDescripcion, 
		vwP.IdClaveSSA_Sal, vwP.ClaveSSA_Base, vwP.ClaveSSA, vwP.ClaveSSA_Aux,
		vwP.DescripcionSal, vwP.DescripcionClave, vwP.DescripcionCortaClave, 
		vwP.IdGrupoTerapeutico, vwP.GrupoTerapeutico, 
		vwP.Descripcion, vwP.DescripcionCorta, vwP.IdClasificacion, vwP.Clasificacion, vwP.IdTipoProducto, vwP.TipoDeProducto, 
		vwP.TasaIva, vwP.EsControlado, vwP.EsAntibiotico, vwP.EsSectorSalud as EsSectorSalud, 
		vwP.IdFamilia, vwP.Familia, vwP.IdSubFamilia, vwP.SubFamilia, vwP.IdSegmento, vwP.Segmento, vwP.EsRefrigerado, 
		vwP.IdLaboratorio, vwP.Laboratorio, 
		vwP.IdPresentacion_ClaveSSA, 
		vwP.Presentacion_ClaveSSA_Base, vwP.ContenidoPaquete_ClaveSSA_Base, 		
		vwP.Presentacion_ClaveSSA, vwP.ContenidoPaquete_ClaveSSA, 
			
		vwP.Despatilleo, vwP.IdPresentacion, vwP.Presentacion_Base, vwP.ContenidoPaquete_Base, 
		vwP.Presentacion, vwP.ContenidoPaquete, 
		
		C.ContenidoPiezasUnitario, C.ContenidoCorrugado, C.Cajas_Cama, C.Cajas_Tarima, 

		vwP.ManejaCodigosEAN, vwP.Status, C.Status as StatusCodigoRel, 
		vwP.UtilidadProducto, vwP.PrecioMaxPublico, vwP.DescuentoGral, vwP.PrecioVenta 
	From vw_Productos vwP (NoLock) 
	Left Join CatProductos_CodigosRelacionados C (NoLock) On ( vwP.IdProducto = C.IdProducto ) 
	
	
Go--#SQL  
		
		