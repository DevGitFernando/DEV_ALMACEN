

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Inv_Operacion_Maquila' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Inv_Operacion_Maquila 
Go--#SQL 

Create Proc spp_Rpt_Impresion_Inv_Operacion_Maquila
(
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0113', @Folio varchar(8) = '00000001'
) 
With Encryption 
As 
Begin 
Set NoCount On 
	Declare @sMensaje varchar(1000)

	Set @sMensaje = ''
		
	----  se crea la tabla para la impresion ---------
	
	Select *, space(7500) as Descripcion_ND, cast(0 as numeric(14, 4)) as Precio, cast(0 as numeric(14, 4)) as PrecioServicio
	Into #tmp_Impresion_INV_OperacionMaquila
	From vw_INV_OperacionMaquilaDet (Nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
	
	
	Update T Set T.Descripcion_ND = IsNull(P.Descripcion, T.Descripcion)
	From #tmp_Impresion_INV_OperacionMaquila T (Nolock)
	Inner Join INT_ND_Productos P (NoLock) On (P.CodigoEAN_ND = T.CodigoEAN_Externo)
		
	Update T Set T.Precio = IsNull(C.PrecioVenta, 0), T.PrecioServicio = IsNull(C.PrecioServicio, 0)
	From #tmp_Impresion_INV_OperacionMaquila T (Nolock)
	Inner Join CFG_ClavesSSA_OperacionMaquila_Chiapas C (NoLock) On (C.ClaveSSA_Mascara = T.ClaveSSA)	
	
	
	Select * From #tmp_Impresion_INV_OperacionMaquila (Nolock)
	Order By DescripcionSal
		
End 
Go--#SQL 

