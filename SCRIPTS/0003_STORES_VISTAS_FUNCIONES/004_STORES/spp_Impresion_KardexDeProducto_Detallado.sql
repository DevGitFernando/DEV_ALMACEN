If Exists ( Select Name From sysobjects Where Name = 'spp_Impresion_KardexDeProducto_Detallado' and xType = 'P' )
   Drop Proc spp_Impresion_KardexDeProducto_Detallado 
Go--#SQL    

--    spp_Impresion_KardexDeProducto_Detallado 

Create Proc spp_Impresion_KardexDeProducto_Detallado 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182', 
    @IdProducto varchar(20) = '00000326', @FechaInicial varchar(10) = '2011-01-01', @FechaFinal varchar(10) = '2011-07-01' 
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

    Select 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, FechaSistema, FechaRegistro, Folio, DescMovimiento, Referencia, 
        IdProducto, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, CodigoEAN, ClaveLote, FechaRegistroLote, FechaCaducidad, 
        DescProducto, Entrada, Salida, Existencia, 
        Costo, TasaIva, Importe, Status, 
        cast(0 as numeric(14,4)) as CostoMinimo, cast(0 as numeric(14,4)) as CostoMaximo, 
        KeyxMovto, Keyx, getdate() as FechaDeSistema, 
        cast(@FechaInicial as datetime)as FechaInicial, cast(@FechaFinal as datetime)as FechaFinal
    Into #tmpKardex     
    From vw_Kardex_ProductoCodigoEAN_Lotes 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and CodigoEAN = @IdProducto 
          and convert(varchar(10), FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
    Order By KeyxMovto, Keyx,  FechaRegistroLote 

------- Establecer costos  
    Update K Set CostoMinimo = ( select min(Costo) From #tmpKardex where Costo > 0 ) 
    From #tmpKardex K 

    Update K Set CostoMaximo = ( select max(Costo) From #tmpKardex where Costo > 0 ) 
    From #tmpKardex K 

    Update K Set CostoMaximo = 1 From #tmpKardex K Where CostoMaximo = 0 
    Update K Set CostoMinimo = CostoMaximo From #tmpKardex K Where CostoMinimo = 0     
------- Establecer costos      
    
    
--      spp_Impresion_KardexDeProducto_Detallado  


--- Salida Final 
    Select * 
    From #tmpKardex  
    
End 
Go--#SQL 
 