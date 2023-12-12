If Exists ( Select * From Sysobjects (NoLock) where Name = 'spp_INV_Consumos_Claves' and xType = 'P' ) 
   Drop Proc spp_INV_Consumos_Claves 
Go--#SQL  

---		Exec spp_INV_Consumos_Claves '001', '21', '0224', '2012-05-31', 1, 1, 1   

Create Proc spp_INV_Consumos_Claves 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
	@FechaRevision varchar(10) = '2012-12-01', 
	@MesRevision int = 12, @MesesConsumo int = 3, 
	@TipoInformacion int = 1, @WhereMesesPorCaducar varchar(1000) = '',
	@EsAlmacen tinyint = 1, @IdPasillo varchar(8) = '1', @IdEstante varchar(8) = '1', @IdEntrepaño varchar(8) = '*' 
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
--Set NoCount On 

Declare 
	@FechaInicial varchar(10), 
	@FechaFinal varchar(10),   
	@Fecha datetime, 
	--@MesesParaCaducar int,
	@sSql varchar(8000), 
	@sFiltroUbicacion varchar(500) 	
	
Declare 
	@Empresa varchar(200), 
	@Estado varchar(200), 
	@Farmacia varchar(200) 	
	
---- Determinar si la Unidad es Almacen 
	Select @EsAlmacen = EsAlmacen From CatFarmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
	
	Set @Fecha = cast(@FechaRevision  as datetime) 
	Set @FechaInicial = convert(varchar(10), dateadd(month, @MesRevision * -1, @Fecha), 120) 
	Set @FechaFinal = convert(varchar(10), @Fecha, 120)
	--Set @MesesParaCaducar = 2 	
	Set @sSql = '' 
	Set @sFiltroUbicacion  = '' 
	
	Select @Empresa = Nombre From CatEmpresas (NoLock) where IdEmpresa = @IdEmpresa 
	Select @Estado = Estado, @Farmacia = Farmacia From vw_Farmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
	
---	Select @FechaRevision, @Fecha, @FechaInicial, @FechaFinal   
	

	If Exists ( Select name from sysobjects (nolock) where name = 'Rpt_Consumos_Claves ' and xType = 'U' )  
	   Drop table Rpt_Consumos_Claves 

--- Generar concentrado 	 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, -- @Año as Año, @Mes as Mes,  
		D.IdProducto, D.CodigoEAN, sum(cast(D.CantidadVendida as int)) as Cantidad_Consumo   
	into #tmpConsumos 
	From VentasEnc V (NoLock) 
	Inner Join VentasDet D (NoLock) On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	-- Inner Join vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = D.IdProducto and P.CodigoEAN = D.CodigoEAN ) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
		  and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal 
	Group by V.IdEmpresa, V.IdEstado, V.IdFarmacia, D.IdProducto, D.CodigoEAN  


	Select 
		@IdEmpresa as IdEmpresa, @Empresa as Empresa, 
		@IdEstado as IdEstado, @Estado as Estado, 
		@IdFarmacia as IdFarmacia, @Farmacia as Farmacia, 
		P.ClaveSSA, P.DescripcionClave, 
		sum(V.Cantidad_Consumo) as Cantidad_Consumo, 
		@MesRevision as MesesRevision, 
		@MesesConsumo as Meses_Existencia, 		
		cast((sum(V.Cantidad_Consumo) / (@MesRevision*1.0)) as numeric(14,4)) as ConsumoMensual, 
		cast(0 as numeric(14,4)) as ExistenciaMeses, 
		cast(0 as int) as ExistenciaMeses_Piezas, 	
		0 as Existencia, 
		0 as Excedente, 
		0 as Faltante, 		
		0 as EsExcedente, 0 as EsFaltante 
	Into Rpt_Consumos_Claves    	
	From #tmpConsumos V   
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = V.IdProducto and P.CodigoEAN = V.CodigoEAN )    
--	Where P.ClaveSSA = '624' 
	Group by -- V.IdEmpresa, V.IdEstado, V.IdFarmacia, F.IdJurisdiccion, 
		P.ClaveSSA, P.DescripcionClave    
