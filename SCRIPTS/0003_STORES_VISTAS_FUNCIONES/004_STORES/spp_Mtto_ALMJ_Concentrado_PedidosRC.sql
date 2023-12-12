If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_ALMJ_Concentrado_PedidosRC' and xType = 'P' )
   Drop Proc spp_Mtto_ALMJ_Concentrado_PedidosRC 
Go--#SQL 

Create Proc spp_Mtto_ALMJ_Concentrado_PedidosRC 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', 
	@FolioConcentrado varchar(6) = '*', @IdPersonal varchar(6) = '0001'  
) 
With Encryption 
As 
Begin  
Set NoCount On 
Declare @Folio varchar(6),   
        @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 -- En esta tabla se guarda con 1.

	Select @FolioConcentrado = cast( (max(FolioConcentrado) + 1) as varchar)  
	From ALMJ_Concentrado_PedidosRC (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	Set @FolioConcentrado = IsNull(@FolioConcentrado, '1') 
	Set @FolioConcentrado = right(replicate('0', 6) + @FolioConcentrado, 6)

	If Not Exists ( Select * From ALMJ_Concentrado_PedidosRC (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia and FolioConcentrado = @Folio ) 
	   Begin 
	      Insert Into ALMJ_Concentrado_PedidosRC ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado, FechaSistema, IdPersonal, Status, Actualizado ) 
	      Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioConcentrado, getdate(), @IdPersonal, @sStatus, @iActualizado
	   End 
	   
	Select @FolioConcentrado as FolioConcentrado    
	
End 
Go--#SQL

------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'P' )
   Drop Proc spp_Mtto_ALMJ_Concentrado_PedidosRC_Pedidos 
Go--#SQL 	

Create Proc spp_Mtto_ALMJ_Concentrado_PedidosRC_Pedidos 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioConcentrado varchar(6), 
	@IdEmpresaPed varchar(3), @IdEstadoPed varchar(2), @IdFarmaciaPed varchar(4), @FolioPedidoRCPed varchar(6) 	 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	Insert Into ALMJ_Concentrado_PedidosRC_Pedidos 
		( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado, IdEmpresaPed, IdEstadoPed, IdFarmaciaPed, FolioPedidoRCPed, Status, Actualizado ) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioConcentrado, @IdEmpresaPed, @IdEstadoPed, @IdFarmaciaPed, @FolioPedidoRCPed, 'A', 0  


	--- Cambiar el Status del Pedido 
	Update ALMJ_Pedidos_RC Set StatusPedido = 1, Actualizado = 0 
	Where IdEmpresa = @IdEmpresaPed and IdEstado = @IdEstadoPed and IdFarmacia = @IdFarmaciaPed and FolioPedidoRC = @FolioPedidoRCPed 
	
	Update ALMJ_Pedidos_RC_Det Set -- StatusPedido = 1, 
		Actualizado = 0 
	Where IdEmpresa = @IdEmpresaPed and IdEstado = @IdEstadoPed and IdFarmacia = @IdFarmaciaPed and FolioPedidoRC = @FolioPedidoRCPed 	

End 
Go--#SQL	


------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_ALMJ_Concentrado_PedidosRC_Claves' and xType = 'P' )
   Drop Proc spp_Mtto_ALMJ_Concentrado_PedidosRC_Claves 
Go--#SQL	

