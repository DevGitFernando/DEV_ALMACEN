------------------------------------------------------------------------------------------------------------------------	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Claves_Asignadas_A_Clientes' and xType = 'V' ) 
   Drop View vw_Claves_Asignadas_A_Clientes 
Go--#SQL  

Create View vw_Claves_Asignadas_A_Clientes 
With Encryption 
As 

	Select Cc.IdCliente, C.NombreCliente, 
		 Cc.IdClaveSSA_Sal as IdClaveSSA, 
		 S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, 
		 S.DescripcionClave as DescripcionClave, 
		 S.TipoDeClave, S.TipoDeClaveDescripcion As TipoClaveDescripcion, 
	     S.ContenidoPaquete, 
		 Cc.Status, (case when Cc.Status = 'A' Then 'Activa' Else 'Cancelada' End) as StatusRelacion 
	From CFG_Clientes_Claves Cc (NoLock)  
	Inner Join vw_Clientes C (NoLock) On ( Cc.IdCliente = C.IdCliente) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( Cc.IdClaveSSA_Sal = S.IdClaveSSA_Sal )  
--	Where Cc.IdClaveSSA_SAl = '0028' 


Go--#SQL	


------------------------------------------------------------------------------------------------------------------------	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Claves_Asignadas_A_SubClientes' and xType = 'V' ) 
   Drop View vw_Claves_Asignadas_A_SubClientes 
Go--#SQL	 	

Create View vw_Claves_Asignadas_A_SubClientes 
With Encryption 
As 
	Select Cc.IdCliente, C.NombreCliente, Cc.IdSubCliente, C.NombreSubCliente, 
	     Cc.IdClaveSSA_Sal as IdClaveSSA, 
	     S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionClave as DescripcionClave, 
	     S.TipoDeClave, S.TipoDeClaveDescripcion As TipoClaveDescripcion, 
	     S.ContenidoPaquete, 
		 Cc.Status, (case when Cc.Status = 'A' Then 'Activa' Else 'Cancelada' End) as StatusRelacion 
	From CFG_Clientes_SubClientes_Claves Cc (NoLock)  
	Inner Join vw_Clientes_SubClientes C (NoLock) On ( Cc.IdCliente = C.IdCliente and Cc.IdSubCliente = C.IdSubCliente) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( Cc.IdClaveSSA_Sal = S.IdClaveSSA_Sal )  
--	Where Cc.IdClaveSSA_SAl = '0028' 	


Go--#SQL	
 


------------------------------------------------------------------------------------------------------------------------	
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Claves_Precios_Asignados' and xType = 'V' ) 
   Drop View vw_Claves_Precios_Asignados 
Go--#SQL	
 	
