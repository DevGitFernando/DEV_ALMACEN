If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_IME__RegistroDeVales_Validar' and xType = 'P' ) 
   Drop Proc spp_INT_IME__RegistroDeVales_Validar
Go--#SQL 

Create Proc spp_INT_IME__RegistroDeVales_Validar
( 
	@IdSocioComercial varchar(8) = '00000002', @IdSucursal varchar(8) = '00000001', @Folio_Vale varchar(30) = '00000099'

) 
With Encryption 
As 
Begin 
Set NoCount On


	-------------------------------------Cantidades
	Select ClaveSSA As 'Clave SSA', CantidadSolicitada As 'Cantidad Solicitada', CantidadSurtida As 'Cantidad Surtida'
	From  INT_IME__RegistroDeVales_002_Claves (NoLock)
	Where IdSocioComercial = @IdSocioComercial And IdSucursal = @IdSucursal And Folio_Vale = @Folio_Vale And CantidadSolicitada < CantidadSurtida
	-----------------------------------------Cantidades

	-----------------------------------------Claves
	Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta
	Into #Folios
	From  INT_IME__RegistroDeVales_003_Surtidos (NoLock)
	Where IdSocioComercial = @IdSocioComercial And IdSucursal = @IdSucursal And Folio_Vale = @Folio_Vale
	

	Select P.ClaveSSA, Cast(Sum(CantidadVendida) As int) As Cant
	Into #Cant
	From VentasDet D (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) ON (D.CodigoEAN = P.CodigoEAN)
	Inner Join #Folios T (NoLock) On (D.IdEmpresa = T.IdEmpresa And D.IdEstado = T.IdEstado And D.IdFarmacia = T.IdFarmacia And D.FolioVenta = T.FolioVenta)
	Group By P.ClaveSSA
	
	Select ClaveSSA As 'Clave SSA', Cant As 'Cantidad Surtida'
	From  #Cant T
	Where Not Exists (
					  Select *
					  From INT_IME__RegistroDeVales_002_Claves C
					  Where C.ClaveSSA = T.ClaveSSA And IdSocioComercial = @IdSocioComercial And IdSucursal = @IdSucursal And Folio_Vale = @Folio_Vale
					  )
	-----------------------------------------Claves

End 
Go--#SQL 