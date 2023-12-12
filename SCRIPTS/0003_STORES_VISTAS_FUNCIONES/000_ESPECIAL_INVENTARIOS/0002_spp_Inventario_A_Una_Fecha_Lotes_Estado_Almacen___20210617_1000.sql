------------------------------------ --------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Inventario_A_Una_Fecha_Lotes_Estado_Almacen' and  xType = 'P' ) 
   Drop Proc spp_Inventario_A_Una_Fecha_Lotes_Estado_Almacen 
Go--#SQL 	
	
/* 

	Exec spp_Inventario_A_Una_Fecha_Lotes_Estado_Almacen 
		@IdEstado = '11', @IdFarmacia = '0094', 
		@FechaRevision = '2015-08-31', @iMostrarResultado = 1, 
		@sTabla = 'tmpInventario_A_Una_Fecha_Lotes'
	
	
*/ 
	 
	----		Select * From tmpInventario_A_Una_Fecha_Lotes Where ClaveSSA = '010.000.4158.00' and ClaveLote = '4F112A'
	
Create Proc spp_Inventario_A_Una_Fecha_Lotes_Estado_Almacen 
( 
	@IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '0004', 
	@FechaRevision varchar(10) = '2021-05-31', @iMostrarResultado int = 1, 
	@sTabla varchar(200) = 'tmpInventario_A_Una_Fecha_Lotes', 
	@Excluir_Posiciones int = 0    
) 	
As 
Begin 
Set DateFormat YMD 
Declare @sSql varchar(8000)

	Set @sSql = '' 

	-- Se obtienen los datos principales.
	Select DM.IdEmpresa, space(100) as Empresa, 
		DM.IdEstado, space(100) as Estado, 
		DM.IdFarmacia, space(100) as Farmacia, 
		DM.IdSubFarmacia, 
		space(20) as FolioMovto, 
		DM.IdProducto, DM.CodigoEAN, 
		DM.ClaveLote, 
		cast('' as varchar(20)) as Caducidad, 
		DM.SKU, 
		P.Descripcion, P.ClaveSSA, P.DescripcionClave, 
		--max(DM.Keyx) as Keyx,
		max(EM.FechaRegistro) as FechaUltimoMovto, 
		getdate() as FechaUltMovto, 
		DM.IdPasillo, DM.IdEstante, DM.IdEntrepaño, 
		0 as Existencia, 
		cast(0 as numeric(14,4)) as Costo_Movto, 
		cast(0 as numeric(14,4)) as Costo, 

		cast(P.TasaIva as numeric(14,4)) as TasaIVA, 
		cast(0 as numeric(14,4)) as SubTotal, 
		cast(0 as numeric(14,4)) as IVA, 
		cast(0 as numeric(14,4)) as Importe 
				 
	into #tmpInventario 
	from MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones DM (NoLock) 
	Inner Join MovtosInv_Enc EM (NoLock) 
		On ( 
		       DM.IdEmpresa = EM.IdEmpresa and DM.IdEstado = EM.IdEstado 
		       and DM.IdFarmacia = EM.IdFarmacia and DM.FolioMovtoInv = EM.FolioMovtoInv 
     	   ) 	
    Inner Join vw_Productos_CodigoEAN P (NoLock) On ( DM.IdProducto = P.IdProducto and DM.CodigoEAN = P.CodigoEAN )  	   
	Where EM.IdEstado = @IdEstado and EM.IdFarmacia = @IdFarmacia 
		  And Convert(varchar(10), EM.FechaRegistro, 120) <= @FechaRevision 
		  -- And DM.IdProducto = 5 
	Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, DM.IdSubFarmacia,  
			 DM.IdProducto, DM.CodigoEAN, DM.ClaveLote, DM.SKU, P.Descripcion, P.ClaveSSA, P.DescripcionClave,  
			 DM.IdPasillo, DM.IdEstante, DM.IdEntrepaño, P.TasaIva  
			 
	-- Obtener datos de Existencias 			 
	--Update T Set 
	--	FolioMovto = E.FolioMovtoInv, 
	--	Existencia = E.Existencia --, FechaUltMovto = E.FechaSistema   
	--From #tmpInventario T 
	--Inner Join MovtosInv_Det_CodigosEAN_Lotes E On ( E.Keyx = T.Keyx )	
	
	---------- Obtener datos de Ultimo movimiento  
	Update T Set 
		FolioMovto = E.FolioMovtoInv  
	From #tmpInventario T 
	Inner Join MovtosInv_Enc E 
		On ( T.IdEmpresa = E.IdEmpresa and T.IdEstado = E.IdEstado and T.IdFarmacia = E.IdFarmacia and E.FechaRegistro = T.FechaUltimoMovto )	
	---------- Obtener datos de Ultimo movimiento  
	


	---------- Obtener datos de Caducidad  
	Update T Set Caducidad = convert(varchar(7), E.FechaCaducidad, 120) 
	From #tmpInventario T 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes E 
		On ( T.IdEmpresa = E.IdEmpresa and T.IdEstado = E.IdEstado and T.IdFarmacia = E.IdFarmacia and T.IdSubFarmacia = E.IdSubFarmacia 
			and T.IdProducto = E.IdProducto and T.CodigoEAN = E.CodigoEAN and T.ClaveLote = E.ClaveLote and T.SKU = E.SKU 
			 )			
	---------- Obtener datos de Caducidad  


	---------- Obtener datos de Existencias 
	Update T Set Costo_Movto = E.Costo, Costo = E.Costo 
	From #tmpInventario T 
	Inner Join MovtosInv_Det_CodigosEAN E (NoLock) 
		On ( T.IdEmpresa = E.IdEmpresa and T.IdEstado = E.IdEstado and T.IdFarmacia = E.IdFarmacia and E.FolioMovtoInv = T.FolioMovto )	

	Update T Set Existencia = E.Existencia   
	From #tmpInventario T 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones E 
		On ( T.IdEmpresa = E.IdEmpresa and T.IdEstado = E.IdEstado and T.IdFarmacia = E.IdFarmacia and T.FolioMovto = E.FolioMovtoInv 
			and T.IdProducto = E.IdProducto and T.CodigoEAN = E.CodigoEAN and T.ClaveLote = E.ClaveLote and T.SKU = E.SKU 
			and T.IdPasillo = E.IdPasillo and T.IdEstante = E.IdEstante and T.IdEntrepaño = E.IdEntrepaño 
			 )	
	---------- Obtener datos de Existencias 


	---------------- Calcular importes 
	Update T Set SubTotal = round(Existencia * Costo, 2, 1) 
	From #tmpInventario T 

	Update T Set IVA = round(SubTotal * ( TasaIVA / 100.00 ), 2, 1) 
	From #tmpInventario T 
	Where TasaIVA > 0 

	Update T Set Importe = round(SubTotal + IVA, 2, 1) 
	From #tmpInventario T 
	---------------- Calcular importes 




	-------- Se obtiene el nombre de la Empresa
	Update T Set Empresa = F.Nombre 
	From #tmpInventario T 
	Inner Join CatEmpresas F (NoLock) On ( T.IdEmpresa = F.IdEmpresa ) 
	
	-- Se obtiene el nombre de la Farmacia
	Update T Set Estado = F.Estado, Farmacia = F.Farmacia 
	From #tmpInventario T 
	Inner Join vw_Farmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia ) 
	



	-------- Se inserta el resultado en una tabla temporal en caso de solicitarlo
	If @sTabla <> ''
	  Begin
		Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + Char(39) + @sTabla + Char(39) + ' and  xType = ' + Char(39) + 'U' + Char(39) + ' ) ' +
					'Drop Table ' + @sTabla + ' ' 
		Exec(@sSql)

		Set @sSql = 'Select * Into ' + @sTabla + ' From #tmpInventario(NoLock) Order by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN '
		Exec(@sSql)
	  End


	-------- Se muestra el resultado en caso de solicitarlo.
	If @iMostrarResultado = 1 
		Begin 
			Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, 
				  FolioMovto, IdProducto, CodigoEAN, ClaveLote, Caducidad, SKU, 
				  IdPasillo, IdEstante, IdEntrepaño,  Descripcion, ClaveSSA, DescripcionClave, 
				  FechaUltimoMovto as FechaUltMovto, 
				  Costo,  
				  Existencia as Existencia_A_La_Fecha, 
				  SubTotal, IVA, Importe 
			From  #tmpInventario 
			Order by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN 
		End 
	Else 
		Begin 
			Select 'El resultado se almacenó en la tabla [ ' + @sTabla + ' ] ' 
		End 

End 
Go--#SQL 		

