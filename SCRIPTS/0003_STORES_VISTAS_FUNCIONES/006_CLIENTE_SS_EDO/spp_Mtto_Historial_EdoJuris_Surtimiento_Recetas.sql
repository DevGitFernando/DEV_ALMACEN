	    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Historial_EdoJuris_Surtimiento_Recetas' and xType = 'P') 
    Drop Proc spp_Mtto_Historial_EdoJuris_Surtimiento_Recetas 
Go--#SQL 
  
--  Exec spp_Mtto_Historial_EdoJuris_Surtimiento_Recetas '21', '0188', '0002', '0005', '2011-10-10', '2011-10-10'  
  
Create Proc spp_Mtto_Historial_EdoJuris_Surtimiento_Recetas 
(   
    @IdEstado varchar(2) = '21', @IdJurisdiccion varchar(4) = '001', 
    @IdCliente varchar(4) = '0002', @FechaRegistro varchar(10) = '2012-08-23'
	-- @FechaInicial varchar(10) = '2011-10-01', @FechaFinal varchar(10) = '2011-10-19' 
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
		@Claves numeric(14,4), @Abasto numeric(14,4), -- @ClavesPerfil int, 
		@PorcAbasto numeric(14,4),  
		@iTickets numeric(14,4), @iVales numeric(14,4), 
		@iEsCapturaCompleta numeric(14,4), @iEsNoSurtido numeric(14,4)  
	 
	 
		Declare @sFechaMinima datetime 

		----------------------------
		-- Encabezado de reportes --
		----------------------------
		Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

------- Datos temporales 
		Select * 
		into #vw_Productos_CodigoEAN
		From vw_Productos_CodigoEAN 
------- Datos temporales 

		-------------------------------------------
		-- Obtener lista de Farmacias a Procesas --
		------------------------------------------- 
		Select IdEstado, @IdEstado as Estado, IdJurisdiccion, Descripcion as Jurisdiccion 
		Into #tmpJuris 
		From CatJurisdicciones 
		Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion 

		if @IdJurisdiccion = '*' 
			Begin 
				Insert Into #tmpJuris 
				Select IdEstado, @IdEstado as Estado, IdJurisdiccion as IdJuris, Descripcion as Jurisdiccion 
				From CatJurisdicciones 
				Where IdEstado = @IdEstado -- and IdJurisdiccion = @IdJurisdiccion 	   
			End 


		-- Lista de Farmacias 		
		Select F.IdEstado, F.IdJurisdiccion, J.Jurisdiccion, F.IdFarmacia 
		Into #tmpFarmacias 
		From CatFarmacias F (NoLock) 
		Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 
--		Where IdFarmacia = 188 

----		If @IdFarmacia <> '*' 
----		   Delete From #tmpFarmacias Where IdEstado = @IdEstado and IdFarmacia <> @IdFarmacia


		-------------------------------- 
		-- Generar Perfil de Farmacia -- 
		-------------------------------- 
        Select F.IdEstado, F.Estado, J.IdJurisdiccion, J.Jurisdiccion, F.IdFarmacia, F.Farmacia, F.IdCliente, F.IdClaveSSA, F.ClaveSSA, F.DescripcionClave, 0 as ClavesPerfil
        Into #tmpPerfil 
        From vw_CB_CuadroBasico_Farmacias F(NoLock)
		Inner Join #tmpFarmacias J(NoLock) On ( F.IdEstado = J.IdEstado And F.IdFarmacia = J.IdFarmacia ) 
        Where F.IdEstado = @IdEstado -- and IdFarmacia = @IdFarmacia

		Update P Set ClavesPerfil = ( Select Count(*) From #tmpPerfil T(NoLock) Where P.IdEstado = T.IdEstado And P.IdFarmacia = T.IdFarmacia ) 
 		From #tmpPerfil P(NoLock)

        -- Select @ClavesPerfil = count(*) From #tmpPerfil 

	
		-----------------------
		-- Datos Principales --
		-----------------------
		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRpt_EdoJuris_SurtimientoRecetas' and xType = 'U' )
		    Drop Table #tmpRpt_EdoJuris_SurtimientoRecetas 
		   
	    Select @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
	        V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, V.Farmacia, F.IdJurisdiccion, F.Jurisdiccion, 
			V.Folio as FolioVenta, V.FechaRegistro, 
	        space(20) as FolioVale, 0 as TieneVale, 0 as EsNoSurtido,  
	        0 as Tickets, 0 as Vales, 0 as NoSurtido, 
	        cast(0 as numeric(14,4)) as PorcSurtido, 
	        cast(0 as numeric(14,4)) as PorcVales, 
	        
	        -- min(FechaRegistro) as FechaInicial, max(FechaRegistro) as FechaFinal  
	        getdate() as FechaInicial, getdate() as FechaFinal 
	    Into #tmpRpt_EdoJuris_SurtimientoRecetas 
	    From vw_VentasEnc V(NoLock) 
		Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )
	    Where V.IdEstado = @IdEstado -- and IdFarmacia = @IdFarmacia -- and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
	          and convert(varchar(10), FechaRegistro, 120) = @FechaRegistro -- Between @FechaInicial and @FechaFinal 
	          and TipoDeVenta =  2 
        Order by IdEmpresa, IdEstado, IdFarmacia, Folio 

		-------------------------------------
		--  Obtener Encabezados de reporte --
		-------------------------------------
        Set @EncabezadoPeriodo = @FechaRegistro -- @FechaInicial + ' AL ' + @FechaFinal  
        Select top 1 @Empresa = Empresa -- , @EncabezadoReporte = IdFarmacia + '-' + Farmacia  
        From #tmpRpt_EdoJuris_SurtimientoRecetas (NoLock) 

		If @IdJurisdiccion = '*' 
		  Begin
			Set @EncabezadoReporte = 'TODAS LAS JURISDICCIONES'
		  End
		Else
		  Begin
			Select top 1 @EncabezadoReporte = IdJurisdiccion + '-' + Jurisdiccion  
			From #tmpRpt_EdoJuris_SurtimientoRecetas (NoLock) 
		  End

		---------------------
		-- Claves Surtidas --
		---------------------
		---   Drop Table tmpClaves_Validar 
        Select CD.IdEmpresa, CD.IdEstado, CD.IdFarmacia, F.IdJurisdiccion, F.Jurisdiccion, CD.FolioVenta, 
            CD.IdClaveSSA, S.ClaveSSA, S.DescripcionClave, 
            CD.CantidadRequerida,  
            ( case when (CD.CantidadRequerida = 0) Then IsNull(C.CantidadSurtida, 0) else CD.CantidadRequerida end ) as CantidadRequeridaAux, 
            IsNull(C.CantidadSurtida, 0) as CantidadSurtida, 
            IsNull(C.CantidadDevuelta, 0) as CantidadDevuelta                
        into #tmpClaves_Validar      
        From VentasEstadisticaClavesDispensadas CD (NoLock) 
        Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas L (NoLock) 
            On ( CD.IdEmpresa = L.IdEmpresa and CD.IdEstado = L.IdEstado and CD.IdFarmacia = L.IdFarmacia and CD.FolioVenta = L.FolioVenta )     
        Inner Join vw_ClavesSSA_Sales S (NoLock) On ( CD.IdClaveSSA = S.IdClaveSSA_Sal ) 
        Left Join 
        ( 
            Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioVenta, 
                P.IdClaveSSA_Sal, 
                sum(D.CantidadVendida) as CantidadSurtida, 
                sum(D.Cant_Devuelta) as  CantidadDevuelta  
            From VentasDet D 
            Inner Join #vw_Productos_CodigoEAN  P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
            Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas L (NoLock) 
                    On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta ) 
            -- Where D.FolioVenta between 00010027 and 00010027 
            Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioVenta, P.IdClaveSSA_Sal   
        ) as C      
            On ( C.IdEmpresa = CD.IdEmpresa and C.IdEstado = CD.IdEstado and C.IdFarmacia = CD.IdFarmacia 
                 and C.FolioVenta = CD.FolioVenta and C.IdClaveSSA_Sal = CD.IdClaveSSA  )    
		Inner Join #tmpFarmacias F (NoLock) On ( CD.IdEstado = F.IdEstado and CD.IdFarmacia = F.IdFarmacia )
			
		--------------------------
		-- Totalizar las Claves --
		--------------------------
		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpClaves_Cantidades' and xType = 'U' )
		    Drop Table #tmpClaves_Cantidades 
		    
        Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioVenta, 
             C.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, C.CantidadRequerida, 
             0 as TieneVale, 0 as EsNoSurtido
        Into #tmpClaves_Cantidades      
        From #tmpClaves_Validar C (noLock)         
        
        Delete From #tmpClaves_Validar Where CantidadSurtida <> 0 
        Delete From #tmpClaves_Validar Where CantidadDevuelta <> 0 

		---------------------
		-- Claves Surtidas --
		---------------------    

        Update F Set FolioVale = V.FolioVale, TieneVale = 1 
        From #tmpRpt_EdoJuris_SurtimientoRecetas F  
        Inner Join Vales_EmisionEnc V (NoLock) 
            On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta ) 

