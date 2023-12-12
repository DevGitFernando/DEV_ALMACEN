
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_Rpt_ListaDeRemisiones' and xType = 'P' )
    Drop Proc spp_INT_MA__FACT_Rpt_ListaDeRemisiones
Go--#SQL 
	
	--Exec spp_INT_MA__FACT_Rpt_ListaDeRemisiones @IdEmpresa = '002',  @IdEstado = '09', @IdFarmacia = '0001', 
	--@EsFacturable = 3, @IdTipoInsumo = 3
  
Create Proc spp_INT_MA__FACT_Rpt_ListaDeRemisiones
(
	@IdEmpresa Varchar(3) = '002',  @IdEstado Varchar(2) = '09', @IdFarmacia Varchar(4) = '0001',
	@EsFacturable Int = 1, @IdTipoInsumo Int = 3, @FechaInicial Varchar(10) = '2015-01-01', @FechaFinal Varchar(10) = '2016-12-01'
) 
With Encryption		
As 
Begin 
Set NoCount On 
	
	declare @WhereAux Varchar(300),
			@sSql Varchar(8000)
	Set @WhereAux = ''
			
	If(@EsFacturable = 1)
		Begin
			Set @WhereAux = ' And F.EsFacturable = 1'
		End
		
	If(@EsFacturable = 0) 
		Begin
			Set @WhereAux = @WhereAux + ' And F.EsFacturable = 0'
		End
		
	If(@IdTipoInsumo = 1)
		Begin
			Set @WhereAux = @WhereAux + ' And F.TipoInsumo = ' + CHAR(39) + '02' + CHAR(39)
		End
		
	If(@IdTipoInsumo = 2)
		Begin
			Set @WhereAux = @WhereAux + ' And F.TipoInsumo = ' + CHAR(39) + '01' + CHAR(39)
		End
	
	Set @sSql = 'Select ' + CHAR(13) +
				'	F.FolioRemision, Convert(varchar(10), F.FechaRemision, 120) As FechaRemision, F.FacturableDescripcion, ' + CHAR(13) + 
				'F.IdCliente, F.Cliente, F.IdSubCliente, ' + CHAR(13) +
				'	F.SubCliente, F.Total As Importe, F.TipoDeRemisionDesc, F.StatusDesc, F.TipoDeInsumoDesc ' + CHAR(13) +
				'From vw_FACT_Remisiones F (NoLock) ' + CHAR(13) +
				'Where F.IdEmpresa = ' + CHAR(39) + @IdEmpresa  + CHAR(39) + ' And F.IdEstado =  ' + CHAR(39) + @IdEstado  + CHAR(39) +
					 ' And F.IdFarmacia = '  + CHAR(39) + @IdFarmacia  + CHAR(39) + CHAR(13) +
					 ' And Convert(varchar(10), FechaRemision, 120) Between ' + CHAR(39) + @FechaInicial + Char(39) +
					 ' and ' + Char(39) + @FechaFinal + CHAR(39) + @WhereAux
					 
	Exec (@ssql)
	
End 
Go--#SQL