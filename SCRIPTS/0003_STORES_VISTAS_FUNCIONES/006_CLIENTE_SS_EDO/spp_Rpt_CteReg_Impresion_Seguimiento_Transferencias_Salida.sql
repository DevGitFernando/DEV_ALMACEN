If Exists( Select Name From SysObjects(NoLock) Where Name = 'Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida' And xType = 'U' )
	Drop Table Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida  
Go--#SQL 
		
------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida' And xType = 'P' )
	Drop Proc spp_Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida
Go--#SQL 

--	Exec   spp_Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida  '21', '1188', '2012-10-01', '2012-11-12', '0', '0'  

Create Procedure spp_Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida
( 
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '*', 
	@FechaInicial varchar(10) = '2010-12-01', @FechaFinal varchar(10) = '2018-01-08', 
	@iOpcion int = 0, @TipoInsumo tinyint = 0 
)
With Encryption 
As 
Begin 
-- Set NoCount On 
Set DateFormat YMD 
Declare	
	@sEmpresa varchar(150), 
	@EncabezadoPrincipal varchar(200), 
	@EncabezadoSecundario varchar(200),
	@sSql Varchar(max)


	/* @iOpcion
		0 = Ventas
		1 = Consignacion	
	*/

	-- Se obtiene el Encabezado Principal y el Encabezado Secundario

		Select IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, EsAlmacen
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, EsAlmacen ) ' + char(13) + char(10) + 
				'Select IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, EsAlmacen ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  

	Select @EncabezadoPrincipal = EncabezadoPrincipal, @EncabezadoSecundario = EncabezadoSecundario From fg_Unidad_EncabezadoReportesClientesSSA() 

	Create Table #tmpProceso 
	(
		EncabezadoPrincipal varchar(150) Not Null Default '', 
		EncabezadoSecundario varchar(150) Not Null Default '', 

		IdEmpresa varchar(3) Not Null Default '', 
		Empresa varchar(150) Not Null Default '', 
		IdEstado varchar(2) Not Null Default '', 
		Estado varchar(100) Not Null Default '',
		IdJurisdiccion Varchar(10) Not null Default '',
		Jurisdiccion Varchar(200) Not null Default '',
		IdFarmacia varchar(4) Not Null Default '', 
		Farmacia varchar(150) Not Null Default '', 
		EsAlmacen smallint Not Null Default 0, 

		IdProducto varchar(8) Not Null Default '', 
		DescripcionProducto varchar(300) Not Null Default '',	
		CodigoEAN varchar(30) Not Null Default '', 
		ClaveLote varchar(30) Not Null Default '', 
		FechaCaducidad datetime Not Null Default getdate(), 
		MesesPorCaducar int Not Null Default -1,		
		FechaRegistro datetime Not Null Default getdate(), 

		IdClaveSSA_Sal varchar(4) Not Null Default '', 
		ClaveSSA varchar(20) Not Null Default '', 
		DescripcionSal varchar(7000) Not Null Default '', 
		IdPresentacion_ClaveSSA varchar(4) Not Null Default '', 
		Presentacion_ClaveSSA varchar(100) Not Null Default '', 

		IdPresentacion varchar(4) Not Null Default '', 
		Presentacion varchar(100) Not Null Default '', 
		ContenidoPaquete int Not Null Default 0, 

		Folio varchar(30) Not Null Default '', 
		FolioTransferenciaRef varchar(30) Not Null Default '',
		IdJurisdiccionRecibe Varchar(10) Not null Default '',
		JurisdiccionRecibe Varchar(200) Not null Default '',
		IdFarmaciaRecibe varchar(4) Not Null Default '', 
		FarmaciaRecibe varchar(150) Not Null Default '',
		EsAlmacenRecibe smallint Not Null Default 0, 		
		FechaTransferencia datetime Not Null Default getdate(), 
		FechaRegistroTransferencia datetime Not Null Default getdate(), 

		Costo numeric(14,4) Not Null Default 0 , 
		TasaIva numeric(14,4) Not Null Default 0,  
		Cantidad numeric(14,4) Not Null Default 0 ,	
		Importe numeric(14,4) Not Null Default 0 , 
		Existencia int Not Null Default 0, 
		FechaInicial varchar(10), 
		FechaFinal varchar(10), 
		FechaImpresion datetime Not Null Default getdate() 
	)  

------------------------ Obtener la informacion base 
	Insert Into #tmpProceso ( EncabezadoPrincipal, EncabezadoSecundario, 
				IdEmpresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Folio, FolioTransferenciaRef, IdFarmaciaRecibe, 
				IdProducto, CodigoEAN, ClaveLote, Costo, Cantidad, Importe, TasaIva, FechaInicial, FechaFinal  ) 
	Select 
		@EncabezadoPrincipal, @EncabezadoSecundario, 
		E.IdEmpresa, E.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion, E.IdFarmacia, F.Farmacia, E.FolioTransferencia, FolioTransferenciaRef, IdFarmaciaRecibe, 
		L.IdProducto, L.CodigoEAN, L.ClaveLote, D.CostoUnitario, D.CantidadEnviada, D.Importe, D.TasaIva, @FechaInicial, @FechaFinal 
	From TransferenciasEnc E (NoLock) 
	Inner Join TransferenciasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia 
			 and E.FolioTransferencia = D.FolioTransferencia ) 
	Inner Join TransferenciasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia 
			 and D.FolioTransferencia = L.FolioTransferencia And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )
	Inner Join #vw_Farmacias F (NoLock) On (E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia)	
	Where E.TipoTransferencia = 'TS' 
		And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal 
