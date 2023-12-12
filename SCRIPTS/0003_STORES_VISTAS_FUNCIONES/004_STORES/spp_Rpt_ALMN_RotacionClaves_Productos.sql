

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rtp_Rotacion_Claves_Productos' and xType = 'U' ) 
       Drop Table Rtp_Rotacion_Claves_Productos
Go--#SQL

If Exists ( Select Name, crDate From Sysobjects (NoLock) Where Name = 'spp_Rpt_ALMN_RotacionClaves_Productos' and xType = 'P' ) 
   Drop Proc spp_Rpt_ALMN_RotacionClaves_Productos
Go--#SQL  

--	Exec spp_Rpt_ALMN_RotacionClaves_Productos '001', '21', '1247', '2013-07-01', 1  


Create Proc spp_Rpt_ALMN_RotacionClaves_Productos 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', 
    @FechaRevision varchar(10) = '2013-06-14', @TipoRotacion int = 1,    
    @TipoCB tinyint = 0, @DiasAnalisis int = 90,
    @Alto int = 30, @Medio int = 60, @Bajo int = 90         
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
        
        
    Set @Dias_Tope = @DiasAnalisis     
    Set @fFecha_Revision = cast(@FechaRevision as datetime) 

	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
		
--- Drop table #tmpClaves

	Select Distinct ClaveSSA, 0 as EsCuadroBasico  
	Into #tmpPerfil
	From FarmaciaProductos F
	Inner Join vw_Productos P On ( F.IdProducto = P.IdProducto )
	Where F.IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
    
    
---------------------- Generar Perifil de Farmacia 
        ----Select IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave 
        ----Into #tmpPerfil 
        ----From vw_CB_CuadroBasico_Farmacias  
        ----Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
        
        Update P Set EsCuadroBasico = 1
        From #tmpPerfil P
        Inner Join vw_CB_CuadroBasico_Farmacias C 
			On ( IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and P.ClaveSSA = C.ClaveSSA )
---------------------- Generar Perifil de Farmacia 
    
		If @TipoCB = 1
			Delete From #tmpPerfil Where EsCuadroBasico = 0
			
		If @TipoCB = 2
			Delete From #tmpPerfil Where EsCuadroBasico = 1
    
    
---------------------- Obtener datos     
    Select 
        V.IdEmpresa, V.IdEstado, V.IdFarmacia, -- V.FolioVenta, 
        V.FechaReceta, 
        @fFecha_Revision as FechaBase, (datediff(day, V.FechaReceta, @fFecha_Revision) + 1 ) as Dias, @iNulo as Tipo, 
		P.ClaveSSA, D.IdProducto, D.CodigoEAN, P.DescripcionCorta as DescripcionProducto, 
        count(Distinct V.FolioVenta) as Folios, 
        sum(CantidadVendida) as Cantidad  
 --       cas 
    Into #tmpClaves  
    From VentasInformacionAdicional V (NoLock) 
    Inner Join VentasDet D (NoLock) 
        On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
    Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
    Inner Join #tmpPerfil CP (NoLock) On ( CP.ClaveSSA = P.ClaveSSA )	
    Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
          and V.FechaReceta <= @FechaRevision 
          and (datediff(day, V.FechaReceta, @fFecha_Revision) + 1) between @Dias_Alta_Inicio and @Dias_Tope   
    Group by 
        V.IdEmpresa, V.IdEstado, V.IdFarmacia, -- V.FolioVenta, 
        V.FechaReceta, 
        P.ClaveSSA, D.IdProducto, D.CodigoEAN, P.DescripcionCorta          
    Order by V.FechaReceta desc 
    
    
