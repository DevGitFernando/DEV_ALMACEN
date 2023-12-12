
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Kardex_Antibioticos_Farmacia_Lotes' and xType = 'P' ) 
   Drop Proc spp_Kardex_Antibioticos_Farmacia_Lotes 
Go--#SQL  

Create Proc	 spp_Kardex_Antibioticos_Farmacia_Lotes( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008',
	@IdClaveSSA varchar(4) = '0498', @IdProducto varchar(8) = '00000566',  
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-01-31', @TipoReporte smallint = 0, @ClaveLote varchar(30) = '' )    
With Encryption 
As 
Begin 
-- Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10)  

	Set @sNA = ' N/A ' 
	Set @sNA = '' 

	Select IdProducto, CodigoEAN
		Into #tmpProductos_Antibioticos
	From vw_Productos_CodigoEAN (Nolock)
	Where 1 = 0 		
			
			-- Se llena la tabla por los productos antibioticos existentes
			If @TipoReporte = 0
				Begin 
					Insert Into #tmpProductos_Antibioticos
					Select IdProducto, CodigoEAN						
					From vw_Productos_CodigoEAN (Nolock)
					Where EsAntibiotico = 1
				End

			-- Se llena la tabla de los antibioticos por IdClaveSSA
			If @TipoReporte = 1
				Begin 
					Insert Into #tmpProductos_Antibioticos
					Select IdProducto, CodigoEAN						
					From vw_Productos_CodigoEAN (Nolock)
					Where IdClaveSSA_Sal = @IdClaveSSA And EsAntibiotico = 1
				End

			-- Se llena la tabla de los antibioticos por IdProducto
			If @TipoReporte = 2
				Begin 
					Insert Into #tmpProductos_Antibioticos
					Select IdProducto, CodigoEAN						
					From vw_Productos_CodigoEAN (Nolock)
					Where IdProducto = @IdProducto	And EsAntibiotico = 1
				End


			--- Borrar la tabla base de los Datos
			If exists ( select * from sysobjects (nolock) Where Name = 'tmpKardex_Antibioticos_Farmacia_Lotes' and xType = 'U' ) 
				Drop table tmpKardex_Antibioticos_Farmacia_Lotes

			-- Se llena tabla temporal para hacer mas rapido el llenado base.
			Select 
					 E.IdEmpresa, E.IdEstado, E.IdFarmacia, cast(convert(varchar(10), E.FechaSistema, 112) as datetime) as FechaSistema, 
					 E.FechaRegistro, E.FolioMovtoInv as Folio, E.IdTipoMovto_Inv as TipoMovto, E.Referencia, 
					 D.IdProducto, D.CodigoEAN,  
					 (Case When E.TipoES = 'E' Then cast(D.Cantidad as int) Else 0 End) as Entrada, 
					 (Case When E.TipoES = 'S' Then cast(D.Cantidad as int) Else 0 End) as Salida, 		 
					 D.Existencia, D.Costo, D.Importe, D.Status, D.Keyx as Keyx
				Into #tmptmpKardex_Antibioticos_Base   
				From MovtosInv_Enc E (NoLock) 
				Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
					On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv ) 
				Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
					On ( D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioMovtoInv = L.FolioMovtoInv 
						 And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN )   
				Inner Join #tmpProductos_Antibioticos P (NoLock) 
					On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
 				Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.MovtoAplicado = 'S' 
					  and convert(varchar(10), E.FechaSistema, 120) between @FechaInicial and @FechaFinal 
					  And L.ClaveLote = @ClaveLote
			
		------------------- Informacion Base  

			Select V.IdEmpresa, space(102) As Empresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia,				  
				   V.FechaSistema, V.FechaRegistro, V.Folio,  right(V.Folio, 8) as Foliador, 
				   V.TipoMovto, space(52) As DescMovimiento, space(1) As EsMovtoGral, V.Referencia, 
				   V.IdProducto, V.CodigoEAN, space(202) As DescProducto, 
				   space(6) As IdLaboratorio, space(102) As Laboratorio, space(5) As IdPresentacion, space(102) As Presentacion, 
				   space(6) As IdClaveSSA_Sal, space(52) As ClaveSSA, space(7502) As DescripcionClave, 
				   space(10) as IdBeneficiario, space(200) as Beneficiario, Cast(right(V.Folio, 8) as varchar(30)) as NumReceta, 
				   space(15) as FechaReceta, space(10) as IdMedico, ''  + space(200) as Prescribe, 
				   ''  + space(50) as Cedula, ''  + space(200) as Domicilio, 
				   (case when V.TipoMovto = 'SV' then 'SI' else ''  end) + space(10) as RetieneReceta, 
				   sum(Entrada) as Entrada, sum(Salida) as Salida, sum(Existencia) as Existencia,
				   @FechaInicial As FechaInicial, @FechaFinal As FechaFinal, 		   
				   max(V.Keyx) as Keyx, 0 as OrdenReporte  
			Into tmpKardex_Antibioticos_Farmacia_Lotes
			-- Drop Table tmpKardex_Antibioticos_Farmacia_Lotes
			From #tmptmpKardex_Antibioticos_Base V (NoLock)		
			Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )  
			Group by V.IdEmpresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia,				
				V.FechaSistema, V.FechaRegistro, V.Folio, V.TipoMovto, 
				V.Referencia, V.IdProducto, V.CodigoEAN, V.Status
			Order By V.FechaRegistro 
		------------------- Informacion Base 


			--- Asignar Nombre de Empresa
			Update K Set Empresa = Ce.Nombre  
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner join CatEmpresas Ce (NoLock) On ( K.IdEmpresa = Ce.IdEmpresa ) 

			---	Asignar Descripcion del Movimiento
			Update K Set DescMovimiento = E.Descripcion, EsMovtoGral = E.EsMovtoGral 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner join Movtos_Inv_Tipos E (NoLock) On ( K.TipoMovto = E.IdTipoMovto_Inv )

			--- Asignar datos de Clave, Laboratorio, Presentacion 
			Update K Set DescProducto = P.Descripcion, IdClaveSSA_Sal = P.IdClaveSSA_Sal, ClaveSSA = P.ClaveSSA, 
			DescripcionClave = P.DescripcionSal, IdLaboratorio = P.IdLaboratorio, Laboratorio = P.Laboratorio, 
			IdPresentacion = P.IdPresentacion, Presentacion = P.Presentacion  
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join vw_Productos P On ( K.IdProducto = P.IdProducto )


		---- Generar el orden del reporte 
			Select Distinct IdClaveSSA_Sal, ClaveSSA, DescripcionClave, identity(int, 1, 1) as OrdenReporte  
			into #tmpOrden 
			From tmpKardex_Antibioticos_Farmacia_Lotes 
			Order by DescripcionClave 

			Update T Set OrdenReporte = O.OrdenReporte 
			From tmpKardex_Antibioticos_Farmacia_Lotes T 
			Inner Join #tmpOrden O On ( T.IdClaveSSA_Sal = O.IdClaveSSA_Sal )
		---- Generar el orden del reporte 



		--- Asignar Domicilio de la Farmacia 
			Update K Set Domicilio = (F.Domicilio + ', ' + F.Municipio + ', ' + F.Estado) 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join vw_Farmacias F On ( K.IdEstado = F.IdEstado and K.IdFarmacia = F.IdFarmacia ) 
		--- Asignar Domicilio de la Farmacia 


