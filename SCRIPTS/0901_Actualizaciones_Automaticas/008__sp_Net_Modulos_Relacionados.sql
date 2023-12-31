If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Net_Modulos_Relacionados' and xType = 'P' ) 
   Drop Proc sp_Net_Modulos_Relacionados 
Go--#SQL 

Create Proc sp_Net_Modulos_Relacionados  
( 
	@IdModulo varchar(6), @IdModulo_Relacionado varchar(6), @Status varchar(1) = 'A'
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @IdModuloRelacion varchar(6)
 	
	If Not Exists ( Select * From Net_Modulos_Relacionados (NoLock) 
		Where IdModulo = @IdModulo and IdModuloRelacionado = @IdModulo_Relacionado ) 
	   Begin 
		   Select @IdModuloRelacion = right('000000' + cast(max(IdModuloRelacion) + 1 as varchar), 6) From Net_Modulos_Relacionados (NoLock)
		   Set @IdModuloRelacion = IsNull(@IdModuloRelacion, '000001')  
		   
	       Insert Into Net_Modulos_Relacionados ( IdModuloRelacion, IdModulo, IdModuloRelacionado, Status, Actualizado ) 
	       Select @IdModuloRelacion, @IdModulo, @IdModulo_Relacionado, 'A', 0 
	       
	   End 
	Else 
	   Begin 
	   
		   Select @IdModuloRelacion = IdModuloRelacion  
		   From Net_Modulos_Relacionados (NoLock)
		   Where IdModulo = @IdModulo and IdModuloRelacionado = @IdModulo_Relacionado 
		   	   
		   Update M Set Status = @Status, Actualizado = 0 
		   From Net_Modulos_Relacionados (NoLock) 
		   Where IdModuloRelacion = @IdModuloRelacion  
		   	   
	   End    
	   	       	   
End 
Go--#SQL 

