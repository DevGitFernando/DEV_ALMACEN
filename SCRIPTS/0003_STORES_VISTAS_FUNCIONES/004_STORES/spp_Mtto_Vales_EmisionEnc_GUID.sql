------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Vales_EmisionEnc_GUID' and xType = 'P' )  
   Drop Proc spp_Mtto_Vales_EmisionEnc_GUID  
Go--#SQL 

--		Exec spp_Mtto_Vales_EmisionEnc_GUID '001', '11', '0094', '00005324', 'kjhgkfguyuoyoiqryiuory'

Create Proc spp_Mtto_Vales_EmisionEnc_GUID  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0019', 
	@FolioVale varchar(30) = '00005324', @GUID varchar(100) = '', @QR image = null    
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	
	@sMensaje varchar(500)

	Set @sMensaje = ''
	
	If Not Exists ( Select * From Vales_EmisionEnc_GUID (nolock)
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioVale = @FolioVale )	
		Begin
			
			Insert Into Vales_EmisionEnc_GUID ( IdEmpresa, IdEstado, IdFarmacia, FolioVale, GUID, QR )
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVale, @GUID, @QR 
			
			Set @sMensaje = 'La Información se grabo satisfactoriamente en el folio vale: ' + @FolioVale
		End
	
	Select @FolioVale as FolioVale, @sMensaje as Mensaje
	
End 
Go--#SQL 


