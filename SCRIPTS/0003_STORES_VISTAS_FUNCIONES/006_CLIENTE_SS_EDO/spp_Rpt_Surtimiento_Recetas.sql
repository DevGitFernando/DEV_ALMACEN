If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRpt_SurtimientoRecetas' and xType = 'U' )
    Drop Table #tmpRpt_SurtimientoRecetas 
Go--#SQL 		    
		 
------------------------------ 		    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_SurtimientoRecetas' and xType = 'P') 
    Drop Proc spp_Rpt_SurtimientoRecetas 
Go--#SQL 
  
--  Exec spp_Rpt_SurtimientoRecetas '21', '0188', '0002', '0005', '2011-10-10', '2011-10-10'  
  
Create Proc spp_Rpt_SurtimientoRecetas 
(   
    @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0005', 
    @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005',     
    @FechaInicial varchar(10) = '2011-09-01', @FechaFinal varchar(10) = '2011-09-30', @iEjecutar int = 1  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
        @Empresa varchar(500), 
        @EncabezadoReporte varchar(500), 
        @EncabezadoPeriodo varchar(500),         
        @EncPrincipal varchar(500), 
		@EncSecundario varchar(500), 
		@Claves numeric(14,4), @ClavesPerfil int, @Abasto numeric(14,4), 
		@PorcAbasto numeric(14,4),  
		@iTickets numeric(14,4), @iVales numeric(14,4), 
		@iEsCapturaCompleta numeric(14,4), @iEsNoSurtido numeric(14,4)  
	 
	 
Declare @sFechaMinima datetime 


		--- Encabezado de reportes 
		Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

------- Generar Perifil de Farmacia 
        Select IdEstado, Estado, IdFarmacia, Farmacia, IdCliente, IdClaveSSA, ClaveSSA, DescripcionClave 
        Into #tmpPerfil 
        From vw_CB_CuadroBasico_Farmacias  
        Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and StatusClave = 'A' 

        Select @ClavesPerfil = count(*) From #tmpPerfil 
------- Generar Perifil de Farmacia 


		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRpt_SurtimientoRecetas' and xType = 'U' )
		    Drop Table #tmpRpt_SurtimientoRecetas 
		   
	    Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
	        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, Folio as FolioVenta, FechaRegistro, 
	        space(20) as FolioVale, 0 as TieneVale, 0 as EsNoSurtido,  
	        0 as Tickets, 0 as Vales, 0 as NoSurtido, 
	        cast(0 as numeric(14,4)) as PorcSurtido, 
	        cast(0 as numeric(14,4)) as PorcVales, 
	        
	        -- min(FechaRegistro) as FechaInicial, max(FechaRegistro) as FechaFinal  
	        getdate() as FechaInicial, getdate() as FechaFinal 
	    Into #tmpRpt_SurtimientoRecetas 
	    From vw_VentasEnc (NoLock) 
	    Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
	          and convert(varchar(10), FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
	          and TipoDeVenta =  2 
        Order by IdEmpresa, IdEstado, IdFarmacia, Folio 

------  Obtener Encabezados de reporte 
        Set @EncabezadoPeriodo = @FechaInicial + ' AL ' + @FechaFinal  
        Select top 1 @Empresa = Empresa, @EncabezadoReporte = IdFarmacia + '-' + Farmacia  
        From #tmpRpt_SurtimientoRecetas (NoLock) 
        
------  Obtener Encabezados de reporte         


------- Claves Surtidas 
---   Drop Table tmpClaves_Validar 
        Select CD.IdEmpresa, CD.IdEstado, CD.IdFarmacia, CD.FolioVenta, 
            CD.IdClaveSSA, S.ClaveSSA, S.DescripcionClave, 
            CD.CantidadRequerida,  
            ( case when (CD.CantidadRequerida = 0) Then IsNull(C.CantidadSurtida, 0) else CD.CantidadRequerida end ) as CantidadRequeridaAux, 
            IsNull(C.CantidadSurtida, 0) as CantidadSurtida, 
            IsNull(C.CantidadDevuelta, 0) as CantidadDevuelta                
        into #tmpClaves_Validar      
        From VentasEstadisticaClavesDispensadas CD (NoLock) 
        Inner Join #tmpRpt_SurtimientoRecetas L (NoLock) 
            On ( CD.IdEmpresa = L.IdEmpresa and CD.IdEstado = L.IdEstado and CD.IdFarmacia = L.IdFarmacia and CD.FolioVenta = L.FolioVenta )     
        Inner Join vw_ClavesSSA_Sales S (NoLock) On ( CD.IdClaveSSA = S.IdClaveSSA_Sal ) 
        Left Join 
        ( 
            Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioVenta, 
                P.IdClaveSSA_Sal, 
                sum(D.CantidadVendida) as CantidadSurtida, 
                sum(D.Cant_Devuelta) as  CantidadDevuelta  
            From VentasDet D 
            Inner Join vw_Productos_CodigoEAN  P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
            Inner Join #tmpRpt_SurtimientoRecetas L (NoLock) 
                    On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta ) 
            -- Where D.FolioVenta between 00010027 and 00010027 
            Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioVenta, P.IdClaveSSA_Sal   
        ) as C      
            On ( C.IdEmpresa = CD.IdEmpresa and C.IdEstado = CD.IdEstado and C.IdFarmacia = CD.IdFarmacia 
                 and C.FolioVenta = CD.FolioVenta and C.IdClaveSSA_Sal = CD.IdClaveSSA  )    
        -- Where CD.FolioVenta between 00024163 and 00024163  
        --Where CD.CantidadSurtida <> 0 


------- Totalizar las Claves 
		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpClaves_Cantidades' and xType = 'U' )
		    Drop Table #tmpClaves_Cantidades 
		    
        Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioVenta, 
             C.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, C.CantidadRequerida, 
             0 as TieneVale, 0 as EsNoSurtido
        Into #tmpClaves_Cantidades      
        From #tmpClaves_Validar C (noLock) 
        
        
        Delete From #tmpClaves_Validar Where CantidadSurtida <> 0 
        Delete From #tmpClaves_Validar Where CantidadDevuelta <> 0 
------- Claves Surtidas 
    
--        select top 1 * from VentasEstadisticaClavesDispensadas 

---     spp_Rpt_SurtimientoRecetas 

        Update F Set FolioVale = V.FolioVale, TieneVale = 1 
        From #tmpRpt_SurtimientoRecetas F  
        Inner Join Vales_EmisionEnc V (NoLock) 
            On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta ) 


        Update F Set EsNoSurtido = 1 
        From #tmpRpt_SurtimientoRecetas F  
        Inner Join #tmpClaves_Validar V (NoLock) 
            On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta ) 
        Inner Join #tmpPerfil CB (NoLock) 
            On ( CB.IdEstado = @IdEstado and CB.IdCliente = @IdCliente and CB.IdFarmacia = V.IdFarmacia and CB.IdClaveSSA = V.IdClaveSSA )     
        Where -- V.CantidadSurtida = 0 -- and V.CantidadEntregada = 0 
              -- and 
              TieneVale = 0         

