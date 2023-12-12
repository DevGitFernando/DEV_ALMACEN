--------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_InformacionAdicional' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_InformacionAdicional
Go--#SQL
  
Create Proc spp_FACT_Remisiones_InformacionAdicional 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001', 
	@GUID varchar(100) = '', 
	@Info_01 varchar(100) = '', @Info_02 varchar(100) = '', @Info_03 varchar(100) = '', @Info_04 varchar(100) = '', @Info_05 varchar(100) = '' 
)
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare 
	@sMensaje varchar(1000), 
	@sStatus varchar(1) 


	Set @sMensaje = ''
	Set @sStatus = 'A' 



	Update I Set 
		Info_01 = @Info_01, Info_02 = @Info_02, Info_03 = @Info_03, Info_04 = @Info_04, Info_05 = @Info_05 
	From FACT_Remisiones_InformacionAdicional I (nolock) 
	Inner Join FACT_Remisiones R (NoLock) 
		On ( I.IdEmpresa = R.IdEmpresa and I.IdEstado = R.IdEstado and I.IdFarmaciaGenera = R.IdFarmaciaGenera  and I.FolioRemision = R.FolioRemision ) 
	Inner Join FACT_Remisiones___GUID G (NoLock) 
		On ( G.IdEmpresa = R.IdEmpresa and G.IdEstado = R.IdEstado and G.IdFarmaciaGenera = R.IdFarmaciaGenera  and G.GUID = R.GUID ) 
	Where G.GUID = @GUID 


End 
Go--#SQL

