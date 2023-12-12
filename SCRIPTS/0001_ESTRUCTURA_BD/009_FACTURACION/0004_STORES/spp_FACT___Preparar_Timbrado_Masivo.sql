

------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT___Preparar_Timbrado_Masivo' and xType = 'P' ) 
   Drop Proc spp_FACT___Preparar_Timbrado_Masivo 
Go--#SQL 

Create Proc spp_FACT___Preparar_Timbrado_Masivo 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 


	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__01__Encabezado' and xType = 'U' ) Drop Table  FACT_CFDI_TM__01__Encabezado 
	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__02__DetallesClaves' and xType = 'U' ) Drop Table  FACT_CFDI_TM__02__DetallesClaves 
	If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__03__FolioElectronico' and xType = 'U' ) Drop Table  FACT_CFDI_TM__03__FolioElectronico 


End 
Go--#SQL 


