	    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_JOB_Productos_Movimientos_Secretaria' and xType = 'P') 
    Drop Proc spp_JOB_Productos_Movimientos_Secretaria 
Go--#SQL 

Create Proc spp_JOB_Productos_Movimientos_Secretaria ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @Dias int = 30 ) 
With Encryption 
As 
Begin 
Declare 
	@FechaInicial varchar(10), --  = '2011-10-01', P
	@FechaFinal varchar(10) -- = '2012-01-01'

	Set @FechaInicial = convert(varchar(10), getdate() - @Dias, 120) 
	Set @FechaFinal = convert(varchar(10), getdate(), 120) 


	Exec spp_Rpt_CteReg_Productos_Movimientos_Secretaria  @IdEmpresa, @IdEstado, @FechaInicial, @FechaFinal, 0, '' 

End 
Go--#SQL 
