------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_Parametro_TipoFacturacion' and xType = 'FN' )
   Drop Function fg_Parametro_TipoFacturacion  
Go--#SQL     
      
Create Function dbo.fg_Parametro_TipoFacturacion ( ) 
Returns int 
With Encryption 
As 
Begin 
Declare 
	@iEmiteVales int,  
    @sValor varchar(100) 
    
    Set @iEmiteVales = 0 
    Set @sValor = '' 
    
    Select @sValor = Valor 
    From Net_CFG_Parametros_Facturacion (NoLock) 
    Where NombreParametro = 'MostrarPreciosBaseEnFacturacion_Descuentos' 
    
    Set @sValor = IsNull(@sValor, '') 
	If @sValor = 'TRUE' 
	   Set @iEmiteVales = 1 

    return @iEmiteVales 
          
End 
Go--#SQL 

--  select dbo.fg_EmiteVales_Clave('11', '0002', '101' ) 
