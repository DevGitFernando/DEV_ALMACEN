If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciaClavesSolicitadas' and xType = 'P' ) 
   Drop Proc spp_Mtto_TransferenciaClavesSolicitadas 
Go--#SQL 

Create Proc spp_Mtto_TransferenciaClavesSolicitadas   
(	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0012', @FolioTransferencia varchar(30) = '00000000', 
	@IdClaveSSA varchar(4) = '0032', @CantidadRequerida int = 0, @Observaciones varchar(100) = 'PRUEBA'  
)
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sClaveSSA varchar(20), 
	@iExistencia numeric(14,4), 
	@iTieneCartaFaltante int   
	
	Select @sClaveSSA = ClaveSSA From vw_ClavesSSA_Sales (NoLock) Where IdClaveSSA_Sal = @IdClaveSSA 
	Select @iExistencia = dbo.fg_Existencia_Clave(@IdEmpresa, @IdEstado, @IdFarmacia, @sClaveSSA) 
	Select @iTieneCartaFaltante = dbo.fg_EsClaveFaltante(@sClaveSSA)  


	If Not Exists ( Select * From TransferenciasEstadisticaClavesDispensadas (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				  and FolioTransferencia = @FolioTransferencia and IdClaveSSA = @IdClaveSSA ) 
		Begin 
			Insert Into TransferenciasEstadisticaClavesDispensadas ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdClaveSSA, EsCapturada, 
					ExistenciaSistema, TieneCartaFaltante, CantidadRequerida, CantidadEntregada, Observaciones, Status, Actualizado ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTransferencia, @IdClaveSSA, 1 as EsCaptura, 
					@iExistencia, @iTieneCartaFaltante, @CantidadRequerida, 0, @Observaciones, 'A', 0  
		End 		

End 
Go--#SQL 
   
------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasClavesSolicitadasCalcularSurtimiento' and xType = 'P' ) 
   Drop Proc spp_Mtto_TransferenciasClavesSolicitadasCalcularSurtimiento 
Go--#SQL 

Create Proc spp_Mtto_TransferenciasClavesSolicitadasCalcularSurtimiento   
(	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0012', @FolioTransferencia varchar(30) = '00048639' 
)
With Encryption 
As 
Begin 
Set NoCount On 

---- Totalizar las Claves por Cajas Completas 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @FolioTransferencia as FolioTransferencia, 
		IdClaveSSA, sum (CajasCompletas) as CantidadSurtida 
	Into #tmpClavesDispensadas 
	From 
	( 
		Select V.IdEmpresa, V.IdFarmacia, V.FolioTransferencia, 
			V.IdProducto, V.CodigoEAN, V.ClaveLote, 
			P.IdClaveSSA_Sal as IdClaveSSA, V.CantidadEnviada, P.ContenidoPaquete, 
			cast((V.CantidadEnviada / (P.ContenidoPaquete)) as numeric(14,4) ) as CajasCompletas   
			-- P.*  
		From TransferenciasDet_Lotes V (NoLock) 
		Inner Join vw_Productos_CodigoEAN P (NoLock) On ( V.IdProducto = P.IdProducto and V.CodigoEAN = P.CodigoEAN ) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.FolioTransferencia = @FolioTransferencia		
	) as R 
	Group by IdClaveSSA 

	
------- Actualizar los datos de las Claves solicitadas 
	Update E Set CantidadEntregada = CantidadSurtida 
	From TransferenciasEstadisticaClavesDispensadas E 
	Inner Join #tmpClavesDispensadas V 
		On ( E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioTransferencia = V.FolioTransferencia
		     and E.IdClaveSSA = V.IdClaveSSA ) 

------- Agregar las Claves que se entregarón sin capturar 
    Insert Into TransferenciasEstadisticaClavesDispensadas ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdClaveSSA, EsCapturada, CantidadRequerida, CantidadEntregada, Status, Actualizado ) 	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdClaveSSA, 0 as Capturada, 
		0 as CantidadRequerida, CantidadSurtida, 'A' as Status, 0 as Actualizado 
	From #tmpClavesDispensadas V 
	Where Not Exists 
	 (
		Select * From  TransferenciasEstadisticaClavesDispensadas E (NoLock) 
		Where E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioTransferencia = V.FolioTransferencia 
			and E.IdClaveSSA = V.IdClaveSSA 
	 )  
	
End
Go--#SQL