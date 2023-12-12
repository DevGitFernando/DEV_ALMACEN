If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_VentasClavesSolicitadas' and xType = 'P' ) 
   Drop Proc spp_Mtto_VentasClavesSolicitadas 
Go--#SQL 

Create Proc spp_Mtto_VentasClavesSolicitadas   
(	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0012', @FolioVenta varchar(30) = '00000000', 
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


	If Not Exists ( Select * From VentasEstadisticaClavesDispensadas (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				  and FolioVenta = @FolioVenta and IdClaveSSA = @IdClaveSSA ) 
		Begin 
			Insert Into VentasEstadisticaClavesDispensadas ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdClaveSSA, EsCapturada, 
					ExistenciaSistema, TieneCartaFaltante, CantidadRequerida, CantidadEntregada, Observaciones, Status, Actualizado ) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @IdClaveSSA, 1 as EsCaptura, 
					@iExistencia, @iTieneCartaFaltante, @CantidadRequerida, 0, @Observaciones, 'A', 0  
		End 		

End 
Go--#SQL 
   
------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_VentasClavesSolicitadasCalcularSurtimiento' and xType = 'P' ) 
   Drop Proc spp_Mtto_VentasClavesSolicitadasCalcularSurtimiento 
Go--#SQL 

Create Proc spp_Mtto_VentasClavesSolicitadasCalcularSurtimiento   
(	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0012', @FolioVenta varchar(30) = '00048639' 
)
With Encryption 
As 
Begin 
Set NoCount On 

---- Totalizar las Claves por Cajas Completas 
	Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @FolioVenta as FolioVenta, 
		IdClaveSSA, sum (CajasCompletas) as CantidadSurtida 
	Into #tmpClavesDispensadas 
	From 
	( 
		Select V.IdEmpresa, V.IdFarmacia, V.FolioVenta, 
			V.IdProducto, V.CodigoEAN, V.ClaveLote, 
			P.IdClaveSSA_Sal as IdClaveSSA, V.CantidadVendida, P.ContenidoPaquete, 
			cast((V.CantidadVendida / (P.ContenidoPaquete)) as numeric(14,4) ) as CajasCompletas   
			-- P.*  
		From VentasDet_Lotes V (NoLock) 
		Inner Join vw_Productos_CodigoEAN P (NoLock) On ( V.IdProducto = P.IdProducto and V.CodigoEAN = P.CodigoEAN ) 
----		Left Join VentasEstadisticaClavesDispensadas E (NoLock) 
----			On ( E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioVenta = V.FolioVenta ) 	
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.FolioVenta = @FolioVenta		
	) as R 
	Group by IdClaveSSA 

	
------- Actualizar los datos de las Claves solicitadas 
	Update E Set CantidadEntregada = CantidadSurtida 
	From VentasEstadisticaClavesDispensadas E 
	Inner Join #tmpClavesDispensadas V 
		On ( E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioVenta = V.FolioVenta
		     and E.IdClaveSSA = V.IdClaveSSA ) 

------- Agregar las Claves que se entregarón sin capturar 
    Insert Into VentasEstadisticaClavesDispensadas ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdClaveSSA, EsCapturada, CantidadRequerida, CantidadEntregada, Status, Actualizado ) 	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdClaveSSA, 0 as Capturada, 
		0 as CantidadRequerida, CantidadSurtida, 'A' as Status, 0 as Actualizado 
	From #tmpClavesDispensadas V 
	Where Not Exists 
	 (
		Select * From  VentasEstadisticaClavesDispensadas E (NoLock) 
		Where E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioVenta = V.FolioVenta 
			and E.IdClaveSSA = V.IdClaveSSA 
	 )  
	
End
Go--#SQL 	  
