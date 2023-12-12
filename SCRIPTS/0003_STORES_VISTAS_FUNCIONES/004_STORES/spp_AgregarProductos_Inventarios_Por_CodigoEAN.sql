

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_AgregarProductos_Inventarios_Por_CodigoEAN' and xType = 'P' ) 
   Drop Proc spp_AgregarProductos_Inventarios_Por_CodigoEAN 
Go--#SQL 

Create Proc spp_AgregarProductos_Inventarios_Por_CodigoEAN
(
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0154', @Folio varchar(8) = '00000001'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
	Declare @sMensaje varchar(1000), 
	@sStatus varchar(1), @iActualizado smallint

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0  
	
	Select CodigoEAN, Sum(Existencia) as Existencia, 0 as Agregar
	Into #tmp_ProductosExistenciaUnidad
	From vw_ExistenciaPorCodigoEAN_Lotes (nolock)	
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Group By CodigoEAN
	
	Update T Set Agregar = 1
	From #tmp_ProductosExistenciaUnidad T (Nolock)
	Where Not Exists
	( 
		Select * From INV_ConteoRapido_CodigoEAN_Det D (NoLock) 
		Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.Folio = @Folio and D.CodigoEAN = T.CodigoEAN
	)
	
	Delete From #tmp_ProductosExistenciaUnidad Where Agregar = 0
	
	Insert Into INV_ConteoRapido_CodigoEAN_Det 
	( IdEmpresa, IdEstado, IdFarmacia, Folio, CodigoEAN, ExistenciaLogica, ExistenciaFinal, Conteo1, Conteo1_Bodega, EsConteo1  )
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, CodigoEAN, Existencia, 0, 0, 0, 1
	From #tmp_ProductosExistenciaUnidad (Nolock)
	
	
		
End 
Go--#SQL 

