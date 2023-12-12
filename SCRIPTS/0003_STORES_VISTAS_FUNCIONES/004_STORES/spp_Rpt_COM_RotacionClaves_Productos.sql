If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#Rtp_Rotacion_Claves_Productos' and xType = 'U' ) 
       Drop Table #Rtp_Rotacion_Claves_Productos
Go--#SQL

----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name, crDate From Sysobjects (NoLock) Where Name = 'spp_Rpt_COM_RotacionClaves_Productos' and xType = 'P' ) 
   Drop Proc spp_Rpt_COM_RotacionClaves_Productos  
Go--#SQL  

--	Exec spp_Rpt_COM_RotacionClaves_Productos '001', '21', '1247', '2013-07-01', 0, 0   


Create Proc spp_Rpt_COM_RotacionClaves_Productos 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
    @IdFarmacia varchar(4) = '0003', 
    @FechaInicial varchar(10) = '2013-07-01', @FechaFinal varchar(10) = '2013-11-30', 
    @TipoInsumo tinyint = 2, 
    @TipoCB tinyint = 0, 
    @Alto int = 40, @Medio int = 85, @Bajo int = 95         
) 
With Encryption 
As 
Begin 
Set NoCount Off  
Set DateFormat YMD 

Declare 
    @iAlto int, 
    @iMedio int, 
    @iBajo int,     
    @iNulo int, 
    @iTotalFolios int, @Dias_Alta_Inicio int 

Declare 
    @dAlto int, 
    @dMedio int, 
    @dBajo int,     
    @dNulo int 


Declare @EncPrincipal varchar(500), 
		@EncSecundario varchar(500),
		@Status varchar(2),
		@TipoUnidad varchar(3)	 

Declare 
    @Dias_Tope int, 
    @fFecha_Revision datetime  
    
    
    Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

	Set @Status = 'A'
	Set @TipoUnidad = '006'	
	    
	Set @Dias_Alta_Inicio = 1
    Set @iAlto = 1 
    Set @iMedio = 2 
    Set @iBajo = 3     
    Set @iNulo = 4      
        
    Set @dAlto = @Alto		--30 
    Set @dMedio = @Medio	--60 
    Set @dBajo = @Bajo		--90     
    Set @dNulo = 0              
        
/*         
    Select @iAlto as RotacionTipo, 'ALTA ROTACIÓN' + space(20) as DescripcionRotacion      
    Into #tmpRotaciones 
    
    Insert Into #tmpRotaciones Select @iMedio, 'MEDIA ROTACIÓN'  
    Insert Into #tmpRotaciones Select @iBajo, 'BAJA ROTACIÓN'  
    Insert Into #tmpRotaciones Select @iNulo, 'NULA ROTACIÓN'          
*/         

    Select top 0 0 as RotacionTipo, 'ALTA' + space(20) as DescripcionRotacion      
    Into #tmpRotaciones 
    
    
    Insert Into #tmpRotaciones Select @iAlto, 'ALTA'      
    Insert Into #tmpRotaciones Select @iMedio, 'MEDIA'  
    Insert Into #tmpRotaciones Select @iBajo, 'BAJA'  
    Insert Into #tmpRotaciones Select @iNulo, 'NULA'   
        
        
    --Set @Dias_Tope = @DiasAnalisis     
    --Set @fFecha_Revision = cast(@FechaRevision as datetime) 


	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
		
--- Drop table #tmpClaves

	Select Distinct ClaveSSA, DescripcionClave, 0 as EsCuadroBasico, 
		(case when cast(IdTipoProducto as int) = 2 Then 1 Else 0 End ) as EsMedicamento, TipoDeProducto as Tipo   
	Into #tmpPerfil
	From FarmaciaProductos F
	Inner Join vw_Productos P On ( F.IdProducto = P.IdProducto )
	Where F.IdEmpresa = @IdEmpresa and IdEstado = @IdEstado -- and IdFarmacia = @IdFarmacia
    
    
---------------------- Generar Perifil de Farmacia 
	        
    Update P Set EsCuadroBasico = 1
    From #tmpPerfil P 
    Inner Join vw_CB_CuadroBasico_Farmacias C 
		On ( IdEstado = @IdEstado and P.ClaveSSA = C.ClaveSSA )
