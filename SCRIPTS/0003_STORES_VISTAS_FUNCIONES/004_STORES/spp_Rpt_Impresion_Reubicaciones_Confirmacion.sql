If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Reubicaciones_Confirmacion' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Reubicaciones_Confirmacion
Go--#SQL  
--38,068
Create Proc	 spp_Rpt_Impresion_Reubicaciones_Confirmacion
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005',
	@FechaInicial varchar(10) = '2014-04-18', @FechaFinal varchar(10) = '2015-10-18', @IdPersonal varchar(4) = ''
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
			Set @sWhere = ' And IdPersonalRegistra = ' + Char(39) + @IdPersonal + Char(39)
		End
	
	 Set @sSql = 'Select R.*, dbo.fg_DiferenciaFechas_Hrs_min(R.FechaRegistro, R.FechaConfirmacion) As DemoraEnConfirmar From vw_Ctrl_Reubicaciones R (NoLock) Inner Join MovtosInv_Enc M (NoLock) ' +
				'On (R.IdEmpresa = M.IdEmpresa And R.IdEstado = M.IdEstado And R.IdFarmacia = M.IdFarmacia And R.Folio_Inv = M.FolioMovtoInv) ' +
				' Where  R.IdEmpresa = ' + @IdEmpresa + ' And R.IdEstado = ' + @IdEstado + ' And R.IdFarmacia = ' + @IdFarmacia +
				'	And Convert(varchar(10), R.FechaRegistro, 120) Between ' + Char(39) + @FechaInicial + Char(39) + ' And ' + Char(39) + @FechaFinal + Char(39)  + 
				@sWhere

	Exec(@sSql)
End	
Go--#SQL