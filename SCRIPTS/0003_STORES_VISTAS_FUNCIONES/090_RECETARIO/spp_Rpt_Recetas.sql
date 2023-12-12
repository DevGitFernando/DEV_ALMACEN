


If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Recetas' and xType = 'P' ) 
   Drop Proc spp_Rpt_Recetas 
Go--#SQL


--- Exec spp_Rpt_Recetas '25', '0010', '00000001'

Create Proc spp_Rpt_Recetas ( @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0012', @Folio varchar(8) = '00000002'
	  ) 
With Encryption 
As 
Begin 
Set NoCount On 


Declare @sSql varchar(8000),   
		@sWhere varchar(8000), @sMostrar varchar(50), 
		@dFecha datetime 	 

Declare @sEncPrinRpt varchar(300), 
		@sEncSecRpt varchar(300)

	
	Select V.IdEstado, V.Estado, V.IdFarmacia, V.Farmacia, F.IdMunicipio, F.Municipio, F.Colonia, F.Domicilio, 
		V.Folio, V.IdBeneficiario, V.NombreBeneficiario, V.Sexo, V.Edad, V.FechaNacimiento, V.FolioReferencia, 
		V.IdMedico, V.NombreMedico, V.NumCedula, V.FechaRegistro, 
		C.IdClaveSSA, S.Descripcion,  C.Cantidad, D.IdDiagnostico, CI.ClaveDiagnostico, CI.Descripcion As DescDiagnostico
	Into #tmpRecetasDetClaves 
	From vw_Rec_Recetas V ( Nolock )
	Inner Join Rec_Recetas_ClavesSSA C ( Nolock ) On ( V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.Folio = C.IdReceta )
	Inner Join vw_Farmacias F (Nolock) On ( V.IdEstado = F.IdEstado And V.IdFarmacia = F.IdFarmacia )
	Inner Join CatClavesSSA_Sales S (Nolock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal )
	Inner Join Rec_Recetas_Diagnosticos D ( Nolock ) On ( V.IdEstado = D.IdEstado And V.IdFarmacia = D.IdFarmacia And V.Folio = D.IdReceta )
	Inner Join CatCIE10_Diagnosticos CI (NoLock) On ( D.IdDiagnostico = CI.IdDiagnostico )
	Where V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia And V.Folio = @Folio
	
	
	Select * From #tmpRecetasDetClaves (Nolock)

	
	
End 
Go--#SQL

