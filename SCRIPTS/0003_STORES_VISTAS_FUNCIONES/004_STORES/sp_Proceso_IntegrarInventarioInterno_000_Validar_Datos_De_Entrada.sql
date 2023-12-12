------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada  
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0032', @Tipo int = 2, 
	@ValidarCajasCompletas smallint = 0, @TipoDeInventario smallint = 0    	
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	If @Tipo  = 1 
		Begin 
			Exec sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Almacen @IdEmpresa, @IdEstado, @IdFarmacia, @ValidarCajasCompletas, @TipoDeInventario   
		End 
	Else 
		Begin 
			-- print '1_____________' 
			Exec sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada___Farmacia @IdEmpresa, @IdEstado, @IdFarmacia, @ValidarCajasCompletas, @TipoDeInventario   
		End 	

End 
Go--#SQL 