--- Determinar el volumen de Folios     
    Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto, 
           0 as TotalFolios, sum(Folios) as Folios, 
           cast(0 as float) as Porcentaje, 
           cast(0 as float) as Acumulado,             
           sum(Cantidad) as Cantidad, 
           @iNulo as Tipo, 2 as RotacionTipo,  
           identity(int, 1,1 ) as keyx  
    into #tmpFolios        
    From #tmpClaves 
    Group by IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto
    Order by Folios Desc 
    
    Select @iTotalFolios = sum(Folios) From #tmpFolios 
    Update F Set TotalFolios = @iTotalFolios, Porcentaje = (Folios / (@iTotalFolios * 1.00)) * 100 
    From #tmpFolios F 
    
    Update F Set Acumulado = (select sum(Porcentaje) From #tmpFolios X Where X.Keyx <= F.Keyx) 
    From #tmpFolios F      
    
    
--- Determinar el volumen de Salidas 
    Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto, 
           0 as TotalCantidad, sum(Cantidad) as Cantidad, 
           cast(0 as float) as Porcentaje, 
           cast(0 as float) as Acumulado,             
           @iNulo as Tipo, 3 as RotacionTipo,  
           identity(int, 1,1 ) as keyx  
    into #tmpCantidades 
    From #tmpClaves 
    Group by IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto 
    Order by Cantidad Desc     
    
    Select @iTotalFolios = sum(Cantidad) From #tmpCantidades  
    Update F Set TotalCantidad = @iTotalFolios, Porcentaje = (Cantidad / (@iTotalFolios * 1.00)) * 100 
    From #tmpCantidades F 
    
    Update F Set Acumulado = (select sum(Porcentaje) From #tmpCantidades X Where X.Keyx <= F.Keyx) 
    From #tmpCantidades F         
---------------------- Obtener datos         
    
    
---------------------- Determinar tipo de rotacion     
    ----Update C Set Tipo = @iAlto 
    ----From #tmpClaves C  
    ----Where Dias Between @Dias_Alta_Inicio and @Dias_Alta_Fin 
    
    ----Update C Set Tipo = @iMedio 
    ----From #tmpClaves C  
    ----Where Dias Between @Dias_Media_Inicio and @Dias_Media_Fin and Tipo = @iNulo 
    
    ----Update C Set Tipo = @iBajo 
    ----From #tmpClaves C  
    ----Where Dias Between @Dias_Baja_Inicio and @Dias_Baja_Fin and Tipo = @iNulo         
    
/*     
    Set @dAlto = 20 
    Set @dMedio = 40 
    Set @dBajo = 60     
    Set @dNulo = 0    
*/ 
       
    Update C Set Tipo = @iAlto 
    From #tmpFolios C  
    Where Acumulado <= @dAlto 
           
    Update C Set Tipo = @iMedio 
    From #tmpFolios C  
    Where Acumulado > @dAlto and Acumulado <= @dMedio 
           
    Update C Set Tipo = @iBajo 
    From #tmpFolios C  
    Where Acumulado > @dMedio and Acumulado <= @dBajo            
    
    
    Update C Set Tipo = @iAlto 
    From #tmpCantidades C  
    Where Acumulado <= @dAlto 
           
    Update C Set Tipo = @iMedio 
    From #tmpCantidades C  
    Where Acumulado > @dAlto and Acumulado <= @dMedio 
           
    Update C Set Tipo = @iBajo 
    From #tmpCantidades C  
    Where Acumulado > @dMedio and Acumulado <= @dBajo              
    


    If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rtp_Rotacion_Claves_Productos' and xType = 'U' ) 
       Drop Table Rtp_Rotacion_Claves_Productos  

    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 1 as RotacionTipo, 
        IdEmpresa, space(100) as Empresa, 
        IdEstado, space(100) as Estado, 
        IdFarmacia, space(100) as Farmacia, 
        ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto,  
        @iAlto as TipoRotacion, 'ALTA ROTACIÓN' + space(20) as DescripcionRotacion, 
        cast(0 as float) as Porcentaje, cast(0 as float) as Cantidad,         
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    Into Rtp_Rotacion_Claves_Productos 
    From #tmpClaves C (NoLock) 
    Where Tipo = @iAlto 
    
      
    
    Insert Into Rtp_Rotacion_Claves_Productos 
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 1 as RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdFarmacia, '', 
        ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto,  
        @iMedio as TipoRotacion, 'MEDIA ROTACIÓN' as DescripcionRotacion, 
        0, 0, 
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpClaves C (NoLock) 
    Where 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA and C.IdProducto = X.IdProducto and C.CodigoEAN = X.CodigoEAN )    
		And 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA 
					and C.IdProducto = X.IdProducto and C.CodigoEAN = X.CodigoEAN and Tipo = @iMedio )    		


    Insert Into Rtp_Rotacion_Claves_Productos 
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 1 as RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdFarmacia, '', 
        ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto,  
        @iBajo as TipoRotacion, 'BAJA ROTACIÓN' as DescripcionRotacion, 
        0, 0,         
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpClaves C (NoLock) 
    Where 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA and C.IdProducto = X.IdProducto and C.CodigoEAN = X.CodigoEAN )    
		And 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA 
					and C.IdProducto = X.IdProducto and C.CodigoEAN = X.CodigoEAN and Tipo = @iBajo )    		
		
           
    Insert Into Rtp_Rotacion_Claves_Productos            
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 1 as RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdFarmacia, '', 
        ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto, 
        @iNulo as TipoRotacion, 'NULA ROTACIÓN' as DescripcionRotacion, 
        0, 0,         
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpClaves C (NoLock) 
    Where 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA and C.IdProducto = X.IdProducto and C.CodigoEAN = X.CodigoEAN )    
		And 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA 
						and C.IdProducto = X.IdProducto and C.CodigoEAN = X.CodigoEAN and Tipo = @iNulo )    		
           
           
