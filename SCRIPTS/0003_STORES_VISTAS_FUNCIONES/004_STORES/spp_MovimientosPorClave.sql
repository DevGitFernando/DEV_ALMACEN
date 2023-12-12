If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_MovimientosPorClave' And xType = 'P')
    Drop Proc spp_MovimientosPorClave
Go--#SQL	

Create Proc spp_MovimientosPorClave ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '20', @IdFarmacia Varchar(4) = '0130', 
	@ClaveSSA varchar(20) = '060.456.0334', @FechaInicial varchar(10) = '2014-05-01', @FechaFin varchar(10) = '2014-05-31', @IdPersonal Varchar(4) = '0010') 
With Encryption
As 
Begin 
Set NoCount On 
Set DateFormat YMD

	Declare @sSql Varchar(1000),
			@sWhere Varchar(300),
			@sWhereAux varchar(600)
			
	Set @sWhere = ''
	
	Set @sWhereAux = ''
	
	
	Set @sWhereAux = ' Where K.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and k.IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + 
				' And k.IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' And Convert(varchar(10),k.FechaSistema,120) Between ' + char(13) + 
				+ Char(39) + @FechaInicial + Char(39) + ' And ' + Char(39) + @FechaFin + Char(39)
	
	
	
	If(@ClaveSSA <> '')
		Set @sWhere = ' And k.ClaveSSA =  '  + char(39) + @ClaveSSA + char(39)
	
	If (@IdPersonal <> '')
	    Set @sWhere = @sWhere + ' And K.IdPersonalRegistra =  ' + char(39) + @IdPersonal + char(39)
	    
	 --Print @sWhere

	Select IdEstado, IdFarmacia, Efecto_Movto, Efecto, TipoMovto, DescMovimiento As Movimiento, 0 As Folios, 0 As Piezas
	Into #Concentrado
	From vw_MovtosInv_Tipos_Farmacia M (NoLock)
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

-------------------------------------------------------------------------------------------------------------
	Select Top 0 *   
	Into #vw_Kardex_ProductoCodigoEAN 
	From vw_Kardex_ProductoCodigoEAN 

	Set @sWhereAux = @sWhereAux + @sWhere

	--print (@sWhereAux)

	Set @sSql = 'Insert Into #vw_Kardex_ProductoCodigoEAN ' + 
	' Select * ' + 
	' From vw_Kardex_ProductoCodigoEAN K (NoLock) ' + char(13) + 
	'  ' + @sWhereAux  
	Exec(@sSql) 
-----------------------------------------------------------------------------------------------------------------

	set @sSql = 'Update C  ' + Char(13) +
	 'Set Folios = IsNull((Select Count(Distinct(Folio))As Folios ' + char(13) +
	 '			From #vw_Kardex_ProductoCodigoEAN k (NoLock) ' + char(13) +
	 '			Where k.IdEstado = c.IdEstado And k.IdFarmacia = c.IdFarmacia And k.TipoMovto = C.TipoMovto And ' + char(13) +
	 '			 Convert(varchar(10),FechaSistema,120) Between ' + Char(39) + @FechaInicial + Char(39) + ' And ' + Char(39) + @FechaFin + Char(39) +
	              @sWhere + '),0) ' + char(13) +
	'From #Concentrado C (NoLock) '
	
	
	--Print(@sSql)
	Exec(@sSql)

	set @sSql = 'Update C ' + Char(13) +
	'Set Piezas = IsNull((Select Sum(Salida) As Piezas ' + Char(13) +
	'		   From #vw_Kardex_ProductoCodigoEAN K (NoLock) ' + Char(13) +
	'		   Where K.IdEstado = C.IdEstado And K.IdFarmacia = C.IdFarmacia And k.TipoMovto = C.TipoMovto And ' + Char(13) +
	'		    Convert(Varchar(10),FechaSistema,120) Between ' + Char(39) +  @FechaInicial + Char(39) +  ' And ' + Char(39) +  @FechaFin + Char(39) +
				 @sWhere + '),0) ' + Char(13) +
	'From #Concentrado C (NoLock) ' + Char(13) +
	'Where C.Efecto_Movto = ' + Char(39) + 'S' + Char(39)

	--Print(@sSql)
	Exec(@sSql)
	
	set @sSql = 'Update C ' + Char(13) +
	'Set Piezas = IsNull((Select Sum(Entrada) As Piezas ' + Char(13) +
   	'		   From #vw_Kardex_ProductoCodigoEAN K (NoLock) ' + Char(13) +
	'		   Where K.IdEstado = C.IdEstado And K.IdFarmacia = C.IdFarmacia And k.TipoMovto = C.TipoMovto And ' + Char(13) +
	'		    Convert(varchar(10),FechaSistema,120) Between ' + Char(39) +  @FechaInicial + Char(39) +  ' And ' + Char(39) +  @FechaFin + Char(39) +
				 @sWhere + '),0) ' + Char(13) +
	'From #Concentrado C (NoLock) ' + Char(13) +
	'Where C.Efecto_Movto = ' + Char(39) + 'E' + Char(39)

	--Print(@sSql)
	Exec(@sSql)
	
	Select Efecto, TipoMovto, Movimiento As Descripcion, Folios, Piezas
	From #Concentrado C (NoLock) 
	Order By Efecto_Movto, Movimiento
		
End 
Go--#SQL 
	