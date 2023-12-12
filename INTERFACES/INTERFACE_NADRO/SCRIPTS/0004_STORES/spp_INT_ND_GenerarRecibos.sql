------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarRecibos' and xType = 'P') 
    Drop Proc spp_INT_ND_GenerarRecibos
Go--#SQL 
  
--		ExCB spp_INT_ND_GenerarRecibos '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarRecibos 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0011', 
    @CodigoCliente varchar(20) = '2181002', 
    @FechaInicial varchar(10) = '2016-01-05', @FechaFinal varchar(10) = '2016-01-05', 
    @GUID varchar(100) = '', @TipoDeProceso int = 1, @MostrarResultado int = 1    
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

	If @TipoDeProceso = 1 
		Begin 
			Exec spp_INT_ND_GenerarRecibos__001_Ejecucion @IdEmpresa, @IdEstado, @IdFarmacia, @CodigoCliente, 
				 @FechaInicial, @FechaFinal, @GUID, @MostrarResultado  
		End 
	Else 
		Begin   
			Exec spp_INT_ND_GenerarRecibos__002_Historico @IdEmpresa, @IdEstado, @IdFarmacia, @CodigoCliente, 
				 @FechaInicial, @FechaFinal, @GUID, @MostrarResultado  
		End 


End  
Go--#SQL 

