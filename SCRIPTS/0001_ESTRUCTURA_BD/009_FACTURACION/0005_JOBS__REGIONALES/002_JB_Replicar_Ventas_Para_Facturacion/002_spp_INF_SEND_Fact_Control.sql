------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'spp_INF_SEND_Fact_Control' and xType = 'P' )
	Drop Proc spp_INF_SEND_Fact_Control 
Go--#SQL


Create Proc spp_INF_SEND_Fact_Control 
(
    @BaseDeDatosOrigen varchar(100) = 'SII_Regional_Hidalgo_20170425', 
	@BaseDeDatosDestino varchar(100) = 'SII_Facturacion_Hidalgo', 
	@BaseDeDatosEstructura varchar(100) = 'SII_Regional_Hidalgo_20170425', 
	@TablaControlFact varchar(100) = 'Ctl_CierresDePeriodos_Facturacion',  
	@TablaCierres varchar(100) = 'Ctl_CierresDePeriodos',	
	@FechaCorte varchar(10) = '2017-03-01',
	@TipoSalida tinyint = 0,
	@SalidaFinal varchar(8000) = '' output,
	@ExistenDatos tinyint = 0 output	 
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

		@QueryFinal varchar (8000),
		@Query varchar(8000),
		@Campo varchar(200), 
		@Campo_Aux varchar(200), 	
		@Campo_Diferencia varchar(8000), 		
		@Tipo tinyint, 
		@Padre varchar(80), 
		@Tiene_Pk bit, 
		@Parte_Exists varchar(2000),
		@Parte_Relacion_Tablas varchar(2000),	
		@Parte_Lista_Campos varchar(2000),		
		@Parte_Update varchar(8000), 
		@Parte_Update_Pura varchar(8000), 	
		@Parte_Insersion varchar(8000), 	
		@Parte_Insersion_Pura varchar(8000), 		
		@bParamVal int, @sMsj varchar(1000),    
		@Alias_Origen varchar(10),
		@Alias_Destino varchar(10),	
		@Alias_Origen_Base varchar(10),
		@Alias_Destino_Base varchar(10)	         

----Begin tran 
----print 'Begin tran' 

	Set @Esquema = '.dbo.'           
    Set @sSql = '' 
    Set @iError = 0
	Set @IdEnvio = 1 

-----------------------------------
	Set @QueryFinal = '' 
    Set @Parte_Relacion_Tablas = '' 
    Set @Parte_Lista_Campos = '' 
    Set @Parte_Insersion = '' 
    Set @Campo_Aux = '' 
    Set @Campo_Diferencia = '' 
    Set @Parte_Update_Pura = '' 
    Set @Parte_Insersion_Pura = '' 
    Set @Alias_Origen_Base = 'Bd_O'
    Set @Alias_Destino_Base = 'Bd_D' 

	--Set @Esquema = '.dbo.'
    Set @Alias_Origen = 'Bd_O.'
    Set @Alias_Destino = 'Bd_D.'	
	Set @Padre = ''
----------------------------------------
-------------  GENERAR LA RELACION y CAMPOS DE LAS TABLAS DE CIERRES Y LA DE CONTROL    ----------------------------------------------------------------------------

	-- Buscar si la tabla tiene llave primaria              
	Select 0 as IdPadre Into #tmpPadre where 1 = 0 
	Select 0 as TienePK Into #tmpPK where 1 = 0 	
	
	Set @QueryFinal = 'Set NoCount On ' + char(10) + 
	    'Insert into #tmpPadre ' +  
	    'Select so.Id  ' +
	    'From ' + @BaseDeDatosEstructura + '.dbo.Sysobjects so (NoLock) ' + 
	    'Where so.Name = ' + char(39) + @TablaControlFact + char(39) + ' and xType = ' + char(39) + 'U' + char(39) 
    Exec(@QueryFinal ) 	
	Set @Padre = ( Select IdPadre From #tmpPadre (NoLock) )

	Set @QueryFinal = 'Set NoCount On ' + char(10) + 
	    'Insert into #tmpPK ' +  
	    'Select 1  ' +
	    'From ' + @BaseDeDatosEstructura + '.dbo.Sysobjects so (NoLock) ' + 
	    'Where so.parent_obj = ' + char(39) + cast(@Padre as varchar) + char(39) + ' and xType = ' + char(39) + 'PK' + char(39) 
    Exec(@QueryFinal ) 	
	Set @Tiene_Pk = ( Select TienePK From #tmpPK (NoLock) )
	Set @Tiene_Pk = IsNull(@Tiene_Pk, 0) 
	
	
	-- Select @Tiene_Pk
	Set @QueryFinal = ''
	-- Set @Criterio = ''
	Set @Parte_Exists = ''
	Set @Campo = ''
	Set @Tipo = 0
	
----- Este Parametro se envia dependiendo el tipo de Tabla 
--	Set @ParteUpDate = 1

	If @Tiene_Pk = 1 
		begin 

			Select top 0 sc.name, Sc.ColId  
			into #tmpColumnasPK 
			From Syscolumns as sc 
			Inner Join sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
			Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
			    and Sc.status <> 128 -- Ignorar las columnas Identidad
			Order by Sc.ColId
			
			Set @QueryFinal = 'Set NoCount On ' + char(10) + 
			    'Insert into #tmpColumnasPK ' +  
			    'Select sc.name, Sc.ColId   
			    From ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc 
			    Inner Join ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
			    Where sc.id = ' + cast(@Padre as varchar) + ' and si.indid = (Select min(indid) From ' + @BaseDeDatosEstructura + '.dbo.sysindexkeys Where id = ' + cast(@Padre as varchar) + ' ) 
			        and Sc.status <> 128 
			    Order by Sc.ColId ' 
			Exec(@QueryFinal) 
--			print @QueryFinal 


            Declare Llave_Cursor Cursor For  
            Select name From #tmpColumnasPK Order by ColId 
			open Llave_Cursor 
			Fetch From Llave_Cursor into @Campo 

			Set @Parte_Exists = 'Select ' + char(39) + 'If Not Exists ( Select * From ' + @TablaControlFact + ' (NoLock)  Where ' + char(39) + ' + ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 
            Set @Parte_Exists = 'Where ' 
            Set @Parte_Relacion_Tablas = '' 

			while @@Fetch_status = 0 
				begin               
			        Set @query = @campo 
			        Set @query = @Alias_Destino  + @query + ' = ' + @Alias_Origen + @query + ''
        							
			        Fetch next From Llave_Cursor into @campo -- , @tipo 
			        If @@Fetch_status = 0 
				        begin 
					        Set @query = @query + ' and '							
				        end 
			         Else 
				        begin 
					        Set @query = @query + ' ' --- + char(13) + char(10)							
				        end 
			         Set @Parte_Exists = @Parte_Exists + @query 
			         Set @Parte_Relacion_Tablas = @Parte_Relacion_Tablas + @query					 		         
				end              
			
			close Llave_Cursor              
			deallocate Llave_cursor               
		end
	Else
		begin 
			Set @Parte_Exists = 'Select ' 
			Set @QueryFinal = @QueryFinal + @Parte_Exists 
		end 
	
	
	-- Barrer todos los campos de la tabla
	Set @QueryFinal = '' 
	
	Select top 0 sc.name, Sc.ColId    
	into #tmpColumnas 
	From Sysobjects as so 
	Inner Join Syscolumns as sc on (so.id = sc.id) 
	Where so.name = @TablaControlFact and so.xtype = 'U' and Sc.status <> 128 
	
	Set @QueryFinal = 'Set NoCount On ' + char(10) + 	
	    'Insert into #tmpColumnas ' +  
	    'Select sc.name, Sc.ColId   
	    From ' + @BaseDeDatosEstructura + '.dbo.Sysobjects as so 
	    Inner Join ' + @BaseDeDatosEstructura + '.dbo.Syscolumns as sc on (so.id = sc.id)  
	    Where so.name = ' + char(39) + @TablaControlFact + char(39) + ' and Sc.status <> 128 and so.xtype = ' + char(39) + 'U' + char(39) + ' Order by Sc.ColId ' 
	Exec(@QueryFinal) 	
--	print '_____xxxxx' + @QueryFinal
	

	Declare query_Cursor Cursor For 
	select name from #tmpColumnas  Order by ColId
	open Query_cursor Fetch From Query_cursor into @campo --, @tipo              
	Set @query = '' 	
	Set @query = char(13) + char(10) + char(39) + 'Insert Into ' + @TablaControlFact + ' ( ' -- + char(39) 
	
	-- @BaseDeDatosOrigen varchar(50) = '', @BaseDeDatosDestino  
    Set @Alias_Origen = 'Bd_O.'
    Set @Alias_Destino = 'Bd_D.' 
    	
	Set @query = 'Update ' + @Alias_Destino_Base + ' Set ' 
	
	-- Set @query = char(13) + char(10) + ' Insert Into ' + @Tabla + ' ( ' -- + char(39) 
	Set @QueryFinal = @QueryFinal + @Query 
	Set @QueryFinal = @Query 
	while @@Fetch_status = 0 
		begin      
			Set @query = @campo 
			Set @Campo_Aux = @campo 			
			Set @query = @Alias_Destino  + @query + ' = ' + @Alias_Origen + @query + '' 
			Set @Campo_Diferencia = @Campo_Diferencia + '(' + @Alias_Origen + @campo + '<>' + @Alias_Destino + @campo + ')' 
							
			Fetch next From query_cursor into @campo -- , @tipo 
			If @@Fetch_status = 0 
				begin 
					Set @query = @query + ', ' 
			        Set @Campo_Aux = @Campo_Aux + ', ' 	
			        Set @Campo_Diferencia = @Campo_Diferencia + ' OR '
				end 
			 Else 
				begin 
					Set @query = @query + ' ' --- + char(13) + char(10)
			        Set @Campo_Aux = @Campo_Aux + ' ' 						
				end 
			 Set @QueryFinal = @QueryFinal + @Query 
			 Set @Parte_Lista_Campos = @Parte_Lista_Campos + @Campo_Aux 
		end 
	close Query_cursor              
	deallocate Query_cursor 		
	
--	Print @Campo_Diferencia 

-----------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------   Insertar los cierres en tabla de control de Facturacion que no estan en la BD de Facturacion----------------------------------------
	If @TipoSalida = 0
		Begin 
			Delete From Ctl_CierresDePeriodos_Facturacion
		
			Set @Parte_Insersion = 'Set NoCount On ' + char(10) + 
				'Insert Into ' + @BaseDeDatosOrigen + @Esquema + @TablaControlFact + ' ( ' + @Parte_Lista_Campos + ' ) ' + char(13) +
				'Select ' + @Parte_Lista_Campos + char(13) + 
				'From ' + @BaseDeDatosOrigen + @Esquema + @TablaCierres + ' ' + @Alias_Origen_Base + ' (NoLock) ' + char(13) 
			   
			If @Tiene_Pk = 1     
			   Begin  
    			   Set @Parte_Insersion = @Parte_Insersion +
					'Where Not Exists ( Select * From ' + @BaseDeDatosDestino + @Esquema + @TablaCierres + ' ' + @Alias_Destino_Base + char(13) +  
					'   Where ' + @Parte_Relacion_Tablas + ' ) ' +
					' and Convert(varchar(10),' + @Alias_Origen + 'FechaCorte, 120) >= ' + char(39) + @FechaCorte + char(39)	           
			   End
			 
			Exec(@Parte_Insersion) 
			--Print @Parte_Insersion 

			--		spp_INF_SEND_Fact_Control 


			If Exists ( Select * From Ctl_CierresDePeriodos_Facturacion (Nolock) )
				Begin
					Set @sSql = '' 
					Set @ExistenDatos = 1

					Select * Into ##Ventas_Enc_Cierres From VentasEnc (Nolock) Where 1 = 0
----					If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Ventas_Enc_Cierres' and xType = 'U' ) 
----						Drop Table Ventas_Enc_Cierres

					Set @sSql = ' Insert Into ##Ventas_Enc_Cierres ' + char(10) + 
					' Select ' + @Alias_Origen + ' * From VentasEnc ' + @Alias_Origen_Base + ' (Nolock) ' +
					' Inner Join ' + @BaseDeDatosOrigen + @Esquema + @TablaControlFact + ' ' + @Alias_Destino_Base + char(13) +  
					'  On ( ' + @Parte_Relacion_Tablas + ' ) '

					Exec(@sSql)
					--Print @sSql 


					Select IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario Into ##Beneficiarios_Vtas From CatBeneficiarios (Nolock) Where 1 = 0
----					If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Ventas_Enc_Cierres' and xType = 'U' ) 
----						Drop Table Ventas_Enc_Cierres

					Set @sSql = ' Insert Into ##Beneficiarios_Vtas ' + char(10) + 
					' Select Distinct E.IdEstado, E.IdFarmacia, E.IdCliente, E.IdSubCliente, I.IdBeneficiario From VentasEnc E (Nolock) ' +
					' Inner Join VentasInformacionAdicional I (Nolock) ' +
					'     On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta )'

					Exec(@sSql)
					--Print @sSql 
				End
			
		End 

--------------------------------------------------------------------------------------------------------------------------------------------------------------
		

	
----------------------------    Insertar en la tabla de Control de Facturacion ----------------------------------------------------------------

	If @TipoSalida = 1
		Begin
			Set @Parte_Insersion = ''

			Set @Parte_Insersion = 'Set NoCount On ' + char(10) + 
				'Insert Into ' + @BaseDeDatosDestino + @Esquema + @TablaControlFact + ' ( ' + @Parte_Lista_Campos + ' ) ' + char(13) +
				'Select ' + @Parte_Lista_Campos + char(13) + 
				'From ' + @BaseDeDatosOrigen + @Esquema + @TablaControlFact + ' ' + @Alias_Origen_Base + ' (NoLock) ' + char(13) 
			   
			If @Tiene_Pk = 1     
			   Begin  
    			   Set @Parte_Insersion = @Parte_Insersion +
					'Where Not Exists ( Select * From ' + @BaseDeDatosDestino + @Esquema + @TablaControlFact + ' ' + @Alias_Destino_Base + char(13) +  
					'   Where ' + @Parte_Relacion_Tablas + ' ) ' +
					' and Exists ( Select * From ##Ventas_Enc_Cierres TE (Nolock) ' +
						' Where TE.IdEmpresa = ' + @Alias_Origen + 'IdEmpresa and TE.IdEstado = ' + @Alias_Origen + 'IdEstado ' +
						'and TE.IdFarmacia = ' + @Alias_Origen + 'IdFarmacia and TE.FolioCierre = ' + @Alias_Origen + 'FolioCierre  ) '	           
			   End

----			Exec(@Parte_Insersion)
----			Print @Parte_Insersion

		
			------------    Insertar en la tabla de Cierres en la BD de Facturacion ----------------------------------------------------------------			
			Set @Parte_Insersion = @Parte_Insersion + char(10) +
				'Set NoCount On ' + char(10) + 
				'Insert Into ' + @BaseDeDatosDestino + @Esquema + @TablaCierres + ' ( ' + @Parte_Lista_Campos + ' ) ' + char(13) +
				'Select ' + @Parte_Lista_Campos + char(13) + 
				'From ' + @BaseDeDatosOrigen + @Esquema + @TablaCierres + ' ' + @Alias_Origen_Base + ' (NoLock) ' + char(13) 
			   
			If @Tiene_Pk = 1     
			   Begin  
    			   Set @Parte_Insersion = @Parte_Insersion +
					'Where Not Exists ( Select * From ' + @BaseDeDatosDestino + @Esquema + @TablaCierres + ' ' + @Alias_Destino_Base + char(13) +  
					'   Where ' + @Parte_Relacion_Tablas + ' ) ' +
					' and Convert(varchar(10),' + @Alias_Origen + 'FechaCorte, 120) >= ' + char(39) + @FechaCorte + char(39) + '  ' +
					' and Exists ( Select * From ##Ventas_Enc_Cierres TE (Nolock) ' +
						' Where TE.IdEmpresa = ' + @Alias_Origen + 'IdEmpresa and TE.IdEstado = ' + @Alias_Origen + 'IdEstado ' +
						'and TE.IdFarmacia = ' + @Alias_Origen + 'IdFarmacia and TE.FolioCierre = ' + @Alias_Origen + 'FolioCierre  ) '		           
			   End

----			Exec(@Parte_Insersion)
----			Print @Parte_Insersion
			
			----   SE LIMPIA LA TABLA DE CONTROL			
			Set @sSql = ' Delete From ' + @BaseDeDatosOrigen + @Esquema + @TablaControlFact 
			Set @Parte_Insersion = @Parte_Insersion + char(10) + @sSql

----			Exec(@sSql)

			Set @SalidaFinal = @Parte_Insersion

			--Select @SalidaFinal
		End
		
    
End 
Go--#SQL 



