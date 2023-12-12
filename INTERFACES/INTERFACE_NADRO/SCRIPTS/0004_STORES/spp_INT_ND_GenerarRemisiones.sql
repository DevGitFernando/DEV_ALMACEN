------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarRemisiones' and xType = 'P' ) 
    Drop Proc spp_INT_ND_GenerarRemisiones
Go--#SQL 

/* 
  
Exec spp_INT_ND_GenerarRemisiones  @IdEmpresa = '003', @IdEstado = '16', @CodigoCliente = '2181002', 
	@FechaDeProceso = '2015-01-02', @GUID = '123c6b4b-c202-4944-9197-a4f217ae9b9e', @IdFarmacia = '0011', @Año_Causes = '2014'  

*/ 
  
Create Proc spp_INT_ND_GenerarRemisiones 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @CodigoCliente varchar(20) = '1788213', -- '2179105', 
    @FechaDeProceso varchar(10) = '2015-05-30', @GUID varchar(100) = '3B9B9429-498D-44A0-B22E-F5D7792E2D2D', 
    @IdFarmacia varchar(4) = '0104', @Año_Causes int = 2012,  @TipoDeProceso int = 1, @SepararCauses int = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

Declare 
	-- @IdFarmacia varchar(4), 
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
	Set @dFechaRemision = cast(@FechaDeProceso as datetime) 
	Set @sFolioRemision = right('0000' + cast(day(@dFechaRemision) as varchar), 2) 
	Set @sFolioRemision = @sFolioRemision + right('0000' + cast(month(@dFechaRemision) as varchar), 2) 
	Set @sFolioRemision = @sFolioRemision + right('0000' + cast(year(@dFechaRemision) as varchar), 2)  
	Set @Existen_Datos = 0 
	Set @NombreFarmacia = '' 
	Set @NombreEmpresa = '' 


------------------------------------------  GENERAR BASE DEL REPORTE   
------------------------------------------  GENERAR BASE DEL REPORTE   


------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	
------------------------ Obtener las ventas 
	----If Exists ( Select * From tempdb..Sysobjects (NoLock) Where Name like '#INT_ND__tmpRemisiones%' and xType = 'U' ) 
	   ----Drop Table tempdb..#INT_ND__tmpRemisiones  
	   
	Select 
		@GUID as GUID, 
		@NombreEmpresa as Empresa, 
		IdEstado, @IdFarmacia as IdFarmacia, NombreFarmacia, 
		@CodigoCliente as CodigoCliente, Modulo, 
		FechaRemision, 
		NombrePrograma, 			
		IdAnexo, 
		NombreAnexo, 	
		Prioridad, 
		Tipo,  
		FolioDispensacion, 
		FolioRemision, 
		EsCauses, 
		TipoDeClave, TipoDeClaveDescripcion, 
		ClaveSSA as ClaveSSA_Original,  
		ClaveSSA_Base, ClaveSSA,		
		ClaveSSA_ND, ClaveSSA_Mascara, 
		Descripcion_Mascara, 	
		SUM(Piezas) as Piezas, 
		sum(Cantidad) as Cantidad, 
		ManejaIva, 
		PrecioVenta, PrecioServicio, 
		sum(SubTotal) as SubTotal, 
		sum(Iva) as Iva, 
		sum(ImporteTotal) as ImporteTotal, 		
		1 as Procesado, 
		EsEnResguardo as EnResguardo, 
		Incluir, 
		TipoRelacion, 
		'' as IdSubFarmacia, 
		identity(int, 1, 1) as Keyx, 0 as Keyx_Anexo   
	Into #INT_ND__tmpRemisiones 	
	From   
	(
		Select 	
			IdEstado, IdFarmacia, 
			Farmacia as NombreFarmacia, CodigoCliente, Modulo, 
			Folio as FolioDispensacion, 
			FechaRegistro as FechaRemision, NombrePrograma, 			
			IdAnexo, NombreAnexo, Prioridad, TipoDispensacion as Tipo,  
			FechaRegistro, 
			FolioRemision, EsCauses, '' as TipoDeClave, '' as TipoDeClaveDescripcion, 
			ClaveSSA, ClaveSSA_Base, 	
			ClaveSSA_ND, ClaveSSA_Mascara, 
			Descripcion_Mascara, 	
			Piezas, 
			Cantidad, 
			ManejaIva, 
			EsEnResguardo, Incluir, TipoRelacion, 
			PrecioVenta, PrecioServicio, 
			SubTotal, Iva, ImporteTotal   	
		From INT_ND_RptAdmonDispensacion_Detallado__General V (NoLock) 		
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia --- and L.IdSubFarmacia = 5 
			and convert(varchar(10), V.FechaRegistro, 120) = @FechaDeProceso 
			and EsEnResguardo = 0 and Incluir = 1 and Cantidad > 0 	
			-- and ClaveSSA = '010.000.5186.00' 
	) as T 
	Group by  
		IdEstado, IdFarmacia, NombreFarmacia, 
		Modulo, 
		FolioDispensacion, 
		FechaRemision, 
		NombrePrograma, 			
		IdAnexo, 
		NombreAnexo, 	
		Prioridad, 
		Tipo,  
		FolioRemision, 
		EsCauses, 
		TipoDeClave, TipoDeClaveDescripcion, 	
		ClaveSSA_Base, ClaveSSA,		
		ClaveSSA_ND, ClaveSSA_Mascara, 
		Descripcion_Mascara, ManejaIva, 
		EsEnResguardo, Incluir, TipoRelacion, 
		PrecioVenta, PrecioServicio    	
	Order By Prioridad, Tipo, FolioRemision, FolioDispensacion, Descripcion_Mascara   


