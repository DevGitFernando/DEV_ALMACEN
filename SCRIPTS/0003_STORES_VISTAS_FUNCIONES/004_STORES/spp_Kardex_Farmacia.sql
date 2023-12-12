---------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Kardex_Farmacia' and xType = 'P' ) 
   Drop Proc spp_Kardex_Farmacia 
Go--#SQL  

Create Proc	 spp_Kardex_Farmacia 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '3182',
	@IdClaveSSA varchar(4) = '*', @IdProducto varchar(8) = '',  
	@FechaInicial varchar(10) = '2018-01-01', @FechaFinal varchar(10) = '2018-12-10', @TipoReporte smallint = 0, 
	@ClaveLote varchar(30) = '' , @TipoDeClave Int = 0 
)    
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10), 
		@iCons int,
		@Orden Int,
		@CodigoEAN varchar(100), 
		@Keyx Int,
		@TipoMovto varchar(100), 
		@bCrearTablaBase int 

	Set @sNA = ' N/A ' 
	Set @sNA = ''
	Set @bCrearTablaBase = 1 

----- Catalogos 		
	Select *
	Into #tmpProductos
	From vw_Productos_CodigoEAN (Nolock)
	Where 1 = 0 


	------------------------------------------ Generar tablas de catalogos     
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN___Kardex' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_Productos_CodigoEAN___Kardex' and xType = 'U' and datediff(hh, crDate, getdate()) > 1  
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 	

		If @bCrearTablaBase = 1 Drop Table vw_Productos_CodigoEAN___Kardex 
	End 


	If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN___Kardex' and xType = 'U' )
	Begin 
		Select *
		Into vw_Productos_CodigoEAN___Kardex 
		From vw_Productos_CodigoEAN (Nolock)
	End 



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
	Into #tmpTransferencias 
	From vw_TransferenciasEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia
	      and convert(varchar(10), T.FechaTransferencia, 120) between @FechaInicial and @FechaFinal
	      
	      
	If @TipoDeClave = 0 
	Begin 
		If @TipoReporte = 0 
			Begin 
				Insert Into #tmpProductos 
				Select * 
				From vw_Productos_CodigoEAN___Kardex (Nolock) 
				Where EsAntibiotico = 0 And EsControlado = 0 
			End

		-- Se llena la tabla de los antibioticos por IdClaveSSA
		If @TipoReporte = 1 
			Begin 
				Insert Into #tmpProductos 
				Select * 
				From vw_Productos_CodigoEAN___Kardex (Nolock) 
				Where IdClaveSSA_Sal = @IdClaveSSA And EsAntibiotico = 0 And EsControlado = 0 
			End 

		-- Se llena la tabla de los antibioticos por IdProducto 
		If @TipoReporte = 2 
			Begin 
				Insert Into #tmpProductos 
				Select * 
				From vw_Productos_CodigoEAN___Kardex (Nolock) 
				Where IdProducto = @IdProducto	And EsAntibiotico = 0 And EsControlado = 0 
			End 
	End
	
	If @TipoDeClave = 1 
		Begin
			If @TipoReporte = 0 
				Begin 
					Insert Into #tmpProductos 
					Select *
					From vw_Productos_CodigoEAN___Kardex (Nolock) 
					Where EsAntibiotico = 1  
				End

			-- Se llena la tabla de los antibioticos por IdClaveSSA
			If @TipoReporte = 1 
				Begin 
					Insert Into #tmpProductos 
					Select * 
					From vw_Productos_CodigoEAN___Kardex (Nolock) 
					Where IdClaveSSA_Sal = @IdClaveSSA And EsAntibiotico = 1 
				End  

			-- Se llena la tabla de los antibioticos por IdProducto 
			If @TipoReporte = 2 
				Begin  
					Insert Into #tmpProductos 
					Select * 
					From vw_Productos_CodigoEAN___Kardex (Nolock) 
					Where IdProducto = @IdProducto	And EsAntibiotico = 1 
				End  
		End 

	If @TipoDeClave = 2 
		Begin 
			If @TipoReporte = 0 
				Begin 
					Insert Into #tmpProductos 
					Select * 
					From vw_Productos_CodigoEAN___Kardex (Nolock) 
					Where EsControlado = 1  
				End

			-- Se llena la tabla de los antibioticos por IdClaveSSA
			If @TipoReporte = 1 
				Begin  
					Insert Into #tmpProductos 
					Select * 
					From vw_Productos_CodigoEAN___Kardex (Nolock) 
					Where IdClaveSSA_Sal = @IdClaveSSA And EsControlado = 1 
				End 

			-- Se llena la tabla de los antibioticos por IdProducto
			If @TipoReporte = 2 
				Begin  
					Insert Into #tmpProductos 
					Select * 
					From vw_Productos_CodigoEAN___Kardex (Nolock) 
					Where IdProducto = @IdProducto	And EsControlado = 1 
				End
		End
 

	------------ Se llena tabla temporal para hacer mas rapido el llenado base.
	Select *
	Into #MovtosInv_Enc
	From MovtosInv_Enc E 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.MovtoAplicado = 'S' 
		  and convert(varchar(10), E.FechaSistema, 120) between @FechaInicial and @FechaFinal

	Select 
		 E.IdEmpresa, E.IdEstado, E.IdFarmacia, cast(convert(varchar(10), E.FechaSistema, 112) as datetime) as FechaSistema, 
		 E.FechaRegistro, E.FolioMovtoInv as Folio, E.IdTipoMovto_Inv as TipoMovto, E.Referencia, 
		 D.IdProducto, D.CodigoEAN, DescripcionClave,
		 L.ClaveLote, 
		 getdate() as FechaIngresoInicialLote, 
		 L.IdSubFarmacia, space(10) as FechaCaducidad, Cast('' As Varchar(2)) As ContieneCaducados,
		 (Case When dbo.fg_INV_GetStatusProducto(L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else (Case When E.TipoES = 'E' Then cast(L.Cantidad as int) Else 0 End) End) as Entrada, 	 
		 (Case When dbo.fg_INV_GetStatusProducto(L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else (Case When E.TipoES = 'S' Then cast(L.Cantidad as int) Else 0 End) End) as Salida, 
		 (Case When dbo.fg_INV_GetStatusProducto(L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else L.Existencia End) as Existencia, 
		 D.Costo, D.Importe, D.Status, L.Keyx as Keyx 
	Into #tmpKardex_Base
	From #MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On ( L.IdEmpresa = D.IdEmpresa and L.IdEstado = D.IdEstado and L.IdFarmacia = D.IdFarmacia and L.FolioMovtoInv = D.FolioMovtoInv 
			 And L.IdProducto = D.IdProducto and L.CodigoEAN = D.CodigoEAN)  				
	Inner Join #tmpProductos P (NoLock) 
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Order by E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FechaRegistro 	  
	

	------------ Quitar los lotes no requeridos 
	If @ClaveLote <> '' 
	Begin
	   Delete From #tmpKardex_Base 
	   Where (IdProducto + ClaveLote) <> (@IdProducto + @ClaveLote) 
	End
	
	
	------------------- Informacion de Caducidades  
	Update B
	Set FechaCaducidad = convert(varchar(7), C.FechaCaducidad, 120), FechaIngresoInicialLote = C.FechaRegistro, ContieneCaducados = (Case When C.FechaRegistro >= C.FechaCaducidad then 'SI' Else 'NO' End)
	From #tmpKardex_Base B 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes C (NoLock) 
		On ( B.IdEmpresa = C.IdEmpresa and B.IdEstado = C.IdEstado and B.IdFarmacia = C.IdFarmacia and B.IdSubFarmacia = C.IdSubFarmacia 
			and B.IdProducto = C.IdProducto and B.CodigoEAN = C.CodigoEAN and B.ClaveLote = C.ClaveLote )
			
	 
 	--- Borrar la tabla base de los Datos
	If Exists ( select * from sysobjects (nolock) Where Name = '#tmp_Farmacia' and xType = 'U' ) 
		Drop table #tmp_Farmacia
		
	------------------- Informacion Base  
	Select 
		   V.IdEmpresa, space(102) As Empresa, 
		   V.IdEstado, F.Estado, 
		   V.IdFarmacia, F.Farmacia,				  
		   V.FechaSistema, V.FechaRegistro, V.Folio, 
		   right(V.Folio, 8) as Foliador, 
		   V.TipoMovto, space(52) As DescMovimiento, space(1) As EsMovtoGral, V.Referencia, 
		   V.IdProducto, V.CodigoEAN, space(202) As DescProducto,
		   V.ClaveLote, V.FechaIngresoInicialLote,
		   (V.codigoEAN + ' -- ' + V.ClaveLote) As 'Nota Interna',
		   V.IdSubFarmacia, V.FechaCaducidad, V.ContieneCaducados,
		   space(6) As IdLaboratorio, space(102) As Laboratorio, space(5) As IdPresentacion, space(102) As Presentacion, 
		   space(6) As IdClaveSSA_Sal, space(52) As ClaveSSA_Base, 
		   space(52) As ClaveSSA, 
		   
		   -- space(52) As ClaveSSA_Aux, 

		   V.DescripcionClave As DescripcionClave, 
		   cast('' as varchar(100)) as TipoDeInsumo, 
		   space(10) as IdBeneficiario, space(200) as Beneficiario, Cast(right(V.Folio, 8) as varchar(30)) as NumReceta, 
		   space(15) as FechaReceta, space(10) as IdMedico, ''  + space(200) as Prescribe, 
		   ''  + space(50) as Cedula, ''  + space(200) as Domicilio, 
		   (case when V.TipoMovto = 'SV' then 'SI' else ''  end) + space(10) as RetieneReceta, 
		   sum(Entrada) as Entrada, sum(Salida) as Salida, sum(Existencia) as Existencia,
		   @FechaInicial As FechaInicial, @FechaFinal As FechaFinal, 		   
		   max(V.Keyx) as Keyx, 0 as OrdenReporte  
	Into #tmp_Farmacia 
	-- Drop Table tmpAntibioticos_Farmacia
	From #tmpKardex_Base V (NoLock)		
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )  
	Group by V.IdEmpresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia, V.FechaSistema, V.FechaRegistro, V.Folio, V.TipoMovto, 
		V.Referencia, V.ClaveLote, V.FechaIngresoInicialLote, V.IdSubFarmacia, V.FechaCaducidad, V.ContieneCaducados, V.DescripcionClave, V.IdProducto, V.CodigoEAN, V.Status
	Order By V.DescripcionClave, V.FechaRegistro 
	------------------- Informacion Base


--- Asignar Nombre de Empresa
	Update K Set Empresa = Ce.Nombre  
	From #tmp_Farmacia K 	
	Inner join CatEmpresas Ce (NoLock) On ( K.IdEmpresa = Ce.IdEmpresa ) 

---	Asignar Descripcion del Movimiento
	Update K Set DescMovimiento = E.Descripcion, EsMovtoGral = E.EsMovtoGral 
	From #tmp_Farmacia K 	
	Inner join Movtos_Inv_Tipos E (NoLock) On ( K.TipoMovto = E.IdTipoMovto_Inv )

--- Asignar datos de Clave, Laboratorio, Presentacion 
	Update K Set DescProducto = P.Descripcion, IdClaveSSA_Sal = P.IdClaveSSA_Sal, 
		ClaveSSA_Base = P.ClaveSSA_Base, ClaveSSA = P.ClaveSSA, --ClaveSSA_Aux = P.ClaveSSA_Aux, 
		TipoDeInsumo = P.TipoDeClaveDescripcion, 
		-- DescripcionClave = P.DescripcionSal, 
		IdLaboratorio = P.IdLaboratorio, Laboratorio = P.Laboratorio, 
		IdPresentacion = P.IdPresentacion, Presentacion = P.Presentacion  
	From #tmp_Farmacia K 	
	Inner Join #tmpProductos P On ( K.IdProducto = P.IdProducto ) 

--- Generar el orden del reporte 
	Select Distinct IdClaveSSA_Sal, ClaveSSA, DescripcionClave, identity(int, 1, 1) as OrdenReporte  
	into #tmpOrden 
	From #tmp_Farmacia 
	Order by DescripcionClave 


	Update T Set OrdenReporte = O.OrdenReporte 
	From #tmp_Farmacia T 
	Inner Join #tmpOrden O On ( T.IdClaveSSA_Sal = O.IdClaveSSA_Sal )
--- Generar el orden del reporte 



--- Asignar Domicilio de la Farmacia 
	--Update K Set Domicilio = (F.Domicilio + ', ' + F.Municipio + ', ' + F.Estado) 
	--From tmpAntibioticos_Farmacia K 	
	--Inner Join #tmpFarmacias F On ( K.IdEstado = F.IdEstado and K.IdFarmacia = F.IdFarmacia ) 
--- Asignar Domicilio de la Farmacia
 

 
----------------------------------------------  VENTAS  ----------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------  
	Update K Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120),  
		IdMedico = M.IdMedico,  
		Prescribe = 'Dr(a). ' + M.NombreCompleto, Cedula = M.NumCedula, Domicilio = M.Direccion
	From #tmp_Farmacia K 	
	Inner Join VentasInformacionAdicional V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioVenta )
	Inner Join #tmpMedicos M (NoLock) On ( K.IdEstado = M.IdEstado and K.IdFarmacia = M.IdFarmacia and V.IdMedico = M.IdMedico ) 
	Where TipoMovto = 'SV'  

	Update K Set Beneficiario = B.NombreCompleto 	
	From #tmp_Farmacia  K 	
	Inner Join VentasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioVenta )	
	Inner Join #tmpBeneficiarios B (NoLock) 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and 
				V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and K.IdBeneficiario = B.IdBeneficiario ) 		
	Where TipoMovto = 'SV' 	

 			
