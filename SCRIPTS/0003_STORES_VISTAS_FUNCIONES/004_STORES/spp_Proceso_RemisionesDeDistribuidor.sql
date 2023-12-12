

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_RemisionesDeDistribuidor' and xType = 'P') 
    Drop Proc spp_Proceso_RemisionesDeDistribuidor
Go--#SQL 
  
--  Exec spp_Proceso_RemisionesDeDistribuidor '0001', '21', '0182', '0001', '0001'
  
Create Proc spp_Proceso_RemisionesDeDistribuidor 
(   
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182', 
	@IdDistribuidor varchar(4) = '0001', @IdPersonal varchar(4) = '0001'
) 
With Encryption 
As 
Begin 
	Declare @FechaDocumento varchar(10), @FolioRemision varchar(8), @sMensaje varchar(1000),
			@iRenglonMax int, @iRenglonCont int, @Observaciones varchar(100),
			@sStatus varchar(1), @iActualizado smallint, @FolioCargaMasiva int, @iCont int

	Set NoCount On 
	Set DateFormat YMD
	Set @sMensaje = '' 
	Set @FechaDocumento = ''
	Set @iRenglonCont = 0
	Set @iRenglonMax = 0
	Set @Observaciones = 'CARGA MASIVA'
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @FolioCargaMasiva = 0
	Set @iCont = 0

	Select Distinct IdEmpresa, IdEstado, IdFarmacia, IdDistribuidor, CodigoCliente, Referencia, FechaDocumento, EsConsignacion
	Into #tmpRemisionContador
	From Remisiones_CargaMasiva R (Nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor
	Order By FechaDocumento

	Set @iCont = ( Select Count(*) From #tmpRemisionContador (Nolock) )
	
	Select Distinct R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.IdDistribuidor, R.CodigoCliente, R.Referencia, R.FechaDocumento, R.EsConsignacion
	Into #tmpRemisionDis
	From Remisiones_CargaMasiva R (Nolock)
	Where Not Exists (Select * From RemisionesDistEnc E (Nolock)
		Where R.IdEmpresa = E.IdEmpresa and R.IdEstado = E.IdEstado and R.IdFarmacia = E.IdFarmacia and R.IdDistribuidor = E.IdDistribuidor
		And R.CodigoCliente = E.CodigoCliente and R.Referencia = E.ReferenciaPedido and R.FechaDocumento = E.FechaDocumento 
		and R.EsConsignacion = E.EsConsignacion )
	and R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia and R.IdDistribuidor = @IdDistribuidor
	Order By R.FechaDocumento

	If Exists ( Select * From #tmpRemisionDis (Nolock) )
		Begin
			Select ROW_NUMBER() OVER (ORDER BY IdEmpresa) As Renglon, *
			Into #tmpRemisionDisEnc
			From #tmpRemisionDis (Nolock)	
			
			Set @iRenglonCont = 1
			Set @iRenglonMax = ( Select Max(Renglon) From #tmpRemisionDisEnc (Nolock) )	

			Select @FolioCargaMasiva =	(Max(FolioCargaMasiva) + 1) From RemisionesDistEnc (Nolock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor			
				
			Set @FolioCargaMasiva = IsNull(@FolioCargaMasiva, 1)
			
			Declare #cRemisiones Cursor For Select @iRenglonCont 
			Open #cRemisiones Fetch #cRemisiones Into @iRenglonCont
				While (@iRenglonCont <= @iRenglonMax ) 
					Begin
						If @iRenglonCont <= @iRenglonMax
							Begin
								--- Se Obtiene el folio de la remision automaticamente
								Select @FolioRemision = Cast( (Max(FolioRemision) + 1) As varchar) From RemisionesDistEnc (Nolock) 
								Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor 

								Set @FolioRemision = IsNull(@FolioRemision, '1')
								Set @FolioRemision = right(replicate('0', 8) + @FolioRemision, 8)
								
								--- Se inserta el folio de la remision generado automaticamente
								If Not Exists ( Select * From RemisionesDistEnc (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
												and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor and FolioRemision = @FolioRemision ) 
									Begin 
										Insert Into RemisionesDistEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision, IdDistribuidor, ReferenciaPedido, 
																		CodigoCliente, FechaDocumento, Observaciones, IdPersonal, FechaRegistro, EsConsignacion,
																		FolioCargaMasiva, Status, Actualizado) 
										Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioRemision, @IdDistribuidor, Referencia, CodigoCliente, FechaDocumento,
										@Observaciones, @IdPersonal, GetDate(), EsConsignacion, @FolioCargaMasiva, @sStatus, @iActualizado
										From #tmpRemisionDisEnc (Nolock) Where Renglon = @iRenglonCont

											
										--- Se Inserta los detalles de las claves del folio de la remision
										Insert Into RemisionesDistDet ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision,	IdClaveSSA, Cant_Recibida, Cant_Devuelta,
																		CantidadRecibida, Status, Actualizado )
										Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioRemision, C.IdClaveSSA_Sal, 
										Sum(R.Cantidad) As Cant_Recibida, 0, Sum(R.Cantidad) As CantidadRecibida, @sStatus, @iActualizado 
										From RemisionesDistEnc E (Nolock)
										Inner Join Remisiones_CargaMasiva R (Nolock)
											On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmacia
												and E.IdDistribuidor = R.IdDistribuidor and E.CodigoCliente = R.CodigoCliente
												and E.ReferenciaPedido = R.Referencia and E.FechaDocumento = R.FechaDocumento 
												and E.EsConsignacion = R.EsConsignacion )
										Inner Join CatClavesSSA_Sales C (Nolock) On ( R.ClaveSSA = C.ClaveSSA )		
										Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and E.IdDistribuidor = @IdDistribuidor
										and E.FolioRemision = @FolioRemision
		 					   			Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioRemision, C.IdClaveSSA_Sal		
									End								

								Set @iRenglonCont = ( @iRenglonCont + 1 )					
							End
						Fetch #cRemisiones Into @iRenglonCont
					End		
			Close #cRemisiones
			DeAllocate #cRemisiones
			
			Set @sMensaje = 'Folios de Remisiones generados Satisfactoriamente..'			
		End
	Else
		Begin
			Set @sMensaje = 'Los Datos del Archivo ya han sido cargados anteriormente..'			
		End

	If @iRenglonCont > 0 and @iRenglonCont < @iCont
		Begin
			Set @sMensaje = 'Folios de Remisiones generados Satisfactoriamente, algunos datos ya han sido cargados anteriormente '
		End 
	
	Select @FolioCargaMasiva As FolioCargaMasiva, @sMensaje As Mensaje

End
Go--#SQL 