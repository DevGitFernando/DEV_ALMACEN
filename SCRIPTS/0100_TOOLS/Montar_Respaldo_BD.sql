
Declare 
    @IdEstado varchar(2), 
    @IdFarmacia varchar(4),  
    @NumeroFarmacia int, 
    @NombreUnidad varchar(50),  
    @NombreBaseDeDatos varchar(100), 
    @Nombre_Data varchar(150), 
    @Nombre_Log varchar(150),  
    @Nombre_Data_Aux varchar(150), 
    @Nombre_Log_Aux varchar(150),      
    @Ruta_Destino_Data varchar(500), 
    @Ruta_Destino_Log varchar(500), 
    @sSql varchar(500)      

    
    Set @IdEstado = '21'    
    Set @NumeroFarmacia = 4 
    Set @IdFarmacia = right('0000' + cast(@NumeroFarmacia as varchar), 4)  
    Set @NombreUnidad = 'NOMBRE' 
        
--- 
    Set @NombreBaseDeDatos = 'SII_' + @IdEstado + '_' + @IdFarmacia + '_' + @NombreUnidad 
    Set @Nombre_Data = @NombreBaseDeDatos + '_data.mdf' 
    Set @Nombre_Log = @NombreBaseDeDatos + '_log.mdf' 
    Set @Nombre_Data_Aux = @NombreBaseDeDatos + '_data' 
    Set @Nombre_Log_Aux = @NombreBaseDeDatos + '_log'   
    

    Set @Ruta_Destino_Data = 'D:\BaseDeDatos\Respaldos\' + @Nombre_Data 
    Set @Ruta_Destino_Log = 'D:\BaseDeDatos\Respaldos\' + @Nombre_Log 

    -- Select @NombreBaseDeDatos, @Nombre_Data, @Nombre_Log  
    


    RESTORE DATABASE @NombreBaseDeDatos 
    FROM DISK = N'D:\BaseDeDatos\Respaldos\SII_PtoVta_EnBlanco_2K110701_142241.bak' 
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
    
    
    
    