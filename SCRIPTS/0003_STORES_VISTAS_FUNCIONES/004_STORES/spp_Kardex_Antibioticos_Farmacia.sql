If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpKardex_Antibioticos_Farmacia' and xType = 'U' ) 
   Drop Table tmpKardex_Antibioticos_Farmacia 
Go--#SQL  


-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Kardex_Antibioticos_Farmacia' and xType = 'P' ) 
   Drop Proc spp_Kardex_Antibioticos_Farmacia 
Go--#SQL  

Create Proc	 spp_Kardex_Antibioticos_Farmacia 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0011',
	@IdClaveSSA varchar(4) = '*', @IdProducto varchar(8) = '',  
	@FechaInicial varchar(10) = '2014-05-01', @FechaFinal varchar(10) = '2016-10-31', @TipoReporte smallint = 0, 
	@ClaveLote varchar(30) = '' , @Tipo Int = 2
)    
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10)  

	Set @sNA = ' N/A ' 
	Set @sNA = '' 

--- Catalogos 		
	Select IdProducto, CodigoEAN, ClaveSSA, DescripcionClave  
	Into #tmpProductos_Antibioticos
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
	
	Select * 
	Into #tmpTransferencias 
	From vw_TransferenciasEnc T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia
	      and convert(varchar(10), T.FechaTransferencia, 120) between @FechaInicial and @FechaFinal 
