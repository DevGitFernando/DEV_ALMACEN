If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_ProductosLotes_Excluir_Dispensacion' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_ProductosLotes_Excluir_Dispensacion 
Go--#SQL 

Create Proc spp_Mtto_CFG_ProductosLotes_Excluir_Dispensacion  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '', 
	@IdSubFarmacia varchar(2) = '', 
	@IdProducto varchar(8) = '', @CodigoEAN varchar(20) = '', @ClaveLote varchar(30) = '', @Status varchar(2) = ''  
) 
With Encryption 
As 
Begin 
--Set NoCount On 
Set DateFormat YMD 

	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 
	Set @IdSubFarmacia = right('0000' + @IdSubFarmacia, 2) 
	Set @IdProducto = right('0000' + @IdProducto, 8) 
			

	If Not Exists ( Select * From CFG_ProductosLotes_Excluir_Dispensacion (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
			and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote ) 
	Begin 
		Insert Into CFG_ProductosLotes_Excluir_Dispensacion ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
			IdProducto, CodigoEAN, ClaveLote, Status, Actualizado ) 
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @Status, 0 as Actualizado   
	End 
	Else 
	Begin 
		Update C Set Status = @Status, Actualizado = 0  
		From CFG_ProductosLotes_Excluir_Dispensacion C (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia  
			and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote
	End 


End 
Go--#SQL 

