

------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ProductosExistenEnEstadoFarmacia' and xType = 'P' ) 
   Drop Proc spp_ProductosExistenEnEstadoFarmacia
Go--#SQL 

Create Proc spp_ProductosExistenEnEstadoFarmacia 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0019', 
	@CodigoEAN varchar(15) = '2870', @EsPublicoGeneral int = 0  
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 

Declare 
	@sCodigoEAN varchar(20), 
	@iEsSectorSalud int 
	

	Set @iEsSectorSalud = 1 
	Set @sCodigoEAN = right('0000000000000000000' + @CodigoEAN, 13) 


	If @EsPublicoGeneral = 1 
	   Set @iEsSectorSalud = 0 

	Select Distinct @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		CR.IdProducto, CR.CodigoEAN, CR.CodigoEAN_Interno, P.Descripcion, 
		0 as Existencia  
	Into #tmpRevision 	
--	From CatProductos_Estado PE (NoLock) 
	From CatProductos_CodigosRelacionados CR (NoLock)
	-- Inner Join CatProductos_CodigosRelacionados CR (NoLock) 
	--	On ( CR.Status = 'A' and PE.IdProducto = CR.IdProducto and ( CR.CodigoEAN_Interno = @sCodigoEAN or CR.CodigoEAN = @CodigoEAN )  ) 
	Inner Join CatProductos P (Nolock) On ( P.IdProducto = CR.IdProducto and P.Status = 'A' and P.EsSectorSalud in ( 0, @iEsSectorSalud )  ) 
	Where CR.Status = 'A' and P.IdProducto = CR.IdProducto and ( CR.CodigoEAN_Interno = @sCodigoEAN or CR.CodigoEAN = @CodigoEAN )




--		On ( CR.Status = 'A' and PE.IdProducto = CR.IdProducto and ( CR.CodigoEAN_Interno = @sCodigoEAN or CR.CodigoEAN = @CodigoEAN )  ) 


	
	--		select top 1 * from CatProductos where EsSectorSalud = 1 
	
	Update E Set Existencia = 
	IsNull( 
	(
		Select sum(Existencia - ( ExistenciaEnTransito + ExistenciaSurtidos ) ) From FarmaciaProductos_CodigoEAN F (NoLock) 
		Where E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia 
			  and E.IdProducto = F.IdProducto and E.CodigoEAN = F.CodigoEAN 
	), 0)  
	From #tmpRevision E 
	
	
------------------	Salida Final 	
	------Select Existencia, ExistenciaEnTransito, (Existencia - ExistenciaEnTransito) as X 
	------From FarmaciaProductos_CodigoEAN F (NoLock) 
	------Where CodigoEAN = @CodigoEAN 
	
	------Select E.*, 
	------	F.Existencia, F.ExistenciaEnTransito
	------From #tmpRevision E 	
	------Left Join FarmaciaProductos_CodigoEAN F (NoLock) 
	------	On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia 
	------		  and E.IdProducto = F.IdProducto and E.CodigoEAN = F.CodigoEAN )
	
	
	Select 'Codigo EAN' = CodigoEAN, Existencia, 'Id Producto' = IdProducto, 'Nombre comercial' = Descripcion
	From #tmpRevision 
	
--		spp_ProductosExistenEnEstadoFarmacia  	
	
End 
Go--#SQL 