--- Catalogos 		
			
			
	-- Se llena la tabla por los productos antibioticos existentes
	If @TipoReporte = 0
		Begin 
			Insert Into #tmpProductos_Antibioticos
			Select IdProducto, CodigoEAN, ClaveSSA, DescripcionClave 
			From vw_Productos_CodigoEAN (Nolock)
			Where EsAntibiotico = 1 
		End

	-- Se llena la tabla de los antibioticos por IdClaveSSA
	If @TipoReporte = 1
		Begin 
			Insert Into #tmpProductos_Antibioticos 
			Select IdProducto, CodigoEAN, ClaveSSA, DescripcionClave 
			From vw_Productos_CodigoEAN (Nolock)
			Where IdClaveSSA_Sal = @IdClaveSSA And EsAntibiotico = 1
		End 

	-- Se llena la tabla de los antibioticos por IdProducto
	If @TipoReporte = 2
		Begin 
			Insert Into #tmpProductos_Antibioticos 
			Select IdProducto, CodigoEAN, ClaveSSA, DescripcionClave  
			From vw_Productos_CodigoEAN (Nolock)
			Where IdProducto = @IdProducto	And EsAntibiotico = 1
		End


	--- Borrar la tabla base de los Datos 
	If exists ( select * from sysobjects (nolock) Where Name = 'tmpAntibioticos_Farmacia' and xType = 'U' ) 
		Drop table tmpAntibioticos_Farmacia 

	-- Se llena tabla temporal para hacer mas rapido el llenado base.
	Select 
		 E.IdEmpresa, E.IdEstado, E.IdFarmacia, cast(convert(varchar(10), E.FechaSistema, 112) as datetime) as FechaSistema, 
		 E.FechaRegistro, E.FolioMovtoInv as Folio, E.IdTipoMovto_Inv as TipoMovto, E.Referencia, 
		 D.IdProducto, D.CodigoEAN, P.ClaveSSA, P.DescripcionClave, L.ClaveLote, L.IdSubFarmacia, space(10) as FechaCaducidad,
		 
		 (Case When dbo.fg_INV_GetStatusProducto(L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else (Case When E.TipoES = 'E' Then cast(L.Cantidad as int) Else 0 End) End) as Entrada, 	 
		 (Case When dbo.fg_INV_GetStatusProducto(L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else (Case When E.TipoES = 'S' Then cast(L.Cantidad as int) Else 0 End) End) as Salida, 
		 (Case When dbo.fg_INV_GetStatusProducto(L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto) 
				In ( 'I', 'S' ) Then 0 Else D.Existencia End) as Existencia, 				
		 -- (Case When E.TipoES = 'E' Then cast(L.Cantidad as int) Else 0 End) as Entrada, 
		 -- (Case When E.TipoES = 'S' Then cast(L.Cantidad as int) Else 0 End) as Salida, 
		 -- D.Existencia, 
		 
		 D.Costo, D.Importe, L.Status, L.Keyx as Keyx
	Into #tmpKardex_Antibioticos_Base   
	From MovtosInv_Enc E (NoLock) 
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
		On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
		On ( D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
			 And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN)  		
	Inner Join #tmpProductos_Antibioticos P (NoLock) 
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.MovtoAplicado = 'S' 
		  and convert(varchar(10), E.FechaSistema, 120) between @FechaInicial and @FechaFinal 
		  -- and E.FolioMovtoInv = 'SV00067601' 
	Order by P.DescripcionClave, E.FechaSistema 
	

	
	------------ Quitar los lotes no requeridos 
	If @ClaveLote <> '' 
	Begin
	   Delete From #tmpKardex_Antibioticos_Base 
	   Where (IdProducto + ClaveLote) <> (@IdProducto + @ClaveLote) 
	End
	
	
	------------------- Informacion de Caducidades  
	Update B Set FechaCaducidad = convert(varchar(7), C.FechaCaducidad, 120) 
	From #tmpKardex_Antibioticos_Base B 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes C (NoLock) 
		On ( B.IdEmpresa = C.IdEmpresa and B.IdEstado = C.IdEstado and B.IdFarmacia = C.IdFarmacia and B.IdSubFarmacia = C.IdSubFarmacia 
			and B.IdProducto = C.IdProducto and B.CodigoEAN = C.CodigoEAN and B.ClaveLote = C.ClaveLote ) 
	 
 
	------------------- Informacion Base  
	Select V.IdEmpresa, space(102) As Empresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia,				  
		   V.FechaSistema, V.FechaRegistro, V.Folio,  right(V.Folio, 8) as Foliador, 
		   V.TipoMovto, space(52) As DescMovimiento, space(1) As EsMovtoGral, V.Referencia, 
		   V.IdProducto, V.CodigoEAN, space(202) As DescProducto,
		   V.ClaveLote, V.IdSubFarmacia, V.FechaCaducidad, 
		   space(6) As IdLaboratorio, space(102) As Laboratorio, space(5) As IdPresentacion, space(102) As Presentacion, 
		   space(6) As IdClaveSSA_Sal, space(52) As ClaveSSA_Base, 
		   space(52) As ClaveSSA, space(52) As ClaveSSA_Aux, 
		   V.DescripcionClave As DescripcionClave, 
		   space(10) as IdBeneficiario, space(200) as Beneficiario, Cast(right(V.Folio, 8) as varchar(30)) as NumReceta, 
		   space(15) as FechaReceta, space(10) as IdMedico, ''  + space(200) as Prescribe, 
		   ''  + space(50) as Cedula, ''  + space(200) as Domicilio, 
		   (case when V.TipoMovto = 'SV' then 'SI' else ''  end) + space(10) as RetieneReceta, 
		   sum(Entrada) as Entrada, sum(Salida) as Salida, sum(Existencia) as Existencia,
		   @FechaInicial As FechaInicial, @FechaFinal As FechaFinal, 		   
		   max(V.Keyx) as Keyx, 0 as OrdenReporte  
	Into tmpAntibioticos_Farmacia 
	-- Drop Table tmpAntibioticos_Farmacia
	From #tmpKardex_Antibioticos_Base V (NoLock)		
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )  
	Group by V.IdEmpresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia, V.FechaSistema, V.FechaRegistro, V.Folio, V.TipoMovto, 
		V.Referencia, V.ClaveLote, V.IdSubFarmacia, V.FechaCaducidad, V.DescripcionClave, V.IdProducto, V.CodigoEAN, V.Status
	Order By V.DescripcionClave, V.FechaRegistro 
	------------------- Informacion Base 
 

 --		spp_Kardex_Antibioticos_Farmacia
  
  
--- Asignar Nombre de Empresa
	Update K Set Empresa = Ce.Nombre  
	From tmpAntibioticos_Farmacia K 	
	Inner join CatEmpresas Ce (NoLock) On ( K.IdEmpresa = Ce.IdEmpresa ) 

---	Asignar Descripcion del Movimiento
	Update K Set DescMovimiento = E.Descripcion, EsMovtoGral = E.EsMovtoGral 
	From tmpAntibioticos_Farmacia K 	
	Inner join Movtos_Inv_Tipos E (NoLock) On ( K.TipoMovto = E.IdTipoMovto_Inv )

--- Asignar datos de Clave, Laboratorio, Presentacion 
	Update K Set DescProducto = P.Descripcion, IdClaveSSA_Sal = P.IdClaveSSA_Sal, 
		ClaveSSA_Base = P.ClaveSSA_Base, ClaveSSA = P.ClaveSSA, ClaveSSA_Aux = P.ClaveSSA_Aux, 
		-- DescripcionClave = P.DescripcionSal, 
		IdLaboratorio = P.IdLaboratorio, Laboratorio = P.Laboratorio, 
		IdPresentacion = P.IdPresentacion, Presentacion = P.Presentacion  
	From tmpAntibioticos_Farmacia K 	
	Inner Join #tmpProductos P On ( K.IdProducto = P.IdProducto ) 


--- Generar el orden del reporte 
	Select Distinct IdClaveSSA_Sal, ClaveSSA, DescripcionClave, identity(int, 1, 1) as OrdenReporte  
	into #tmpOrden 
	From tmpAntibioticos_Farmacia 
	Order by DescripcionClave 


	Update T Set OrdenReporte = O.OrdenReporte 
	From tmpAntibioticos_Farmacia T 
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
	From tmpAntibioticos_Farmacia K 	
	Inner Join VentasInformacionAdicional V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioVenta )
	Inner Join #tmpMedicos M (NoLock) On ( K.IdEstado = M.IdEstado and K.IdFarmacia = M.IdFarmacia and V.IdMedico = M.IdMedico ) 
	Where TipoMovto = 'SV'  

	Update K Set Beneficiario = B.NombreCompleto 	
	From tmpAntibioticos_Farmacia  K 	
	Inner Join VentasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioVenta )	
	Inner Join #tmpBeneficiarios B (NoLock) 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and 
				V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and K.IdBeneficiario = B.IdBeneficiario ) 		
	Where TipoMovto = 'SV' 	

 			
