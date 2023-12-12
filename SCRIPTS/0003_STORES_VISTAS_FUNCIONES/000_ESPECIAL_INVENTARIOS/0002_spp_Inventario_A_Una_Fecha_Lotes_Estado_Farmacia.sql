
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Inventario_A_Una_Fecha_Lotes_Estado_Farmacia' and  xType = 'P' ) 
   Drop Proc spp_Inventario_A_Una_Fecha_Lotes_Estado_Farmacia 
Go--#SQL 	
	
	---			Exec spp_Inventario_A_Una_Fecha_Lotes_Estado_Farmacia '11', '0094', '2015-08-31', 0, 'tmpInventario_A_Una_Fecha_Lotes'
	 
	----		Select * From tmpInventario_A_Una_Fecha_Lotes Where ClaveSSA = '010.000.4158.00' and ClaveLote = '4F112A'
	
Create Proc spp_Inventario_A_Una_Fecha_Lotes_Estado_Farmacia 
( 
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0094', 
	@FechaRevision varchar(10) = '2015-08-31', @iMostrarResultado int = 0, 
	@sTabla varchar(200) = 'RPT_SESEQ__Existencias__20211011__20210930'   
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
		P.Descripcion, P.ClaveSSA, P.DescripcionClave, 
		--max(DM.Keyx) as Keyx,
		max(EM.FechaRegistro) as FechaUltimoMovto, 
		getdate() as FechaUltMovto, 
		0 as Existencia 
	into #tmpInventario 
	from MovtosInv_Det_CodigosEAN_Lotes DM (NoLock) 
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
			 DM.IdProducto, DM.CodigoEAN, DM.ClaveLote, P.Descripcion, P.ClaveSSA, P.DescripcionClave 
			 
			 
	-- Obtener datos de Existencias 			 
	--Update T Set 
	--	FolioMovto = E.FolioMovtoInv, 
	--	Existencia = E.Existencia --, FechaUltMovto = E.FechaSistema   
	--From #tmpInventario T 
	--Inner Join MovtosInv_Det_CodigosEAN_Lotes E On ( E.Keyx = T.Keyx )	
	
	Update T Set 
		FolioMovto = E.FolioMovtoInv  
	From #tmpInventario T 
	Inner Join MovtosInv_Enc E 
		On ( T.IdEmpresa = E.IdEmpresa and T.IdEstado = E.IdEstado and T.IdFarmacia = E.IdFarmacia and E.FechaRegistro = T.FechaUltimoMovto )	
	
	Update T Set Existencia = E.Existencia   
	From #tmpInventario T 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes E 
		On ( T.IdEmpresa = E.IdEmpresa and T.IdEstado = E.IdEstado and T.IdFarmacia = E.IdFarmacia and T.FolioMovto = E.FolioMovtoInv 
			and T.IdProducto = E.IdProducto and T.CodigoEAN = E.CodigoEAN and T.ClaveLote = E.ClaveLote )	


	-- Se obtiene el nombre de la Empresa
	Update T Set Empresa = F.Nombre 
	From #tmpInventario T 
	Inner Join CatEmpresas F (NoLock) On ( T.IdEmpresa = F.IdEmpresa ) 
	
	-- Se obtiene el nombre de la Farmacia
	Update T Set Estado = F.Estado, Farmacia = F.Farmacia 
	From #tmpInventario T 
	Inner Join vw_Farmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia ) 
	

	-- Se inserta el resultado en una tabla temporal en caso de solicitarlo
	If @sTabla <> ''
	  Begin
		Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + Char(39) + @sTabla + Char(39) + ' and  xType = ' + Char(39) + 'U' + Char(39) + ' ) ' +
					'Drop Table ' + @sTabla + ' ' 
		Exec(@sSql)

		Set @sSql = 'Select * Into ' + @sTabla + ' From #tmpInventario(NoLock) Order by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN '
		Exec(@sSql)
	  End

	-- Se muestra el resultado en caso de solicitarlo.
	If @iMostrarResultado = 1 
    Begin 
		Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, 
			  FolioMovto, IdProducto, CodigoEAN, ClaveLote, Descripcion, ClaveSSA, DescripcionClave, 
			  FechaUltMovto, Existencia as Existencia_A_La_Fecha 
		From  #tmpInventario 
		Order by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN 
	End 
	Else 
	Begin 
		Select 'El resultado se almacenó en la tabla [ ' + @sTabla + ' ] ' 
	End 

End 
Go--#SQL 		

