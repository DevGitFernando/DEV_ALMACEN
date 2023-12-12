If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Kardex_Por_Clave' and xType = 'P' ) 
   Drop Proc spp_Rpt_Kardex_Por_Clave 
Go--#SQL 

--  Exec spp_Rpt_Kardex_Por_Clave '001', '21', '0182', '101', '2010-12-06','2011-12-06' 

Create Proc spp_Rpt_Kardex_Por_Clave 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182', 
    @ClaveSSA varchar(30) = '109', @FechaInicial varchar(10) = '2011-11-04', @FechaFinal varchar(10) = '2012-11-14'
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 

Declare 
    @Keyx int, 
    @ExistenciaInicial int, 
    @Entrada int, 
    @Salida int  

    Set @ExistenciaInicial = 0 
    Set @Entrada = 0 
    Set @Salida = 0     


    Select Top 0 ClaveSSA  
    Into #tmpClaves_Procesar 
    From vw_ExistenciaPorSales 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

    Insert Into #tmpClaves_Procesar Select @ClaveSSA  

----    If @ClaveSSA <> '*'
----        Begin 
----           Insert Into #tmpClaves_Procesar Select @ClaveSSA 
----        End 
----    Else 
----        Begin 
----            Insert Into #tmpClaves_Procesar 
----            Select ClaveSSA  
----            From vw_ExistenciaPorSales 
----            Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
----        End 

--- Obtener información base del Reporte     
    Select K.IdEmpresa, K.Empresa, 
        K.IdEstado, F.Estado, K.IdFarmacia, F.Farmacia,  
        cast( convert(varchar(10), K.FechaRegistro, 120) as datetime) as FechaRegistro, 
        K.IdProducto, K.CodigoEAN, 
        K.ClaveSSA, K.DescripcionSal as DescripcionClave, 
        cast(sum(K.Entrada) as int) as Entradas, cast(sum(K.Salida) as int) as Salidas, 
        cast( 
        dbo.fg_Existencia_A_Una_Fecha
            ( 
                K.IdEmpresa, K.IdEstado, K.IdFarmacia, 
                K.IdProducto, K.CodigoEAN, 
                convert(varchar(10), K.FechaRegistro, 120) 
            )
            as int )  as Existencia 
        -- 0 as Existencia   
    Into #tmpKardex     
    From vw_Kardex_ProductoCodigoEAN K (nolock) 
    Inner Join vw_Farmacias F (NoLock) On ( K.IdEstado = F.IdEstado and K.IdFarmacia = F.IdFarmacia ) 
    Where K.IdEmpresa = @IdEmpresa and K.IdEstado = @IdEstado and K.IdFarmacia = @IdFarmacia 
          and K.ClaveSSA in ( Select ClaveSSA From #tmpClaves_Procesar ) 
          and convert(varchar(10), K.FechaRegistro, 120) between @FechaInicial and @FechaFinal 
    Group by 
        K.IdEmpresa, K.Empresa, K.IdEstado, F.Estado, K.IdFarmacia, F.Farmacia,  
        K.IdProducto, K.CodigoEAN, 
        K.ClaveSSA, K.DescripcionSal, 
        convert(varchar(10), K.FechaRegistro, 120)  
        
--    Select * from #tmpKardex order by FechaRegistro 
--- Obtener información base del Reporte     



--- Salida de Informacion 
    Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
        ClaveSSA, DescripcionClave, space(100) as Presentacion, 0 as ContenidoPaquete, 
        FechaRegistro,  
        sum(Entradas) as Entradas, sum(Salidas) as Salidas, 
        sum(Existencia) as Existencia, 
        sum(Existencia) as ExistenciaAux,  
        --0 as Entradas_Aux, 
        identity(int, 1, 1) as  Keyx    
    Into #tmpKardex_Claves      
    from #tmpKardex (nolock)     
    group by 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
        ClaveSSA, DescripcionClave, FechaRegistro 
        
    Update K Set Presentacion = C.Presentacion, ContenidoPaquete = C.ContenidoPaquete  
    From #tmpKardex_Claves K 
    Inner Join vw_ClavesSSA_Sales C On ( K.ClaveSSA = C.ClaveSSA )
        

    Set @ExistenciaInicial = 0 
    Set @Entrada = 0 
    Set @Salida = 0    
        
    Select @ExistenciaInicial = Existencia From #tmpKardex_Claves Where Keyx = 1 
    
    
    Declare tmpExistencia  
    Cursor For 
    Select Keyx, Entradas, Salidas 
    From #tmpKardex_Claves 
    Where Keyx >= 2 
    order by keyx
    Open tmpExistencia
    FETCH NEXT FROM tmpExistencia Into @Keyx, @Entrada, @Salida   
        WHILE @@FETCH_STATUS = 0
        BEGIN 
           Set @ExistenciaInicial = (@ExistenciaInicial + @Entrada ) - @Salida 

           Update E Set Existencia = @ExistenciaInicial 
           From  #tmpKardex_Claves E 
           Where Keyx = @Keyx 
           FETCH NEXT FROM tmpExistencia Into  @Keyx, @Entrada, @Salida   
        END
    Close tmpExistencia
    Deallocate tmpExistencia
        
    

-------------------------------------------------------------------------------------------- 
--    Drop Table Rpt_Kardex_Claves 
    
    If Not Exists ( Select Name From SysObjects (NoLock) Where Name = 'Rpt_Kardex_Claves' and xType = 'U' )
    Begin 
        Select 
            IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
            ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
            FechaRegistro, 
            Entradas, Salidas, Existencia, 
            getdate() as FechaImpresion, host_name() as MAC  
        Into Rpt_Kardex_Claves     
        From #tmpKardex_Claves E     
    End 

    Delete From Rpt_Kardex_Claves 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ClaveSSA = @ClaveSSA and MAC = host_name() 
     
    Insert Into Rpt_Kardex_Claves     
    Select 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
        ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
        FechaRegistro, 
        Entradas, Salidas, Existencia, 
        getdate() as FechaImpresion, host_name() as MAC  
    From #tmpKardex_Claves E 
    
--      Select * From Rpt_Kardex_Claves     
    
    
--------------------------------------------------------------------------------------------              
             
-----------------------  
    Select 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
        ClaveSSA, DescripcionClave, convert(varchar(10), FechaRegistro, 120) as FechaRegistro, 
        Entradas, Salidas, Existencia, 
        getdate() as FechaImpresion 
    From #tmpKardex_Claves E 
 
--      spp_Rpt_Kardex_Por_Clave 
    
        
End 
Go--#SQL 

    