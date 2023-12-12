-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__ConsultarProductos' and xType = 'P' ) 
   Drop Proc spp_INT_MA__ConsultarProductos
Go--#SQL 

/* 
	
	Exec spp_INT_MA__ConsultarProductos @Tipo = 1, @Consulta = 'parace 500' 
	Exec spp_INT_MA__ConsultarProductos @Tipo = 2, @Consulta = 'parace 500', @Id_Producto =  00009859   
	
	Exec spp_INT_MA__ConsultarProductos @Tipo = 2, @Consulta = 'parace 500', @Id_Producto =  00009859  
		
	
*/ 

Create Proc spp_INT_MA__ConsultarProductos 
( 
	@Id_Producto bigint = 0, @Tipo int = 1, @Consulta varchar(max) = 'parace', @IdFarmacia varchar(15) = '59347', 
	@Ranking int = 1   

) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@sFiltroProducto varchar(200),    
	@IdFarmacia_Interno varchar(15)
	
	Set @Consulta = replace(@Consulta, ' ', '%') 
	Set @sFiltroProducto = '' 
	
	Select @IdFarmacia_Interno = (IdEmpresa + IdEstado + IdFarmacia)
	From INT_MA__CFG_FarmaciasClinicas (NoLock) 
	Where Referencia_MA = @IdFarmacia 
	
	If @Id_Producto <> 0 
	Begin 
		Set @Consulta = '' 
		Set @sFiltroProducto = ' and P.IdProducto = ' + cast(@Id_Producto as varchar) 
	End 
	
	Select 
		P.IdProducto, P.CodigoEAN, cast(F.Existencia as int) as Existencia, 
		P.ClaveSSA, P.Descripcion as NombreComercial, (P.ClaveSSA + ' -- ' + P.DescripcionClave) as DescripcionGenerica,
		P.IdFamilia, 2 as Clasificacion  
	Into #tmp_Resultado 
	From vw_Productos_CodigoEAN P (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN F On ( P.IdProducto = F.IdProducto and P.CodigoEAN = F.CodigoEAN ) 
	Where 1 = 0 
	
	Set @sSql = ' 
		Insert Into #tmp_Resultado 
		Select 
			P.IdProducto, P.CodigoEAN, cast(F.Existencia as int) as Existencia, 
			P.ClaveSSA, P.Descripcion as NombreComercial, 
			(P.ClaveSSA + ' + char(39) + ' ---- ' + char(39) + ' + P.DescripcionClave) as DescripcionGenerica, 
			P.IdFamilia, 2 as Clasificacion --- (case when IdFamilia in ( 2, 6 ) then 1 else 0 end) as Clasificacion  
		From vw_Productos_CodigoEAN P (NoLock) 
		Inner Join FarmaciaProductos_CodigoEAN F On ( P.IdProducto = F.IdProducto and P.CodigoEAN = F.CodigoEAN ) 
		Where P.IdProducto > 0 
			and (IdEmpresa + IdEstado + IdFarmacia) = ' + char(39) + @IdFarmacia_Interno + char(39) + ' 
			and P.Descripcion like ' + char(39) + '%' + @Consulta + '%' + char(39) + ' ' + @sFiltroProducto + ' 
		Order by Existencia desc, PrecioVenta Desc, P.ClaveSSA, P.Descripcion   ' 
	Exec( @sSql ) 
	Print @sSql 
	
	Update R Set Clasificacion = 1 
	From #tmp_Resultado R (NoLock) 
	Where IdFamilia in ( 2, 6 ) 
	
	
	If @Ranking <> 0 
	Begin 
		Delete From #tmp_Resultado Where Clasificacion <> @Ranking 	
	End 
	
	
	
--		Exec spp_INT_MA__ConsultarProductos @Tipo = 1, @Consulta = 'parace 500' 	
	
	
	
	If @Tipo = 1 
		Begin  
			Select IdProducto, CodigoEAN, Existencia, ClaveSSA, NombreComercial, DescripcionGenerica, Clasificacion, 
			ROW_NUMBER() Over ( Order By Existencia desc ) as Ranking   
			From #tmp_Resultado 
		End 
	Else 
		Begin  
			Select 
				'' as IdProducto, '' as CodigoEAN, sum(Existencia) as Existencia, 
				ClaveSSA, DescripcionGenerica as NombreComercial, DescripcionGenerica, min(Clasificacion) as Clasificacion, 
				ROW_NUMBER() Over ( Order By sum(Existencia) desc ) as Ranking 
			From #tmp_Resultado 
			Group By ClaveSSA, DescripcionGenerica 
			Order by sum(Existencia) desc  
		End 

End 
Go--#SQL 
