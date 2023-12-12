If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_JOB_ADMI_Vales_Estadisticas' and xType = 'P' ) 
    Drop Proc spp_JOB_ADMI_Vales_Estadisticas 
Go--#SQL 

Create Proc spp_JOB_ADMI_Vales_Estadisticas 
(   
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @MesesRevision int = 2 
)  
With Encryption 
As 
Begin 
Set DateFormat YMD  
Declare		
	@Fecha datetime, 
	@Revisiones int,   
	@Año int, 
	@Mes int, 
	@Dias int, 
	@FechaInicial datetime, 
	@FechaFinal datetime,  
	@sFechaInicial varchar(10), 
	@sFechaFinal varchar(10) 
	
	
	--Select @Fecha = getdate() 
	Select @Fecha = dateadd(mm, -1, getdate()) 	
	-- Select @FechaInicial, @FechaFinal, @sFechaInicial, @sFechaFinal 
	
	Set @IdEmpresa = right('000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000' + @IdEstado, 2) 	
	
	Set @Revisiones = 0 
	Set @Año = 0 
	Set @Mes = 0 	
	Set @FechaInicial = dateadd(mm, 1, getdate())   
	--Set @MesesRevision = @MesesRevision + 1 
	
	While ( @Revisiones < @MesesRevision ) 
	Begin 		
		Select @FechaInicial = dateadd(mm, -1, @FechaInicial)  			
		Set @Año = datepart(yy, @FechaInicial)  
		Set @Mes = datepart(mm, @FechaInicial)  	
		Select @Dias = dbo.fg_NumeroDiasAñoMes( @Año, @Mes ) 
		
		Select @FechaInicial = 
			cast(
			right('0000' + cast(@Año as varchar), 4) + '-' +   
			right('0000' + cast(@Mes as varchar), 2) + '-01' as datetime) 
		Select @FechaFinal = 		
			cast(
			right('0000' + cast(@Año as varchar), 4) + '-' +   
			right('0000' + cast(@Mes as varchar), 2) + '-' + 
			right('0000' + cast(@Dias as varchar), 2)  		  
			as datetime) 
		
--		spp_JOB_Calcular_Consumo_Mensual_Claves 		
		
		Select @sFechaInicial = convert(varchar(10), @FechaInicial, 120) 
		Select @sFechaFinal = convert(varchar(10), @FechaFinal, 120)			
		
		Select @FechaInicial, @FechaFinal, @sFechaInicial, @sFechaFinal 
	
		-----   Ejecutar el proceso 
		Exec spp_ADMI_Vales_Estadisticas @IdEmpresa, @IdEstado, @sFechaInicial,  @sFechaFinal, 0  			
						
		Set @Revisiones = @Revisiones + 1 
		
	End 
	

End 
Go--#SQL 
