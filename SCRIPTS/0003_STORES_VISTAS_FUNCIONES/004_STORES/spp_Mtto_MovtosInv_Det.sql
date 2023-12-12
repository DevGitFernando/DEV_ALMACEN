If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_MovtosInv_Det' and xType = 'P' ) 
   Drop Proc spp_Mtto_MovtosInv_Det
Go--#SQL

Create Proc spp_Mtto_MovtosInv_Det ( @IdEmpresa varchar(3), @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001',
	@FolioMovtoInv varchar(14), @IdProducto varchar(8), @CodigoEAN varchar(30), @UnidadDeSalida smallint, 
	@TasaIva numeric(14,4), @Cantidad int, 
	@Costo numeric(14,4), @Importe numeric(14,4), @Status varchar(1) 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @Actualizado smallint 
	Set @Actualizado = 0 

	If Not Exists ( Select * From MovtosInv_Det_CodigosEAN (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and
					FolioMovtoInv = @FolioMovtoInv and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN ) 
	   Begin 
	       Insert Into MovtosInv_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Status, Actualizado ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv, @IdProducto, @CodigoEAN, @UnidadDeSalida, @TasaIva, @Cantidad, @Costo, @Importe, @Status, @Actualizado 
	   End 
	Else 
	   Begin 
	       Update MovtosInv_Det_CodigosEAN Set UnidadDeSalida = @UnidadDeSalida, TasaIva = @TasaIva, Cantidad = @Cantidad, Costo = @Costo, Importe = @Importe, Status = @Status, Actualizado = @Actualizado 
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovtoInv and IdProducto = @IdProducto  and CodigoEAN = @CodigoEAN
	   End    

End 
Go--#SQL

