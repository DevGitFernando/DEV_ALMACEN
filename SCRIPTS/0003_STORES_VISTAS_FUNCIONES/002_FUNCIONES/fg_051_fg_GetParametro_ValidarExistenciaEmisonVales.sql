If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_GetParametro_ValidarExistenciaEmisonVales' and xType = 'FN' )
   Drop Function fg_GetParametro_ValidarExistenciaEmisonVales 
Go--#SQL     

----	Select  dbo.fg_GetParametro_ValidarExistenciaEmisonVales ('9', '11')

      
Create Function dbo.fg_GetParametro_ValidarExistenciaEmisonVales 
( 
	@IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0011'  --, @TipoUnidad int = 1  
)       
Returns bit   
With Encryption 
As 
Begin 
Declare 
	@bValorParametro bit, 
    @sValorParametro varchar(30),  
    @sValor varchar(2), 
    @sArbol varchar(10)  
    
    Set @bValorParametro = 0
    Set @sArbol = 'PFAR'-- (case when @TipoUnidad = 1 then 'ALMN' else 'PFAR' end) 
    
    Select @sValorParametro = Upper(Valor)  
    From Net_CFGC_Parametros (NoLock) 
    Where IdEstado = right('0000' + @IdEstado, 2) and IdFarmacia = right('0000' + @IdFarmacia, 4)  
          and ArbolModulo = @sArbol and NombreParametro = 'EmisionVales_ValidarExistencia'   -- 'NoValidarClavesEnPerfil' 
    
    --		'TipoDispensacionVale'   
    --		'NoValidarClavesEnPerfil'       
    
    --------- Solo los almacenes pueden trabajar sin que exista el parametro 
    if @sValorParametro is null -- and @TipoUnidad = 1 
		Set @sValorParametro = IsNull(@sValorParametro, 'FALSE') 
	else 	
		Set @sValorParametro = IsNull(@sValorParametro, 'TRUE')     
    
    
    If @sValorParametro = 'TRUE'  
       Set @bValorParametro = 1  
       
       
    -- Set @sValor = 0 

    return @bValorParametro
          
End 
Go--#SQL 

-- select dbo.fg_GetParametro_ValidarExistenciaEmisonVales('21', '0101' ) 
 