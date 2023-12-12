
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_RutasDistribucion' and xType = 'P' )
    Drop Proc spp_Rpt_RutasDistribucion 
Go--#SQL

Create Proc spp_Rpt_RutasDistribucion 
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', @Folio varchar(8) = '00000001' 
)    
With Encryption 
As 
Begin 
Set DateFormat YMD
Set NoCount On


	Select Top 0 IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, space(1000) as DireccionFarmacia,
	Folio, FolioPedido, IdVehiculo, Vehiculo, IdPersonal, Chofer,
		IdPersonalCaptura, PersonalCaptura,
		CAST('' As Varchar(2)) As IdTipo, Cast('' As varchar(100)) As Tipo, FolioTransferenciaVenta, FechaRegistro, FechaPedido, 
		Fecha, space(8) as IdReferencia, Cast('' As varchar(100)) As Referencia, Bultos, Piezas
	Into #Ruta
	From vw_RutasDistribucionDetTrans (NoLock)


	Insert into #Ruta
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, '' as DireccionFarmacia, Folio, FolioPedido, IdVehiculo, Vehiculo, IdPersonal, Chofer,
	IdPersonalCaptura, PersonalCaptura,
		02 As IdTipo,'TRANSFERENCIAS' As Tipo, FolioTransferenciaVenta, FechaRegistro, FechaPedido, Fecha, IdFarmaciaRecibe, FarmaciaRecibe, Bultos, Piezas
	From vw_RutasDistribucionDetTrans (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio
	

	Insert into #Ruta
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, Domicilio as DireccionFarmacia, Folio, FolioPedido, IdVehiculo, Vehiculo, IdPersonal, Chofer,
	IdPersonalCaptura, PersonalCaptura,
		   01 As IdTipo, 'VENTAS' As Tipo, FolioTransferenciaVenta, FechaRegistro, FechaPedido, Fecha, IdBeneficiario, Beneficiario, Bultos, Piezas
	From vw_RutasDistribucionDetVentas (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio
	
	Update T Set T.DireccionFarmacia = (F.Domicilio + ', C.P.: ' + F.CodigoPostal + ', ' + F.Colonia + ', ' + F.Municipio + ', ' + F.Estado )
	From #Ruta T
	Inner Join vw_Farmacias F On ( F.IdEstado = T.IdEstado AND F.IdFarmacia = T.IdReferencia )
	Where IdTipo = 02
		


------------------------------	SALIDA FINAL 
	Select *
	From #Ruta R (NoLock)

End
Go--#SQL