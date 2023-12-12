If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_NavegacionDetalles' and xType = 'P' )
   Drop proc sp_NavegacionDetalles 
Go--#SQL   

Create Proc sp_NavegacionDetalles ( @sArbol varchar(4), @iRamaPadre int output, @sRutaCompleta varchar(20) out, @iNivel int = 1 output ) 
With Encryption 
As 
Begin 
	Set DateFormat YMD 

declare @iRama int 
	Set @iRama = 0 

	-- Buscar todos los hijos de la rama padre, primer nivel 
	Select *, space(20) as TipoRama 
	Into #tmpHijosPadre 
	From Net_Navegacion (NoLock) 
	Where Arbol = @sArbol and Padre = @iRamaPadre 
	Order by IdOrden 

	-- Select * From #tmpHijosPadre 

    Declare tmp cursor Local For Select Rama From #tmpHijosPadre Order by IdOrden 
    OPEN tmp 
    FETCH NEXT FROM tmp INTO @iRama 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN
            
            If @@NESTLEVEL < 32
            Begin

			   Set @sRutaCompleta = replace(@sRutaCompleta, '||', '|') 
			   Set @sRutaCompleta = ltrim(rtrim(@sRutaCompleta)) + '|' + cast(@iRama as varchar)	

			   Insert Into tmpNavegacion 
			   Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, @sRutaCompleta, Status   
			   From #tmpHijosPadre 
			   Where Arbol = @sArbol and Padre = @iRamaPadre and Rama = @iRama 	

               Set @iNivel = @iNivel + 1
               Exec sp_NavegacionDetalles @sArbol, @iRama, @sRutaCompleta, @iNivel out 
			   Set @sRutaCompleta = ltrim(rtrim(@sRutaCompleta))
			   --Print @sRutaCompleta 	
			   Set @sRutaCompleta = left(@sRutaCompleta, len(@sRutaCompleta) - (len(cast(@iRama as varchar)) ))
			   Set @sRutaCompleta = replace(@sRutaCompleta, '||', '|')
			   --Print @sRutaCompleta 
               Set @iNivel = @iNivel - 1
            End
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 


End 
Go--#SQL  