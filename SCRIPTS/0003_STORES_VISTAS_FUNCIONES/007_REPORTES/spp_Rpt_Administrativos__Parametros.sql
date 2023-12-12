If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_Parametros' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_Parametros 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Encabezado_Rpt_Administrativos' and xType = 'U' ) 
	Drop Table Encabezado_Rpt_Administrativos 
Go--#SQL 	

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Administrativos_Parametros' and xType = 'U' ) 
	Drop Table Rpt_Administrativos_Parametros 
Go--#SQL 	


--------------------------------------------------------------------------------------  
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_Parametros' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_Parametros  
Go--#SQL 

--		spp_Rpt_Administrativos_Parametros  

Create Proc spp_Rpt_Administrativos_Parametros 
(   
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@IdPrograma varchar(4) = '0002', @IdSubPrograma varchar(4) = '1312', 
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2012-01-01', @FechaFinal varchar(10) = '2012-01-01', 
	@TipoInsumo tinyint = 1, @TipoInsumoMedicamento tinyint = 0, 
	@SubFarmacias varchar(200) = '''03'', ''04'', ''05'', ''06''' --''
	, @iEjecutar bit = 0     
) 
With Encryption 
As 
Begin 
Set NoCount On 

	-- Se crea la tabla 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Administrativos_Parametros' and xType = 'U' ) 
	   Drop Table Rpt_Administrativos_Parametros  

	Create Table Rpt_Administrativos_Parametros 
	( 
		IdEmpresa varchar(3) Not Null Default '',  
		Empresa varchar(100) Not Null Default '', 
		IdEstado varchar(2) Not Null Default '', 
		Estado varchar(100) Not Null Default '', 
		IdFarmacia varchar(4) Not Null Default '', 
		Farmacia varchar(100) Not Null Default '', 

----		FechaInicial varchar(20) Not Null Default '', 
----		FechaFinal varchar(20) Not Null Default '',  

		IdCliente varchar(4) Not Null Default '', 
		Cliente varchar(100) Not Null Default '', 		
		IdSubCliente varchar(4) Not Null Default '', 
		SubCliente varchar(100) Not Null Default '', 				

		IdPrograma varchar(4) Not Null Default '', 
		Programa varchar(100) Not Null Default '', 		
		IdSubPrograma varchar(4) Not Null Default '', 
		SubPrograma varchar(100) Not Null Default '', 		

		TipoDispensacion tinyint Not Null Default 0, 
		TipoDispensacionDesc varchar(100) Not Null Default '', 
				
		FechaInicial varchar(20) Not Null Default GetDate(), 
		FechaFinal varchar(20) Not Null Default GetDate(), 

		TipoInsumo tinyint Default 0,  
		TipoInsumoDesc varchar(100) Not Null Default '', 				
		
		TipoInsumoMedicamento tinyint Not Null Default 0, 
		TipoInsumoMedicamentoDesc varchar(100) Not Null Default '', 

		SubFarmacias varchar(1000) Not Null Default '', 
		FechaRegistro datetime Not Null Default GetDate(), 

		CadenaGeneracion varchar(1000) Not Null Default '', 

		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)
 
 
 Declare 
 	@sSql varchar(1000), 	 
	@Empresa varchar(100), 	 
	@Estado varchar(100),	@Farmacia varchar(100),   
	@FechaInicial_Datos varchar(20),	@FechaFinal_Datos varchar(20),   	
	@Cliente varchar(100), 	@SubCliente varchar(100),    		
	@Programa varchar(100), @SubPrograma varchar(100), 
	@TipoDispensacionDesc  varchar(100), @TipoInsumoDesc varchar(100),  
	@TipoInsumoMedicamentoDesc varchar(100), 
	@SubFarmaciasSeleccionadas varchar(1000),	 
	@SubFarmaciasSeleccionadas_Lista varchar(1000),	 	
	@CadenaGeneracion varchar(max)	  
 
 Declare 
	@sTodo varchar(100)  
	
 
 
	Select @Empresa = Nombre From CatEmpresas (NoLock) Where IdEmpresa = @IdEmpresa 
	Select @Estado = Estado, @Farmacia = Farmacia From vw_Farmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	Set @sTodo = 'TODOS' 
	Set @Cliente  =		@sTodo  
	Set @SubCliente  = 	@sTodo 
	Set @Programa  =	@sTodo 
	Set @SubPrograma  = @sTodo 	
	Set @TipoDispensacionDesc = 'VENTA Y CONSIGNACIÓN' 	 
	Set @TipoInsumoDesc = 'MEDICAMENTO Y MATERIAL DE CURACIÓN' 
	Set @TipoInsumoMedicamentoDesc =  '' --- @sTodo 


