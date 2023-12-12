
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'fg_ObtenerMetodosDePago' and xType = 'FN' ) 
   Drop Function dbo.fg_ObtenerMetodosDePago 
Go--#SQL    	

---		select dbo.fg_ObtenerMetodosDePago( '001', '21', '0001', 'PE', 30004, 3 ) 

Create Function fg_ObtenerMetodosDePago 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@Serie varchar(10) = '', @Folio varchar(10) = '', 
	@EsVistaPrevia smallint = 0, @Tipo smallint = 1  
) 	
Returns varchar(max)    
With Encryption 
As 
Begin 
Declare 
	@sMetodoDePago varchar(100), 
	@sMetodoDePagoDescripcion varchar(1000), 	
	@sListaMetodosDePago varchar(max) 
	
	
	Set @sMetodoDePago = '' 
	Set @sMetodoDePagoDescripcion = '' 
	Set @sListaMetodosDePago = '' 
	
	If @EsVistaPrevia = 0
		Select @sListaMetodosDePago = dbo.fg_MetodosDePago( @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @Tipo ) 
	Else
		Select @sListaMetodosDePago = dbo.fg_MetodosDePago_VP( @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @Tipo ) 	
	
		
	
	return  @sListaMetodosDePago 
	
End 
Go--#SQL 