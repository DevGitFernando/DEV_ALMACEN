------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_OP_Ventas__03_NoSurtido' and xType = 'P' )  
    Drop Proc spp_Rpt_OP_Ventas__03_NoSurtido  
Go--#SQL

Create Proc spp_Rpt_OP_Ventas__03_NoSurtido   
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia Varchar(4) = '111', 
	@IdCliente Varchar(4) = '', @IdSubCliente Varchar(4) = '', @IdPrograma Varchar(4) = '', @IdSubPrograma Varchar(4) = '',
	@FechaIncial varchar(10) = '2019-01-01', @FechaFinal varchar(10) = '2019-03-31', @IdBeneficiario Varchar(8) = '',  @ConcentradoReporte smallint = 0, 
	@AplicarMascara smallint = 0  
)  
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sWhereCliente Varchar(200), 
	@sWhereSubCliente Varchar(200), 
	@sWherePrograma Varchar(200), 
	@sWhereSubPrograma Varchar(200),
	@sWhereBeneficiario Varchar(200),
	@sSql Varchar(Max)  
	
	Set @sWhereCliente = ''
	Set @sWhereSubCliente = ''
	Set @sWherePrograma  = ''
	Set @sWhereSubPrograma = ''
	Set @sWhereBeneficiario  = ''
	
	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('00000000' + @IdFarmacia, 4) 

	-- select @IdEmpresa, @IdEstado, @IdFarmacia 
	
	/*
		TipoReporte 
		1 = Venta
		2 = Devolucion
		3 = No Surtido	
	*/
	
---------------------------------------------------------- FILTROS 	
	if (@IdCliente <> '') 
    Begin
		Set @IdCliente = right('0000000000' + @IdCliente, 4) 
		Set @sWhereCliente = ' and IdCliente = ' + Char(39) + @IdCliente + Char(39)
    End
    
    if (@IdSubCliente <> '')
    Begin 
		Set @IdSubCliente = right('0000000000' + @IdSubCliente, 4) 
		Set @sWhereSubCliente = ' and IdSubCliente = ' + Char(39) + @IdSubCliente + Char(39)
    End
	
    if (@IdPrograma <> '')
    Begin 
		Set @IdPrograma = right('0000000000' + @IdPrograma, 4) 		
		Set @sWherePrograma = ' and IdPrograma = ' + Char(39) + @IdPrograma + Char(39)
    End
    
    if (@IdSubPrograma <> '')
    Begin 
		Set @IdSubPrograma = right('0000000000' + @IdSubPrograma, 4) 
		Set @sWhereSubPrograma = ' And IdSubPrograma = '  + Char(39) + @IdSubPrograma  + Char(39)
    End 

    if (@IdBeneficiario <> '') 
    Begin
		Set @IdBeneficiario = right('0000000000' + @IdBeneficiario, 8) 
		Set @sWhereBeneficiario = ' And IdBeneficiario = '  + Char(39) + @IdBeneficiario  + Char(39)
    End 
---------------------------------------------------------- FILTROS 	


