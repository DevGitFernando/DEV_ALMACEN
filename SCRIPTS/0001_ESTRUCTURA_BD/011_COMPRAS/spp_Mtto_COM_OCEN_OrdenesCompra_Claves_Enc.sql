-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_OrdenesCompra_Claves_Enc' and xType = 'P' )
    Drop Proc spp_Mtto_COM_OCEN_OrdenesCompra_Claves_Enc
Go--#SQL 
  
Create Proc spp_Mtto_COM_OCEN_OrdenesCompra_Claves_Enc 
(
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@FolioOrden varchar(8) = '', @FacturarA varchar(3) = '', 
	@IdProveedor varchar(4) = '', @IdPersonal varchar(4) = '', @EstadoEntrega varchar(2) = '', @EntregarEn varchar(4) = '',
	@FechaRequeridaEntrega varchar(10) = '', @Observaciones varchar(100) = '', @TipoOrden smallint = 0, 
	@EsAutomatica tinyint = 0, @FolioPedido varchar(6) = '', @EsCentral tinyint = 0, @iOpcion smallint = 0, @EsContado tinyint = 0, 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@FolioOrdenVinculada varchar(8) = '', @DiasDePLazo Int = 0
) 
With Encryption 
As
Begin 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(2), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion 
	Opcion 2.- Cancelar ----Eliminar.
	Opcion 3.- Orden Colocada
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	If @EsAutomatica = 0
		Set @FolioPedido = ''

	If @FolioOrden = '*' 
	   Select @FolioOrden = cast( (max(FolioOrden) + 1) as varchar)  From COM_OCEN_OrdenesCompra_Claves_Enc (NoLock)
	   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 

	-- Asegurar que FolioOrden sea valido y formatear la cadena 
	Set @FolioOrden = IsNull(@FolioOrden, '1')
	Set @FolioOrden = right(replicate('0', 8) + @FolioOrden, 8)

	If @iOpcion = 1 
		Begin 
			-- Se borra los detalles de las claves para que se grabe solo lo que va a surtir el proveedor
			Delete From COM_OCEN_OrdenesCompra_Claves_Det 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden

			-- Se borra los detalles de los Productos para que se grabe solo lo que va a surtir el proveedor
			Delete From COM_OCEN_OrdenesCompra_CodigosEAN_Det 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden

			If Not Exists ( Select * From COM_OCEN_OrdenesCompra_Claves_Enc  (NoLock) 
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden ) 
				Begin 
					Insert Into COM_OCEN_OrdenesCompra_Claves_Enc  
					(	IdEmpresa, IdEstado, IdFarmacia, FolioOrden, FacturarA, IdProveedor, IdPersonal, 
						FechaRegistro, EstadoEntrega, EntregarEn, FechaRequeridaEntrega, FechaColocacion, 
						TipoOrden, Observaciones, Status, Actualizado, EsAutomatica, FolioPedido, EsCentral, 
						EsContado, IdCliente, IdSubCliente, FolioOrdenVinculada, DiasDePLazo ) 
					Select 
						@IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @FacturarA, @IdProveedor, @IdPersonal, 
						Getdate(), @EstadoEntrega, @EntregarEn, @FechaRequeridaEntrega, Getdate(), 
						@TipoOrden, @Observaciones, @sStatus, @iActualizado, @EsAutomatica, @FolioPedido, @EsCentral, 
						@EsContado, @IdCliente, @IdSubCliente, @FolioOrdenVinculada, @DiasDePLazo
				End 
			Else 
				Begin 
					Update COM_OCEN_OrdenesCompra_Claves_Enc  Set FacturarA = @FacturarA,  IdProveedor = @IdProveedor, IdPersonal = @IdPersonal, 
						EstadoEntrega = @EstadoEntrega, EntregarEn = @EntregarEn, FechaRequeridaEntrega = @FechaRequeridaEntrega, 
						Observaciones = @Observaciones, FechaColocacion = GetDate(), 
						Status = @sStatus, Actualizado = @iActualizado, EsContado = @EsContado,
						FolioOrdenVinculada = @FolioOrdenVinculada, DiasDePLazo = @DiasDePLazo
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden 
				End 

				Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioOrden 
		   
	   End 


----------------------------------------------------------------------------------------------- 
	If @iOpcion = 2 
		Begin 
			Set @sStatus = 'C' 
			Update COM_OCEN_OrdenesCompra_Claves_Enc  Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden 
			
			Exec spp_Mtto_COM_OCEN_Devolucion_Pedido @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden			
			
			Set @sMensaje = 'La información de la Orden de Compra ' + @FolioOrden + ' ha sido cancelada satisfactoriamente.'			
			
		End 


----------------------------------------------------------------------------------------------- 	
	If @iOpcion = 3 	
		Begin 
			Set @sStatus = 'OC' 
			Update COM_OCEN_OrdenesCompra_Claves_Enc  Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden

			Update C Set C.Status = 'A' 
			From COM_CartasFaltantes_ClavesSSA C (Nolock) 
			Inner Join vw_OrdenesCompras_CodigosEAN_Det D (Nolock) On ( C.ClaveSSA = D.ClaveSSA )
			Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.Folio = @FolioOrden 
			Set @sMensaje = 'La información de la Orden de Compra ' + @FolioOrden + ' ha sido colocada satisfactoriamente.' 
		End 
			 

	---- Regresar la Clave Generada
    Select @FolioOrden as Clave, @sMensaje as Mensaje  -- , @IdCliente as IdCliente, @IdSubCliente  as IdSubCliente 


	----select @FolioOrden as Clave, @sMensaje as Mensaje  
	----from COM_OCEN_OrdenesCompra_Claves_Enc (NOLOCK) 
	----Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden 


End
Go--#SQL

