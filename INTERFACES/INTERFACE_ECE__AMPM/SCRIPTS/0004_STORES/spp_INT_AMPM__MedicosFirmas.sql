-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_AMPM__MedicosFirmas' and xType = 'P' ) 
   Drop Proc spp_INT_AMPM__MedicosFirmas
Go--#SQL 

Create Proc spp_INT_AMPM__MedicosFirmas 
( 
	@NombreFirma varchar(200) = '', 
	@FirmaDigital varchar(max) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 


	If Not Exists ( Select NombreFirma From INT_AMPM__MedicosFirmas (NoLock) Where NombreFirma = @NombreFirma ) 
		Begin 
			Insert Into INT_AMPM__MedicosFirmas ( NombreFirma, FirmaDigital, FechaRegistro, FechaActualizacion, Status ) 
			Select @NombreFirma, @FirmaDigital, getdate() as FechaRegistro, getdate() as FechaActualizacion, 'A' as Status  
		End 
	Else 
		Begin 
			Update F Set FirmaDigital = @FirmaDigital, FechaActualizacion = getdate() 
			From INT_AMPM__MedicosFirmas F (NoLock) 
			Where NombreFirma = @NombreFirma 
		End 


End 
Go--#SQL 