--		spp_Rpt_OP_Ventas__03_NoSurtido      

	--If exists ( select * from sysobjects (NoLock) Where Name = 'tmp_OP__04_NoSurtido' and xType = 'U' ) Drop Table tmp_OP__04_NoSurtido
	
	Select *
	Into #VentasEnc
	From VentasEnc V (NoLock) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado  And V.IdFarmacia = @IdFarmacia  
		And Convert(Varchar(10), V.FechaRegistro, 120) between @FechaIncial And @FechaFinal
		

	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente,
		V.IdPrograma, Cast( '' As Varchar(200)) As Programa, V.IdSubPrograma, Cast( '' As Varchar(200)) As SubPrograma,
		convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, 
		I.IdBeneficiario, cast('' as varchar(200)) as NombreBeneficiario,
		V.IdPersonal, Cast( '' As Varchar(200)) As NombrePersonal,
		cast('' as varchar(50)) as FolioReferencia, I.NumReceta, convert(varchar(10), I.FechaReceta, 120) as FechaReceta, 

		I.IdMedico, 
		cast('' As Varchar(200)) As NombreMedico, 
		cast('' As Varchar(200)) As NumeroDeCedulaMedico, 

		I.IdServicio, 
		cast('' As Varchar(200)) As Servicio, 		 
		I.IdArea, 
		cast('' As Varchar(200)) As Area, 

		I.IdTipoDeDispensacion, 
		cast('' As Varchar(200)) As TipoDeDispensacion, 


		D.IdClaveSSA as IdClaveSSA_Base, C.ClaveSSA as ClaveSSA_Base, 
		C.DescripcionClave as DescripcionClave_Base, C.Presentacion as Presentacion_Base, 
		D.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, C.Presentacion, C.ContenidoPaquete, 
		
		--((D.CantidadEntregada + D.CantidadRequerida) * C.ContenidoPaquete) as CantidadSolicitada, 		
		--(D.CantidadEntregada * C.ContenidoPaquete) as CantidadDispensada, 
		--cast(0 as numeric(14,4)) as CantidadDevoluciones, 	
		--(D.CantidadRequerida * C.ContenidoPaquete) as CantidadNoSurtida, 
		
		sum( ((D.CantidadEntregada + D.CantidadRequerida) * C.ContenidoPaquete) ) as CantidadSolicitada, 		
		sum((D.CantidadEntregada * C.ContenidoPaquete) ) as CantidadDispensada, 
		--cast((case when L.ClaveLote not like '%%' then L. else 0 end) as numeric(14,4)) as CantidadDevoluciones_Venta, 	
		cast(0 as numeric(14,4)) as CantidadDispensada_Venta, 	
		cast(0 as numeric(14,4)) as CantidadDispensada_Consigna, 	
		cast(0 as numeric(14,4)) as CantidadDevoluciones, 	
		sum((D.CantidadRequerida * C.ContenidoPaquete) ) as CantidadNoSurtida, 


		0 as Relacionada  
	Into #tmp_OP__NoSurtido 
	From #VentasEnc V (NoLock) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( V.IdEmpresa = I.IdEmpresa And V.IdEstado = I.IdEstado And V.IdFarmacia = I.IdFarmacia And V.FolioVenta = I.FolioVenta ) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
	 	On ( V.IdEmpresa = D.IdEmpresa And V.IdEstado = D.IdEstado And V.IdFarmacia = D.IdFarmacia And V.FolioVenta = D.FolioVenta )  
	Inner Join vw_ClavesSSA_Sales C On ( D.IdClaveSSA = C.IdClaveSSA_Sal ) 
	--Inner Join vw_VentasDet_CodigosEAN_Lotes L (NoLock) On ( V.IdEmpresa = L.IdEmpresa and V.IdEstado = L.IdEstado and V.IdFarmacia = L.IdFarmacia and V.FolioVenta = L.Folio and C.ClaveSSA = L.ClaveSSA ) 
	--Where C.ClaveSSA = '010.000.0109.00'  and V.FolioVenta = 00004941  
	Group by 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.IdCliente, V.IdSubCliente,
		V.IdPrograma, V.IdSubPrograma, convert(varchar(10), V.FechaRegistro, 120), 
		I.IdBeneficiario, V.IdPersonal, I.NumReceta, convert(varchar(10), I.FechaReceta, 120), 
		I.IdMedico, I.IdServicio, I.IdArea, I.IdTipoDeDispensacion, 
		D.IdClaveSSA, C.ClaveSSA, 
		C.DescripcionClave, C.Presentacion, 
		D.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, C.Presentacion, C.ContenidoPaquete   


	--select * from #tmp_OP__NoSurtido 

	Select FolioVenta, IdClaveSSA, ClaveSSA, SUM(CantidadDispensada_Venta) as CantidadDispensada_Venta, SUM(CantidadDispensada_Consigna) as CantidadDispensada_Consigna 
	Into #tmp_OP__NoSurtido___SubFarmacia 
	From 
	( 
		Select 
			V.IdEstado, V.IdCliente, V.IdSubCliente, 
			V.FolioVenta, V.IdClaveSSA, V.ClaveSSA, --L.Cant_Vendida, 
			cast((case when L.ClaveLote not like '%*%' then (L.Cant_Vendida) else 0 end) as numeric(14,4)) as CantidadDispensada_Venta, 			
			cast((case when L.ClaveLote like '%*%' then (L.Cant_Vendida ) else 0 end) as numeric(14,4)) as CantidadDispensada_Consigna 
			--cast((case when L.ClaveLote not like '%*%' then (L.Cant_Vendida / ( P.ContenidoPaquete * 1.0 )) else 0 end) as numeric(14,4)) as CantidadDispensada_Venta, 			
			--cast((case when L.ClaveLote like '%*%' then (L.Cant_Vendida / ( P.ContenidoPaquete * 1.0 )) else 0 end) as numeric(14,4)) as CantidadDispensada_Consigna 

		From #tmp_OP__NoSurtido V (NoLock) 
		Inner Join vw_Productos_CodigoEAN P On ( V.IdClaveSSA_Base = P.IdClaveSSA_Sal ) 
		Inner Join VentasDet_Lotes L (NoLock) 
			On ( V.IdEmpresa = L.IdEmpresa and V.IdEstado = L.IdEstado and V.IdFarmacia = L.IdFarmacia and V.FolioVenta = L.FolioVenta and P.IdProducto = L.IdProducto and P.CodigoEAN = L.CodigoEAN  )  
	) T 
	-- Where CantidadDevoluciones_Consigna > 0 and CantidadDevoluciones_Venta > 0 
	Group by FolioVenta, IdClaveSSA, ClaveSSA 

	Update V Set 
		-- CantidadDispensada_Venta = ( D.CantidadDispensada_Venta / V.ContenidoPaquete ) , CantidadDispensada_Consigna = ( D.CantidadDispensada_Consigna / V.ContenidoPaquete )  
		CantidadDispensada_Venta = D.CantidadDispensada_Venta, CantidadDispensada_Consigna = D.CantidadDispensada_Consigna 
	From #tmp_OP__NoSurtido V 
	Inner Join #tmp_OP__NoSurtido___SubFarmacia D (NoLock) On ( V.FolioVenta = D.FolioVenta and V.IdClaveSSA = D.IdClaveSSA ) 