----------------------------------------------  VENTAS  ----------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------ 

			Update K Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120),  
				IdMedico = V.IdBeneficiario,  
				Prescribe = 'Dr(a). ' + M.NombreCompleto, Cedula = M.NumCedula  		 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join VentasInformacionAdicional V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioVenta )
			Inner Join vw_Medicos M (NoLock) On ( K.IdEstado = M.IdEstado and K.IdFarmacia = M.IdFarmacia and V.IdMedico = M.IdMedico ) 
			Where TipoMovto = 'SV' 

			Update K Set Beneficiario = B.NombreCompleto 	
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join VentasEnc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioVenta )	
			Inner Join vw_Beneficiarios B (NoLock) 
				On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and 
						V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and K.IdBeneficiario = B.IdBeneficiario ) 		
			Where TipoMovto = 'SV' 

			
------------------------------------------------  COMPRAS  ------------------------------------------------
-----------------------------------------------------------------------------------------------------------
			Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join ComprasEnc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioCompra )			
			Where TipoMovto = 'EC' 

			Update K Set Beneficiario = B.Nombre  	
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join ComprasEnc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioCompra )	
			Inner Join vw_Proveedores B (NoLock) 
				On ( K.IdBeneficiario = B.IdProveedor ) 		
			Where TipoMovto = 'EC'