---		select count(*) from #tmpRpt_EdoJuris_SurtimientoRecetas where TieneVale = 1  

---	spp_Mtto_Historial_EdoJuris_Surtimiento_Recetas

        Update F Set EsNoSurtido = 1 
        From #tmpRpt_EdoJuris_SurtimientoRecetas F  
        Inner Join #tmpClaves_Validar V (NoLock) 
            On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta ) 
        Inner Join #tmpPerfil CB (NoLock) 
            On ( CB.IdEstado = @IdEstado and CB.IdCliente = @IdCliente and CB.IdFarmacia = V.IdFarmacia and CB.IdClaveSSA = V.IdClaveSSA )     
        Where -- V.CantidadSurtida = 0 -- and V.CantidadEntregada = 0 
              -- and 
              TieneVale = 0         
        
		------------
		-- FECHAS --
		------------
        Update F Set 
			FechaInicial = (select min(FechaRegistro) from #tmpRpt_EdoJuris_SurtimientoRecetas), 
			FechaFinal = (select max(FechaRegistro) from #tmpRpt_EdoJuris_SurtimientoRecetas)  
        From #tmpRpt_EdoJuris_SurtimientoRecetas F  

		----------------------------
		-- Separar la informacion --
		----------------------------
        Update F Set TieneVale = V.TieneVale, EsNoSurtido = V.EsNoSurtido
        From #tmpClaves_Cantidades F 
        Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas V (NoLock) 
            On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta )  
            

		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRpt_EdoJuris_SurtimientoRecetas_ClavesCantidades_Aux' and xType = 'U' )
		    Drop Table #tmpRpt_EdoJuris_SurtimientoRecetas_ClavesCantidades_Aux 
		    
        Select IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, DescripcionClave, 
            sum(Cantidad_Vales) as Cantidad_Vales, 
            sum(Cantidad_NoSurtido) as Cantidad_NoSurtido, 
            sum(CantidadRequerida - (Cantidad_NoSurtido + Cantidad_Vales)) as Cantidad_Dispensada,         
            sum(CantidadRequerida) as CantidadRequerida 
        Into #tmpRpt_EdoJuris_SurtimientoRecetas_ClavesCantidades_Aux     
        From 
        ( 
            Select IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, DescripcionClave,  TieneVale, EsNoSurtido, 
                 (case when sum(TieneVale) >= 1 then sum(CantidadRequerida) else 0 end) as Cantidad_Vales, 
                 (case when sum(EsNoSurtido) >= 1 then sum(CantidadRequerida) else 0 end) as Cantidad_NoSurtido,              
                 sum(CantidadRequerida) as CantidadRequerida 
            From #tmpClaves_Cantidades 
            group by IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, DescripcionClave, TieneVale, EsNoSurtido 

        ) as T   
        Group by IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, DescripcionClave 
            
        ---------------------------------------------
		-- Excepcion por emision de vales manuales --
		---------------------------------------------
        Select @sFechaMinima = cast(convert(varchar(10), min(FechaRegistro), 120) as datetime) 
               From Vales_EmisionEnc (NoLock) 
               Where IdEstado = @IdEstado -- and IdFarmacia = @IdFarmacia 


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
        ) as T 
        Where (FechaVale > FechaReceta) and (FechaReceta > @sFechaMinima)  
    
        Update F Set FolioVale = '', TieneVale = 0  
        From #tmpRpt_EdoJuris_SurtimientoRecetas F  
        Inner Join #tmpExcepcionVales V (NoLock) 
            On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta )
        
		-------------------------
		-- Contabilizar Claves --
		------------------------- 
        If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRpt_EdoJuris_NivelDeAbasto_ClavesDispensadas' and xType = 'U' ) 
           Drop Table #tmpRpt_EdoJuris_NivelDeAbasto_ClavesDispensadas         

		-- Contabilizar Claves Dispensadas de Primera Vez 
        Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, -- D.FolioVenta, 
            P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, 
            sum(D.CantidadVendida) as CantidadSurtida, -- sum
            -- sum(D.Cant_Devuelta) as  CantidadDevuelta,  
            1 as Tipo  
        into #tmpRpt_EdoJuris_NivelDeAbasto_ClavesDispensadas 
        From VentasDet D(NoLock) 
        Inner Join #vw_Productos_CodigoEAN  P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
        Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas L (NoLock) 
                On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta ) 
        Where TieneVale = 0 and EsNoSurtido = 0 
        Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave      

		-- Contabilizar Claves Dispensadas por Vale 
        Insert Into #tmpRpt_EdoJuris_NivelDeAbasto_ClavesDispensadas 
        Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, -- D.FolioVenta, 
            P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, 
            sum(V.Cantidad) as CantidadSurtida, -- sum
            -- 0 as  CantidadDevuelta, 
            2 as Tipo    
        From Vales_EmisionDet V (NoLock) 
        Inner Join Vales_EmisionEnc D (NoLock) 
                On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVale = D.FolioVale )         
        Inner Join vw_ClavesSSA_Sales P On ( V.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) 
        Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas L (NoLock) 
                On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta ) 
        Where TieneVale = 1 and EsNoSurtido = 0 
        Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave 
        Having sum(V.Cantidad) > 0 

		-- Contabilizar Claves Dispensadas No Surtido 
        Insert Into #tmpRpt_EdoJuris_NivelDeAbasto_ClavesDispensadas 
        Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, -- D.FolioVenta, 
            P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, 
            round(sum(D.CantidadEntregada), 0) as CantidadSurtida, -- sum
            -- 0 as  CantidadDevuelta, 
            3 as Tipo    
        From VentasEstadisticaClavesDispensadas D (NoLock) 
        Inner Join vw_ClavesSSA_Sales P On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
        Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas L (NoLock) 
                On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta ) 
        Where TieneVale = 0 and EsNoSurtido = 1 and EsCapturada = 0 
        Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave 
        Having round(sum(D.CantidadEntregada), 0) > 0 

		-- Contabilizar Claves Dispensadas 
        Select IdEmpresa, IdEstado, IdFarmacia, count(distinct IdClaveSSA) as ClavesDiferentes, sum(CantidadSurtida) as CantidadPiezas, 0 as Tipo  
        Into #tmp_ClavesDispensadas_Resumen      
        From #tmpRpt_EdoJuris_NivelDeAbasto_ClavesDispensadas(NoLock)
		Group By IdEmpresa, IdEstado, IdFarmacia      
        
        Insert Into #tmp_ClavesDispensadas_Resumen              
        Select IdEmpresa, IdEstado, IdFarmacia, count(distinct IdClaveSSA) as ClavesDiferentes, sum(CantidadSurtida) as CantidadPiezas, Tipo  
        From #tmpRpt_EdoJuris_NivelDeAbasto_ClavesDispensadas                 
        -- Where Tipo = 1 
        Group by IdEmpresa, IdEstado, IdFarmacia, Tipo 

        ---------------------
		-- Generar Resumen --
		---------------------
		Update P Set Tickets = ( Select Count(*) From #tmpRpt_EdoJuris_SurtimientoRecetas T (Nolock) Where P.IdEstado = T.IdEstado And P.IdFarmacia = T.IdFarmacia ) From #tmpRpt_EdoJuris_SurtimientoRecetas P(NoLock) 
		Update P Set Vales = ( Select Count(*) From #tmpRpt_EdoJuris_SurtimientoRecetas T (Nolock) Where P.IdEstado = T.IdEstado And P.IdFarmacia = T.IdFarmacia And T.TieneVale =  1 ) From #tmpRpt_EdoJuris_SurtimientoRecetas P(NoLock) 
		Update P Set NoSurtido = ( Select Count(*) From #tmpRpt_EdoJuris_SurtimientoRecetas T (Nolock) Where P.IdEstado = T.IdEstado And P.IdFarmacia = T.IdFarmacia And T.EsNoSurtido =  1 ) From #tmpRpt_EdoJuris_SurtimientoRecetas P(NoLock) 

		Update F 
		Set PorcSurtido = (( ( Cast( Tickets as Numeric(14,4) )  - Vales)  / Tickets ) * 100),          
            -- PorcSurtido = 100 - (( @iVales / @iTickets) * 100.00) ,  
            PorcVales = (( Cast( Vales as Numeric(14,4) ) / Tickets) * 100.00) 
        From #tmpRpt_EdoJuris_SurtimientoRecetas F  
		
------- Revision 
--		Select * From #tmpRpt_EdoJuris_SurtimientoRecetas T (Nolock) Where T.EsNoSurtido =  1 				
		
		
		-----------------------------          
		-- Generar Resumen General --
		-----------------------------
        If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRpt_EdoJuris_NivelDeAbasto' and xType = 'U' ) 
           Drop Table #tmpRpt_EdoJuris_NivelDeAbasto          
        
        Select 
            @Empresa as Empresa, 
            -- @EncabezadoReporte as Jurisdiccion, 
            -- @EncabezadoPeriodo as Periodo, 
            IdEstado, Estado, 
            IdJurisdiccion, Jurisdiccion, 
            IdFarmacia, Farmacia, 
            getdate() as FechaReporte, 
            cast(Tickets as int) as FoliosDeVenta, Vales, NoSurtido, Cast(0.0000 as numeric(14,4)) as PorcSurtido, 
			Cast(0.0000 as numeric(14,4)) as PorcVales, Cast(0.0000 as numeric(14,4)) as PorcNoSurtido,             
            0 as ClavesDiferentes,  cast(0 as int) as CantidadTotal,  
            0 as ClavesSurtidas,    cast(0 as int) as CantidadSurtida,    cast(0 as numeric(14,4)) as PorcClavesSurtidas,              
            -- @ClavesPerfil as ClavesPerfil,    
			0 as ClavesPerfil, 
			cast(0 as numeric(14,4)) as PorcClavesPerfil,             
            0 as ClavesVales,       cast(0 as int) as CantidadVale,       cast(0 as numeric(14,4)) as PorcClavesVales, 
            0 as ClavesNoSurtido,   cast(0 as int) as CantidadNoSurtida,  cast(0 as numeric(14,4)) as PorcClavesNoSurtida  
        Into #tmpRpt_EdoJuris_NivelDeAbasto 
		From #tmpRpt_EdoJuris_SurtimientoRecetas (NoLock)
		Group By IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Tickets, Vales, NoSurtido
        
--        select top 1 *  from #tmpRpt_EdoJuris_SurtimientoRecetas 
        
		----------------------------------
		-- Se Actualizan las Cantidades --
		----------------------------------

		Update P Set ClavesPerfil = T.ClavesPerfil
		From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock) 
		Inner Join #tmpPerfil T (NoLock) On ( P.IdFarmacia = T.IdFarmacia )

		Update P Set PorcSurtido = cast( (( ( Cast(T.Tickets as Numeric(14,4))  - (T.Vales + T.NoSurtido))  / T.Tickets ) * 100) as numeric(14,4))
		From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock) 
		Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas T (NoLock) On ( P.IdFarmacia = T.IdFarmacia )

		Update P Set PorcVales = cast( (( Cast( T.Vales as Numeric(14,4) ) / T.Tickets) * 100.00) as numeric(14,4))
		From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock) 
		Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas T (NoLock) On ( P.IdFarmacia = T.IdFarmacia )
		
		Update P Set PorcNoSurtido = cast( (( Cast( T.NoSurtido as Numeric(14,4) ) / T.Tickets) * 100.00) as numeric(14,4))
		From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock) 
		Inner Join #tmpRpt_EdoJuris_SurtimientoRecetas T (NoLock) On ( P.IdFarmacia = T.IdFarmacia )
		
		Update P Set ClavesDiferentes = T.ClavesDiferentes, CantidadTotal = T.CantidadPiezas 
        From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock)
		Inner Join #tmp_ClavesDispensadas_Resumen T(NoLock) On ( P.IdFarmacia = T.IdFarmacia ) 
        Where Tipo = 0 

		Update P Set ClavesSurtidas = T.ClavesDiferentes, CantidadSurtida = T.CantidadPiezas 
        From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock)
		Inner Join #tmp_ClavesDispensadas_Resumen T(NoLock) On ( P.IdFarmacia = T.IdFarmacia ) 
        Where Tipo = 1 

		Update P Set ClavesVales = T.ClavesDiferentes, CantidadVale = T.CantidadPiezas 
        From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock)
		Inner Join #tmp_ClavesDispensadas_Resumen T(NoLock) On ( P.IdFarmacia = T.IdFarmacia ) 
        Where Tipo = 2

		Update P Set ClavesNoSurtido = T.ClavesDiferentes, CantidadNoSurtida = T.CantidadPiezas 
        From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock)
		Inner Join #tmp_ClavesDispensadas_Resumen T(NoLock) On ( P.IdFarmacia = T.IdFarmacia ) 
        Where Tipo = 3 

		-- Cantidades Totales
		Update P Set PorcClavesPerfil = (( ClavesSurtidas / (ClavesPerfil * 1.00) ) * 100) From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock) Where ClavesPerfil > 0 
		Update P Set PorcClavesSurtidas = (( (CantidadSurtida * 1.00) / CantidadTotal ) * 100) From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock) Where CantidadTotal > 0 
		Update P Set PorcClavesVales = (( (CantidadVale * 1.00) / CantidadTotal ) * 100) From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock) Where CantidadTotal > 0
        Update P Set PorcClavesNoSurtida = (( (CantidadNoSurtida * 1.00) / CantidadTotal ) * 100) From #tmpRpt_EdoJuris_NivelDeAbasto P(NoLock) Where CantidadTotal > 0

		-- Select * From #tmpRpt_EdoJuris_NivelDeAbasto(NoLock)

		-- Exec spp_Mtto_Historial_EdoJuris_Surtimiento_Recetas