--		spp_Rpt_OP_Ventas__03_NoSurtido 

	
	--select * from #tmp_OP__NoSurtido___SubFarmacia 

	--- vw_Productos_CodigoEAN  

--		spp_Rpt_OP_Ventas__03_NoSurtido 



	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 
		I.IdProducto, I.CodigoEAN, 
		cast('' as varchar(10)) as IdClaveSSA, 
		cast('' as varchar(100)) as ClaveSSA,
		0 as ContenidoPaquete_ClaveSSA, 
		--cast( sum(( I.Cant_Entregada / P.ContenidoPaquete_ClaveSSA)) as numeric(14,4) ) as Cant_Entregada, 
		--cast( sum(( I.Cant_Devuelta / P.ContenidoPaquete_ClaveSSA)) as numeric(14,4) )  as Cant_Devuelta, 
		--cast( sum(( I.CantidadVendida / P.ContenidoPaquete_ClaveSSA)) as numeric(14,4) )  as CantidadVendida, 

		cast( sum(( I.Cant_Entregada )) as numeric(14,4) ) as Cant_Entregada, 
		cast( sum(( I.Cant_Devuelta )) as numeric(14,4) )  as Cant_Devuelta, 
		cast( sum(( I.CantidadVendida )) as numeric(14,4) )  as CantidadVendida, 

		0 as Relacionada 
	Into #tmpDevoluciones 
	From #VentasEnc V (NoLock) 
	Inner Join VentasDet I (NoLock) 
		On ( V.IdEmpresa = I.IdEmpresa And V.IdEstado = I.IdEstado And V.IdFarmacia = I.IdFarmacia And V.FolioVenta = I.FolioVenta ) 
	--Inner Join vw_Productos_CodigoEAN P On ( I.IdProducto = P.IdProducto and I.CodigoEAN = P.CodigoEAN ) 
	--Where P.ClaveSSA = '010.000.0104.00'  
	Group by  
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 
		I.IdProducto, I.CodigoEAN 
		-- , P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, P.Presentacion, P.ContenidoPaquete_ClaveSSA  


	Update I Set 
		IdClaveSSA = P.IdClaveSSA_Sal, 
		ClaveSSA = P.ClaveSSA, 
		ContenidoPaquete_ClaveSSA = P.ContenidoPaquete_ClaveSSA, 
		Cant_Entregada = cast( (( I.Cant_Entregada / P.ContenidoPaquete_ClaveSSA)) as numeric(14,4) ), 
		Cant_Devuelta = cast( (( I.Cant_Devuelta / P.ContenidoPaquete_ClaveSSA)) as numeric(14,4) ),
		CantidadVendida = cast( (( I.CantidadVendida / P.ContenidoPaquete_ClaveSSA)) as numeric(14,4) )  
	From #tmpDevoluciones I (NoLock) 
	Inner Join vw_Productos_CodigoEAN P On ( I.IdProducto = P.IdProducto and I.CodigoEAN = P.CodigoEAN ) 