----------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------  CANCELACION DE COMPRAS   -------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------
			
			Update K Set Referencia = V.Referencia		 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join DevolucionesEnc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioDevolucion )			
			Where TipoMovto = 'CC' 

			Update K Set IdBeneficiario = V.IdProveedor, NumReceta = V.ReferenciaDocto, FechaReceta = convert(varchar(10), V.FechaDocto, 120)  		 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join ComprasEnc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioCompra )			
			Where TipoMovto = 'CC' 

			Update K Set Beneficiario = B.Nombre  	
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join ComprasEnc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioCompra )	
			Inner Join vw_Proveedores B (NoLock) 
				On ( K.IdBeneficiario = B.IdProveedor ) 		
			Where TipoMovto = 'CC'

----------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------  DEVOLUCION DE VENTAS POR DISPENSACION --------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------- 

			Update K Set Referencia = V.Referencia		 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join DevolucionesEnc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Foliador = V.FolioDevolucion )			
			Where TipoMovto = 'ED' 


			Update K Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = convert(varchar(10), V.FechaReceta, 120),  
				IdMedico = V.IdBeneficiario,  
				Prescribe = 'Dr(a). ' + M.NombreCompleto, Cedula = M.NumCedula  		 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join VentasInformacionAdicional V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioVenta )
			Inner Join vw_Medicos M (NoLock) On ( K.IdEstado = M.IdEstado and K.IdFarmacia = M.IdFarmacia and V.IdMedico = M.IdMedico ) 
			Where TipoMovto = 'ED' 

			Update K Set Beneficiario = B.NombreCompleto 	
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join VentasEnc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Referencia = V.FolioVenta )	
			Inner Join vw_Beneficiarios B (NoLock) 
				On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and 
						V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and K.IdBeneficiario = B.IdBeneficiario ) 		
			Where TipoMovto = 'ED' 

----------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------- TRANSFERENCIAS DE SALIDA -------------------------------------------------------------------------
			
			Update K Set IdBeneficiario = T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join vw_TransferenciasEnc T On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio )
			Where TipoMovto = 'TS'  
		
-------------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------- TRANSFERENCIAS DE ENTRADA -------------------------------------------------------------------------
			
			Update K Set IdBeneficiario = T.IdFarmaciaRecibe, Beneficiario = FarmaciaRecibe
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join vw_TransferenciasEnc T On ( K.IdEstado = T.IdEstado and K.IdFarmacia = T.IdFarmacia and K.Folio = T.Folio )
			Where TipoMovto = 'TE'  
		
-----------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------  MOVIMIENTOS GENERALES  ----------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------------------
			Update K Set IdBeneficiario = V.IdPersonalRegistra 	 
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join MovtosInv_Enc V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.Folio = V.FolioMovtoInv )						
			Where EsMovtoGral = 1 

			Update K Set Beneficiario = V.NombreCompleto
			From tmpKardex_Antibioticos_Farmacia_Lotes K 	
			Inner Join vw_Personal V (NoLock) 
				On ( K.IdEstado = V.IdEstado and K.IdFarmacia = V.IdFarmacia and K.IdBeneficiario = V.IdPersonal )						
			Where EsMovtoGral = 1
			

----------------------------------------------------------------------------------------------------------------------------------------------
		--		spp_Kardex_Antibioticos_Farmacia_Lotes 
		
--			Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
--				   FechaSistema, FechaRegistro, Folio, Foliador, TipoMovto, DescMovimiento, Referencia, 
--				   IdProducto, CodigoEAN, DescProducto, IdLaboratorio, Laboratorio, IdPresentacion, Presentacion, 
--				   IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
--				   Beneficiario,NumReceta, FechaReceta, 		   
--				   Prescribe, Cedula, Domicilio, RetieneReceta, 
--				   Entrada, Salida, Existencia,  
--				   Keyx, OrdenReporte, getdate() as FechaImpresion    
--			From tmpKardex_Antibioticos_Farmacia_Lotes			
--			Order By DescripcionClave, DescProducto, FechaRegistro 

		----     spp_Kardex_Antibioticos_Farmacia_Lotes 

 
--		Select * From tmpKardex_Antibioticos_Farmacia_Lotes
--		Order By TipoMovto
 
End 
Go--#SQL  
