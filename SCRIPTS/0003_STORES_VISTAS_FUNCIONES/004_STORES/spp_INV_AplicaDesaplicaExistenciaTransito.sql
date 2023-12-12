------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INV_AplicaDesaplicaExistenciaTransito' and xType = 'P' )
   Drop Proc spp_INV_AplicaDesaplicaExistenciaTransito
Go--#SQL

--	Exec spp_INV_AplicaDesaplicaExistenciaTransito '001', '21', '1182', 'TS00000033', 2

Create Proc spp_INV_AplicaDesaplicaExistenciaTransito 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
	@FolioTransferencia varchar(30) = 'TS00000033', @TipoFactor tinyint = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare @iFactor smallint	
	
	
	-- Determinar el tipo de Efecto de existencia en transito
	Set @iFactor = 1 
	If @TipoFactor = 1 
	   Set @iFactor = 1 
	Else 
	   Set @iFactor = -1 

	
---------------- Obtener 	
	-- Articulos de la Transferencia
	-- Totalizar por CodigoInterno-CodigoEAN
	Select M.IdEmpresa, M.IdEstado, M.IdFarmacia, M.FolioTransferencia as Folio,
		M.IdProducto, M.CodigoEAN, F.ExistenciaEnTransito as Existencia, (M.CantidadEnviada * @iFactor) as Cantidad
	Into #tmpArticulosEAN 
	From TransferenciasDet M (NoLock) 
	Inner Join FarmaciaProductos E (NoLock) 
		On ( M.IdEmpresa = E.IdEmpresa and M.IdEstado = E.IdEstado and M.IdFarmacia = E.IdFarmacia and M.IdProducto = E.IdProducto ) 			
	Inner Join FarmaciaProductos_CodigoEAN F (NoLock) 
		On ( M.IdEmpresa = F.IdEmpresa and M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia and M.IdProducto = F.IdProducto and M.CodigoEAN = F.CodigoEAN ) 		
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioTransferencia = @FolioTransferencia 

--	Select top 1 * from FarmaciaProductos


	-- Totalizar por CodigoInterno 	
	Select IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto, 
		sum(Existencia) as Existencia, sum(Cantidad) as Cantidad   
	Into #tmpArticulos 
	From #tmpArticulosEAN (NoLock) 
	Group by IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto 

	-- Totalizar por CodigoInterno-CodigoEAN-Lotes 		
	Select M.IdEmpresa, M.IdEstado, M.IdFarmacia, M.IdSubFarmaciaEnvia as IdSubFarmacia, M.FolioTransferencia as Folio,
		M.IdProducto, M.CodigoEAN, M.ClaveLote, F.ExistenciaEnTransito as Existencia, (M.CantidadEnviada * @iFactor) as Cantidad   
	Into #tmpArticulosEAN_Lotes 
	From TransferenciasDet_Lotes M (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( M.IdEmpresa = F.IdEmpresa and M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia and M.IdSubFarmaciaEnvia = F.IdSubFarmacia 
			 and M.IdProducto = F.IdProducto and M.CodigoEAN = F.CodigoEAN and M.ClaveLote = F.ClaveLote ) 			
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioTransferencia = @FolioTransferencia


	-- Totalizar por CodigoInterno-CodigoEAN-Lotes_Ubicaciones 		
	Select M.IdEmpresa, M.IdEstado, M.IdFarmacia, M.IdSubFarmaciaEnvia as IdSubFarmacia, M.FolioTransferencia as Folio,
		M.IdProducto, M.CodigoEAN, M.ClaveLote, F.IdPasillo, F.IdEstante, F.IdEntrepaño,
		F.ExistenciaEnTransito as Existencia, (M.CantidadEnviada * @iFactor) as Cantidad   
	Into #tmpArticulosEAN_Lotes_Ubicaciones 
	From TransferenciasDet_Lotes_Ubicaciones M (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
		On ( M.IdEmpresa = F.IdEmpresa and M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia and M.IdSubFarmaciaEnvia = F.IdSubFarmacia 
			 and M.IdProducto = F.IdProducto and M.CodigoEAN = F.CodigoEAN and M.ClaveLote = F.ClaveLote
			 and M.IdPasillo = F.IdPasillo and M.IdEstante = F.IdEstante and M.IdEntrepaño = F.IdEntrepaño ) 			
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioTransferencia = @FolioTransferencia
 		

---------------- Actualizar Existencia en Transito
	Update F Set ExistenciaEnTransito = ( F.ExistenciaEnTransito + A.Cantidad ), Actualizado = 0 
	From FarmaciaProductos F (NoLock) 
	Inner Join #tmpArticulos A (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdProducto = A.IdProducto ) 

	Update F Set ExistenciaEnTransito = ( F.ExistenciaEnTransito + A.Cantidad ), Actualizado = 0 
	From FarmaciaProductos_CodigoEAN F (NoLock) 
	Inner Join #tmpArticulosEAN A (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia 
			and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN ) 
	
	Update F Set ExistenciaEnTransito = ( F.ExistenciaEnTransito + A.Cantidad ), Actualizado = 0 
	From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
	Inner Join #tmpArticulosEAN_Lotes A (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
			 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote )
	
	Update F Set ExistenciaEnTransito = ( F.ExistenciaEnTransito + A.Cantidad ), Actualizado = 0 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
	Inner Join #tmpArticulosEAN_Lotes_Ubicaciones A (NoLock) 
		On ( F.IdEmpresa = A.IdEmpresa and F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia and F.IdSubFarmacia = A.IdSubFarmacia 
			 and F.IdProducto = A.IdProducto and F.CodigoEAN = A.CodigoEAN and F.ClaveLote = A.ClaveLote
			 and F.IdPasillo = A.IdPasillo and F.IdEstante = A.IdEstante and F.IdEntrepaño = A.IdEntrepaño ) 
---------------- Actualizar Existencia 
	
--		spp_INV_AplicaDesaplicaExistenciaTransito
		
		
---------------- Revisar por posibles descuadres 
	if @@error = 0 
	Begin 
		Exec spp_INV_AplicaDesaplicaExistenciaTransito__Validar @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTransferencia
	End 
---------------- Revisar por posibles descuadres 		
		
		
End    
Go--#SQL


