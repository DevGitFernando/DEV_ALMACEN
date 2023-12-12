If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__Reactivar_Elegibilidad' and xType = 'P' ) 
   Drop Proc spp_INT_MA__Reactivar_Elegibilidad 
Go--#SQL

Create Proc spp_INT_MA__Reactivar_Elegibilidad
(
	@IdEmpresa Varchar(3) = '002', @IdEstado Varchar(2) = '09', @IdFarmacia Varchar(4) = '0013',
	@NumReceta Varchar(30) = '500006053067', @Elegibilidad Varchar(30) = 'E006669403',
	@IdPersonal Varchar(4), @Esadministrador bit = 0
) 
With Encryption 
As 
Begin 
Set NoCount On
	Declare @Cant Numeric(14,4),
			@bRegresa bit,
			@sMensaje Varchar(500)
			
	Set @bRegresa = 0
	
	Set @sMensaje = 'Se reactivo el folio con éxito.'
	
	if (@Esadministrador <> 1)	
		Begin
			Select @bRegresa = (Case When COUNT(*) > 0 then 0 Else 1 End) From INT_MA__ADT_Elegibilidades Where Elegibilidad = @Elegibilidad
			If (@bRegresa = 0)
				Begin
					Set @sMensaje = 'Receta con modificación previa.'
				End
		End
	Else
		Begin
			Set @bRegresa = 1
		End

	If(@bRegresa = 1)
	Begin
		Select @bRegresa = (Case When COUNT(*) > 0 then 1 Else 0 End)
		From INT_MA__RecetasElectronicas_001_Encabezado D (NoLock)
		Where D.IdEmpresaSurtido = @IdEmpresa And D.IdEstadoSurtido = @IdEstado And D.IdFarmaciaSurtido = @IdFarmacia And D.Folio_MA = @NumReceta

		Set @bRegresa = ISNULL(@bRegresa, 0)
		If(@bRegresa = 0)
			Begin
				Set @sMensaje = 'La Elegibilidad pertenese a otra farmacia, no es posible reactivarla.'
			End
	End
	
	If(@bRegresa = 1)
		Begin
			Select *
			Into #Folios
			From VentasInformacionAdicional A
			Where NumReceta = @NumReceta

			Select @Cant = IsNull(SUM(CantidadVendida), 0)
			From VentasDet D (NoLock)
			Inner Join #Folios F (NoLock)
				On (D.IdEmpresa = F.IdEmpresa And D.IdEstado = F.IdEstado And D.IdFarmacia = F.IdFarmacia And D.FolioVenta = F.FolioVenta)

			If (@Cant = 0)
				Begin
					Set @bRegresa = 1
				End
			Else
				Begin
					Set @bRegresa = 0
					Set @sMensaje = 'Se encontro una venta relacionada con este número de receta, no es posible reactivarla.'
				End
		End
		
		
	if (@bRegresa = 1)
		Begin
			--Select * From INT_MA__ADT_Elegibilidades
			Insert Into INT_MA__ADT_Elegibilidades
				(Folio, FechaRegistro, IdEmpresa, IdEstado, IdFarmacia, IdPersonal, Elegibilidad,
				Empresa_y_RazonSocial, Elegibilidad_DisponibleSurtido, NombreEmpleado, ReferenciaEmpleado,
				NombreMedico, ReferenciaMedico, Copago, CopagoEn, IdPersonalModifica)
			Select Folio, FechaRegistro, IdEmpresa, IdEstado, IdFarmacia, IdPersonal, Elegibilidad,
				Empresa_y_RazonSocial, Elegibilidad_DisponibleSurtido, NombreEmpleado, ReferenciaEmpleado,
				NombreMedico, ReferenciaMedico, Copago, CopagoEn, @IdPersonal
			From INT_MA__Elegibilidades
			Where Elegibilidad = @Elegibilidad
			
			UpDate INT_MA__Elegibilidades Set Elegibilidad_DisponibleSurtido = 1 Where Elegibilidad = @Elegibilidad
			Update INT_MA__RecetasElectronicas_001_Encabezado Set Surtido = 0  Where Elegibilidad = @Elegibilidad
		End
			
	Select @bRegresa As Valor, @sMensaje As Mensaje

		
End 
Go--#SQL 
