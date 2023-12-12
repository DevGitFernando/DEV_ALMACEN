If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_Kardex_Por_Clave' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_Kardex_Por_Clave 
Go--#SQL 

--		Exec spp_Rpt_CteReg_Kardex_Por_Clave '21', '*', '*', '3422', '2012', '11' 

Create Proc spp_Rpt_CteReg_Kardex_Por_Clave 
( 
    -- @IdEmpresa varchar(3) = '001', 
    @IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', @IdFarmacia varchar(4) = '0188', 
    @ClaveSSA varchar(30) = '3422', @iAño int = 2012, @iMes int = 1 
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
    @Keyx int, 
    @ExistenciaInicial int, 
    @Entrada int, 
    @Salida int  

Declare @EncPrincipal varchar(500), 
		@EncSecundario varchar(500)
	 

    Set @ExistenciaInicial = 0 
    Set @Entrada = 0 
    Set @Salida = 0     

    Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

	-- Se Obtienen las Farmacias a Procesar
	Select * Into #tmpFarmacias From CatFarmacias(NoLock) Where IdEstado = @IdEstado and Status = 'A'

	If @IdJurisdiccion <> '*' 
	  Begin
		Delete From #tmpFarmacias Where IdJurisdiccion <> @IdJurisdiccion
	  End

	If @IdFarmacia <> '*'
	  Begin
		Delete From #tmpFarmacias Where IdFarmacia <> @IdFarmacia
	  End 


    Select Top 0 ClaveSSA  
    Into #tmpClaves_Procesar 
    From vw_ExistenciaPorSales 
    Where -- IdEmpresa = @IdEmpresa and 
        IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

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

--------------------------------- Obtener información base del Reporte     
    Select K.IdEmpresa, K.Empresa, 
        K.IdEstado, F.Estado, 
		F.IdJurisdiccion, F.Jurisdiccion, 
		K.IdFarmacia, F.Farmacia,  
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
    Where -- K.IdEmpresa = @IdEmpresa and 
          K.IdEstado = @IdEstado -- and K.IdFarmacia = @IdFarmacia 
		  And K.IdFarmacia In ( Select IdFarmacia From #tmpFarmacias(NoLock) ) 
          and K.ClaveSSA in ( Select ClaveSSA From #tmpClaves_Procesar ) 
		  And Year( K.FechaRegistro ) = @iAño And Month( K.FechaRegistro ) = @iMes
    Group by 
        K.IdEmpresa, K.Empresa, K.IdEstado, F.Estado, F.IdJurisdiccion, F.Jurisdiccion, K.IdFarmacia, F.Farmacia,  
        K.IdProducto, K.CodigoEAN, 
        K.ClaveSSA, K.DescripcionSal, 
        convert(varchar(10), K.FechaRegistro, 120)  
        
--    Select * from #tmpKardex order by FechaRegistro 
--------------------------------- Obtener información base del Reporte     



-------------------------------------------------- Salida de Informacion 
    Select Top 0 IdEmpresa, Empresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,  
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
        IdEmpresa, Empresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,  
        ClaveSSA, DescripcionClave, FechaRegistro         
    
	If @IdJurisdiccion = '*' and @IdFarmacia = '*' 
		Begin 
			Insert Into #tmpKardex_Claves 
			Select IdEmpresa, Empresa, IdEstado, Estado, '' as IdJurisdiccion, '' as Jurisdiccion, '' as IdFarmacia, '' as Farmacia, 
				ClaveSSA, DescripcionClave, space(100) as Presentacion, 0 as ContenidoPaquete, 
				FechaRegistro,  
				sum(Entradas) as Entradas, sum(Salidas) as Salidas, 
				sum(Existencia) as Existencia, 
				sum(Existencia) as ExistenciaAux 
			from #tmpKardex (nolock)     
			group by 
				IdEmpresa, Empresa, IdEstado, Estado, 
				ClaveSSA, DescripcionClave, FechaRegistro     
		End 	

	If @IdJurisdiccion <> '*' and @IdFarmacia = '*' 
		Begin 
			Insert Into #tmpKardex_Claves 
			Select IdEmpresa, Empresa, IdEstado, Estado, '' as IdJurisdiccion, '' as Jurisdiccion, '' as IdFarmacia, '' as Farmacia, 
				ClaveSSA, DescripcionClave, space(100) as Presentacion, 0 as ContenidoPaquete, 
				FechaRegistro,  
				sum(Entradas) as Entradas, sum(Salidas) as Salidas, 
				sum(Existencia) as Existencia, 
				sum(Existencia) as ExistenciaAux 
			from #tmpKardex (nolock)     
			group by 
				IdEmpresa, Empresa, IdEstado, Estado, 
				ClaveSSA, DescripcionClave, FechaRegistro     
		End 	


	If @IdJurisdiccion <> '*' and @IdFarmacia <> '*' 
		Begin 
			Insert Into #tmpKardex_Claves 
			Select IdEmpresa, Empresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
				ClaveSSA, DescripcionClave, space(100) as Presentacion, 0 as ContenidoPaquete, 
				FechaRegistro,  
				sum(Entradas) as Entradas, sum(Salidas) as Salidas, 
				sum(Existencia) as Existencia, 
				sum(Existencia) as ExistenciaAux 
			from #tmpKardex (nolock)     
			group by 
				IdEmpresa, Empresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
				ClaveSSA, DescripcionClave, FechaRegistro     
		End 	


    Update K Set Presentacion = C.Presentacion, ContenidoPaquete = C.ContenidoPaquete  
    From #tmpKardex_Claves K 
    Inner Join vw_ClavesSSA_Sales C On ( K.ClaveSSA = C.ClaveSSA ) 


-------------------------------------------------- Salida de Informacion     
    

    Set @ExistenciaInicial = 0 
    Set @Entrada = 0 
    Set @Salida = 0    
        
--      spp_Rpt_CteReg_Kardex_Por_Clave    
        
    Select @ExistenciaInicial = Existencia From #tmpKardex_Claves Where Keyx = 1 
    -- Select @ExistenciaInicial as Inicio 
    
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
           Set @ExistenciaInicial = ( @ExistenciaInicial + @Entrada ) - @Salida 
           -- Print @ExistenciaInicial 

           Update E Set Existencia = @ExistenciaInicial 
           From  #tmpKardex_Claves E 
           Where Keyx = @Keyx 
           FETCH NEXT FROM tmpExistencia Into  @Keyx, @Entrada, @Salida   
        END
    Close tmpExistencia
    Deallocate tmpExistencia
        
    
--   drop table Rpt_CteReg_Kardex_Claves 

-------------------------------------------------------------------------------------------- 
    If Exists ( Select Name From SysObjects (NoLock) Where Name = 'Rpt_CteReg_Kardex_Claves' and xType = 'U' ) 
       Drop Table Rpt_CteReg_Kardex_Claves 
----    Begin 
----        Select 
----            @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,        
----            IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
----            ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
----            FechaRegistro, 
----            Entradas, Salidas, Existencia, 
----            getdate() as FechaImpresion -- , host_name() as MAC  
----        Into Rpt_CteReg_Kardex_Claves     
----        From #tmpKardex_Claves E     
----    End 
----
----    Delete From Rpt_Kardex_Claves 
----    Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and ClaveSSA = @ClaveSSA and MAC = host_name() 
     
     
--    Insert Into Rpt_CteReg_Kardex_Claves     
    Select 
        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,    
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
        ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
        FechaRegistro, 
        Entradas, Salidas, Existencia, 
        @iAño as Año, @iMes as Mes, 
        getdate() as FechaImpresion -- , host_name() as MAC  
    Into Rpt_CteReg_Kardex_Claves 
    From #tmpKardex_Claves E 
    
--      Select * From Rpt_Kardex_Claves     
    
    
--------------------------------------------------------------------------------------------   
-----------------------  
--    Select 
--        @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
--        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
--        ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
--        convert(varchar(10), FechaRegistro, 120) as FechaRegistro, 
--        Entradas, Salidas, Existencia, 
--        getdate() as FechaImpresion 
--    From #tmpKardex_Claves E 


-----------------------  
	If @IdJurisdiccion = '*'  and @IdFarmacia = '*' 
		Begin 
			Select 
				@EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
				IdEmpresa, Empresa, IdEstado, Estado, '' as IdFarmacia, '' as Farmacia,  
				ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
				cast(convert(varchar(10), FechaRegistro, 120) as datetime) as FechaRegistro, 
				sum(Entradas) as Entradas, sum(Salidas) as Salidas, sum(Existencia) as Existencia, 
				getdate() as FechaImpresion 
			From #tmpKardex_Claves E 
			Group by 
				IdEmpresa, Empresa, IdEstado, Estado, 
				ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
				convert(varchar(10), FechaRegistro, 120) 
		End 

	If @IdJurisdiccion <> '*'  and @IdFarmacia = '*' 
		Begin 
			Select 
				@EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
				IdEmpresa, Empresa, IdEstado, Estado, '' as IdFarmacia, '' as Farmacia,    
				ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
				cast(convert(varchar(10), FechaRegistro, 120) as datetime) as FechaRegistro, 
				sum(Entradas) as Entradas, sum(Salidas) as Salidas, sum(Existencia) as Existencia, 
				getdate() as FechaImpresion 
			From #tmpKardex_Claves E 
			Group by 
				IdEmpresa, Empresa, IdEstado, Estado, 
				ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
				convert(varchar(10), FechaRegistro, 120) 
		End 

	If @IdJurisdiccion <> '*'  and @IdFarmacia <> '*' 
		Begin 
			Select 
				@EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario,
				IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,    
				ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
				cast(convert(varchar(10), FechaRegistro, 120) as datetime) as FechaRegistro, 
				sum(Entradas) as Entradas, sum(Salidas) as Salidas, sum(Existencia) as Existencia, 
				getdate() as FechaImpresion 
			From #tmpKardex_Claves E 
			Group by 
				IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,   
				ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 
				convert(varchar(10), FechaRegistro, 120) 
		End 


--      spp_Rpt_CteReg_Kardex_Por_Clave   
    
        
End 
Go--#SQL 

    