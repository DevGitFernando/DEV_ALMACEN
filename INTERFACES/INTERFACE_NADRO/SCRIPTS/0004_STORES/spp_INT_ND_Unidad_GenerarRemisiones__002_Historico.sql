------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_INT_ND_Unidad_GenerarRemisiones__002_Historico' and xType = 'P' ) 
    Drop Proc spp_INT_ND_Unidad_GenerarRemisiones__002_Historico
Go--#SQL 
  
/* 
	Exec spp_INT_ND_Unidad_GenerarRemisiones__002_Historico  
		@IdEmpresa = '003', @IdEstado = '16', @CodigoCliente = '2181428', 
		@FechaDeProceso_Inicial = '2014-12-12', @GUID = '38bd84e6-0756-4c7f-ae22-4469c1efd0f4' 

*/   
  
Create Proc spp_INT_ND_Unidad_GenerarRemisiones__002_Historico 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @CodigoCliente varchar(20) = '2181002', -- '2179105', 
    @FechaDeProceso_Inicial varchar(10) = '2015-03-01', @FechaDeProceso_Final varchar(10) = '2015-03-01',
    @GUID varchar(100) = 'c5cf57ab-03f8-4de6-a92b-ec3661d5e566', 
    @Año_Causes int = 2012, @SepararCauses int = 0   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

Declare 
	@IdFarmacia varchar(4), 
	@NombreFarmacia varchar(200), 
	@NombreEmpresa varchar(200), 	
	@Folio varchar(8), 
	@sFCBha varchar(8), 
	@sConsCButivo varchar(3), 
	@sMensaje varchar(1000), 
	@dFechaRemision datetime, 
	@sFolioRemision varchar(100)   
	
Declare 
	@Existen_Datos int 	
	
	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @dFechaRemision = cast(@FechaDeProceso_Inicial as datetime) 
	Set @sFolioRemision = right('0000' + cast(day(@dFechaRemision) as varchar), 2) 
	Set @sFolioRemision = @sFolioRemision + right('0000' + cast(month(@dFechaRemision) as varchar), 2) 
	Set @sFolioRemision = @sFolioRemision + right('0000' + cast(year(@dFechaRemision) as varchar), 2)  
	Set @Existen_Datos = 0 
	Set @NombreFarmacia = '' 
	Set @NombreEmpresa = '' 


	----Select @IdFarmacia = IdFarmacia, @NombreFarmacia =  NombreCliente 
	----From #tmpClientes F 
	----Where CodigoCliente = @CodigoCliente 	

---------------------------------------------- Obtener la informacion del proceso 
	Select * 
	Into #INT_ND__tmpRemisiones 	
	From   
	( 
		Select * 
		From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) 
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.CodigoCliente = @CodigoCliente -- V.IdFarmacia = @IdFarmacia 
			and convert(varchar(10), V.FechaRegistro, 120) 
				between @FechaDeProceso_Inicial and @FechaDeProceso_Final  -- and V.GUID = @GUID 		
	) as T  


	If @FechaDeProceso_Inicial <> @FechaDeProceso_Final
	Begin 
		Update D Set 
			-- GUID = 'CONSOLIDADO_MES', 
			GUID = 'CONSOLIDADO_MES____' + TipoDispensacion + '___' + IdAnexo,  
			FolioRemision = 'CONSOLIDADO_MES____' + TipoDispensacion + '___' + IdAnexo 
		From #INT_ND__tmpRemisiones D  
	End  			
---------------------------------------------- Obtener la informacion del proceso 	
	

----------------- Separar en base al causes 
	If @SepararCauses = 1 
		Begin 
			Update D Set Tipo = ltrim(rtrim(Tipo)) + 'N' 
			From #INT_ND__tmpRemisiones D 
			Where EsCauses = 0 
		End 
	Else 
		Begin 
			Update D Set EsCauses = 1 
			From #INT_ND__tmpRemisiones D 		
		End 
----------------- Separar en base al causes 	