--- Anexar el resto de los tipos procesados 
    Insert Into Rtp_Rotacion_Claves_Productos 
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdFarmacia, '', 
        ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto, 
        Tipo as TipoRotacion, '' as DescripcionRotacion, 
        Porcentaje, Folios,         
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpFolios 
    
    
    Insert Into Rtp_Rotacion_Claves_Productos 
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdFarmacia, '', 
        ClaveSSA, IdProducto, CodigoEAN, DescripcionProducto, 
        Tipo as TipoRotacion, '' as DescripcionRotacion, 
        Porcentaje, Cantidad, 
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpCantidades     
--- Anexar el resto de los tipos procesados                
                          
           
    Update R Set Empresa = E.Nombre
    From Rtp_Rotacion_Claves_Productos R 
    Inner Join CatEmpresas E (NoLock) On ( R.IdEmpresa = E.IdEmpresa ) 

    Update R Set Estado = E.Estado, Farmacia = E.Farmacia  
    From Rtp_Rotacion_Claves_Productos R 
    Inner Join vw_Farmacias E (NoLock) On ( R.IdEstado = E.IdEstado and R.IdFarmacia = E.IdFarmacia ) 	
    
    Update R Set DescripcionRotacion = C.DescripcionRotacion 
    From Rtp_Rotacion_Claves_Productos R     
    Inner Join #tmpRotaciones C On ( R.TipoRotacion = C.RotacionTipo )
    
---------------------- Determinar tipo de rotacion Parte 002 


------------ SALIDA FINAL 
	Select  
	--	'Núm. Farmacia' = IdFarmacia, Farmacia,  
	'Producto' = IdProducto, 'Código EAN' = CodigoEAN, 'Clave SSA' = ClaveSSA, 'Descripción Producto' = DescripcionProducto, 
	'Descripción Rotación' = DescripcionRotacion 
    From Rtp_Rotacion_Claves_Productos (NoLock) 
    Where RotacionTipo = @TipoRotacion 
		  -- and IdFarmacia = '1005' and ClaveSSA = '871' 
    Order by IdFarmacia, TipoRotacion, DescripcionRotacion 
------------ SALIDA FINAL 

End 
Go--#SQL     