---	01.- Identificador de la operacion  
	Set @CadenaGeneracion = 'ID' + @IdEmpresa + @IdEstado + @IdFarmacia  


----------------------------------------- 
--------- Ciente y Sub-Cliente 
	Select @FechaInicial_Datos = min(convert(varchar(10), FechaRegistro, 120)), @FechaFinal_Datos = max(convert(varchar(10), FechaRegistro, 120)) 
	From tmpRptAdmonDispensacion (NoLock) 
	
	Set @FechaInicial_Datos = IsNull(@FechaInicial_Datos, '')  
	Set @FechaFinal_Datos = IsNull(@FechaFinal_Datos, '')  	
	-- select top 1 * from tmpRptAdmonDispensacion 

---	02.- Datos contenidos en el reporte 
	Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'FI' + replace(@FechaInicial_Datos, '-', '')  
	Set @CadenaGeneracion = @CadenaGeneracion + '' + 'FF' + replace(@FechaFinal_Datos, '-', '')  	
	
----------------------------------------- 


	
----------------------------------------- 
--------- Ciente y Sub-Cliente 
---	02.- Informacion de Cliente y Sub-Cliente  
	If @IdCliente <> '*' or @IdCliente <> '' 
	   Begin 
	   	  Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'C' + @IdCliente 
	      Select @Cliente = Nombre From CatClientes (NoLock) Where IdCliente = @IdCliente 
	      
		  If @IdSubCliente <> '*' or @IdSubCliente <> '' 
			 Begin 
			 	Set @CadenaGeneracion = @CadenaGeneracion + 'SC' + @IdSubCliente 
				Select @SubCliente = Nombre From CatSubClientes (NoLock) Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
			 End
		   Else 
		     Begin 
			 	Set @CadenaGeneracion = @CadenaGeneracion + 'SC*' 		     
		     End  	   
	   End 
	 Else 
	   Begin 
		  Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'C*SC*'  	   
	   End   
----------------------------------------- 
 
 
----------------------------------------- 
---	03.- Informacion de Programa y Sub-Programa  
--------- Programa y Sub-Programa  
 	If @IdPrograma <> '*' or @IdPrograma <> '' 
	   Begin 
	   	  Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'P' + @IdPrograma 	   
	      Select @Programa = Descripcion From CatProgramas (NoLock) Where IdPrograma = @IdPrograma 
	      
		  If @IdSubPrograma <> '*' or @IdSubPrograma <> '' 
			 Begin 
			 	Set @CadenaGeneracion = @CadenaGeneracion + 'SP' + @IdSubPrograma 			 
				Select @SubPrograma = Descripcion From CatSubProgramas (NoLock) Where IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma 
			 End 	
		   Else 
		     Begin 
			 	Set @CadenaGeneracion = @CadenaGeneracion + 'SP*' 		     
		     End  	   			    
	   End 
	 Else 
	   Begin 
		  Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'P*SP*'  	   
	   End   	   
----------------------------------------- 


----------------------------------------- 
---	04.- Informacion de Dispensacion 
	Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'TD' + cast(@TipoDispensacion as varchar) 
	   
--------- Tipo de Dispensacion  
	If @TipoDispensacion = 1 
	   Set @TipoDispensacionDesc = 'CONSIGNACIÓN' 
	   
	If @TipoDispensacion = 2 
	   Set @TipoDispensacionDesc = 'VENTA'  
----------------------------------------- 

----------------------------------------- 
---	05.- Informacion de Insumo  
	Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'TI' + cast(@TipoInsumo as varchar) 

--------- Tipo de Insumo   
	If @TipoInsumo = 1 
	Begin 
	   Set @TipoInsumoDesc = 'MEDICAMENTO' 
	   Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'TIM' + cast(@TipoInsumoMedicamento as varchar) 
	   
	   If @TipoInsumoMedicamento = 1 
		  Set @TipoInsumoMedicamentoDesc = 'SEGURO POPULAR'
	 
	   If @TipoInsumoMedicamento = 2 
	      Set @TipoInsumoMedicamentoDesc = 'NO SEGURO POPULAR'	   	   
	End    
	   
	If @TipoInsumo = 2 
	   Set @TipoInsumoDesc = 'MATERIAL DE CURACIÓN' 	    
-----------------------------------------  


