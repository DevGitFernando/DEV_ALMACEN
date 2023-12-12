---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Kardex_General' and xType = 'P' ) 
   Drop Proc spp_Kardex_General 
Go--#SQL  

--		Exec spp_Kardex_General '001', '20', '44', '*', '7616', '2014-03-03', '2015-03-03', -1, ''	

Create Proc	 spp_Kardex_General 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '14', @IdFarmacia varchar(4) = '2',
	@IdClaveSSA varchar(4) = '0498', @IdProducto varchar(8) = '00000566',  
	@FechaInicial varchar(10) = '2009-07-01', @FechaFinal varchar(10) = '2015-12-31', @TipoReporte smallint = -1, 
	@ClaveLote varchar(30) = ''  
)    
With Encryption 
As 
Begin 
-- Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10)  

	Set @sNA = ' N/A ' 
	Set @sNA = '' 
	
---	Formatear Parametros 
	Set @IdEmpresa = right('000000000000' + IsNull(@IdEmpresa, '0'), 3) 
	Set @IdEstado = right('000000000000' + IsNull(@IdEstado, '0'), 2) 
	Set @IdFarmacia = right('000000000000' + IsNull(@IdFarmacia, '0'), 4) 
	Set @IdClaveSSA = right('000000000000' + IsNull(@IdClaveSSA, '0'), 4) 
	Set @IdProducto = right('000000000000' + IsNull(@IdProducto, '0'), 8) 