------------------------------------------------  COMPRAS  ------------------------------------------------
-----------------------------------------------------------------------------------------------------------
	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
	From tmpAntibioticos_Farmacia K 	
	Inner Join ComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioCompra )			
	Where TipoMovto = 'EC' 

	Update K Set Beneficiario = B.Nombre, Domicilio = UPPER(B.Estado + ', ' + B.Municipio + ' Colonia: ' + B.Colonia + ' ' + B.Domicilio)
	From tmpAntibioticos_Farmacia K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'EC'

----------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------  CANCELACION DE COMPRAS   -------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------
			
	Update K Set K.Referencia = V.Referencia		 
	From tmpAntibioticos_Farmacia K 	
	Inner Join DevolucionesEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Folio = V.FolioMovtoInv )
	Where TipoMovto = 'CC' 

	Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
	From tmpAntibioticos_Farmacia K 	
	Inner Join ComprasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioCompra )			
	Where TipoMovto = 'CC' 

	Update K Set Beneficiario = B.Nombre, Domicilio = UPPER(B.Estado + ', ' + B.Municipio + ' Colonia: ' + B.Colonia + ' ' + B.Domicilio)
	From tmpAntibioticos_Farmacia K 	
	Inner Join #tmpProveedores B (NoLock) On ( K.IdBeneficiario = B.IdProveedor ) 		
	Where TipoMovto = 'CC'

