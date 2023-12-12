
/* 

	Select * 
	From tmpProceso_Reindexado (nolock) 
	where Orden = 47     

*/ 


	select Procesado, count(*) as Registros, ((count(*) / 519.0) * 100) as Porcentaje    
	from tmpProceso_Reindexado (NoLock) 
	group by Procesado 
	
	