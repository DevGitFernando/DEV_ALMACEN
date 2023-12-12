If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Administrativos__Sabana_Guanajuato' and xType = 'P')
    Drop Proc spp_Rpt_Administrativos__Sabana_Guanajuato
Go--#SQL

Create Proc spp_Rpt_Administrativos__Sabana_Guanajuato
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmaciaInicial varchar(4) = '0011', @IdFarmaciaFinal varchar(4) = '0011',
	@FechaInicial varchar(10) = '2014-06-01', @FechaFinal varchar(10) = '2014-06-30'
) 
--With Encryption 
As
Begin
Set NoCount On
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint

--If Exists(Select * From SysObjects Where name = 'RptAdmonDispensacion_Detallado__Sabana_A  ' And xtype = 'U')
--Begin
--	Drop Table RptAdmonDispensacion_Detallado__Sabana_A  
--End
----Go--#SQL 
	
	Delete RptAdmonDispensacion_Detallado__Sabana_A


Declare 
    @IdFarmacia varchar(4), 
	@IdCliente varchar(4), @IdSubCliente varchar(4), 
	@IdPrograma varchar(4), @IdSubPrograma varchar(4),  

	@TipoDispensacion tinyint,
	@TipoInsumo tinyint , @TipoInsumoMedicamento tinyint , @SubFarmacias varchar(200), 
	@MostrarPrecios tinyint  	
	
Declare   
	@EjecutarReporte bit, 
	@EjecutarParametros bit  	

	Set @EjecutarReporte = 1 
	Set @EjecutarParametros = 1 	
	
    Select 
        --@IdEmpresa = '001', @IdEstado = '11', @IdFarmacia = '0003', 
        @IdCliente = '*', @IdSubCliente = '*', 
        @IdPrograma = '*', @IdSubPrograma = '*',  	
	    @TipoDispensacion = 0,
	    @TipoInsumo = 0, @TipoInsumoMedicamento = 0, @SubFarmacias = '', 
	    @MostrarPrecios = 1
	    
	--If exists ( Select Name From tempdb..sysobjects (noLock) Where Name like '#tmpF%' and xType = 'U' )  
	--	Drop Table tempdb..#tmpF 

	select space(2) as IdEstado, space(4) as IdFarmacia Into #tmpF where 1  = 0  
	Insert Into #tmpF 
	Select Distinct IdEstado, IdFarmacia 
	From VentasEnc 
	Where IdEstado = @IdEstado and idfarmacia Between @IdFarmaciaInicial And @IdFarmaciaFinal--('0028','0003')--,'0003','0033','0043')	-- and 1 = 0
	
	------If exists ( Select Name From sysobjects (noLock) Where Name = 'RptAdmonDispensacion_Detallado__Sabana_A' and xType = 'U' )
	------	Drop Table RptAdmonDispensacion_Detallado__Sabana_A

	------	Select *, 0 as EsDeConsignacion,
	------	Cast( 0 As numeric(14,4)) As Factor, Cast(0 As numeric(14,4)) As CantidadCajas, Cast(0 As Int) As Agrupado, Cast(0 As Int) As Residuo,
	------	Cast( 0 As numeric(14,4)) As preciolicCaja, Cast( 0 As numeric(14,4)) As precioadmin, Cast('' As varchar(100)) As Financiamiento,
	------	Cast('' As varchar(30)) As CLUES
	------	Into RptAdmonDispensacion_Detallado__Sabana_A   
	------	From RptAdmonDispensacion_Detallado 
	------	Where 1 = 0 



	Declare #cursor_002 
	Cursor For 
		Select IdFarmacia  
		From #tmpF (NoLock) 
		Order By IdFarmacia 
	Open #cursor_002
	FETCH NEXT FROM #cursor_002 Into @IdFarmacia  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 		

				Set @TipoDispensacion = 1  
				Exec spp_Rpt_Administrativos @IdEmpresa, @IdEstado, @IdFarmacia, 
											 @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
											 @TipoDispensacion, @FechaInicial,  @FechaFinal, 
											 @TipoInsumo, @TipoInsumoMedicamento, @SubFarmacias, @MostrarPrecios  

				Insert Into RptAdmonDispensacion_Detallado__Sabana_A  
				Select *, 1 as EsConsignacion, 0, 0, 0, 0, 0, 0, '', ''
				From RptAdmonDispensacion_Detallado 
				--print 'Hola'		
				
				Set @TipoDispensacion = 2 
				Exec spp_Rpt_Administrativos @IdEmpresa, @IdEstado, @IdFarmacia, 
											 @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
											 @TipoDispensacion, @FechaInicial,  @FechaFinal, 
											 @TipoInsumo, @TipoInsumoMedicamento, @SubFarmacias, @MostrarPrecios  

				Insert Into RptAdmonDispensacion_Detallado__Sabana_A  
				Select *, 0 as EsDeConsignacion, 0, 0, 0, 0, 0, 0, '', ''
				From RptAdmonDispensacion_Detallado 
				
		
			FETCH NEXT FROM #cursor_002 Into  @IdFarmacia  
		END
	Close #cursor_002 
	Deallocate #cursor_002