----------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------  DEVOLUCION DE VENTAS POR DISPENSACION --------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------- 

	Update K Set Referencia = V.Referencia		 
	From tmpAntibioticos_Farmacia K 	
	Inner Join DevolucionesEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioDevolucion )			
	Where TipoMovto = 'ED' 


	Update K Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120),  
		IdMedico = M.IdMedico,  
		Prescribe = 'Dr(a). ' + M.NombreCompleto, Cedula = M.NumCedula, Domicilio = M.Direccion 		 
	From tmpAntibioticos_Farmacia K 	
	Inner Join VentasInformacionAdicional V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioVenta )
	Inner Join #tmpMedicos M (NoLock) On ( V.IdEstado = M.IdEstado and V.IdFarmacia = M.IdFarmacia and V.IdMedico = M.IdMedico ) 
	Where TipoMovto = 'ED' 
 
	Update K Set Beneficiario = B.NombreCompleto 	
	From tmpAntibioticos_Farmacia K 	
	Inner Join VentasEnc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioVenta )	
	Inner Join #tmpBeneficiarios B (NoLock) 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and 
				V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and K.IdBeneficiario = B.IdBeneficiario ) 		
	Where TipoMovto = 'ED' 
 
----------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------- TRANSFERENCIAS DE SALIDA -------------------------------------------------------------------------
			
	Update K Set IdBeneficiario = T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
	From tmpAntibioticos_Farmacia K 	
	Inner Join #tmpTransferencias T On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio )
	Where TipoMovto = 'TS'  
		
-------------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------- TRANSFERENCIAS DE ENTRADA -------------------------------------------------------------------------
			
	Update K Set IdBeneficiario = T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
	From tmpAntibioticos_Farmacia K 	
	Inner Join #tmpTransferencias T On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio )
	Where TipoMovto = 'TE'  
		
-----------------------------------------------------------------------------------------------------------------------------------------------


------------------------------------------------  MOVIMIENTOS GENERALES  ----------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
	Update K Set IdBeneficiario = V.IdPersonalRegistra 	 
	From tmpAntibioticos_Farmacia K 	
	Inner Join MovtosInv_Enc V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Folio = V.FolioMovtoInv )						
	Where EsMovtoGral = 1 

	Update K Set Beneficiario = V.NombreCompleto
	From tmpAntibioticos_Farmacia K 	
	Inner Join vw_Personal V (NoLock) 
		On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.IdBeneficiario = V.IdPersonal )						
	Where EsMovtoGral = 1
			
			
			
----------------------------------------------------------------------------------------------------------------------------------------------
	--- Borrar la tabla base de los Datos
	If Exists ( select * from sysobjects (nolock) Where Name = 'tmpKardex_Antibioticos_Farmacia' and xType = 'U' ) 
		Drop table tmpKardex_Antibioticos_Farmacia

	Select identity(int, 1, 1) as Progresivo, 0 as Consecutivo, * 
	Into tmpKardex_Antibioticos_Farmacia
	From tmpAntibioticos_Farmacia
	Order By OrdenReporte, FechaRegistro 

	
--	drop table tmpKardex_Antibioticos_Farmacia___Identificador 
	
	Select TipoMovto, Folio, max(Progresivo) as Progresivo, identity(int, 1, 1) as Consecutivo 
	Into #tmpKardex_Antibioticos_Farmacia___Identificador 
	From tmpKardex_Antibioticos_Farmacia 
	Where TipoMovto = 'SV' 
	Group By TipoMovto, Folio 
	Order by TipoMovto, Folio 


	Update D Set Consecutivo = F.Consecutivo 
	From tmpKardex_Antibioticos_Farmacia D 
	Inner Join #tmpKardex_Antibioticos_Farmacia___Identificador F 
		On ( D.TipoMovto = F.TipoMovto and D.Folio = F.Folio ) 


--		spp_Kardex_Antibioticos_Farmacia  