---------------------- Generar Perifil de Farmacia 
    
    --- select * from #tmpPerfil 
    
	If @TipoCB = 1
		Delete From #tmpPerfil Where EsCuadroBasico = 0 
		
	If @TipoCB = 2
		Delete From #tmpPerfil Where EsCuadroBasico = 1
    
     
   	If @TipoInsumo = 1
		Delete From #tmpPerfil Where EsMedicamento = 0 
		
	If @TipoInsumo = 2
		Delete From #tmpPerfil Where EsMedicamento = 1  
 

      
---------------------- Obtener datos     
    Select 
		V.IdEmpresa, V.IdEstado, -- V.FolioVenta,  		
		P.ClaveSSA, P.DescripcionClave, CP.Tipo as TipoInsumo,  
		sum(ceiling(CantidadVendida /(P.ContenidoPaquete * 1.0))) as Cantidad,   
		cast(0 as float) as Porcentaje, 
		cast(0 as float) as Acumulado,             
		@iNulo as Tipo, 0 as RotacionTipo,  
		identity(int, 1,1 ) as keyx          
    Into #tmpClaves  
    From VentasEnc V (NoLock) 
    Inner Join VentasDet D (NoLock) 
        On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
    Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
    Inner Join #tmpPerfil CP (NoLock) On ( CP.ClaveSSA = P.ClaveSSA )	
    Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdFarmacia 
          and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal  
    Group by 
        V.IdEmpresa, V.IdEstado, -- V.IdFarmacia, -- V.FolioVenta, 
        -- V.FechaRegistro, 
        P.ClaveSSA, P.DescripcionClave, CP.Tipo           
    -- Order by V.FechaRegistro desc 
    
    Select @iTotalFolios = sum(Cantidad) From #tmpClaves       
    Update F Set Porcentaje = (Cantidad / (@iTotalFolios * 1.00)) * 100 
    From #tmpClaves F 
        
---	    spp_Rpt_COM_RotacionClaves_Productos		
    
    
--- Ordernar por porcentajes  
    Select IdEmpresa, IdEstado, ClaveSSA, DescripcionClave, TipoInsumo, 
           0 as TotalCantidad, sum(Cantidad) as Cantidad, 
           Porcentaje, 
           cast(0 as float) as Acumulado,             
           @iNulo as Tipo, 0 as RotacionTipo,  
           identity(int, 1,1 ) as keyx  
    into #tmpCantidades 
    From #tmpClaves 
    Group by IdEmpresa, IdEstado, ClaveSSA, DescripcionClave, TipoInsumo, Porcentaje  
    Order by Porcentaje Desc     
   
    
    Update F Set TotalCantidad = @iTotalFolios, Acumulado = (select sum(Porcentaje) From #tmpCantidades X Where X.Keyx <= F.Keyx) 
    From #tmpCantidades F         
---------------------- Obtener datos         
 


    
---------------------------------------------------     
-------------------- Determinar tipo de rotacion     
    Update C Set Tipo = @iAlto 
    From #tmpCantidades C  
    Where Acumulado <= @dAlto 
           
    Update C Set Tipo = @iMedio 
    From #tmpCantidades C  
    Where Acumulado > @dAlto and Acumulado <= @dMedio 
           
    Update C Set Tipo = @iBajo 
    From #tmpCantidades C  
    Where Acumulado > @dMedio and Acumulado <= @dBajo              
 

    
    select * 
    from #tmpCantidades     
    
    
    
/* 
------------------------------------------------------------ 
    If Exists ( Select Name From tempdb..Sysobjects (NoLock) Where Name like '#Rtp_Rotacion_Claves_Productos%' and xType = 'U' ) 

------------ SALIDA FINAL 
	Select  
	--	'Núm. Farmacia' = IdFarmacia, Farmacia,  
	'Producto' = IdProducto, 'Codigo EAN' = CodigoEAN, 'Clave SSA' = ClaveSSA, 'Descripción Producto' = DescripcionClave, 
	-- Porcentaje, 
	'Descripción Rotación' = DescripcionRotacion 
    From #Rtp_Rotacion_Claves_Productos (NoLock) 
    Where RotacionTipo = @TipoRotacion 
		  -- and IdFarmacia = '1005' and ClaveSSA = '871' 
    Order by IdFarmacia, TipoRotacion, DescripcionRotacion 
------------ SALIDA FINAL 
*/ 


End 
Go--#SQL     
