If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_ClavesSSA_Asignadas_Cte_SubCte' and xType = 'P' ) 
   Drop Proc spp_Rpt_ClavesSSA_Asignadas_Cte_SubCte 
Go--#SQL 

Create Proc spp_Rpt_ClavesSSA_Asignadas_Cte_SubCte 
( 
	@IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '3'
) 
As 
Begin 
Set DateFormat YMD 
Declare @sSql varchar(7500), 
		@sWhere varchar(500), 
		@sEncPricipal varchar(500), 
		@sEncSecundario varchar(500)
		  

	If @IdCliente = '' 
	   Set @IdCliente = '*'
	   
	If @IdSubCliente = '' 
	   Set @IdSubCliente = '*'	   

------ 
	Set @sSql = '' 	
	Set @sWhere = ' Where 1 = 1 ' 

	Select top 1 @sEncPricipal = EncPrincipal, @sEncSecundario = EncSecundario From vw_EncabezadoReportes 

--- Preparar la tabla de Datos 	
	Select Top 0 IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdClaveSSA, ClaveSSA, DescripcionClave, Status, StatusRelacion 
    Into #tmpListaClaves 
	From vw_Claves_Asignadas_A_SubClientes (NoLock) 


--- Armar Where 
	If @IdCliente <> '*' or @IdCliente = '' 
	   Set @sWhere = @sWhere + ' and IdCliente = ' + char(39) + right('0000' + @IdCliente, 4) + char(39) 
	   
	If @IdSubCliente <> '*' or @IdSubCliente = '' 
	   Set @sWhere = @sWhere + ' and IdSubCliente = ' + char(39) + right('0000' + @IdSubCliente, 4) + char(39) 	   


----------
	Set @sSql = '
		Insert Into #tmpListaClaves ( IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdClaveSSA, ClaveSSA, DescripcionClave, Status, StatusRelacion ) 
		Select IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdClaveSSA, ClaveSSA, DescripcionClave, Status, StatusRelacion  
		From vw_Claves_Asignadas_A_SubClientes (NoLock) ' + 
		@sWhere + ' Order By IdCliente, IdSubCliente ' 
	Exec(@sSql)	

--- Salida final del Proceso 
	Select @sEncPricipal as EncPrincipal, @sEncSecundario as EncSecundario,  
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdClaveSSA, ClaveSSA, DescripcionClave, 
		Status, StatusRelacion, getdate() as FechaImpresion 
	From #tmpListaClaves  
	

End 
Go--#SQL 	
	
	