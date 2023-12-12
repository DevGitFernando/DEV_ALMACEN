If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CierreDeInventario_001' and xType = 'P' ) 
   Drop Proc spp_Mtto_CierreDeInventario_001 
Go--#SQL 

Create Proc spp_Mtto_CierreDeInventario_001  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182', @ManejaUbicaciones tinyint = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare @Actualizado smallint 
	
	-- spp_Mtto_CierreDeInventario_Igualar_Existencias_001	
	
	--print 'Inicio : ' + convert(varchar(50), getdate(), 120) 

	If @ManejaUbicaciones = 1
	  Begin 

		------------------ Si algun producto no tiene posicion, se envia a la posicion cero.  
		Select 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
			EsConsignacion, 0 as IdPasillo, 0 as IdEstante, 0 as IdEntrepaño, 0 as Existencia, 'A' as Status, 0 as Actualizado, 0 as Existe 	
		Into #tmp__05__Ubicaciones__Nuevas  
		From FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia


		Update F set Existe = 1 
		From #tmp__05__Ubicaciones__Nuevas F (NoLock) 
		Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones P (NoLock) 
			On ( F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia And F.IdSubFarmacia = P.IdSubFarmacia
								And F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN And F.ClaveLote = P.ClaveLote ) 
	
		Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
				EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado )
		Select 				
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
			EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado
		From #tmp__05__Ubicaciones__Nuevas 
		Where Existe = 0 
		------------------ Si algun producto no tiene posicion, se envia a la posicion cero.  


		------ Si algun producto no tiene posicion, se envia a la posicion cero.  
		----Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
		----			EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado )
		----Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 0, 0, 0, Existencia, Status, 0 
		----From FarmaciaProductos_CodigoEAN_Lotes (NoLock)
		----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		----		And Cast( IdProducto + CodigoEAN + ClaveLote + IdSubFarmacia as varchar ) Not In 
		----		(	Select Cast( IdProducto + CodigoEAN + ClaveLote + IdSubFarmacia as varchar ) 
		----			From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia )
		

		--print 'Paso 01 : ' + convert(varchar(50), getdate(), 120) 


		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, sum(Existencia) as Existencia, sum(ExistenciaEnTransito) as ExistenciaEnTransito   
		Into #tmp__04__Ubicaciones 
		From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote 

		--print 'Paso 02 : ' + convert(varchar(50), getdate(), 120) 

		-------- Se actualiza la existencia de los Lotes.
		------Update F Set Existencia = IsNull(
		------	(Select Sum(Existencia) From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones P(NoLock) 
		------	 Where	F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia And F.IdSubFarmacia = P.IdSubFarmacia
		------			And F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN And F.ClaveLote = P.ClaveLote ), 0 ) 
		------From FarmaciaProductos_CodigoEAN_Lotes F(NoLock)
		------Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 

		Update F Set Existencia = 0, ExistenciaEnTransito = 0 
		From FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 

		Update F Set Existencia = P.Existencia, ExistenciaEnTransito = P.ExistenciaEnTransito  
		From FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
		Inner Join #tmp__04__Ubicaciones P
			On ( F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia And F.IdSubFarmacia = P.IdSubFarmacia
						And F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN And F.ClaveLote = P.ClaveLote ) 

		--print 'Paso 03 : ' + convert(varchar(50), getdate(), 120) 

	  End


	-------------------- Se actualiza la existencia de los CodigosEAN.  
	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, sum(Existencia) as Existencia, sum(ExistenciaEnTransito) as ExistenciaEnTransito   
	Into #tmp__03__Lotes 
	From FarmaciaProductos_CodigoEAN_Lotes 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN  


	----Update F Set Existencia = IsNull(
	----	(Select Sum(Existencia) From FarmaciaProductos_CodigoEAN_Lotes P(NoLock) 
	----	 Where	F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia 
	----			And F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ), 0 )
	----From FarmaciaProductos_CodigoEAN F(NoLock)
	----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	----	and 1 = 0 

	Update F Set Existencia = 0, ExistenciaEnTransito = 0 
	From FarmaciaProductos_CodigoEAN F (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		
	Update F Set Existencia = P.Existencia, ExistenciaEnTransito = P.ExistenciaEnTransito  
	From FarmaciaProductos_CodigoEAN F (NoLock)
	Inner Join #tmp__03__Lotes P
		On ( F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia And F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ) 

	-------------------- Se actualiza la existencia de los CodigosEAN.  




	-------------------- Se actualiza la existencia de los Productos. 
	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, sum(Existencia) as Existencia, sum(ExistenciaEnTransito) as ExistenciaEnTransito   
	Into #tmp__03__EAN 
	From FarmaciaProductos_CodigoEAN 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto 

	----Update F Set Existencia = IsNull(
	----	(Select Sum(Existencia) From FarmaciaProductos_CodigoEAN P(NoLock) 
	----	 Where	F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia 
	----			And F.IdProducto = P.IdProducto ), 0 )
	----From FarmaciaProductos F(NoLock)
	----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	----	and 1 = 0  


	Update F Set Existencia = 0, ExistenciaEnTransito = 0 
	From FarmaciaProductos F (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 

	Update F Set Existencia = P.Existencia, ExistenciaEnTransito = P.ExistenciaEnTransito  
	From FarmaciaProductos F (NoLock)
	Inner Join #tmp__03__EAN P
		On ( F.IdEmpresa = P.IdEmpresa And F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia And F.IdProducto = P.IdProducto ) 
	-------------------- Se actualiza la existencia de los Productos.


End 
Go--#SQL 

