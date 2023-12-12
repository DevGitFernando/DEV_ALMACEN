If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_Efectividad_Vales' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_Efectividad_Vales 
Go--#SQL 

--  Exec spp_Rpt_CteReg_Efectividad_Vales '21', '0188', '2012-01-01', '2012-01-01' 

Create Proc spp_Rpt_CteReg_Efectividad_Vales 
( 
    @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0224', 
    @FechaInicial varchar(10) = '2011-12-01', @FechaFinal varchar(10) = '2011-12-30'
) 
With Encryption 
As 
Begin 
Declare @EncPrincipal varchar(500), 
		@EncSecundario varchar(500)
		
--- Encabezado de reportes 		
    Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	
    		
----------------------- Vales Emitidos a procesar 
    Select V.IdEmpresa, E.Nombre as Empresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia, 
         V.FolioVale, V.FechaRegistro as FechaEmision, space(10) as FolioRegistro, 0 as TieneRegistro, 
         S.ClaveSSA, S.DescripcionClave, 
         cast(ceiling(D.Cantidad) as int) as CantidadRequerida, 
         0 as CantidadSurtida,  
         0 as TotalCantidadRequerida, 
         0 as TotalCantidadSurtida, 
         cast(0 as numeric(14,4)) as PorcEfectividad 
         
    into #tmpVales_Emitidos  
    From Vales_EmisionEnc V (NoLock) 
    Inner Join Vales_EmisionDet D On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVale = D.FolioVale ) 
    Inner Join vw_ClavesSSA_Sales S On ( D.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) 
    Inner Join CatEmpresas E (NoLock) On ( V.IdEmpresa = E.IdEmpresa ) 
    Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )
    Where V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
          and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal 
          and D.Cantidad > 0       
----------------------- Vales Emitidos a procesar     
   
    
----------------------------------- Cantidades recibidas     
    Select 
        V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.Folio, V.FolioVale, V.FechaRegistro as FechaRecepcion, 
        P.ClaveSSA, P.DescripcionClave, cast(sum(CantidadRecibida) as int) as CantidadRecibida    
    into #tmpVales_Recepcion 
    From ValesDet D (NoLock) 
    Inner Join ValesEnc V (NoLock) 
        On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.Folio = V.Folio )
    Inner Join 
    (
        Select IdEmpresa, IdEstado, IdFarmacia, FolioVale 
        From #tmpVales_Emitidos 
        Group by IdEmpresa, IdEstado, IdFarmacia, FolioVale 
    ) F On (  F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVale = V.FolioVale ) 
    Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
    Group by 
        V.IdEmpresa, V.IdEstado, V.IdFarmacia,  V.Folio, V.FolioVale, V.FechaRegistro, 
        P.ClaveSSA, P.DescripcionClave
    
--- Asignar la informacion de los Vales registrados     
    Update F Set 
        FolioRegistro = V.Folio, TieneRegistro = 1,  
        CantidadSurtida = V.CantidadRecibida     
    From #tmpVales_Emitidos F 
    Inner Join #tmpVales_Recepcion V (NoLock) 
        On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVale = V.FolioVale ) 
----------------------------------- Cantidades recibidas 
    
---------------- Totalizar     
    Update F Set TotalCantidadRequerida = 
                ( 
                   Select sum(CantidadRequerida) 
                   From #tmpVales_Emitidos V
                   Where F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVale = V.FolioVale
                 )   
    From #tmpVales_Emitidos F 
    
    Update F Set TotalCantidadSurtida = 
                ( 
                   Select sum(CantidadSurtida) 
                   From #tmpVales_Emitidos V
                   Where F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVale = V.FolioVale
                 )   
    From #tmpVales_Emitidos F     
    
--- Efectividad del Vale emitido     
    Update F Set PorcEfectividad = (TotalCantidadSurtida / (TotalCantidadRequerida * 1.0)) * 100 
    From #tmpVales_Emitidos F 
    Where TotalCantidadRequerida > 0     
    
---------------- Totalizar         


---    Select * From #tmpVales_Emitidos     where PorcEfectividad = 0     

--      spp_Rpt_CteReg_Efectividad_Vales         



