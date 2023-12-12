------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_FACT_GetFacturaElectronica_Remision' and xType = 'FN' )
   Drop Function fg_FACT_GetFacturaElectronica_Remision  
Go--#SQL     
      
Create Function dbo.fg_FACT_GetFacturaElectronica_Remision
(
	@IdEmpresa varchar(3) = '',  @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @FolioRemision varchar(10) = '' 
)  
Returns varchar(50)   
With Encryption 
As 
Begin 
Declare 
    @sValorParametro varchar(30),  
    @sFacturaElectronica varchar(50), 
	@sSerie varchar(20), 
    @iFolio int  
    
	Set @sSerie = '' 
	Set @iFolio = 0 
	Set @sFacturaElectronica = '' 

	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2)
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4) 
	Set @FolioRemision= right('000000000000' + @FolioRemision, 10)


	Select @sFacturaElectronica = FolioFacturaElectronica 
	From FACT_Facturas R (NoLock) 
	Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia and R.FolioRemision = @FolioRemision and R.Status = 'A'  
	Set @sFacturaElectronica = IsNull(@sFacturaElectronica, '')

	If @sFacturaElectronica <> '' 
	Begin 
		-- Set @sFacturaElectronica = ''  
		Select @sFacturaElectronica = R.Serie + ' - ' + cast(R.Folio as varchar(20)) 
		From FACT_CFD_Documentos_Generados R (NoLock) 
		Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = @IdFarmacia and R.Status = 'A'  
			and R.Serie + ' - ' + cast(R.Folio as varchar(20)) = @sFacturaElectronica 
			
	End    
	Set @sFacturaElectronica = IsNull(@sFacturaElectronica, '')	


    return @sFacturaElectronica  
          

End 
Go--#SQL 


