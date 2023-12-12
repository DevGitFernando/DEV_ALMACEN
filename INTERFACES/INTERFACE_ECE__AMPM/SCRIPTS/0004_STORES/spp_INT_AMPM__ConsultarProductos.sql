-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_AMPM__ConsultarExistenciasProductos' and xType = 'P' ) 
   Drop Proc spp_INT_AMPM__ConsultarExistenciasProductos
Go--#SQL 

/* 
	
	Exec spp_INT_AMPM__ConsultarExistenciasProductos @Tipo = 1, @CriterioDeBusqueda = 'parace 500' 
	Exec spp_INT_AMPM__ConsultarExistenciasProductos @Tipo = 2, @CriterioDeBusqueda = 'parace 500', @Id_Producto =  00009859   
	
	Exec spp_INT_AMPM__ConsultarExistenciasProductos @Tipo = 2, @CriterioDeBusqueda = 'parace 500', @Id_Producto =  00009859  
		
	
*/ 

Create Proc spp_INT_AMPM__ConsultarExistenciasProductos 
( 
	@Referencia_IdClinica varchar(100) = '0011', 
	@IdFarmacia varchar(20) = '', 
	@TipoDeConsulta int = 1, @CriterioDeBusqueda varchar(100) = '' 
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
	@IdFarmacia_Interno varchar(15), 
	@sTipoDeBusqueda varchar(100) 

	
	Set @CriterioDeBusqueda = replace(@CriterioDeBusqueda, ' ', '%') 
	Set @sFiltroProducto = '' 
	
	Select @IdFarmacia_Interno = (IdEmpresa + IdEstado + IdFarmacia)
	From INT_AMPM__CFG_FarmaciasClinicas (NoLock) 
	Where -- IdEstado = '09' and IdFarmacia = '0011' -- 
		( Referencia_AMPM = @Referencia_IdClinica ) or ( IdFarmacia = @IdFarmacia ) 
	Set @IdFarmacia_Interno = IsNull(@IdFarmacia_Interno, '') 



	
	Select 
		P.IdProducto, P.CodigoEAN, cast(F.Existencia as int) as Existencia, 
		P.ClaveSSA, P.Descripcion as NombreComercial, (P.ClaveSSA + ' -- ' + P.DescripcionClave) as DescripcionGenerica, 
		P.Presentacion, -- cast('' as varchar(100)) as TipoBusqueda, 
		cast('' as varchar(100)) as FechaCaducidad, 0 as MesesCaducidad  
	Into #tmp_Resultado 
	From vw_Productos_CodigoEAN P (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN F On ( P.IdProducto = F.IdProducto and P.CodigoEAN = F.CodigoEAN ) 
	Where 1 = 0 
	

	--------------- APLICAR FILTRO SOLICITADO 
	Set @sTipoDeBusqueda = 'Comercial'
	Set @sFiltroProducto = ' and P.Descripcion like ' + char(39) + '%' + @CriterioDeBusqueda + '%' + char(39) + ' '  	
	If @TipoDeConsulta = 2 
	Begin 
		Set @sTipoDeBusqueda = 'Sustancia activa'
		Set @sFiltroProducto = ' and P.DescripcionClave like ' + char(39) + '%' + @CriterioDeBusqueda + '%' + char(39) + ' '  
	End 



	Set @sSql = ' 
		Insert Into #tmp_Resultado 
		Select 
			P.IdProducto, P.CodigoEAN, cast(F.Existencia as int) as Existencia, 
			P.ClaveSSA, P.Descripcion as NombreComercial, 
			P.DescripcionClave as DescripcionGenerica, P.Presentacion, 
			cast(L.FechaCaducidad as varchar(10)) as FechaCaducidad, datediff(month, getdate(), L.FechaCaducidad) as MesesCaducidad  
		From vw_Productos_CodigoEAN P (NoLock) 
		Inner Join FarmaciaProductos_CodigoEAN F On ( P.IdProducto = F.IdProducto and P.CodigoEAN = F.CodigoEAN )  
		Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock)  
			On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia 
				 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN  ) 
		Where P.IdProducto > 0 and F.Existencia > 0 
			and (F.IdEmpresa + F.IdEstado + F.IdFarmacia) = ' + char(39) + @IdFarmacia_Interno + char(39) + ' ' + @sFiltroProducto + char(13) + char(10) + 
		'Order by F.Existencia desc, P.ClaveSSA, P.Descripcion   ' 
	Exec( @sSql ) 
	Print @sSql 
		
	
--		Exec spp_INT_AMPM__ConsultarExistenciasProductos @Tipo = 1, @CriterioDeBusqueda = 'parace 500' 	
	


--------------------------------  SALIDA FINAL 	
	Select 
		--P.IdProducto, 
		@sTipoDeBusqueda as TipoDeBusqueda, 
		P.CodigoEAN, P.NombreComercial, Presentacion, 
		sum(P.Existencia) as Existencia, P.ClaveSSA, DescripcionGenerica 
	From #tmp_Resultado P (NoLock) 
	Where MesesCaducidad >= 1 
	Group by 
		--P.IdProducto, 
		P.CodigoEAN, P.NombreComercial, Presentacion, 
		P.ClaveSSA, DescripcionGenerica 
	Order By Existencia, ClaveSSA, NombreComercial -- DescripcionGenerica 


End 
Go--#SQL 