------------------------------------------------  COMPRAS  ------------------------------------------------
-----------------------------------------------------------------------------------------------------------
	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = 'FACT: ' + V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
	From #tmp_Farmacia K 	
	Inner Join ComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioCompra )			
	Where TipoMovto = 'EC' 

	Update K Set Beneficiario = 'PROV: ' + B.Nombre, Domicilio = UPPER(B.Estado + ', ' + B.Municipio + ' Colonia: ' + B.Colonia + ' ' + B.Domicilio)
	From #tmp_Farmacia K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'EC' 

----------------------------------------------------------------------------------------------------------------------------------------------


------------------------------------------------  CANCELACION DE COMPRAS   -------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------- 			
	Update K Set K.Referencia = V.Referencia		 
	From #tmp_Farmacia K 	
	Inner Join DevolucionesEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Folio = V.FolioMovtoInv )
	Where TipoMovto = 'CC' 

	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
	From #tmp_Farmacia K 	
	Inner Join ComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioCompra )			
	Where TipoMovto = 'CC' 

	Update K Set Beneficiario = B.Nombre, Domicilio = UPPER(B.Estado + ', ' + B.Municipio + ' Colonia: ' + B.Colonia + ' ' + B.Domicilio)
	From #tmp_Farmacia K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'CC' 
