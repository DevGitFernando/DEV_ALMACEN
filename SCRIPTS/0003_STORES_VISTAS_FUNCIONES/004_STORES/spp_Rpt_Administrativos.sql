If Exists ( select * from sysobjects (nolock) Where Name = 'tmpRptAdmonDispensacion' and xType = 'U' ) 
	Drop table tmpRptAdmonDispensacion 
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'tmpDatosInicialesLotes' and xType = 'U' )
      Drop Table tmpDatosInicialesLotes
Go--#SQL  

-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos 
Go--#SQL 

Create Proc spp_Rpt_Administrativos 
(   
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '3224', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005', 
	@IdPrograma varchar(4) = '0002', @IdSubPrograma varchar(4) = '1312',  
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2017-10-01', @FechaFinal varchar(10) = '2017-10-15', 
	@TipoInsumo tinyint = 0, @TipoInsumoMedicamento tinyint = 0, @SubFarmacias varchar(200) = '', -- '06'  
	@MostrarPrecios tinyint = 0, @IncluirDetalleInformacion tinyint = 1, @SoloDispensacion tinyint = 0, 
	@MostrarDevoluciones tinyint = 1, 
	@OrigenDispensacion tinyint = 0, --  0 ==> Todo | 1 ==> Dispensacion | 2 ==> Vales, 

	@Ordenamiento tinyint = 1 --  1 ==> Clave SSA | 1 ==> Descripcion Clave SSA 
)     
As 
Begin 
Set NoCount On 

	if @TipoInsumoMedicamento <>  0 
           Set @TipoInsumo = 1

--------------------------------	VALIDACION 
	---- Exec spp_Rpt_Administrativos_009 @IdEmpresa, @IdEstado, @IdCliente, @IdSubCliente, 0    -- Preparar las tablas de catalogo 
	Print 'spp_Rpt_Administrativos_009 ' 
	Exec spp_Rpt_Administrativos_009 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @ForzarActualizacion = 0 ---- Preparar las tablas de catalogo  


	Print 'spp_Rpt_Administrativos_001 ' 
	Exec spp_Rpt_Administrativos_001 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, 
		@IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @IdPrograma = @IdPrograma, @IdSubPrograma = @IdSubPrograma, 
		@TipoDispensacion = @TipoDispensacion, @FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @TipoInsumo = @TipoInsumo, @SubFarmacias = @SubFarmacias,  
		@OrigenDispensacion = @OrigenDispensacion 


	Print 'spp_Rpt_Administrativos_002 ' 
	Exec spp_Rpt_Administrativos_002
		@TipoDispensacion = @TipoDispensacion, @FechaInicial = @FechaInicial, @FechaFinal = @FechaFinal, @TipoInsumoMedicamento = @TipoInsumoMedicamento, 
		@MostrarPrecios = @MostrarPrecios, @IncluirDetalleInformacion = @IncluirDetalleInformacion, @MostrarDevoluciones = @MostrarDevoluciones      

--------------------------------	VALIDACION 


	If @SoloDispensacion = 0 
	Begin  
	--------------------------------	DOCUMENTOS  
		Print 'spp_Rpt_Administrativos_003 ' 
		Exec spp_Rpt_Administrativos_003 
			@IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, 
			@IdPrograma, @IdSubPrograma, @TipoDispensacion, @FechaInicial, @FechaFinal, 
			@TipoInsumo, @SubFarmacias 

		Print 'spp_Rpt_Administrativos_004 ' 
		Exec spp_Rpt_Administrativos_004 @TipoDispensacion, @FechaInicial, @FechaFinal, @TipoInsumoMedicamento, @MostrarPrecios   
	--------------------------------	DOCUMENTOS  


	--------------------------------	NO SURTIDO 
		Print 'spp_Rpt_Administrativos_005 ' 
		Exec spp_Rpt_Administrativos_005 
			@IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, 
			@IdPrograma, @IdSubPrograma, @TipoDispensacion, @FechaInicial, @FechaFinal, 
			@TipoInsumo, @SubFarmacias 

		Print 'spp_Rpt_Administrativos_006 ' 
		Exec spp_Rpt_Administrativos_006 @TipoDispensacion, @FechaInicial, @FechaFinal, @TipoInsumoMedicamento, @MostrarPrecios   
	--------------------------------	NO SURTIDO 
	End 


----------------------------------	REEMPLAZO DE TITULOS DE ENCABEZADOS Y BENEFICIARIOS 
	Print 'spp_Rpt_Administrativos_007 ' 
	Exec spp_Rpt_Administrativos_007 
----------------------------------	REEMPLAZO DE TITULOS DE ENCABEZADOS Y BENEFICIARIOS 


----------------------------------	CRUZAR INFORMACION  
	If @TipoDispensacion = 2 
	Begin 
		Print 'spp_Rpt_Administrativos_008 ' 
		Exec spp_Rpt_Administrativos_008 @IdEstado, @IdCliente, @IdSubCliente 
	End 
----------------------------------	CRUZAR INFORMACION  


	Print 'spp_Rpt_Administrativos_Parametros ' 
	Exec spp_Rpt_Administrativos_Parametros  
		@IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, @TipoDispensacion, @FechaInicial, @FechaFinal,
		@TipoInsumo, @TipoInsumoMedicamento, @SubFarmacias


	Print 'spp_Rpt_Administrativos_ConcentradoInsumos ' 
	Exec spp_Rpt_Administrativos_ConcentradoInsumos @Ordenamiento = @Ordenamiento 


	Print 'spp_Rpt_Administrativos_VentasPorClaveMensual ' 
	Exec spp_Rpt_Administrativos_VentasPorClaveMensual -- Tablas Cruzadas por Mes  


	Print 'spp_Rpt_Administrativos_VentasPorClaveMensualProgramas ' 
	Exec spp_Rpt_Administrativos_VentasPorClaveMensualProgramas -- Tablas Cruzadas por Mes por Programa 
	
	
--- 	
	
End 
Go--#SQL 
