
Declare 
	@ServerViejo varchar(100),  
	@ServerNuevo varchar(100),  
	@CambiarNombre bit, 
	@Jurisdiccion int,  
	@NumeroFarmacia int  


	Select @CambiarNombre = 0  


	Select @Jurisdiccion = 6 
	Set @NumeroFarmacia = 2 

	Select @ServerNuevo = 'PL-' + right('0000' + cast(@Jurisdiccion as varchar), 2) + '-' + right('0000' + cast(@NumeroFarmacia as varchar), 4) 
	Select @ServerViejo = @@servername 


	If @CambiarNombre = 0 
		Begin 
			Select @ServerViejo, @ServerNuevo  
		End 
	Else 
		Begin 
			Exec sp_dropserver @ServerViejo 
			Exec sp_addserver @ServerNuevo, 'local'
		End 

