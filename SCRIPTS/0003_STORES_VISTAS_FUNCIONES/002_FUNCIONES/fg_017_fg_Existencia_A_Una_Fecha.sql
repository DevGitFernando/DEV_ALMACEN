If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_Existencia_A_Una_Fecha' and xType = 'FN' ) 
   Drop Function fg_Existencia_A_Una_Fecha 
Go--#SQL

	
Create Function dbo.fg_Existencia_A_Una_Fecha 
(    
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182', 
    @IdProducto varchar(8) = '', @CodigoEAN varchar(30) = '',  
	@FechaRevision varchar(10) = '2011-10-31'
) 	
Returns numeric(14,4) 
With Encryption 
As 
Begin  
Declare @dExistencia numeric(14,4) 


    Select @dExistencia = ( Select Top 1 Existencia 
                            From MovtosInv_Det_CodigosEAN E (NoLock) 
                            Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and 
                                  E.IdProducto = @IdProducto and E.CodigoEAN = @CodigoEAN and 
                                  convert(varchar(10), E.FechaSistema, 120) <= @FechaRevision 
                            Order By Keyx Desc       
                           )        
                           	
--- Salida Final 
	return IsNull(@dExistencia, 0) 
End 
Go--#SQL 		
				