---     spp_Rpt_SurtimientoRecetas 
        
-----   FECHAS         
        Update F Set FechaInicial = (select min(FechaRegistro) from #tmpRpt_SurtimientoRecetas) , FechaFinal = (select max(FechaRegistro) from #tmpRpt_SurtimientoRecetas)  
        From #tmpRpt_SurtimientoRecetas F  
-----   FECHAS                 


-----   Separar la informacion 
        Update F Set TieneVale = V.TieneVale, EsNoSurtido = V.EsNoSurtido
        From #tmpClaves_Cantidades F 
        Inner Join #tmpRpt_SurtimientoRecetas V (NoLock) 
            On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta )  
            

		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRpt_SurtimientoRecetas_ClavesCantidades_Aux' and xType = 'U' )
		    Drop Table #tmpRpt_SurtimientoRecetas_ClavesCantidades_Aux 
		    
        Select IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, DescripcionClave, 
            sum(Cantidad_Vales) as Cantidad_Vales, 
            sum(Cantidad_NoSurtido) as Cantidad_NoSurtido, 
            sum(CantidadRequerida - (Cantidad_NoSurtido + Cantidad_Vales)) as Cantidad_Dispensada,         
            sum(CantidadRequerida) as CantidadRequerida 
        Into #tmpRpt_SurtimientoRecetas_ClavesCantidades_Aux     
        From 
        ( 
            Select IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, DescripcionClave,  TieneVale, EsNoSurtido, 
                 (case when sum(TieneVale) >= 1 then sum(CantidadRequerida) else 0 end) as Cantidad_Vales, 
                 (case when sum(EsNoSurtido) >= 1 then sum(CantidadRequerida) else 0 end) as Cantidad_NoSurtido,              
                 sum(CantidadRequerida) as CantidadRequerida 
            From #tmpClaves_Cantidades 
    --         where -- TieneVale = 1 
    --               IdClaveSSA = '0324' 
    ----               EsNoSurtido = 1 
            group by IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, DescripcionClave, TieneVale, EsNoSurtido 

        ) as T   
        Group by IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, DescripcionClave 
            
