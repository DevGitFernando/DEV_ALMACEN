
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatEmpresas' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatEmpresas 
Go--#SQL

Create Proc spp_Mtto_CatEmpresas ( @IdEmpresa varchar(3), @Nombre varchar(100), @NombreCorto varchar(100), 
	@RFC varchar(16), 
	@EsConsignacion tinyint, @EdoCiudad varchar(100), @Colonia varchar(100), @CodigoPostal varchar(20), 
	@Domicilio varchar(200), @iOpcion smallint )  
With Encryption 
As
Begin 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	If @IdEmpresa = '*' 
	   Select @IdEmpresa = cast( (max(IdEmpresa) + 1) as varchar)  From CatEmpresas (NoLock) 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdEmpresa = IsNull(@IdEmpresa, '1')
	Set @IdEmpresa = right(replicate('0', 3) + @IdEmpresa, 3)

-- 	@EsConsignacion tinyint, @EdoCiudad varchar(100), @Colonia varchar(100), @CodigoPostal varchar(20),  
--	select * from catEmpresas 
	
	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CatEmpresas (NoLock) Where IdEmpresa = @IdEmpresa ) 
			  Begin 
				 Insert Into CatEmpresas ( IdEmpresa, Nombre, NombreCorto, RFC, EsDeConsignacion, 
						EdoCiudad, Colonia, Domicilio, CodigoPostal, Status, Actualizado ) 
				 Select @IdEmpresa, @Nombre, @NombreCorto, @RFC, @EsConsignacion, @EdoCiudad, 
						@Colonia, @Domicilio, @CodigoPostal, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatEmpresas Set Nombre = @Nombre, NombreCorto = @NombreCorto, 
										RFC = @RFC, EsDeConsignacion = @EsConsignacion, 
										EdoCiudad = @EdoCiudad, Colonia = @Colonia, 
										Domicilio = @Domicilio, CodigoPostal = @CodigoPostal,
										Status = @sStatus, Actualizado = @iActualizado
				 Where IdEmpresa = @IdEmpresa  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdEmpresa 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatEmpresas Set Status = @sStatus, Actualizado = @iActualizado Where IdEmpresa = @IdEmpresa 
		   Set @sMensaje = 'La información de la Empresa ' + @IdEmpresa + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdEmpresa as Estado, @sMensaje as Mensaje 


End 
Go--#SQL