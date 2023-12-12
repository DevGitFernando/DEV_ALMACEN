If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Inventario_A_Una_Fecha_ControladoAntibiotico' and  xType = 'P' ) 
   Drop Proc spp_Inventario_A_Una_Fecha_ControladoAntibiotico 
Go--#SQL
	
Create Proc spp_Inventario_A_Una_Fecha_ControladoAntibiotico
( 
	@IdEmpresa Varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0011', 
	@FechaRevision varchar(10) = '2015-07-27', @TipoProducto int = 2	
) 	
As 
Begin 
Set DateFormat YMD 
Declare 
	@sSql varchar(8000), 
	@EsControlado Bit, 
	@EsAntibiotico Bit, 
	@Encabezado varchar(2000)  
		
		

	Set @sSql = '' 
	Set @EsControlado = 0 
	Set @EsAntibiotico = 0 
	Set @Encabezado = '' 
	
	If (@TipoProducto = 1) 
		Begin
			Set @EsControlado = 1 
			Set @Encabezado = 'REPORTE DE EXISTENCIA DE CONTROLADO AL' 			
		End
		
	If (@TipoProducto = 2)
		Begin
			Set @EsAntibiotico = 1 
			Set @Encabezado = 'REPORTE DE EXISTENCIA DE ANTIBIOTICOS AL' 
		End

	-- Se obtienen los datos principales.
	Select DM.IdEmpresa, space(100) as Empresa, 
		DM.IdEstado, space(100) as Estado, 
		DM.IdFarmacia, space(100) as Farmacia, 
		DM.IdSubFarmacia, 
		space(20) as FolioMovto, 
		DM.IdProducto, DM.CodigoEAN, getDate() As FechaCaducidad,
		DM.ClaveLote, 
		P.Descripcion, P.ClaveSSA, P.DescripcionClave, 
		P.Laboratorio, 
		max(DM.Keyx) as Keyx, 
		--getdate() as FechaUltMovto, 
		0 as Existencia, 
		0 as Orden  
	into #tmpInventario 
	from MovtosInv_Det_CodigosEAN_Lotes DM (NoLock) 
	Inner Join MovtosInv_Enc EM (NoLock) 
		On ( 
		       DM.IdEmpresa = EM.IdEmpresa and DM.IdEstado = EM.IdEstado 
		       and DM.IdFarmacia = EM.IdFarmacia and DM.FolioMovtoInv = EM.FolioMovtoInv 
     	   ) 	
    Inner Join vw_Productos_CodigoEAN P (NoLock) On ( DM.IdProducto = P.IdProducto and DM.CodigoEAN = P.CodigoEAN )  	   
	Where EM.IdEmpresa = @IdEmpresa And EM.IdEstado = @IdEstado and EM.IdFarmacia = @IdFarmacia 
		  And Convert(varchar(10), EM.FechaRegistro, 120) <= @FechaRevision  And
		  EsControlado = @EsControlado And EsAntibiotico = @EsAntibiotico 
		  -- And DM.IdProducto = 5 
	Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, DM.IdSubFarmacia,  
			 DM.IdProducto, DM.CodigoEAN, P.Laboratorio, DM.ClaveLote, P.Descripcion, P.ClaveSSA, P.DescripcionClave 
	Order by IdEmpresa, IdEstado, IdFarmacia, DescripcionClave, ClaveSSA, IdProducto, CodigoEAN 
			 
			 
			 
	------ Obtener datos de Existencias 			 
	Update T Set 
		FolioMovto = E.FolioMovtoInv, 
		Existencia = E.Existencia --, FechaUltMovto = E.FechaSistema   
	From #tmpInventario T 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes E On ( E.Keyx = T.Keyx )	


	------ Se obtiene el nombre de la Empresa
	Update T Set Empresa = F.Nombre 
	From #tmpInventario T 
	Inner Join CatEmpresas F (NoLock) On ( T.IdEmpresa = F.IdEmpresa ) 
	
	------ Se obtiene el nombre de la Farmacia 
	Update T Set Estado = F.Estado, Farmacia = F.Farmacia 
	From #tmpInventario T 
	Inner Join vw_Farmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )
	
	
	------ Se obtinen la caducidad 
	Update T Set T.FechaCaducidad = F.FechaCaducidad
	From #tmpInventario T
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
		On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia And
			 T.IdSubFarmacia = F.IdSubFarmacia And T.IdProducto = F.IdProducto And T.CodigoEAN = F.CodigoEAN And T.ClaveLote = F.ClaveLote)



--------------- Ordenar las Claves 
	Select Distinct ClaveSSA, DescripcionClave, identity(int, 1, 1) as Orden  
	into #tmpClaves 
	From #tmpInventario 
	Order by DescripcionClave  

	Update I Set Orden = O.Orden 
	From #tmpInventario I 
	Inner Join #tmpClaves O On ( I.ClaveSSA = O.ClaveSSA ) 

------------------------------	 SALIDA 
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia,  
		  IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, 
		  Convert(Varchar(10), FechaCaducidad, 120) As FechaCaducidad, Descripcion, ClaveSSA, DescripcionClave, --FechaUltMovto,
		  Existencia as Existencia_A_La_Fecha, Laboratorio, @Encabezado as Encabezado, Orden    
	From  #tmpInventario 
	Order by Orden --- IdEmpresa, IdEstado, IdFarmacia, DescripcionClave, ClaveSSA, IdProducto, CodigoEAN 


End 


Go--#SQL 


