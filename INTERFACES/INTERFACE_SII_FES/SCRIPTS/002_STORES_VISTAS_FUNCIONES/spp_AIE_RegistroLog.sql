If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_AIE_RegistroLog' and xType = 'P' ) 
   Drop Proc spp_AIE_RegistroLog 
Go--#SQL 

Create Proc spp_AIE_RegistroLog 
(
	@IdAccesoExterno varchar(4) = '0001', @Sentencia varchar(500) = '', @MAC varchar(20) = '', @Host varchar(100) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD     
	
	Insert Into AIE_ADT_Accesos ( IdAccesoExterno, Sentencia, MAC, NombreHost ) 
	Select @IdAccesoExterno, @Sentencia, @MAC, @Host  
	
End 
Go--#SQL 

--	select * from AIE_ADT_Accesos 
