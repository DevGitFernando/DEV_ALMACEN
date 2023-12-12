


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Vales_VerificaCantidades_ServDomicilio' and xType = 'P')
Drop Proc spp_Vales_VerificaCantidades_ServDomicilio
Go--#SQL

--		Exec spp_Vales_VerificaCantidades_ServDomicilio '001', '21', '2132', '00000001'
  
Create Proc spp_Vales_VerificaCantidades_ServDomicilio 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2132', @Folio varchar(8) = '00000001'
)
With Encryption 
As
Begin
Set NoCount On

Declare @iResultado tinyint, @sSql varchar(7500) 

	Set @sSql = ''	
	Set @iResultado = 0	

	
	Select S.ClaveSSA, S.DescripcionSal, Sum(S.CantidadLote) as CantServDomicilio, Sum(E.Cantidad) as CantEmision,
	( Sum(S.CantidadLote) - Sum(E.Cantidad)) AS Diferencia
	Into #tmpValidaCantidades
	From vw_Impresion_Vales_Registrados_ServicioDom S (Nolock)
	Left Join vw_Vales_EmisionDet E (Nolock)
		On (E.IdEmpresa = S.IdEmpresa AND E.IdEstado = S.IdEstado AND E.IdFarmacia = S.IdFarmacia 
		AND E.Folio = S.FolioVale and E.ClaveSSA = S.ClaveSSA )
	Where S.IdEmpresa = @IdEmpresa and S.IdEstado = @IdEstado and S.IdFarmacia = @IdFarmacia and S.Folio = @Folio
	Group By S.ClaveSSA, S.DescripcionSal
	Having Sum(S.CantidadLote) >  Sum(E.Cantidad)


	Select * From #tmpValidaCantidades (Nolock)
		

End
Go--#SQL	
