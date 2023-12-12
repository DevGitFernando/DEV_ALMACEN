
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Inventario_A_Una_Fecha_Estado' and  xType = 'P' ) 
   Drop Proc spp_Inventario_A_Una_Fecha_Estado 
Go--#SQL 	
	
Create Proc spp_Inventario_A_Una_Fecha_Estado 
( 
	@IdEstado varchar(2) = '21', @FechaRevision varchar(10) = '2011-11-15', @iMostrarResultado int = 1, 
	@sTabla varchar(200) = 'tmpInventario_A_Una_Fecha_Estado'
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
		DM.IdProducto, DM.CodigoEAN, P.ContenidoPaquete * 1.0 as ContenidoPaquete, 
		P.Descripcion, P.ClaveSSA, P.DescripcionClave, 
		max(DM.Keyx) as Keyx, 
		getdate() as FechaUltMovto, 
		0 as Existencia, 
		cast(0 as numeric(14,4)) as Costo_Movto 
	into #tmpInventario 
	from MovtosInv_Det_CodigosEAN DM (NoLock) 
	Inner Join MovtosInv_Enc EM (NoLock) 
		On ( 
		       DM.IdEmpresa = EM.IdEmpresa and DM.IdEstado = EM.IdEstado 
		       and DM.IdFarmacia = EM.IdFarmacia and DM.FolioMovtoInv = EM.FolioMovtoInv 
     	   ) 	
    Inner Join vw_Productos_CodigoEAN P (NoLock) On ( DM.IdProducto = P.IdProducto and DM.CodigoEAN = P.CodigoEAN )  	   
	Where EM.IdEstado = @IdEstado 
		  And Convert(varchar(10), EM.FechaRegistro, 120) <= @FechaRevision 
	Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia,  
			 DM.IdProducto, DM.CodigoEAN, P.ContenidoPaquete, P.Descripcion, P.ClaveSSA, P.DescripcionClave 
			 
	-- Obtener datos de Existencias 			 
	Update T Set Existencia = E.Existencia, Costo_Movto = E.Costo, FechaUltMovto = E.FechaSistema   
	From #tmpInventario T 
	Inner Join MovtosInv_Det_CodigosEAN E On ( E.Keyx = T.Keyx )	

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
		Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
			  IdProducto, CodigoEAN, ContenidoPaquete, Descripcion, ClaveSSA, DescripcionClave, 
			  FechaUltMovto, Existencia as Existencia_A_La_Fecha 
		From  #tmpInventario 
		Order by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN 
	End

End 
Go--#SQL 		