--- Generar concentrado 	  		  

	Select Top 0 IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, Existencia Into #tmpClavesExistenciaUbicacion From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (Nolock)
	Select Top 0 IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, Existencia Into #tmpClavesExistenciaPorSales From vw_ExistenciaPorCodigoEAN_Lotes (Nolock)

	If @EsAlmacen = 1
		Begin 
			Set @sFiltroUbicacion  = '' 
			-- @IdPasillo varchar(8) = '', @IdEstante varchar(8) = '', @IdEntrepaño varchar(8) = ''

			If @IdPasillo <> '*' 
			Begin 
				Set @sFiltroUbicacion  = ' and IdPasillo = ' + @IdPasillo +  ' ' 
				If @IdEstante <> '*' 
				Begin 
					Set @sFiltroUbicacion  = @sFiltroUbicacion + '   ' + ' and IdEstante = ' + @IdEstante + ' ' 
					If @IdEntrepaño <> '*' 
					Begin 
						Set @sFiltroUbicacion  = @sFiltroUbicacion + '   ' + ' and IdEntrepaño = ' + @IdEntrepaño + ' ' 
					End 				
				End		
			End 	
		
			Set @sSql = 'Insert Into #tmpClavesExistenciaUbicacion( IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, Existencia )' + char(13) + 
						'Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, Sum(Existencia) As Existencia ' + char(13) + 
						'From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (Nolock) ' + char(13) + 
						'Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' ' + char(13) + 
						'and IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' ' + char(13) + 
						'And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' ' + char(13) + 
						@sFiltroUbicacion + ' ' + char(13) + 
						@WhereMesesPorCaducar + ' ' + 
						'Group By  IdEmpresa, IdEstado, IdFarmacia, ClaveSSA ' 	
			Exec (@sSql) 
			--print @sSql 
	
			update R Set R.Existencia = E.Existencia 
			From Rpt_Consumos_Claves R (NoLock) 
			inner join #tmpClavesExistenciaUbicacion E (NoLock) 
				On ( R.IdEmpresa = E.IdEmpresa and R.IdEstado = E.IdEstado and R.IdFarmacia = E.IdFarmacia and R.ClaveSSA = E.ClaveSSA )

			
			
		End
	Else
		Begin
			Set @sSql = 'Insert Into #tmpClavesExistenciaPorSales( IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, Existencia ) ' +
						'Select IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, Sum(Existencia) As Existencia ' +
						'From vw_ExistenciaPorCodigoEAN_Lotes (Nolock) ' +
						'Where IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' ' + 
						'and IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' ' + 
						'And IdFarmacia = ' + Char(39) + @IdFarmacia + Char(39) + ' ' + 
						@WhereMesesPorCaducar + ' ' + 
						'Group By  IdEmpresa, IdEstado, IdFarmacia, ClaveSSA '
			Exec (@sSql)

			update R Set R.Existencia = E.Existencia 
			From Rpt_Consumos_Claves R (NoLock)  
			inner join #tmpClavesExistenciaPorSales E (NoLock) 
				On ( R.IdEmpresa = E.IdEmpresa and R.IdEstado = E.IdEstado and R.IdFarmacia = E.IdFarmacia and R.ClaveSSA = E.ClaveSSA )
		End  

	update R Set 
		ExistenciaMeses = (ConsumoMensual * @MesesConsumo),  
		ExistenciaMeses_Piezas = round((ConsumoMensual * @MesesConsumo), 0)  		
	From Rpt_Consumos_Claves  R  

------- Faltantes 
	update R Set 
		EsFaltante = 1, 
		Faltante = abs(Existencia - ExistenciaMeses_Piezas) 
	From Rpt_Consumos_Claves  R  	
	Where (Existencia - ExistenciaMeses_Piezas) < 0 
	
------- Excedentes  
	update R Set 
		EsExcedente = 1, 
		Excedente = Existencia - ExistenciaMeses_Piezas 
	From Rpt_Consumos_Claves  R  	
	Where (Existencia - ExistenciaMeses_Piezas) > 0 

---  IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Cantidad_Consumo, MesesRevision, ConsumoMensual, ExistenciaMeses, ExistenciaMeses_Piezas, Existencia, Excedente, Faltante, EsExcedente, EsFaltante 

	If @TipoInformacion = 0 
	Begin 
		Select 
			-- IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
			'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClave, Cantidad_Consumo, MesesRevision, Meses_Existencia, ConsumoMensual, 
			ExistenciaMeses, ExistenciaMeses_Piezas, Existencia, Excedente, Faltante 
		From Rpt_Consumos_Claves  
	End 
	
	If @TipoInformacion = 1  
	Begin 
		Select 
			-- IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
			'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClave, 
			'Consumo' = Cantidad_Consumo, -- MesesRevision, Meses_Existencia, ConsumoMensual, 
			-- ExistenciaMeses, 
			'Stock sugerido' = ExistenciaMeses_Piezas, Existencia, Excedente -- , Faltante 
		From Rpt_Consumos_Claves  
		Where EsExcedente = 1 
	End 	
	
	If @TipoInformacion = 2  
	Begin 
		Select 
			IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
			ClaveSSA, DescripcionClave, Cantidad_Consumo, MesesRevision, Meses_Existencia, ConsumoMensual, 
			ExistenciaMeses, ExistenciaMeses_Piezas, Existencia, Excedente, Faltante 
		From Rpt_Consumos_Claves  
		Where EsFaltante = 1 
	End 		
	
-------------	Cargar historico  	
	If @TipoInformacion = 3   
	Begin 
		Select 
			-- IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
			'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClave, 
			'Consumo' = Cantidad_Consumo, -- MesesRevision, Meses_Existencia, ConsumoMensual, 
			-- ExistenciaMeses, 
			'Stock sugerido' = ExistenciaMeses_Piezas, Existencia, Excedente -- , Faltante 
		From Rpt_Consumos_Claves  
		Where EsExcedente = 1 
	End 
	
	

---		spp_INV_Consumos_Claves   		

---		select top 1 * from vw_ExistenciaPorSales 

---		sp_listacolumnas Rpt_Consumos_Claves  
		  
End 
Go--#SQL 
