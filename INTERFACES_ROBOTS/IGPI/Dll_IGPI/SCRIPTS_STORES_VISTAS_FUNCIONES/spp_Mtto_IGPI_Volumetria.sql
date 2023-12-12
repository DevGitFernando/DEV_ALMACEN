If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_IGPI_Volumetria' and xType = 'P' ) 
   Drop Proc spp_Mtto_IGPI_Volumetria 
Go--#SQL   

Create Proc spp_Mtto_IGPI_Volumetria  
( 
	@CodigoEAN varchar(30) = '', 
	@Peso Numeric(14,4) = 0, 
    @Ancho Numeric(14,4) = 0, 
    @Alto Numeric(14,4) = 0,     
    @Largo Numeric(14,4) = 0 	
)
With Encryption 
As 
Begin 
Set NoCount On 

	   
	If Not Exists ( Select * From IGPI_VolumetriaG (NoLock) Where CodigoEAN = @CodigoEAN ) 
	   Begin 
	      Insert Into IGPI_VolumetriaG ( CodigoEAN, Peso, Ancho, Alto, Largo, Status, Actualizado ) 
	      Select @CodigoEAN, @Peso, @Ancho, @Alto, @Largo, 'A', 0  
	   End 
	Else
	   Begin  
	      Update G Set Peso = @Peso, Ancho = @Ancho, Alto = @Alto, Largo = @Largo 
	      From IGPI_VolumetriaG F (NoLock) 
	      Where CodigoEAN = @CodigoEAN    
	   End 

End 
Go--#SQL   
  
 /* 
 	CodigoEAN varchar(30) Not Null, 
	Existencia int Not Null Default 0,  
	Status varchar(1) Not Null Default 'A',  
*/   