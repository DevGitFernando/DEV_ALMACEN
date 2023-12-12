If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_ClavesSSA_Precios_Asignados' and xType = 'P' ) 
   Drop Proc spp_Rpt_ClavesSSA_Precios_Asignados 
Go--#SQL 

Create Proc spp_Rpt_ClavesSSA_Precios_Asignados 
( 
--	@IdEmpresa varchar(3) = '*', 
	@IdEstado varchar(2) = '10', @IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '3'
) 
As 
Begin 
Set DateFormat YMD 
Declare @sSql varchar(7500), 
		@sWhere varchar(500), 
		@sEncPricipal varchar(500), 
		@sEncSecundario varchar(500)
		  

--- Preparar en caso de Vacios 
	If @IdEstado = '' 
	   Set @IdEstado = '*'

	If @IdCliente = '' 
	   Set @IdCliente = '*'
	   
	If @IdSubCliente = '' 
	   Set @IdSubCliente = '*'	   

------ 
	Set @sSql = '' 	
	Set @sWhere = ' Where 1 = 1 ' 

	Select top 1 @sEncPricipal = EncPrincipal, @sEncSecundario = EncSecundario From vw_EncabezadoReportes 

--- Preparar la tabla de Datos 	
	Select Top 0 IdEstado, Estado, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdClaveSSA, ClaveSSA, DescripcionClave, Precio, Status, StatusRelacion -- , getdate() as FechaImpresion  
    Into #tmpListaClavesPrecios 
	From vw_Claves_Precios_Asignados (NoLock) 

--- Armar Where 
	If @IdEstado <> '*' or @IdEstado = '' 
	   Set @sWhere = @sWhere + ' and IdEstado = ' + char(39) + right('00' + @IdEstado, 2) + char(39) 

	If @IdCliente <> '*' or @IdCliente = '' 
	   Set @sWhere = @sWhere + ' and IdCliente= ' + char(39) + right('0000' + @IdCliente, 4) + char(39) 
	   
	If @IdSubCliente <> '*' or @IdSubCliente = '' 
	   Set @sWhere = @sWhere + ' and IdSubCliente = ' + char(39) + right('0000' + @IdSubCliente, 4) + char(39) 	   


----------
	Set @sSql = '
		Insert Into #tmpListaClavesPrecios ( IdEstado, Estado, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdClaveSSA, ClaveSSA, DescripcionClave, Precio, Status, StatusRelacion ) 
		Select IdEstado, Estado, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
			IdClaveSSA, ClaveSSA, DescripcionClave, Precio, Status, StatusRelacion  
		From vw_Claves_Precios_Asignados (NoLock) ' + 
		@sWhere + ' Order By IdEstado, IdCliente, IdSubCliente ' 
	Exec(@sSql)	

--- Salida final del Proceso 
	Select @sEncPricipal as EncPrincipal, @sEncSecundario as EncSecundario,  
		IdEstado, Estado, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdClaveSSA, ClaveSSA, DescripcionClave, Precio, Status, StatusRelacion, getdate() as FechaImpresion 
	From #tmpListaClavesPrecios  
	

End 
Go--#SQL 	
	
	