


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_JOB_Proceso_TipoMovimientos_Estados' and xType = 'P') 
    Drop Proc spp_JOB_Proceso_TipoMovimientos_Estados 
Go--#SQL 

Create Proc spp_JOB_Proceso_TipoMovimientos_Estados 
(   
	@IdEstado varchar(2) = '21', @Dias int = 30
) 
With Encryption 
As 
Begin 
Declare 
	@FechaInicial varchar(10), 
	@FechaFinal varchar(10) 

	Set @FechaInicial = convert(varchar(10), getdate() - @Dias, 120) 
	Set @FechaFinal = convert(varchar(10), getdate(), 120)

	Exec spp_Proceso_Tipos_Inv_Movimientos_Estados  @IdEstado, @FechaInicial, @FechaFinal 

End 
Go--#SQL 