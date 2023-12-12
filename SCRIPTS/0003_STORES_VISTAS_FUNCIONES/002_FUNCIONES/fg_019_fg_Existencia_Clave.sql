----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_Existencia_Clave' and xType = 'FN' )
   Drop Function fg_Existencia_Clave  
Go--#SQL     
      
Create Function dbo.fg_Existencia_Clave
(
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @ClaveSSA varchar(30)
) 
Returns int 
With Encryption 
As 
Begin 
Declare 
	@iExistencia int 
    
    Select @iExistencia = sum(Existencia)   
    From vw_ExistenciaPorCodigoEAN_Lotes (NoLock) 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		  and ClaveSSA = @ClaveSSA and MesesParaCaducar > 0 
    
    Set @iExistencia = IsNull(@iExistencia, 0) 

    return @iExistencia 
          
End 
Go--#SQL 

--  select dbo.fg_GetParametro_FechaSistema('21', '0101' ) 
