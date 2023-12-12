-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects Where Name = 'sp_ListaColumnas__Stores' and xType = 'P' )
    Drop Proc sp_ListaColumnas__Stores
Go
-- Exec sp_ListaColumnas__Stores MovPedidosP0

-- Exec sp_ListaColumnas__Stores tmpDist_MovPedidosP0

Create Proc sp_ListaColumnas__Stores ( @sTabla varchar(100), @iParametros int = 0, @iFormato int = 0 ) 
With Encryption
As
Begin

-- select * from tmpPeriodos
Declare @iPeriodoActual int, @iReg int, @iPrimerCol int, @iUltimaCol int,
        @sColumna varchar(50),
        @sRegresa varchar(8000), 
        @sParametro varchar(20), 
		@iCampos int 

Declare @sSql varchar(8000)

    Set NoCount On 
    Set @iPeriodoActual = 10
    Set @iPrimerCol =  3
    Set @iUltimaCol = 15 
    Set @sParametro = '' 
	Set @iCampos = 0 

    if (object_id('master..tmpPeriodosColumnas') is not null)
        drop table master..tmpPeriodosColumnas

    Select So.Name as Tabla, Sc.Name as Columna, Sc.ColId as Orden, identity(int, 1, 1) as keyx 
    Into #tmpPeriodosColumnas
    From Sysobjects So (Nolock)
    Inner join Syscolumns Sc (NoLock) On (So.Id = Sc.Id)
    Where So.Name = '' + @sTabla + ''
    order by Sc.ColId

    --- Obterner las columnas ordenadas
    Set @sRegresa = ''
    Set @iReg = 0
    Set @sSql = '' 

--- Crear Tabla Base 
	Select '' as _x 
	Into #tmpCols 


    Declare tmpCol Cursor For Select Columna from #tmpPeriodosColumnas order by keyx
    Open tmpCol
    FETCH NEXT FROM tmpCol Into @sColumna 
        WHILE @@FETCH_STATUS = 0
        BEGIN
           
           If @iParametros = 1 
              Set @sParametro = ' = ' + char(39) + '{' + cast(@iReg as varchar) + '}' + char(39)  
            
           
		   Set @iCampos = @iCampos + 1 
		    
           If @iReg = 0 
               Begin 
                    --Set @sRegresa = @sRegresa + '' + @sColumna + @sParametro 

					If @iFormato = 1 
						Set @sRegresa = char(34) + '\t' + @sRegresa + '' + @sColumna + @sParametro 
					Else 
						Set @sRegresa = @sRegresa + ', ' + @sColumna + @sParametro  

               End
           Else
               Begin 
					--If @iCampos <= 5 
					--Begin 
					--	Set @sRegresa = @sRegresa + ', ' 
					--End 

					If ( @iCampos >= 5  )
						Begin 
							Set @iCampos = 0 
							--Set @sRegresa = @sRegresa + ', ' + char(10) + @sColumna + @sParametro   

							If @iFormato = 1 
								Set @sRegresa = @sRegresa + ', ' + '\n' + char(34) + ' + ' + char(10) + char(34) + '\t' + @sColumna + @sParametro  
							Else 
								Set @sRegresa = @sRegresa + ', ' + @sColumna + @sParametro  
						End 
					Else 
						Begin 
							Set @sRegresa = @sRegresa + ', ' + @sColumna + @sParametro   
							----If @iFormato = 0 
							----	Begin 
							----		Set @sRegresa = @sRegresa + ', ' + @sColumna + @sParametro   
							----	End 
							----Else  
							----	Begin 
							----		If @iCampos = 1 
							----		   Set @sRegresa = @sRegresa + ', ' + @sColumna + @sParametro   
							----		Else 
							----		   Set @sRegresa = @sRegresa + ', ' + @sColumna + @sParametro    
							----	End 
						End 

					----If ( @iCampos = 5  )
					----Begin 
					----	Set @iCampos = 0 
					----	Set @sRegresa = @sRegresa + ', ' + char(10) 
					----End 
               End
    
            Set @iReg = @iReg + 1
 
 
---- ---------- Agregar las columnas requeridas 
----           If @iParametros = 1 
----              Begin 
----		         Set @sSql = ' Alter Table #tmpCols Add ' + @sColumna + ' varchar(200) Not Null Default ' + char(39) + @sColumna + char(39) -- + '' 
----		         Exec(@sSql) 
----		   -- print @sSql 
----		      End 
 
           FETCH NEXT FROM tmpCol Into  @sColumna
        END
    Close tmpCol
    Deallocate tmpCol


	----Print  right(@sRegresa, 1) 
	----If right(@sRegresa, 1) = ',' 
	----Begin 
	----	Set @sRegresa = left(@sRegresa, LEN(@sRegresa) - 1 )
	----End 

	If @iFormato = 1 
		Set @sRegresa = @sRegresa + ' \n' + char(34) 

--- Siempre se Imprime la lista 
    --Print '' 
	--Print '' 
	Print @sRegresa    

----    If @iParametros = 1 
----       Begin 
----			Alter Table #tmpCols Drop Column _x 
----			       
----			Select * 
----			From #tmpCols        
----       End 
		      
    --Exec('Select ' + @sRegresa + ' from tmpPeriodos ')
    -- if (object_id('tmpPeriodosColumnas') is not null)
    --    drop table tmpPeriodosColumnas
End
Go
