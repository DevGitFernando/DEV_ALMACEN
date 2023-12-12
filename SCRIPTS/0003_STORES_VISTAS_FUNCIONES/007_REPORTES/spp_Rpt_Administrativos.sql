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
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1187', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005', 
	@IdPrograma varchar(4) = '0002', @IdSubPrograma varchar(4) = '0001',  
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2012-11-16', @FechaFinal varchar(10) = '2012-11-30', 
	@TipoInsumo tinyint = 0, @TipoInsumoMedicamento tinyint = 0, @SubFarmacias varchar(200) = '' -- '06'  
)
As 
Begin 
Set NoCount On 

	if @TipoInsumoMedicamento <>  0 
           Set @TipoInsumo = 1

--------------------------------	VALIDACION 
	Exec spp_Rpt_Administrativos_001 
		@IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, 
		@IdPrograma, @IdSubPrograma, @TipoDispensacion, @FechaInicial, @FechaFinal, 
		@TipoInsumo, @SubFarmacias 

	Exec spp_Rpt_Administrativos_002 @TipoDispensacion, @FechaInicial, @FechaFinal, @TipoInsumoMedicamento  
--------------------------------	VALIDACION 


--------------------------------	DOCUMENTOS  
	Exec spp_Rpt_Administrativos_003 
		@IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, 
		@IdPrograma, @IdSubPrograma, @TipoDispensacion, @FechaInicial, @FechaFinal, 
		@TipoInsumo, @SubFarmacias 

	Exec spp_Rpt_Administrativos_004 @TipoDispensacion, @FechaInicial, @FechaFinal, @TipoInsumoMedicamento  
--------------------------------	DOCUMENTOS  


--------------------------------	NO SURTIDO 
	Exec spp_Rpt_Administrativos_005 
		@IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, 
		@IdPrograma, @IdSubPrograma, @TipoDispensacion, @FechaInicial, @FechaFinal, 
		@TipoInsumo, @SubFarmacias 

	Exec spp_Rpt_Administrativos_006 @TipoDispensacion, @FechaInicial, @FechaFinal, @TipoInsumoMedicamento  
--------------------------------	NO SURTIDO 


--------------------------------	REEMPLAZO DE TITULOS DE ENCABEZADOS Y BENEFICIARIOS 
	Exec spp_Rpt_Administrativos_007 
--------------------------------	REEMPLAZO DE TITULOS DE ENCABEZADOS Y BENEFICIARIOS 


--------------------------------	CRUZAR INFORMACION  
	If @TipoDispensacion = 2 
	Begin 
		Exec spp_Rpt_Administrativos_008 @IdEstado, @IdCliente, @IdSubCliente 
	End 
--------------------------------	CRUZAR INFORMACION  



	Exec spp_Rpt_Administrativos_Parametros  
		@IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, @TipoDispensacion, @FechaInicial, @FechaFinal,
		@TipoInsumo, @TipoInsumoMedicamento, @SubFarmacias


    Exec spp_Rpt_Administrativos_ConcentradoInsumos 

	Exec spp_Rpt_Administrativos_VentasPorClaveMensual -- Tablas Cruzadas por Mes 
    ----   Rpt_DispensacionPorClaveMensual  

	Exec spp_Rpt_Administrativos_VentasPorClaveMensualProgramas -- Tablas Cruzadas por Mes por Programa 
    ----   Rpt_DispensacionPorClaveMensual_Programas  	
	
	
--- 	
	
End 
Go--#SQL 
