---------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Vales_ConfigurarConexiones' and xType = 'U' )
   Drop Table CFG_Vales_ConfigurarConexiones 
Go--#SQL   

Create Table CFG_Vales_ConfigurarConexiones 
(
	IdConexion int Not Null identity(1,1), 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	

	SSL bit Not Null Default 'false', 
	Servidor varchar(100) Not Null Default '', 
	WebService varchar(100) Not Null Default '', 
	PaginaWeb varchar(100) Not Null Default '', 
	IdOrden int Not Null Default 0, 
		
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL   

Alter Table CFG_Vales_ConfigurarConexiones Add Constraint PK_CFG_Vales_ConfigurarConexiones Primary Key ( Servidor, WebService, PaginaWeb )
Go--#SQL   

	Insert Into CFG_Vales_ConfigurarConexiones ( IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status ) 
	Select  '000', '00', '0000', 1, 'intermedcom.cloudapp.net', 'wsEPharmaInformacionDeVales' as WebService, 'wsEPharmaVales' as PaginaWeb, 1, 'A' 
Go--#SQL   