

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_ProvExternos_Dispensacion' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_ProvExternos_Dispensacion 
Go--#SQL 

Create Proc spp_Rpt_CteReg_ProvExternos_Dispensacion   
( 
	@IdEstado varchar(2) = '21', 
	@Año int = 2012, @Mes int = 1, @IdJurisdiccion varchar(3) = '*'
) 
With Encryption 
As 
Begin 
-- Set NoCount On 
Declare @sWhere varchar(7500), @sSql varchar(7500)

	Set @sWhere = ''
	Set @sSql = ''
	
	
	If @IdJurisdiccion = '*'
		Begin 
			Set @sWhere = 'IdEstado = ' + char(39) + @IdEstado + char(39) 
		End
	Else
		Begin
			Set @sWhere = 'IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdJurisdiccion = ' + char(39) + @IdJurisdiccion + char(39)
		End
	

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpJurisdicciones' and xType = 'U' )
	   Drop Table tmpJurisdicciones 

	Set @sSql = ' Select IdEstado, IdJurisdiccion Into tmpJurisdicciones From CatJurisdicciones (Nolock) Where  ' + @sWhere
	Exec(@sSql)

	Select 
		P.IdProveedor, P.Nombre, 
		CP.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, C.Presentacion 
	into #tmpClaves_ProvExternos   
	From CatProveedores_Externos P (NoLock) 
	Inner Join CatProveedores_Externos_Claves CP On ( P.IdEstado = CP.IdEstado and P.IdProveedor = CP.IdProveedor ) 
	Inner Join vw_ClavesSSA_Sales C On ( C.IdClaveSSA_Sal = CP.IdClaveSSA ) 
	Where P.IdEstado = @IdEstado and P.Status = 'A' and CP.Status = 'A'  
	Order by P.IdProveedor, C.DescripcionClave 

--- Concentrado Claves 
	Select IdClaveSSA, ClaveSSA 
	Into #tmpClaves 
	From #tmpClaves_ProvExternos 
	Group by IdClaveSSA, ClaveSSA 

		
--		sp_listacolumnas vw_Impresion_Transferencias	
	
	Select E.IdEstado, F.Estado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, 
	convert(varchar(10), E.FechaSistema, 120) as FechaSistema, 
	space(3) as IdProveedor, space(100) as NombreProveedor, P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, 
	P.DescripcionSal as DescripcionClave, sum(D.CantidadVendida)  as Cantidad, 0 as ProvExt  
	Into #tmpDispensacion 	 
	From VentasEnc E (Nolock)
	Inner Join VentasDet D (Nolock)
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta )
	Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN  )
	Inner Join vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )
	Where E.IdEstado = @IdEstado 
		and F.IdJurisdiccion In ( Select IdJurisdiccion From tmpJurisdicciones (Nolock) Where IdEstado = @IdEstado )
		and year(E.FechaSistema) = @Año and month(E.FechaSistema) = @Mes   
		and day(E.FechaSistema) in ( 1, 2, 3 )
	Group by E.IdEstado, F.Estado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia,   
		convert(varchar(10), E.FechaSistema, 120), P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal
		

---------------------------------------------------------------------------------------------------------------- 
	Update T Set ProvExt = 1, 
		IdProveedor = C.IdProveedor, NombreProveedor = C.Nombre
	From #tmpDispensacion T 
	Inner Join #tmpClaves_ProvExternos C On ( C.ClaveSSA = T.ClaveSSA ) 
	
	Delete From #tmpDispensacion Where ProvExt = 0 
	
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpJurisdicciones' and xType = 'U' )
	   Drop Table tmpJurisdicciones 
--------------- 	
--	Select * 	From #tmpClaves_ProvExternos 
	
---		spp_Rpt_CteReg_ProvExternos_Dispensacion    	
	
	Select 'Núm. Jurisdicción' = IdJurisdiccion, 'Nombre Jurisdicción' = Jurisdiccion,
		'Núm. Unidad' = IdFarmacia, 'Nombre unidad' = Farmacia,		 
		'Fecha registro' = FechaSistema, 
		'Id Proveedor' = IdProveedor, 'Nombre proveedor' = NombreProveedor, 	
		'Clave SSA' = ClaveSSA, 'Descripción clave' = DescripcionClave, 'Piezas' = Cantidad 
	From #tmpDispensacion  	
	Order By IdJurisdiccion, IdFarmacia


	Select 'Núm. Jurisdicción' = IdJurisdiccion, 'Nombre Jurisdicción' = Jurisdiccion,
		'Núm. Unidad' = IdFarmacia, 'Nombre unidad' = Farmacia,		 
		'Piezas' = Sum(Cantidad) 
	From #tmpDispensacion
	Group By IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia
	Order By IdJurisdiccion, IdFarmacia

--	Select * 	from 	#tmpDispensacion   
	
End 
Go--#SQL 

   