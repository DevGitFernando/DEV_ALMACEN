------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_Remisiones_Cancelacion_Completa' and xType = 'P')
    Drop Proc spp_INT_MA__FACT_Remisiones_Cancelacion_Completa 
Go--#SQL
  
Create Proc spp_INT_MA__FACT_Remisiones_Cancelacion_Completa 
( 
	@IdEmpresa varchar(3) = '002', @IdEstadoGenera varchar(2) = '09', 
	@IdFarmaciaGenera varchar(4) = '0001', @FolioRemision varchar(10) = '0000000007',  
	@IdPersonalCancela varchar(4) = '0001', @Observaciones varchar(200) = ''  
)
With Encryption 
As
Begin 
Set NoCount On 

		
----  Se Desmarca la informacion referente al folio de remision que se esta cancelando------------------
	Update L Set L.EnRemision = 0, L.FolioRemision = ''
	From
		VentasDet L(NoLock) 
	Inner Join
		FACT_Remisiones V(NoLock) 
		On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.FolioRemision = v.FolioRemision)
	Where V.IdEmpresa = @IdEmpresa And V.IdEstadoGenera = @IdEstadoGenera And V.IdFarmaciaGenera = @IdFarmaciaGenera And V.FolioRemision = @FolioRemision 
-----------------------------------------------------------------------------------------------------------------------------------------------
				
-------------- Borrar información de Detalles   
	Delete FACT_Remisiones_Detalles 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstadoGenera And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		
------------ Borrar información de Detalles  
	

----- Se actualiza el encabezado de la remision original 
	Update R
	Set Status = 'C', SubTotalSinGrabar = 0, 
		SubTotalGrabado = 0, Iva = 0, Total = 0,
		IdPersonalCancela = @IdPersonalCancela, FechaCancelacion = GETDATE()
	From FACT_Remisiones R (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstadoGenera = @IdEstadoGenera And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 			
------------ Generar el registro de Cancelacion	
	
End 
Go--#SQL 