----------------------------------------------------------------------------------------------------------------------------------------------


------------------------------------------------  ORDENES DE COMPRAS  ------------------------------------------------
-----------------------------------------------------------------------------------------------------------
	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = 'FACT: ' + V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
	From #tmp_Farmacia K 	
	Inner Join OrdenesDeComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioOrdenCompra )			
	Where TipoMovto = 'EOC' 

	Update K Set Beneficiario = 'PROV: ' + B.Nombre, Domicilio = UPPER(B.Estado + ', ' + B.Municipio + ' Colonia: ' + B.Colonia + ' ' + B.Domicilio)
	From #tmp_Farmacia K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'EOC' 

---------------------------------------------------------------------------------------------------------------------------------------------- 


------------------------------------------------  CANCELACION DE ORDENES DE COMPRAS   -------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------- 			
	Update K Set K.Referencia = V.Referencia		 
	From #tmp_Farmacia K 	
	Inner Join DevolucionesEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Folio = V.FolioMovtoInv )
	Where TipoMovto = 'DOC' 

	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = 'FACT: ' + V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
	From #tmp_Farmacia K 	
	Inner Join OrdenesDeComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioOrdenCompra )			
	Where TipoMovto = 'DOC' 

	Update K Set Beneficiario = 'PROV: ' + B.Nombre, Domicilio = UPPER(B.Estado + ', ' + B.Municipio + ' Colonia: ' + B.Colonia + ' ' + B.Domicilio)
	From #tmp_Farmacia K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'DOC' 
