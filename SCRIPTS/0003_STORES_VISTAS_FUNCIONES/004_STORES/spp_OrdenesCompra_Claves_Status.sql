If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_OrdenesCompra_Claves_Status' and xType = 'P' ) 
	Drop Proc spp_OrdenesCompra_Claves_Status
Go--#SQL   

Create Proc spp_OrdenesCompra_Claves_Status
	(@IdEmpresa Varchar(3), @IdEstado Varchar(2), @IdFarmacia Varchar(4), @FolioOrden Varchar(8),
	 @IdPersonal Varchar(4), @IdStatus Varchar(2), @Observaciones Varchar(500), @EsAlmacen Bit = 0)
With Encryption 
As 
Begin 
Set NoCount On

	If (@EsAlmacen = 1)
		Begin
			Select @IdFarmacia = IdFarmacia
			From vw_OrdenesCompras_Claves_Enc (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And EntregarEn = @IdFarmacia And Folio = @FolioOrden
		End
	
	If Not Exists (Select *
				   From COM_OCEN_OrdenesCompra_Claves_Status (NoLock)
				   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden)
		Begin
			Insert Into COM_OCEN_OrdenesCompra_Claves_Status (IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdPersonal, IdStatus, Observaciones)
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @IdPersonal, @IdStatus, @Observaciones
		End
	Else
		Begin
			Update COM_OCEN_OrdenesCompra_Claves_Status Set IdStatus = @IdStatus, Actualizado = 0
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden
		End
		
		Insert Into COM_OCEN_OrdenesCompra_Claves_Status_Historial (IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdPersonal, IdStatus, Observaciones, FechaRegistro)
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @IdPersonal, @IdStatus, @Observaciones, GetDate()

End
Go--#SQL