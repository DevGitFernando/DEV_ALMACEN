If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInicial_004' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInicial_004 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInicial_004  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182',  
	@IdPersonal varchar(6) = '0001', @Poliza_Salida varchar(20) = '' output, @Tipo int = 1  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	If @Tipo  = 1 
		Begin 
			Exec sp_Proceso_IntegrarInventarioInicial_004___Almacen @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @Poliza_Salida output  
		End 
	Else 
		Begin 
			Exec sp_Proceso_IntegrarInventarioInicial_004___Farmacia @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @Poliza_Salida output  
		End 	

 			
End 
Go--#SQL 

