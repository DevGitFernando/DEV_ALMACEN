If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInterno_002' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInterno_002 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInterno_002  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
	@IdPersonal varchar(6) = '0001', @Tipo int = 1, @TipoInv tinyint = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	--	@TipoInv = 0 : Inventario Completo
	--	@TipoInv = 1 : Inventario Parcial

	If @Tipo  = 1 
		Begin 
			Exec sp_Proceso_IntegrarInventarioInterno_002___Almacen @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @TipoInv
		End 
	Else 
		Begin 
			Exec sp_Proceso_IntegrarInventarioInterno_002___Farmacia @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @TipoInv 
		End 		
 	 			
End 
Go--#SQL 

