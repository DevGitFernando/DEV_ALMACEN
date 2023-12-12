--------------------------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_INV_FolioAleatorio_Productos' and xType = 'FN' ) 
   Drop Function fg_INV_FolioAleatorio_Productos 
Go--#SQL 

Create Function dbo.fg_INV_FolioAleatorio_Productos ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) ) 
Returns varchar(6) 
With Encryption 
As 
Begin 
Declare 
	@sFolio varchar(6) 
	
	Select @sFolio = cast( ((max(cast(Folio as int)) + 1)) as varchar)  
		   From INV_Aleatorios_Productos_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		   
	Set @sFolio = IsNull(@sFolio, '1') 
	Set @sFolio = right(replicate('0', 6) + @sFolio, 6)
	
	Return @sFolio 

End 
Go--#SQL 