If Exists ( Select * From Sysobjects (NoLock) where Name = 'spp_ALMN_Asignar_PCM_A_Pedidos_Cedis' and xType = 'P' ) 
   Drop Proc spp_ALMN_Asignar_PCM_A_Pedidos_Cedis 
Go--#SQL  

Create Proc spp_ALMN_Asignar_PCM_A_Pedidos_Cedis 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia Varchar(4) = '1101', @Año int = 2012, @Mes int = 2 
) 
With Encryption 
As 
Begin 
Set NoCount On  

	-- Poner aplicado a los pedidos originados desde la misma unidad
	Update Pedidos_Cedis_Det Set PCMAplicado = 1 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And PCMAplicado = 0


	Update P
		Set PCMAplicado = 1, PCM = IsNull((Select ConsumoMensual
										From INV_Consumos_Mensuales
										Where IdEmpresa = P.IdEmpresa And IdEstado = P.IdEstado And IdFarmacia = P.IdFarmacia And
										CLaveSSA = P.ClaveSSA And Año = @Año And Mes = @Mes), 0)
	From Pedidos_Cedis_Det P 
	Where PCMAplicado = 0


End 
Go--#SQL 
