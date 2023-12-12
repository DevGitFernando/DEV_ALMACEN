If Exists ( Select Name From Sysobjects Where Name = 'spp_GetCampos' and xType = 'P' )
	Drop Proc spp_GetCampos
Go--#SQL 

Create Proc spp_GetCampos 
(
    @Tabla varchar(max) = 'CatFarmacias', 
	@Alias_Origen varchar(10) = 'U',
	@Alias_Destino varchar(10) = 'I',	
    @SalidaFinal varchar(max) = '' output 
)
with Encryption 
As 
Begin 
Set NoCount On 
Declare -- @Tabla varchar(50), 
	@QueryFinal varchar (max),
	@Query varchar(max),
	@Campo varchar(max), 
	@Campo_Aux varchar(max), 	
	@Campo_Diferencia varchar(max), 		
	@Tipo tinyint, 
	@Padre varchar(80), 
	@Tiene_Pk bit, 
	@Parte_Exists varchar(max),
	@Parte_Relacion_Tablas varchar(max),	
	@Parte_Lista_Campos varchar(max),		
	@Parte_Update varchar(max), 
	@Parte_Update_Pura varchar(max), 	
	@Parte_Insersion varchar(max), 	
	@Parte_Insersion_Pura varchar(max), 		
	@bParamVal int, @sMsj varchar(max),    
----	@Alias_Origen varchar(10),
----	@Alias_Destino varchar(10),	
	@Alias_Origen_Base varchar(10),
	@Alias_Destino_Base varchar(10), 
	@Esquema varchar(100), 
	@iActualizado int 	
	

--	@ParteUpDate bit 

-- Declare @sValorActualizado varchar(2)
--	Set @sValorActualizado = '1'

-- Declare @Criterio varchar(max), @ParteUpDate bit 

	Set @bParamVal = 1
	Set @sMsj = 'Error al generar  ' + char(13) + char(10)
	If @Tabla = ''
	Begin
		Set @bParamVal = 0
		Set @sMsj = @sMsj + char(13) + char(10) + 'Falta proporcionar el nombre de tabla a cual se le obtendra la estructura.'
	End

	If @bParamVal = 0
	Begin
		RaisError (@sMsj, 10,16 )
		Return
	End	

    
    Set @QueryFinal = '' 
    Set @Parte_Relacion_Tablas = '' 
    Set @Parte_Lista_Campos = '' 
    Set @Parte_Insersion = '' 
    Set @Campo_Aux = '' 
    Set @Campo_Diferencia = '' 
    Set @Parte_Update_Pura = '' 
    Set @Parte_Insersion_Pura = '' 
    

--- Set @Tabla = 'OrdenCompra_Det'
--- Inicia proceso

	-- Buscar si la tabla tiene llave primaria              
	Select 0 as IdPadre Into #tmpPadre where 1 = 0 
	Select 0 as TienePK Into #tmpPK where 1 = 0 	
	
	Set @QueryFinal = 'Set NoCount On ' + char(10) + 
	    'Insert into #tmpPadre ' +  
	    'Select so.Id  ' +
	    'From .dbo.Sysobjects so (NoLock) ' + 
	    'Where so.Name = ' + char(39) + @Tabla + char(39) + ' and xType = ' + char(39) + 'U' + char(39) 
    Exec(@QueryFinal) 	
	Set @Padre = ( Select IdPadre From #tmpPadre (NoLock) ) 

	Set @QueryFinal = 'Set NoCount On ' + char(10) + 
	    'Insert into #tmpPK ' +  
	    'Select 1  ' +
	    'From dbo.Sysobjects so (NoLock) ' + 
	    'Where so.parent_obj = ' + char(39) + cast(@Padre as varchar) + char(39) + ' and xType = ' + char(39) + 'PK' + char(39) 
    Exec(@QueryFinal) 	
	Set @Tiene_Pk = ( Select TienePK From #tmpPK (NoLock) )
	Set @Tiene_Pk = IsNull(@Tiene_Pk, 0) 
	


	-- Select @Tiene_Pk
	Set @QueryFinal = '' 
	Set @Parte_Exists = ''
	Set @Campo = ''
	Set @Tipo = 0
	
----- Este Parametro se envia dependiendo el tipo de Tabla 
--	Set @ParteUpDate = 1

	-- Print 'XX'
	Select top 0 sc.name, Sc.ColId  
	into #tmpColumnasPK 
	From Syscolumns as sc 
	Inner Join sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
	Where sc.id = @Padre and si.indid = (Select min(indid) From sysindexkeys Where id = @Padre) 
	    and Sc.status <> 128 -- Ignorar las columnas Identidad
	Order by Sc.ColId 
			

	If @Tiene_Pk = 1 
		Begin 			
			Set @QueryFinal = 'Set NoCount On ' + char(10) + 
			    'Insert into #tmpColumnasPK ' +  
			    'Select sc.name, Sc.ColId   
			    From dbo.Syscolumns as sc 
			    Inner Join dbo.sysindexkeys as si on (sc.id=si.id and sc.colid=si.colid) 
			    Where sc.id = ' + cast(@Padre as varchar) + ' and si.indid = (Select min(indid) From dbo.sysindexkeys Where id = ' + cast(@Padre as varchar) + ' ) 
			        and Sc.status <> 128 
			    Order by Sc.ColId ' 
		End 
	Else 
		Begin 
			Set @QueryFinal = 'Set NoCount On ' + char(10) + 
			    'Insert into #tmpColumnasPK ' +  
			    'Select sc.name, Sc.ColId   
			    From dbo.Syscolumns as sc  
			    Where sc.id = ' + cast(@Padre as varchar) + ' and Sc.status <> 128 Order by Sc.ColId ' 		
		End 	
	Exec(@QueryFinal) 
			

		--		spp_GetCampos  

        Declare Llave_Cursor Cursor For  
        Select name From #tmpColumnasPK Order by ColId 
		open Llave_Cursor 
		Fetch From Llave_Cursor into @Campo 

		Set @Parte_Exists = 'Select ' + char(39) + 'If Not Exists ( Select * From ' + @Tabla + ' (NoLock)  Where ' + char(39) + ' + ' 
		Set @QueryFinal = @QueryFinal + @Parte_Exists 
        Set @Parte_Exists = 'Where ' 
        --Set @Parte_Relacion_Tablas = '' 

		while @@Fetch_status = 0 
			begin               
		        Set @query = @campo 
		        Set @query = @Alias_Destino + '.' + @query + ' = ' + @Alias_Origen + '.' + @query + '' 
    							
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
		
		
	-- Print @Parte_Relacion_Tablas 	
	Set @SalidaFinal = @Parte_Relacion_Tablas 


------------------
----    Set @SalidaFinal = '' 
----    Set @SalidaFinal = @Parte_Update + char(13) + @Parte_Insersion 
----    Set @SalidaFinal = '------------------------------------------------ ' + @Tabla + char(10) + 
----                       @Parte_Update + char(13) + @Parte_Insersion + char(10) + 
----                       '------------------------------------------------ ' + @Tabla + char(10) + char(10) 




	
End
Go--#SQL