/* 
@TipoDispensacion 
    0 ==> Todo 
    1 ==> Consignacion 
    2 ==> Venta 
                       
                        drop table RptAdmonDispensacion

                        select * from RptAdmonDispensacion_Detallado

@TipoInsumo 
    0 ==> Todo 
    1 ==> Medicamento  
    2 ==> Material de Curacion  

*/ 
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add Factor numeric(14,4)
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add CantidadCajas numeric(14,4)
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add Agrupado Int
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add Residuo Int
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add precioadmin Numeric(14,4)
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add PorcParticipacion Numeric(14,10)
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add AAsignar Bit
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add Asignado Bit
		
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add IdFuenteFinanciamiento varchar(4)
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add IdFinanciamiento varchar(4)
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add Financiamiento varchar(100)
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add CLUES varchar(30)
		------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add preciolicCaja Numeric(14,4)
		--------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add preciolicCaja varchar(30)
		--------Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add preciolicCaja varchar(30)
		--------Go
	
	--------Select Min(Folio), Max(Folio)
	--------From RptAdmonDispensacion_Detallado__Sabana_A  
	--------Go
	
			
	------Factor
	--Alter Table RptAdmonDispensacion_Detallado__Sabana_A   Add Factor numeric(14,4)
	--Go
	Update  V
	Set Factor = dbo.fg_CalcularFactorLicitacion( V.IdEstado, V.IdCliente, V.IdSubCliente, V.IdProducto)
	From RptAdmonDispensacion_Detallado__Sabana_A   V
	--Go
	--------------------------Factor
	
	-------CantidadCajas
	Update  V
	Set CantidadCajas = ((cast(V.Cantidad as int)* Factor) / (V.ContenidoPaquete_ClaveSSA * 1.0))
	From RptAdmonDispensacion_Detallado__Sabana_A   V
	--Go
	-------------------------CantidadCajas
	
	-------Agrupado
	Update  V
	Set Agrupado = Cast(CantidadCajas As Int)
	From RptAdmonDispensacion_Detallado__Sabana_A   V
	--Go
	-------------------------Agrupado
	
	-------Residuo
	Update  V
	Set Residuo = ((cast(V.Cantidad as int)* Factor) % (V.ContenidoPaquete_ClaveSSA * 1.0))
	From RptAdmonDispensacion_Detallado__Sabana_A   V
	--Go
	-------------------------Residuo
	
	---preciolicCaja
	--Update  V
	--Set preciolicCaja = (PrecioLicitacionUnitario * ContenidoPaquete_ClaveSSA )
	--From RptAdmonDispensacion_Detallado__Sabana_A   V
	--Go
	---------------------preciolicCaja
	
	-------SubTotal
	--Update  V
	--Set SubTotal = Agrupado * PrecioLicitacion
	--From RptAdmonDispensacion_Detallado__Sabana_A   V
	--Go
	-------------------------SubTotal
	
	-------Iva
	--Update  V
	--Set Iva = SubTotal * (TasaIva/100)
	--From RptAdmonDispensacion_Detallado__Sabana_A   V
	--Go
	-------------------------Iva
	
	-------Total
	--Update  V
	--Set Total = SubTotal + Iva
	--From RptAdmonDispensacion_Detallado__Sabana_A   V
	--Go
	-------------------------Total
	
	-------precioadmin
	Update  V
	Set precioadmin = precio_Administracion
	From RptAdmonDispensacion_Detallado__Sabana_A   V
	Inner Join Gto_PreciosAdmon P ON (V.ClaveSSA = P.ClaveSSA)
	--Go
	-------------------------precioadmin
	
	---Poner Financiamiento 
	Update A
	Set A.Financiamiento = IsNull((Select Financiamiento From vw_FACT_FuentesDeFinanciamiento_Claves F (NoLock) Where F.ClaveSSA = A.ClaveSSA), 'No Licitado')
	From RptAdmonDispensacion_Detallado__Sabana_A   A
	--Go
	------------Poner Financiamiento
	
	---Poner Clues
	Update A
	Set A.Clues = IsNull(C.Clues, Left(Beneficiario, 11))
	From RptAdmonDispensacion_Detallado__Sabana_A   A (NoLock)
	Left Join CatFarmacias_CLUES C On (A.IdEstado = C.IdEstado And A.IdFarmacia = C.Idfarmacia)
	--Go
	------------Poner Clues

	Select 1 as proveedor, Upper(Financiamiento) As fuentef, Clues, IdFarmacia As farmacia, Farmacia As nom_farmacia, Folio, FechaRegistro As fechaf,
		NumReceta As receta, fechaReceta As fechar, FolioReferencia As Poliza, Beneficiario, IdMedico, Medico As doctor, ClaveSSA, DescripcionSal, 
		Esconsignacion As consigna, ContenidoPaquete_ClaveSSA As contpaquete, ClaveLote As lote, FechaCaducidad As fechacad,
		PrecioLicitacion As preciolic, precioadmin, Cantidad, Cantidad As Cantidadv,
		(Case When Tasaiva > 0 Then 1 Else 0 End) As IVA, Factor, CantidadCajas, Agrupado, Residuo, PrecioLicitacionUnitario As preciolicUnitario, IdBeneficiario
	From RptAdmonDispensacion_Detallado__Sabana_A   A
	--where ClaveSSA Like '%5485%' --DescripcionSal Like '%cubre%' Or clavessa = '010.000.1732.00'
	--Order BY ClaveSSA
	End
Go--#SQL