------------------------------------------------
--- Se Inserta en el Historial de Surtimiento -- 
------------------------------------------------ 
	Update H Set Actualizado = 5 
	From Historial_EdoJuris_Surtimiento_Recetas H (NoLock) 
	Inner Join #tmpRpt_EdoJuris_NivelDeAbasto D (NoLock) 
		On ( H.IdEstado = D.IdEstado and H.IdFarmacia = D.IdFarmacia 
			 and convert(varchar(10), H.FechaRegistro, 120) = @FechaRegistro )

	Delete From Historial_EdoJuris_Surtimiento_Recetas Where Actualizado = 5  
	
	

	Insert Into Historial_EdoJuris_Surtimiento_Recetas 
	Select @IdEstado, 
		'Núm. Juris' = IdJurisdiccion, Jurisdiccion, 'Núm. Unidad' = IdFarmacia, 'Unidad' = Farmacia, 
		@FechaRegistro,
		FoliosDeVenta as Recetas, 'Recetas completas' = PorcSurtido, 
		Vales, 'Porcentaje vales' = PorcVales, 
		'No surtido' = NoSurtido, 'Porcentaje no surtido' = PorcNoSurtido, 'A' as Status, 0 as Actualizado
	From #tmpRpt_EdoJuris_NivelDeAbasto 
	Order by IdJurisdiccion, IdFarmacia 

-- Select * From Historial_EdoJuris_Surtimiento_Recetas

End
Go--#SQL 
