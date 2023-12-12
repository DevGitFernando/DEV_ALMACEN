------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_Unidad_GenerarRemisiones' and xType = 'P') 
    Drop Proc spp_INT_ND_Unidad_GenerarRemisiones
Go--#SQL 
  
/* 
	Exec spp_INT_ND_Unidad_GenerarRemisiones  
		@IdEmpresa = '003', @IdEstado = '16', @CodigoCliente = '2181428', 
		@FechaDeProceso = '2014-12-12', @GUID = '38bd84e6-0756-4c7f-ae22-4469c1efd0f4' 

*/   
  
Create Proc spp_INT_ND_Unidad_GenerarRemisiones 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @CodigoCliente varchar(20) = '1787489', -- '2179105', 
    @FechaDeProceso_Inicial varchar(10) = '2015-05-01', @FechaDeProceso_Final varchar(10) = '2015-05-31',     
    @GUID varchar(100) = 'c5cf57ab-03f8-4de6-a92b-ec3661d5e566', 
    @Año_Causes int = 2012, @TipoDeProceso int = 2, @SepararCauses int = 0    
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  


	If @TipoDeProceso = 1 
		Begin 
			Exec spp_INT_ND_Unidad_GenerarRemisiones__001_Ejecucion @IdEmpresa, @IdEstado, @CodigoCliente, 
			@FechaDeProceso_Inicial, @GUID, @Año_Causes 
		End 
	Else 
		Begin   
			Exec spp_INT_ND_Unidad_GenerarRemisiones__002_Historico @IdEmpresa, @IdEstado, @CodigoCliente, 
				@FechaDeProceso_Inicial, @FechaDeProceso_Final, @GUID, @Año_Causes 		
		End 
	
	
End  
Go--#SQL 