--		spp_Rpt_OP_Ventas__03_NoSurtido      


	-------------------------- COMPLETAR INFORMACION ADICIONAL 
	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion, Relacionada = 1   
	From #tmp_OP__NoSurtido B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) 
		On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	

	Update B Set NombreBeneficiario = C.ApPaterno + ' ' + C.ApMaterno + ' ' + C.Nombre, FolioReferencia = C.FolioReferencia 
	From #tmp_OP__NoSurtido B (NoLock) 
	Inner Join CatBeneficiarios C (NoLock) 
		On ( B.IdEstado = C.IdEstado and B.IdFarmacia = C.IdFarmacia and B.IdCliente = C.IdCliente and B.IdSubCliente = C.IdSubCliente and B.IdBeneficiario = C.IdBeneficiario ) 
		
	Update I Set I.Programa = D.Programa, I.SubPrograma = D.SubPrograma
	From #tmp_OP__NoSurtido I(NoLock)
	Inner Join vw_Programas_SubProgramas D (NoLock) On (I.IdPrograma = D.IdPrograma And I.IdSubPrograma = D.IdSubPrograma)
	
	Update I Set NombrePersonal = NombreCompleto
	From #tmp_OP__NoSurtido I(NoLock)
	Inner Join vw_Personal D (NoLock) On (I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.IdPersonal = D.IdPersonal)

	Update I Set NombreMedico = D.NombreCompleto, NumeroDeCedulaMedico = D.NumCedula  
	From #tmp_OP__NoSurtido I (NoLock)
	Inner Join vw_Medicos D (NoLock) On (I.IdEstado = D.IdEstado And I.IdFarmacia = D.IdFarmacia And I.IdMedico= D.IdMedico )

	Update I Set TipoDeDispensacion = D.Descripcion 
	From #tmp_OP__NoSurtido I (NoLock)
	Inner Join CatTiposDispensacion D (NoLock) On ( I.IdTipoDeDispensacion = D.IdTipoDeDispensacion ) 

	Update I Set Servicio = D.Servicio, Area = D.Area_Servicio
	From #tmp_OP__NoSurtido I (NoLock)
	Inner Join vw_Servicios_Areas D (NoLock) On ( I.IdServicio = D.IdServicio and I.IdArea = D.IdArea ) 
	-------------------------- COMPLETAR INFORMACION ADICIONAL 

	------------------------------ 
	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, 
		--DescripcionClave = C.Descripcion, 
		Relacionada = 1   
	From #tmpDevoluciones B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) 
		On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	


	Update N Set CantidadDevoluciones = D.Cant_Devuelta 
	From #tmp_OP__NoSurtido N (NoLock) 
	Inner Join #tmpDevoluciones D (NoLock) On ( N.FolioVenta = D.FolioVenta and N.ClaveSSA = D.ClaveSSA ) 

	----Select * 
	----from #tmpDevoluciones 
	----Where Cant_Devuelta > 0 

	------------------------------ 




	------------------------------------------ Aplicar Mascara en caso de que la operacion lo maneje  
	If @AplicarMascara = 1 
	Begin 
		Update B Set ClaveSSA = PC.Mascara, DescripcionClave = PC.Descripcion, Presentacion = PC.Presentacion 
		From #tmp_OP__NoSurtido B (NoLock) 
		Inner Join CFG_clavessa_Mascara PC (NoLock) 
			On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA and PC.Status = 'A' ) 
	End 



