
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatUbicaciones_CodigosEAN_Lotes' and xType = 'P')
    Drop Proc spp_Mtto_CatUbicaciones_CodigosEAN_Lotes
Go--#SQL

Create Proc spp_Mtto_CatUbicaciones_CodigosEAN_Lotes 
(
    @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(4), 
    @IdClaveSSA_Sal varchar(4), @IdPasillo int, @IdEstante int, @IdEntrepaño int, 
    @IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30), @Cantidad int, @Opcion int)
With Encryption 
As
Begin
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint  

	-- Este SP, sólo hará inserciones...

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


    If Not Exists (Select * From CatUbicaciones_CodigosEAN_Lotes (NoLock) 
        Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
            And IdClaveSSA_Sal = @IdClaveSSA_Sal And IdPasillo = @IdPasillo And IdEstante = @IdEstante And IdEntrepaño = @IdEntrepaño 
            And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote)
    Begin
        Insert Into CatUbicaciones_CodigosEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdClaveSSA_Sal, IdPasillo, IdEstante, IdEntrepaño, 
        IdProducto, CodigoEAN, ClaveLote, Cantidad, Status, Actualizado)				
        Select	@IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @IdClaveSSA_Sal, @IdPasillo, @IdEstante, @IdEntrepaño, @IdProducto, 
        @CodigoEAN, @ClaveLote, @Cantidad, @sStatus, @iActualizado
    End
			  
			  
	Select @IdProducto As IdProducto, @CodigoEAN As CodigoEAN, @ClaveLote as ClaveLote,  @sMensaje As Mensaje 
End
Go--#SQL