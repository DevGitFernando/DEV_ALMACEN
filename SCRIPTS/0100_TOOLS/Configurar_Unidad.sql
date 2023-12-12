Use Master 
Go--#SQL 

Declare 
	@NumeroUnidad int,  
	@Nombre_Unidad varchar(100) 

	Set @NumeroUnidad = 4  
	Set @Nombre_Unidad = 'HG_MICASA'


	Exec sp_Configurar_Unidad  @NumeroUnidad, @Nombre_Unidad 

