------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_CFDI_ComplementoPagos_Parcialidad' and xType = 'FN' )
   Drop Function fg_CFDI_ComplementoPagos_Parcialidad  
Go--#SQL     
      
Create Function dbo.fg_CFDI_ComplementoPagos_Parcialidad
( 
	@IdEmpresa varchar(3) = '001',  @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001',
	@Serie_Relacionada Varchar(20) = 'pruebas', @Folio_Relacionado int = 2
)  
Returns int 
With Encryption 
As 
Begin 
Declare 
    @Anterior int--  Varchar(20)-- Numeric(14,4)    
    
	Set @Anterior = 0 
	
	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2)
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4) 
	Set @Serie_Relacionada = Upper(@Serie_Relacionada)


	Select Top 1 @Anterior = count(*) 
	--Select Sum(Total)
	From CFDI_ComplementoDePagos_DoctosRelacionados R (NoLock)
	Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia 
		and Upper(R.Serie_Relacionada) = @Serie_Relacionada And R.Folio_Relacionado = @Folio_Relacionado
	
	Set @Anterior = IsNull(@Anterior + 1, 1) 

    return @Anterior  
          
End 
Go--#SQL 


