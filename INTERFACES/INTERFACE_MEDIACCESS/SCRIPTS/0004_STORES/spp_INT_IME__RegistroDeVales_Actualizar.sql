If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_IME__RegistroDeVales_Actualizar' and xType = 'P' ) 
   Drop Proc spp_INT_IME__RegistroDeVales_Actualizar
Go--#SQL 

Create Proc spp_INT_IME__RegistroDeVales_Actualizar
( 
	@IdSocioComercial varchar(8), @IdSucursal varchar(8), @Folio_Vale varchar(30)

) 
With Encryption 
As 
Begin 
Set NoCount On 	
	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta
	Into #Folios
	From  INT_IME__RegistroDeVales_003_Surtidos (NoLock)
	Where IdSocioComercial = @IdSocioComercial And IdSucursal = @IdSucursal And Folio_Vale = @Folio_Vale
	
	select P.ClaveSSA, Cast(Sum(CantidadVendida) As int) As Cant
	Into #Cant
	From VentasDet D (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) ON (D.CodigoEAN = P.CodigoEAN)
	Inner Join #Folios T (NoLock) On (D.IdEmpresa = T.IdEmpresa And D.IdEstado = T.IdEstado And D.IdFarmacia = T.IdFarmacia And D.FolioVenta = T.FolioVenta)
	Group By P.ClaveSSA
		
		
	Update C Set C.CantidadSurtida = T.Cant
	From  INT_IME__RegistroDeVales_002_Claves C
	Inner Join #Cant T On (C.ClaveSSA = T.ClaveSSA)
	Where IdSocioComercial = @IdSocioComercial And IdSucursal = @IdSucursal And Folio_Vale = @Folio_Vale
	
	
	--Select * From  INT_IME__RegistroDeVales_002_Claves (NoLock) Where IdSocioComercial = @IdSocioComercial And IdSucursal = @IdSucursal And Folio_Vale = @Folio_Vale


End 
Go--#SQL 