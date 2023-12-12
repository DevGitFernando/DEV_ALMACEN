If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Reubicaciones' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Reubicaciones
Go--#SQL  
--38,068
Create Proc	 spp_Rpt_Impresion_Reubicaciones
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003',
	@FechaInicial varchar(10) = '2014-04-18', @FechaFinal varchar(10) = '2014-06-18', @IdPersonal varchar(4) = ''
)    
With Encryption 
As 
Begin 
-- Set NoCount On 
Set DateFormat YMD

Declare @sSql Varchar(8000),
		@sWhere varchar(100)
		
		Set @sWhere = ''

	if (@IdPersonal <> '')
		Begin
			Set @sWhere = ' And IdPersonal = ' + Char(39) + @IdPersonal + Char(39)
		End
	

	Set @sSql = 'Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, IdPersonal, E.FechaReg, ' +
		'D.IdProducto, D.CodigoEAN, P.DescripcionCorta ,L.IdSubFarmacia , ' +
		'(L.IdSubFarmacia + ' + Char(39) + ' - ' + Char(39) + ' + L.SubFarmacia) As SubFarmacia, L.ClaveLote, E.Folio As FolioSalida, Referencia, ' +
		'(Cast(U.IdPasillo As Varchar(4)) + ' + Char(39) + ' - ' + Char(39) + ' + Cast(U.IdEstante As Varchar(4)) + ' + Char(39) + ' - ' + Char(39) + 
		' + Cast(U.IdEntrepaño As Varchar(4))) As UbicacionOrigen , ' +
		'U.Cantidad As CantidadOrigen, ' +
		 Char(39) + @FechaInicial + Char(39) + ' As FechaInicial, ' + Char(39)  + @FechaFinal + Char(39) + ' As FechaFinal ' +
	'From vw_MovtosInv_Enc E (NoLock) ' +
	'Inner Join MovtosInv_Det_CodigosEAN D (NoLock) ' +
	'	On (E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia =  D.IdFarmacia And E.Folio = D.FolioMovtoInv) ' +
	'Inner Join CatProductos P (NoLock) On (D.IdProducto = P.Idproducto) ' +
	'Inner Join vw_MovtosInv_Det_CodigosEAN_Lotes L (NoLock) ' +
	'	On (E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia =  L.IdFarmacia And E.Folio = L.Folio ' +
		' And D.CodigoEAN = L.CodigoEAN) ' +
	'Inner Join MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones U (NoLock) ' +
	'	On (E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and E.IdFarmacia =  U.IdFarmacia And E.Folio = U.FolioMovtoInv ' +
	'		And D.CodigoEAN = U.CodigoEAN And L.IdSubFarmacia = U.IdSubFarmacia And L.ClaveLote = U.ClaveLote) ' +	
	'Where E.TipoMovto = ' + Char(39) + 'SPR' + Char(39) + 
		' And E.IdEmpresa = ' + @IdEmpresa + ' And E.IdEstado = ' + @IdEstado + ' And E.IdFarmacia = ' + @IdFarmacia +
	'	And Convert(varchar(10), E.FechaReg, 120) Between ' + Char(39) + @FechaInicial + Char(39) + ' And ' + Char(39) + @FechaFinal + Char(39)  +  @sWhere
	--Print @sSql
	Exec(@sSql)
End	
Go--#SQL