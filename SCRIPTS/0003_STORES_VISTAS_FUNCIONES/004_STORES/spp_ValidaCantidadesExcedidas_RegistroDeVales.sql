If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_ValidaCantidadesExcedidas_RegistroDeVales' and xType = 'P')
    Drop Proc spp_ValidaCantidadesExcedidas_RegistroDeVales
Go--#SQL
  
Create Proc spp_ValidaCantidadesExcedidas_RegistroDeVales 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @Folio varchar(8), @Tabla varchar(100)
)
With Encryption 
As
Begin
Set NoCount On

Declare @iResultado tinyint, @sSql varchar(7500), @sTablaRegistro varchar(100), @Registros Int

	Set @sSql = ''	
	Set @iResultado = 0
	

	Select
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdProducto, T.CodigoEAN, P.DescripcionSal As Descripcion,
		P.ClaveSSA, P.DescripcionSal , Cast(T.CantidadRecibida as Int) As Cantidad
	Into #tmpCaptura	
	From ValesDet T (Nolock)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where 1 = 0
	
	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, P.Descripcion, Cast(Sum(T.CantidadRecibida) as int) As Cantidad
	Into #tmpRegistroVales
	From ValesDet T (Nolock)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )	
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia 
	And T.Folio In ( Select Folio From ValesEnc (Nolock)
								Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								And FolioVale = @Folio )
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, P.Descripcion
	
	Set @sSql = ' Insert #tmpCaptura (IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, Descripcion, Cantidad ) ' +
				' Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, P.DescripcionSal, T.IdProducto, T.CodigoEAN, P.Descripcion, Sum(T.Cantidad) As Cantidad ' +				
				' From ' + @Tabla + '  T (Nolock) ' +
				' Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN )' +	
				' Where T.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' And T.IdEstado = ' + char(39) + @IdEstado + char(39) + ' ' +  
				' And T.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' ' +
				' Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, P.DescripcionSal, T.IdProducto, T.CodigoEAN, P.Descripcion '
	Exec(@sSql)	

	
----	Insert Into #tmpRegistroOC (IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProducto, CodigoEAN, Descripcion, Cantidad )
----	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.IdProducto, T.CodigoEAN, T.Descripcion, T.Cantidad
----	From #tmpCaptura T (Nolock)


	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA, Sum(T.Cantidad) As Cantidad
	Into #tmpOriginal
	From Vales_EmisionDet T (Nolock)
	Inner Join vw_ClavesSSA_Sales P (Nolock) On ( T.IdClaveSSA_Sal = P.IdClaveSSA_Sal )
	Where T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.FolioVale = @Folio
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, P.ClaveSSA


	Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, Sum(T.Cantidad) As Cantidad
	Into #tmpValeTotal
	From #tmpRegistroVales T (Nolock) 
	Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA 
	
	--Select * From #tmpCaptura
	--Select * From #tmpOriginal 
	--Select * From #tmpValeTotal 


	Select T.ClaveSSA, CA.IdProducto, CA.CodigoEAN, 'Descripción' =  CA.Descripcion,
	'Cantidad Vale' = T.Cantidad, 'Ingresos Previos' = IsNull(C.Cantidad, 0),
	'Ingreso Actual' = CA.Cantidad, 'Cantidad Excedente' = ABS((IsNull(C.Cantidad, 0) + CA.Cantidad ) - T.Cantidad )
	Into #tmpValidarCantidades
	From #tmpCaptura CA (Nolock)
	Inner Join #tmpOriginal T  (Nolock)
		On ( T.ClaveSSA = CA.ClaveSSA )
	Left Join #tmpValeTotal C (Nolock)
		On ( CA.ClaveSSA = C.ClaveSSA )	
	Where (IsNull(C.Cantidad, 0) + CA.Cantidad ) > T.Cantidad
	 
	Select @Registros =  IsNull(COUNT(*), 0) From #tmpValidarCantidades (Nolock)

	If (@Registros > 0)
		Begin
			Select * From #tmpValidarCantidades (Nolock)
		End
	Else
		Begin
			Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.DescripcionSal, Sum(T.Cantidad) As Cantidad
			Into #tmpCaptura_Resumen
			From #tmpCaptura T
			Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.DescripcionSal		
		
		
			Select CA.ClaveSSA, CA.DescripcionSal, 'Cantidad Vale' = T.Cantidad, 'Ingresos Previos' = IsNull(C.Cantidad, 0),
				'Ingreso Actual' = CA.Cantidad, 'Cantidad Excedente' = ABS((IsNull(C.Cantidad, 0) + CA.Cantidad ) - T.Cantidad )
			From #tmpCaptura_Resumen CA
			Inner Join #tmpOriginal T  (Nolock) On ( T.ClaveSSA = CA.ClaveSSA )
			Left Join #tmpValeTotal C (Nolock) On ( CA.ClaveSSA = C.ClaveSSA )	
			Where (IsNull(C.Cantidad, 0) + CA.Cantidad ) > T.Cantidad
		End
	

End
Go--#SQL	


