
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFG_CB_CuadroBasico_Claves' and xType = 'P')
    Drop Proc spp_Mtto_CFG_CB_CuadroBasico_Claves
Go--#SQL
  
Create Proc spp_Mtto_CFG_CB_CuadroBasico_Claves ( @IdEstado varchar(2), @IdCliente varchar(4), @IdNivel int, @IdClaveSSA_Sal varchar(4), @iOpcion smallint )
With Encryption 
As
Begin 
Set NoCount On

	If Not Exists ( Select * From CFG_CB_CuadroBasico_Claves (NoLock) 
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdNivel = @IdNivel and IdClaveSSA_Sal = @IdClaveSSA_Sal ) 
	   Begin 
		  Insert Into CFG_CB_CuadroBasico_Claves ( IdEstado, IdCliente, IdNivel, IdClaveSSA_Sal ) 
		  Select @IdEstado, @IdCliente, @IdNivel, @IdClaveSSA_Sal 
	   End 
	Else 
	   Begin 				   
			update CFG_CB_CuadroBasico_Claves Set Status = 'A', Actualizado = 0  
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdNivel = @IdNivel and IdClaveSSA_Sal = @IdClaveSSA_Sal 	   
	   End 
	   	   
--	Select @IdNivel as Grupo 
End
Go--#SQL 
