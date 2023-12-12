------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_FACT_NotasDeCredito_TasaIva' and xType = 'FN' )
   Drop Function fg_FACT_NotasDeCredito_TasaIva  
Go--#SQL     
      
Create Function dbo.fg_FACT_NotasDeCredito_TasaIva
(
	@IdEmpresa varchar(3) = '',  @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @Serie Varchar(20) = '', @Folio varchar(10) = '' 
)  
Returns Varchar(20)--Numeric(14,4)
With Encryption 
As 
Begin 
Declare 
    @TasIva Varchar(20)-- Numeric(14,4)    
    
	Set @TasIva = '' 
	
	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2)
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4) 


	Select Top 1 @TasIva = TasaIva 
	From FACT_CFD_Documentos_Generados_Detalles R (NoLock) 
	Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia and R.Serie = @Serie And R.Folio = @Folio
	
    return @TasIva  
          

End 
Go--#SQL 


