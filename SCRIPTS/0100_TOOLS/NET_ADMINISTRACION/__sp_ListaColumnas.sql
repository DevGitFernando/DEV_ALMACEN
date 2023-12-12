
If Exists ( Select * From Sysobjects Where Name = 'sp_ListaColumnas' and xType = 'P' )
    Drop Proc sp_ListaColumnas
Go
-- Exec sp_ListaColumnas MovPedidosP0

-- Exec sp_ListaColumnas tmpDist_MovPedidosP0

Create Proc sp_ListaColumnas ( @sTabla varchar(100), @iNombres int = 0, @sRegresa Varchar(max) = '' output, @Alias varchar(10) = '' )
With Encryption
As
Begin

-- select * from tmpPeriodos
Declare @iPeriodoActual int, @iReg int, @iPrimerCol int, @iUltimaCol int,
        @sColumna varchar(50)

Declare @sSql varchar(8000)

    Set NoCount On 
    Set @iPeriodoActual = 10
    Set @iPrimerCol =  3
    Set @iUltimaCol = 15 

	If @Alias <> '' 
	Begin 
		Set @Alias = @Alias + '.'
	End 


    if (object_id('master..tmpPeriodosColumnas') is not null)
        drop table master..tmpPeriodosColumnas

    Select So.Name as Tabla, Sc.Name as Columna, Sc.ColId as Orden, identity(int, 1, 1) as keyx 
    Into master..tmpPeriodosColumnas
    From Sysobjects So (Nolock)
    Inner join Syscolumns Sc (NoLock) On (So.Id = Sc.Id)
    Where So.Name = '' + @sTabla + '' and Sc.iscomputed = 0  
    order by Sc.ColId

    --- Obterner las columnas ordenadas
    Set @sRegresa = ''
    Set @iReg = 0
    Set @sSql = '' 

--- Crear Tabla Base 
	Select '' as _x 
	Into #tmpCols 


    Declare tmpCol Cursor For Select @Alias + Columna from master..tmpPeriodosColumnas order by keyx
    Open tmpCol
    FETCH NEXT FROM tmpCol Into @sColumna 
        WHILE @@FETCH_STATUS = 0
        BEGIN
            
           If @iReg = 0
               Begin
                    Set @sRegresa = @sRegresa + ' ' + @sColumna
               End
           Else
               Begin
                    Set @sRegresa = @sRegresa + ', ' + @sColumna
               End
    
            Set @iReg = 1
 
 ---------- Agregar las columnas requeridas 
           If @iNombres = 1 
              Begin 
		         Set @sSql = ' Alter Table #tmpCols Add ' + @sColumna + ' varchar(200) Not Null Default ' + char(39) + @sColumna + char(39) -- + '' 
		         Exec(@sSql) 
		   -- print @sSql 
		      End 
 
           FETCH NEXT FROM tmpCol Into  @sColumna
        END
    Close tmpCol
    Deallocate tmpCol

--- Siempre se Imprime la lista 
    Print @sRegresa    

    If @iNombres = 1 
       Begin 
			Alter Table #tmpCols Drop Column _x 
			       
			Select * 
			From #tmpCols        
       End
		      
    --Exec('Select ' + @sRegresa + ' from tmpPeriodos ')
    -- if (object_id('tmpPeriodosColumnas') is not null)
    --    drop table tmpPeriodosColumnas
End
Go