------------------------------------------------------ Catalogos 
	Select IdProducto, CodigoEAN
	Into #tmpProductos_General
	From vw_Productos_CodigoEAN (Nolock)
	Where 1 = 0 		

	Select * 
	Into #tmpFarmacias 
	From vw_Farmacias (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	Select * 
	into #tmpMedicos 
	From vw_Medicos	(NoLock) 	
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
	Select IdEstado, IdFarmacia, IdCliente, IdSubCliente,   
		 IdBeneficiario, ( ApPaterno + ' ' + ApMaterno + ' ' + Nombre ) as NombreCompleto 
	Into #tmpBeneficiarios 
	From CatBeneficiarios (NoLock) 	
	
	Select * 
	Into #tmpProveedores 
	From vw_Proveedores (NoLock) 	
	
	Select * 
	Into #tmpProductos 
	From vw_Productos (NoLock) 	
	
	Select *, 0 as Interestatal  
	Into #tmpTransferencias 
	From vw_TransferenciasEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia
	      and convert(varchar(10), T.FechaTransferencia, 120) between @FechaInicial and @FechaFinal 

	Update T Set Interestatal = 1 
	From #tmpTransferencias T 
	Where IdEstado <> IdEstadoRecibe  

------------------------------------------------------ Catalogos 
			
			
	-- Se llena la tabla por los productos antibioticos existentes
	If @TipoReporte = 0
		Begin 
			Insert Into #tmpProductos_General
			Select IdProducto, CodigoEAN						
			From vw_Productos_CodigoEAN (Nolock)
			Where EsControlado = 1
		End

	-- Se llena la tabla de los antibioticos por IdClaveSSA
	If @TipoReporte = 1
		Begin 
			Insert Into #tmpProductos_General
			Select IdProducto, CodigoEAN						
			From vw_Productos_CodigoEAN (Nolock)
			Where IdClaveSSA_Sal = @IdClaveSSA And EsControlado = 1
		End

	-- Se llena la tabla de los antibioticos por IdProducto
	If @TipoReporte = 2
		Begin 
			Insert Into #tmpProductos_General
			Select IdProducto, CodigoEAN						
			From vw_Productos_CodigoEAN (Nolock)
			Where IdProducto = @IdProducto And EsControlado = 1
		End


	If @TipoReporte = -1
		Begin 
			Insert Into #tmpProductos_General
			Select IdProducto, CodigoEAN						
			From vw_Productos_CodigoEAN (Nolock) 
			
			If @IdProducto <> '' 
			Begin 
				Delete From #tmpProductos_General Where IdProducto <> @IdProducto 
			End 						
		End 



	--- Borrar la tabla base de los Datos
	If exists ( select * from sysobjects (nolock) Where Name = 'tmpKardex_General' and xType = 'U' ) 
		Drop table tmpKardex_General

	-- Se llena tabla temporal para hacer mas rapido el llenado base.
	Select 
		 E.IdEmpresa, E.IdEstado, E.IdFarmacia, cast(convert(varchar(10), E.FechaSistema, 112) as datetime) as FechaSistema, 
		 E.FechaRegistro, E.FolioMovtoInv as Folio, E.IdTipoMovto_Inv as TipoMovto, E.Referencia, 
		 D.IdProducto, D.CodigoEAN, L.IdSubFarmacia, SPACE(100) as SubFarmacia, L.ClaveLote, space(10) as Caducidad, 
		 (Case When E.TipoES = 'E' Then cast(L.Cantidad as int) Else 0 End) as Entrada, 
		 (Case When E.TipoES = 'S' Then cast(L.Cantidad as int) Else 0 End) as Salida, 		 
		 L.Existencia, D.Costo, D.Importe, D.Status, D.Keyx as Keyx
	Into #tmpKardex_General   
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On ( D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
			 And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  				
	Inner Join #tmpProductos_General P (NoLock) 
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.MovtoAplicado = 'S' 
		  and convert(varchar(10), E.FechaSistema, 120) between @FechaInicial and @FechaFinal 
			
	If @ClaveLote <> '' 
	Begin
	   Delete From #tmpKardex_Antibioticos_Base 
	   Where (IdProducto + ClaveLote) <> (@IdProducto + @ClaveLote) 
	End 			
			
			
------------------- Informacion Base  
	Select V.IdEmpresa, space(102) As Empresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia,				  
		   V.FechaSistema, V.FechaRegistro, V.Folio,  right(V.Folio, 8) as Foliador, 
		   V.TipoMovto, space(52) As DescMovimiento, space(1) As EsMovtoGral, V.Referencia, 
		   V.IdProducto, V.CodigoEAN, space(202) As DescProducto, 
		   V.IdSubFarmacia, V.ClaveLote, v.Caducidad, 
		   
		   CAST(0 as numeric(14,4)) as SubTotalFactura, 
		   CAST(0 as numeric(14,4)) as IvaFactura, 
		   CAST(0 as numeric(14,4)) as ImporteFactura, 
		   CAST(0 as numeric(14,4)) as CostoUnitario, 
		   CAST(0 as numeric(14,4)) as TasaIva, 
		   		     
		   space(6) As IdLaboratorio, space(102) As Laboratorio, space(5) As IdPresentacion, space(102) As Presentacion, 
		   space(6) As IdClaveSSA_Sal, space(52) As ClaveSSA_Base, space(52) As ClaveSSA, space(52) As ClaveSSA_Aux, space(7502) As DescripcionClave, 
		   space(10) as IdBeneficiario, space(200) as Beneficiario, Cast(right(V.Folio, 8) as varchar(30)) as NumReceta, 
		   space(15) as FechaReceta, space(10) as IdMedico, ''  + space(200) as Prescribe, 
		   ''  + space(50) as Cedula, ''  + space(200) as Domicilio, 
		   (case when V.TipoMovto = 'SV' then 'SI' else ''  end) + space(10) as RetieneReceta, 
		   sum(Entrada) as Entrada, sum(Salida) as Salida, sum(Existencia) as Existencia,
		   @FechaInicial As FechaInicial, @FechaFinal As FechaFinal,			   
		   max(V.Keyx) as Keyx, 0 as OrdenReporte  
	Into tmpKardex_General
	-- Drop Table tmpKardex_General
	From #tmpKardex_General V (NoLock)		
	Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )  
	Group by V.IdEmpresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia,				
		V.FechaSistema, V.FechaRegistro, V.Folio, V.TipoMovto, 
		V.Referencia, V.IdProducto, V.CodigoEAN, V.IdSubFarmacia, V.ClaveLote, v.Caducidad, V.Status
	Order By V.FechaRegistro 
------------------- Informacion Base 


--- Asignar Nombre de Empresa
	Update K Set Empresa = Ce.Nombre  
	From tmpKardex_General K 	
	Inner join CatEmpresas Ce (NoLock) On ( K.IdEmpresa = Ce.IdEmpresa ) 

---	Asignar Descripcion del Movimiento
	Update K Set DescMovimiento = E.Descripcion, EsMovtoGral = E.EsMovtoGral 
	From tmpKardex_General K 	
	Inner join Movtos_Inv_Tipos E (NoLock) On ( K.TipoMovto = E.IdTipoMovto_Inv )

--- Asignar datos de Clave, Laboratorio, Presentacion 
	Update K Set 
		DescProducto = P.Descripcion, IdClaveSSA_Sal = P.IdClaveSSA_Sal, 
		ClaveSSA_Base = P.ClaveSSA_Base, ClaveSSA = P.ClaveSSA, ClaveSSA_Aux = P.ClaveSSA_Aux,
		DescripcionClave = P.DescripcionSal, IdLaboratorio = P.IdLaboratorio, Laboratorio = P.Laboratorio, 
		IdPresentacion = P.IdPresentacion, Presentacion = P.Presentacion  
	From tmpKardex_General K 	
	Inner Join #tmpProductos P On ( K.IdProducto = P.IdProducto )

---	Asignar Caducidades 
	Update K Set Caducidad = convert(varchar(7), P.FechaCaducidad, 120)  
	From tmpKardex_General K 	
	Inner Join FarmaciaProductos_CodigoEAN_Lotes P 
		On ( K.IdSubFarmacia = P.IdSubFarmacia And K.IdProducto = P.IdProducto And K.CodigoEAN = P.CodigoEAN and K.ClaveLote = P.ClaveLote )	



--- Generar el orden del reporte 
	Select Distinct IdClaveSSA_Sal, ClaveSSA, DescripcionClave, identity(int, 1, 1) as OrdenReporte  
	into #tmpOrden 
	From tmpKardex_General 
	Order by DescripcionClave 

	Update T Set OrdenReporte = O.OrdenReporte 
	From tmpKardex_General T 
	Inner Join #tmpOrden O On ( T.IdClaveSSA_Sal = O.IdClaveSSA_Sal )
--- Generar el orden del reporte 



--- Asignar Domicilio de la Farmacia 
	Update K Set Domicilio = (F.Domicilio + ', ' + F.Municipio + ', ' + F.Estado) 
	From tmpKardex_General K 	
	Inner Join #tmpFarmacias F On ( K.IdEstado = F.IdEstado and K.IdFarmacia = F.IdFarmacia ) 
--- Asignar Domicilio de la Farmacia 


----------------------------------------------  VENTAS  ----------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------ 		
	Update K Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120),  
		IdMedico = V.IdBeneficiario,  
		Prescribe = 'Dr(a). ' + M.NombreCompleto, Cedula = M.NumCedula  		 
	From tmpKardex_General K 	
	Inner Join VentasInformacionAdicional V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioVenta )
	Inner Join #tmpMedicos M (NoLock) On ( K.IdEstado = M.IdEstado and K.IdFarmacia = M.IdFarmacia and V.IdMedico = M.IdMedico ) 
	Where TipoMovto = 'SV' 

	Update K Set Beneficiario = B.NombreCompleto 	
	From tmpKardex_General K 	
	Inner Join VentasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioVenta )	
	Inner Join #tmpBeneficiarios B (NoLock) 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and 
				V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and K.IdBeneficiario = B.IdBeneficiario ) 		
	Where TipoMovto = 'SV' 

			
