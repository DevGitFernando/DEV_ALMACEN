If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_CartasFaltantes_Detalles' and xType = 'P' )
    Drop Proc spp_Mtto_COM_CartasFaltantes_Detalles
Go--#SQL
						  
Create Proc spp_Mtto_COM_CartasFaltantes_Detalles 
(	
	@Folio varchar(8), @ClaveSSA varchar(30)
 ) 
 With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(2), 
		@iActualizado smallint		


	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	-- Set @FolioMovtoInv = '0'


	If Not Exists ( Select * From COM_CartasFaltantes_Detalles (NoLock) 
				   Where Folio = @Folio and ClaveSSA = @ClaveSSA ) 
	  Begin 
		 Insert Into COM_CartasFaltantes_Detalles ( Folio, ClaveSSA, Status, Actualizado ) 
		 Select @Folio, @ClaveSSA, @sStatus, @iActualizado 
	  End 	

	Set @sStatus = 'F'

	If Not Exists ( Select * From COM_CartasFaltantes_ClavesSSA (NoLock) 
				   Where ClaveSSA = @ClaveSSA ) 
		Begin 
			 Insert Into COM_CartasFaltantes_ClavesSSA ( ClaveSSA, Status, Actualizado ) 
			 Select @ClaveSSA, @sStatus, @iActualizado 
		End 
	Else
		Begin
			Update COM_CartasFaltantes_ClavesSSA Set Status = @sStatus, Actualizado = @iActualizado
			 Where ClaveSSA = @ClaveSSA		
		End	

End
Go--#SQL	


	