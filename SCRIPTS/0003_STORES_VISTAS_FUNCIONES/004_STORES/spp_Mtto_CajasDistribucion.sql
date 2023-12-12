
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CajasDistribucion' and xType = 'P' )
    Drop Proc spp_Mtto_CajasDistribucion
Go--#SQL
  
Create Proc spp_Mtto_CajasDistribucion 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001',
	@IdCaja varchar(10) = '*', @Status Varchar(1) = 'A'
)
With Encryption 
As
Begin
Set NoCount On

	Declare @iBadera Tinyint
	Set @iBadera = 0

	If @IdCaja = '*'
	  Begin 
		Select @IdCaja = cast( (Max(IdCaja) + 1) as varchar)  
		From CatCajasDistribucion (NoLock)
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	  End
	  
	Set @IdCaja = IsNull(@IdCaja, '1')
	Set @IdCaja = Right(Replicate('0', 8) + @IdCaja, 8)

	If Not Exists (Select * From CatCajasDistribucion (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCaja = @IdCaja)
		Begin
			Insert Into CatCajasDistribucion (IdEstado, IdFarmacia,  IdCaja)
			Select @IdEstado, @IdFarmacia,  @IdCaja
		End
		
	If Exists (Select * From CatCajasDistribucion  (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCaja = @IdCaja)
		Begin
			Select @IdEmpresa As IdEmpresa, IdEstado, IdFarmacia, IdCaja, FechaRegistro, status = (Case When status = 'A' Then 'Activo' Else 'Cancelado' End)
			From CatCajasDistribucion (NoLock)
			Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCaja = @IdCaja
		End
	Else
		Begin
			Select 'No Encontrado' As Status
		End

End
Go--#SQL 