Create Proc spp_Mtto_ALMJ_Concentrado_PedidosRC_Claves 
( 
	@IdEmpresa varchar(3)='001', @IdEstado varchar(2)='25', @IdFarmacia varchar(4)='0001', @FolioConcentrado varchar(6)='000009' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
	
-------------------------- 	Obtener los Pedidos a Procesar 	
	Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioConcentrado, -- C.FolioPedidoRCPed, 
		D.IdClaveSSA, Sum(D.Cantidad) as CantidadPedido, 
		0 as Existencia, 0 as ExistenciaCSGN, 0 as ExistenciaVTA, -- 0 as CantidadSolicita, 
		0 as Surtido, 0 as Pendiente, 0 as Resta   
		-- , D.IdEmpresa, D.IdEstado, D.IdFarmacia
	Into #tmpClaves 	
	From ALMJ_Concentrado_PedidosRC_Pedidos C 
	Inner Join ALMJ_Pedidos_RC_Det D (NoLock) 
		On ( C.IdEmpresaPed = D.IdEmpresa and C.IdEstadoPed = D.IdEstado and C.IdFarmacia = D.IdFarmacia and C.FolioPedidoRCPed = D.FolioPedidoRC ) 
	Where C.IdEmpresa = @IdEmpresa and C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmacia and C.FolioConcentrado = @FolioConcentrado 
	Group by C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioConcentrado, D.IdClaveSSA 
	

-------------------------- 	
---	Obtener la Existencia de Consignacion 	
	Select -- Distinct 1 as Tipo, 
		IdClaveSSA_Sal as IdClaveSSA, E.Existencia as ExistenciaCSGN, 0 as ExistenciaVTA -- , E.IdFarmacia 
	Into #tmpExistencia 
	From vw_ExistenciaPorSales E (NoLock) 
	Inner Join CFGC_AlmacenesJurisdiccionales C (NoLock) 
		On ( E.IdEmpresa = C.IdEmpresaCSGN and E.IdEstado = C.IdEstadoCSGN and E.IdFarmacia = C.IdFarmaciaCSGN )  

---	Obtener la Existencia de Venta 	
	Insert Into #tmpExistencia ( IdClaveSSA, ExistenciaCSGN, ExistenciaVTA ) 
	Select -- Distinct 2 as Tipo, 
		IdClaveSSA_Sal as IdClaveSSA, 0, E.Existencia  -- , E.IdFarmacia  
	From vw_ExistenciaPorSales E (NoLock) 
	Inner Join CFGC_AlmacenesJurisdiccionales C (NoLock) 
		On ( E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia )  
-------------------------- 	
	
--	spp_Mtto_ALMJ_Concentrado_PedidosRC_Claves 	

-------------------------- 	Asignar la existencia por ClaveSSA 	
--- Obtener la Existencia 	
	Update T Set Existencia = E.ExistenciaCSGN + E.ExistenciaVTA, 
			     ExistenciaCSGN = E.ExistenciaCSGN, ExistenciaVTA = E.ExistenciaVTA 
	From #tmpClaves T 	
	Inner Join 
	( 
		Select IdClaveSSA, sum(ExistenciaCSGN) as ExistenciaCSGN, sum(ExistenciaVTA) as ExistenciaVTA  
		From #tmpExistencia 
		Group by IdClaveSSA 
	) E On ( T.IdClaveSSA = E.IdClaveSSA ) 
-------------------------- 	Asignar la existencia por ClaveSSA 	

--	spp_Mtto_ALMJ_Concentrado_PedidosRC_Claves 	
	
--- Obtener la Existencia de la Farmacia-Almacen de Consignacion 	
	Update T Set -- ExistenciaCSGN = E.ExistenciaCSGN, 
		Surtido = (case when T.Existencia >= T.CantidadPedido then T.CantidadPedido else T.Existencia end), 		
		Pendiente = (case when T.Existencia >= T.CantidadPedido then 0 else T.CantidadPedido - T.Existencia end),   
		Resta = Existencia - (case when T.Existencia >= T.CantidadPedido then T.CantidadPedido else T.Existencia end) 
	From #tmpClaves T 


--- Pasar la informacion a la Tabla Maestro de Pedidos de Claves 
	Delete from ALMJ_Concentrado_PedidosRC_Claves 
	Insert Into ALMJ_Concentrado_PedidosRC_Claves ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado, IdClaveSSA, 
		CantidadPedidoRC, ExistenciaCSGN, ExistenciaVTA, CantidadSurtir, CantidadPedido, Status, Actualizado ) 
	Select IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado, IdClaveSSA, 
		   CantidadPedido, ExistenciaCSGN, ExistenciaVTA, Surtido, Pendiente, 'A', 0   
	From #tmpClaves     
		
--	spp_Mtto_ALMJ_Concentrado_PedidosRC_Claves 		
	
--	Select * from #tmpClaves 
--	select * from ALMJ_Concentrado_PedidosRC_Claves 

End 
Go--#SQL
