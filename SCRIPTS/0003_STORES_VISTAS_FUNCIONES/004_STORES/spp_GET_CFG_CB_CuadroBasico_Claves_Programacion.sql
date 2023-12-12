------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_GET_CFG_CB_CuadroBasico_Claves_Programacion' and xType = 'P' ) 
	Drop Proc spp_GET_CFG_CB_CuadroBasico_Claves_Programacion
Go--#SQL 

Create Proc spp_GET_CFG_CB_CuadroBasico_Claves_Programacion 
( 
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0016', @Año int = 2016, @Mes int = 7 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro varchar(200) 

	Set @sFiltro = 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + 
		' and Año = ' + char(39) + cast(@Año as varchar) + char(39) + ' and Mes = ' + char(39) + cast(@Mes as varchar) + char(39)
	
	
	Set @sFiltro = 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + 
		' and Año = ' + cast(@Año as varchar) + ' and Mes = ' + cast(@Mes as varchar)  
	
	Set @sSql = 'Exec spp_CFG_ObtenerDatos ' + char(39) + 'CFG_CB_CuadroBasico_Claves_Programacion' + char(39) + ', [ ' + @sFiltro + ' ], 1 ' + char(13) + char(10) 
	Set @sSql = @sSql + 'Exec spp_CFG_ObtenerDatos ' + char(39) + 'CFG_CB_CuadroBasico_Claves_Programacion_Excepciones' + char(39) + ', [ ' + @sFiltro + ' ], 1 ' + char(13) + char(10) 

---		spp_GET_CFG_CB_CuadroBasico_Claves_Programacion  

	Print @sSql 
	Exec(@sSql) 

--CFG_CB_CuadroBasico_Claves_Programacion 
--CFG_CB_CuadroBasico_Claves_Programacion_Excepciones  

End 
Go--#SQL 