----------------------------------------------------------------------------------------------------------------------------------------------



----------------------------------------------  DEVOLUCION DE VENTAS POR DISPENSACION --------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------- 

	Update K Set Referencia = V.Referencia		 
	From #tmp_Farmacia K 	
	Inner Join DevolucionesEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia And K.Folio = V.FolioMovtoInv )			
	Where TipoMovto = 'ED' 


	Update K Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120),  
		IdMedico = M.IdMedico,  
		Prescribe = 'Dr(a). ' + M.NombreCompleto, Cedula = M.NumCedula, Domicilio = M.Direccion 		 
	From #tmp_Farmacia K 	
	Inner Join VentasInformacionAdicional V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioVenta )
	Inner Join #tmpMedicos M (NoLock) On ( V.IdEstado = M.IdEstado and V.IdFarmacia = M.IdFarmacia and V.IdMedico = M.IdMedico ) 
	Where TipoMovto = 'ED' 
 
	Update K Set Beneficiario = B.NombreCompleto 	
	From #tmp_Farmacia K 	
	Inner Join VentasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioVenta )	
	Inner Join #tmpBeneficiarios B (NoLock) 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and 
				V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and K.IdBeneficiario = B.IdBeneficiario ) 		
	Where TipoMovto = 'ED' 
 

