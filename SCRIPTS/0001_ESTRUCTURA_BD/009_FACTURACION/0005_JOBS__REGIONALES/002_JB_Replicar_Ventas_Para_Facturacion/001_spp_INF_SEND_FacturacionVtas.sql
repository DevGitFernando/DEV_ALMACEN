------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'spp_INF_SEND_FacturacionVtas' and xType = 'P' )
	Drop Proc spp_INF_SEND_FacturacionVtas 
Go--#SQL 
 
---  Exec spp_INF_SEND_FacturacionVtas 'SII_PtoVta_EnBlanco', '[CENTRAL].[SII_OficinaCentral]', 'SII_PtoVta_EnBlanco'

-- Exec spp_INF_SEND_FacturacionVtas 'SII_OficinaCentral', '[SVRPUEBLA].[SII_RegionalPuebla]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 

-- Exec spp_INF_SEND_FacturacionVtas 'SII_OficinaCentral', '[SVROAXACA].[SII_Oaxaca]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 


Create Proc spp_INF_SEND_FacturacionVtas 
(
    @BaseDeDatosOrigen varchar(100) = 'SII_Regional_Hidalgo_20170425', 
	@BaseDeDatosDestino varchar(100) = 'SII_Facturacion_Hidalgo', 
	@BaseDeDatosEstructura varchar(100) = 'SII_Regional_Hidalgo_20170425', 
	@TablaDeControl varchar(100) = 'CFGSC_EnvioDetallesVentas',
	@FechaCorte varchar(10) = '2017-03-01',  
	@Ejecutar int = 1  			 
)
With Encryption 
As
Begin 
Set NoCount On 
Declare @sTabla varchar(200), 
        @sSql varchar(8000), 
        @iError int, 
		@Esquema varchar(100),
		@IdEnvio int,
		@TablaControlFact varchar(100),
		@TablaCierres varchar(100),
		@ExistenDatos tinyint 		         

----Begin tran 
----print 'Begin tran' 

	Set @Esquema = '.dbo.'           
    Set @sSql = '' 
    Set @iError = 0
	Set @IdEnvio = 1
	Set @ExistenDatos = 0 

-----------------------------------	
	Set @TablaControlFact = 'Ctl_CierresDePeriodos_Facturacion'
	Set @TablaCierres = 'Ctl_CierresDePeriodos'
	
----------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------------------------
	Set @sSql = ''	

	------------------ Eliminar tabla de proceso 
	----If Exists ( select Name From tempdb..Sysobjects (NoLock) Where Name like '##Ventas_Enc_Cierres%' and xType = 'U' ) 
	----   Drop Table tempdb..##Ventas_Enc_Cierres 


	----If Exists ( select Name From tempdb..Sysobjects (NoLock) Where Name like '##Beneficiarios_Vtas%' and xType = 'U' ) 
	----   Drop Table tempdb..##Beneficiarios_Vta


	Exec spp_INF_SEND_Fact_Control @BaseDeDatosOrigen, @BaseDeDatosDestino, @BaseDeDatosEstructura, @TablaControlFact, @TablaCierres, @FechaCorte, 0, 
									@sSql output, @ExistenDatos output
	
	-- select @sSql  
	Print '' 
	Print '' 

	If @ExistenDatos = 1
		Begin			

			if exists ( select * from tempdb..sysobjects(nolock) where Name = '#CFGC_EnvioDetalles' ) 
			   drop table #CFGC_EnvioDetalles 

			Select space(200) as NombreTabla, 0 as Procesada, 0 as IdOrden, 1 as Existe Into #CFGC_EnvioDetalles Where 1 = 0 
			Set @sSql = 'Insert Into #CFGC_EnvioDetalles ' + 
				'Select NombreTabla, 0, IdOrden, 1   ' + 
				'From ' + @BaseDeDatosEstructura + @Esquema + '' + @TablaDeControl + ' T (Nolock) ' + 
				'Inner Join ' + @BaseDeDatosEstructura + @Esquema + ' Sysobjects S (NoLock) On ( T.NombreTabla = S.Name )  ' + 
				'Where T.Status = ' + char(39) + 'A' + char(39) + ' Order By T.IdOrden ' 
			Exec(@sSql) 
			


			Declare Llave_Tablas Cursor For 
				Select NombreTabla 
				From #CFGC_EnvioDetalles 
				-- Where 1 = 0 -- NombreTabla <> 'Net_Usuarios' 
				-- Where Existe = 1 
				Order By IdOrden 
			open Llave_Tablas 
			Fetch From Llave_Tablas into @sTabla 
			while @@Fetch_status = 0 and @iError = 0 
				begin 
					Set @sSql = '' 
					-- Print @sTabla 
					Exec spp_INF_SEND_Detalles_FacturacionVtas @BaseDeDatosOrigen, @BaseDeDatosDestino, @BaseDeDatosEstructura, @sTabla, @sSql output, @IdEnvio 

					If @Ejecutar = 1 
					   Exec(@sSql) 		
					Else 
					   Print @sSql 

		--			print @sSql 

					if (@@error <> 0 ) 
						begin 
						   Set @iError = 1  
						   update #CFGC_EnvioDetalles Set Procesada = 2 where NombreTabla = @sTabla  
						end 
					else 
						begin 
						   update #CFGC_EnvioDetalles Set Procesada = 1 where NombreTabla = @sTabla
						   Set @IdEnvio = @IdEnvio + 1  
						end 
				    
					Fetch next From Llave_Tablas into @sTabla 
				end 
			close Llave_Tablas  
			deallocate Llave_Tablas

			If @iError = 0 
				Begin
					Set @sSql = ''
					--Exec spp_INF_SEND_Fact_Control @BaseDeDatosOrigen, @BaseDeDatosDestino, @BaseDeDatosEstructura, @TablaControlFact, @TablaCierres, @FechaCorte, 1, 
					--								@sSql output, @ExistenDatos output

					Exec(@sSql)
				End

		End 
    

		------------------ Eliminar tabla de proceso 
		--If Exists ( select Name From tempdb..Sysobjects (NoLock) Where Name like '##Ventas_Enc_Cierres%' and xType = 'U' ) 
		--   Drop Table tempdb..##Ventas_Enc_Cierres 


		--If Exists ( select Name From tempdb..Sysobjects (NoLock) Where Name like '##Beneficiarios_Vtas%' and xType = 'U' ) 
		--   Drop Table tempdb..##Beneficiarios_Vtas 


End 
Go--#SQL 