--------------- Preparar la tabla de impresion 
	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_RptAdmonDispensacion' and xType = 'U' ) 
	   Drop Table INT_ND_RptAdmonDispensacion 

	Select 
		identity(int, 1, 1) as Keyx, Keyx_Anexo, Procesado, IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, 
		IdColonia, Colonia, Domicilio, ExpedidoEN, 
		IdFarmacia, Farmacia, CodigoCliente, GUID, IdAnexo, NombreAnexo, Prioridad, Modulo, NombrePrograma, FolioRemision, FolioRemision_Auxiliar, 
		Folio, TipoDispensacion, FechaRegistro, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, Departamento, IdBeneficiario, Beneficiario, FolioReferencia, 
		NumReceta, FechaReceta, StatusVenta, IdClaveSSA_Sal, EsCauses, ClaveSSA, ClaveSSA_Base, DescripcionSal, 
		ContenidoPaquete_ClaveSSA, Presentacion, ClaveSSA_ND, ClaveSSA_Mascara, Descripcion_Mascara, ManejaIva, 
		PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, FechaGeneracion, IdSubFarmacia, 
		IdProducto, CodigoEAN, ContenidoPaquete, ContenidoPaquete_Auxiliar, EsConsignacion, EsControlado, Renglon, 
		DescProducto, DescripcionCorta, TasaIva, Piezas, Agrupacion, Cantidad, Cantidad_ClaveSSA, 
		Cantidad_Comercial, Cantidad_Auxiliar, Cantidad_Proceso, TipoInsumo, TipoDeInsumo, IdMedico, Medico, CedulaMedico, 
		IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal, FechaMenor, FechaMayor, 
		NombreEncargado, NombreDirector, NombreAdministrador, EsEnResguardo, Incluir, TipoRelacion, Lote  
	Into INT_ND_RptAdmonDispensacion 	
	From #INT_ND__tmpRemisiones  
--------------- Preparar la tabla de impresion 	
	
	
	
---------------------------------------------------------- SALIDA FINAL  	
	Select GUID, 
		CodigoCliente, Prioridad, IdAnexo, NombreAnexo, TipoDispensacion as Tipo, FolioRemision 
	From #INT_ND__tmpRemisiones D 
	-- Where D.IdAnexo in ( '1.3.3.1', '1.1.5'  ) 
	Group by GUID, CodigoCliente, Prioridad, IdAnexo, NombreAnexo, TipoDispensacion, FolioRemision 
	Having sum(D.Cantidad) > 0 
	Order by Prioridad, IdAnexo 


	Select D.GUID, D.TipoDispensacion, D.TipoInsumo, D.EsEnResguardo -- , R.EsCauses  	
	From #INT_ND__tmpRemisiones D (NoLock) 
	--Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx ) 	 
	--Where D.IdAnexo in ( '1.3.3.1', ''  ) 
	Group by D.GUID, D.TipoDispensacion, D.TipoInsumo, D.EsEnResguardo -- , R.EsCauses  
	Having sum(D.Cantidad) > 0 	
	Order By D.EsEnResguardo, D.TipoDispensacion, D.TipoInsumo -- , R.EsCauses 


	Select D.* 	
	From #INT_ND__tmpRemisiones D (NoLock) 
	--Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx ) 	 
	Where D.Cantidad > 0  --and 
		-- D.IdAnexo in ( '1.3.3.1', '1.1.5'  ) 
		-- R.Prioridad >= 20 
	Order By D.EsEnResguardo, D.Prioridad, D.TipoDispensacion, D.TipoInsumo		
		
		
	----------------------- Analizar el número total de remisiones a generar 	
	Select 
		D.GUID, 
		D.CodigoCliente, D.Prioridad, D.IdAnexo, D.NombreAnexo, D.TipoDispensacion as Tipo, D.FolioRemision, 
		D.TipoDispensacion, D.TipoInsumo, D.EsEnResguardo -- , R.EsCauses  	
	From #INT_ND__tmpRemisiones D (NoLock) 
	-- Inner Join #INT_ND__tmpRemisiones R On ( D.Keyx = R.Keyx ) 	 
	Group by 
		D.GUID, 
		D.CodigoCliente, D.Prioridad, D.IdAnexo, D.NombreAnexo, D.TipoDispensacion, D.FolioRemision, 
		D.TipoDispensacion, D.TipoInsumo, D.EsEnResguardo -- , R.EsCauses  	
	Having sum(D.Cantidad) > 0 	
	Order By D.EsEnResguardo, D.Prioridad, D.TipoDispensacion, D.TipoInsumo -- , R.EsCauses 
			
---------------------------------------------------------- SALIDA FINAL  
	
---		spp_INT_ND_Unidad_GenerarRemisiones__002_Historico  	

	
End  
Go--#SQL 

