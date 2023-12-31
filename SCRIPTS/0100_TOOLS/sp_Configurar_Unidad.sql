If Exists ( Select Name From Sysobjects Where Name = 'sp_Configurar_Unidad' and xType = 'P' )  
   Drop Proc sp_Configurar_Unidad 
Go--#SQL 

Create Proc sp_Configurar_Unidad 
(
	@NumeroFarmacia int = 1, @NombreUnidad varchar(50) = 'NOMBRE'  
)
As 
Begin 
Declare 
	@IdEmpresa varchar(3), 	
    @IdEstado varchar(2), 
    @IdFarmacia varchar(4),  
    -- @NumeroFarmacia int, 
    -- @NombreUnidad varchar(50),  
    @NombreBaseDeDatos varchar(100), 
    @Nombre_Data varchar(150), 
    @Nombre_Log varchar(150),  
    @Nombre_Data_Aux varchar(150), 
    @Nombre_Log_Aux varchar(150),      
    @Ruta_Destino_Data varchar(500), 
    @Ruta_Destino_Log varchar(500), 
	@RutaDeReportes varchar(500),  
	@sParametros varchar(500),      
	@sSql varchar(500)      


	Set @IdEmpresa = '001'    
    Set @IdEstado = '21'    
    -- Set @NumeroFarmacia = 4 
    Set @IdFarmacia = right('0000' + cast(@NumeroFarmacia as varchar), 4)  
    -- Set @NombreUnidad = 'NOMBRE' 
        
--- 
    Set @NombreBaseDeDatos = 'SII_' + @IdEstado + '_' + @IdFarmacia + '_' + @NombreUnidad 
    Set @Nombre_Data = @NombreBaseDeDatos + '_data.mdf' 
    Set @Nombre_Log = @NombreBaseDeDatos + '_log.mdf' 
    Set @Nombre_Data_Aux = @NombreBaseDeDatos + '_data' 
    Set @Nombre_Log_Aux = @NombreBaseDeDatos + '_log'   
    

    Set @Ruta_Destino_Data = 'D:\SII_BD\' + @Nombre_Data 
    Set @Ruta_Destino_Log = 'D:\SII_BD\' + @Nombre_Log 

    -- Select @NombreBaseDeDatos, @Nombre_Data, @Nombre_Log  
    

-------------------------------- Creacion de Base de Datos 
    RESTORE DATABASE @NombreBaseDeDatos 
    FROM DISK = N'D:\SII_BD\RESPALDOS\SII_PtoVta_EnBlanco_2K110701_142241.bak' 
        WITH FILE = 1, 
        MOVE N'SII_PtoVta_EnBlanco_data' TO @Ruta_Destino_Data, 
        MOVE N'SII_PtoVta_EnBlanco_log' TO @Ruta_Destino_Log, 
        NOUNLOAD,  REPLACE,  STATS = 10 


--- Renombrar los archivos Base 
    Set @sSql = 
        'ALTER DATABASE [' + @NombreBaseDeDatos + '] ' + ' MODIFY FILE (NAME = ' + char(39) + 'SII_PtoVta_EnBlanco_data' + char(39) + ' , NEWNAME = N' + char(39) + @Nombre_Data_Aux + char(39) + ' ) ' + char(10)  + 
        'ALTER DATABASE [' + @NombreBaseDeDatos + '] ' + ' MODIFY FILE (NAME = ' + char(39) + 'SII_PtoVta_EnBlanco_log' + char(39) + ' , NEWNAME = N' + char(39) + @Nombre_Log_Aux + char(39) + ' ) '    
    -- Print @sSql 
    Exec (  @sSql  )

-------------------------------- Creacion de Base de Datos 
    


-------------------------------- Parametros y Otras configuraciones 
	-- Use @NombreBaseDeDatos 
	Set @RutaDeReportes = 'D:\SII_PUNTO_DE_VENTA\REPORTES\'

--- Agregar Ruta de Reportes 
	Set @sParametros = char(39) + @IdEstado + char(39) + ', ' + char(39) + @IdFarmacia + char(39) + ', ' + char(39) + 'PFAR' + char(39) + ', ' + char(39) + 'RutaReportes' + char(39) + ', ' + char(39) + @RutaDeReportes + char(39)+ ', ' + char(39) + 'Determina la ruta donde se encuentran los reportes del Punto de Venta' + char(39) + ', 0, 0 ' 
	-- Set @sSql = 'Exec ' + @NombreBaseDeDatos + '.spp_Mtto_Net_CFGC_Parametros ' + [ @IdEstado, @IdFarmacia, 'PFAR', 'RutaReportes', @RutaDeReportes, 'Determina la ruta donde se encuentran los reportes del Punto de Venta', 0, 0     ] 
	Set @sSql = 'Exec ' + @NombreBaseDeDatos + '.dbo.spp_Mtto_Net_CFGC_Parametros ' + @sParametros 
	Exec ( @sSql ) 

--	sp_Configurar_Unidad 



---	Unidad Administrada por Servidor 
	Set @sParametros = char(39) + @IdEmpresa + char(39) + ', ' + char(39) + @IdEstado + char(39) + ', ' + char(39) + @IdFarmacia + char(39) + ', 1, 0 '
	Set @sSql = 'Exec ' + @NombreBaseDeDatos + '.dbo.spp_Mtto_CFG_Svr_UnidadesRegistradas ' + @sParametros 
	Exec ( @sSql ) 

	-- Exec spp_Mtto_CFG_Svr_UnidadesRegistradas @IdEmpresa, @IdEstado, @IdFarmacia, 1, 0 
	-- Select * From CFG_Svr_UnidadesRegistradas
-------------------------------- Parametros y Otras configuraciones     

End 
Go--#SQL 
