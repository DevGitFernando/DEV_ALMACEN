
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_AjustesInv_Det' and xType = 'P' ) 
   Drop Proc spp_Mtto_AjustesInv_Det
Go--#SQL

Create Proc spp_Mtto_AjustesInv_Det ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @Poliza varchar(8), 
	@IdProducto varchar(8), @CodigoEAN varchar(30), @UnidadDeSalida smallint, @TasaIva numeric(14,4), @ExistenciaFisica int, @Costo numeric(14,4), 
	@Importe numeric(14,4), @ExistenciaSistema int, @Status varchar(1) 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @Actualizado smallint 
	Set @Actualizado = 0 
	Set @Actualizado = 3  --- Solo se marca para replicacion cuando se termina el Proceso  

	If Not Exists ( Select * From AjustesInv_Det (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and
					Poliza = @Poliza and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN ) 
		Begin 

		  -- Se actualiza el Status del Producto en la tabla Farmacia Productos.
		  Update FarmaciaProductos Set Status = 'I', Actualizado = @Actualizado 
		  Where IdEmpresa= @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdProducto = @IdProducto

		  -- Se Inserta en la tabla de Ajustes Detalles.			
	      Insert Into AjustesInv_Det ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, UnidadDeSalida, TasaIva, Costo, Importe, Status, Actualizado ) 
	      Select @IdEmpresa, @IdEstado, @IdFarmacia, @Poliza, @IdProducto, @CodigoEAN, @UnidadDeSalida, @TasaIva, @Costo, @Importe, @Status, @Actualizado 
	   End 
	Else 
	   Begin 
	       Update AjustesInv_Det Set 
				ExistenciaSistema = @ExistenciaSistema, UnidadDeSalida = @UnidadDeSalida, TasaIva = @TasaIva, ExistenciaFisica = @ExistenciaFisica,  
				Costo = @Costo, Importe = @Importe, 
				Status = @Status, Actualizado = @Actualizado 
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	             and Poliza = @Poliza and IdProducto = @IdProducto  and CodigoEAN = @CodigoEAN
	   End    

End 
Go--#SQL 
