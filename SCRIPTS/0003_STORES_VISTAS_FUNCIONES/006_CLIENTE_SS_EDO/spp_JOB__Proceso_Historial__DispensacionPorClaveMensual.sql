	    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_JOB_Proceso_Historial__DispensacionPorClaveMensual' and xType = 'P') 
    Drop Proc spp_JOB_Proceso_Historial__DispensacionPorClaveMensual 
Go--#SQL 

Create Proc spp_JOB_Proceso_Historial__DispensacionPorClaveMensual 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @Dias int = 90 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@FechaInicial varchar(10), --  = '2011-10-01', P  
	@FechaFinal varchar(10) -- = '2012-01-01'

	Set @FechaInicial = convert(varchar(10), getdate() - @Dias, 120) 
	Set @FechaFinal = convert(varchar(10), getdate(), 120) 


	Exec spp_Proceso_Historial__DispensacionPorClaveMensual  @IdEmpresa, @IdEstado, @FechaInicial, @FechaFinal   

End 
Go--#SQL 
