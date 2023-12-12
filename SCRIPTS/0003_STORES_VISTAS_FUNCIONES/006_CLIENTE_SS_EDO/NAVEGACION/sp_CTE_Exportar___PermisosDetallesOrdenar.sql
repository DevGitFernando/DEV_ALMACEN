--------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_CTE_Exportar___PermisosDetallesOrdenar' and xType = 'P' )
   Drop proc sp_CTE_Exportar___PermisosDetallesOrdenar 
Go--#SQL 
 

Create Proc sp_CTE_Exportar___PermisosDetallesOrdenar ( @sArbol varchar(4) = 'COM', @iRamaPadre int, @iNivel int = 1 output ) 
with Encryption 
As 
Begin 
	Set DateFormat YMD 

declare @iRama int 
	Set @iRama = 0 

	-- Buscar todos los hijos de la rama padre, primer nivel 
	Select Distinct Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta  
	Into #tmpHijosPadre 
	From tmpPermisos_Exportar (NoLock) 
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
			   Insert Into tmpPermisos_ExportarFinal ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta ) 
			   Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta  
			   From #tmpHijosPadre 
			   Where Arbol = @sArbol and Padre = @iRamaPadre and Rama = @iRama 	

               Set @iNivel = @iNivel + 1
               Exec sp_CTE_Exportar___PermisosDetallesOrdenar @sArbol, @iRama, @iNivel out 
               Set @iNivel = @iNivel - 1
            End
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 


End 
Go--#SQL  
