-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__RegistrarAtencionElegibilidades' and xType = 'P' ) 
   Drop Proc spp_INT_MA__RegistrarAtencionElegibilidades
Go--#SQL 

Create Proc spp_INT_MA__RegistrarAtencionElegibilidades 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '09', 
	@IdFarmacia varchar(4) = '0012', 
	@IdPersonal varchar(4) = '0001', 
	
	@Elegibilidad varchar(50) = 'E006668207', 
	@FolioReceta varchar(50) = '500006108941',   	
	@FolioDispensacion varchar(50) = ''  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sMensaje varchar(max) 
	  
	---------------------- Marcar el UUID como Habilitado 
	Set @sMensaje = 'La Elegibilidad ' + @Elegibilidad + ' previamente atendida.'   


------------------ Provocar el error 
	If Exists ( Select Top 1 * From INT_MA__Elegibilidades E (NoLock) 
				Where Elegibilidad = @Elegibilidad and Elegibilidad_DisponibleSurtido = 0 ) 
	Begin 	
		RaisError (@sMensaje, 16, 1 )
		Return	
	End 
	
	Update E Set Elegibilidad_Surtidos_Aplicados = Elegibilidad_Surtidos_Aplicados + 1  
	From INT_MA__Elegibilidades E (NoLock) 
	Where Elegibilidad = @Elegibilidad 

	Update E Set Elegibilidad_DisponibleSurtido = 0 
	From INT_MA__Elegibilidades E (NoLock)
	Inner Join INT_MA__RecetasElectronicas_001_Encabezado R (NoLock) On (E.Elegibilidad = R.Elegibilidad)
	Where R.EsRecetaManual = 0 And E.Elegibilidad = @Elegibilidad and Elegibilidad_Surtidos_Aplicados >= Elegibilidad_Surtidos 
	

	Update R Set 
		Surtido = 1, FechaSurtido = getdate(), 
		IdEmpresaSurtido = @IdEmpresa, IdEstadoSurtido = @IdEstado, IdFarmaciaSurtido = @IdFarmacia, 
		IdPersonalSurtido = @IdPersonal 
	From INT_MA__RecetasElectronicas_001_Encabezado R (NoLock) 
	Where Elegibilidad = @Elegibilidad and Folio_MA = @FolioReceta 


	--------------- Reserva de Existencia 
	Select R.IdEmpresa, R.IdEstado, R.IdFarmacia, P.CodigoEAN as IdProducto, P.CantidadSolicitada 
	Into #tmpProducto 
	From INT_MA__CFG_FarmaciasClinicas R (NoLock) 
	Inner Join INT_MA__RecetasElectronicas_001_Encabezado E (NoLock) On ( E.Folio_MA = @FolioReceta and E.IdFarmacia = R.Referencia_MA ) 
	Inner Join INT_MA__RecetasElectronicas_002_Productos P On ( E.Folio_MA = P.Folio_MA )
	
--	select * from #tmpProducto 
	
	Update F Set CantidadReservada = ( F.CantidadReservada - R.CantidadSolicitada ) 
	From INT_MA__RecetasElectronicas_003_ReservaExistencia F (NoLock) 
	Inner Join #tmpProducto R (NoLock) 
		On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmacia and F.IdProducto = R.IdProducto ) 		

	Update F Set CantidadReservada = 0 
	From INT_MA__RecetasElectronicas_003_ReservaExistencia F (NoLock) 
	where CantidadReservada < 0 	
	
	

End 
Go--#SQL 

