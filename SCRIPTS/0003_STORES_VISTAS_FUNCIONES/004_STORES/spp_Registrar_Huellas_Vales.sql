------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Registrar_Huellas_Vales' and xType = 'P' ) 
   Drop Proc spp_Registrar_Huellas_Vales
Go--#SQL 

Create Proc spp_Registrar_Huellas_Vales
( 
	@ReferenciaHuella varchar(100) = '', @Dedo smallint = 0, @Huella varchar(max) = '' 
) 
As 
Begin 
Set NoCount On 

Declare 
	@CodigoDeSalida int,  
	@MensajeDeSalida varchar(500) 
	
	Set @CodigoDeSalida = -1 
	Set @MensajeDeSalida = ''

	If Exists ( Select * From FP_Huellas_Vales (NoLock) Where ReferenciaHuella = @ReferenciaHuella and NumDedo = @Dedo ) 
	Begin 
		Set @MensajeDeSalida = 'La huella ya se encuentra registrada, verifique.'
		Set @CodigoDeSalida = 0  
	End 
	Else 
	Begin 
		Insert Into FP_Huellas_Vales ( FechaRegistro, ReferenciaHuella, NumDedo, Huella, Status ) 
		Select getdate() as FechaRegistro, @ReferenciaHuella, @Dedo, @Huella, 'A' as Status 
		
		Set @MensajeDeSalida = 'La huella fue registrada satisfactoriamente.'
		Set @CodigoDeSalida = 1  		
	End 

--- Salida final 
	Select @CodigoDeSalida as CodigoDeSalida, @MensajeDeSalida as MensajeDeSalida 


End 
Go--#SQL 

