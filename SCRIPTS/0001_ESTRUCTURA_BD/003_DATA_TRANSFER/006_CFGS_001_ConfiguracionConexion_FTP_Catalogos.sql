
---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_ConfigurarFTP_Catalogos' and xType = 'U' )
   Drop Table CFGS_ConfigurarFTP_Catalogos 
Go--#SQL   

Create Table CFGS_ConfigurarFTP_Catalogos 
(
	IdEstado varchar(2) Not Null Default '',  
    ServidorFTP varchar(100) Not Null Default '', 	
    UserFTP varchar(50) Not Null Default '', 
    PasswordFTP varchar(500) Not Null Default '', 	
	DirectorioDeTrabajo varchar(500) Not Null Default '',  
	
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL   

Alter Table CFGS_ConfigurarFTP_Catalogos Add Constraint PK_CFGS_ConfigurarFTP_Catalogos Primary Key ( IdEstado )
Go--#SQL  


------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGS_ConfigurarFTP_Catalogos' and xType = 'P' )
   Drop Proc spp_CFGS_ConfigurarFTP_Catalogos 
Go--#SQL  

Create Proc spp_CFGS_ConfigurarFTP_Catalogos 
( 
	@IdEstado varchar(2) = '', @ServidorFTP varchar(100) = '', @UserFTP varchar(50) = '', @PasswordFTP varchar(500) = '', 
	@DirectorioDeTrabajo varchar(500) = '', @Status varchar(1) = 'A'
) 
With Encryption 	
As 
Begin 

	--- Solo debe existir una configuracion de conexion 
	Delete From  CFGS_ConfigurarFTP_Catalogos Where IdEstado = @IdEstado 
	
	Insert Into CFGS_ConfigurarFTP_Catalogos ( IdEstado, ServidorFTP, UserFTP, PasswordFTP, DirectorioDeTrabajo, Status  ) 
	Select @IdEstado, @ServidorFTP, @UserFTP, @PasswordFTP, @DirectorioDeTrabajo, @Status  	

End 
Go--#SQL   