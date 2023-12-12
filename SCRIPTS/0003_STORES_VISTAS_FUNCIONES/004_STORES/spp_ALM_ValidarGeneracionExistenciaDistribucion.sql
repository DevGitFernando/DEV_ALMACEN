---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ALM_ValidarGeneracionExistenciaDistribucion' and xType = 'P' ) 
   Drop Proc spp_ALM_ValidarGeneracionExistenciaDistribucion 
Go--#SQL 

Create Proc spp_ALM_ValidarGeneracionExistenciaDistribucion  
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0044' 
) 
With Encryption  
As 
Begin 
Set NoCount On 

	
	Select Top 1 
		cast(1 as bit) as ExistenPedidosEnSurtimiento, 
		'Se encontrarón folios de pedidos en proceso de surtimiento' as Mensaje
	From Pedidos_Cedis_Enc_Surtido P 
	Where Status not in ( 'C', 'D', 'T', 'R', 'F', 'E', 'P' ) and 
		P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia 

End 
Go--#SQL 

