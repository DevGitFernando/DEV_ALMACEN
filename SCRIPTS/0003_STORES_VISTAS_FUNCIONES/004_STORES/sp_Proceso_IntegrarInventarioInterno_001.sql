If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInterno_001' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInterno_001 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInterno_001  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', @ManejaUbicaciones tinyint = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare @Actualizado smallint 
	
	-- spp_Mtto_CierreDeInventario_Igualar_Existencias_001	
	

	If @ManejaUbicaciones = 1 
	Begin 
	--- Si algun producto no tiene posicion, se envia a la posicion cero.
		Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
					EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado )
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 0, 0, 0, Existencia, Status, 0 
		From FarmaciaProductos_CodigoEAN_Lotes (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
				And Cast( IdProducto + CodigoEAN + ClaveLote + IdSubFarmacia as varchar ) 
				Not In 
				(	Select Cast( IdProducto + CodigoEAN + ClaveLote + IdSubFarmacia as varchar ) 
					From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones(NoLock) 
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia )

		------------------------------------------ Se actualiza la existencia de los Lotes.	
			Update F Set Existencia = 0 
			From FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
			
			Update F Set Existencia = IsNull(
				(Select Sum(Existencia) From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones P(NoLock) 
				 Where	F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia And F.IdSubFarmacia = P.IdSubFarmacia
						And F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN And F.ClaveLote = P.ClaveLote ), 0 ) 
			From FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		------------------------------------------ Se actualiza la existencia de los Lotes.  
	End 
	

	-- Se actualiza la existencia de los CodigosEAN.
	Update F Set Existencia = IsNull(
		(Select Sum(Existencia) From FarmaciaProductos_CodigoEAN_Lotes P(NoLock) 
		 Where	F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia 
				And F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ), 0 )
	From FarmaciaProductos_CodigoEAN F (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	
	-- Se actualiza la existencia de los Productos.
	Update F Set Existencia = IsNull(
		(Select Sum(Existencia) From FarmaciaProductos_CodigoEAN P(NoLock) 
		 Where	F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia 
				And F.IdProducto = P.IdProducto ), 0 )
	From FarmaciaProductos F (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 



End 
Go--#SQL 

