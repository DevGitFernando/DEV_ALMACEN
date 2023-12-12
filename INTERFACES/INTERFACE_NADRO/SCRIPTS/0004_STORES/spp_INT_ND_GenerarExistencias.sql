------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarExistencias' and xType = 'P') 
    Drop Proc spp_INT_ND_GenerarExistencias 
Go--#SQL 
  
--  ExCB spp_INT_ND_GenerarExistencias '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarExistencias 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', 
    @IdFarmacia varchar(4) = '13', 
    @CodigoCliente varchar(20) = '2179067', 
    @FechaDeProceso varchar(10) = '2015-05-31', 
    @TipoDeProceso int = 1, 
    @MostrarResultado int = 1     
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  


	If @TipoDeProceso = 1 
		Begin 
			Exec spp_INT_ND_GenerarExistencias__001_Ejecucion @IdEmpresa, @IdEstado, @IdFarmacia, @CodigoCliente, 
				 @FechaDeProceso, @MostrarResultado  
		End 
	Else 
		Begin   
			Exec spp_INT_ND_GenerarExistencias__002_Historico @IdEmpresa, @IdEstado, @IdFarmacia, @CodigoCliente, 
				 @FechaDeProceso, @MostrarResultado 
		End 
	
End  
Go--#SQL 

