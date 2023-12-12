If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_EXE_ClavesPrecios_Estados' and xType = 'P' )
   Drop Proc spp_EXE_ClavesPrecios_Estados 
Go 

Create Proc spp_EXE_ClavesPrecios_Estados 
With Encryption 
As 
Begin 
Declare @sSql varchar(7500), 
	@sIdEstado varchar(2), 
	@sEstado varchar(100)  


--- Estados Involucrados 
	Select Distinct identity(int, 1, 1) as Keyx, IdEstado, Estado 
	Into #tmpPreciosEstados 	
	From vw_Claves_Precios_Asignados 
	Order By IdEstado 


--- Claves con Precios 
	Select Distinct IdClaveSSA, ClaveSSA, DescripcionClave  
	Into #tmpClaves_Precios 
	From vw_Claves_Precios_Asignados 
	Order by DescripcionClave 
	
	
    Declare tmpCol Cursor For Select IdEstado, Estado from #tmpPreciosEstados order by keyx	
    Open tmpCol
    FETCH NEXT FROM tmpCol Into @sIdEstado, @sEstado 
        WHILE @@FETCH_STATUS = 0
        BEGIN
           -- print  @sIdEstado + '   ' + @sEstado 
           --- Agregar la Columna Correspondiente 
           Set @sSql = ' Alter Table ' + '#tmpClaves_Precios' + ' Add ' + @sEstado + ' numeric(14,4) not null Default 0 ' 
           Exec(@sSql)
           
           --- Agregar la Informacion Relacionada 
		   Set @sSql = ' Update E Set ' + @sEstado + ' = P.Precio 
				From #tmpClaves_Precios E  
				Inner join vw_Claves_Precios_Asignados P (NoLock) ' + 
				'	On ( P.IdEstado = ' + char(39) + @sIdEstado + char(39) + ' and E.IdClaveSSA = P.IdClaveSSA ) '
           Exec(@sSql)
           	           
           
           FETCH NEXT FROM tmpCol Into   @sIdEstado, @sEstado 
        END
    Close tmpCol
    Deallocate tmpCol 
    
    
    Select * 
    From #tmpClaves_Precios 
    
End 
Go     	