-----------------------------------------  
------- Sub-Farmacias seleccionadas 
Declare  
	@sIdSubFarmacia varchar(4),   
	@sSubFarmacia varchar(100)  
	
	Select IdSubFarmacia, SubFarmacia 
	into #tmpSubFarmacias 
	From vw_Farmacias_SubFarmacias 
	Where IdEstado = @IdEstado and IdFarmacia =  @IdFarmacia 
	Order By IdSubFarmacia 
         
    Set @sIdSubFarmacia = ''      
    Set @sSubFarmacia = ''      
    Set @SubFarmaciasSeleccionadas = '' 
    Set @SubFarmaciasSeleccionadas_Lista = '' 
	If @SubFarmacias  <> '' 
	Begin 
	   Delete From #tmpSubFarmacias 
	   Set @sSql = 'Insert Into #tmpSubFarmacias ' + 
	   ' Select IdSubFarmacia, SubFarmacia 
	   From vw_Farmacias_SubFarmacias 
	   Where IdEstado = ' + char(39) + @IdEstado + char(39) + 
			 ' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + 'and IdSubFarmacia in ( ' + @SubFarmacias + ' ) ' 
	   Exec(@sSql) 
	End 
	
	
	-- Se eliminan los registros de la Farmacia que se cerro.
	Declare #cSubFarmacias Cursor For 
		Select IdSubFarmacia, (cast(IdSubFarmacia as varchar) + ' - ' + SubFarmacia) 
		From #tmpSubFarmacias 
		-- where 1 = 1 
		Order By IdSubFarmacia 
	Open #cSubFarmacias Fetch #cSubFarmacias Into @sIdSubFarmacia, @sSubFarmacia  
		While (@@Fetch_Status = 0 )  
			Begin 
				Set @SubFarmaciasSeleccionadas = @SubFarmaciasSeleccionadas + @sSubFarmacia + char(13) + char(10) -- + ', ' 
				Set @SubFarmaciasSeleccionadas_Lista = @SubFarmaciasSeleccionadas_Lista + @sIdSubFarmacia 
				Fetch #cSubFarmacias Into @sIdSubFarmacia, @sSubFarmacia 
			End		
	Close #cSubFarmacias 
	DeAllocate #cSubFarmacias 	
	
---	06.- Informacion de Sub-Farmacias 
	Set @CadenaGeneracion = @CadenaGeneracion + '|' + 'SF' + @SubFarmaciasSeleccionadas_Lista 

	
	
--	print @SubFarmaciasSeleccionadas 
--	Set @SubFarmaciasSeleccionadas = ltrim(rtrim(@SubFarmaciasSeleccionadas)) 
--	Set @SubFarmaciasSeleccionadas = left(@SubFarmaciasSeleccionadas, len(@SubFarmaciasSeleccionadas) - 1)
-----------------------------------------  
 
------------------------------------ Se insertan los parametros en la tabla
	Insert Into Rpt_Administrativos_Parametros ( 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		IdCliente, Cliente, IdSubCliente, SubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		TipoDispensacion, TipoDispensacionDesc, 
		FechaInicial, FechaFinal, 
		TipoInsumo, TipoInsumoDesc, 
		TipoInsumoMedicamento, TipoInsumoMedicamentoDesc, 
		SubFarmacias, CadenaGeneracion  
		)
	Select	
		@IdEmpresa, @Empresa, @IdEstado, @Estado, @IdFarmacia, @Farmacia, 
		@IdCliente, @Cliente, @IdSubCliente, @SubCliente, 
		@IdPrograma, @Programa, @IdSubPrograma, @SubPrograma, 
		@TipoDispensacion, @TipoDispensacionDesc, 
		@FechaInicial_Datos, @FechaFinal_Datos, 
		@TipoInsumo, @TipoInsumoDesc, 
		@TipoInsumoMedicamento, @TipoInsumoMedicamentoDesc, 
		@SubFarmaciasSeleccionadas, @CadenaGeneracion  --- @SubFarmacias



-------------------------- CAMBIO PNDJ PBL 


------------------------------- Reemplazo de Titulos	
	Update B Set IdCliente = '0001', 
		Cliente = R.Cliente, SubCliente = R.SubCliente, Programa = R.Programa, SubPrograma = R.SubPrograma
	From Rpt_Administrativos_Parametros B (NoLock) 
	Inner Join CFG_EX_Validacion_Titulos R (NoLock) 
		On (
				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma 
		   )
------------------------------- Reemplazo de Titulos		

-------------------------- CAMBIO PNDJ PBL 




	If @iEjecutar = 1 
	Begin 
		Select * 
		From Rpt_Administrativos_Parametros (NoLock) 
	End 

--		spp_Rpt_Administrativos_Parametros	

End
Go--#SQL