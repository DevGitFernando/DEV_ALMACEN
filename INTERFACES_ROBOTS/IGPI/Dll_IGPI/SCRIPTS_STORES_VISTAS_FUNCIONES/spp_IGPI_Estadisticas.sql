If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_IGPI_Estadisticas' and xType = 'P' ) 
   Drop Proc spp_IGPI_Estadisticas 
Go--#SQL      


Create Proc spp_IGPI_Estadisticas 
With Encryption 
As 
Begin 

Declare @iTotal float 

	Set @iTotal =  0 

--- 
	Select StatusIGPI, StatusIGPI_Aux, cast(count(*) as float)  as Registros, cast(0 as float) as Total, cast(0 as float) as Porcentaje   
	into #tmpStats 
	From vw_IGPI_SolicitudesDeProductos 
	Group By StatusIGPI, StatusIGPI_Aux 
	
	
--- Totales 	
	Select @iTotal = sum(Registros) From #tmpStats  	
	Update #tmpStats Set Total = @iTotal, Porcentaje =  (Registros / @iTotal) * 100 
	
	
	Select StatusIGPI, StatusIGPI_Aux, Registros, Total, Porcentaje -- * -- , (select sum(Porcentaje) From #tmpStats) as Totalx 
	From #tmpStats S 
	Order by Porcentaje Desc 
	
---   spp_IGPI_Estadisticas 	
	
	
End 
Go--#SQL   	

	