
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'fg_MetodosDePago_VP' and xType = 'FN' ) 
   Drop Function dbo.fg_MetodosDePago_VP 
Go--#SQL    	

---		select dbo.fg_MetodosDePago_VP( '001', '21', '0001', 'PE', 30004, 3 ) 

Create Function fg_MetodosDePago_VP 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@Serie varchar(10) = '', @Folio varchar(10) = '', @Tipo smallint = 1  
) 	
Returns varchar(max)    
With Encryption 
As 
Begin 
Declare 
	@sMetodoDePago varchar(100), 
	@sMetodoDePagoDescripcion varchar(1000), 	
	@sMetodoDePagoReferencia varchar(1000), 		
	@sListaMetodosDePago varchar(max) 
	
	
	Set @sMetodoDePago = '' 
	Set @sMetodoDePagoDescripcion = '' 
	Set @sMetodoDePagoReferencia = '' 
	Set @sListaMetodosDePago = '' 
	

	
	----------------------------- RECORRER LA LISTA DE METODOS DE PAGO 	
	Declare #cursorMetodosDePago  
	Cursor For 
		Select S.IdMetodoDePago, M.Descripcion as MetodoDePagoDescripcion, S.Referencia 
		From CFDI_Documentos_MetodosDePago_VP S (NoLock)  
		Inner Join CFDI_FormasDePago M (NoLock) On ( S.IdMetodoDePago = M.IdFormaDePago ) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and Serie = @Serie and Folio = @Folio and Importe > 0 
		Order by Importe desc 
	Open #cursorMetodosDePago 
	FETCH NEXT FROM #cursorMetodosDePago Into @sMetodoDePago, @sMetodoDePagoDescripcion, @sMetodoDePagoReferencia   
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			If @Tipo = 1
				Set @sListaMetodosDePago = @sListaMetodosDePago + @sMetodoDePago + ',' 
			
			If @Tipo = 2 
				Set @sListaMetodosDePago = @sListaMetodosDePago + @sMetodoDePagoDescripcion + ',' 	
			
			If @Tipo = 3 
				Set @sListaMetodosDePago = @sListaMetodosDePago + @sMetodoDePagoReferencia + ',' 				
			
			FETCH NEXT FROM #cursorMetodosDePago Into @sMetodoDePago, @sMetodoDePagoDescripcion, @sMetodoDePagoReferencia   
		END	 
	Close #cursorMetodosDePago 
	Deallocate #cursorMetodosDePago 	
	

	If @sListaMetodosDePago <> '' 
	Begin 
		Set @sListaMetodosDePago = left(@sListaMetodosDePago, len(@sListaMetodosDePago)-1)   
	End 	
	Else 
	Begin 
		Set @sListaMetodosDePago = '98'	
		Select Top 1 @sListaMetodosDePago = '' -- XMLMetodoPago 
		From CFDI_Documentos_VP (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and Serie = @Serie and Folio = @Folio 	
	End 	
	----------------------------- RECORRER LA LISTA DE METODOS DE PAGO 		



	
	return  @sListaMetodosDePago 
	
End 
Go--#SQL 