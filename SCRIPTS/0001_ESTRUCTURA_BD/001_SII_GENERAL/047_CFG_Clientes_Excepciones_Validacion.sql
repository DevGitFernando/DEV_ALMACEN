
-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_EX_Validacion_Titulos' and xType = 'U' )
   Drop Table CFG_EX_Validacion_Titulos  
Go--#SQL  

Create Table CFG_EX_Validacion_Titulos  
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 	

	Cliente varchar(100) Not Null Default '', 	
	SubCliente varchar(100) Not Null Default '', 
	Programa varchar(100) Not Null Default '', 	
	SubPrograma varchar(100) Not Null Default '', 		

--	IdClienteAux varchar(4) Not Null, 		
--	IdSubClienteAux varchar(4) Not Null, 	
--	IdPrograma varchar(4) Not Null, 	
--	IdSubPrograma varchar(4) Not Null, 			
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CFG_EX_Validacion_Titulos 
	Add Constraint PK_CFG_EX_Validacion_Titulos Primary Key ( IdEstado, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma ) 
Go--#SQL 


--Insert Into CFG_EX_Validacion_Titulos select '21', '0002', '0005', '0002', '1312', 'FASSA', 'SERVICIOS DE SALUD DEL ESTADO DE PUEBLA', 'FASSA', 'POBLACION ABIERTA', 'A', 0 
-- Insert Into CFG_EX_Validacion_Titulos   Select '21', '0002', '0005', '0002', '1312', 'FASSA', 'SERVICIOS DE SALUD DEL ESTADO DE PUEBLA', 'FASSA', 'POBLACION ABIERTA', 'A', 0 


If Not Exists ( Select * From CFG_EX_Validacion_Titulos Where IdEstado = '21' and IdCliente = '0002' and IdSubCliente = '0005' and IdPrograma = '0002' and IdSubPrograma = '1312' )  Insert Into CFG_EX_Validacion_Titulos (  IdEstado, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Cliente, SubCliente, Programa, SubPrograma, Status, Actualizado )  Values ( '21', '0002', '0005', '0002', '1312', 'FASSA', 'SERVICIOS DE SALUD DEL ESTADO DE PUEBLA', 'FASSA', 'POBLACION ABIERTA', 'A', 0 )    Else Update CFG_EX_Validacion_Titulos Set Cliente = 'FASSA', SubCliente = 'SERVICIOS DE SALUD DEL ESTADO DE PUEBLA', Programa = 'FASSA', SubPrograma = 'POBLACION ABIERTA', Status = 'A', Actualizado = 0 Where IdEstado = '21' and IdCliente = '0002' and IdSubCliente = '0005' and IdPrograma = '0002' and IdSubPrograma = '1312'   
Go--#SQL 


--	sp_generainserts 'CFG_EX_Validacion_Titulos' , 1 


/* 
select top 10 *
from vw_Programas_SubProgramas 
where IdPrograma = 2 and SubPrograma like '%poblacion%'


select top 10 *
from VentasEnc
where IdPrograma = 2 and IdSubPrograma = 1312 
*/ 