-----   Separar la informacion 


        
----------------- Excepcion por emision de vales manuales 
        Select @sFechaMinima = cast(convert(varchar(10), min(FechaRegistro), 120) as datetime) 
               From Vales_EmisionEnc (NoLock) 
               Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 


        Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioVale, 
            FechaRegistro, FechaReceta, FechaVale  
        into #tmpExcepcionVales     
        From 
        ( 
            Select V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, T.FolioVale, 
                 cast(convert(varchar(10), V.FechaRegistro, 120) as datetime) as FechaRegistro, 
                 cast(convert(varchar(10), I.FechaReceta, 120) as datetime) as FechaReceta, 
                 cast(convert(varchar(10), T.FechaRegistro, 120) as datetime) as FechaVale  
            From VentasEnc V (NoLock) 
            Inner Join Vales_EmisionEnc T (NoLock) 
                On ( V.IdEmpresa = T.IdEmpresa and V.IdEstado = T.IdEstado and V.IdFarmacia = T.IdFarmacia and V.FolioVenta = T.FolioVenta )
            Inner Join VentasInformacionAdicional I (NoLock) 
                On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.FolioVenta )
            -- Where convert(varchar(10), V.FechaRegistro, 120) > '2011-09-15' 
            -- Where V.FolioVenta = 19680     
        ) as T 
        Where (FechaVale > FechaReceta) and (FechaReceta > @sFechaMinima)  
    
        Update F Set FolioVale = '', TieneVale = 0  
        From #tmpRpt_SurtimientoRecetas F  
        Inner Join #tmpExcepcionVales V (NoLock) 
            On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta )                
----------------- Excepcion por emision de vales manuales 
        
---     spp_Rpt_SurtimientoRecetas         
        
------------------------------------------ Contabilizar Claves  
        If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#Rpt_NivelDeAbasto_ClavesDispensadas' and xType = 'U' ) 
           Drop Table #Rpt_NivelDeAbasto_ClavesDispensadas         

------- Contabilizar Claves Dispensadas de Primera Vez 
        Select -- D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioVenta, 
            P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, 
            sum(D.CantidadVendida) as CantidadSurtida, -- sum
            -- sum(D.Cant_Devuelta) as  CantidadDevuelta,  
            1 as Tipo  
        into #Rpt_NivelDeAbasto_ClavesDispensadas 
        From VentasDet D 
        Inner Join vw_Productos_CodigoEAN  P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
        Inner Join #tmpRpt_SurtimientoRecetas L (NoLock) 
                On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta ) 
        Where TieneVale = 0 and EsNoSurtido = 0 
        Group by P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave
------- Contabilizar Claves Dispensadas de Primera Vez         

---     spp_Rpt_SurtimientoRecetas         

------- Contabilizar Claves Dispensadas por Vale 
        Insert Into #Rpt_NivelDeAbasto_ClavesDispensadas 
        Select -- D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioVenta, 
            P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, 
            sum(V.Cantidad) as CantidadSurtida, -- sum
            -- 0 as  CantidadDevuelta, 
            2 as Tipo    
        From Vales_EmisionDet V (NoLock) 
        Inner Join Vales_EmisionEnc D (NoLock) 
                On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVale = D.FolioVale )         
        Inner Join vw_ClavesSSA_Sales P On ( V.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) 
        Inner Join #tmpRpt_SurtimientoRecetas L (NoLock) 
                On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta ) 
        Where TieneVale = 1 and EsNoSurtido = 0 
        Group by P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave 
        Having sum(V.Cantidad) > 0 
