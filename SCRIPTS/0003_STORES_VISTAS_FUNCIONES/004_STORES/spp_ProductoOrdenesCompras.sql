----------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_ProductoOrdenesCompras' And xType = 'P' )
	Drop Proc spp_ProductoOrdenesCompras
Go--#SQL

Create Procedure dbo.spp_ProductoOrdenesCompras 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '3', @FolioOrden varchar(8) = '552', 
	@IdCodigo varchar(30) = '219', @CodigoEAN varchar(30) = '219' 
)
With Encryption 
As 
Begin
Declare 
    @iLenCodEAN int  

	Set @iLenCodEAN = 15 
	Set @IdEmpresa = right('000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('000000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000000000' + @IdFarmacia, 4) 
	Set @FolioOrden = right('000000000000000' + @FolioOrden, 8) 


	--------------------------------- CLAVES VALIDAS PARA LA ORDEN DE COMPRA 
	Select @IdEstado as IdEstado, ClaveSSA, 0 as Relacionada  
	Into #tmpClaves 
	From vw_Productos_CodigoEAN X (NoLock)  
	Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det Y (NoLock) On ( X.IdProducto = Y.IdProducto And X.CodigoEAN = Y.CodigoEAN )  
	Where Y.IdEmpresa = @IdEmpresa And Y.IdEstado = @IdEstado And Y.FolioOrden = @FolioOrden 


	Insert Into #tmpClaves 
	Select @IdEstado as IdEstado, ClaveSSA_Relacionada, 1 as Relacionada
	From vw_Relacion_ClavesSSA_Claves C (NoLock) 
	Inner Join #tmpClaves R On ( C.IdEstado = R.IdEstado and C.ClaveSSA = R.ClaveSSA and C.Status = 'A' )  


	------------ CONSULTA DE INFORMACION 
	Select 
		P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, P.IdProducto, P.CodigoEAN, P.Descripcion,  P.TasaIva, 
		dbo.fg_ObtenerCostoPromedio(@IdEmpresa, @IdEstado, @IdFarmacia, P.IdProducto) as CostoPromedio  
	From vw_Productos_CodigoEAN P (Nolock)  
	Inner Join #tmpClaves C On ( C.ClaveSSA = P.ClaveSSA ) 
	Where 
		( 
			right(replicate('0', @iLenCodEAN) + P.CodigoEAN_Interno, @iLenCodEAN) = right(replicate('0', @iLenCodEAN) + @IdCodigo, @iLenCodEAN) 
			Or 
			right(replicate('0', @iLenCodEAN) + P.CodigoEAN, @iLenCodEAN) = right(replicate('0', @iLenCodEAN) + @CodigoEAN, @iLenCodEAN )  
		) And P.Status = 'A'



End
Go--#SQL