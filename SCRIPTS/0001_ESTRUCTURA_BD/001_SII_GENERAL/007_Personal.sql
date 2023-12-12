If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal' and xType = 'U' ) 
	Drop Table CatPersonal
Go--#SQL 

Create Table CatPersonal 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	Nombre varchar(50) Not Null, 
	ApPaterno varchar(50) Not Null Default '', 
	ApMaterno varchar(50) Not Null Default '', 	
	FechaRegistro datetime Not Null Default getdate(), 
	Actualizado tinyint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A'
)
Go--#SQL  

Alter Table CatPersonal Add Constraint PK_CatPersonal Primary Key ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  
------------------------ 


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPersonal' and xType = 'P')
    Drop Proc spp_Mtto_CatPersonal
Go--#SQL 
  
Create Proc spp_Mtto_CatPersonal ( @IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4), 
	@Nombre varchar(50), @ApPaterno varchar(50), @ApMaterno varchar(50), @iOpcion smallint )
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado tinyint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdPersonal = '*' 
	   Select @IdPersonal = cast( (max(IdPersonal) + 1) as varchar)  From CatPersonal (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdPersonal = IsNull(@IdPersonal, '1')
	Set @IdPersonal = right(replicate('0', 4) + @IdPersonal, 4)


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CatPersonal (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal ) 
			  Begin 
				 Insert Into CatPersonal ( IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdPersonal, @Nombre, @ApPaterno, @ApMaterno, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatPersonal Set Nombre = @Nombre, ApPaterno = @ApPaterno, ApMaterno = @ApMaterno,
					Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdPersonal 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPersonal Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal
		   Set @sMensaje = 'La información de el Personal ' + @IdPersonal + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdPersonal as Personal, @sMensaje as Mensaje 
End
Go--#SQL 