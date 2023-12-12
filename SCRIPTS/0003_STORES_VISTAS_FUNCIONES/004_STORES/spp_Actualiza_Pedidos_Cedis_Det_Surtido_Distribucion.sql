
---------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Actualiza_Pedidos_Cedis_Det_Surtido_Distribucion' and xType = 'P' ) 
   Drop Proc spp_Actualiza_Pedidos_Cedis_Det_Surtido_Distribucion 
Go--#SQL   

Create Proc spp_Actualiza_Pedidos_Cedis_Det_Surtido_Distribucion 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', @FolioSurtido varchar(8) = '00000004', 
	@IdSurtimiento int = 1, @ClaveSSA varchar(20) = '', @IdSubFarmacia varchar(2) = '01', @IdProducto varchar(8) = '00000353', 
	@CodigoEAN varchar(30) = '7501672690184', @ClaveLote varchar(30) = '2091674', @IdPasillo int = 0, @IdEstante int = 0, 
	@IdEntrepaño int = 0, @CantidadAsignada int = 0, @Observaciones varchar(100) = '', @sStatus varchar(1) = 'A',
	@IdCaja varchar(8) = '0', @Validado bit = 0  
)  
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@sMensaje varchar(1000),	 
	@iActualizado smallint
	
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso         
    
    Set @IdCaja = Right(Replicate('0', 8) + @IdCaja, 8)
    
    If Exists 
        ( Select * From Pedidos_Cedis_Det_Surtido_Distribucion (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido 
			and IdSurtimiento = @IdSurtimiento and ClaveSSA = @ClaveSSA and IdSubFarmacia = @IdSubFarmacia and IdProducto = @IdProducto 
			and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño 
		)
	Begin 	
		Update P  Set CantidadAsignada = @CantidadAsignada, Observaciones = @Observaciones, IdCaja = @IdCaja, Validado = @Validado
		From Pedidos_Cedis_Det_Surtido_Distribucion P 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido 
			and IdSurtimiento = @IdSurtimiento and ClaveSSA = @ClaveSSA and IdSubFarmacia = @IdSubFarmacia and IdProducto = @IdProducto 
			and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño
    End    
    
End 
Go--#SQL   


	