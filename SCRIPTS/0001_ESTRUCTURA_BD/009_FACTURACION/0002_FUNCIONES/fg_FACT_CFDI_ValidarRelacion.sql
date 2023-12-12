------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_FACT_CFDI_ValidarRelacion' and xType = 'FN' )
   Drop Function fg_FACT_CFDI_ValidarRelacion  
Go--#SQL     
      
Create Function dbo.fg_FACT_CFDI_ValidarRelacion
( 
	@IdEmpresa varchar(3) = '001',  @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001',
	@Serie Varchar(20) = 'pruebas', @Folio int = 2
)  
Returns bit--Numeric(14,4)
With Encryption 
As 
Begin 
Declare 
	@iResultado bit 
	
    
	Set @iResultado = 0  
	
	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2)
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4) 
	--Set @Serie_Relacionada = Upper(@Serie_Relacionada)


	Select Top 1 @iResultado = 1 
	--Select Sum(Total)
	From FACT_CFD_Documentos_Generados R (NoLock)
	Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia 
		and Upper(R.Serie_Relacionada) = @Serie And R.Folio_Relacionado = @Folio
		and Status = 'A' 

	
	Set @iResultado = Isnull(@iResultado, 0) 
	

    return @iResultado  
          
End 
Go--#SQL 


