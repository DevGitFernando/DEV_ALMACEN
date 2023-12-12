If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rtp__DocumentosGenerados' and xType = 'P' ) 
   Drop Proc spp_FACT_Rtp__DocumentosGenerados 
Go--#SQL 

Create Proc spp_FACT_Rtp__DocumentosGenerados 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@RFC varchar(15) = 'SSEP120101XYZ', @IdTipoDocto varchar(4) = '*', 
	@PeriodoTodo bit = 0, @FechaInicial varchar(10) = '2010-09-01', @FechaFinal varchar(10) = '2012-11-10'
) 
As 
Begin 
Set NoCount On 

Declare 
	@sSql varchar(7000), 
	@sFiltro varchar(500) 

	Set @sSql = '' 
	Set @sFiltro = '' 
	
------------------------- Preparar tabla vacia 
	Select Top 0 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, FechaRegistro, IdTipoDocumento, TipoDeDocumento, 
		Serie, Folio, Importe, RFC, NombreReceptor, StatusDocto  
	Into #tmpReporte 	
	From vw_FACT_CFD_DocumentosElectronicos 

---		spp_FACT_Rtp__DocumentosGenerados  

------------------------- Armar filtro 
	Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + 
				   ' and IdEstado = ' + char(39) + @IdEstado + char(39) + 
				   ' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39)  

	If @RFC <> '' and @RFC <> '*'
	   Set @sFiltro = @sFiltro + ' and RFC = ' + char(39) + @RFC + char(39) 
	   
	If @IdTipoDocto <> '' and @IdTipoDocto <> '*'  
	   Set @sFiltro = @sFiltro + ' and IdTipoDocumento = ' + char(39) + @IdTipoDocto + char(39) 	   
	   
	if @PeriodoTodo = 0 
	Begin 
	   Set @sFiltro = @sFiltro + char(10) + space(5) + 
	       ' and convert(varchar(10), FechaRegistro, 120) ' + 
	       ' Between ' + char(39) +  @FechaInicial + char(39) + ' and ' + char(39) + @FechaFinal + char(39)  
	End 
	

------------------------- Armar filtro 


------------------------- Obtener informacion 
	Set @sSql = 'Insert Into #tmpReporte ' + char(10) + 
	'Select ' + char(10) + 
	'	IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, FechaRegistro, IdTipoDocumento, TipoDeDocumento, ' + char(10) + 
	'	Serie, Folio, Importe, RFC, NombreReceptor, StatusDocto  	' + char(10) + 
	'From vw_FACT_CFD_DocumentosElectronicos ' + char(10) + @sFiltro 	
	
	Print @sSql  
	Exec(@sSql) 


-------------------------------------------------- 
------------------------- Salida Final 
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, FechaRegistro, IdTipoDocumento, TipoDeDocumento, 
		Serie, Folio, Importe, RFC, NombreReceptor, StatusDocto, getdate() as FechaImpresion 
	From #tmpReporte 

---		spp_FACT_Rtp__DocumentosGenerados  

	-- sp_listacolumnas vw_FACT_CFD_DocumentosElectronicos

End 
Go--#SQL 



