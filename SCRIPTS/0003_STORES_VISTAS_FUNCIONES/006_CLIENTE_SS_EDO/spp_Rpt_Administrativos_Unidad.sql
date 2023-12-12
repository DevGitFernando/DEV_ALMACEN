If exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_Unidad' and xType = 'U' ) 
	Drop table RptAdmonDispensacion_Unidad 
Go--#SQL

If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_Unidad' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_Unidad 
Go--#SQL

Create Proc spp_Rpt_Administrativos_Unidad 
(   @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0010', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001', 
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-05-31', @TipoInsumo tinyint = 0,
	@TipoInsumoMedicamento tinyint = 0, @MostrarPrecio tinyint = 0, 
	@IdBeneficiario varchar(8) = '*', @IdMedico varchar(6) = '*', @ClaveSSA varchar(50) = '*', @Folio varchar(8) = '*'
)  
With Encryption 
As 
Begin 
Set NoCount On

		if @TipoInsumoMedicamento <>  0 
           Set @TipoInsumo = 1
 

		Exec spp_Rpt_Administrativos_Unidad_Parte_001
		@IdEstado, @IdFarmacia,
		@IdCliente, @IdSubCliente, 
		@IdPrograma, @IdSubPrograma,  
		@TipoDispensacion , 
		@FechaInicial, @FechaFinal, @TipoInsumo, @IdBeneficiario, @IdMedico, @ClaveSSA, @Folio

		Exec spp_Rpt_Administrativos_Unidad_Parte_002
		@TipoDispensacion , @FechaInicial, @FechaFinal, @TipoInsumo, @MostrarPrecio

		Exec spp_Rpt_Administrativos_ConcentradoInsumos_Unidad 

End 
Go--#SQL
