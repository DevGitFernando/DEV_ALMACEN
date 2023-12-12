If Exists ( Select Name From sysobjects (NoLock) Where Name = 'spp_Mtto_Limpiar_Tablas_ListaLotes' and xType = 'P' ) 
   Drop Proc spp_Mtto_Limpiar_Tablas_ListaLotes  
Go--#SQL 
   
Create Proc spp_Mtto_Limpiar_Tablas_ListaLotes 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
    @sTabla varchar(200),  
    @sSql varchar(500) 

    Select * 
    Into #tmpBorrar           
    From 
    ( 
        Select Name as Tabla, crdate as FechaCreacion, 
             (case when datediff(mi, crdate, getdate()) > 30 then 1 else 0 end ) as Borrar  
        From Sysobjects (NoLock) 
        Where 
			( Name like 'tmpListaLotes%' ) 
			 Or 
			( Name like 'tmpCantidadesOC%' )     
			 Or 
			( Name like 'tmpCantClavesSubPerfil%' )     			
    ) as T 
    Where Borrar = 1 

    Declare TablasBorrar Cursor For 
        Select Tabla  
        From #tmpBorrar 
	open TablasBorrar 
	Fetch From TablasBorrar into @sTabla 
	while @@Fetch_status = 0 
	    Begin 
	        Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + @sTabla + char(39) + ' ) Drop Table ' +  @sTabla 
	        Exec(@sSql)  
	        
	        Fetch next From TablasBorrar into @sTabla 
		end 
    close TablasBorrar  
	deallocate TablasBorrar 	    


End 
Go--#SQL 

   