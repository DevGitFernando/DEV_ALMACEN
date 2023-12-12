
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rtp_CteReg_TiposRotacion_Claves' and xType = 'U' ) 
       Drop Table Rtp_CteReg_TiposRotacion_Claves
Go--#SQL

If Exists ( Select Name, crDate From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_RotacionClaves' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_RotacionClaves
Go--#SQL  

--	Exec spp_Rpt_CteReg_RotacionClaves '001', '21', '006', '0188', '2012-01-01', 1  


--	Exec spp_Rpt_CteReg_RotacionClaves '', '21', '001', '1005', '2012-11-15', '1' 

Create Proc spp_Rpt_CteReg_RotacionClaves 
( 
    @IdEmpresa varchar(3) = '001', 
    @IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', @IdFarmacia varchar(4) = '*', 
    @FechaRevision varchar(10) = '2012-05-01', 
    @TipoRotacion int = 1, 
    @Dias_Alta_Inicio   int =   1,      @Dias_Alta_Fin int  =   7, 
    @Dias_Media_Inicio  int =   8,      @Dias_Media_Fin int =  30, 
    @Dias_Baja_Inicio   int =  31,      @Dias_Baja_Fin int  =  90          
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
    @iTotalFolios int 

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
	    
    Set @iAlto = 1 
    Set @iMedio = 2 
    Set @iBajo = 3     
    Set @iNulo = 4      
        
    Set @dAlto = 30 
    Set @dMedio = 60 
    Set @dBajo = 90     
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
        
        
    Set @Dias_Tope = @Dias_Baja_Fin     
    Set @fFecha_Revision = cast(@FechaRevision as datetime) 

	---------- Obtener lista de Farmacias a Procesar  
	Select * 
	into #vw_Farmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado 
	
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 
	
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
		

	Select IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia 
	Into #tmpFar 
	From #vw_Farmacias (NoLock) 
	Where IdEstado = @IdEstado and IdJurisdiccion In ( Select IdJurisdiccion From #tmpJuris (Nolock) Where IdEstado = @IdEstado )
	and IdFarmacia = @IdFarmacia and Status = @Status and IdTipoUnidad <> @TipoUnidad

	if @IdFarmacia = '*'
		Begin
			Insert Into #tmpFar 
			Select IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia 
			From #vw_Farmacias (NoLock) 
			Where IdEstado = @IdEstado and IdJurisdiccion In ( Select IdJurisdiccion From #tmpJuris (Nolock) Where IdEstado = @IdEstado )
			and Status = @Status and IdTipoUnidad <> @TipoUnidad
		End
--------------------------------------------------------------------------------------------------------------------------------------------------

--- Drop table #tmpClaves
    
---------------------- Generar Perifil de Farmacia 
        Select IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave 
        Into #tmpPerfil 
        From vw_CB_CuadroBasico_Farmacias  
        Where IdEstado = @IdEstado and IdFarmacia In (Select IdFarmacia From #tmpFar (Nolock) Where IdEstado = @IdEstado)--and IdFarmacia = @IdFarmacia
---------------------- Generar Perifil de Farmacia 
    
    
    
---------------------- Obtener datos     
    Select 
        V.IdEmpresa, V.IdEstado, F.IdJurisdiccion, V.IdFarmacia, -- V.FolioVenta, 
        V.FechaReceta, 
        @fFecha_Revision as FechaBase, (datediff(day, V.FechaReceta, @fFecha_Revision) + 1 ) as Dias, @iNulo as Tipo, 
        -- D.IdProducto, D.CodigoEAN, 
        P.ClaveSSA, P.DescripcionClave, 
        count(Distinct V.FolioVenta) as Folios, 
        sum(CantidadVendida) as Cantidad  
 --       cas 
    Into #tmpClaves  
    From VentasInformacionAdicional V (NoLock) 
    Inner Join VentasDet D (NoLock) 
        On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
    Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
    Inner Join #tmpPerfil CP (NoLock) On ( CP.ClaveSSA = P.ClaveSSA )
	Inner Join #tmpFar F (Nolock) On (V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia)
    Where ---- V.IdEmpresa = @IdEmpresa and 
          V.IdEstado = @IdEstado and V.IdFarmacia In ( Select IdFarmacia From #tmpFar (Nolock) Where IdEstado = @IdEstado ) --= @IdFarmacia 
          and V.FechaReceta <= @FechaRevision 
          and (datediff(day, V.FechaReceta, @fFecha_Revision) + 1) between @Dias_Alta_Inicio and @Dias_Tope   
    Group by 
        V.IdEmpresa, V.IdEstado, F.IdJurisdiccion, V.IdFarmacia, -- V.FolioVenta, 
        V.FechaReceta, 
        -- D.IdProducto, D.CodigoEAN     
        P.ClaveSSA, P.DescripcionClave          
    Order by V.FechaReceta desc 
    
    
--- Determinar el volumen de Folios     
    Select IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, ClaveSSA, DescripcionClave, 
           0 as TotalFolios, sum(Folios) as Folios, 
           cast(0 as float) as Porcentaje, 
           cast(0 as float) as Acumulado,             
           sum(Cantidad) as Cantidad, 
           @iNulo as Tipo, 2 as RotacionTipo,  
           identity(int, 1,1 ) as keyx  
    into #tmpFolios        
    From #tmpClaves 
    Group by IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, ClaveSSA, DescripcionClave 
    Order by Folios Desc 
    
    Select @iTotalFolios = sum(Folios) From #tmpFolios 
    Update F Set TotalFolios = @iTotalFolios, Porcentaje = (Folios / (@iTotalFolios * 1.00)) * 100 
    From #tmpFolios F 
    
    Update F Set Acumulado = (select sum(Porcentaje) From #tmpFolios X Where X.Keyx <= F.Keyx) 
    From #tmpFolios F      
    
    
--- Determinar el volumen de Salidas 
    Select IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, ClaveSSA, DescripcionClave, 
           0 as TotalCantidad, sum(Cantidad) as Cantidad, 
           cast(0 as float) as Porcentaje, 
           cast(0 as float) as Acumulado,             
           @iNulo as Tipo, 3 as RotacionTipo,  
           identity(int, 1,1 ) as keyx  
    into #tmpCantidades 
    From #tmpClaves 
    Group by IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, ClaveSSA, DescripcionClave 
    Order by Cantidad Desc     
    
    Select @iTotalFolios = sum(Cantidad) From #tmpCantidades  
    Update F Set TotalCantidad = @iTotalFolios, Porcentaje = (Cantidad / (@iTotalFolios * 1.00)) * 100 
    From #tmpCantidades F 
    
    Update F Set Acumulado = (select sum(Porcentaje) From #tmpCantidades X Where X.Keyx <= F.Keyx) 
    From #tmpCantidades F         
---------------------- Obtener datos         
    
    
---------------------- Determinar tipo de rotacion     
    Update C Set Tipo = @iAlto 
    From #tmpClaves C  
    Where Dias Between @Dias_Alta_Inicio and @Dias_Alta_Fin 
    
    Update C Set Tipo = @iMedio 
    From #tmpClaves C  
    Where Dias Between @Dias_Media_Inicio and @Dias_Media_Fin and Tipo = @iNulo 
    
    Update C Set Tipo = @iBajo 
    From #tmpClaves C  
    Where Dias Between @Dias_Baja_Inicio and @Dias_Baja_Fin and Tipo = @iNulo         
    
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
    
---------------------- Determinar tipo de rotacion     

--    select * from #tmpCantidades 
    
--      spp_Rpt_CteReg_RotacionClaves     


---------------------- Determinar tipo de rotacion Parte 002 
/* 

    Drop table #tmpClaves 
    
--    spp_Rpt_CteReg_RotacionClaves            

*/   

    If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rtp_CteReg_TiposRotacion_Claves' and xType = 'U' ) 
       Drop Table Rtp_CteReg_TiposRotacion_Claves  

    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 1 as RotacionTipo, 
        IdEmpresa, space(100) as Empresa, 
        IdEstado, space(100) as Estado, IdJurisdiccion,  space(100) as Jurisdiccion,
        IdFarmacia, space(100) as Farmacia, 
        ClaveSSA, DescripcionClave, @iAlto as TipoRotacion, 'ALTA ROTACIÓN' + space(20) as DescripcionRotacion, 
        cast(0 as float) as Porcentaje, cast(0 as float) as Cantidad,         
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    Into Rtp_CteReg_TiposRotacion_Claves 
    From #tmpClaves C (NoLock) 
    Where Tipo = @iAlto 
    
--    spp_Rpt_CteReg_RotacionClaves        
    
    Insert Into Rtp_CteReg_TiposRotacion_Claves 
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 1 as RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdJurisdiccion, '', IdFarmacia, '', 
        ClaveSSA, DescripcionClave, @iMedio as TipoRotacion, 'MEDIA ROTACIÓN' as DescripcionRotacion, 
        0, 0, 
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpClaves C (NoLock) 
    Where 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA )    
		And 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA and Tipo = @iMedio )    		


    Insert Into Rtp_CteReg_TiposRotacion_Claves 
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 1 as RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdJurisdiccion, '', IdFarmacia, '', 
        ClaveSSA, DescripcionClave, @iBajo as TipoRotacion, 'BAJA ROTACIÓN' as DescripcionRotacion, 
        0, 0,         
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpClaves C (NoLock) 
    Where 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA )    
		And 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA and Tipo = @iBajo )    		
		
           
    Insert Into Rtp_CteReg_TiposRotacion_Claves            
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 1 as RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdJurisdiccion, '', IdFarmacia, '', 
        ClaveSSA, DescripcionClave, @iNulo as TipoRotacion, 'NULA ROTACIÓN' as DescripcionRotacion, 
        0, 0,         
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpClaves C (NoLock) 
    Where 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA )    
		And 
		Not Exists ( Select * From #tmpClaves X (NoLock) Where C.ClaveSSA = X.ClaveSSA and Tipo = @iNulo )    		
           
           
--- Anexar el resto de los tipos procesados 
    Insert Into Rtp_CteReg_TiposRotacion_Claves 
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdJurisdiccion, '', IdFarmacia, '', 
        ClaveSSA, DescripcionClave, Tipo as TipoRotacion, '' as DescripcionRotacion, 
        Porcentaje, Folios,         
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpFolios 
    
    
    Insert Into Rtp_CteReg_TiposRotacion_Claves 
    Select Distinct 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, RotacionTipo, 
        IdEmpresa, '', IdEstado, '', IdJurisdiccion, '', IdFarmacia, '', 
        ClaveSSA, DescripcionClave, Tipo as TipoRotacion, '' as DescripcionRotacion, 
        Porcentaje, Cantidad, 
        @fFecha_Revision as FechaRevision, getdate() as FechaImpresion               
    From #tmpCantidades     
--- Anexar el resto de los tipos procesados                
                          
           
    Update R Set Empresa = E.Nombre
    From Rtp_CteReg_TiposRotacion_Claves R 
    Inner Join CatEmpresas E (NoLock) On ( R.IdEmpresa = E.IdEmpresa ) 

    Update R Set Estado = E.Estado, Farmacia = E.Farmacia  
    From Rtp_CteReg_TiposRotacion_Claves R 
    Inner Join #vw_Farmacias E (NoLock) On ( R.IdEstado = E.IdEstado and R.IdFarmacia = E.IdFarmacia ) 

	Update R Set Jurisdiccion = E.Jurisdiccion  
    From Rtp_CteReg_TiposRotacion_Claves R 
    Inner Join #tmpFar E (NoLock) On ( R.IdEstado = E.IdEstado and R.IdJurisdiccion = E.IdJurisdiccion )   
    
    Update R Set DescripcionRotacion = C.DescripcionRotacion 
    From Rtp_CteReg_TiposRotacion_Claves R     
    Inner Join #tmpRotaciones C On ( R.TipoRotacion = C.RotacionTipo )
    
---------------------- Determinar tipo de rotacion Parte 002 


------------ SALIDA FINAL 
	Select 'Núm. Jurisdicción' = IdJurisdiccion, 'Jurisdicción' = Jurisdiccion, 
	'Núm. Farmacia' = IdFarmacia, Farmacia, 'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClave, 
	'Descripción Rotación' = DescripcionRotacion 
    From Rtp_CteReg_TiposRotacion_Claves (NoLock) 
    Where RotacionTipo = @TipoRotacion 
		  -- and IdFarmacia = '1005' and ClaveSSA = '871' 
    Order by IdJurisdiccion, IdFarmacia, TipoRotacion, DescripcionRotacion 
------------ SALIDA FINAL 

End 
Go--#SQL     

/* 
select * 
from Rtp_CteReg_TiposRotacion_Claves 
Where RotacionTipo = 1 and 
	IdFarmacia = '1005' and ClaveSSA = '871' 
*/ 	
		  