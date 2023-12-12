

If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CatAcreedores' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CatAcreedores 
Go--#SQL    

Create Proc spp_Mtto_FACT_CatAcreedores 
(
	@IdEstado varchar(2) = '21', @IdAcreedor varchar(4) = '0001', @Nombre varchar(100) = '', @RFC varchar(15) = 'DIMJ19830516AIJ', 
	@Pais varchar(100) = '', @Estado varchar(100) = '', @Municipio varchar(100) = '', @Localidad varchar(100) = '', 
	@Colonia varchar(100) = '', @Calle varchar(100) = '', @NoExterior varchar(8) = '', @NoInterior varchar(8) = '', 
	@CodigoPostal varchar(100) = '', @Referencia varchar(100) = '', @Opcion int = 1 
) 
As 
Begin 
Set NoCount On 
Declare 
	@iTipo int,
	@Mensaje varchar(200),
	@sStatus varchar(1), 
	@iActualizado smallint 
	
	Set @iTipo = 0 
	Set @Mensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	If @IdAcreedor = '*' 
	Begin			
		Select @IdAcreedor = cast( (max(IdAcreedor) + 1) as varchar) From FACT_CatAcreedores (NoLock)
		Where IdEstado = @IdEstado				
	End 
	
	Set @IdAcreedor = IsNull(@IdAcreedor, '1') 
	Select @IdAcreedor = right(replicate('0', 4) + @IdAcreedor, 4)
	
		
	If @Opcion =  1 
		Begin 
			If Not Exists ( Select * From FACT_CatAcreedores (NoLock)  Where IdEstado = @IdEstado and IdAcreedor = @IdAcreedor ) 
				Begin 		
					Insert Into FACT_CatAcreedores ( IdEstado, IdAcreedor, Nombre, RFC, Pais, Estado, Municipio, Localidad, 
							Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia )  
					Select @IdEstado, @IdAcreedor, @Nombre, @RFC, @Pais, @Estado, @Municipio, @Localidad, 
							@Colonia, @Calle, @NoExterior, @NoInterior, @CodigoPostal, @Referencia 
					
					Set @Mensaje = 'Se ha registrado satisfactoriamente el Acreedor con la Clave [' +  @IdAcreedor + '] ' 
				End 
			Else 
				Begin 
					Update C Set @Nombre = Nombre, RFC = @RFC, Pais = @Pais, 
						Estado = @Estado, Municipio = @Municipio, Localidad = @Localidad, 
						Colonia = @Colonia, Calle = @Calle, NoExterior = @NoExterior, NoInterior = @NoInterior, 
						CodigoPostal = @CodigoPostal, Referencia = @Referencia, Status = @sStatus
					From FACT_CatAcreedores C (NoLock)  Where IdEstado = @IdEstado and IdAcreedor = @IdAcreedor 	
					
					Set @Mensaje = 'Se actualizo satisfactoriamente el Acreedor con la Clave [' +  @IdAcreedor + '] ' 
				End 	
		End 
	Else 
		Begin 
			Set @sStatus = 'C'
			Update C Set Status = @sStatus
			From FACT_CatAcreedores C (NoLock)  Where IdEstado = @IdEstado and IdAcreedor = @IdAcreedor 
			Set @Mensaje = 'Se cancelo satisfactoriamente el Acreedor con la Clave [' +  @IdAcreedor + '] ' 			
		End 


	Select 	@IdAcreedor as Folio, @Mensaje as Mensaje  

End 
Go--#SQL 