----------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------- TRANSFERENCIAS DE SALIDA -------------------------------------------------------------------------			
	Update K Set IdBeneficiario = T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
	From #tmp_Farmacia K 	
	Inner Join #tmpTransferencias T On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio )
	Where TipoMovto = 'TS'  
		
-------------------------------------------------------------------------------------------------------------------------------------------------



----------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------- TRANSFERENCIAS DE ENTRADA -------------------------------------------------------------------------
			
	Update K Set IdBeneficiario = T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
	From #tmp_Farmacia K 	
	Inner Join #tmpTransferencias T On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio )
	Where TipoMovto = 'TE'  
		
-----------------------------------------------------------------------------------------------------------------------------------------------


------------------------------------------------  MOVIMIENTOS GENERALES  ----------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
	Update K Set IdBeneficiario = V.IdPersonalRegistra 	 
	From #tmp_Farmacia K 	
	Inner Join MovtosInv_Enc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Folio = V.FolioMovtoInv )						
	Where EsMovtoGral = 1 

	Update K Set Beneficiario = V.NombreCompleto
	From #tmp_Farmacia K 	
	Inner Join vw_Personal V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.IdBeneficiario = V.IdPersonal )						
	Where EsMovtoGral = 1
			
			
			
----------------------------------------------------------------------------------------------------------------------------------------------
	--- Borrar la tabla base de los Datos
	--If Exists ( select * from sysobjects (nolock) Where Name = 'tmpKardex_Farmacia' and xType = 'U' ) 
	--	Drop table #tmpKardex_Farmacia

	Select identity(int, 1, 1) as Progresivo, 0 as ConsecutivoGeneral, 0 as ConsecutivoCodigoEAN, * 
	Into #tmpKardex_Farmacia_progresivo 
	From #tmp_Farmacia
	Order By FechaRegistro -- Keyx--OrdenReporte, FechaRegistro 

	
----	drop table tmpKardex_Antibioticos_Farmacia___Identificador 
	
--	Select distinct TipoMovto, Folio, max(Progresivo) as Progresivo, identity(int, 1, 1) as ConsecutivoGeneral 
--	Into #tmpKardex_Farmacia___Identificador 
--	From #tmpKardex_Farmacia_progresivo
--	Where TipoMovto = 'SV' 
--	Group By TipoMovto, Folio 
--	Order by TipoMovto, Folio 