------- Contabilizar Claves Dispensadas por Vale 

---     spp_Rpt_SurtimientoRecetas         

------- Contabilizar Claves Dispensadas No Surtido 
        Insert Into #Rpt_NivelDeAbasto_ClavesDispensadas 
        Select -- D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioVenta, 
            P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, 
            round(sum(D.CantidadEntregada), 0) as CantidadSurtida, -- sum
            -- 0 as  CantidadDevuelta, 
            3 as Tipo    
        From VentasEstadisticaClavesDispensadas D (NoLock) 
        Inner Join vw_ClavesSSA_Sales P On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
        Inner Join #tmpRpt_SurtimientoRecetas L (NoLock) 
                On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta ) 
        Where TieneVale = 0 and EsNoSurtido = 1 and EsCapturada = 0 
        Group by P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave 
        Having round(sum(D.CantidadEntregada), 0) > 0 
------- Contabilizar Claves Dispensadas No Surtido 

---     spp_Rpt_SurtimientoRecetas         

------- Contabilizar Claves Dispensadas 
        Select count(distinct IdClaveSSA) as ClavesDiferentes, sum(CantidadSurtida) as CantidadPiezas, 0 as Tipo  
        Into #tmp_ClavesDispensadas_Resumen      
        From #Rpt_NivelDeAbasto_ClavesDispensadas          
        
        Insert Into #tmp_ClavesDispensadas_Resumen              
        Select count(distinct IdClaveSSA) as ClavesDiferentes, sum(CantidadSurtida) as CantidadPiezas, Tipo  
        From #Rpt_NivelDeAbasto_ClavesDispensadas                 
        -- Where Tipo = 1 
        Group by Tipo        
        
--        select *         from #tmp_ClavesDispensadas_Resumen         order by Tipo 
        
------- Contabilizar Claves Dispensadas 

