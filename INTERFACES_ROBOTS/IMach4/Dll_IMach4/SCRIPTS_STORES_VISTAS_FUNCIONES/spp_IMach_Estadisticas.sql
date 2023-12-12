If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_IMach_Estadisticas' and xType = 'P' ) 
   Drop Proc spp_IMach_Estadisticas 
Go--#SQL      


Create Proc spp_IMach_Estadisticas 
With Encryption 
As 
Begin 

Declare @iTotal float 

	Set @iTotal =  0 

--- 
	Select StatusIMach, StatusIMach_Aux, cast(count(*) as float)  as Registros, cast(0 as float) as Total, cast(0 as float) as Porcentaje   
	into #tmpStats 
	From vw_IMach_SolicitudesDeProductos 
	Group By StatusIMach, StatusIMach_Aux 
	
	
--- Totales 	
	Select @iTotal = sum(Registros) From #tmpStats  	
	Update #tmpStats Set Total = @iTotal, Porcentaje =  (Registros / @iTotal) * 100 
	
	
	Select StatusIMach, StatusIMach_Aux, Registros, Total, Porcentaje -- * -- , (select sum(Porcentaje) From #tmpStats) as Totalx 
	From #tmpStats S 
	Order by Porcentaje Desc 
	
---   spp_IMach_Estadisticas 	
	
	
End 
Go--#SQL   	

	