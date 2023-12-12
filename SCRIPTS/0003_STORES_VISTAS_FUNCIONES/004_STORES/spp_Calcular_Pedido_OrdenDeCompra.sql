If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Calcular_Pedido_OrdenDeCompra' and xType = 'P' )
   Drop proc spp_Calcular_Pedido_OrdenDeCompra
Go--#SQL 

--Exec spp_Calcular_Pedido_OrdenDeCompra '001', '11', '0005', '2'

Create Proc spp_Calcular_Pedido_OrdenDeCompra
	(@IdEmpresa  Varchar(3)= '001', @Idestado Varchar(2)= '11', @IdFarmacia varchar(4) = '0005', @TipoPed Varchar(2) = '5' ) 
With Encryption 
As 
Begin 
	Set dateformat YMD
	Set NoCount On

	Declare @TotalTiket int,
			@Acumulado Numeric(14, 8),
			@ClaveSSA varchar(200),
			@Porcentaje numeric(14, 8),
			@Año Varchar(4),
			@Mes varchar(2),
			@Fecha datetime,
			@Where Varchar(200),
			@sSql Varchar(500)
			
		Set @Fecha = DATEADD(MM, -1, GetDate())			
		Set @Año = DatePart(YYYY, @Fecha)
		Set @Mes = DatePart(MM, @Fecha)
		Set @Mes = right('00' + @Mes, 2)

	Select
		TipoDeClave, EsControlado, EsAntibiotico, C.ClaveSSA, C.IdClaveSSA_Sal, C.DescripcionClave, C.Presentacion, C.ContenidoPaquete, Sum(ConsumoMensual) As CantidadPiezas,
		Cast(0 As numeric(14, 8)) as Porcentaje, Cast(0 As numeric(14, 8)) As Acumulado
	Into #Tmp_PCM
	From INV_Consumos_Mensuales M
	Inner Join vw_ClavesSSA_Sales C (NoLock) On (M.ClaveSSA = C.ClaveSSA)
	Where ConsumoMensual > 0 And IdEmpresa = @IdEmpresa And IdEstado = @Idestado And Año = @Año And @Mes = @Mes
	Group By TipoDeClave, EsControlado, EsAntibiotico, C.ClaveSSA, C.IdClaveSSA_Sal, C.DescripcionClave, C.Presentacion, C.ContenidoPaquete

	Select @TotalTiket = SUM(CantidadPiezas) From #Tmp_PCM
	Update #Tmp_PCM Set Porcentaje = (CantidadPiezas * 100.000000) / @TotalTiket
	
	Set @Acumulado = 0
	
	Declare tmp cursor Local For 
		Select ClaveSSA, Porcentaje From #Tmp_PCM Order By Porcentaje
	OPEN tmp 
		FETCH NEXT FROM tmp INTO @ClaveSSA, @Porcentaje
		WHILE ( @@FETCH_STATUS = 0 ) 
			BEGIN
				Set @Acumulado = @Acumulado + @Porcentaje
				Update #Tmp_PCM Set Acumulado = @Acumulado Where ClaveSSA = @ClaveSSA
				FETCH NEXT FROM tmp INTO @ClaveSSA, @Porcentaje
			End
	CLOSE tmp 
    DEALLOCATE tmp
    
    If (@TipoPed = '1')
    Begin
        Set @Where = ' TipoDeClave <> ' + Char(39) + '01' + Char(39)
    End
    
    If (@TipoPed = '2')
    Begin
        Set  @Where = 'TipoDeClave <> ' + Char(39) + '02' + Char(39) + ' or EsControlado = 1  Or EsAntibiotico = 1'
    End
    
    If (@TipoPed = '3')
    Begin
        Set @Where = 'TipoDeClave <> ' + Char(39) + '02' + Char(39) + ' Or EsControlado = 0 '
    End
   
    If (@TipoPed = '4')
    Begin
        Set @Where = 'TipoDeClave <> ' + Char(39) + '02' + Char(39) + ' Or EsAntibiotico = 0 '
    End
    
    Set @sSql = 'Delete From #Tmp_PCM Where ' + @Where
        
    Exec (@sSql)
    --print (@sSql)
    

	Select ClaveSSA, IdClaveSSA_Sal, DescripcionClave, Presentacion, ContenidoPaquete,
		CEILING((
				Case
					When Acumulado <= 20 Then CantidadPiezas
					When Acumulado > 20 Then CantidadPiezas * 2
				End
				)) As CantidadPiezas
	Into #Pedido
	From #Tmp_PCM
	Order By Porcentaje
	
	Select ClaveSSA, SUM(Existencia - ExistenciaEnTransito) As Existencia
	Into #Existencia
	From FarmaciaProductos_CodigoEAN_Lotes F
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.CodigoEAN = P.CodigoEAN)
	Where IdEmpresa = @IdEmpresa And IdEstado = @Idestado And IdFarmacia = @IdFarmacia And
	Convert(Varchar(10), FechaCaducidad, 120) >= Convert(Varchar(10), Getdate(), 120)
	Group By ClaveSSA
	
	Update P
	Set CantidadPiezas = (Case When (CantidadPiezas - Existencia) > 0 Then CantidadPiezas - Existencia Else 0 End )  
	From #Pedido P
	Inner Join #Existencia E On (P.ClaveSSA = E.ClaveSSA)
	
	Delete From #Pedido Where CantidadPiezas <= 0
	
	Select * From #Pedido P Order By CantidadPiezas
	
End
Go--#SQL