-------- Aplica solo para las remisiones TXT 	
----	Update D Set Tipo = ltrim(rtrim(Tipo)) + 'N' 
----	From #INT_ND__tmpRemisiones D 
----	Where EsCauses = 0 
	
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



-------------------------------------------------- SALIDA FINAL  
 
	If @GUID <> '' 
	Begin 
		--		drop table INT_ND__Dispensacion  
		
		-- Print @GUID 
		If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__Dispensacion' and xType = 'U' ) 
		Begin 
			Select 	
				GUID, Empresa, CodigoCliente, IdFarmacia, NombreFarmacia, Modulo, 
				FechaRemision, NombrePrograma, IdAnexo, NombreAnexo, Prioridad, Tipo, FolioRemision, EsCauses, 
				TipoDeClave, TipoDeClaveDescripcion, 
				ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_Mascara, 
				Descripcion_Mascara, Piezas, Cantidad, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, Procesado, Incluir, 
				TipoRelacion, 
				0 as EnResguardo, 0 as Keyx, Keyx_Anexo, getdate() as FechaGeneracion    
			Into INT_ND__Dispensacion 
			From #INT_ND__tmpRemisiones 
			Where 1 = 0  
		End 
	
	
		-- Delete From INT_ND__Dispensacion Where convert(varchar(10), FechaGeneracion, 120) < convert(varchar(10), getdate(), 120)
		-- Delete From INT_ND__Dispensacion Where GUID = @GUID 
		Delete From INT_ND__Dispensacion Where CodigoCliente = @CodigoCliente and FechaRemision = @FechaDeProceso 
		
		
		Insert Into INT_ND__Dispensacion 
		( 
			GUID, Empresa, CodigoCliente, IdFarmacia, NombreFarmacia, Modulo, 
			FechaRemision, NombrePrograma, IdAnexo, NombreAnexo, Prioridad, Tipo, FolioRemision, EsCauses, 
			TipoDeClave, TipoDeClaveDescripcion, 
			ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_Mascara, 
			Descripcion_Mascara, Piezas, Cantidad, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, Procesado, Incluir, 
			EnResguardo, TipoRelacion, Keyx, Keyx_Anexo, FechaGeneracion	
		) 
		Select 
			GUID, Empresa, CodigoCliente, IdFarmacia, NombreFarmacia, Modulo, 
			FechaRemision, NombrePrograma, IdAnexo, NombreAnexo, Prioridad, Tipo, FolioRemision, EsCauses, 
			TipoDeClave, TipoDeClaveDescripcion, 
			ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_Mascara, 
			Descripcion_Mascara, Piezas, Cantidad, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, Procesado, Incluir,  			
			EnResguardo, TipoRelacion, Keyx, Keyx_Anexo, getdate() as FechaGeneracion    	
		From #INT_ND__tmpRemisiones 
		
	End 
 

	
	
	Select 
		IdFarmacia, NombreFarmacia, 
		CodigoCliente, Modulo, 
		replace(convert(varchar(10), FechaRemision, 120), '-', '') as FechaRemision, 
		-- FechaRemision, 
		Prioridad, IdAnexo, NombreAnexo, Tipo, FolioRemision, 
		EsCauses, 
		IdSubFarmacia, 
		ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_Mascara, Descripcion_Mascara, 
		Cantidad, ManejaIva, PrecioVenta, PrecioServicio, SubTotal, Iva, ImporteTotal, Procesado, Keyx, Keyx_Anexo, 
		EnResguardo, Incluir, 
		replace(convert(varchar(10), @FechaDeProceso, 120), '-', '') as FechaGeneracion  
	From #INT_ND__tmpRemisiones 
	Where -- Procesado = 1  
		Incluir = 1 and 
		Cantidad > 0 
	-- Order by Prioridad, EnResguardo desc, Procesado, IdAnexo, Descripcion_Mascara   
	Order By Prioridad, Tipo, FolioRemision, FolioDispensacion, Descripcion_Mascara   


-------------------------------------------------- SALIDA FINAL  
	
---		spp_INT_ND_GenerarRemisiones  	

	
End  
Go--#SQL 

