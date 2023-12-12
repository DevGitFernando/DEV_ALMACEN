If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_AIEF_RegistroLog' and xType = 'P' ) 
   Drop Proc spp_AIEF_RegistroLog 
Go--#SQL 

Create Proc spp_AIEF_RegistroLog 
(
	@IdAccesoExterno varchar(4) = '0001', @NumOpcion int = 0, @Sentencia varchar(500) = '', @MAC varchar(20) = '', @Host varchar(100) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD     
	
	Insert Into AIEF_ADT_Accesos ( IdAccesoExterno, NumOpcion, Sentencia, MAC, NombreHost ) 
	Select @IdAccesoExterno, @NumOpcion, @Sentencia, @MAC, @Host  
	
End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_AIEF_ADT_Existencia_ClavesLog' and xType = 'P' ) 
   Drop Proc spp_AIEF_ADT_Existencia_ClavesLog 
Go--#SQL 

Create Proc spp_AIEF_ADT_Existencia_ClavesLog 
(
	@ClaveSSA varchar(500) = '', @MAC varchar(20) = '', @Host varchar(100) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD     
	
	Insert Into AIEF_ADT_Existencia_Claves ( FechaRegistro, ClaveSSA, MAC, NombreHost ) 
	Select getdate() as FechaRegistro, @ClaveSSA as ClaveSSA, @MAC as MAC, @Host as Host 
	
End 
Go--#SQL 


