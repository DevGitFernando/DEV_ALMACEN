If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_AdminListaClientesProgramas' and xType = 'P' ) 
   Drop Proc spp_Rpt_AdminListaClientesProgramas  
Go--#SQL 

Create Proc spp_Rpt_AdminListaClientesProgramas 
(   
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0010', 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-01-15' 
)  
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @sSql varchar(7500), 
		@sWhere varchar(2500), 
		@sWhereAux varchar(2500), 
		@sFechaValidacion varchar(50)

Declare @iSinAsignar tinyint, 
		@iMedicamento tinyint, 
		@iMedicamentoOtros tinyint, 
		@iMatCuracion tinyint, 
		@iDispVenta tinyint, 
		@iDispConsignacion tinyint, 
		@iCriterioFechaValidacion tinyint  
						  
	
------------------------------------------ 
	   

----- Crear Tabla Intermedia 
If Exists ( Select Name From Sysobjects Where Name = 'tmpDatosVentasClientesProgramas' and xType = 'U' )
      Drop Table tmpDatosVentasClientesProgramas
      
Create Table tmpDatosVentasClientesProgramas
(
     IdEmpresa varchar(3) Not Null Default '',
     Empresa varchar(500) Null Default '',
     IdEstado varchar(2) Not Null Default '',
     Estado varchar(50) Not Null Default '',
     ClaveRenapo varchar(2) Not Null Default '',
     IdMunicipio varchar(4) Not Null Default '',
     Municipio varchar(50) Not Null Default '',
     IdFarmacia varchar(4) Not Null Default '',
     Farmacia varchar(50) Not Null Default '',
     
     IdCliente varchar(4) Not Null Default '',
     NombreCliente varchar(100) Null Default '',
     IdSubCliente varchar(4) Not Null Default '',
     NombreSubCliente varchar(100) Null Default '',
     IdPrograma varchar(4) Not Null Default '',
     Programa varchar(200) Null Default '',
     IdSubPrograma varchar(4) Not Null Default '',
     SubPrograma varchar(200) Null Default '', 
     Registros int Not Null Default 0  	 
)

----- Crear Tabla Intermedia  

	Set @sSql = '' 
	Set @sWhere = '' 
	Set @sWhereAux = '' 
	Set @sFechaValidacion = ' v.FechaRegistro ' 

--- Obtener el Tipo de Validacion que maneja la Unidad 
	Select @iCriterioFechaValidacion = dbo.fg_PtoVta_FechaCriterioValidacion(@IdEstado, @IdFarmacia)	

	if @iCriterioFechaValidacion = 2 
	   Set @sFechaValidacion = ' IA.FechaReceta ' 	

	Set @sWhere =  ' Where TipoDeVenta = 2 ' +  
		  ' and v.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) +  ' and v.IdEstado = ' + char(39) + @IdEstado +  char(39) +  ' and v.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + 
		  ' and convert(varchar(10), ' + @sFechaValidacion + ', 120)  between convert(varchar(10), ' + char(39) +  @FechaInicial + char(39) + ', 120) and convert(varchar(10), ' + char(39) + @FechaFinal + char(39) + ', 120) '    


--	select top 1 * from vw_Farmacias 

-- Armar criterios para la consulta 
--	Set @sWhere = @sWhere + @sWhereAux   
	Set @sSql = ' 		
	Insert 	Into  tmpDatosVentasClientesProgramas ( IdEmpresa, IdEstado, Estado, ClaveRenapo, 
		IdMunicipio, Municipio, IdFarmacia, Farmacia, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, Registros  )    
	Select V.IdEmpresa, V.IdEstado, F.Estado, F.ClaveRenapo, F.IdMunicipio, F.Municipio, 
		V.IdFarmacia, F.Farmacia, 
		V.IdCliente, C.NombreCliente, V.IdSubCliente, C.NombreSubCliente, 
		V.IdPrograma, P.Programa, V.IdSubPrograma, P.SubPrograma, count(*) as Registros    
	From VentasEnc V (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia) 		
	Inner Join vw_Clientes_SubClientes C (NoLock) On ( V.IdCliente = C.IdCliente and V.IdSubCliente = C.IdSubCliente ) 
	Inner Join vw_Programas_SubProgramas P (NoLock) On ( V.IdPrograma = P.IdPrograma and V.IdSubPrograma = P.IdSubPrograma ) 
	Inner Join VentasInformacionAdicional IA (NoLock) 
		On ( V.IdEmpresa = IA.IdEmpresa and V.IdEstado = IA.IdEstado and V.IdFarmacia = IA.IdFarmacia and V.FolioVenta = IA.FolioVenta ) ' + 
	@sWhere +  	
	' Group by V.IdEmpresa, V.IdEstado, F.Estado, F.ClaveRenapo, F.IdMunicipio, F.Municipio, 
		V.IdFarmacia, F.Farmacia, 
		V.IdCliente, C.NombreCliente, V.IdSubCliente, C.NombreSubCliente, 
		V.IdPrograma, P.Programa, V.IdSubPrograma, P.SubPrograma   ' + 
	' order by V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma ' 
 	-- Print 	@sSql 	  
 	Exec(@sSql) 
 
	   
---------------------------------------------------------------- 
	if exists ( select * from sysobjects (nolock) Where Name = 'tmpRptAdmonListaClientesProgramas' and xType = 'U' ) 
	   Drop table tmpRptAdmonListaClientesProgramas  

	Select IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, 
		IdFarmacia, Farmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, Registros, 
		-- getdate() as FechaInicial,  getdate() as FechaFinal
		cast(@FechaInicial as datetime) as FechaInicial, cast(@FechaFinal as datetime) as FechaFinal 
	Into tmpRptAdmonListaClientesProgramas 
	From 
	( 
	Select IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, 
		IdFarmacia, Farmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, Registros 
    -- Into tmpRptAdmonDispensacion 
	From tmpDatosVentasClientesProgramas (NoLock) 
	) as T 		
 	order by IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma 
 	
----------------------  	
	Update B Set Empresa = I.Nombre 
	From tmpRptAdmonListaClientesProgramas B (NoLock) 
	Inner Join CatEmpresas I (NoLock) On ( B.IdEmpresa = I.IdEmpresa ) 
	 	
-- @FechaInicial varchar(10) = '2010-01-01', @FechaFinal	 	
 
--- Borrar tabla base 
	If Exists ( Select Name From Sysobjects (noLock) Where Name = 'tmpDatosVentasClientesProgramas' and xType = 'U' ) 
	   Drop Table tmpDatosVentasClientesProgramas 
 
End 
Go--#SQL 
	
	
		
	
