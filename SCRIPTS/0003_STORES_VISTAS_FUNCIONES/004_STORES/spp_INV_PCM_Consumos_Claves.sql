/* 

	Select top 0 IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, 
		cast(0 as int) as Consumo, cast(0 as int) as ConsumoMensual, cast(0 as int) as StockSugerido 
	Into #tmpPCM  
	From ADMI_Consumos_Claves  

*/ 

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) where Name = 'spp_INV_PCM_Consumos_Claves' and xType = 'P' ) 
   Drop Proc spp_INV_PCM_Consumos_Claves 
Go--#SQL  

---		Exec spp_INV_PCM_Consumos_Claves '001', '21', '0224', '001', '2012-05-31', 1, 1, 1   

Create Proc spp_INV_PCM_Consumos_Claves 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0224', 
	@IdJurisdiccion varchar(3) = '006', 
	@FechaRevision varchar(10) = '2012-05-31', 
	@MesRevision int = 12, @MesesConsumo int = 3, 
	@TipoInformacion int = 1, @TablaDeProceso varchar(100) = ''       
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
--Set NoCount On 

Declare 
	@sSql varchar(2000) 

Declare 
	@FechaInicial varchar(10), 
	@FechaFinal varchar(10),   
	@Fecha datetime 
	
	
Declare 
	@Empresa varchar(200), 
	@Estado varchar(200), 
	@Farmacia varchar(200) 	


	Set @Fecha = cast(@FechaRevision  as datetime) 
	Set @FechaInicial = right('0000' + cast(datepart(yy, convert(varchar(10), dateadd(month, @MesRevision * -1, @Fecha), 120)) as varchar), 4)   
	Set @FechaInicial = @FechaInicial + right('0000' + cast(datepart(mm, convert(varchar(10), dateadd(month, @MesRevision * -1, @Fecha), 120)) as varchar), 2)   

	Set @FechaFinal = right('0000' + cast(datepart(yy, convert(varchar(10), @Fecha, 120)) as varchar), 4)   	
	Set @FechaFinal = @FechaFinal + right('0000' + cast(datepart(mm, convert(varchar(10), @Fecha, 120)) as varchar), 2)   		
	
		
	Select @Empresa = Nombre From CatEmpresas (NoLock) where IdEmpresa = @IdEmpresa 
	Select @Estado = Estado, @Farmacia = Farmacia From vw_Farmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
--	Select @FechaRevision, @Fecha, @FechaInicial, @FechaFinal   

--		spp_INV_PCM_Consumos_Claves 


--- Informacion concentrada  
	Select IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, sum(Cantidad) as Consumo, 
		cast(0 as int) as ConsumoMensual, 
		cast(0 as int) as StockSugerido 
	into #tmpConsumos 
	From ADMI_Consumos_Claves  
	where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
		  and IdJurisdiccion = @IdJurisdiccion  --and IdFarmacia not in ( @IdFarmacia ) 
		  and 
		  (Right('0000' + cast(A�o as varchar), 4) + Right('0000' + cast(Mes as varchar), 2)) between @FechaInicial and @FechaFinal 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA 
	Order by IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA 
	
	Delete From #tmpConsumos Where IdEstado = @IdEstado and IdFarmacia in ( 4, 182 ) 


--- Procesos 
	Update R Set 
		ConsumoMensual = round((Consumo / (@MesRevision*1.0)), 0), 
		StockSugerido = round(((Consumo / (@MesRevision*1.0)) * @MesesConsumo), 0)
	From #tmpConsumos R 	
	
	Delete From #tmpConsumos Where StockSugerido = 0 
	
------------------- SALIDA 
--	Select * 	From #tmpConsumos  	
	-- Where ClaveSSA = '624' 
	
----	If Not Exists ( Select Name From tempdb..sysobjects (noLock) Where Name  = '#tmpPCM' and xType = 'U')   
----	Begin 
----		Select top 0 IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, 
----			cast(0 as int) as Consumo, cast(0 as int) as ConsumoMensual, cast(0 as int) as StockSugerido 
----		Into #tmpPCM  
----		From ADMI_Consumos_Claves 	
----	End 
	
	If Exists ( Select Name From sysobjects (noLock) Where Name = @TablaDeProceso and xType = 'U')   
	Begin 	
		Set @sSql = 'Delete From ' + @TablaDeProceso 
		Set @sSql = @sSql + ' Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdJurisdiccion = ' + char(39) + @IdJurisdiccion + char(39)   
		Exec(@sSql) 
		
		Set @sSql = @sSql + ' Insert Into ' + @TablaDeProceso + ' ( IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, Consumo, ConsumoMensual, StockSugerido ) '  
		Set @sSql = @sSql + ' Select IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, Consumo, ConsumoMensual, StockSugerido  '   
		Set @sSql = @sSql + ' From #tmpConsumos '   
		Exec(@sSql) 
		
	End 
	
	
	
--		spp_INV_PCM_Consumos_Claves 	
	
End 
Go--#SQL 

	