------------------------------------------ Contabilizar Claves  



        
------------------------------------------- Generar Resumen 
        Select @iTickets = (select count(*) from #tmpRpt_SurtimientoRecetas (nolock) )  
        Select @iVales = (select count(*) from #tmpRpt_SurtimientoRecetas (nolock) Where TieneVale =  1 ) 
        Select @iEsNoSurtido = (select count(*) from #tmpRpt_SurtimientoRecetas (nolock) Where EsNoSurtido =  1 ) 
    
        -- select @iTickets, @iVales 
        Update F Set 
            Tickets = @iTickets,  
            Vales = @iVales,  
            NoSurtido = @iEsNoSurtido, 
            PorcSurtido = (case when @iTickets > 0 Then (( (@iTickets - (@iVales + @iEsNoSurtido))  / @iTickets ) * 100) else 0 end),          
            -- PorcSurtido = 100 - (( @iVales / @iTickets) * 100.00) ,  
            PorcVales = (case when @iTickets > 0 Then (( @iVales / @iTickets) * 100.00) else 0 end) 
        From #tmpRpt_SurtimientoRecetas F  
        
        
--        Select Top 1 Empresa, Farmacia, Periodo, FechaReporte From #Rpt_NivelDeAbasto (NoLock) 
-------          
------------------------------------------- Generar Resumen General 		
        If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#Rpt_NivelDeAbasto' and xType = 'U' ) 
           Drop Table #Rpt_NivelDeAbasto 
        
        Select 
            @Empresa as Empresa, @EncabezadoReporte as Farmacia, @EncabezadoPeriodo as Periodo, 
            getdate() as FechaReporte, 
            cast(@iTickets as int) as FoliosDeVenta, 
            cast(@iVales as int) as Vales, 
            cast(@iEsNoSurtido as int) as NoSurtido, 
            cast( (( (@iTickets - (@iVales + @iEsNoSurtido))  / @iTickets ) * 100) as numeric(14,4)) as PorcSurtido,    
            cast( (( @iVales / @iTickets) * 100.00) as numeric(14,4)) as PorcVales,  
            cast( (( @iEsNoSurtido / @iTickets) * 100.00) as numeric(14,4)) as PorcNoSurtido, 
            
            0 as ClavesDiferentes,  cast(0 as int) as CantidadTotal,  
            0 as ClavesSurtidas,    cast(0 as int) as CantidadSurtida,    cast(0 as numeric(14,4)) as PorcClavesSurtidas,  
            
            @ClavesPerfil as ClavesPerfil,    cast(0 as numeric(14,4)) as PorcClavesPerfil,              
            
            0 as ClavesVales,       cast(0 as int) as CantidadVale,       cast(0 as numeric(14,4)) as PorcClavesVales, 
            0 as ClavesNoSurtido,   cast(0 as int) as CantidadNoSurtida,  cast(0 as numeric(14,4)) as PorcClavesNoSurtida  
        Into #Rpt_NivelDeAbasto 
        Where @iTickets > 0 
        
        
        
        Update A Set ClavesDiferentes = B.ClavesDiferentes, CantidadTotal = B.CantidadPiezas 
        From #Rpt_NivelDeAbasto A, #tmp_ClavesDispensadas_Resumen B  
        Where Tipo = 0 

        Update A Set ClavesSurtidas = B.ClavesDiferentes, CantidadSurtida = B.CantidadPiezas 
        From #Rpt_NivelDeAbasto A, #tmp_ClavesDispensadas_Resumen B  
        Where Tipo = 1 
        
        Update A Set ClavesVales = B.ClavesDiferentes, CantidadVale = B.CantidadPiezas 
        From #Rpt_NivelDeAbasto A, #tmp_ClavesDispensadas_Resumen B  
        Where Tipo = 2 
        
        Update A Set ClavesNoSurtido = B.ClavesDiferentes, CantidadNoSurtida = B.CantidadPiezas 
        From #Rpt_NivelDeAbasto A, #tmp_ClavesDispensadas_Resumen B  
        Where Tipo = 3                         
        
        Update A Set -- CantidadTotal 
            PorcClavesPerfil = (case when ClavesPerfil > 0 then (( ClavesSurtidas / (ClavesPerfil * 1.00) ) * 100) else 0 end),   
            PorcClavesSurtidas = (case when CantidadTotal > 0 then (( (CantidadSurtida * 1.00) / CantidadTotal ) * 100) else 0 end), 
            PorcClavesVales = (case when CantidadTotal > 0 then (( (CantidadVale * 1.00) / CantidadTotal ) * 100) else 0 end), 
            PorcClavesNoSurtida = (case when CantidadTotal > 0 then (( (CantidadNoSurtida * 1.00) / CantidadTotal ) * 100) else 0 end)
        From #Rpt_NivelDeAbasto A 
        -- Where CantidadTotal > 0         
        
------------------------------------------- Para impresion 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRpt_SurtimientoRecetas' and xType = 'U' )
	    Drop Table tmpRpt_SurtimientoRecetas 

	Select * 
	Into tmpRpt_SurtimientoRecetas 
	From #tmpRpt_SurtimientoRecetas
	
	
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_NivelDeAbasto' and xType = 'U' )
	    Drop Table Rpt_NivelDeAbasto 

	Select * 
	Into Rpt_NivelDeAbasto 
	From #Rpt_NivelDeAbasto 
	
	
    If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_NivelDeAbasto_ClavesDispensadas' and xType = 'U' ) 
       Drop Table Rpt_NivelDeAbasto_ClavesDispensadas    

	Select * 
	Into Rpt_NivelDeAbasto_ClavesDispensadas
	From #Rpt_NivelDeAbasto_ClavesDispensadas 
           			
------------------------------------------- Para impresion 
        
------------------------------------------- Generar Resumen General                
---     spp_Rpt_SurtimientoRecetas         
	If @iEjecutar = 1 
	Begin 
		Select '' as IdEmpresa, Empresa, '' as IdEstado, '' as Estado, '' as IdFarmacia, Farmacia, Periodo, FechaReporte, 
			FoliosDeVenta, Vales, NoSurtido, PorcSurtido, PorcVales, PorcNoSurtido, 
			ClavesDiferentes, CantidadTotal, 
			ClavesSurtidas, CantidadSurtida, PorcClavesSurtidas, 
			ClavesPerfil, PorcClavesPerfil, 
			ClavesVales, CantidadVale, PorcClavesVales, 
			ClavesNoSurtido, CantidadNoSurtida, PorcClavesNoSurtida 
		From #Rpt_NivelDeAbasto (NoLock) 
    End     
        
------------------------------------------- Generar Resumen 


-- IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, FechaRegistro

End
Go--#SQL 
