


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_ProvExternos_Distribucion' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_ProvExternos_Distribucion 
Go--#SQL 

Create Proc spp_Rpt_CteReg_ProvExternos_Distribucion   
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

	Select E.IdEstado, F.Estado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, 
	E.FolioTransferencia As Folio, convert(varchar(10), E.FechaTransferencia, 120) as FechaSistema, 
	E.IdFarmaciaRecibe, FR.Farmacia As FarmaciaRecibe, space(3) as IdProveedor, space(100) as NombreProveedor, 	
	C.IdClaveSSA_Sal as IdClaveSSA, C.ClaveSSA, C.DescripcionSal as DescripcionClave, 
	sum(L.CantidadEnviada)  as Cantidad, 0 as ProvExt  
	into #tmpTransferencias 	
	From TransferenciasEnc E (NoLock)
	Inner Join TransferenciasDet_Lotes L (NoLock)
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.FolioTransferencia = L.FolioTransferencia )
	Inner Join vw_Productos_CodigoEAN C (NOLock) On ( L.IdProducto = C.IdProducto and L.CodigoEAN = C.CodigoEAN  )
	Inner Join vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )
	Inner Join vw_Farmacias FR (Nolock) On ( E.IdEstadoRecibe = FR.IdEstado and E.IdFarmaciaRecibe = FR.IdFarmacia )  
	Where E.IdEstado = @IdEstado and E.TipoTransferencia = 'TS'
	and F.IdJurisdiccion In ( Select IdJurisdiccion From tmpJurisdicciones (Nolock) Where IdEstado = @IdEstado ) 
	and year(E.FechaTransferencia) = @Año and month(E.FechaTransferencia) = @Mes    
	Group by E.IdEstado, F.Estado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, 
	L.IdSubFarmaciaEnvia, E.FolioTransferencia, convert(varchar(10), E.FechaTransferencia, 120), 
	E.IdFarmaciaRecibe, FR.Farmacia, L.IdSubFarmaciaRecibe,  		
	C.IdClaveSSA_Sal, C.ClaveSSA, C.DescripcionSal 


---------------------------------------------------------------------------------------------------------------- 
	Update T Set ProvExt = 1, 
		IdProveedor = C.IdProveedor, NombreProveedor = C.Nombre
	From #tmpTransferencias T 
	Inner Join #tmpClaves_ProvExternos C On ( C.ClaveSSA = T.ClaveSSA ) 
	
	Delete From #tmpTransferencias Where ProvExt = 0 
	
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpJurisdicciones' and xType = 'U' )
	   Drop Table tmpJurisdicciones 
--------------- 	
--	Select * 	From #tmpClaves_ProvExternos 
	
	Select 'Núm. Jurisdicción' = IdJurisdiccion, 'Nombre Jurisdicción' = Jurisdiccion,
		'Núm. Unidad' = IdFarmacia, 'Nombre unidad' = Farmacia, 
		Folio, 
		'Fecha registro' = FechaSistema, 
		'Núm. Unidad recibe' = IdFarmaciaRecibe, 'Nombre unidad recibe' = FarmaciaRecibe, 
		'Id Proveedor' = IdProveedor, 'Nombre proveedor' = NombreProveedor, 	
		'Clave SSA' = ClaveSSA, 'Descripción clave' = DescripcionClave, 'Piezas' = Cantidad 
	From #tmpTransferencias
	Order By IdJurisdiccion, IdFarmacia  	

	Select 'Núm. Jurisdicción' = IdJurisdiccion, 'Nombre Jurisdicción' = Jurisdiccion,
		'Núm. Unidad' = IdFarmacia, 'Nombre unidad' = Farmacia,		 
		'Piezas' = Sum(Cantidad) 
	From #tmpTransferencias
	Group By IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia
	Order By IdJurisdiccion, IdFarmacia
	
	
End 
Go--#SQL 

   