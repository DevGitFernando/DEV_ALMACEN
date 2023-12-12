If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INT_AMPM__MedicosFirmas' and xType = 'V' ) 
   Drop View vw_INT_AMPM__MedicosFirmas
Go--#SQL

Create View vw_INT_AMPM__MedicosFirmas
With Encryption 
As 

	Select FM.NombreFirma, CAST(N'' as XML).value('xs:base64Binary(sql:column("FirmaDigital"))', 'varbinary(max)') as FirmaDigital
	From INT_AMPM__MedicosFirmas FM (NoLock)
	
Go--#SQL 