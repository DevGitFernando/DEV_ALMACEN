
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Remisiones' and xType = 'P' )
    Drop Proc spp_Mtto_Remisiones
Go--#SQL
  
Create Proc spp_Mtto_Remisiones 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVenta varchar(32), @FolioRemision varchar(32), @FechaSistema varchar(10)  
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar / Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0

	If @FolioRemision = '*' 
	  Begin
		Select @FolioRemision = cast( (max(FolioRemision) + 1) as varchar)  
		From RemisionesEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	  End
	Else
	  Begin
		Select @FolioRemision = FolioRemision 
		From RemisionesEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
	  End

	-- Asegurar que FolioRemision sea valido y formatear la cadena 
	Set @FolioRemision = IsNull(@FolioRemision, '1')
	Set @FolioRemision = right(replicate('0', 8) + @FolioRemision, 8) 


   --------------------------------------
   -- Se inserta/actualiza la remision --
   --------------------------------------
   If Not Exists ( Select * From RemisionesEnc (NoLock) 
				   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioRemision = @FolioRemision ) 
	  Begin 

		 ---------------------------------
		 -- Se inserta en RemisionesEnc --
		 ---------------------------------
		 Insert Into RemisionesEnc 
			 ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FolioVenta, FolioMovtoInv, FechaSistema, 
			   IdCaja, IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,   
			   SubTotal, Descuento, Iva, Total, TipoDeVenta, Status, Actualizado
			 ) 
		 Select IdEmpresa, IdEstado, IdFarmacia, @FolioRemision, FolioVenta, FolioMovtoInv, @FechaSistema, 
				IdCaja, IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
				SubTotal, Descuento, Iva, Total, TipoDeVenta, @sStatus, @iActualizado 
		 From VentasEnc(NoLock)
		 Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 

		---------------------------------
		-- Se inserta en RemisionesDet --
		---------------------------------
		Insert Into RemisionesDet 
		(  
			IdEmpresa, IdEstado, IdFarmacia, FolioRemision, IdProducto, CodigoEAN, Renglon, UnidadDeSalida, 
			CantidadVendida, CostoUnitario, PrecioUnitario, TasaIva, ImpteIva, PorcDescto, ImpteDescto, Status, Actualizado
		 ) 
		 Select IdEmpresa, IdEstado, IdFarmacia, @FolioRemision, IdProducto, CodigoEAN, Renglon, UnidadDeSalida, 
				CantidadVendida, CostoUnitario, PrecioUnitario, TasaIva, ImpteIva, PorcDescto, ImpteDescto, @sStatus, @iActualizado 
		 From VentasDet(NoLock)
		 Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 

		---------------------------------------
		-- Se inserta en RemisionesDet_Lotes --
		---------------------------------------
		Insert Into RemisionesDet_Lotes 
			( 
				IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioRemision, IdProducto, CodigoEAN, 
				ClaveLote, Renglon, CantidadVendida, EsConsignacion, Status, Actualizado 
			) 
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioRemision, IdProducto, CodigoEAN, 
			   ClaveLote, Renglon, CantidadVendida, EsConsignacion, @sStatus, @iActualizado 
		From VentasDet_Lotes(NoLock)
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 

	  End 
   Else 
	  Begin 

		--------------------------------
		-- Se actualiza RemisionesEnc --
		--------------------------------
		Update RemisionesEnc Set
			SubTotal = V.SubTotal, Descuento = V.Descuento, Iva = V.Iva, Total = V.Total, Actualizado = @iActualizado
		From VentasEnc V(NoLock)
		Where V.IdEmpresa = RemisionesEnc.IdEmpresa and V.IdEstado = RemisionesEnc.IdEstado And V.IdFarmacia = RemisionesEnc.IdFarmacia 
			  And V.FolioVenta = RemisionesEnc.FolioVenta And RemisionesEnc.FolioRemision = @FolioRemision

		--------------------------------
		-- Se actualiza RemisionesDet --
		--------------------------------
		Update RemisionesDet Set
			UnidadDeSalida = D.UnidadDeSalida, CantidadVendida = D.CantidadVendida, CostoUnitario = D.CostoUnitario, 
			PrecioUnitario = D.PrecioUnitario, ImpteIva = D.ImpteIva,  TasaIva = D.TasaIva,
			PorcDescto = D.PorcDescto, ImpteDescto = D.ImpteDescto, Actualizado = @iActualizado
		From VentasDet D(NoLock)
		Where RemisionesDet.IdEmpresa = D.IdEmpresa and RemisionesDet.IdEstado = D.IdEstado And RemisionesDet.IdFarmacia = D.IdFarmacia 
			  And D.FolioVenta = @FolioVenta And RemisionesDet.FolioRemision = @FolioRemision
			  And RemisionesDet.IdProducto = D.IdProducto And RemisionesDet.CodigoEAN = D.CodigoEAN 
			  And RemisionesDet.Renglon = D.Renglon 

		--------------------------------------
		-- Se actualiza RemisionesDet_Lotes --
		--------------------------------------
		Update RemisionesDet_Lotes Set 						
				CantidadVendida = L.CantidadVendida, Actualizado = @iActualizado
		From VentasDet_Lotes L(NoLock)
		Where RemisionesDet_Lotes.IdEmpresa = L.IdEmpresa and RemisionesDet_Lotes.IdEstado = L.IdEstado 
			And RemisionesDet_Lotes.IdFarmacia = L.IdFarmacia And RemisionesDet_Lotes.IdSubFarmacia = L.IdSubFarmacia
			And L.FolioVenta = L.FolioVenta 
			And RemisionesDet_Lotes.FolioRemision = @FolioRemision
			And RemisionesDet_Lotes.IdProducto = L.IdProducto And RemisionesDet_Lotes.CodigoEAN = L.CodigoEAN 
			And RemisionesDet_Lotes.ClaveLote = L.ClaveLote And RemisionesDet_Lotes.Renglon = L.Renglon 

	  End 

	-- Regresar la Clave Generada
    Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioRemision  	
    Select @FolioRemision as Clave, @sMensaje as Mensaje 
End
Go--#SQL	