-----			spp_Rpt_OP_Ventas__03_NoSurtido 

	--select * from #tmp_OP__NoSurtido 


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
	If (@ConcentradoReporte = 1) 
		Begin
			Select 
				--IdEmpresa, IdEstado, IdFarmacia, 
				IdClaveSSA_Base, ClaveSSA_Base, DescripcionClave_Base, Presentacion_Base, 
				IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion, 
				sum(CantidadSolicitada) as CantidadSolicitada, 
				sum(CantidadDispensada) as CantidadDispensada, 
				sum(CantidadDevoluciones) as CantidadDevoluciones, 
				sum(CantidadNoSurtida) as CantidadNoSurtida  
			From #tmp_OP__NoSurtido  
			Group by 
				IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA_Base, ClaveSSA_Base, DescripcionClave_Base, Presentacion_Base, 
				IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion 
			Order by ClaveSSA 
		End
	Else
		Begin
			----Update S Set 
			----	FolioVenta = '', FechaRegistro = '', IdCliente = '', IdSubCliente = '', IdPrograma = '', IdSubPrograma = '', 
			----	IdBeneficiario  = '', NombreBeneficiario = '', NumReceta = '', FechaReceta = '', FolioReferencia = '' 
			----From tmp_OP__04_NoSurtido S 

			--select * from #tmpDevoluciones 

			----------------------------------------------------------------------
			Select 
				IdClaveSSA_Base As 'Id Clave SSA Base', ClaveSSA_Base As 'Clave SSA Base', DescripcionClave_Base As 'Descripcion Clave Base', Presentacion_Base As 'Presentación Base', 
				IdClaveSSA As 'Id Clave SSA', ClaveSSA As 'Clave SSA', DescripcionClave As 'Descripción Clave', Presentacion As 'Presentación', 
				sum(CantidadSolicitada) as 'Cantidad Solicitada', 

				sum(CantidadDispensada_Venta) as 'Cantidad Dispensada Venta', 
				sum(CantidadDispensada_Consigna) as 'Cantidad Dispensada Consigna',  
				sum(CantidadDispensada) as 'Cantidad Dispensada', 

				sum(CantidadDevoluciones) as CantidadDevoluciones, 
				sum(CantidadNoSurtida) as 'Cantidad No Surtida'  
			From #tmp_OP__NoSurtido  
			--Where CantidadDevoluciones > 0 
			Group by 
				IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA_Base, ClaveSSA_Base, DescripcionClave_Base, Presentacion_Base, 
				IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion 
			Order by ClaveSSA 	

			
			Select				
				Convert(Varchar(10), FechaRegistro, 120) As 'Fecha No Surtido', 
				FolioVenta As 'Folio No Surtido', 
				'Tipo de dispensación' as TipoDeDispensacion, 
				NumReceta As Receta, 
				'Médico' = NombreMedico, 
				'Número de cédula' as NumeroDeCedulaMedico, 
				IdBeneficiario As 'Núm. Beneficiario', 
				NombreBeneficiario As Beneficiario,
				IdPrograma As 'Núm. Programa', 
				Programa, 
				IdSubPrograma As 'Núm. Sub-Programa', 
				SubPrograma As 'Sub-Programa', 

				Servicio As 'Servicio', 
				Area As 'Área', 

				ClaveSSA_Base As 'Clave SSA Base', 
				DescripcionClave_Base As 'Descripción Clave SSA  Base', 
				Presentacion_Base As 'Presentación Base', 
				ClaveSSA As 'Clave SSA', 
				DescripcionClave As 'Descripción Clave SSA', 
				Presentacion As Presentación, 
				ContenidoPaquete As 'Contenido Paquete', 
				CantidadSolicitada As 'Cantidad Solicitada', 

				CantidadDispensada_Venta as 'Cantidad Dispensada Venta', 
				CantidadDispensada_Consigna as 'Cantidad Dispensada Consigna',  
				CantidadDispensada as 'Cantidad Dispensada', 

				CantidadDevoluciones as 'CantidadDevoluciones', 
				CantidadNoSurtida As 'Cantidad No Surtida' 
			From #tmp_OP__NoSurtido 
			Where 1 = 1
			--Where CantidadNoSurtida > 0
			--Where CantidadDevoluciones > 0 
			Order by FolioVenta, ClaveSSA  	

		End  




---		sp_listacolumnas tmp_OP__04_NoSurtido 
	
	-- print (@sSql) 
	-- Exec  (@sSql)



End
Go--#SQL