------------------------------------------------  COMPRAS  ------------------------------------------------
-----------------------------------------------------------------------------------------------------------
	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120), 
		K.SubTotalFactura = V.SubTotal, 
		K.IvaFactura = V.Iva, 		
		K.ImporteFactura = V.Total,   
		CostoUnitario =  D.CostoUnitario, 
		TasaIva = D.TasaIva 
	From tmpKardex_General K 	
	Inner Join ComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioCompra ) 
	Inner Join ComprasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioCompra = D.FolioCompra 
			and K.IdProducto = D.IdProducto and K.CodigoEAN = D.CodigoEAN ) 	
	Inner Join ComprasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioCompra = L.FolioCompra 
			and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and K.IdSubFarmacia = L.IdSubFarmacia and K.ClaveLote = L.ClaveLote ) 	
	Where TipoMovto = 'EC' 
	
	Update K Set Beneficiario = B.Nombre  	
	From tmpKardex_General K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'EC' 


----------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------  CANCELACION DE COMPRAS   -------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------
			
	Update K Set Referencia = V.Referencia		 
	From tmpKardex_General K 	
	Inner Join DevolucionesEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioDevolucion )			
	Where TipoMovto = 'CC' 

	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
	From tmpKardex_General K 	
	Inner Join ComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioCompra )			
	Where TipoMovto = 'CC' 

	Update K Set Beneficiario = B.Nombre  	
	From tmpKardex_General K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'CC'




------------------------------------------------  ORDENES DE COMPRAS  ------------------------------------------------
-----------------------------------------------------------------------------------------------------------
	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120), 
		K.SubTotalFactura = V.SubTotal, 
		K.IvaFactura = V.Iva, 		
		K.ImporteFactura = V.Total,   
		CostoUnitario =  D.CostoUnitario, 
		TasaIva = D.TasaIva 		 		  		 
	From tmpKardex_General K 	
	Inner Join OrdenesDeComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioOrdenCompra )		
	Inner Join OrdenesDeComprasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioOrdenCompra = D.FolioOrdenCompra 
			and K.IdProducto = D.IdProducto and K.CodigoEAN = D.CodigoEAN ) 	
	Inner Join OrdenesDeComprasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioOrdenCompra = L.FolioOrdenCompra 
			and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and K.IdSubFarmacia = L.IdSubFarmacia and K.ClaveLote = L.ClaveLote ) 				
	Where TipoMovto = 'EOC' 
	

	Update K Set Beneficiario = B.Nombre  	
	From tmpKardex_General K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'EOC' 


