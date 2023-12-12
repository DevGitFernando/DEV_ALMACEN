If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_JOB_Calcular_Consumo_Mensual' and xType = 'P' ) 
    Drop Proc spp_JOB_Calcular_Consumo_Mensual 
Go--#SQL 

Create Proc spp_JOB_Calcular_Consumo_Mensual 
(   
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @MesesRevision int = 4   
) 
With Encryption 
As 
Begin 
Set DateFormat YMD  
Declare		
	@Fecha datetime, 
	@Revisiones int,   
	@A�o int, 
	@Mes int 
	
	Select @Fecha = getdate() 
	Set @Revisiones = 0 
	Set @A�o = 0 
	Set @Mes = 0 	
	
	While ( @Revisiones < @MesesRevision ) 
	Begin 
		
		Print @Fecha 
		Set @A�o = datepart(yy, @Fecha)  
		Set @Mes = datepart(mm, @Fecha)  		
	
		-----   Ejecutar el proceso 
		Exec spp_INV_ADMI_Consumos_Claves @IdEmpresa, @IdEstado, @A�o, @Mes  	
		
		
		Set @Fecha = dateadd(mm, -1, @Fecha) 		
		Set @Revisiones = @Revisiones + 1 
		
	End 
	

End 
Go--#SQL 
