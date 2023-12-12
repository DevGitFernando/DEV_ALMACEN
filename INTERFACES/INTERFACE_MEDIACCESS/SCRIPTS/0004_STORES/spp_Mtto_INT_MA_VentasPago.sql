If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_INT_MA_VentasPago' and xType = 'P' ) 
   Drop Proc spp_Mtto_INT_MA_VentasPago
Go--#SQL

Create Proc spp_Mtto_INT_MA_VentasPago
(
	 @IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	 @Folio varchar(8) = '', @IdFormasDePago varchar(2) = '', 
	 @Importe numeric(14, 4) = 0, @PagoCon numeric(14, 4) = 0, @Cambio numeric(14, 4) = 0, @Referencia Varchar(100) = ''  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sStatus varchar(1), @iActualizado smallint 
		
		Set @sStatus = 'A'
		Set @iActualizado = 0

	--- Iniciar el proceso de guardado
     Insert Into INT_MA_VentasPago ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdFormasDePago, Importe, PagoCon, Cambio, Referencia, Status, Actualizado ) 
     Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @IdFormasDePago, @Importe, @PagoCon, @Cambio, @Referencia,  @sStatus, @iActualizado
     	
End 
Go--#SQL
