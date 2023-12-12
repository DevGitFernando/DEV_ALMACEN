Set NoCount On   
Go--#SQL 

Declare 
	@BorrarCatalogo bit 

	Set @BorrarCatalogo = 0  
	
	

	If Exists ( Select Name From tempdb..sysobjects Where Name like '#tmpTabla_Registros%' and xType = 'U' ) 
	   Drop Table tempdb..#tmpTabla_Registros 
	

	If Exists ( Select Name From tempdb..sysobjects Where Name like '#tmpLista_Tablas%' and xType = 'U' ) 
	   Drop Table tempdb..#tmpLista_Tablas 


	If Exists ( Select Name From sysobjects Where Name like '#tmpLista_Tablas%' and xType = 'U' ) 
	   Drop Table #tmpLista_Tablas 


	select Top 0 cast('' as varchar(500)) as Tabla, 0 as Registros 
	Into #tmpTabla_Registros 



	Select 
		NombreTabla, getdate() as Inicio, getdate() as Final, 0 as Error, 
		--0 as Registros, 
		identity(int, 1, 1) as Orden 
	Into #tmpLista_Tablas 
	From CFGC_EnvioDetallesTrans 
	where 1 = 0 
	Order by IdOrden Desc 

	----Insert Into #tmpLista_Tablas ( NombreTabla ) Select 'MovtosInv_ADT' 
	----Insert Into #tmpLista_Tablas ( NombreTabla ) Select 'CatBeneficiarios_Historico' 


	--Insert Into #tmpLista_Tablas ( NombreTabla, Inicio, Final, Error ) Select 'CFG_ClavesSSA_Precios_Cajas' as NombreTabla, getdate() as Inicio, getdate() as Final, 0 as Error
	Insert Into #tmpLista_Tablas ( NombreTabla, Inicio, Final, Error ) Select 'CFG_CostosPromediosPrecios' as NombreTabla, getdate() as Inicio, getdate() as Final, 0 as Error  
	Insert Into #tmpLista_Tablas ( NombreTabla, Inicio, Final, Error ) Select 'CFG_Svr_UnidadesRegistradas' as NombreTabla, getdate() as Inicio, getdate() as Final, 0 as Error  



	Insert Into #tmpLista_Tablas ( NombreTabla, Inicio, Final, Error ) 
	Select NombreTabla, getdate() as Inicio, getdate() as Final, 0 as Error 
	From CFGC_EnvioDetallesTrans   
	Order by IdOrden Desc 

	Insert Into #tmpLista_Tablas ( NombreTabla, Inicio, Final, Error ) 
	Select NombreTabla, getdate() as Inicio, getdate() as Final, 0 as Error 
	From CFGC_EnvioDetalles 
	Order by IdOrden Desc 
	
	Delete From #tmpLista_Tablas Where NombreTabla = 'Net_Usuarios' 
	--Delete From #tmpLista_Tablas Where NombreTabla = 'Net_CFGC_Parametros' 
	Delete From #tmpLista_Tablas Where NombreTabla = 'Movtos_Inv_Tipos_Farmacia' 

	Delete From #tmpLista_Tablas Where NombreTabla = 'CatPasillos_Estantes_Entrepaños' 
	Delete From #tmpLista_Tablas Where NombreTabla = 'CatPasillos_Estantes' 
	Delete From #tmpLista_Tablas Where NombreTabla = 'CatPasillos' 
	
		
	If @BorrarCatalogo = 1 
	Begin 
		Insert Into #tmpLista_Tablas ( NombreTabla, Inicio, Final, Error ) 
		Select NombreTabla, getdate() as Inicio, getdate() as Final, 0 as Error 
		From CFGS_EnvioCatalogos 
		Order by IdOrden Desc 	
		
		Delete From #tmpLista_Tablas Where NombreTabla = 'CFGS_EnvioCatalogos' 
		Delete From #tmpLista_Tablas Where NombreTabla = 'CFGS_ConfigurarConexiones' 
		Delete From #tmpLista_Tablas Where NombreTabla = 'CFGC_EnvioDetallesTrans' 	
		Delete From #tmpLista_Tablas Where NombreTabla = 'CFGC_EnvioDetalles' 	
		
		----Delete From #tmpLista_Tablas Where NombreTabla = 'Movtos_Inv_Tipos' 	
		----Delete From #tmpLista_Tablas Where NombreTabla = 'Movtos_Inv_Tipos_Farmacia' 		
		 
		
		Delete From #tmpLista_Tablas Where NombreTabla = 'Net_Arboles' 	
		Delete From #tmpLista_Tablas Where NombreTabla = 'Net_Navegacion' 			
						
	End 
					
--	select * from #tmpLista_Tablas where NombreTabla = 'DevolucionTransferenciasEnc' 

---------------------------------------------------------------------------  
/* 
	Select * 
	From #tmpLista_Tablas 
	Order by Orden 
*/ 


Set NoCount On  
Go--#SQL 



