------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_OP_Ventas' and xType = 'P')
    Drop Proc spp_Rpt_OP_Ventas 
Go--#SQL 

Create Proc spp_Rpt_OP_Ventas 
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia Varchar(4) = '0012',
	@IdCliente Varchar(4) = '', @IdSubCliente Varchar(4) = '', @IdPrograma Varchar(4) = '', @IdSubPrograma Varchar(4) = '',
	@FechaIncial varchar(10) = '2017-06-01', @FechaFinal varchar(10) = '2017-06-25', @IdBeneficiario Varchar(8) = '',
	@TipoReporte smallint = 1, @ConcentradoReporte smallint = 0, @AplicarMascara smallint = 1
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sWhereCliente Varchar(200), 
	@sWhereSubCliente Varchar(200), 
	@sWherePrograma Varchar(200), 
	@sWhereSubPrograma Varchar(200),
	@sWhereBeneficiario Varchar(200),
	@sSql Varchar(Max)
	
	Set @sWhereCliente = ''
	Set @sWhereSubCliente = ''
	Set @sWherePrograma  = ''
	Set @sWhereSubPrograma = ''
	Set @sWhereBeneficiario  = ''
	
	
	/*
		TipoReporte 
		1 = Venta
		2 = Devolucion
		3 = No Surtido	
		4 = Demanda 
	*/

	
	--print (@sSql)
	--Exec  (@sSql)
	
	If (@TipoReporte = 1)
	Begin
		Exec spp_Rpt_OP_Ventas__01_Salidas @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma,
			@FechaIncial, @FechaFinal, @IdBeneficiario,  @ConcentradoReporte, @AplicarMascara
	End
	
	If (@TipoReporte = 2)
	Begin
		Exec spp_Rpt_OP_Ventas__02_Devoluciones @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma,
			@FechaIncial, @FechaFinal, @IdBeneficiario,  @ConcentradoReporte, @AplicarMascara
	End
	
	If (@TipoReporte = 3)
	Begin
		Exec spp_Rpt_OP_Ventas__03_NoSurtido @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma,
			@FechaIncial, @FechaFinal, @IdBeneficiario,  @ConcentradoReporte, @AplicarMascara
	End
	
	If (@TipoReporte = 4)
	Begin
		Exec spp_Rpt_OP_Ventas__04_Demanda @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma,
			@FechaIncial, @FechaFinal, @IdBeneficiario,  @ConcentradoReporte, @AplicarMascara
	End
	



End
Go--#SQL