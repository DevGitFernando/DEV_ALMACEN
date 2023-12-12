


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_ProvExternos_Entradas' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_ProvExternos_Entradas 
Go--#SQL 

Create Proc spp_Rpt_CteReg_ProvExternos_Entradas   
( 
	@IdEstado varchar(2) = '21', -- @FechaInicial varchar(10) = '2012-01-01', @FechaFinal varchar(10) = '2012-01-05' 
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
	
	Select 
		T.IdEstado, T.Estado, F.IdJurisdiccion, F.Jurisdiccion,
		T.IdFarmacia, T.Farmacia,  
		T.Folio, 
		convert(varchar(10), T.FechaRegistroPed, 120) as FechaSistema, 
		space(3) as IdProveedor, space(100) as NombreProveedor, 	
		T.IdClaveSSA_Sal as IdClaveSSA, T.ClaveSSA, T.DescripcionSal as DescripcionClave, sum(T.CantidadLote)  as Cantidad, 
		0 as ProvExt  
	into #tmpEntradas 	 
	From vw_Impresion_Entradas_Consignacion T (Nolock)
	Inner Join vw_Farmacias F (Nolock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )
	Where T.IdEstado = @IdEstado 
		and F.IdJurisdiccion In ( Select IdJurisdiccion From tmpJurisdicciones (Nolock) Where IdEstado = @IdEstado )
		and year(T.FechaRegistroPed) = @Año and month(T.FechaRegistroPed) = @Mes    
--		and day(T.FechaRegistroPed) in ( 1, 2, 3 )
	Group by 
		T.IdEstado, T.Estado, F.IdJurisdiccion, F.Jurisdiccion,
		T.IdFarmacia, T.Farmacia,  
		T.Folio, 
		convert(varchar(10), T.FechaRegistroPed, 120), 
		T.IdClaveSSA_Sal, T.ClaveSSA, T.DescripcionSal 
		

---------------------------------------------------------------------------------------------------------------- 
	Update T Set ProvExt = 1, 
		IdProveedor = C.IdProveedor, NombreProveedor = C.Nombre
	From #tmpEntradas T 
	Inner Join #tmpClaves_ProvExternos C On ( C.ClaveSSA = T.ClaveSSA ) 
	
	Delete From #tmpEntradas Where ProvExt = 0 
	
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpJurisdicciones' and xType = 'U' )
	   Drop Table tmpJurisdicciones
--------------- 	
--	Select * 	From #tmpClaves_ProvExternos 
	
---		spp_Rpt_CteReg_ProvExternos_Entradas    	
	
	Select 'Núm. Jurisdicción' = IdJurisdiccion, 'Nombre Jurisdicción' = Jurisdiccion,
		'Núm. Unidad' = IdFarmacia, 'Nombre unidad' = Farmacia, 
		Folio, 
		'Fecha registro' = FechaSistema, 
		'Id Proveedor' = IdProveedor, 'Nombre proveedor' = NombreProveedor, 	
		'Clave SSA' = ClaveSSA, 'Descripción clave' = DescripcionClave, 'Piezas' = Cantidad 
	From #tmpEntradas  	
	Order By IdJurisdiccion, IdFarmacia

	Select 'Núm. Jurisdicción' = IdJurisdiccion, 'Nombre Jurisdicción' = Jurisdiccion,
		'Núm. Unidad' = IdFarmacia, 'Nombre unidad' = Farmacia,	'Piezas' = Sum(Cantidad) 
	From #tmpEntradas
	Group By IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia
	Order By IdJurisdiccion, IdFarmacia


--	Select * 	from 	#tmpEntradas   
	
End 
Go--#SQL 

   