----------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------  CANCELACION ORDENES DE COMPRAS   -------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------
			
	Update K Set Referencia = V.Referencia		 
	From tmpKardex_General K 	
	Inner Join DevolucionesEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioDevolucion )			
	Where TipoMovto = 'DOC' 

	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
	From tmpKardex_General K 	
	Inner Join OrdenesDeComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioOrdenCompra )			
	Where TipoMovto = 'DOC' 

	Update K Set Beneficiario = B.Nombre  	
	From tmpKardex_General K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'DOC' 




----------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------  DEVOLUCION DE VENTAS POR DISPENSACION --------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------- 

	Update K Set Referencia = V.Referencia		 
	From tmpKardex_General K 	
	Inner Join DevolucionesEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioDevolucion )			
	Where TipoMovto = 'ED' 


	Update K Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120),  
		IdMedico = V.IdBeneficiario,  
		Prescribe = 'Dr(a). ' + M.NombreCompleto, Cedula = M.NumCedula  		 
	From tmpKardex_General K 	
	Inner Join VentasInformacionAdicional V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioVenta )
	Inner Join #tmpMedicos M (NoLock) On ( K.IdEstado = M.IdEstado and K.IdFarmacia = M.IdFarmacia and V.IdMedico = M.IdMedico ) 
	Where TipoMovto = 'ED' 

	Update K Set Beneficiario = B.NombreCompleto 	
	From tmpKardex_General K 	
	Inner Join VentasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioVenta )	
	Inner Join #tmpBeneficiarios B (NoLock) 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and 
				V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and K.IdBeneficiario = B.IdBeneficiario ) 		
	Where TipoMovto = 'ED' 

----------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------- TRANSFERENCIAS DE SALIDA -------------------------------------------------------------------------
	Update K Set IdBeneficiario = T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
	From tmpKardex_General K 	
	Inner Join #tmpTransferencias T On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio )
	Where TipoMovto = 'TS'  
	
	Update K Set IdBeneficiario = T.IdEstadoRecibe + '-' + T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
	From tmpKardex_General K 	
	Inner Join #tmpTransferencias T 
		On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio and T.Interestatal = 1 )
	Where TipoMovto = 'TS' 
		
-------------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------- TRANSFERENCIAS DE ENTRADA -------------------------------------------------------------------------
	Update K Set IdBeneficiario = T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
	From tmpKardex_General K 	
	Inner Join #tmpTransferencias T On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio )
	Where TipoMovto = 'TE'  
		
		
	Update K Set IdBeneficiario = T.IdEstadoRecibe + '-' + T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
	From tmpKardex_General K 	
	Inner Join #tmpTransferencias T 
		On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio and T.Interestatal = 1 )
	Where TipoMovto = 'TE'  		
-----------------------------------------------------------------------------------------------------------------------------------------------


------------------------------------------------  MOVIMIENTOS GENERALES  ----------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
	Update K Set IdBeneficiario = V.IdPersonalRegistra 	 
	From tmpKardex_General K 	
	Inner Join MovtosInv_Enc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Folio = V.FolioMovtoInv )						
	Where EsMovtoGral = 1 

	Update K Set Beneficiario = V.NombreCompleto
	From tmpKardex_General K 	
	Inner Join vw_Personal V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.IdBeneficiario = V.IdPersonal )						
	Where EsMovtoGral = 1
			

----------------------------------------------------------------------------------------------------------------------------------------------
		--		spp_Kardex_General 

		
--			Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
--				   FechaSistema, FechaRegistro, Folio, Foliador, TipoMovto, DescMovimiento, Referencia, 
--				   IdProducto, CodigoEAN, DescProducto, IdLaboratorio, Laboratorio, IdPresentacion, Presentacion, 
--				   IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
--				   Beneficiario,NumReceta, FechaReceta, 		   
--				   Prescribe, Cedula, Domicilio, RetieneReceta, 
--				   Entrada, Salida, Existencia,  
--				   Keyx, OrdenReporte, getdate() as FechaImpresion    
--			From tmpKardex_General			
--			Order By DescripcionClave, DescProducto, FechaRegistro 

		----     spp_Kardex_General 

 
--		Select * From tmpKardex_General
--		Order By TipoMovto
 
End 
Go--#SQL  