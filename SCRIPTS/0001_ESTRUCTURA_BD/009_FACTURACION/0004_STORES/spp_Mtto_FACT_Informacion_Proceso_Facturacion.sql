------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_Informacion_Proceso_Facturacion'  and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_Informacion_Proceso_Facturacion  
Go--#SQL 

Create Proc spp_Mtto_FACT_Informacion_Proceso_Facturacion 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '0034', @FolioCierre int = 0, @Identificador varchar(100) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	Insert Into FACT_Informacion_Proceso_Facturacion ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre, HostName, Identificador, FechaRegistro )
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCierre, Host_Name() as HostName, @Identificador, getdate() as FechaRegistro

End 
Go--#SQL


