	    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_JOB_Proceso_Historial_EdoJuris_NoCauses_Surtimiento_Recetas' and xType = 'P') 
    Drop Proc spp_JOB_Proceso_Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
Go--#SQL 

Create Proc spp_JOB_Proceso_Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
(   
	@IdEstado varchar(2) = '21', @Dias int = 30, @Porcentaje numeric(14,4) = 20 
) 
With Encryption 
As 
Begin 
Declare 
	@FechaInicial varchar(10), --  = '2011-10-01', P
	@FechaFinal varchar(10) -- = '2012-01-01'

	Set @FechaInicial = convert(varchar(10), getdate() - @Dias, 120) 
	Set @FechaFinal = convert(varchar(10), getdate(), 120) 


	Exec spp_Proceso_Historial_EdoJuris_NoCauses_Surtimiento_Recetas  @IdEstado, @FechaInicial, @FechaFinal, @Porcentaje    

End 
Go--#SQL 
