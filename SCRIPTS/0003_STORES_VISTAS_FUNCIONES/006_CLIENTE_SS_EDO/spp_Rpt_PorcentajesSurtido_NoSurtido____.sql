If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_NoCauses_PorcentajesSurtido_NoSurtido' and xType = 'P' ) 
   Drop Proc spp_Rpt_NoCauses_PorcentajesSurtido_NoSurtido 
Go--#SQL  

--	Exec spp_Rpt_NoCauses_PorcentajesSurtido_NoSurtido '001', '21', '', '2011-01-01', '2012-03-31'  

Create Proc	 spp_Rpt_NoCauses_PorcentajesSurtido_NoSurtido 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001',
	@FechaInicial varchar(10) = '2012-03-01', @FechaFinal varchar(10) = '2012-03-10' 
)    
With Encryption 
As 
Begin 
-- Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10)  

	Set @sNA = ' N/A ' 
	Set @sNA = '' 
	
	Select IdEstado, Estado, IdFarmacia, Farmacia, IdJurisdiccion, Jurisdiccion 
	Into #tmpFarmacias 
	From vw_Farmacias 
	Where IdEstado =  @IdEstado


	Select C.IdEstado, C.IdFarmacia, C.FolioVenta, C.IdClaveSSA, S.ClaveSSA, S.TipoDeClave, S.TipoDeClaveDescripcion, 
		C.CantidadRequerida As CantidadNoSurtida, 0 As EsCause
	Into #tmpNoSurtido 
	From VentasEnc V (Nolock)
	Inner Join VentasEstadisticaClavesDispensadas C (Nolock)
		On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
	Inner Join vw_ClavesSSA_Sales S (Nolock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado 
		And Convert(varchar(10),FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal And C.EsCapturada = 1	
	Order By C.FolioVenta		

--		select top 1 * from vw_

--	select count(*) from #tmpNoSurtido 

--	Marcar las claves que son causes
	Update T Set EsCause = 1  
	From #tmpNoSurtido T (Nolock)
	Inner Join vw_CB_CuadroBasico_Farmacias C (Nolock)
		On ( T.IdEstado = C.IdEstado And T.IdFarmacia = C.IdFarmacia And T.ClaveSSA = C.ClaveSSA )

--	Borrar las claves que son causes
	Delete From #tmpNoSurtido Where EsCause = 1

	Select 
		V.IdEstado, V.IdFarmacia, V.FolioVenta, P.IdClaveSSA_Sal As IdClaveSSA, P.ClaveSSA, 
		S.TipoDeClave, S.TipoDeClaveDescripcion, 
		Sum(V.CantidadVendida) As CantidadSurtida, 0 As EsCause
	Into #tmpSurtido 
	From VentasDet V (Nolock)
	Inner Join #tmpNoSurtido T (Nolock) 
		On ( V.IdEstado = T.IdEstado And V.IdFarmacia = T.IdFarmacia And V.FolioVenta = T.FolioVenta )
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( V.IdProducto = P.IdProducto and V.CodigoEAN = P.CodigoEAN ) 
	Inner Join vw_ClavesSSA_Sales S (Nolock) On ( P.ClaveSSA = S.ClaveSSA ) 	
	Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado 
	Group By V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, P.IdClaveSSA_Sal, P.ClaveSSA, S.TipoDeClave, S.TipoDeClaveDescripcion  
	Order By V.FolioVenta

	--	Marcar las claves que son causes
	Update T Set EsCause = 1  
	From #tmpSurtido T (Nolock)
	Inner Join vw_CB_CuadroBasico_Farmacias C (Nolock)
		On ( T.IdEstado = C.IdEstado And T.IdFarmacia = C.IdFarmacia And T.ClaveSSA = C.ClaveSSA )	

--	Borrar las claves que son causes
	Delete From #tmpSurtido Where EsCause = 1 

	--- Borrar la tabla base de los Datos
	If Exists ( select * from sysobjects (nolock) Where Name = 'tmpConcentradoPorcentaje' and xType = 'U' ) 
	   Drop table tmpConcentradoPorcentaje

	Select N.IdEstado, F.Estado, N.IdFarmacia, F.Farmacia, F.IdJurisdiccion, F.Jurisdiccion,  
		N.FolioVenta, 
		S.TipoDeClave, S.TipoDeClaveDescripcion,   
		Sum(S.CantidadSurtida) As CantidadSurtida, Sum(N.CantidadNoSurtida) As CantidadNoSurtida, 
		(Sum(S.CantidadSurtida) + Sum(N.CantidadNoSurtida)) As Total, 
		Cast(0 as Numeric(14, 4) ) As PorcentajeSurtido, Cast(0 as Numeric(14, 4) ) As PorcentajeNoSurtido
	Into tmpConcentradoPorcentaje
	From #tmpSurtido S (Nolock) 
	Inner Join #tmpFarmacias F (NoLock) On ( S.IdEstado = F.IdEstado and S.IdFarmacia = F.IdFarmacia ) 
	Inner Join #tmpNoSurtido N (Nolock) On ( S.IdEstado = N.IdEstado And S.IdFarmacia = N.IdFarmacia And S.FolioVenta = N.FolioVenta  )
	Group By N.IdEstado, F.Estado, N.IdFarmacia, F.Farmacia, F.IdJurisdiccion, F.Jurisdiccion, N.FolioVenta, 
		S.TipoDeClave, S.TipoDeClaveDescripcion    
--	Order By N.FolioVenta 

--	Se calculan los porcentajes de lo surtido y No Surtido
	Update P Set 
		PorcentajeSurtido = Case When CantidadSurtida = 0 Then 0 Else ((CantidadSurtida/Total) * 100 ) End , 
		PorcentajeNoSurtido = Case When CantidadNoSurtida = 0 Then 0 Else ((CantidadNoSurtida/Total) * 100 ) End 
	From tmpConcentradoPorcentaje P (Nolock) 
 
 
End 
Go--#SQL  