Declare 
	@sSql varchar(max), 
	@sTabla varchar(200),  
	@bEjecutar bit,  
    @IdFarmacia varchar(10), 
    @FechaDesde varchar(10),   
    @sFiltroDeFecha varchar(max) 
    
	Set @sSql = '' 
	Set @sTabla = '' 	 
	Set @IdFarmacia = '5000' 

	Set @FechaDesde = '' 	------- Dejar en vacio si se desa borrar toda la informacion de la unidad  
	Set @sFiltroDeFecha = '' 
	
	If @FechaDesde <> '' 
		Set @sFiltroDeFecha = ' and convert(varchar(10), FechaControl, 120) >= ' + char(39) + @FechaDesde + char(39) 
	

	Set @bEjecutar = 1        
	--  0	====> Generar solo script 
	--  1	====> Ejecutar script 


	Delete From CatBeneficiarios_Historico 
	Delete From CatMedicos_Direccion 
	Delete from CatMedicos_Historico 


    Declare #cLimpiar 
    Cursor For 
		Select NombreTabla 
		From #tmpLista_Tablas   
		Order by Orden   
    Open #cLimpiar 
    FETCH NEXT FROM #cLimpiar Into @sTabla 
        WHILE @@FETCH_STATUS = 0 
        BEGIN 

			Delete from #tmpTabla_Registros 

			Insert Into #tmpTabla_Registros ( Tabla, Registros ) Select @sTabla, 0 
			
			Set @sSql = 
				'Update T Set Registros = ( Select count(*) From ' + @sTabla + ' (NoLock) ) ' + 
				'From #tmpTabla_Registros T (NoLock) '
			Exec(@sSql) 

			If Exists ( select * From #tmpTabla_Registros Where Registros > 0 ) 
			Begin 
				Set @sSql = '' -- '----------------------------------------------------------------------------------------------------' + char(13) 
				Set @sSql = @sSql + 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + @sTabla + char(39) + ' and xType = ' + char(39) + 'U' + char(39) + ' ) ' + char(10) 
				Set @sSql = replicate('-', len(@sSql) + 5 ) + char(10) + @sSql     
				Set @sSql = @sSql + '   Delete From ' + @sTabla   
				---' Where IdFarmacia <= ' + char(39) + @IdFarmacia + char(39) + @sFiltroDeFecha + char(10)  
				--Set @sSql = @sSql + 'Go--#S#QL ' 
			
				-- getdate() as Inicio, getdate() as Final 
				if @bEjecutar = 1 
					Begin 
						Update #tmpLista_Tablas Set Inicio = getdate() Where NombreTabla = @sTabla 
						Exec ( @sSql ) 
						Update #tmpLista_Tablas Set Final = getdate(), Error = @@ERROR Where NombreTabla = @sTabla 
					End 
				else 
					Begin 
						print @sSql 
					End 
			
			End 

			FETCH NEXT FROM #cLimpiar Into @sTabla  
        END 
    Close #cLimpiar 
    Deallocate #cLimpiar  

Go--#SQL 




/*     
---------------------------------------------------------------------------------------------------------------------------------------- 
-----	Reiniciar los Keyx  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Enc' and xType = 'U' ) 
	DBCC CHECKIDENT ('MovtosInv_Enc', RESEED, 1)  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN' and xType = 'U' ) 
	DBCC CHECKIDENT ('MovtosInv_Det_CodigosEAN', RESEED, 1) 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
	DBCC CHECKIDENT ('MovtosInv_Det_CodigosEAN_Lotes', RESEED, 1) 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'U' ) 
	DBCC CHECKIDENT ('MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones', RESEED, 1) 
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Enc' and xType = 'U' ) 
	DBCC CHECKIDENT ('AjustesInv_Enc', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det' and xType = 'U' ) 
	DBCC CHECKIDENT ('AjustesInv_Det', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes' and xType = 'U' ) 
	DBCC CHECKIDENT ('AjustesInv_Det_Lotes', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes_Ubicaciones' and xType = 'U' ) 
	DBCC CHECKIDENT ('AjustesInv_Det_Lotes_Ubicaciones', RESEED, 1)
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico' and xType = 'U' ) 
	DBCC CHECKIDENT ('Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido_Distribucion' and xType = 'U' ) 
	DBCC CHECKIDENT ('Pedidos_Cedis_Det_Surtido_Distribucion', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso' and xType = 'U' ) 
	DBCC CHECKIDENT ('Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis_Enc_Surtido_Atenciones' and xType = 'U' ) 
	DBCC CHECKIDENT ('Pedidos_Cedis_Enc_Surtido_Atenciones', RESEED, 1)
Go--#SQL 



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosCartasCanje_Enc' and xType = 'U' ) 
	DBCC CHECKIDENT ('CambiosCartasCanje_Enc', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CambiosProv_Enc' and xType = 'U' ) 
	DBCC CHECKIDENT ('CambiosProv_Enc', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatClavesSSA_Sales_Historico' and xType = 'U' ) 
	DBCC CHECKIDENT ('CatClavesSSA_Sales_Historico', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Terminales_Versiones' and xType = 'U' ) 
	DBCC CHECKIDENT ('CFG_Terminales_Versiones', RESEED, 1)

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CierreInventario_Tablas_Limpiar' and xType = 'U' ) 
	DBCC CHECKIDENT ('CierreInventario_Tablas_Limpiar', RESEED, 1) 
GO--#SQL 
*/ 


    