------------------------ Obtener la informacion base 

	
--		spp_Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida 

------------------------ Completar la informacion de registro 
	Update R Set IdClaveSSA_Sal = P.IdClaveSSA_Sal, ClaveSSA = P.ClaveSSA, DescripcionSal = P.DescripcionClave, 
		 IdPresentacion_ClaveSSA = P.IdPresentacion_ClaveSSA, Presentacion_ClaveSSA = P.Presentacion_ClaveSSA, 
		 DescripcionProducto = P.Descripcion, IdPresentacion = P.IdPresentacion, Presentacion = P.Presentacion, 
		 ContenidoPaquete = P.ContenidoPaquete 
	From #tmpProceso R 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( R.IdProducto = P.IdProducto and R.CodigoEAN = P.CodigoEAN ) 

	Update R Set Empresa = E.Nombre 
	From #tmpProceso R 
	Inner Join CatEmpresas E (NoLock) On ( R.IdEmpresa = E.IdEmpresa ) 

	--Update R Set Estado = F.Estado, Farmacia = F.Farmacia, EsAlmacen = F.EsAlmacen  
	--From #tmpProceso R 
	--Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia )

	Update R Set FarmaciaRecibe = F.Farmacia, EsAlmacenRecibe = F.EsAlmacen, IdJurisdiccionRecibe = F.IdJurisdiccion, JurisdiccionRecibe = F.Jurisdiccion
	From #tmpProceso R 
	Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmaciaRecibe = F.IdFarmacia ) 


	Update R Set FechaRegistro = F.FechaRegistro, FechaCaducidad = F.FechaCaducidad, Existencia = F.Existencia, 
		 MesesPorCaducar = DateDiff( Month, GetDate(), F.FechaCaducidad )     
	From #tmpProceso R 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( R.IdEmpresa = F.IdEmpresa and R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia 
			 and R.IdProducto = F.IdProducto and R.CodigoEAN = F.CodigoEAN and R.ClaveLote = F.ClaveLote )
------------------------ Completar la informacion de registro 

--	select top 1 * from FarmaciaProductos_CodigoEAN_Lotes 


-------- Se Filtra la informacion  
	If @iOpcion <> 0
	  Begin
		If @iOpcion = 1
			Delete From #tmpProceso Where ClaveLote Like '%*%'
		
		If @iOpcion = 2
			Delete From #tmpProceso Where ClaveLote Not Like '%*%'
	  End

	If @TipoInsumo <> 0 
		   Begin 
			  If @TipoInsumo = 1 
				 Delete From #tmpProceso Where TasaIva <> 0  --- Medicamentos 

			  If @TipoInsumo = 2
				 Delete From #tmpProceso Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
		   End 
-------- Se Filtra la informacion  

--	Select * 	From #tmpProceso 


--------------- Se elimina la tabla en caso de existir. 
	If Exists( Select Name From SysObjects(NoLock) Where Name = 'Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida' And xType = 'U' )
		Drop Table Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida  

	Select * 
	Into Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida  
	From #tmpProceso 



/* 
	-- Se inserta en la tabla temporal los productos de consignacion
	Select	@EncabezadoPrincipal as EncabezadoPrincipal, @EncabezadoSecundario as EncabezadoSecundario, 
			E.IdClaveSSA_Sal, E.ClaveSSA, E.DescripcionSal, 
			E.IdPresentacion_ClaveSSA, E.Presentacion_ClaveSSA,
			E.IdProducto, E.CodigoEAN, E.ClaveLote, 
			E.FechaCaducidad, DateDiff( Month, GetDate(), E.FechaCaducidad ) as MesesPorCaducar, 
			E.FechaRegistro, E.DescripcionProducto, E.IdPresentacion, E.Presentacion, E.ContenidoPaquete, 
			E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, GetDate() as FechaImpresion, E.Existencia, 
			D.Folio, T.FolioTransferenciaRef, T.IdFarmaciaRecibe, T.FarmaciaRecibe,
			T.FechaTransferencia, T.FechaReg as FechaRegistroTransferencia, 
			D.Costo, D.Cantidad, D.Importe, @FechaInicial as FechaInicial, @FechaFinal as FechaFinal, E.TasaIva
	Into Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida
	From #vw_ExistenciaPorCodigoEAN_Lotes E(NoLock) 
	Inner Join #vw_TransferenciaDet_CodigosEAN_Lotes L(NoLock) On ( E.IdProducto = L.IdProducto And E.CodigoEAN = L.CodigoEAN And E.ClaveLote = L.ClaveLote )
	Inner Join #vw_TransferenciasDet_CodigosEAN D(NoLock) On( L.IdEmpresa = D.IdEmpresa And L.IdEstado = D.IdEstado And L.IdFarmacia = D.IdFarmacia And L.Folio = D.Folio And L.IdProducto = D.IdProducto And L.CodigoEAN = D.CodigoEAN )
	Inner Join #vw_TransferenciasEnc T(NoLock) On ( D.IdEmpresa = T.IdEmpresa And D.IdEstado = T.IdEstado And D.IdFarmacia = T.IdFarmacia And D.Folio = T.Folio )
	Where E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And T.TipoTransferencia = 'TS' 
		And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia
		And Convert( varchar(10), T.FechaReg, 120 ) Between @FechaInicial And @FechaFinal 

*/ 

End
Go--#SQL 