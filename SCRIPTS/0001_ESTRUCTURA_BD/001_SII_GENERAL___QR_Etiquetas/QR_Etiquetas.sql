If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'QR_Etiquetas' and xType = 'U' ) 
Begin 
	Create Table QR_Etiquetas 
	( 
		IdEstado varchar(2) Not Null Default '', 
		IdFarmacia varchar(4) Not Null Default '', 
		Folio varchar(8) Not Null Default '', 	

		IdPasillo int Not Null Default 0, 
		IdEstante int Not Null Default 0, 
		IdEntrepaño int Not Null Default 0, 		
		
		FechaRegistro Datetime Not Null Default '', 
		FechaCancelacion Datetime Not Null Default '', 
			
		Datos xml Not Null Default '', 
		
		Status varchar(2) Not Null Default 'A' 
	) 
	
	Alter Table QR_Etiquetas Add Constraint PK_QR_Etiquetas Primary Key ( IdEstado, IdFarmacia, Folio ) 
End 
Go--#SQL 

 
  
----------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_QR_Etiquetas' and xType = 'P' ) 
   Drop Proc spp_QR_Etiquetas 
Go--#SQL  

Create Proc spp_QR_Etiquetas 
(
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0128',  @Folio varchar(8) = '*',   
	@IdPasillo int = 0, @IdEstante int = 0, @IdEntrepaño int = 0,   
	@Datos xml = '', @Opcion int = 1   
)
As 
Begin 
Set NoCount On 

	If @Folio = '*'
	   Select @Folio = cast(max(Folio) + 1 as varchar) From QR_Etiquetas (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
	   
	Set @Folio = IsNull(@Folio, '1') 
	Set @Folio = right('000000000000' +  @Folio, 8)    
	    

	If Not Exists ( Select * From QR_Etiquetas (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio ) 
	   Begin 
	      Insert Into QR_Etiquetas ( IdEstado, IdFarmacia, Folio, IdPasillo, IdEstante, IdEntrepaño, FechaRegistro, FechaCancelacion, Datos, Status ) 
	      Select @IdEstado, @IdFarmacia, @Folio, @IdPasillo, @IdEstante, @IdEntrepaño, getdate() as FechaRegistro, getdate() as FechaCancelacion, @Datos, 'A' As Status 
	   End 
	Else 
	   Begin 
	      If @Opcion = 1 
			  Begin 
				  Update Q Set Datos = @Datos 
				  From QR_Etiquetas Q (NoLock) 
				  Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
			  End 
		  Else 
			  Begin 
				  Update Q Set Status = 'C', FechaCancelacion = getdate() 
				  From QR_Etiquetas Q (NoLock) 
				  Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
			  End 		  
	   End  
	
---	Folio de etiqueta generado 	
	Select @Folio As Folio 
	
End 
Go--#SQL 