Create View vw_Claves_Precios_Asignados  
With Encryption 
As 

	Select Cc.IdEstado, E.Nombre as  Estado, Cc.IdCliente, C.NombreCliente, Cc.IdSubCliente, C.NombreSubCliente, 
	     Cc.IdClaveSSA_Sal as IdClaveSSA, 
	     S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionClave as DescripcionClave, 
	     S.TipoDeClave, S.TipoDeClaveDescripcion As TipoClaveDescripcion, S.IdPresentacion, S.Presentacion, 
	     S.ContenidoPaquete as ContenidoPaquete_ClaveSSA, 
		 S.ContenidoPaquete,  
	     (case when CC.Factor = 0 Then 1 else CC.Factor end) Factor, 

		 --CC.ContenidoPaquete_Licitado as ContenidoPaquete, 
		 CC.ContenidoPaquete_Licitado, 
		 CC.IdPresentacion_Licitado, 
		 ( select top 1 Descripcion From CatPresentaciones (NoLock) Where IdPresentacion = CC.IdPresentacion_Licitado ) as Presentacion_Licitado,

		 /* 
			 (case when CC.ContenidoPaquete_Licitado = 0 Then S.ContenidoPaquete else CC.ContenidoPaquete_Licitado end) as ContenidoPaquete_Licitado, 
			 (case when CC.IdPresentacion_Licitado = '' Then S.IdPresentacion else CC.IdPresentacion_Licitado end) as IdPresentacion_Licitado, 

			 (case when CC.IdPresentacion_Licitado = '' Then S.Presentacion 
				else ( select top 1 Descripcion From CatPresentaciones (NoLock) Where IdPresentacion = CC.IdPresentacion_Licitado )
			 end) as Presentacion_Licitado, 
		*/

		 Cc.Dispensacion_CajasCompletas, 
	     cast(round(Cc.Precio, 4) as numeric(14, 4)) as Precio, 
	     cast(round(Cc.Precio, 4) as numeric(14, 4)) as Precio_Licitacion, 
	     cast(round(Cc.Precio, 4) as numeric(14, 4)) as PrecioCaja, 

		 --cast(round((Cc.Precio / (S.ContenidoPaquete * 1.00)), 4) as numeric(14, 4)) as PrecioUnitario, ---- Original 
		 cast(round((Cc.Precio / (CC.ContenidoPaquete_Licitado * 1.00)), 4) as numeric(14, 4)) as PrecioUnitario, 
		 cast(round((Cc.Precio / (CC.ContenidoPaquete_Licitado * 1.00)), 4) as numeric(14, 4)) as PrecioUnitario_Licitacion, 

		 cast(round((Cc.Precio / (S.ContenidoPaquete * 1.00)), 4) as numeric(14, 4)) as PrecioUnitario_01, ---- Original 
	     cast(round((Cc.Precio / (CC.ContenidoPaquete_Licitado * 1.00)), 2, 1) as numeric(14, 2)) as PrecioUnitario_02, 

	     --cast(0 as numeric(14,4)) as PrecioCaja, 
		 Cc.Status, (case when Cc.Status = 'A' Then 'Activa' Else 'Cancelada' End) as StatusRelacion 

		 , SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida 

	From CFG_ClavesSSA_Precios Cc (NoLock)  
	Inner Join CatEstados E (NoLock) On ( Cc.IdEstado = E.IdEstado ) 
	Inner Join vw_Clientes_SubClientes C (NoLock) On ( Cc.IdCliente = C.IdCliente and Cc.IdSubCliente = C.IdSubCliente ) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( Cc.IdClaveSSA_Sal = S.IdClaveSSA_Sal )  
--	Where Cc.IdClaveSSA_SAl = '0028' 	

Go--#SQL



------------------------------------------------------------------------------------------------------------------------	
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Claves_Precios_Asignados_DosisUnitaria' and xType = 'V' ) 
   Drop View vw_Claves_Precios_Asignados_DosisUnitaria 
Go--#SQL	
 	
Create View vw_Claves_Precios_Asignados_DosisUnitaria  
With Encryption 
As 

	Select 
		P.IdEstado, P.Estado, P.IdCliente, P.NombreCliente, P.IdSubCliente, P.NombreSubCliente, P.IdClaveSSA, P.ClaveSSA_Base, P.ClaveSSA, P.ClaveSSA_Aux, P.DescripcionClave, 
		P.TipoDeClave, P.TipoClaveDescripcion, P.IdPresentacion, P.Presentacion, 
		--P.ContenidoPaquete_ClaveSSA, P.ContenidoPaquete, 
		--P.Factor, P.ContenidoPaquete_Licitado, P.IdPresentacion_Licitado, P.Presentacion_Licitado, P.Dispensacion_CajasCompletas, 
		P.Precio, -- Precio_Licitacion, PrecioCaja, PrecioUnitario, PrecioUnitario_Licitacion, PrecioUnitario_01, PrecioUnitario_02, 
		DU.Factor as Factor_DosisUnitaria, 
		--DU.Precio as Precio_Ref___DosisUnitaria, 
		cast(round((P.Precio / DU.Factor), 2, 1) as numeric(14, 4))  as Precio_DosisUnitaria, 
		P.Status as StatusRelacion, 
		DU.Status as StatusRelacion_DosisUnitaria, 
		P.SAT_ClaveDeProducto_Servicio, P.SAT_UnidadDeMedida
	From vw_Claves_Precios_Asignados P (NoLock) 
	Inner Join CFG_ClavesSSA_Unidosis_Precios DU (NoLock) On ( P.IdEstado = DU.IdEstado and P.IdCliente = DU.IdCliente and P.IdSubCliente = DU.IdSubCliente and P.IdClaveSSA = DU.IdClaveSSA_Sal ) 

Go--#SQL	
	
--	sp_listacolumnas vw_Claves_Precios_Asignados 

