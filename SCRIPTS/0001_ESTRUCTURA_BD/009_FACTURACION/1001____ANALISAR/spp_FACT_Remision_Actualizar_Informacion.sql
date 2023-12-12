---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remision_Actualizar_Informacion' and xType = 'P' )
    Drop Proc spp_FACT_Remision_Actualizar_Informacion
Go--#SQL 
  
---		Exec spp_FACT_Remision_Actualizar_Informacion @Identificador_UUID = 'XX'

Create Proc spp_FACT_Remision_Actualizar_Informacion 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '411', 
	@Detallado bit = 1, @Aplicar_Mascara bit = 0, @Identificador_UUID varchar(500) = '' 
) 
With Encryption		
As 
Begin 
Set NoCount On 
Declare 
	@Anexar__Lotes_y_Caducidades bit, 
	@EsInsumos bit, 	
	@EsMedicamento bit, 
	@sUnidadDeMedida varchar(100) 
	
Declare 
	@Keyx int, 
	@IdClaveSSA varchar(50), 
	@ClaveSSA varchar(50), 
	@ClaveSSA_Descripcion varchar(50), 
	@Informacion varchar(max), 
	@IdCliente varchar(10), 
	@IdSubCliente varchar(10)   	

--------- Obtener datos iniciales 	
	Set @sUnidadDeMedida = ''
	Set @Keyx = 0 
	Set @ClaveSSA = '' -- 'SERVICIO' 
	Set @IdClaveSSA = '' -- 'SERVICIO' 
	Set @ClaveSSA_Descripcion = '' -- 'SERVICIO DE DISPENSACIÓN' -- DE MEDICAMENTO Y MATERIAL DE CURACIÓN' 
	Set @Informacion = '' 
	Set @EsInsumos = 0  
	Set @EsMedicamento = 0  	

	Set @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Set @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Set @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
	Set @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 
	Set @IdCliente = '' 
	Set @IdSubCliente = '' 


	Select	
		* 
	From FACT_Remisiones E (NoLock)	
	--Inner Join FACT_Remisiones_Resumen D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmaciaGenera And E.FolioRemision = D.FolioRemision )
	Inner Join FACT_Remisiones_Detalles D (NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmaciaGenera And E.FolioRemision = D.FolioRemision ) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision	
	Group by E.IdEmpresa, E.IdEstado, E.IdFarmaciaGenera, E.TipoDeRemision, D.ClaveSSA, E.TipoDeRemision, E.TipoInsumo    
	-- Having sum(D.Cantidad_Agrupada) = 0  



End
Go--#SQL
