----------------------------------------------------------------------------------------------------------------------------     
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_INV_ConteoRapido_CodigoEAN' and xType = 'P' ) 
   Drop Proc spp_Rpt_INV_ConteoRapido_CodigoEAN 
Go--#SQL 

Create Proc spp_Rpt_INV_ConteoRapido_CodigoEAN
(
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0113', @Folio varchar(8) = '00000001'
) 
With Encryption 
As 
Begin 
Set NoCount On 
	Declare @sMensaje varchar(1000)

	Set @sMensaje = ''
		
	----  SE MUESTRA SOLO EL LISTADO DE LOS PRODUCTOS QUE TUVIERON DIFERENCIAS ---------
	
	Select *,  
	Case When (Conteos + 1) = 2 Then 'SEGUNDO CONTEO FISICO DE INVENTARIO' 
			When (Conteos + 1) = 3 Then 'TERCER CONTEO FISICO DE INVENTARIO'
			Else 'TERCER CONTEO FISICO DE INVENTARIO' End As TituloReporte,
	0 as Cantidad
	From vw_INV_ConteoRapido_CodigoEAN_Det (Nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	and Folio = @Folio and ExistenciaLogica <> ExistenciaFinal 
	Order By DescripcionSal
		
		
End 
Go--#SQL 

