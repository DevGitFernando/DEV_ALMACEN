If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Usuarios_Mtto' and xType = 'P' )
   Drop Proc spp_Net_Usuarios_Mtto 
Go--#SQL 

Create Proc spp_Net_Usuarios_Mtto 
( 
	@IdPersonal varchar(8), @Nombre varchar(100) = '', @LoginUser varchar(50), @Password varchar(500), @iTipo smallint = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(100) 

	Set @sMensaje = '' 
	
----	If @IdPersonal = '*' 
----	   Select @IdPersonal = cast(max(cast(IdPersonal as int)) + 1 as varchar) From Net_Usuarios (NoLock) 

	Set @IdPersonal = IsNull(@IdPersonal, '1') 
	Set @IdPersonal = right('00000' + @IdPersonal, 8 ) 


	If @iTipo = 1 
	   Begin 	
			If Not Exists ( Select LoginUser From Net_Usuarios (NoLock) 
				Where IdPersonal = @IdPersonal ) 
			   Insert Into Net_Usuarios ( IdPersonal, Nombre, LoginUser, Password, Status, Actualizado ) 
			   Select @IdPersonal, @Nombre, @LoginUser, @Password, 'A', 0  
			Else 
			   Update Net_Usuarios Set Password = @Password, FechaUpdate = getdate(), Status = 'A', Actualizado = 0 
			   Where IdPersonal = @IdPersonal 
			   
			Set @sMensaje = 'Información guardada satisfactoriamente con la Clave ' + @IdPersonal     
	   End 
    Else 
       Begin 
		   Update Net_Usuarios Set FechaUpdate = getdate(), Status = 'C', Actualizado = 0 
		   Where IdPersonal = @IdPersonal 
			Set @sMensaje = 'Información cancelada satisfactoriamente. ' 
	   End 
	
	Select @sMensaje as Mensaje 
	   
End
Go--#SQL 

--		select * from Net_Usuarios 