----------------------------- Determinar Totales    
Declare 
    @iClaves int, 
    @fEfectividad float, 
    @iValesEmitidos int, 
    @iValesRegistrados int, 
    @iVales_No_Registrados int, 
    @iValesSurtidosCompletos int, 
    @iValesSurtidosParcialmente int,     
    @iCant_Requerida int, 
    @iCant_Surtida int,     
    @iCant_No_Surtida int     

--      spp_Rpt_CteReg_Efectividad_Vales     

-------- Vales 
    Select @iValesEmitidos = count(Distinct FolioVale) From #tmpVales_Emitidos 
    Select @iVales_No_Registrados = count(Distinct FolioVale) From #tmpVales_Emitidos Where TieneRegistro = 0         
    Select @iValesRegistrados = count(Distinct FolioVale) From #tmpVales_Emitidos Where TieneRegistro = 1     
    Select @iValesSurtidosCompletos = count(Distinct FolioVale) From #tmpVales_Emitidos Where TieneRegistro = 1 and TotalCantidadRequerida = TotalCantidadSurtida 
    Select @iValesSurtidosParcialmente = count(Distinct FolioVale) From #tmpVales_Emitidos Where TieneRegistro = 1 and TotalCantidadRequerida <> TotalCantidadSurtida 

    -- Set @iVales_No_Registrados = @iValesEmitidos - @iValesRegistrados  



-------- Cantidades 
    Select @iClaves = count(Distinct ClaveSSA) From #tmpVales_Emitidos 
    Select @iCant_Requerida = sum(CantidadRequerida), @iCant_Surtida = sum(CantidadSurtida) From #tmpVales_Emitidos     
    Set @iCant_No_Surtida = @iCant_Requerida - @iCant_Surtida 

-------- Efectividad de Vales 
    Select @fEfectividad = avg(PorcEfectividad) 
    From 
    ( 
        Select FolioVale, avg(PorcEfectividad) as PorcEfectividad  
        From #tmpVales_Emitidos 
        Group by FolioVale, PorcEfectividad  
    ) as T 
    
    Select @fEfectividad = (@iCant_Surtida / (@iCant_Requerida *  1.00)) * 100     
    
----------------------------- Determinar Totales        
    
--------------------- Salida Final 
    If Exists ( Select * From Sysobjects (NoLock) Where Name = 'Rpt_CteReg_EfectividadVales' and xType = 'U' ) 
       Drop Table Rpt_CteReg_EfectividadVales 
          
    Select 
         @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,  
         IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
         FolioVale, FechaEmision, FolioRegistro, TieneRegistro, 
         ClaveSSA, DescripcionClave, 
         CantidadRequerida, CantidadSurtida,  
         TotalCantidadRequerida, TotalCantidadSurtida, 
         PorcEfectividad, 
         @FechaInicial as FechaInicial, @FechaFinal as FechaFinal,  
         getdate() as FechaImpresion      
    Into Rpt_CteReg_EfectividadVales 
    From #tmpVales_Emitidos 
    Order by FolioVale 
    
--- Resumen reporte         
    Select 
        IsNull(@iValesEmitidos, 0) as ValesEmitidos, 
        IsNull(@iValesRegistrados, 0) as ValesRegistrados, 
        IsNull(@iValesSurtidosCompletos, 0) as ValesSurtidosCompletos, 
        IsNull(@iValesSurtidosParcialmente, 0) as ValesSurtidosParcialmente,         
        IsNull(@iVales_No_Registrados, 0) as Vales_No_Registrados, 
        IsNull(@iClaves, 0) as Claves, 
        IsNull(@iCant_Requerida, 0) as CantidadRequerida,                 
        IsNull(@iCant_Surtida, 0) as CantidadSurtida, 
        IsNull(@iCant_No_Surtida, 0) as Cantidad_No_Surtida, 
        IsNull(@fEfectividad, 0) as Efectividad   
    
--------------------- Salida Final     
    
    

----    where 
----        TieneRegistro = 1 
----        and FolioVale = 00002098 
----        -- FolioRegistro = 00000238 

    
--    Select * From #tmpVales_Recepcion where FolioVale = 00001674       
    
--      spp_Rpt_CteReg_Efectividad_Vales     


--    select top 1 *     from Vales_EmisionDet 
    
    
End 
Go--#SQL 
 
    