--	Update D Set ConsecutivoGeneral = F.ConsecutivoGeneral 
--	From #tmpKardex_Farmacia_progresivo D 
--	Inner Join #tmpKardex_Farmacia___Identificador F 
--		On ( D.TipoMovto = F.TipoMovto and D.Folio = F.Folio ) 


--	----------------------------------------------------------------------------------------------------------------------------------------------
--	--- Borrar la tabla base de los Datos
--	--If exists ( select * from sysobjects (nolock) Where Name = 'tmpKardex_Farmacia' and xType = 'U' ) 
--	--	Drop table tmpKardex_Farmacia
		
--	--Select *, CAST(0 As numeric(14,0)) As Consecutivo  -- ROW_NUMBER() OVER (PARTITION BY CodigoEAN ORDER BY OrdenReporte,idproducto, Keyx) as Consecutivo
--	--Into #tmpKardex_Farmacia_Consecutivo
--	--From #tmpKardex_Farmacia_progresivo
--	--Order By OrdenReporte, Keyx
	

--	Declare Llave_Cursor Cursor For
--		Select Distinct OrdenReporte, CodigoEAN
--		From #tmpKardex_Farmacia_progresivo
--		Order By OrdenReporte
--	open Llave_Cursor
--	Fetch From Llave_Cursor into @Orden, @CodigoEAN
--		while @@Fetch_status = 0 
--		Begin
--				Set @iCons = 1
--				--Select @Orden, @CodigoEAN
--				Declare Llave_Interno Cursor For
--					Select Keyx, TipoMovto
--					From #tmpKardex_Farmacia_progresivo
--					Where OrdenReporte = @Orden And CodigoEAN = @CodigoEAN
--					Order By CodigoEAN, Keyx
--				open Llave_Interno
--				Fetch From Llave_Interno into @Keyx, @TipoMovto
--				while @@Fetch_status = 0 
--					Begin
--						if ( @TipoMovto = 'SV' )
--							begin
--								--Select @Orden, @CodigoEAN, @Keyx, @TipoMovto
--								Update #tmpKardex_Farmacia_progresivo Set ConsecutivoCodigoEAN =  @iCons Where OrdenReporte = @Orden And CodigoEAN = @CodigoEAN And Keyx = @Keyx
--								Set @iCons = @iCons + 1
--							End
--						Fetch next From Llave_Interno into @Keyx, @TipoMovto	
--					End
--				close Llave_Interno
--				deallocate Llave_Interno
--				Fetch next From Llave_Cursor into @Orden, @CodigoEAN		
--		End
--	close Llave_Cursor
--	deallocate Llave_Cursor
	

-------------------	SALIDA FINAL 
	--If @TipoDeClave = 1
	--	Begin
			Select
				Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, DescMovimiento, ClaveSSA, DescProducto,
				Sum(Entrada) As Entrada, Sum(Salida) As Salida, Sum(Existencia) As Existencia
			From #tmpKardex_Farmacia_progresivo (Nolock)
			Group By Convert(varchar(10), FechaRegistro, 120), Folio, DescMovimiento, ClaveSSA, DescProducto
			Order By ClaveSSA, DescProducto, FechaRegistro
	--	End
	
	--Select * From #tmpKardex_Farmacia___Identificador
	

	Select * 
	From #tmpKardex_Farmacia_progresivo (Nolock) 
	Order By progresivo--, Consecutivo
	
-------------------	SALIDA FINAL 

	--Select F.*, D.* 
	--From #tmpKardex_Farmacia_progresivo D 
	--Inner Join #tmpKardex_Farmacia___Identificador F 
	--	On ( D.TipoMovto = F.TipoMovto and D.Folio = F.Folio ) 
	
	
	--If @TipoDeClave = 2
	--	Begin
			--Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia, ClaveLote, IdSubFarmacia, FechaCaducidad
			--From #tmpKardex_Farmacia_Consecutivo (Nolock)
			--Order By IdClaveSSA_Sal, IdProducto, FechaRegistro
		--End
 
End 
Go--#SQL  

