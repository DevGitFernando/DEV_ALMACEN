If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_FACT_Documentos_Emitidos' and xType = 'P' ) 
   Drop Proc spp_Rpt_FACT_Documentos_Emitidos 
Go--#SQL    

Create Proc spp_Rpt_FACT_Documentos_Emitidos  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21',  @IdFarmacia varchar(4) = '0001', 
	@Año int = 2014, @Mes int = 4, @EsGeneral int = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@Sql varchar(max), 
	@sFiltro varchar(500), 
	@iRegistros int,    
	@iBloque int, 
	@sFolio varchar(100), 
	@sFolios varchar(max) 


	Set @Sql = '' 
	Set @sFiltro = '' 
	Set @iBloque = 50  
	Set @iRegistros = 0 
	Set @sFolio = '' 
	Set @sFolios = '' 
	
	Set @sFiltro = '	Where F.IdEmpresa = ' + @IdEmpresa + ' and F.IdEstado = ' + @IdEstado + '  and F.IdFarmacia = ' + @IdFarmacia 
	If @EsGeneral = 1 
	   Set @sFiltro = '		Where F.IdEmpresa = ' + @IdEmpresa 
	
	Set @sFiltro = @sFiltro + ' and year(F.FechaRegistro) = ' + cast(@Año as varchar) + ' and month(F.FechaRegistro) = ' + cast(@Mes as varchar) 


--------------- Generar folios a procesar 
	Select identity(int, 1, 1) as Keyx, F.Keyx as Identificador 
	into #tmpListaFolios 
	From FACT_CFD_Documentos_Generados F (NoLock)    
	Left Join FACT_CFDI_XML X (NoLock) 
		On ( F.IdEmpresa = X.IdEmpresa and F.IdEstado = X.IdEstado and F.IdFarmacia = X.IdFarmacia and F.Serie = X.Serie And F.Folio = X.Folio ) 
	Where 1 = 0 
	Order by F.Keyx 

	Set @Sql = '
	Insert Into #tmpListaFolios ( Identificador ) 
	Select F.Keyx as Identificador 
	From FACT_CFD_Documentos_Generados F (NoLock)    
	Left Join FACT_CFDI_XML X (NoLock) 
		On ( F.IdEmpresa = X.IdEmpresa and F.IdEstado = X.IdEstado and F.IdFarmacia = X.IdFarmacia 
			and F.Serie = X.Serie And F.Folio = X.Folio )	' + char(13) + 
	@sFiltro + char(13) + 
	'	Order by F.Keyx ' 
	Exec ( @Sql ) 
--------------- Generar folios a procesar 


--------------- Generar cadena a procesar 
	Select top 0 identity(int, 1, 1) as Keyx, cast('' as varchar(max)) as Folios 
	into #tmpFolios 
--------------- Generar cadena a procesar 


	Declare #cursor Cursor For 
		Select Identificador 
		From #tmpListaFolios 
		Order by Keyx 
	Open #cursor
	Fetch Next From #cursor Into @sFolio 
		WHILE @@FETCH_STATUS = 0
		Begin 
        
			If @iRegistros <= @iBloque  
			Begin 
				Set @iRegistros = @iRegistros + 1 
				Set @sFolios = @sFolios + ' ' + @sFolio + ', ' 
			End 
        
			If @iRegistros >= @iBloque  
			Begin 
			   Set @iRegistros = 0 
			   Set @sFolios = ltrim(rtrim(@sFolios)) 
			   Set @sFolios = left(@sFolios, len(@sFolios)-1) 
			   Insert Into #tmpFolios ( Folios ) Select @sFolios 
			   Set @sFolios = '' 
			End 
        
           Fetch Next From #cursor Into  @sFolio
        End
    Close #cursor 
    Deallocate #cursor 


	If @iRegistros > 0 and @iRegistros < @iBloque  
	Begin 
	   Set @iRegistros = 0 
	   Set @sFolios = ltrim(rtrim(@sFolios)) 
	   Set @sFolios = left(@sFolios, len(@sFolios)-1) 
	   Insert Into #tmpFolios ( Folios ) Select @sFolios 
	   Set @sFolios = '' 
	End 


--		spp_Rpt_FACT_Documentos_Emitidos 


	Select count(*) as TotalFolios From #tmpListaFolios 
	Select * From #tmpFolios order by keyx  
--	Select * From #tmpListaFolios order by keyx 

End 
Go--#SQL 
