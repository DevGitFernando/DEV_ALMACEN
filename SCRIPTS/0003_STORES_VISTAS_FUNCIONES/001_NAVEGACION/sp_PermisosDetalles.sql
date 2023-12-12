If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_PermisosDetalles' and xType = 'P' )
   Drop proc sp_PermisosDetalles 
Go--#SQL 

Create Proc sp_PermisosDetalles ( @sArbol varchar(4) = 'VEN', @iRamaPadre int, @iNivel int = 1 output ) 
With Encryption 
As 
Begin 
	Set DateFormat YMD 

declare @iRama int 
	Set @iRama = 0 

	-- Buscar todos los hijos de la rama padre, primer nivel 
	Select * 
	Into #tmpHijosPadre 
	From Net_Navegacion (NoLock) 
	Where Arbol = @sArbol and Padre = @iRamaPadre and Status = 'A' 
	Order by IdOrden 
	
	-- Select * from #tmpHijosPadre  
	--Select @@rowcount 
	-- Select * From #tmpHijosPadre 

    Declare tmp cursor Local For Select Rama From #tmpHijosPadre Order by IdOrden 
    OPEN tmp 
    FETCH NEXT FROM tmp INTO @iRama 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN
            
            If @@NESTLEVEL < 32
            Begin
			   Insert Into tmpPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
			   Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta  
			   From #tmpHijosPadre 
			   Where Arbol = @sArbol and Padre = @iRamaPadre and Rama = @iRama 	
				
               Set @iNivel = @iNivel + 1
               Exec sp_PermisosDetalles @sArbol, @iRama, @iNivel out 
               Set @iNivel = @iNivel - 1
            End
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 


End 
Go--#SQL   