--	select top 1 * from tmpAntibioticos_Farmacia 
	



	
----Declare @iCons int,
----	@Orden Int,
----	@CodigoEAN varchar(100),
----	@Keyx Int,
----	@TipoMovto varchar(100)	
	
	------if (@Tipo = 1)
	------Begin
	------	Select *,CAST(0 As numeric(14,0)) As Consecutivo  --ROW_NUMBER() OVER (PARTITION BY CodigoEAN ORDER BY OrdenReporte,idproducto, Keyx) as Consecutivo
	------	Into tmpKardex_Antibioticos_Farmacia
	------	From tmpAntibioticos_Farmacia
	------	--Where TipoMovto = 'SV'
	------	Order By OrdenReporte, Keyx
		
	------	Declare Llave_Cursor Cursor For
	------		Select Distinct OrdenReporte, CodigoEAN
	------		From tmpKardex_Antibioticos_Farmacia
	------		Order By OrdenReporte 
	------	open Llave_Cursor
	------	Fetch From Llave_Cursor into @Orden, @CodigoEAN
	------		while @@Fetch_status = 0 
	------		Begin
	------				Set @iCons = 1 
	------				--Select @Orden, @CodigoEAN
	------				Declare Llave_Interno Cursor For
	------					Select Keyx, TipoMovto
	------					From tmpKardex_Antibioticos_Farmacia
	------					Where OrdenReporte = @Orden And CodigoEAN = @CodigoEAN
	------					Order By CodigoEAN, Keyx
	------				open Llave_Interno
	------				Fetch From Llave_Interno into @Keyx, @TipoMovto
	------				while @@Fetch_status = 0 
	------					Begin
	------						if ( @TipoMovto = 'SV' )
	------							begin
	------								--Select @Orden, @CodigoEAN, @Keyx, @TipoMovto
	------								Update tmpKardex_Antibioticos_Farmacia Set Consecutivo =  @iCons Where OrdenReporte = @Orden And CodigoEAN = @CodigoEAN And Keyx = @Keyx
	------								Set @iCons = @iCons + 1
	------							End
	------						Fetch next From Llave_Interno into @Keyx, @TipoMovto	
	------					End
	------				close Llave_Interno
	------				deallocate Llave_Interno
	------				Fetch next From Llave_Cursor into @Orden, @CodigoEAN		
	------		End
	------	close Llave_Cursor
	------	deallocate Llave_Cursor
	------End
	
	
	------If (@Tipo = 2)
	------Begin
	------	Select *,CAST(0 As numeric(14,0)) As Consecutivo  --ROW_NUMBER() OVER (PARTITION BY CodigoEAN ORDER BY OrdenReporte,idproducto, Keyx) as Consecutivo
	------	Into tmpKardex_Antibioticos_Farmacia
	------	From tmpAntibioticos_Farmacia
	------	--Where TipoMovto = 'SV'
	------	Order By Keyx
				
	------	Set @iCons = 1
		
	------	--Select @Orden, @CodigoEAN 
	------	Declare Llave_Interno Cursor For
	------		Select Keyx, TipoMovto
	------		From tmpKardex_Antibioticos_Farmacia
	------		--Where OrdenReporte = @Orden And CodigoEAN = @CodigoEAN
	------		Order By Keyx
	------	open Llave_Interno
	------	Fetch From Llave_Interno into @Keyx, @TipoMovto
	------	while @@Fetch_status = 0 
	------		Begin
	------			if ( @TipoMovto = 'SV' )
	------				begin
	------					--Select @Orden, @CodigoEAN, @Keyx, @TipoMovto
	------					Update tmpKardex_Antibioticos_Farmacia Set Consecutivo =  @iCons Where Keyx = @Keyx
	------					Set @iCons = @iCons + 1
	------				End
	------			Fetch next From Llave_Interno into @Keyx, @TipoMovto	
	------		End
	------	close Llave_Interno
	------	deallocate Llave_Interno
	------End



----------------------------------------------------------------------------------------------------------------------------------------------
------		--		spp_Kardex_Antibioticos_Farmacia 
------		
--------			Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
--------				   FechaSistema, FechaRegistro, Folio, Foliador, TipoMovto, DescMovimiento, Referencia, 
--------				   IdProducto, CodigoEAN, DescProducto, IdLaboratorio, Laboratorio, IdPresentacion, Presentacion, 
--------				   IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
--------				   Beneficiario,NumReceta, FechaReceta, 		   
--------				   Prescribe, Cedula, Domicilio, RetieneReceta, 
--------				   Entrada, Salida, Existencia,  
--------				   Keyx, OrdenReporte, getdate() as FechaImpresion    
--------			From tmpAntibioticos_Farmacia			
--------			Order By DescripcionClave, DescProducto, FechaRegistro 
------
------		----     spp_Kardex_Antibioticos_Farmacia 
------ 
------ 
--------		Select * From tmpAntibioticos_Farmacia
--------		Order By TipoMovto
 
End 
Go--#SQL  
